using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.Testcases
{
    internal class TestNewWindows
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://www.way2automation.com";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();

            driver.SwitchTo().NewWindow(WindowType.Tab);
            driver.Url = "https://gmail.com/";
            Console.WriteLine(driver.Title);

            driver.SwitchTo().NewWindow(WindowType.Window);
            driver.Url = "https://www.google.com/";
            Console.WriteLine(driver.Title);

            Console.WriteLine(driver.WindowHandles.Count()); 
        }
    }
}
