using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using SeleniumAutomation.Tests.Configuration;
using SeleniumAutomation.Tests.TestData.Content;

namespace SeleniumAutomation.Tests.Infrastructure;

[NonParallelizable]
public abstract class BrowserTest
{
    private IWebDriver? driver;
    protected IWebDriver Driver => driver ?? throw new InvalidOperationException("Browser has not started.");
    protected TestSettings Settings { get; private set; } = null!;
    protected WebsiteContent Content { get; private set; } = null!;

    [SetUp]
    public void StartBrowser()
    {
        Settings = TestSettings.FromEnvironment();
        Content = WebsiteContentLoader.Load();
        driver = BrowserFactory.Create(Settings);
        Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
    }

    [TearDown]
    public void StopBrowser()
    {
        if (driver is null)
        {
            return;
        }

        try
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                try
                {
                    string path = TestRun.Artifact("screenshots", $"failure-{Guid.NewGuid():N}.png");
                    ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(path);
                    TestContext.AddTestAttachment(path, "Browser at failure");
                }
                catch (Exception exception) when (exception is WebDriverException or IOException or UnauthorizedAccessException)
                {
                    TestContext.Progress.WriteLine($"Could not capture failure screenshot: {exception.Message}");
                }
            }
        }
        finally
        {
            try
            {
                driver.Dispose();
            }
            finally
            {
                driver = null;
            }
        }
    }
}
