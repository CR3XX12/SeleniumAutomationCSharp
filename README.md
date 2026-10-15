# Selenium Automation with C#

A C# and Selenium QA portfolio with a three-scenario login smoke suite for Sauce Demo. The original learning exercises are preserved in a separate examples project.

## Technology

- C# targeting .NET 8
- Selenium WebDriver and Selenium Support
- NUnit and the NUnit test adapter
- ExtentReports, log4net, EPPlus, and MySQL integration examples

## Repository structure

```text
SeleniumAutomation.sln
tests/
  SeleniumAutomation.Tests/
    Configuration/           Environment-based application URL and headless setting
    Infrastructure/          Browser lifecycle, failure capture, and run artifacts
    Pages/                   Login and Products page objects
    TestData/                Public practice accounts and typed JSON website content
    Tests/                   Login smoke scenarios and assertions
examples/
  SeleniumAutomation.Examples/
    Browser/                 Browser options, navigation, windows, and tabs
    DataDriven/              Keyword-driven example
    DevTools/                Chrome DevTools Protocol examples
    Grid/                    Remote browser execution example
    Integration/             Database, email, and logging examples
    Interactions/            Alerts, dropdowns, mouse actions, frames, and screenshots
    NUnit/                   Lifecycle, assertions, parameterization, retries, and reports
    Resources/
      Configuration/         Application settings and log4net configuration
      Locators/              XML locator definitions
      Samples/Screenshots/   Screenshots saved during the original exercises
      TestData/              Excel test data and the original ODS workbook
    Support/                 Shared helpers and the original path demonstration
    SeleniumAutomation.Examples.csproj
docs/
  samples/reports/            HTML reports saved during the original exercises
```

The portfolio suite and learning examples are separate projects. `tests/SeleniumAutomation.Tests` is the suite to run for portfolio validation. The examples include intentional failures and optional integrations, so running all tests at solution level is not a passing regression check.

## Build

Install an SDK that can build .NET 8 projects and the .NET 8 runtime. Open `SeleniumAutomation.sln` in Visual Studio, or run these commands from the repository root:

```powershell
dotnet restore SeleniumAutomation.sln
dotnet build SeleniumAutomation.sln --no-restore
```

## Run the portfolio suite

```powershell
dotnet test tests/SeleniumAutomation.Tests/SeleniumAutomation.Tests.csproj
```

This opens Chrome for Testing and verifies successful login, rejection of an incorrect password, and rejection of a locked user on [Sauce Demo](https://www.saucedemo.com/). Selenium downloads the test browser and matching driver on the first run. The practice credentials are published by that application.

To run without browser windows in PowerShell:

```powershell
$env:TEST_HEADLESS = "true"
dotnet test tests/SeleniumAutomation.Tests/SeleniumAutomation.Tests.csproj
Remove-Item Env:TEST_HEADLESS
```

In VS Code's Testing panel, expand `SeleniumAutomation.Tests` and `LoginTests` to select one case or the whole fixture. For breakpoints, choose **Debug Test**. See the [login suite coverage and workflow](docs/login-suite.md) for the test matrix, configuration, artifact paths, and limitations.

## Working with the examples

For a visible browser example with two selectable test cases, follow [Debug a Selenium test in VS Code](docs/debugging-tests.md). It includes breakpoints, running a group, and screenshot locations.

The project contains both console demonstrations using `Main()` and NUnit examples. Console demonstrations are not discovered as NUnit tests. The existing console startup object is `SeleniumAutomation.Examples.Integration.TestDB`; running the project starts that database example and requires a configured local MySQL database.

List the NUnit examples without executing their test bodies:

```powershell
dotnet test SeleniumAutomation.sln --no-build --list-tests
```

To run a specific NUnit example, use its fully qualified name:

```powershell
dotnet test SeleniumAutomation.sln --no-build --filter "FullyQualifiedName~SeleniumAutomation.Examples.NUnit.TestParameterizationExcel"
```

These are learning examples rather than a passing regression suite. Some deliberately fail or skip, some only print data, and browser examples depend on external websites. Grid, database, and email examples require separate setup. Existing credential handling and test behavior will be addressed in subsequent assignments.

## Resources and output

Configuration, locator files, workbooks, and sample screenshots are copied to the build output under `Resources/`. Examples resolve these files relative to the compiled application rather than the shell's working directory.

Each project's NUnit execution (one selected test or a selected group) gets a folder under that project's `bin/Debug/net8.0/artifacts/runs/run_<UTC timestamp>_<id>/`. The run folder is printed in test output. Each project retains its five newest executions locally; older run folders and their contents are deleted together. Run one execution at a time when using this simple local retention policy.

Generated output is ignored by Git through the `bin/` and `artifacts/` rules. Output from the previous layout is preserved as one `run_0000_legacy` archive, which counts toward the five-folder limit and rotates out as new runs are created. Historical reports in `docs/samples/reports/` and screenshots in `Resources/Samples/Screenshots/` remain tracked learning records.

The portfolio suite saves ChromeDriver logs for each case and attaches screenshots when a test fails. It does not yet generate HTML reports. In the examples project, `ExtentReportsDemo` produces HTML reports and `BrowserDebugTests` produces step screenshots and ChromeDriver logs. Selenium's downloaded browser and driver cache is separate from run artifacts and is reused across executions.
