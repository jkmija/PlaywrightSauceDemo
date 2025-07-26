using Microsoft.Playwright;

namespace PlaywrightDemo.Core
{
    public interface IDriverFactory
    {
        IPage Page { get; }
        IBrowserContext BrowserContext { get; }
        Task<IPlaywright> InitAsync(bool headless = true);
        Task DisposeAsync();
    }
}
