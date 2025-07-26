using Microsoft.Playwright;
using PlaywrightDemo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightDemo.Pages
{
    public class CheckoutInformationPage : BasePage
    {
        private ILocator TitlePage => page.Locator("//span[@class='title' and contains(text(),'Information')]");
        private ILocator FirstNameTextField => page.Locator("#first-name");
        private ILocator LastNameTextField => page.Locator("#last-name");
        private ILocator PostCodeTextField => page.Locator("#postal-code");
        private ILocator ContinueButton => page.Locator("#continue");

        private ILocator ShoppingCarBadgeLabel => page.Locator(".shopping_cart_badge");
        public override async Task WaitUntilPageIsLoaded()
        {
            await page.WaitForLoadStateAsync(LoadState.Load);
        }
        public async Task<bool> IsCheckoutInformationPageDisplayed()
        {
            // Check if the checkout information page is displayed by looking for a specific element
            return await TitlePage.IsVisibleAsync();
        }
        public async Task EnterFirstName(string firstName)
        {
            await FirstNameTextField.WaitForAsync();
            await FirstNameTextField.FillAsync(firstName);
        }

        public async Task EnterLastName(string lastName)
        {
            await LastNameTextField.WaitForAsync();
            await LastNameTextField.FillAsync(lastName);
        }
        public async Task EnterPostCode(string postCode)
        {
            await PostCodeTextField.WaitForAsync();
            await PostCodeTextField.FillAsync(postCode);
        }
        public async Task ClickContinueButton()
        {
            await ContinueButton.WaitForAsync();
            await ContinueButton.ClickAsync();
        }
        public async Task FillCheckoutInformationForm(string firstName, string lastName, string postCode)
        {
            await EnterFirstName(firstName);
            await EnterLastName(lastName);
            await EnterPostCode(postCode);
            await ClickContinueButton();
        }
        public async Task<string> GetShoppingCartBadgeLabel()
        {
            // Get the text of the shopping cart badge label
            return await ShoppingCarBadgeLabel.InnerTextAsync();

        }

     }
}
