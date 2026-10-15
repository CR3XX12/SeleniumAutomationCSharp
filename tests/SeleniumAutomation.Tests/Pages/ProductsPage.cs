using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumAutomation.Tests.Pages;

public class ProductsPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public ProductsPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
    }

    public string Heading => wait.Until(browser =>
    {
        var title = browser.FindElement(By.CssSelector("[data-test='title']"));
        return title.Displayed ? title.Text : null;
    });

    public int ProductCount => wait.Until(browser =>
    {
        var products = browser.FindElements(By.CssSelector("[data-test='inventory-item']"));
        return products.Count > 0 ? products : null;
    }).Count;

    public string UrlPath => new Uri(driver.Url).AbsolutePath;
}
