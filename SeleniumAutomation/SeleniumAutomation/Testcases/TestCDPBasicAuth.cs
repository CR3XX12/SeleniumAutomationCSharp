using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V139.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.Testcases
{
    internal class TestCDPBasicAuth
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();

            DevToolsSession session = ((IDevTools)driver).GetDevToolsSession();
            var domains = session.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V139.DevToolsSessionDomains>();

            domains.Network.Enable(new OpenQA.Selenium.DevTools.V139.Network.EnableCommandSettings());

            var encodedAuth = Convert.ToBase64String(Encoding.Default.GetBytes("admin:admin"));

            var headerSettings = new SetExtraHTTPHeadersCommandSettings
            {
                Headers = new Headers()
                {
                    {"authorization", "Basic " + encodedAuth}
                }
            };

            domains.Network.SetExtraHTTPHeaders(headerSettings);

            driver.Url = "https://admin:admin@the-internet.herokuapp.com/basic_auth"; 

        }
    }
}
