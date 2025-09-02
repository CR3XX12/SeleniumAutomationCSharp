using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.utilities
{
    internal class ReadingAppsettings
    {
        static void Main(string[] args)
        {


            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\resources\\")
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string browser = configuration["Appsettings:browser"];
            string testsiteurl = configuration["Appsettings:testsiteurl"];
            string env = configuration["Appsettings:env"];


            Console.WriteLine(browser);
            Console.WriteLine(testsiteurl);
            Console.WriteLine(env);
        }
    }
}

        
