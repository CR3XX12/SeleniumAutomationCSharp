using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.Testcases
{
    internal class TestLogger
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TestLogger));

        static void Main(string[] args)
        {
            var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetExecutingAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("resources\\log4net.config"));

            log.Info("Text execution started");
            log.Debug("This is a debug message");
            log.Error("This is an Error");
            log.Warn("This is a warning");
            log.Fatal("This is a fatal message");
        }
    }
}
