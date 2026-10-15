using SeleniumAutomation.Examples.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAutomation.Examples.Integration
{
    internal class TestMail
    {
        static void Main(string[] args)
        {
            string fromEmail = "automationtestemail232@gmail.com"; 
            string password = "dffz ileg lcjg bmhd";
            List<string> toEmails = new List<string> { "crojas@algomau.ca"};
            string subject = "Test Email from C#";
            string body = "This is a test email sent from a C# application.";
            string attachment = ExamplePaths.Resource("Samples", "Screenshots", "2025-08-27_20-27-31.jpg");
        
            MailSender.SendEmail(fromEmail, password, toEmails, subject, body, attachment);
            
        }
    }
}
