using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.nunitlearning
{
    internal class TestCase1
    {
        [SetUp]
        public void LaunchBrowser()
        {
            Console.WriteLine("Launching Browser");
        }

        [TearDown]
        public void CloseBrowser()
        {
            Console.WriteLine("Closing Browser");
        }

        [Test, Order(1), Category("smoke")]
        public void DoUserReg()
        {
            Console.WriteLine("Executing User reg Test");
        }

        [Test, Order(2), Category("smoke")]
        public void DoLogin()
        {
            Console.WriteLine("Executing Login Test");
        }

        [Test, Order(3), Category("bvt"),Ignore("Ignoring the test case")]
        public void Test3()
        {
            Console.WriteLine("Executing Test 3");
        }

        [Test, Order(4), Category("functional")]
        public void Test4()
        {
            Console.WriteLine("Executing Test 4");
        }

        [Test, Order(5), Category("functional")]
        public void Test5()
        {
            Console.WriteLine("Executing Test 5");
        }

    }
}
