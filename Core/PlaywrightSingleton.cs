using Microsoft.Playwright;
using PlaywrightDemo.Core;
using PlaywrightDemo.Settings;

public class PlaywrightSingleton
{
    private static readonly Lazy<Task<PlaywrightSingleton>> _instance =
        new(() => CreateInstance(), LazyThreadSafetyMode.ExecutionAndPublication);

    public static Task<PlaywrightSingleton> Instance => _instance.Value;

    public IPlaywright Playwright { get; private set; }
    public IBrowser Browser { get; private set; }
    public IPage Page { get; private set; }
    public IBrowserContext? BrowserContext { get; private set; }

    // Change _config to static for use in static method
    private static AppSetting? _config;

    private PlaywrightSingleton() { }

    // Change the method signature to remove the non-constant default value.
    // Remove '= ConfigHelper.GetHeadless()' from the parameter list.
    // Instead, overload the method to provide a default using a parameterless version.

    private static async Task<PlaywrightSingleton> CreateInstance(string browserType, bool isHeadlessMode)
    {
       
        var singleton = new PlaywrightSingleton();
        singleton.Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        singleton.Browser = await BrowserFactory.CreateAsync(browserType, headless: isHeadlessMode);
        singleton.BrowserContext = await singleton.Browser.NewContextAsync();
        singleton.Page = await singleton.Browser.NewPageAsync();
        return singleton;
    }

    // Overload for default headless mode using ConfigHelper
    private static Task<PlaywrightSingleton> CreateInstance()
    {
        _config = ConfigHelper.GetSettings() ?? throw new InvalidOperationException("Configuration settings not found.");
        return CreateInstance(_config.Browser!, _config.IsHeadlessMode);
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
