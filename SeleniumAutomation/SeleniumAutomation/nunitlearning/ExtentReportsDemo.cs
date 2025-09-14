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
            var htmlReporter = new ExtentSparkReporter(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\reports\\extent.html");
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

        [Test]
        public void LoginTest()
        {
            extent.CreateTest("LoginTest");
        }
           

        [OneTimeTearDown]
        public void AfterAllTests()
        {
            extent.Flush();
        }
    }
}
