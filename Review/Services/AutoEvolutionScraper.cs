using Microsoft.Playwright;
using Review.Handlers;
using Review.Model.DTO;
using Review.Model.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Review.Services {
    public class AutoEvolutionScraper : ICarScraper {
        private const string BaseUrl = "https://www.autoevolution.com/cars/";

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
    return Array.from(document.querySelectorAll('div.carman a'))
        .map(a => {
            try {
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

            foreach( var brand in brands ) {
                if( string.IsNullOrWhiteSpace( brand.Url ) )
                    continue;

                await page.GotoAsync( brand.Url, new PageGotoOptions {
                    WaitUntil = WaitUntilState.DOMContentLoaded,
                    Timeout = 30000
                } );

                // kratki buffer da lazy load odradi svoje
                await page.WaitForTimeoutAsync( 1000 );

                var raw = await page.EvaluateAsync<JsonElement>(
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

                if( raw.ValueKind != JsonValueKind.Array || raw.GetArrayLength() == 0 )
                    continue;

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

                await Task.Delay( 300 ); // polite scraping
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
