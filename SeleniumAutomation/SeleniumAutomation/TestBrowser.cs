using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation
{
    class TestBrowser
    {
        static String browser = "firefox"; // Notepad
        static IWebDriver driver;
        
        static void Main(string[] args)
        {
            if (browser.Equals("chrome"))
            {
                driver = new ChromeDriver();
                } else if(browser.Equals("firefox"))
            {
                driver = new FirefoxDriver();
            } else if(browser.Equals("edge"))
            {
                driver = new EdgeDriver();
    }
            //driver.Url = "http://gmail.com";
            driver.Navigate().GoToUrl("http://gmail.com");
            Console.WriteLine(driver.Title);
            driver.Manage().Window.Maximize();

            //Implicit wait and Explicit wait (fluent wait)
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            //IWebElement username = driver.FindElement(By.Id("identifierId"));
            //username.SendKeys("java@way2automation.com");

            driver.FindElement(By.Id("identifierId")).SendKeys("java@way2automation.com");
            driver.FindElement(By.XPath("//*[@id=\"identifierNext\"]/div/button/span")).Click();
            //Thread.Sleep(3500); --Avoid using hardcoded
            //driver.FindElement(By.XPath("//*[@id='password']/div[1]/div/div[1]/input")).SendKeys("sdfsfsd");
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='password']/div[1]/div/div[1]/input"))).SendKeys("sdfsfsd");
            driver.FindElement(By.XPath("//*[@id=\"passwordNext\"]/div/button/span")).Click();

            Console.WriteLine(wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"c0\"]/div[2]"))).Text);

            //Console.WriteLine(driver.FindElement(By.XPath("//*[@id=\"c0\"]/div[2]")).Text);
            //driver.Close();
        }

    }
}
