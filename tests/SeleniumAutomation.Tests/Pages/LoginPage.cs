using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumAutomation.Tests.Pages;

public class LoginPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;
    private static readonly By Username = By.CssSelector("[data-test='username']");
    private static readonly By Password = By.CssSelector("[data-test='password']");
    private static readonly By LoginButton = By.CssSelector("[data-test='login-button']");
    private static readonly By Error = By.CssSelector("[data-test='error']");

    public LoginPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
    }

    public void Open(Uri baseUrl)
    {
        driver.Navigate().GoToUrl(baseUrl);
        wait.Until(browser => browser.FindElement(Username).Displayed);
    }

    public void SubmitCredentials(string username, string password)
    {
        driver.FindElement(Username).SendKeys(username);
        driver.FindElement(Password).SendKeys(password);
        wait.Until(browser => browser.FindElement(LoginButton).Enabled);
        driver.FindElement(LoginButton).Click();
    }

    public string ErrorMessage => wait.Until(browser =>
    {
        var error = browser.FindElement(Error);
        return error.Displayed && !string.IsNullOrWhiteSpace(error.Text) ? error.Text : null;
    });
}
