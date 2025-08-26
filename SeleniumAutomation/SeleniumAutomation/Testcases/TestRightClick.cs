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
    internal class TestRightClick
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://deluxe-menu.com/popup-mode-sample.html";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();

            IWebElement img = driver.FindElement(By.XPath("//p[2]/img"));

            new Actions(driver).ContextClick(img).Perform();          

            IWebElement click2 = driver.FindElement(By.XPath("//*[@id=\"dm2m1i1it\"]"));

            new Actions(driver).MoveToElement(click2).Perform();

            IWebElement click3 = driver.FindElement(By.XPath("//*[@id=\"dm2m2i1tdT\"]"));

            new Actions(driver).MoveToElement(click3).Perform();

            IWebElement click4 = driver.FindElement(By.XPath("//*[@id=\"dm2m3i0tdT\"]"));

            new Actions(driver).MoveToElement(click4).Click().Perform();



        }
    }
}
