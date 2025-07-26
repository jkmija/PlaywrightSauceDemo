using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightDemo.Core
{
    public class SharedDriver
    {
        public SharedDriver()
        {

            if (PlaywrightFactory.getDriver() == null)
            {
                var playwrightSingleton = PlaywrightSingleton.Instance.GetAwaiter().GetResult();
                // Assuming DriverFactory has a constructor that accepts IPlaywright or IBrowser
                // You may need to adjust this based on your actual DriverFactory implementation
  
                PlaywrightFactory.AddDriver(playwrightSingleton);
            }
            //else {
            //    throw new InvalidOperationException("PlaywrightSingleton instance is not initialized.");
            //}

        }
    }
}
