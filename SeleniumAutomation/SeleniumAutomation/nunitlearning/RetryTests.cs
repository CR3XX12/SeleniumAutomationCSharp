using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.nunitlearning
{
    [TestFixture]
    internal class RetryTests
    {
        private int _counter = 0;

        [Test, Retry(3)]
        public void TestMethod_withretries()
        {
            _counter++;

            if(_counter < 3)
            {
                TestContext.Progress.WriteLine($"Test attemp {_counter}");
                Assert.Fail("Simulated failure");
            }
            
            // Pass on the 3rd attempt
            Assert.Pass("Test passed on attempt " + _counter);


        }

    }
}
