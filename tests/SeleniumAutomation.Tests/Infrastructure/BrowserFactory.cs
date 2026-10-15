using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumAutomation.Tests.Configuration;

namespace SeleniumAutomation.Tests.Infrastructure;

internal static class BrowserFactory
{
    private static readonly Lazy<Dictionary<string, string>> Binaries = new(() =>
        SeleniumManager.BinaryPaths("--browser chrome --browser-version stable --force-browser-download --avoid-stats"));

    public static IWebDriver Create(TestSettings settings)
    {
        var binaries = Binaries.Value;
        var options = new ChromeOptions { BinaryLocation = binaries["browser_path"] };
        options.AddArgument("--window-size=1280,900");
        options.AddUserProfilePreference("credentials_enable_service", false);
        options.AddUserProfilePreference("profile.password_manager_enabled", false);
        if (settings.Headless)
        {
            options.AddArgument("--headless=new");
        }

        var service = ChromeDriverService.CreateDefaultService(
            Path.GetDirectoryName(binaries["driver_path"])!, Path.GetFileName(binaries["driver_path"]));
        string logPath = TestRun.Artifact("logs", $"chromedriver-{Guid.NewGuid():N}.log");
        service.LogPath = logPath;
        service.EnableVerboseLogging = true;
        try
        {
            return new ChromeDriver(service, options);
        }
        catch
        {
            service.Dispose();
            throw;
        }
    }
}
