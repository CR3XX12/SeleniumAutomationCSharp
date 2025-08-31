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
    internal class TestCDPDeviceMetricsOverride
    {
        static void Main(string[] args)
        {

            IWebDriver driver = new ChromeDriver();

            DevToolsSession session = ((IDevTools)driver).GetDevToolsSession();
            var domains = session.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V139.DevToolsSessionDomains>();
            
            domains.Emulation.SetDeviceMetricsOverride(new OpenQA.Selenium.DevTools.V139.Emulation.SetDeviceMetricsOverrideCommandSettings()
                {
                Width = 375,
                Height = 667,
                DeviceScaleFactor = 50,
                Mobile = true,
            });

            driver.Url = "https://selenium.dev";
        }
    }
}
