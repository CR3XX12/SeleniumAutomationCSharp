using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.nunitlearning
{
    internal class ExtentReportsDemo
    {
        private ExtentReports extent;
        private ExtentTest test;

        [OneTimeSetUp]
        public void BeforeAllTests()
        {
            DateTime currentTime = DateTime.Now;
            string fileName = "Extent_" + currentTime.ToString("yyyy-MM_dd_HH-mm-ss") + ".html";

            var htmlReporter = new ExtentSparkReporter(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\reports\\"+fileName);
            htmlReporter.Config.Theme = Theme.Standard;
            htmlReporter.Config.DocumentTitle = "Automation Test Suite";
            htmlReporter.Config.ReportName = "Automation Tests Results";
            htmlReporter.Config.Encoding = "utf-8";

            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            extent.AddSystemInfo("Automation tester","Chris Rojas");
            extent.AddSystemInfo("Organization", "Way2Automation");
            extent.AddSystemInfo("Build No: ", "W2A-1234");

        }

        public void BeforeEachTest()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void LoginTest()
        {
            // extent.CreateTest("LoginTest");
            test.Pass("Test case passed");
        }       

        [Test]
        public void UserRegTest()
        {
            //test = extent.CreateTest("User Reg Test");
            test.Fail("Failing the test");
            Assert.Fail("Failing the test case");

        }

        [Test]
        public void ComposeEmailTest()
        {
            test =extent.CreateTest("Compose Email Test");
            test.Skip("Skipping the test");
            Assert.Ignore("Skipping the test case");
           

        }

        [OneTimeTearDown]
        public void AfterAllTests()
        {
            extent.Flush();
        }
    }
}
