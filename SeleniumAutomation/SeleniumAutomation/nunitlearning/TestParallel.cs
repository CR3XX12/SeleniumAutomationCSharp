using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.nunitlearning
{
    [Parallelizable]
    internal class TestParallel
    {
        //private IWebDriver driver;
        public static ThreadLocal<IWebDriver> driver = new();

        public static IWebDriver GetDriver()
        {
            return driver.Value;
        }

        [SetUp]
        public void Setup()
        {
            driver.Value = new ChromeDriver();
            GetDriver().Url = "https://www.facebook.com/";            
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
        public void LoginTest(string username, string password)
        {
            DateTime currentTime = DateTime.Now;
            string time = currentTime.ToString("yyyy-MM_dd_HH-mm-ss");
            Console.WriteLine($"Logging in with {username} using {password} ---- {time}");
            GetDriver().FindElement(By.Id("email")).SendKeys(username);
            GetDriver().FindElement(By.Id("pass")).SendKeys(password);
            Thread.Sleep(5000);
        }

        public static IEnumerable<TestCaseData> GetTestData()
        {
            var columns = new List<string> { "username", "password" };
            return DataUtil.GetTestDataFromExcel(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\resources\\testdata.xlsx", "LoginTest", columns);
        }

        //[Parallelizable(ParallelScope.Children)]
        //[Test, TestCaseSource("GetUserRegTestData")]
        //public void UserRegTest(string firstname, string lastname)
        //{
        //    DateTime currentTime = DateTime.Now;
        //    string time = currentTime.ToString("yyyy-MM_dd_HH-mm-ss");

        //    Console.WriteLine(firstname + " ----" + lastname+"--"+time);
        //    Thread.Sleep(1000);
        //}

        //public static IEnumerable<TestCaseData> GetUserRegTestData()
        //{
        //    var columns = new List<string> { "firstname", "lastname" };
        //    return DataUtil.GetTestDataFromExcel(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\resources\\testdata.xlsx", "UserRegTest", columns);
        //}
    }
}
