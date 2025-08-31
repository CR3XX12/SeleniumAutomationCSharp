using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.Testcases
{
    internal class TestRelativeLocators
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://www.way2automation.com/way2auto_jquery/index.php";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Window.Maximize();

            IWebElement above =  driver.FindElement(RelativeBy.WithLocator(By.TagName("input")).Above(By.TagName("Select")));
            above.SendKeys("trainer@way2automation.com");

            IWebElement below = driver.FindElement(RelativeBy.WithLocator(By.TagName("input")).Below(By.TagName("Select")));
            below.SendKeys("New Delhi");

            IWebElement rightOf = driver.FindElement(RelativeBy.WithLocator(By.XPath("//*[@id=\"load_form\"]/fieldset[7]/input")).RightOf(By.TagName("label")));
            rightOf.SendKeys("Password123");
        }
    }
}
