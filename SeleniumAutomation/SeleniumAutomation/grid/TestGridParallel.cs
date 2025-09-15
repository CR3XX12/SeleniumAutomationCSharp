using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using SeleniumAutomation.nunitlearning;
using SeleniumAutomation.Testcases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.IO.RecyclableMemoryStreamManager;

namespace SeleniumAutomation.grid
{
    [Parallelizable]
    internal class TestGridParallel
    {

        //  private IWebDriver driver;
        public static ThreadLocal<IWebDriver> driver = new();


        public static IWebDriver GetDriver()
        {


            return driver.Value;
        }



        private static dynamic GetBrowserOptions(string browserName)
        {

            switch (browserName)
            {

                case "chrome":
                    return new ChromeOptions();
                case "firefox":
                    return new FirefoxOptions();


            }

            return new ChromeOptions();
        }



        // [SetUp]
        public void SetUp(string browserName)
        {
            //java - jar selenium - server - 4.23.0.jar node--detect - drivers true--publish - events tcp://192.168.1.16:4442 --subscribe-events tcp://192.168.1.24:4443
            dynamic options = GetBrowserOptions(browserName);
            options.PlatformName = "windows";



            driver.Value = new RemoteWebDriver(new Uri("http://localhost:4444"), options.ToCapabilities());
            GetDriver().Url = "http://facebook.com";
        }

        [TearDown]
        public void TearDown()
        {
            if(driver!= null)
            {
                GetDriver().Quit();
                GetDriver().Dispose();
                driver.Value = null;
            }

        }



        [Parallelizable(ParallelScope.Children)]
        [Test, TestCaseSource("GetTestData")]
        public void LoginTest(string username, string password,string browserName)
        {

            SetUp(browserName);

            DateTime currentTime = DateTime.Now;
            string time = currentTime.ToString("yyyy-MM-dd_HH-mm-ss");

            Console.WriteLine(username + " " + password+"--"+ time);
            GetDriver().FindElement(By.Id("email")).SendKeys(username);
            GetDriver().FindElement(By.Id("pass")).SendKeys(password);
            Thread.Sleep(5000);

        }


        public static IEnumerable<TestCaseData> GetTestData()
        {

            var columns = new List<string> { "username", "password", "browser" };

            return DataUtil.GetTestDataFromExcel(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\resources\\testdata.xlsx", "LoginTest", columns);

        }

    }
}
