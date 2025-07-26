using Microsoft.Playwright;
using PlaywrightDemo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightDemo.Pages
{
    public class CartPage : BasePage
    {
        private ILocator PageTitle => page.Locator("//span[@class='title' and contains(text(),'Cart')]");
        private ILocator CheckoutButton => page.Locator("#checkout");

        private ILocator CartList => page.Locator(".cart_list");

        public override async Task WaitUntilPageIsLoaded()
        {
            await page.WaitForLoadStateAsync(LoadState.Load);
            await PageTitle.WaitForAsync();
        }

        public async Task<bool> IsCartPageDisplayed()
        {
            // Check if the cart page is displayed by looking for a specific element    
            return await PageTitle.IsVisibleAsync();
        }
        public async Task<string> GetCartPageTitle()
        {
            // Get the title of the cart page
            return await PageTitle.InnerTextAsync();
        }
        public async Task ClickCheckoutButton()
        {
            // Click the checkout button
            await CheckoutButton.ClickAsync();
        }

        public async Task<List<List<string>>> GetCartItems()
        {
            // Get the list of items in the cart
            var items = await CartList.Locator(".cart_item").AllAsync();
            List<List<string>> cartItems = new List<List<string>>();
            List<string> itemNames = new List<string>();
            foreach (var item in await CartList.AllAsync())
            {
                var itemQuantity = await item.Locator(".cart_quantity").InnerTextAsync();
                var itemName = await item.Locator(".inventory_item_name").InnerTextAsync();
                var itemPrice = await item.Locator(".inventory_item_price").InnerTextAsync();
                itemNames.Add(itemName);
                itemNames.Add(itemName);
                itemNames.Add(itemPrice);
                cartItems.Add(itemNames);
            }
            return cartItems;
        }
    }
}
