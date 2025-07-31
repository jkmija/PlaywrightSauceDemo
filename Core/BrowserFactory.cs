using Microsoft.Playwright;

namespace PlaywrightDemo.Core
{
    public static class BrowserFactory
    {
        public static async Task<IBrowser> CreateAsync(string browserType, bool headless = true)
        {
            var playwright = await Playwright.CreateAsync();

            return browserType switch
            {
                "Chrome" => await playwright.Chromium.LaunchAsync(new() { Headless = headless }),
                "Firefox" => await playwright.Firefox.LaunchAsync(new() { Headless = headless }),
                "Webkit" => await playwright.Webkit.LaunchAsync(new() { Headless = headless }),
                _ => throw new ArgumentException("Unsupported browser type.")
            };
        }
    }
}


//IBrowser browser = await BrowserFactory.CreateAsync("Firefox", headless: false);
//IPage page = await browser.NewPageAsync();
//await page.GotoAsync("https://mijhail.dev");
