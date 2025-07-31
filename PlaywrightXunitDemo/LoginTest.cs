using Microsoft.Playwright;
using PlaywrightDemo.Core;
using PlaywrightDemo.Pages;
using PlaywrightDemo.Settings;
using System.Threading.Tasks;
using Xunit;

namespace PlaywrightDemo.PlaywrightXunitDemo
{
    public class LoginTest
    {
        private readonly LoginPage _loginPage;
        private readonly PlaywrightSingleton? playwright;
        private readonly static AppSetting? Config = ConfigHelper.GetSettings();

        public LoginTest()
        {
            // Initialize Playwright and the browser context
            new SharedDriver();
            //PlaywrightSingleton.Instance.GetAwaiter().GetResult();
            playwright = PlaywrightFactory.getDriver();
            var page = playwright.Page;
            page!.GotoAsync(Config!.BaseUrl!);
            _loginPage = new LoginPage();
        }

        [Fact]
        public async Task LoginWithInvalidCredentials()
        {
            //  await _loginPage.NavigateToLoginPageAsync("https://www.saucedemo.com/");
            // Arrange
            await _loginPage.LoginWithValidCreadencials("InvalidUser ss",Config?.Password!);
            String expectedMessage = "Epic sadface: Username and password do not match any user in this service";

            // Assert
            string actualMessage = await _loginPage.GetErrorMessageAsync();
            Assert.Equal(expectedMessage, actualMessage);
            // Act
            bool isLoginSuccessful = await _loginPage.IsLoginPageDisplayed();
            // Assert
            Assert.True(isLoginSuccessful, "Login page is not displayed.");
        }


        [Fact]
        public async Task LoginWithValidCredentials()
        {
            //  await _loginPage.NavigateToLoginPageAsync("https://www.saucedemo.com/");
            // Arrange
            await _loginPage.LoginWithValidCreadencials(Config?.Username!, Config?.Password!);

            // Act
            bool isLoginSuccessful = await _loginPage.IsLoginPageDisplayed();
            // Assert
            Assert.False(isLoginSuccessful, "Login page is displayed.");
        }
 
        public void Dispose()
        {
            // playwright?.DisposeAsync().GetAwaiter().GetResult();
            var page = playwright.Page;
           // page!.GotoAsync("https://www.saucedemo.com/#");

        }
    }
}
