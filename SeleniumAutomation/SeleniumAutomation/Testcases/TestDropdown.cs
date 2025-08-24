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

            var values = dropdown.FindElements(By.TagName("option"));

            Console.WriteLine("Total values are: " + values.Count);

            foreach (var value in values)
            {
                Console.WriteLine(value.Text+ " Value is: " + value.GetAttribute("value"));

            }

            Console.WriteLine("----printing footer links-------");

           // IWebElement section2 = driver.FindElement(By.XPath("//section2"));
           // driver.FindElements(By.Id("abc"))[1].Click(); //Narrowing to particular element

            IWebElement section = driver.FindElement(By.XPath("//*[@id=\"www-wikipedia-org\"]/footer/nav"));
            

            var links = section.FindElements(By.TagName("a"));
            Console.WriteLine("Total links are: " + links.Count);

            foreach (var link in links)
            {
                Console.WriteLine(link.Text + "URL is: " + link.GetAttribute("href"));

            }
        }
    }
}
