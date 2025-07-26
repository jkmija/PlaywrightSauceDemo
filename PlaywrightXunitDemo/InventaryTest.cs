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
    public class InventaryTest
    {
        private readonly LoginPage _loginPage;
        private InventoryPage? inventoryPage;
        private readonly PlaywrightSingleton? playwright;
        public InventaryTest()
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
        public async Task AddItemsToShoppingCar()
        {
            await _loginPage.LoginWithValidCreadencials("standard_user", "secret_sauce");
            inventoryPage = new InventoryPage();
            // Arrange

            // Act


            await inventoryPage.SelectItem("Sauce Labs Backpack");
            await inventoryPage.SelectItem("Sauce Labs Bike Light");
            await inventoryPage.SelectItem("Sauce Labs Bolt T-Shirt");
            await inventoryPage.SelectItem("Sauce Labs Fleece Jacket");
            await inventoryPage.SelectItem("Sauce Labs Onesie");
            await inventoryPage.SelectItem("Test.allTheThings() T-Shirt (Red)");
            //   inventoryPage.ClickShoppingCartButton();
            String expectedBudgeLabel = "6";
            string actualBudgeLabel = await inventoryPage.GetShoppingCarBadgeLabelText();
            Assert.Equal(expectedBudgeLabel, actualBudgeLabel);
            // Assert
            bool isInventoryPageDisplayed = await inventoryPage.IsInventoryPageDisplayed();
            Assert.True(isInventoryPageDisplayed, "Inventory page is not displayed.");
        }
    }
}
