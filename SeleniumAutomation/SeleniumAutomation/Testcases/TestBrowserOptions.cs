using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.Testcases
{
    internal class TestBrowserOptions
    {
        static void Main(string[] args)
        {
            ChromeOptions options = new ChromeOptions();
            //options.AddArgument("--headless");
            options.AddArgument("--ignore-certificate-errors");
            options.AddExcludedArgument("enable-automation");
            options.AddUserProfilePreference("profile.default_content_setting_values.notifications",2); // 1 allow 2 block
            IWebDriver driver = new ChromeDriver(options);
            driver.Url = "https://expired.badssl.com/";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();

            Console.WriteLine(driver.Title);

            //driver.Quit();
        }
    }
}
