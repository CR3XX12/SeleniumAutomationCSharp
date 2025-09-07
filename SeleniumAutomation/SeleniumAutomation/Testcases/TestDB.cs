using SeleniumAutomation.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.Testcases
{
    internal class TestDB
    {
        static void Main(string[] args)
        {
            DBManager.SetMySQLDBConnection();
            Console.WriteLine(DBManager.GetQuery("select tutorial_author from selenium where tutorial_id = 1")[0]);

        }
    }
}
