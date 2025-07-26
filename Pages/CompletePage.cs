using Microsoft.Playwright;
using PlaywrightDemo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightDemo.Pages
{
    public class CompletePage : BasePage
    {
        private ILocator PageTitle => page.Locator("//span[@class='title' and contains(text(),'Complete!')]");
        private ILocator OrderConfirmationMessage => page.Locator(".complete-header");
        public override async Task WaitUntilPageIsLoaded()
        {
            await page.WaitForLoadStateAsync(LoadState.Load);
            await PageTitle.WaitForAsync();
        }
        public async Task<bool> IsCompletePageDisplayed()
        {
            // Check if the complete page is displayed by looking for a specific element
            return await PageTitle.IsVisibleAsync();
        }
        public async Task<string> GetPageTitle()
        {
            // Get the title of the complete page
            return await PageTitle.InnerTextAsync();
        }
        public async Task<string> GetOrderConfirmationMessage()
        {
            // Get the order confirmation message
            return await OrderConfirmationMessage.InnerTextAsync();
        }

    }
}
