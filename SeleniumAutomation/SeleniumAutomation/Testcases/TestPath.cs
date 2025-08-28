using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.Testcases
{
    internal class TestPath
    {
        static void Main(string[] args)
        {

            Console.WriteLine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent);

            DateTime currentTime = DateTime.Now;
            Console.WriteLine(currentTime.ToString("yyyy-MM-dd_HH-mm-ss")+".jpg");
        }
    }
}
