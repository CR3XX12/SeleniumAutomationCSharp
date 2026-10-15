using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumAutomation.Examples.Support;

namespace SeleniumAutomation.Examples.NUnit;

[TestFixture]
[Category("BrowserDemo")]
[NonParallelizable]
public class BrowserDebugTests
{
    [TestCase("en", "English")]
    [TestCase("es", "Spanish")]
    public void ChoosingLanguageDisplaysConfirmation(string languageCode, string languageName)
    {
        // Chrome is visible. Dispose closes the browser even if an assertion fails.
        var binaries = SeleniumManager.BinaryPaths(
            "--browser chrome --browser-version stable --force-browser-download --avoid-stats");
        var options = new ChromeOptions { BinaryLocation = binaries["browser_path"] };
        options.AddArgument("--window-size=1100,800");
        using var service = ChromeDriverService.CreateDefaultService(
            Path.GetDirectoryName(binaries["driver_path"])!, Path.GetFileName(binaries["driver_path"]));
        service.LogPath = ExamplePaths.Artifact("logs", $"chromedriver-{Guid.NewGuid():N}.log");
        service.EnableVerboseLogging = true;
        using var driver = new ChromeDriver(service, options);

        string pagePath = ExamplePaths.Resource("Samples", "Pages", "language-picker.html");
        driver.Navigate().GoToUrl(new Uri(pagePath).AbsoluteUri);
        CaptureStep(driver, "01-page-opened");

        var languageDropdown = new SelectElement(driver.FindElement(By.Id("language")));
        languageDropdown.SelectByValue(languageCode);
        CaptureStep(driver, "02-language-selected");

        driver.FindElement(By.Id("confirm")).Click();
        string confirmation = driver.FindElement(By.Id("confirmation")).Text;
        CaptureStep(driver, "03-confirmation-shown");

        Assert.That(confirmation, Is.EqualTo($"Selected language: {languageName}"));
    }

    private static void CaptureStep(ChromeDriver driver, string step)
    {
        TestContext.Progress.WriteLine(step);
        string fileName = $"{TestContext.CurrentContext.Test.ID}-{Guid.NewGuid():N}-{step}.png";
        string path = ExamplePaths.Artifact("screenshots", fileName);
        driver.GetScreenshot().SaveAsFile(path);
        TestContext.AddTestAttachment(path, step);
    }
}
