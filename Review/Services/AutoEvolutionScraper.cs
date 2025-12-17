using Microsoft.Extensions.Logging;
using Microsoft.Playwright;
using Review.Handlers;
using Review.Model.DTO;
using Review.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Review.Services {
    public class AutoEvolutionScraper : ICarScraper {
        private const string BaseUrl = "https://www.autoevolution.com/cars/";
        private readonly ILogger<AutoEvolutionScraper> _logger;

        public AutoEvolutionScraper( ILogger<AutoEvolutionScraper> logger ) {
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



        private static BrowserNewContextOptions GetContextOptions()
            => new() {
                UserAgent =
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36",
                Locale = "hr-HR",
                ViewportSize = new() { Width = 1920, Height = 1080 }
            };
    }
}
