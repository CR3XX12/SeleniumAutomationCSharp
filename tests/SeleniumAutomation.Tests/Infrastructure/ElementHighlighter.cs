using OpenQA.Selenium;

namespace SeleniumAutomation.Tests.Infrastructure;

internal sealed class ElementHighlighter(IWebDriver driver, bool enabled, Action<string> logInfo)
{
    private const string ClearScript = """
        const state = window.__seleniumDebugHighlights || [];
        for (const [element, outline, priority] of state) {
            if (outline) element.style.setProperty('outline', outline, priority);
            else element.style.removeProperty('outline');
        }
        window.__seleniumDebugHighlights = [];
        """;

    public void Clear() => Execute(ClearScript);

    public void Show(IEnumerable<IWebElement> elements)
    {
        Execute(ClearScript + """
            for (const element of arguments[0]) {
                window.__seleniumDebugHighlights.push([
                    element, element.style.getPropertyValue('outline'),
                    element.style.getPropertyPriority('outline')]);
                element.style.setProperty('outline', '3px solid magenta', 'important');
            }
            """, elements.ToArray());
    }

    private void Execute(string script, IWebElement[]? elements = null)
    {
        if (!enabled) return;
        try
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(script, new object[] { elements ?? [] });
        }
        catch (WebDriverException exception)
        {
            // Visual debugging must not change whether a test passes or fails.
            logInfo($"Highlight unavailable: {exception.Message}");
        }
    }
}
