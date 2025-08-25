using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.Testcases
{
    internal class TestHandlingTabsAndPopups
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://www.gob.mx/";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();

            IList<String> winids = driver.WindowHandles;

            Console.WriteLine("Total windows: " + winids.Count());

            driver.FindElement(By.LinkText("Acta de nacimiento")).Click();

            winids = driver.WindowHandles;
            Console.WriteLine("Total windows count: " + winids.Count());

            driver.SwitchTo().Window(winids[1]);

            driver.FindElement(By.XPath("//*[@id=\"navbarColor03\"]/div/a[1]")).Click();
            driver.FindElement(By.LinkText("Iniciar sesión")).Click();
            driver.FindElement(By.Name("frmLogin:txtCorreo")).SendKeys("User123@gmail.com");

            ///driver.Quit(); //close all tabs

            driver.Close(); // current focused window - 2nd window            

            driver.SwitchTo().Window(winids[0]); // 

            Thread.Sleep(3000);

            winids = driver.WindowHandles;
            Console.WriteLine("Total windows count: " + winids.Count());

            driver.Close(); // current focused window - 1nd window


        }
    }
}
