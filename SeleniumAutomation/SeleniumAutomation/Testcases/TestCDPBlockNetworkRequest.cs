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
    internal class TestCDPBlockNetworkRequest
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();

            DevToolsSession session = ((IDevTools)driver).GetDevToolsSession();
            var domains = session.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V139.DevToolsSessionDomains>();

            domains.Network.Enable(new OpenQA.Selenium.DevTools.V139.Network.EnableCommandSettings());

            domains.Network.SetBlockedURLs(new OpenQA.Selenium.DevTools.V139.Network.SetBlockedURLsCommandSettings()
            {
                Urls = new string[] { "*.jpg", "*.png", "*.gif", "*jpeg" }

            });

            domains.Network.SetCacheDisabled(new OpenQA.Selenium.DevTools.V139.Network.SetCacheDisabledCommandSettings()
            {
                CacheDisabled = true
            });
            driver.Url = "https://www.expedia.ca/";
        }
    }
}
