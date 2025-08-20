using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.Testcases
{
    internal class TestDropdown
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://www.wikipedia.org/";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();

            //driver.FindElement(By.XPath("//*[@id="searchLanguage"]");

            IWebElement dropdown = driver.FindElement(By.XPath("//*[@id=\"searchLanguage\"]"));
            SelectElement select = new SelectElement(dropdown);
            //select.SelectByText("Eesti"); 
            select.SelectByValue("hi");

            var values = driver.FindElements(By.TagName("option"));

            Console.WriteLine("Total values are: " + values.Count);

            foreach (var value in values)
            {
                Console.WriteLine(value.Text+ " Value is: " + value.GetAttribute("value"));

            }
        }
    }
}
