# Selenium Automation in C# 🚀

This project contains practice using Selenium WebDriver in C#.
A robust automation testing framework built with .NET 8, Selenium WebDriver, and NUnit, designed for scalability, maintainability, and easy integration into CI/CD pipelines. This framework supports data-driven, keyword-driven, and parallel testing, while providing rich test reports, screenshots, and logging.

__________________________________________________________________________________________________________


### ✨ Features

✅ Selenium WebDriver for browser automation

✅ NUnit test runner with retry & parallel execution

✅ ExtentReports for rich HTML reports

✅ Log4Net logging with daily logs

✅ Data-driven testing using Excel & ODS

✅ Keyword-driven testing via XML locators

✅ Database integration (MySQL)

✅ Email notifications after test execution

✅ Automatic screenshots on failure

✅ Cross-browser & Selenium Grid support

__________________________________________________________________________________________________________


### 🛠️ Tech Stack

Language: C# (.NET 8)

Testing Framework: NUnit

Automation Library: Selenium WebDriver

Reporting: ExtentReports

Logging: log4net

Database: MySQL

Build Tool: MSBuild

__________________________________________________________________________________________________________

### 📂 Project Structure

| Folder / File              | Description                                   |
|-----------------------------|-----------------------------------------------|
| `Testcases/`               | Core Selenium test cases                      |
| `nunitlearning/`           | NUnit tests & base classes                    |
| `grid/`                    | Selenium Grid parallel execution              |
| `utilities/`               | DB manager, mail sender, config readers       |
| `resources/`               | Config, test data, XML locators               |
| `reports/`                 | ExtentReports HTML results                    |
| `screenshot/`              | Captured screenshots                          |
| `Logs/`                    | Log files                                     |
| `SeleniumAutomation.csproj`| Project configuration file                    |


__________________________________________________________________________________________________________

### 📌 Test Categories

| Category                    | Example Testcases                                                                 |
|----------------------------:|:----------------------------------------------------------------------------------|
| **Browser & Navigation**    | `TestBrowser`, `TestNewWindows`, `TestHandlingTabsAndPopups`                     |
| **UI Interactions**         | `TestDropdown`, `TestSlider`, `TestRightClick`, `TestResizable`, `TestDroppable` |
| **Waits & Synchronization** | `TestFluentWait`, `TestElementPresence`                                          |
| **Advanced Selenium (CDP)** | `TestCDPBadSSL`, `TestCDPGeoLocationOverride`, `TestCDPBlockNetworkRequest`      |
| **Data & Config Driven**    | `KeywordDriven`, `TestParameterization`, `TestParameterizationExcel`             |
| **Integration**             | `TestDB`, `TestMail`, `TestLogger`                                               |
| **Parallel & Grid**         | `TestParallel`, `TestGridParallel`                                               |


__________________________________________________________________________________________________________


### ⚡ Getting Started

1. Clone the repository git clone https://github.com/CR3XX12/SeleniumAutomationCSharp.git
2. Install dependencies 

    Ensure you have:

    a) .NET 8 SDK

    b). Visual Studio

3. Configure resources

Update resources/appsettings.json with environment details

Update resources/locators.xml with element locators

Add/update resources/testdata.xlsx for data-driven tests

__________________________________________________________________________________________________________

### 📊 Reports & Logs

ExtentReports: Generated under /reports

Screenshots: Saved under /screenshot

Logs: Available in /Logs/logfile.log

Example report preview:

<img width="2557" height="1373" alt="image" src="https://github.com/user-attachments/assets/7d0d57a2-138e-4eab-bf7a-429142496bae" />


