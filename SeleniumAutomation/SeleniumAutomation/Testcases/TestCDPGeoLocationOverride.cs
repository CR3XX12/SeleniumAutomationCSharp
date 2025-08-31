using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.Testcases
{
    internal class TestCDPGeoLocationOverride
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();

            DevToolsSession session = ((IDevTools)driver).GetDevToolsSession();
            var domains = session.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V139.DevToolsSessionDomains>();

            domains.Emulation.SetGeolocationOverride(new OpenQA.Selenium.DevTools.V139.Emulation.SetGeolocationOverrideCommandSettings()
            {
                Latitude = 37.532600, 
                Longitude = 127.024612,
                Accuracy = 100
            });
            driver.Url = "https://www.gps-coordinates.net/my-location";
        }
    }
}
