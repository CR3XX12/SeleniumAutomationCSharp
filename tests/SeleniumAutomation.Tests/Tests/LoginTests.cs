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
        var login = new LoginPage(Driver, Actions);
        login.Open(Settings.BaseUrl);

        login.SubmitCredentials(DemoUsers.Standard, DemoUsers.Password);

        var products = new ProductsPage(Driver, Actions);
        Assert.That(products.Heading, Is.EqualTo(Content.Products.Heading));
        Assert.That(products.UrlPath, Is.EqualTo("/inventory.html"));
        Assert.That(products.ProductCount, Is.GreaterThan(0));
    }

    [TestCaseSource(nameof(RejectedLoginCases))]
    public void RejectedLoginShowsExpectedError(string username, string password, string expectedError)
    {
        var login = new LoginPage(Driver, Actions);
        login.Open(Settings.BaseUrl);

        login.SubmitCredentials(username, password);

        Assert.That(login.ErrorMessage, Is.EqualTo(expectedError));
        Assert.That(login.UrlPath, Is.EqualTo(Settings.BaseUrl.AbsolutePath));
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
