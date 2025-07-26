using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightDemo.Core
{
    public abstract class BasePage
    {
        protected PlaywrightSingleton playwright;
        protected IPage page;

        public BasePage()
        {
            new SharedDriver(); // Ensure the shared driver is initialized
            // Synchronously wait for the async singleton instance in the constructor
            playwright = PlaywrightFactory.getDriver();
            page = playwright.Page;
            
            WaitUntilPageIsLoaded();
        }

        public abstract Task WaitUntilPageIsLoaded();
    }
}
