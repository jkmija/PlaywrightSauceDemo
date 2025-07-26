using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightDemo.Core
{


    public class DriverFactory : IDriverFactory
    {
        private IPlaywright? _playwright;
        private IBrowser? _browser;

        public IPage? Page { get; private set; }
        public IBrowserContext? BrowserContext { get; private set; }

        public async Task<IPlaywright> InitAsync(bool headless = true)
        {
            _playwright = await Playwright.CreateAsync();

            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = headless
            });

            BrowserContext = await _browser.NewContextAsync();
            Page = await BrowserContext.NewPageAsync();
            return _playwright;
        }

        public async Task DisposeAsync()
        {
            await BrowserContext.CloseAsync();
            await _browser?.CloseAsync()!;
            _playwright?.Dispose();
        }
    }
}
