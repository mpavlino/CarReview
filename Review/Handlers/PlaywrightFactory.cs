using Microsoft.Playwright;
using System.Threading.Tasks;

namespace Review.Handlers {
    public static class PlaywrightFactory {
        public static async Task<(IPlaywright, IBrowser)> CreateAsync() {
            var playwright = await Playwright.CreateAsync();

            var browser = await playwright.Chromium.LaunchAsync( new BrowserTypeLaunchOptions {
                Headless = true, // false ako želiš gledati
                Args = new[]
                {
                "--disable-blink-features=AutomationControlled",
                "--no-sandbox"
            }
            } );

            return (playwright, browser);
        }
    }
}
