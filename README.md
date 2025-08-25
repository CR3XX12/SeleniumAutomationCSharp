# Selenium Automation in C# 🚀

This project contains practice test cases using Selenium WebDriver in C#.
It demonstrates how to handle different types of web automation tasks such as alerts, waits, dropdowns, and iframes.

📂 Project Structure

Each test case is under the namespace SeleniumAutomation.Testcases:

🔹 TestAlerts

Demonstrates handling JavaScript alerts on Rediff Login Page

Switches to alert

Prints alert text

Accepts alert

🔹 TestBrowser

Runs Selenium tests across multiple browsers (Chrome, Firefox, Edge)

Navigates to Gmail login

Uses implicit and explicit waits (WebDriverWait)

Demonstrates element interaction and error message handling

🔹 TestDropdown

Automates dropdown interaction on Wikipedia

Uses SelectElement to choose languages

Iterates through all dropdown options

Scrapes and prints footer links

🔹 TestFluentWait

Example of Fluent Wait using DefaultWait<IWebDriver>

Custom polling interval

Ignores exceptions

Waits dynamically for Gmail password field

🔹 TestTryIT

Demonstrates working with iframes on W3Schools TryIt Editor

Switches into iframe

Submits form inside the iframe

Returns to default content

Prints total number of frames

⚙️ Technologies Used

C# .NET

Selenium WebDriver

ChromeDriver, FirefoxDriver, EdgeDriver

Selenium Support (Waits, ExpectedConditions)

🏃 How to Run

Clone the repo:

git clone https://github.com/yourusername/your-repo-name.git


Open the solution in Visual Studio

Install required NuGet packages:

Selenium.WebDriver

Selenium.Support

Selenium.WebDriver.ChromeDriver (and others as needed)

Update the browser path if required

Run any class with a Main() method

💡 Why This Project?

This repo highlights my ability to:

Build automation scripts in C#

Apply best practices for Selenium

Write clean, maintainable test code

Work with real-world web elements recruiters often care about
