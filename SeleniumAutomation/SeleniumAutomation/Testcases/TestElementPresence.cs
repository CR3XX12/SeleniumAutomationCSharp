using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.Testcases
{
    internal class TestElementPresence
    {
        static IWebDriver driver = new ChromeDriver();
        static bool isElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            } catch(Exception ex) { return false; }
        }
        static void Main(string[] args)
        {
            driver = new ChromeDriver();
            driver.Url = "https://gmail.com";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();

            Console.WriteLine(isElementPresent(By.XPath("//*[@id=\"identifierId\"]"))); 
        }
    }
}
