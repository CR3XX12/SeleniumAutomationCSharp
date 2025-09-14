using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
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

        [SetUp]
        public void BeforeEachTest()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void LoginTest()
        {
            test.Info("Entering username");
            test.Info("Entering password");
            test.Info("Clicking on submit button");
        }       

        [Test]
        public void UserRegTest()
        {      
            test.Info("Entering First Name");
            test.Info("Entering Last Name");
            Assert.Fail("Failing the test case");
        }

        [Test]
        public void ComposeEmailTest()
        {    
            test.Info("Composing email");
            Assert.Ignore("Skipping the test case");           
        }

        [TearDown]
        public void AfterEachTest()
        {
            //Get the test status
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;

            if (testStatus == TestStatus.Passed)
            {
                test.Pass("Test case passed");
            }
            else if (testStatus == TestStatus.Skipped)
            {
                test.Skip("Test case skipped");
            }
            else if (testStatus == TestStatus.Failed)
            {
                test.Fail("Test case failed");
            }
        }

            [OneTimeTearDown]
        public void AfterAllTests()
        {
            extent.Flush();
        }
    }
}
