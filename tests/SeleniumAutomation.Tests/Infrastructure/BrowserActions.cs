using System.Diagnostics;
using System.Runtime.CompilerServices;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumAutomation.Tests.Infrastructure;

public sealed class BrowserActions
{
    private readonly Action<string> logInfo;
    private readonly Action<string> logFailure;
    private readonly IWebDriver driver;
    private readonly ElementHighlighter highlighter;

    public BrowserActions(IWebDriver driver, bool highlightElements, Action<string> logInfo, Action<string> logFailure)
    {
        this.logInfo = logInfo;
        this.logFailure = logFailure;
        this.driver = driver;
        highlighter = new ElementHighlighter(driver, highlightElements, logInfo);
    }

    public T Run<T>(string action, By? locator, Func<T> operation,
        [CallerFilePath] string sourceFile = "",
        [CallerMemberName] string member = "",
        [CallerLineNumber] int line = 0)
    {
        string context = $"{action} | Locator: {locator?.ToString() ?? "none (browser-level operation)"}"
            + $" | Source: {Path.GetFileName(sourceFile)}:{line} ({member})";
        logInfo($"Starting: {context}");
        var timer = Stopwatch.StartNew();
        try
        {
            highlighter.Clear();
            T result = operation();
            logInfo($"Completed: {context} | Duration: {timer.ElapsedMilliseconds} ms");
            return result;
        }
        catch (Exception exception)
        {
            logFailure($"Failed: {context} | Duration: {timer.ElapsedMilliseconds} ms"
                + $" | {exception.GetType().Name}: {exception.Message}");
            throw;
        }
    }

    public T Element<T>(string action, By locator, Func<IWebElement, T> operation,
        bool requireEnabled = false, bool requireText = false,
        [CallerFilePath] string sourceFile = "",
        [CallerMemberName] string member = "",
        [CallerLineNumber] int line = 0)
    {
        return Run(action, locator, () =>
        {
            int matchCount = 0;
            IWebElement element;
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
                element = wait.Until(browser =>
                {
                    var matches = browser.FindElements(locator);
                    matchCount = matches.Count;
                    if (matchCount == 0) return null;
                    var first = matches[0];
                    return first.Displayed && (!requireEnabled || first.Enabled)
                        && (!requireText || !string.IsNullOrWhiteSpace(first.Text)) ? first : null;
                });
            }
            finally
            {
                logInfo($"Locator: {locator} | Matches: {matchCount} | Target: first match in DOM order");
            }

            highlighter.Show([element]);
            return operation(element);
        }, sourceFile, member, line);
    }

    public void Element(string action, By locator, Action<IWebElement> operation,
        bool requireEnabled = false,
        [CallerFilePath] string sourceFile = "",
        [CallerMemberName] string member = "",
        [CallerLineNumber] int line = 0)
    {
        Element(action, locator, element => { operation(element); return true; },
            requireEnabled, false, sourceFile, member, line);
    }

    public int CountElements(string action, By locator,
        [CallerFilePath] string sourceFile = "",
        [CallerMemberName] string member = "",
        [CallerLineNumber] int line = 0)
    {
        return Run(action, locator, () =>
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            var elements = wait.Until(browser =>
            {
                var matches = browser.FindElements(locator);
                return matches.Count > 0 ? matches : null;
            });
            logInfo($"Locator: {locator} | Matches: {elements.Count} | Target: all matched elements (collection)");
            highlighter.Show(elements);
            return elements.Count;
        }, sourceFile, member, line);
    }

    public void Run(string action, By? locator, Action operation,
        [CallerFilePath] string sourceFile = "",
        [CallerMemberName] string member = "",
        [CallerLineNumber] int line = 0)
    {
        Run(action, locator, () =>
        {
            operation();
            return true;
        }, sourceFile, member, line);
    }
}
