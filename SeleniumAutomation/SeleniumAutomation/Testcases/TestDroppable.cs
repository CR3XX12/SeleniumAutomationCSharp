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
    internal class TestDroppable
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://jqueryui.com/resources/demos/droppable/default.html";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();

            IWebElement draggable = driver.FindElement(By.Id("draggable"));
            IWebElement droppable = driver.FindElement(By.Id("droppable"));

            new Actions(driver).DragAndDrop(draggable, droppable).Perform();
            //new Actions(driver).DragAndDropToOffset(draggable, 400, 400).Perform(); // Dropping somewhere in the page

        }
    }
}
