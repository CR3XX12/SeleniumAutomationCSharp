using OpenQA.Selenium;
using SeleniumAutomation.Tests.Infrastructure;

namespace SeleniumAutomation.Tests.Pages;

public class LoginPage
{
    private readonly IWebDriver driver;
    private readonly BrowserActions actions;
    private static readonly By Username = By.CssSelector("[data-test='username']");
    private static readonly By Password = By.CssSelector("[data-test='password']");
    private static readonly By LoginButton = By.CssSelector("[data-test='login-button']");
    private static readonly By Error = By.CssSelector("[data-test='error']");

    public LoginPage(IWebDriver driver, BrowserActions actions)
    {
        this.driver = driver;
        this.actions = actions;
    }

    public void Open(Uri baseUrl)
    {
        actions.Run("Navigate to login page", null, () => driver.Navigate().GoToUrl(baseUrl));
        actions.Element("Wait for username field to be visible", Username, element => element.Displayed);
    }

    public void SubmitCredentials(string username, string password)
    {
        actions.Element("Enter username", Username, element => element.SendKeys(username));
        actions.Element("Enter password (value hidden)", Password, element => element.SendKeys(password));
        actions.Element("Click login button", LoginButton, element => element.Click(), requireEnabled: true);
    }

    public string ErrorMessage => actions.Element("Read visible login error text", Error,
        element => element.Text, requireText: true);

    public string UrlPath => actions.Run("Read current URL path", null, () => new Uri(driver.Url).AbsolutePath);
}
