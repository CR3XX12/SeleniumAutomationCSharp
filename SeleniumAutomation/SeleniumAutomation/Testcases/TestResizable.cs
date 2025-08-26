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
    internal class TestResizable
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://jqueryui.com/resources/demos/resizable/default.html";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();

            IWebElement resizable =  driver.FindElement(By.XPath("//*[@id=\"resizable\"]/div[3]"));

            new Actions(driver).DragAndDropToOffset(resizable, 800, 400).Perform();
        }
    }
}
