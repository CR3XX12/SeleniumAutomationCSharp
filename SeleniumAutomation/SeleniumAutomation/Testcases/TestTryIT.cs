using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.Testcases
{
    internal class TestTryIT
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://www.w3schools.com/jsref/tryit.asp?filename=tryjsref_form_submit";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();

            driver.SwitchTo().Frame("iframeResult");
            //driver.FindElement(By.XPath("//*[@id=\"myForm\"]/input[3]")).Click();            

            IWebElement elem = driver.FindElement(By.XPath("//*[@id=\"myForm\"]/input[3]"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].style.border='3px solid red'", elem);

            Thread.Sleep(3000); // Visual purpose

            ((IJavaScriptExecutor)driver).ExecuteScript("myFunction()");

            driver.SwitchTo().DefaultContent();

            var frames = driver.FindElements(By.TagName("iframe"));
            Console.WriteLine("Total frames are: " + frames.Count);

            foreach (var frame in frames)
            {
                Console.WriteLine(frame.GetAttribute("id"));
            }

           



            
        }
    }
}
