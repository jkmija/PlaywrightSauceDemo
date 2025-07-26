using PlaywrightDemo.Core;
using PlaywrightDemo.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PlaywrightDemo.PlaywrightXunitDemo
{
    public class CompletePurchase
    {
        private readonly LoginPage _loginPage;
        private InventoryPage? inventoryPage;
        private readonly PlaywrightSingleton? playwright;

        public CompletePurchase()
        {
            // Initialize Playwright and the browser context
            new SharedDriver();
            //PlaywrightSingleton.Instance.GetAwaiter().GetResult();
            playwright = PlaywrightFactory.getDriver();
            var page = playwright.Page;
            page!.GotoAsync("https://www.saucedemo.com/");
            _loginPage = new LoginPage();
        }
        [Fact]
        public async Task CompletePurchaseTestAsync()
        {
            await _loginPage.LoginWithValidCreadencials("standard_user", "secret_sauce");
            // Select items to purchase
            inventoryPage = new InventoryPage();
            await inventoryPage.SelectItem("Sauce Labs Backpack");
            await inventoryPage.SelectItem("Sauce Labs Bike Light");
            await inventoryPage.SelectItem("Sauce Labs Bolt T-Shirt");
            await inventoryPage.ClickShoppingCartButton();
            // Navigate to the checkout page
            var cartPage = new CartPage();
            await cartPage.ClickCheckoutButton();
            // Fill in the checkout information and click the continue button to proceed to the overview page
            var checkoutInformation = new CheckoutInformationPage();
            await checkoutInformation.FillCheckoutInformationForm("John", "Doe", "12345");
        
            // Wait for the overview page to load
            var overviewPage = new OverviewPage();
            await overviewPage.WaitUntilPageIsLoaded();
            // Verify that the overview page is displayed   
            if (!await overviewPage.IsOverviewPageDisplayed())
            {
                throw new Exception("Overview page is not displayed.");
            }
            // Get and print the title of the overview page
            string overviewTitle = await overviewPage.GetPageTitle();
            Console.WriteLine($"Overview Page Title: {overviewTitle}");
            // Get and print the total price of the items in the overview page
            string totalPrice = await overviewPage.GetTotalLabelText();
            Console.WriteLine($"Total Price: {totalPrice}");
            // Get and print the list of items in the overview page
            var overviewItmes = await overviewPage.GetOverviewItems();
            Console.WriteLine($"Items in Overview Page: {overviewItmes.Count}");
            foreach (var item in overviewItmes)
            {
                Console.WriteLine($"Item add car: {item}");
            }
            // Assert that the overview page contains the expected items
            List<string> expectedItems =
            [
                "1 - Sauce Labs Backpack - $29.99",
                "1 - Sauce Labs Bike Light - $9.99",
                "1 - Sauce Labs Bolt T-Shirt - $15.99"
            ];
            Assert.Equal(expectedItems, overviewItmes);
            // Click the finish button to complete the purchase
            await overviewPage.ClickFinishButton();           


            // Wait for the complete page to load
            var completePage = new CompletePage();
            await completePage.WaitUntilPageIsLoaded();
            // Verify that the complete page is displayed
            if (!await completePage.IsCompletePageDisplayed())
            {
                throw new Exception("Complete page is not displayed.");
            }
            // Get and print the title of the complete page
            string title = await completePage.GetPageTitle();
            Console.WriteLine($"Complete Page Title: {title}");
            // Get and print the order confirmation message
            string actualConfirmationMessage = await completePage.GetOrderConfirmationMessage();
            Console.WriteLine($"Order Confirmation Message: {actualConfirmationMessage}");
            String expectedConfirmationMessage = "Thank you for your order!";
            // Assert that the order confirmation message is as expected
            Assert.Equal(expectedConfirmationMessage, actualConfirmationMessage);


        }
    }
}
