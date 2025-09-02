using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SeleniumAutomation.utilities
{
    internal class XMLLocatorReader
    {
        public static string GetLocatorValue(string pageName, string elementName, string locatorType)
        {
            string locatorValue = null;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\resources\\locators.xml");

            //Load the XML File
            XmlElement root = xmlDoc.DocumentElement;

            //Construct XPATH expression to select the specified element under the specified page with given locator type

            string xpath = $"/locators/{pageName}/{elementName}[LocatorType='{locatorType}']/LocatorValue";

            //Select the locator value node
            XmlNode locatorValueNode = root.SelectSingleNode(xpath);

            if (locatorValueNode != null) {
                locatorValue = locatorValueNode.InnerText;
            }
            return locatorValue;
                }
        static void Main(string[] args)
        {
            Console.WriteLine(GetLocatorValue("LoginPage", "username", "ID"));

            Console.WriteLine(GetLocatorValue("RegistrationPage", "password", "XPATH"));
        }

    }
}
