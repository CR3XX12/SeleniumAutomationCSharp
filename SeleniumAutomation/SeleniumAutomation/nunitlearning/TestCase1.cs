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
        [Test]
        public void DoLogin()
        {
            Console.WriteLine("Executing Login Test");
        }

        [Test]
        public void DoUserReg()
        {
            Console.WriteLine("Executing User reg Test");
        }
    }
}
