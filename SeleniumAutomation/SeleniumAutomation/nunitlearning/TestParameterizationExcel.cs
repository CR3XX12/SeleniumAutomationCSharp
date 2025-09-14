using NUnit.Framework;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.nunitlearning
{
    internal class TestParameterizationExcel
    {
        [Test,TestCaseSource("GetTestData")]
        public void LoginTest(string username, string password)
        {
            Console.WriteLine($"Logging in with {username} using {password}");
        }

        public static IEnumerable<TestCaseData> GetTestData()
        {            
            var columns = new List<string> { "username", "password" };
            return DataUtil.GetTestDataFromExcel(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\resources\\testdata.xlsx", "LoginTest", columns);
        }

        [Test, TestCaseSource("GetUserRegTestData")]
        public void UserRegTest(string firstname, string lastname)
        {
            Console.WriteLine(firstname + " ----" + lastname);
        }

        public static IEnumerable<TestCaseData> GetUserRegTestData()
        {           
            var columns = new List<string> { "firstname", "lastname" };
            return DataUtil.GetTestDataFromExcel(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\resources\\testdata.xlsx", "UserRegTest", columns);
        }
    }
}
