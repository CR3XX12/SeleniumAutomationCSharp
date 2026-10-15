using NUnit.Framework;
using SeleniumAutomation.Tests.Infrastructure;
using SeleniumAutomation.Tests.Pages;
using SeleniumAutomation.Tests.TestData;
using SeleniumAutomation.Tests.TestData.Content;

namespace SeleniumAutomation.Tests.Tests;

[TestFixture]
[Category("Smoke")]
public class LoginTests : BrowserTest
{
    [Test]
    public void ValidUserCanViewProducts()
    {
        var login = new LoginPage(Driver);
        LogStep("Open the login page");
        login.Open(Settings.BaseUrl);

        LogStep("Submit valid credentials for the standard practice user");
        login.SubmitCredentials(DemoUsers.Standard, DemoUsers.Password);

        LogStep("Verify the Products heading, inventory URL, and available products");
        var products = new ProductsPage(Driver);
        Assert.That(products.Heading, Is.EqualTo(Content.Products.Heading));
        Assert.That(products.UrlPath, Is.EqualTo("/inventory.html"));
        Assert.That(products.ProductCount, Is.GreaterThan(0));
    }

    [TestCaseSource(nameof(RejectedLoginCases))]
    public void RejectedLoginShowsExpectedError(string username, string password, string expectedError)
    {
        var login = new LoginPage(Driver);
        LogStep("Open the login page");
        login.Open(Settings.BaseUrl);

        LogStep("Submit credentials for the rejected-login scenario");
        login.SubmitCredentials(username, password);

        LogStep("Verify the expected error and that the user stays on the login page");
        Assert.That(login.ErrorMessage, Is.EqualTo(expectedError));
        Assert.That(new Uri(Driver.Url).AbsolutePath, Is.EqualTo(Settings.BaseUrl.AbsolutePath));
    }

    public static IEnumerable<TestCaseData> RejectedLoginCases()
    {
        var content = WebsiteContentLoader.Load();
        yield return new TestCaseData(DemoUsers.Standard, "incorrect-password", content.Login.InvalidCredentials)
            .SetName("IncorrectPasswordIsRejected");
        yield return new TestCaseData(DemoUsers.Locked, DemoUsers.Password, content.Login.LockedUser)
            .SetName("LockedUserIsRejected");
    }
}
