using Microsoft.Extensions.Logging;
using Microsoft.Playwright;
using Review.Handlers;
using Review.Model;
using Review.Model.DTO;
using Review.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Review.Services {
    public class AutoEvolutionScraper : ICarScraper {
        private const string BaseUrl = "https://www.autoevolution.com/cars/";
        private readonly ILogger<AutoEvolutionScraper> _logger;

        public AutoEvolutionScraper(
            ILogger<AutoEvolutionScraper> logger
        ) {
            _logger = logger;
        }

        public async Task<List<CarManDTO>> GetBrandsAsync() {
            var (playwright, browser) = await PlaywrightFactory.CreateAsync();
            await using var context = await browser.NewContextAsync( GetContextOptions() );
            var page = await context.NewPageAsync();

            await page.GotoAsync( BaseUrl, new() { WaitUntil = WaitUntilState.NetworkIdle } );
            await page.WaitForSelectorAsync(
                "div.carman a",
                new PageWaitForSelectorOptions {
                    State = WaitForSelectorState.Attached,
                    Timeout = 15000
                }
            );

            var raw = await page.EvaluateAsync<JsonElement>(
            @"() => {
    return Array.from(document.querySelectorAll('div.carman'))
        .map(div => {
            try {
                const a = div.querySelector('a');
                if (!a) return null;

                const title = a.getAttribute('title');
                const href = a.getAttribute('href');
                if (!title || !href) return null;

                return {
                    name: title.trim(),
                    url: new URL(href, document.baseURI).href
                };
            } catch {
                return null;
            }
        })
        .filter(x => x !== null);
}"
            );

            await browser.CloseAsync();
            playwright.Dispose();

            return raw.EnumerateArray()
                .Select( x => new CarManDTO {
                    Name = x.GetProperty( "name" ).GetString(),
                    Url = x.GetProperty( "url" ).GetString()
                } )
                .ToList();
        }

        public async Task<List<CarModDTO>> GetModelsAsync() {
            var brands = await GetBrandsAsync();
            var models = new List<CarModDTO>();

            var (playwright, browser) = await PlaywrightFactory.CreateAsync();
            await using var context = await browser.NewContextAsync( GetContextOptions() );
            var page = await context.NewPageAsync();

            page.SetDefaultNavigationTimeout( 45000 );

            int processed = 0;

            foreach( var brand in brands ) {
                if( string.IsNullOrWhiteSpace( brand.Url ) )
                    continue;

                bool loaded = false;

                // --- NAVIGATION WITH RETRY ---
                for( int attempt = 1; attempt <= 2; attempt++ ) {
                    try {
                        await page.GotoAsync( brand.Url, new PageGotoOptions {
                            WaitUntil = attempt == 1
                                ? WaitUntilState.DOMContentLoaded
                                : WaitUntilState.Load,
                            Timeout = 30000
                        } );

                        loaded = true;
                        break;
                    }
                    catch( TimeoutException ) {
                        _logger.LogWarning(
                            "Timeout loading brand '{Brand}' (attempt {Attempt})",
                            brand.Name,
                            attempt
                        );
                    }
                }

                if( !loaded ) {
                    _logger.LogWarning(
                        "Skipping brand '{Brand}' due to repeated timeouts",
                        brand.Name
                    );
                    continue;
                }

                // --- SMALL BUFFER FOR LAZY LOAD ---
                await page.WaitForTimeoutAsync( 1000 );

                // --- SCRAPE MODELS (SAFE, NO WAIT) ---
                JsonElement raw;
                try {
                    raw = await page.EvaluateAsync<JsonElement>(
        @"() => {
    const nodes = document.querySelectorAll('div.carmod a');
    if (!nodes || nodes.length === 0)
        return [];

    return Array.from(nodes)
        .map(a => {
            try {
                const name = a.innerText?.trim();
                const href = a.getAttribute('href');
                if (!name || !href) return null;

                return {
                    name,
                    url: new URL(href, document.baseURI).href
                };
            } catch {
                return null;
            }
        })
        .filter(x => x !== null);
}"
                    );
                }
                catch( Exception ex ) {
                    _logger.LogWarning(
                        ex,
                        "Evaluate failed for brand '{Brand}', skipping.",
                        brand.Name
                    );
                    continue;
                }

                if( raw.ValueKind != JsonValueKind.Array || raw.GetArrayLength() == 0 ) {
                    _logger.LogInformation(
                        "Brand '{Brand}' has no models.",
                        brand.Name
                    );
                    continue;
                }

                foreach( var x in raw.EnumerateArray() ) {
                    if( !x.TryGetProperty( "name", out var nameProp ) )
                        continue;
                    if( !x.TryGetProperty( "url", out var urlProp ) )
                        continue;

                    models.Add( new CarModDTO {
                        BrandName = brand.Name,
                        Name = nameProp.GetString(),
                        Url = urlProp.GetString()
                    } );
                }

                processed++;

                // --- POLITE SCRAPING ---
                await Task.Delay( 300 );

                // --- COOLDOWN EVERY 20 BRANDS ---
                if( processed % 20 == 0 ) {
                    _logger.LogInformation( "Cooldown after {Count} brands...", processed );
                    await Task.Delay( 3000 );
                }
            }

            await browser.CloseAsync();
            playwright.Dispose();

            return models;
        }

        public async Task<IEnumerable<Car>> GetAllCarsFromWebAsync( Model.Model model ) {
            var cars = new List<Car>();

            if( model == null || string.IsNullOrWhiteSpace( model.Url ) )
                return cars;

            var (playwright, browser) = await PlaywrightFactory.CreateAsync();
            await using var context = await browser.NewContextAsync( GetContextOptions() );
            var page = await context.NewPageAsync();

            try {
                // 1️⃣ MODEL PAGE
                await page.GotoAsync( model.Url, new() {
                    WaitUntil = WaitUntilState.DOMContentLoaded,
                    Timeout = 30000
                } );

                await page.WaitForTimeoutAsync( 1000 );

                // 2️⃣ GENERATION URLS
                var generationUrls = await page.EvaluateAsync<JsonElement>(
        @"() => {
    return Array.from(document.querySelectorAll('div.carmodel h2 a'))
        .map(a => {
            const href = a.getAttribute('href');
            if (!href) return null;
            return new URL(href, document.baseURI).href;
        })
        .filter(x => x !== null);
}"
                );

                if( generationUrls.ValueKind != JsonValueKind.Array )
                    return cars;

                // 3️⃣ GENERATIONS LOOP
                foreach( var genUrlEl in generationUrls.EnumerateArray() ) {
                    var generationUrl = genUrlEl.GetString();
                    if( string.IsNullOrWhiteSpace( generationUrl ) )
                        continue;

                    var car = new Car {
                        BrandID = model.BrandId,
                        ModelID = model.Id,
                        Engines = new List<Model.Engine>()
                    };

                    try {
                        await page.GotoAsync( generationUrl, new() {
                            WaitUntil = WaitUntilState.DOMContentLoaded,
                            Timeout = 30000
                        } );

                        await page.WaitForTimeoutAsync( 1000 );

                        // 4️⃣ GENERATION BASIC DATA
                        var genData = await page.EvaluateAsync<JsonElement>(
        @"() => {
    const h1 = document.querySelector('h1 a');
    const img = document.querySelector('img.curpo');
    const desc = document.querySelector('div.modelbox p');
    const years = document.querySelector('span.motlisthead_years');

    return {
        name: h1 ? h1.innerText.replace('Photos, engines & full specs','').trim() : null,
        image: img ? img.src : null,
        description: desc ? desc.innerText.trim() : null,
        years: years ? years.innerText.trim() : null
    };
}"
                        );

                        // NAME
                        if( genData.TryGetProperty( "name", out var nameProp ) )
                            car.Generation = nameProp.GetString();

                        // YEARS
                        if( genData.TryGetProperty( "years", out var yearsProp ) ) {
                            var years = yearsProp.GetString()?.Split( ',', StringSplitOptions.RemoveEmptyEntries );
                            if( years?.Length > 0 )
                                car.ModelYearFrom = new DateTime( int.Parse( years.First() ), 1, 1 );
                            if( years?.Length > 1 )
                                car.ModelYearTo = new DateTime( int.Parse( years.Last() ), 1, 1 );
                        }

                        // IMAGE
                        if( genData.TryGetProperty( "image", out var imgProp ) ) {
                            var imgUrl = imgProp.GetString();
                            if( !string.IsNullOrWhiteSpace( imgUrl ) )
                                car.ImageData = await page.APIRequest.GetAsync( imgUrl )
                                    .ContinueWith( t => t.Result.BodyAsync().Result );
                        }

                        // 5️⃣ ENGINE FRAGMENTS
                        var engineFragments = await page.EvaluateAsync<JsonElement>(
        @"() => {
    return Array.from(document.querySelectorAll('li.ellip'))
        .map(li => li.getAttribute('id'))
        .filter(x => x !== null);
}"
                        );

                        if( engineFragments.ValueKind == JsonValueKind.Array ) {
                            foreach( var frag in engineFragments.EnumerateArray() ) {
                                var fragment = frag.GetString();
                                if( string.IsNullOrWhiteSpace( fragment ) )
                                    continue;

                                var engineUrl = $"{generationUrl.Split( '#' )[0]}#aeng_{fragment}";

                                try {
                                    var engines = await ScrapeEngineData( page, engineUrl );
                                    foreach( var dto in engines ) {
                                        car.Engines.Add( new Engine {
                                            Name = dto.Name,
                                            Cylinders = dto.Cylinders,
                                            Displacement = dto.Displacement,
                                            Power = dto.Power,
                                            Torque = dto.Torque,
                                            FuelSystem = dto.FuelSystem,
                                            FuelType = dto.FuelType,
                                            FuelCapacity = dto.FuelCapacity,
                                            TopSpeed = dto.TopSpeed,
                                            Acceleration = dto.Acceleration,
                                            DriveType = dto.DriveType,
                                            Gearbox = dto.Gearbox,
                                            FrontBrakes = dto.FrontBrakes,
                                            RearBrakes = dto.RearBrakes,
                                            TireSize = dto.TireSize,
                                            Length = dto.Length,
                                            Width = dto.Width,
                                            Height = dto.Height,
                                            FrontRearTrack = dto.FrontRearTrack,
                                            Wheelbase = dto.Wheelbase,
                                            GroundClearance = dto.GroundClearance,
                                            CargoVolume = dto.CargoVolume,
                                            UnladenWeight = dto.UnladenWeight,
                                            GrossWeightLimit = dto.GrossWeightLimit,
                                            FuelEconomyCity = dto.FuelEconomyCity,
                                            FuelEconomyHighway = dto.FuelEconomyHighway,
                                            FuelEconomyCombined = dto.FuelEconomyCombined,
                                            CO2Emissions = dto.CO2Emissions
                                        } );
                                    }
                                }
                                catch( Exception ex ) {
                                    _logger.LogWarning( ex, $"Engine scrape failed: {engineUrl}" );
                                }

                                await Task.Delay( 300 );
                            }
                        }

                        cars.Add( car );
                    }
                    catch( TimeoutException ) {
                        _logger.LogWarning( $"Generation timeout: {generationUrl}" );
                    }

                    await Task.Delay( 500 );
                }
            }
            finally {
                await browser.CloseAsync();
                playwright.Dispose();
            }

            return cars;
        }


        private async Task<List<EngineDTO>> ScrapeEngineData(
            IPage page,
            string engineUrl
        ) {
            var engines = new List<EngineDTO>();

            try {
                await page.GotoAsync( engineUrl, new() {
                    WaitUntil = WaitUntilState.DOMContentLoaded,
                    Timeout = 30000
                } );

                await page.WaitForTimeoutAsync( 500 );

                var raw = await page.EvaluateAsync<JsonElement>(
        @"() => {
    const hash = location.hash?.replace('#', '');
    if (!hash) return null;

    const container = document.getElementById(hash.replace('aeng_li_', ''));
    if (!container) return null;

    const engineData = container.querySelector('div.enginedata');
    if (!engineData) return null;

    const tables = engineData.querySelectorAll('table.techdata');
    if (!tables.length) return null;

    const engine = {};

    tables.forEach(table => {
        const titleEl = table.querySelector('th.title span.col-green');
        if (titleEl && !engine.name)
            engine.name = titleEl.innerText.trim();

        table.querySelectorAll('tr').forEach(row => {
            const k = row.querySelector('td.left strong');
            const v = row.querySelector('td.right');
            if (!k || !v) return;

            engine[k.innerText.trim()] = v.innerText.trim();
        });
    });

    return engine;
}"
                );

                // 🔑 SAD PROVJERAVAMO OBJECT, NE ARRAY
                if( raw.ValueKind != JsonValueKind.Object )
                    return engines;

                var engine = new EngineDTO();

                if( raw.TryGetProperty( "name", out var n ) )
                    engine.Name = n.GetString();

                foreach( var p in raw.EnumerateObject() ) {
                    MapEngineProperty( engine, p.Name, p.Value.GetString() );
                }

                engines.Add( engine );
            }
            catch( TimeoutException ex ) {
                _logger.LogWarning( ex, $"Engine timeout: {engineUrl}" );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, $"Engine scrape failed: {engineUrl}" );
            }

            return engines;
        }


        private static void MapEngineProperty( EngineDTO engine, string key, string value ) {
            switch( key ) {
                case "Cylinders:":
                    engine.Cylinders = value;
                    break;
                case "Displacement:":
                    engine.Displacement = value;
                    break;
                case "Power:":
                    engine.Power = value;
                    break;
                case "Torque:":
                    engine.Torque = value;
                    break;
                case "Fuel System:":
                    engine.FuelSystem = value;
                    break;
                case "Fuel:":
                    engine.FuelType = value;
                    break;
                case "Fuel capacity:":
                    engine.FuelCapacity = ParseDecimal( value );
                    break;
                case "Top Speed:":
                    engine.TopSpeed = ParseInt( value );
                    break;
                case "Acceleration 0-62 Mph (0-100 kph):":
                    engine.Acceleration = ParseDecimal( value );
                    break;
                case "Drive Type:":
                    engine.DriveType = value;
                    break;
                case "Gearbox:":
                    engine.Gearbox = value;
                    break;
                case "Front Brakes:":
                    engine.FrontBrakes = value;
                    break;
                case "Rear Brakes:":
                    engine.RearBrakes = value;
                    break;
                case "Tire Size:":
                    engine.TireSize = value;
                    break;
                case "Length:":
                    engine.Length = value;
                    break;
                case "Width:":
                    engine.Width = value;
                    break;
                case "Height:":
                    engine.Height = value;
                    break;
                case "Front/Rear Track:":
                    engine.FrontRearTrack = value;
                    break;
                case "Wheelbase:":
                    engine.Wheelbase = value;
                    break;
                case "Ground Clearance:":
                    engine.GroundClearance = value;
                    break;
                case "Cargo Volume:":
                    engine.CargoVolume = value;
                    break;
                case "Unladen Weight:":
                    engine.UnladenWeight = value;
                    break;
                case "Gross Weight Limit:":
                    engine.GrossWeightLimit = value;
                    break;
                case "Fuel Economy (City):":
                    engine.FuelEconomyCity = value;
                    break;
                case "Fuel Economy (Highway):":
                    engine.FuelEconomyHighway = value;
                    break;
                case "Fuel Economy (Combined):":
                    engine.FuelEconomyCombined = value;
                    break;
                case "CO2 Emissions:":
                    engine.CO2Emissions = value;
                    break;
            }
        }



        private static decimal? ParseDecimal( string value ) {
            if( string.IsNullOrWhiteSpace( value ) )
                return null;

            var num = value.Split( ' ' )[0].Replace( ',', '.' );
            return decimal.TryParse( num, NumberStyles.Any, CultureInfo.InvariantCulture, out var d )
                ? d
                : null;
        }

        private static int? ParseInt( string value ) {
            if( string.IsNullOrWhiteSpace( value ) )
                return null;

            var num = value.Split( ' ' )[0];
            return int.TryParse( num, out var i ) ? i : null;
        }



        private static BrowserNewContextOptions GetContextOptions()
            => new() {
                UserAgent =
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36",
                Locale = "hr-HR",
                ViewportSize = new() { Width = 1920, Height = 1080 }
            };
    }
}
