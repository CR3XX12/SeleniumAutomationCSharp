using OpenQA.Selenium;
using SeleniumAutomation.Tests.Infrastructure;

namespace SeleniumAutomation.Tests.Pages;

public class ProductsPage
{
    private readonly IWebDriver driver;
    private readonly BrowserActions actions;
    private static readonly By Title = By.CssSelector("[data-test='title']");
    private static readonly By Products = By.CssSelector("[data-test='inventory-item']");

    public ProductsPage(IWebDriver driver, BrowserActions actions)
    {
        this.driver = driver;
        this.actions = actions;
    }

    public string Heading => actions.Element("Read visible Products heading", Title,
        element => element.Text, requireText: true);

    public int ProductCount => actions.CountElements("Count available products", Products);

    public string UrlPath => actions.Run("Read current URL path", null, () => new Uri(driver.Url).AbsolutePath);
}
