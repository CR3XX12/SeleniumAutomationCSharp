using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.nunitlearning
{
    internal class TestCase2
    {
        [Test]
        public void ValidateTitles()
        {
            string expectedTitle = "Google"; //excel sheet
            string actualTitle = "Yahoo.com"; //title 

            //if (expectedTitle != actualTitle)
            //{
            //    Console.WriteLine("Test failed");
            //}
            //else
            //{
            //    Console.WriteLine("Test case pass");
            //}

            Console.WriteLine("Beginning");
            Assert.Multiple(() =>
            {
                Assert.That(expectedTitle, Is.EqualTo(actualTitle), "Title is not matching and failing the test");

                Assert.That(false, "Element is not present hence failing the test");

                Assert.Fail("Failing the test as the condition not met");
            });

            
            
            Console.WriteLine("Ending");
        }
    }
}
