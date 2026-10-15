using SeleniumAutomation.Examples.Support;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.Examples.Integration
{
    internal class TestLogger
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TestLogger));

        static void Main(string[] args)
        {
            var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetExecutingAssembly());
            GlobalContext.Properties["LogFile"] = ExamplePaths.Artifact("logs", "logfile.log");
            XmlConfigurator.Configure(logRepository, new FileInfo(ExamplePaths.Resource("Configuration", "log4net.config")));

            log.Info("Text execution started");
            log.Debug("This is a debug message");
            log.Error("This is an Error");
            log.Warn("This is a warning");
            log.Fatal("This is a fatal message");
        }
    }
}
