using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.Testcases
{
    internal class TestCDPBadSSL
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();           

            DevToolsSession session = ((IDevTools)driver).GetDevToolsSession();
            var  domains = session.GetVersionSpecificDomains <OpenQA.Selenium.DevTools.V139.DevToolsSessionDomains>();
            domains.Security.Enable();

            domains.Security.SetIgnoreCertificateErrors(new OpenQA.Selenium.DevTools.V139.Security.SetIgnoreCertificateErrorsCommandSettings()
                {
                Ignore = true
            });
            driver.Url = "https://expired.badssl.com/";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();
        }
    }
}
