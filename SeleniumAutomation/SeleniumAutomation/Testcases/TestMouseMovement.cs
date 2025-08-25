using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.Testcases
{
    internal class TestMouseMovement
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://www.way2automation.com/";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();

            IWebElement menu = driver.FindElement(By.XPath("//*[@id=\"menu-item-27580\"]/a/span[2]"));
            Actions action = new Actions(driver);
            action.MoveToElement(menu).Perform();

            driver.FindElement(By.LinkText("Lifetime Membership")).Click();
        }
    }
}
