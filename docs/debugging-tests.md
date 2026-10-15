# Debug a Selenium test in VS Code

This exercise uses a local HTML page and a visible Chrome for Testing window, launched with a fresh temporary profile for each case. Install Microsoft's C# Dev Kit extension. Selenium Manager downloads and caches Chrome for Testing and its matching driver separately from the installed Chrome. The first run needs network access and can take longer while the browser downloads; later runs reuse the cache. No Google account or manual profile selection is needed.

## Build and discover

Open the repository folder in VS Code, then run:

```powershell
dotnet build SeleniumAutomation.sln
```

Open the Testing panel (flask icon), refresh discovery if necessary, and expand the test tree until you find `BrowserDebugTests`. It contains English and Spanish cases under `ChoosingLanguageDisplaysConfirmation`.

## Debug one case

1. Open `examples/SeleniumAutomation.Examples/NUnit/BrowserDebugTests.cs`.
2. Set a breakpoint on the `var languageDropdown = ...` line by clicking beside its line number.
3. In the Testing panel, right-click the English or Spanish case and choose **Debug Test**.
4. Chrome opens the local page and execution pauses at the breakpoint.
5. Use **F10 (Step Over)** to execute each statement. Arrange Chrome beside VS Code to watch the dropdown selection and confirmation.
6. Use **F5 (Continue)** to finish. The browser closes when the test ends.

Use **Debug Test**, rather than the play button, when you want breakpoints to pause execution. Without breakpoints, these small tests finish quickly.

## Run a group

Use the play button beside `BrowserDebugTests` to run both cases. The terminal equivalent is:

```powershell
dotnet test SeleniumAutomation.sln --no-build --filter "TestCategory=BrowserDemo"
```

## Results and screenshots

The Testing panel shows pass/fail results and test output for the tests you selected. Each demo case saves three screenshots: page opened, language selected, and confirmation shown. Find them under:

```text
examples/SeleniumAutomation.Examples/bin/Debug/net8.0/artifacts/runs/run_<UTC timestamp>_<id>/screenshots/
```

The run folder is printed in test output. Each selected test or group starts one execution. Only the five newest execution folders are kept locally, including their reports, screenshots, and logs. Run one execution at a time. Output from the previous layout is archived as one older run and removed as it rotates beyond the limit.

Screenshots are captured at the named steps; this demo does not capture every WebDriver command or produce video. An error before a capture can leave fewer than three images.

ChromeDriver diagnostic logs are saved in the same run folder under `logs/` if browser startup needs troubleshooting.

The existing `ExtentReportsDemo` generates a separate HTML report for its own simulated tests only. It does not collect results for `BrowserDebugTests` or the rest of the repository. Shared HTML reporting will be a separate assignment.
