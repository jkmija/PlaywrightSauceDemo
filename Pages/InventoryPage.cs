using Microsoft.Playwright;
using PlaywrightDemo.Core;

namespace PlaywrightDemo.Pages
{
    internal class InventoryPage : BasePage
    {
        private ILocator TitlePage => page.Locator("//span[@class='title' and contains(text(),'Products')]");
        private ILocator InventoryItems => page.Locator("div.inventory_item");

        private ILocator ShoppingCarButton => page.Locator(".shopping_cart_link");

        private ILocator ShoppingCarBadgeLabel => page.Locator(".shopping_cart_badge");

        public override async Task WaitUntilPageIsLoaded()
        {
            await page.WaitForLoadStateAsync(LoadState.Load);
            // await page.WaitForTimeoutAsync(15000); // Wait for 1 second to ensure the page is fully loaded

            //await InventoryItems.WaitForAsync();
            //page.Locator(".title").Filter(new() { HasTextString = "Products" }).Filter(new() { Visible = true });
            //await TitlePage.IsVisibleAsync();
        }

        public async Task SelectItem(string itemName)
        {
            foreach (var row in await InventoryItems.AllAsync())
            {
                var links = await row.GetByRole(AriaRole.Link).AllAsync();
                foreach (var link in links)
                {
                    if (itemName.Equals(await link.TextContentAsync())) {
                        Console.WriteLine(await link.TextContentAsync());
                        var buttons = await row.GetByRole(AriaRole.Button).AllAsync();
                        foreach (var button in buttons) {
                            await button.ClickAsync();
                        }
                            
                        break;
                    }
                    
                }
            }

            //await InventoryItems
            //    .Filter(new()
            //    {
            //        Has = page.GetByRole(AriaRole.Link, new() { Name = itemName })
            //    })
            //    .GetByRole(AriaRole.Button, new() { Name = "Add to cart", Exact = true })
            //    .ClickAsync();
        }

        public async Task<bool> IsInventoryPageDisplayed()
        {
            return await TitlePage.IsVisibleAsync() && await InventoryItems.CountAsync() > 0;
        }

        public async Task ClickShoppingCartButton()
        {
            await ShoppingCarButton.ClickAsync();
        }
        public async Task<string> GetTitlePageText()
        {
            return await TitlePage.InnerTextAsync();
        }
        public async Task<string> GetShoppingCarBadgeLabelText()
        {
            return await ShoppingCarBadgeLabel.InnerTextAsync();
        }

        public async Task SelectItem2(string itemName)
        {
            //
            //await page.Locator("//button[@id='add-to-cart-sauce-labs-backpack']").WaitForAsync();     
            var count = page.Locator(".inventory_item")
                .GetByRole(AriaRole.Link).Filter(new() { HasTextString = itemName })

                 .TextContentAsync().GetAwaiter().GetResult();

            Console.WriteLine($"Total items: {count}");
            // Wait for the inventory items to be visible
            //await InventoryItems.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 5000 });
            InventoryItems
                .Filter(new()
                {
                    //Has = page.GetByRole(AriaRole.Link, new() { NameRegex = new Regex(itemName, RegexOptions.IgnoreCase) })
                    Has = page.GetByRole(AriaRole.Link, new() { Name = itemName })
                    //Has = page.GetByRole(AriaRole.Link).Filter(new() { HasTextString = itemName })
                    // Has = page.Locator("a div.inventory_item_name").Filter(new() { HasTextString = itemName })
                })
                .GetByRole(AriaRole.Button, new() { Name = "Add to cart" })
                 .ClickAsync().GetAwaiter().GetResult();

            //.Filter(new()
            // {
            //     Has = page.GetByRole(AriaRole.Heading, new()
            //     {
            //         Name = "Product 2"
            //     })
            // })

            // Filter the inventory items by the item name and click the "Add to cart" button
            //await InventoryItems.Filter(new() { HasTextString = itemName }).GetByRole(AriaRole.Button, new() { Name = "Add to cart", Exact=true }).Filter(new() { Visible = true }).ClickAsync();
            Console.WriteLine($"Total itemss: {"asd"}");


            await InventoryItems
                .Filter(new()
                {
                    //Has = page.GetByRole(AriaRole.Link, new() { NameRegex = new Regex(itemName, RegexOptions.IgnoreCase) })
                    // Has = page.GetByRole(AriaRole.Link, new() { Name = itemName })
                    //Has = page.GetByRole(AriaRole.Link).Filter(new() { HasTextString = itemName })
                    Has = page.Locator("a div.inventory_item_name").Filter(new() { HasTextString = itemName })
                })
                .GetByRole(AriaRole.Button, new() { Name = "Add to cart", Exact = true })
                .Filter(new() { Visible = true })
                .ClickAsync();
        }
    }
}
