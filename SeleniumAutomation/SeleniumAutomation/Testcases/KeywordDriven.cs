using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using SeleniumAutomation.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.Testcases
{
    internal class KeywordDriven
    {
        static IWebDriver driver;

        public static void Click(string pageName, string locatorName, string locatorType)
        {
            if (locatorType.Contains("ID"))
            {
                driver.FindElement(By.Id(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType))).Click();

            }
            else if (locatorType.Contains("XPATH"))
            {
                driver.FindElement(By.XPath(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType))).Click();
            }
            else if (locatorType.Contains("CSS"))
            {
                driver.FindElement(By.CssSelector(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType))).Click();
            }        

        }
        public static void Type(string pageName, string locatorName, string locatorType, string value)
        {
            if (locatorType.Contains("ID"))
            {
                driver.FindElement(By.Id(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType))).SendKeys("trainer@way2automation.com");

            }
            else if (locatorType.Contains("XPATH"))
            {
                driver.FindElement(By.XPath(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType))).SendKeys("trainer@way2automation.com");
            }
            else if (locatorType.Contains("CSS"))
            {
                driver.FindElement(By.CssSelector(XMLLocatorReader.GetLocatorValue(pageName, locatorName, locatorType))).SendKeys("trainer@way2automation.com");
            }
        }
            static void Main(string[] args)
            {

                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\resources\\")
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                if (configuration["Appsettings:browser"].Equals("chrome"))
                {
                    driver = new ChromeDriver();
                }
                else if (configuration["Appsettings:browser"].Equals("firefox"))
                {
                    driver = new FirefoxDriver();
                }

                driver.Url = configuration["Appsettings:testsiteurl"];
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(int.Parse(configuration["AppSettings:implicit.wait"]));


                //string browser = configuration["Appsettings:browser"];

                //driver.FindElement(By.Id(XMLLocatorReader.GetLocatorValue("LoginPage","username","ID"))).SendKeys("trainer@way2automation.com");
                //driver.FindElement(By.XPath(XMLLocatorReader.GetLocatorValue("LoginPage", "password", "XPATH"))).SendKeys("fsegsg");

                Type("LoginPage", "username", "ID", "trainer@way2automation.com");
                Type("LoginPage", "password", "XPATH", "sfdsfdsfs");

            }
        }
    }

