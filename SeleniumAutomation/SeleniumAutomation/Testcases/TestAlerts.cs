using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.Testcases
{
    internal class TestAlerts
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://mail.rediff.com/cgi-bin/login.cgi";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();

            driver.FindElement(By.XPath("/html/body/div[2]/div[1]/div/div[2]/div[2]/form/button")).Click();

            IAlert alert= driver.SwitchTo().Alert();
            Console.WriteLine(alert.Text);
            Thread.Sleep(2000);
            alert.Accept();
        
        }
    }
}
