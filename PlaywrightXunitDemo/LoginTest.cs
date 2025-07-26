using Microsoft.Playwright;
using PlaywrightDemo.Core;
using PlaywrightDemo.Pages;
using System.Threading.Tasks;
using Xunit;

namespace PlaywrightDemo.PlaywrightXunitDemo
{
    public class LoginTest
    {
        private readonly LoginPage _loginPage;
        private readonly PlaywrightSingleton? playwright;
        public LoginTest()
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
        public async Task LoginWithInvalidCredentials()
        {
            //  await _loginPage.NavigateToLoginPageAsync("https://www.saucedemo.com/");
            // Arrange
            await _loginPage.LoginWithValidCreadencials("problem_userddd", "secret_sauced");
            String expectedMessage = "Epic sadface: Username and password do not match any user in this service";

            // Assert
            string actualMessage = await _loginPage.GetErrorMessageAsync();
            Assert.Equal(expectedMessage, actualMessage);
            // Act
            bool isLoginSuccessful = await _loginPage.IsLoginPageDisplayed();
            // Assert
            Assert.True(isLoginSuccessful, "Login page is not displayed.");
        }

      
        public async Task LoginWithValidCredentials()
        {
            //  await _loginPage.NavigateToLoginPageAsync("https://www.saucedemo.com/");
            // Arrange
            await _loginPage.LoginWithValidCreadencials("problem_user", "secret_sauce");

            // Act
            bool isLoginSuccessful = await _loginPage.IsLoginPageDisplayed();
            // Assert
            Assert.False(isLoginSuccessful, "Login page is displayed.");
        }
 
        public void Dispose()
        {
           // playwright?.DisposeAsync().GetAwaiter().GetResult();

        }
    }
}
