using Microsoft.Playwright;
using PlaywrightDemo.Core;
using System.Xml.Linq;

namespace PlaywrightDemo.Pages
{
    public class LoginPage : BasePage
    {

        private ILocator Username => page.Locator("#user-name");
       // private ILocator Username2 => page.Locator("#user-namessss");
        private ILocator Password => page.Locator("#password");
        private ILocator LoginButton => page.Locator("#login-button");
        public override async Task WaitUntilPageIsLoaded()
        {
            await page.WaitForLoadStateAsync(LoadState.Load);
        }

        public async Task EnterUsernameTextField(string username)
        {
            await Username.WaitForAsync();
            await Username.FillAsync(username);
        }

        public async Task EnterPassawordTextField(string password)
        {
            await Password.ClearAsync();
            await Password.FillAsync(password);
        }

        public async Task ClickLoginButton()
        {
            await LoginButton.ClickAsync();
        }

        public async Task LoginWithValidCreadencials(string username, string password)
        {
            await EnterUsernameTextField(username);
            await EnterPassawordTextField(password);
            await ClickLoginButton();
        }
        public async Task<bool> IsLoginPageDisplayed()
        {
      //      var title = await page.TitleAsync();
            return await Username.IsVisibleAsync() && await LoginButton.IsVisibleAsync() && await Password.IsVisibleAsync();
        }
        public async Task NavigateToLoginPageAsync(string url)
        {
            await page.GotoAsync(url);
        }
        public async Task<string> GetErrorMessageAsync()
        {
            // Assuming there's an error message element with a specific selector
            var errorMessageLocator = page.Locator(".error-message-container");
            return await errorMessageLocator.InnerTextAsync();
        }
        public async Task<bool> IsLoginPageLoadedAsync()
        {
            // Check if the login page is loaded by looking for a specific element
            return await Username.IsVisibleAsync() && await Password.IsVisibleAsync() && await LoginButton.IsVisibleAsync();
        }
        public async Task LogoutAsync()
        {
            // Assuming there's a logout button with a specific selector
            var logoutButton = page.Locator("#logout");
            if (await logoutButton.IsVisibleAsync())
            {
                await logoutButton.ClickAsync();
            }
        }
        public async Task<bool> IsLogoutSuccessfulAsync()
        {
            // Assuming that a successful logout redirects to a login page with a specific title
            await page.WaitForLoadStateAsync(LoadState.Load);
            var title = await page.TitleAsync();
            return title.Contains("Login");
        }
        public async Task NavigateToForgotPasswordPageAsync(string url)
        {
            // Assuming there's a link to the forgot password page
            var forgotPasswordLink = page.Locator("#forgot-password");
            if (await forgotPasswordLink.IsVisibleAsync())
            {
                await forgotPasswordLink.ClickAsync();
                await page.WaitForLoadStateAsync(LoadState.Load);
            }
        }
        public async Task<bool> IsForgotPasswordPageLoadedAsync()
        {
            // Check if the forgot password page is loaded by looking for a specific element
            var resetPasswordButton = page.Locator("#reset-password");
            return await resetPasswordButton.IsVisibleAsync();
        }
        public async Task ResetPasswordAsync(string email)
        {
            // Assuming there's an input field for the email and a reset button
            var emailInput = page.Locator("#email");
            var resetButton = page.Locator("#reset-button");
            await emailInput.FillAsync(email);
            await resetButton.ClickAsync();
        }
        public async Task<string> GetResetPasswordMessageAsync()
        {
            // Assuming there's a message element that appears after resetting the password
            var messageLocator = page.Locator(".reset-message");
            return await messageLocator.InnerTextAsync();
        }
        public async Task NavigateToRegistrationPageAsync(string url)
        {
            // Assuming there's a link to the registration page
            var registerLink = page.Locator("#register");
            if (await registerLink.IsVisibleAsync())
            {
                await registerLink.ClickAsync();
                await page.WaitForLoadStateAsync(LoadState.Load);
            }
        }
    }
}
