using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumAutomation.Tests.Infrastructure;

namespace SeleniumAutomation.Tests.Tests.Infrastructure;

[TestFixture]
[Category("Framework")]
public class ElementHighlightTests : BrowserTest
{
    [TestCase(true)]
    [TestCase(false)]
    public void HighlightingTracksTargetsAndRestoresOriginalOutlines(bool enabled)
    {
        const string html = """
            <!doctype html><html><body>
            <button data-test="duplicate" style="outline: 2px dashed blue">First</button>
            <button data-test="duplicate">Second</button>
            <button id="next">Next</button>
            </body></html>
            """;
        Driver.Navigate().GoToUrl("data:text/html;charset=utf-8," + Uri.EscapeDataString(html));
        var messages = new List<string>();
        var actions = new BrowserActions(Driver, enabled, message =>
        {
            messages.Add(message);
            LogStep(message);
        }, LogStep);
        var duplicate = By.CssSelector("[data-test='duplicate']");
        var buttons = Driver.FindElements(duplicate);
        string originalFirstOutline = buttons[0].GetCssValue("outline");
        string originalSecondOutline = buttons[1].GetCssValue("outline");

        string text = actions.Element("Read the first duplicate button", duplicate, element => element.Text);

        Assert.That(text, Is.EqualTo("First"));
        Assert.That(messages.Any(message => message.Contains("Matches: 2")), Is.True);
        Assert.That(buttons[0].GetCssValue("outline"), enabled
            ? Does.Contain("rgb(255, 0, 255)") : Is.EqualTo(originalFirstOutline));
        Assert.That(buttons[1].GetCssValue("outline"), Is.EqualTo(originalSecondOutline));

        actions.Element("Read a different button", By.Id("next"), element => element.Text);

        Assert.That(buttons[0].GetCssValue("outline"), Is.EqualTo(originalFirstOutline));
        Assert.That(actions.CountElements("Count both duplicate buttons", duplicate), Is.EqualTo(2));
        if (enabled)
        {
            Assert.That(buttons.All(button => button.GetCssValue("outline").Contains("rgb(255, 0, 255)")), Is.True);
        }

        actions.Run("Read URL without an element target", null, () => Driver.Url);

        Assert.That(buttons[0].GetCssValue("outline"), Is.EqualTo(originalFirstOutline));
        Assert.That(buttons[1].GetCssValue("outline"), Is.EqualTo(originalSecondOutline));
    }
}
