using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.Testcases
{
    internal class TestTryIT
    {
        static IWebDriver driver;

        static void CaptureScreenshot()
        {
            DateTime currentTime = DateTime.Now;
            string filename = currentTime.ToString("yyyy-MM-dd_HH-mm-ss") + ".jpg";

            Screenshot screenshot = driver.TakeScreenshot();
            screenshot.SaveAsFile(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\screenshot\\"+filename) ;
            
        }

        static void CaptureElementScreenshot(IWebElement element)
        {
            DateTime currentTime = DateTime.Now;
            string filename = "Element_"+currentTime.ToString("yyyy-MM-dd_HH-mm-ss") + ".jpg";

            Screenshot screenshot = ((ITakesScreenshot)element).GetScreenshot();
            screenshot.SaveAsFile(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent + "\\screenshot\\" + filename);

        }
        static void Main(string[] args)
        {
            driver = new ChromeDriver();
            driver.Url = "https://www.w3schools.com/jsref/tryit.asp?filename=tryjsref_form_submit";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();

            driver.SwitchTo().Frame("iframeResult");
            //driver.FindElement(By.XPath("//*[@id=\"myForm\"]/input[3]")).Click();            

            IWebElement elem = driver.FindElement(By.XPath("//*[@id=\"myForm\"]/input[3]"));
            CaptureElementScreenshot(elem);
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

            

            CaptureScreenshot();
                                   
        }
    }
}
