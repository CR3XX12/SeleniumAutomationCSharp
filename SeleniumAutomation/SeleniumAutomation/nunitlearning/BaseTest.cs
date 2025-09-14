using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.nunitlearning
{
    [SetUpFixture]
    internal class BaseTest
    {
        [OneTimeSetUp]
        public void CreateDbConn()
        {
            TestContext.Progress.WriteLine($"Creating DB Connection"); ;
        }

        [OneTimeTearDown]
        public void CloseDbConn()
        {
            TestContext.Progress.WriteLine("Closing DB Connection");
        }
    }
}
