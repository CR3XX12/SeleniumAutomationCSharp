using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using SeleniumAutomation.Tests.Configuration;
using SeleniumAutomation.Tests.TestData.Content;
using System.Net;

namespace SeleniumAutomation.Tests.Infrastructure;

[NonParallelizable]
public abstract class BrowserTest
{
    private IWebDriver? driver;
    private ExtentTest? reportTest;
    protected IWebDriver Driver => driver ?? throw new InvalidOperationException("Browser has not started.");
    protected TestSettings Settings { get; private set; } = null!;
    protected WebsiteContent Content { get; private set; } = null!;

    [SetUp]
    public void StartBrowser()
    {
        reportTest = TestRun.Report.StartTest(TestContext.CurrentContext.Test.Name);
        LogStep("Start a fresh Chrome for Testing session");
        Settings = TestSettings.FromEnvironment();
        Content = WebsiteContentLoader.Load();
        driver = BrowserFactory.Create(Settings);
        Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
    }

    protected void LogStep(string description) => reportTest?.Info(WebUtility.HtmlEncode(description));

    [TearDown]
    public void StopBrowser()
    {
        try
        {
            if (reportTest is not null)
            {
                var result = TestContext.CurrentContext.Result;
                switch (result.Outcome.Status)
                {
                    case TestStatus.Passed:
                        reportTest.Pass("All assertions passed");
                        break;
                    case TestStatus.Failed:
                        reportTest.Fail($"<pre>{WebUtility.HtmlEncode(result.Message + Environment.NewLine + result.StackTrace)}</pre>");
                        break;
                    case TestStatus.Skipped:
                        reportTest.Skip(WebUtility.HtmlEncode(result.Message));
                        break;
                    default:
                        reportTest.Warning(WebUtility.HtmlEncode($"{result.Outcome}: {result.Message}"));
                        break;
                }

                if (driver is not null)
                {
                    CaptureFinalScreenshot();
                }
                else
                {
                    LogStep("Screenshot unavailable: the browser did not start");
                }
            }
        }
        finally
        {
            try
            {
                driver?.Dispose();
            }
            catch (Exception exception)
            {
                reportTest?.Fail(WebUtility.HtmlEncode($"Browser cleanup failed: {exception.Message}"));
                throw;
            }
            finally
            {
                driver = null;
                reportTest = null;
                TestRun.Report.Flush();
            }
        }
    }

    private void CaptureFinalScreenshot()
    {
        try
        {
            string status = TestContext.CurrentContext.Result.Outcome.Status.ToString().ToLowerInvariant();
            string path = TestRun.Artifact("screenshots", $"{status}-{Guid.NewGuid():N}.png");
            var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
            screenshot.SaveAsFile(path);
            TestContext.AddTestAttachment(path, $"Final browser state ({status})");
            reportTest!.Info($"Final browser state ({status})",
                MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot.AsBase64EncodedString).Build());
        }
        catch (Exception exception) when (exception is WebDriverException or IOException or UnauthorizedAccessException)
        {
            string message = $"Screenshot unavailable: {exception.Message}";
            TestContext.Progress.WriteLine(message);
            LogStep(message);
        }
    }
}
