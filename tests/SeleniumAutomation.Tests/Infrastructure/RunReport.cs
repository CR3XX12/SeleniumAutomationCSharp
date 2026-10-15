using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;

namespace SeleniumAutomation.Tests.Infrastructure;

internal sealed class RunReport
{
    private readonly ExtentReports extent = new();
    public string Path { get; } = TestRun.Artifact("reports", "index.html");

    public RunReport()
    {
        var reporter = new ExtentSparkReporter(Path);
        reporter.Config.DocumentTitle = "Selenium portfolio test results";
        reporter.Config.ReportName = "Sauce Demo login smoke suite";
        extent.AttachReporter(reporter);
        extent.AddSystemInfo("Framework", "NUnit / Selenium C#");
    }

    public ExtentTest StartTest(string name) => extent.CreateTest(name);

    public void Flush()
    {
        extent.Flush();
        TestContext.Progress.WriteLine($"HTML report: {Path}");
    }
}
