using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.Testcases
{
    internal class TestFluentWait
    {

        static void Main(string[] args)
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Url = "http://gmail.com";
            
            driver.Manage().Window.Maximize();
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            IWait<IWebDriver> wait = new DefaultWait<IWebDriver>(driver)
            {
                Timeout = TimeSpan.FromSeconds(30),
                PollingInterval = TimeSpan.FromSeconds(5)
            };

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));


            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.FindElement(By.Id("identifierId")).SendKeys("java@way2automation.com");
            driver.FindElement(By.XPath("//*[@id=\"identifierNext\"]/div/button/span")).Click();
            
            
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='password']/div[1]/div/div[1]/input"))).SendKeys("sdfsfsd");
        }
    }
}
