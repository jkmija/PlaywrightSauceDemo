using Microsoft.Playwright;

public class PlaywrightSingleton
{
    private static readonly Lazy<Task<PlaywrightSingleton>> _instance =
        new(() => CreateAsync(), LazyThreadSafetyMode.ExecutionAndPublication);

    public static Task<PlaywrightSingleton> Instance => _instance.Value;

    public IPlaywright Playwright { get; private set; }
    public IBrowser Browser { get; private set; }
    public IPage Page { get; private set; }
    public IBrowserContext? BrowserContext { get; private set; }

    private PlaywrightSingleton() { }

    private static async Task<PlaywrightSingleton> CreateAsync()
    {
        var singleton = new PlaywrightSingleton();
        singleton.Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        singleton.Browser = await singleton.Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        singleton.BrowserContext = await singleton.Browser.NewContextAsync();
        singleton.Page = await singleton.Browser.NewPageAsync();
        return singleton;
    }

    public async Task DisposeAsync()
    {
        await BrowserContext?.CloseAsync()!;
        await Browser?.CloseAsync()!;
        Playwright?.Dispose();
    }
}


//var singleton = await PlaywrightSingleton.Instance;
//var page = await singleton.Browser.NewPageAsync();
//await page.GotoAsync("https://mijhail.dev");
