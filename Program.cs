using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

class Program
{
    public static async Task Main()
    {
        // Initialize Playwright
        using var playwright = await Playwright.CreateAsync();

        // Launch browser (headless = false to see the browser)
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });

        // Create a new page
        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();

        // Navigate to website
        await page.GotoAsync("https://www.saucedemo.com/");

        // Optional: click a button or fill a form
        await page.WaitForLoadStateAsync(LoadState.Load);
        await page.FillAsync("#user-name", "problem_user");
        await page.FillAsync("#password", "secret_sauce");
        await page.ClickAsync("#login-button");

        await page.WaitForSelectorAsync("//span[contains(text(),'Products')]");

        // Check title
        var title = await page.TitleAsync();
        Console.WriteLine($"Page title: {title}");

        // Screenshot (optional)
        await page.ScreenshotAsync(new PageScreenshotOptions { Path = "screenshot.png" });

        // Close browser
        await browser.CloseAsync();
    }
}
