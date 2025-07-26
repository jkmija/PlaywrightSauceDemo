using PlaywrightDemo.Core;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightDemo.Pages
{
    public class OverviewPage : BasePage
    {
        private ILocator PageTitle => page.Locator("//span[@class='title' and contains(text(),'Overview')]");
        private ILocator FinishButton => page.Locator("#finish");
        private ILocator CancelButton => page.Locator("#cancel");
        private ILocator TotalLabel => page.Locator(".summary_total_label");


        public override async Task WaitUntilPageIsLoaded()
        {
            await page.WaitForLoadStateAsync(LoadState.Load);
            await PageTitle.WaitForAsync();
        }
        public async Task<bool> IsOverviewPageDisplayed()
        {
            // Check if the overview page is displayed by looking for a specific element
            return await PageTitle.IsVisibleAsync();
        }
        public async Task ClickFinishButton()
        {
            // Click the finish button
            await FinishButton.ClickAsync();
        }
        public async Task ClickCancelButton()
        {
            // Click the cancel button
            await CancelButton.ClickAsync();
        }
        public async Task<string> GetTotalLabelText()
        {
            // Get the text of the total label
            return await TotalLabel.InnerTextAsync();
        }
        public async Task<List<string>> GetOverviewItems()
        {
            // Get the list of items in the overview
            var items = await page.Locator(".cart_item").AllAsync();
            List<string> overviewItems = new List<string>();
            foreach (var item in items)
            {
                var quantity = await item.Locator(".cart_quantity").InnerTextAsync();
                var itemName = await item.Locator(".inventory_item_name").InnerTextAsync();
                var itemPrice = await item.Locator(".inventory_item_price").InnerTextAsync();
                overviewItems.Add($"{quantity} - {itemName} - {itemPrice}");
            }
            return overviewItems;
        }
        public async Task<string> GetPageTitle()
        {
            // Get the title of the overview page
            return await PageTitle.InnerTextAsync();
        }
    }
}
