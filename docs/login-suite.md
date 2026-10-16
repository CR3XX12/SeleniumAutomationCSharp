# Assignment: Sauce Demo login smoke suite

## Purpose

Verify three important login outcomes against the public [Sauce Demo practice application](https://www.saucedemo.com/). The application is maintained by Sauce Labs; its [source repository](https://github.com/saucelabs/sample-app-web) and practice accounts are public. This suite demonstrates browser automation and test design against an external application.

## Coverage

| Scenario | Test | Expected result | Reason |
| --- | --- | --- | --- |
| Standard account with valid password | `ValidUserCanViewProducts` | Products heading, inventory URL, and at least one product | Login is the entry point to the shopping journey |
| Standard account with incorrect password | `IncorrectPasswordIsRejected` | Exact credentials error and user stays on the login page | Incorrect credentials should not open the inventory |
| Locked account with valid password | `LockedUserIsRejected` | Exact locked-account error and user stays on the login page | An account restriction must prevent login |

Each case starts with a fresh browser profile and closes its browser in teardown. Page objects own selectors, explicit waits, and interactions. Tests own the expected outcomes and assertions. There are no fixed sleeps or automatic retries.

When adding a scenario, construct its page objects, call their interactions, and assert the expected outcome. Test methods do not need manual step logs, screenshot calls, or report flushes. Page operations provide locator logs, match counts, timings, and optional highlights automatically; `BrowserTest` records the outcome and final screenshot. For example, `SubmitCredentials` uses a single click operation that already waits for the login button to be visible and enabled, so a second explicit wait step is unnecessary.

## Run and debug

From the repository root:

```powershell
dotnet test tests/SeleniumAutomation.Tests/SeleniumAutomation.Tests.csproj
```

Select one case from the CLI:

```powershell
dotnet test tests/SeleniumAutomation.Tests/SeleniumAutomation.Tests.csproj --filter "Name=ValidUserCanViewProducts"
```

Select the smoke category:

```powershell
dotnet test tests/SeleniumAutomation.Tests/SeleniumAutomation.Tests.csproj --filter "TestCategory=Smoke"
```

In VS Code, use the Testing panel under `SeleniumAutomation.Tests` → `LoginTests`. Add a breakpoint on `login.SubmitCredentials(...)`, choose **Debug Test**, and use F10 to step over statements. Chrome closes when the test finishes; use F5 to continue from a breakpoint.

## Configuration

| Environment variable | Default | Purpose |
| --- | --- | --- |
| `TEST_BASE_URL` | `https://www.saucedemo.com/` | URL of Sauce Demo or a compatible deployed copy |
| `TEST_HEADLESS` | `false` | Use `true` to run without visible browser windows |
| `TEST_HIGHLIGHT_ELEMENTS` | Whether a debugger is attached | Use `true` for element outlines, or `false` to disable them even when debugging |

Set variables in the terminal where you run `dotnet test`. A VS Code debug session inherits its environment from VS Code, so a variable set in an integrated terminal does not automatically change the Testing panel's environment.

## Visual element debugging

When you choose **Debug Test**, highlighting defaults to on. Use F10 to step over a page operation, then inspect Chrome: the exact element used by that operation has a magenta outline. Each new operation restores the previous outline before selecting its next target. No sleeps or automatic debugger pauses are added. A click that navigates can replace the outlined element, so step into the page method and set a breakpoint on `return operation(element);` in `BrowserActions.Element` if you need to pause before the click itself.

Element actions report the number of locator matches and select the first match in DOM order, waiting for that first element to be visible (and enabled or nonempty where required). If two similar items match, only the chosen item is outlined. Collection operations such as counting products outline all matched items and label the target as a collection. Match counts are reported even when highlighting is disabled. Counts describe the lookup at that moment; a dynamic page can change afterward.

A URL assertion has no page element to highlight. Browser-level operations clear the previous outline; inspect the URL value in VS Code or the address bar instead. Highlighting changes only inline outline styles and restores prior values, including their CSS priority. It does not change selectors or add a delay. Screenshots captured while it is enabled may include outlines. If the browser cannot apply the highlight, an informational entry is logged and the original test action proceeds.

To use it without a debugger in PowerShell:

```powershell
$env:TEST_HIGHLIGHT_ELEMENTS = "true"
dotnet test tests/SeleniumAutomation.Tests/SeleniumAutomation.Tests.csproj --filter "TestCategory=Smoke"
Remove-Item Env:TEST_HIGHLIGHT_ELEMENTS
```

`ElementHighlightTests` (category `Framework`) uses a local page to check duplicate matches, collection highlighting, restoration of original outlines, and disabled mode. These checks are separate from the three Sauce Demo smoke scenarios.

Chrome for Testing and its matching driver are resolved through Selenium Manager, separately from the installed Chrome. Downloads are cached. Internet access is required for the website and for the initial browser download. The stable browser channel may change over time.

## Reusable expected content

Expected headings and error messages live in `tests/SeleniumAutomation.Tests/TestData/Content/en-US.json`. `WebsiteContentLoader` reads the file into typed `WebsiteContent`, `LoginContent`, and `ProductsContent` objects. Assertions can use `Content.Products.Heading` without spelling JSON keys in test code. The loader reports missing files, malformed JSON, missing required fields, and null or empty required text.

The JSON is copied beside the compiled tests and resolved relative to the assembly, so the working directory does not affect loading. Usernames and passwords remain in `DemoUsers.cs`; URL paths and selectors remain in the tests and page objects because they are not localized website text.

The rejected-login cases now use NUnit's `TestCaseSource`: attributes cannot contain values read from a JSON file at runtime, so the source method loads the messages and creates the two named cases. Test names and expected outcomes remain the same.

Only `en-US` is implemented. The loader accepts a culture name for future content files, but these tests currently select `en-US`. Adding another file would not switch the application's language; application locale selection and supported language coverage require a separate change. Review wording changes against the intended requirements before updating expected content.

## HTML reports, screenshots, and retention

The output prints the run directory:

```text
tests/SeleniumAutomation.Tests/bin/Debug/net8.0/artifacts/runs/run_<UTC timestamp>_<id>/
  logs/          ChromeDriver diagnostic logs
  screenshots/   Final browser state for each passed or failed case
  reports/
    index.html   HTML dashboard for the selected tests
```

Run one case or the whole `LoginTests` fixture; both generate a report containing only the selected cases. After execution, find `HTML report:` in the test output, or open the latest run folder's `reports/index.html` in a browser. In VS Code, right-click the HTML file, choose **Reveal in File Explorer**, and double-click it. Select a case in the report to inspect its logged steps, result, and screenshot thumbnail.

The dashboard records passed, failed, and skipped outcomes. Failed cases include the NUnit error and stack trace. The final browser screenshot is captured before browser cleanup for passing and failing cases, embedded in the HTML, saved as a PNG, and attached to NUnit results. This is an end-of-test screenshot, not a video or a screenshot of every action. An invalid-password scenario passes when it correctly verifies the expected error; its screenshot shows the login error as evidence of the expected result.

Page-object operations also log their action, Selenium locator, source filename and line, and duration. Each operation records when it starts and completes. If an operation throws, the report marks that specific action failed and includes the exception type and message; the original exception is rethrown so NUnit retains its stack trace. A failing assertion after a successful read appears in the test-level failure entry, with the preceding read showing which locator supplied the value. Navigation and URL reads are labeled as browser-level operations without an element locator.

For example, `Click login button | Locator: By.CssSelector: [data-test='login-button'] | Source: LoginPage.cs:<line> (SubmitCredentials)` identifies the selector and code to inspect. These entries come from the operations wrapped by `BrowserActions`; they are not automatic interception of every WebDriver command. Action labels omit entered credentials. The separate verbose ChromeDriver diagnostic logs can contain command arguments and should be treated as local debugging artifacts.

The report is updated after each case and flushed again at the end of the selected run. It is not automatically opened. Let the test finish normally; stopping the debugger abruptly can interrupt capture or saving. Browser startup failures may occur before a screenshot is possible, and discovery failures before test setup cannot be recorded by this reporting hook. Screenshot-capture errors are logged without replacing the original test outcome. ExtentReports may load dashboard styling or scripts from its CDN when you open the report; screenshots themselves are embedded.

This project keeps the latest five executions independently of the examples project. Older execution folders are deleted locally. Generated output is Git-ignored. Run one execution at a time when using this local retention policy.

## Limits and next steps

These three scenarios are a login smoke suite, not complete authentication or security coverage. They do not cover cart and checkout, blank fields, logout, session persistence, accessibility, API behavior, or multiple browsers. An external outage or application change can fail the suite.

Next: expand cart and checkout user journeys, then introduce CI in separate commits.
