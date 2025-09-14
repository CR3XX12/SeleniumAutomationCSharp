using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.nunitlearning
{
    internal class TestParameterization
    {

        [Test]
        [TestCase("trainer@way2automation.com", "sfdsfdsfd")]
        [TestCase("info@way2automation.com", "xfdsfdawdsfd")]
        [TestCase("java@way2automation.com", "pfdsfdsfdwad")]
        public void LoginTest(string username, string password)
        {
            Console.WriteLine($"Logging in with {username} using {password}");
        }

        [Test, TestCaseSource("GetTestData")]
        public void UserRegTest(string firstname, string lastname)
        {
            Console.WriteLine(firstname+" ----"+lastname);
        }

        public static IEnumerable<TestCaseData> GetTestData()
        {
            //Yield will wait until all returrns are successful completed
            //The purpose of using yield is to provide a simple way to implement an iterator method
            //and temporarily suspend the execution until the next value is requested.
            //This allows the method to resume execution from where it left off when the next value is requested,

            yield return new TestCaseData("John", "Cena");
            yield return new TestCaseData("Chris", "Rojas");
            yield return new TestCaseData("John", "Wick");

        }
    }
}
