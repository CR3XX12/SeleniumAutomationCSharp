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
    internal class TestSlider
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://jqueryui.com/resources/demos/slider/default.html";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();

            IWebElement slider = driver.FindElement(By.XPath("//*[@id=\"slider\"]/span"));

            Actions action = new Actions(driver);
            action.DragAndDropToOffset(slider, 400, 0).Perform();
        }

    }
}
