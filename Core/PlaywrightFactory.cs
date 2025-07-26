using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightDemo.Core
{
    public class PlaywrightFactory
    {
        private static readonly ThreadLocal<PlaywrightSingleton> drivers = new();

        // Replace this line:
        // private static List<DriverFactory> storedDrivers = new();

        // With the following simplified collection initialization:
        private static List<PlaywrightSingleton> storedDrivers = [];


        private PlaywrightFactory()
        {
        }

        public static PlaywrightSingleton getDriver()
        {
            return drivers.Value!;
        }

        public static void AddDriver(PlaywrightSingleton driver)
        {
            storedDrivers.Add(driver);
            drivers.Value = driver;
        }
    }
}
