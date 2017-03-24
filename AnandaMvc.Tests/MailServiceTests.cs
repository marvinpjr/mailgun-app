using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnandaMvc.Code;
using RestSharp;

namespace AnandaMvc.Tests
{
    [TestClass]
    public class MailServiceTests
    {
        //[TestMethod]
        //public void CanSendMailMessage()
        //{
        //    MailMessage message = new MailMessage()
        //    {
        //        To = "Marvin Palmer <marvinpalmerjr@gmail.com>",
        //        From = "Mailgun Sandbox <postmaster@sandbox19040.mailgun.org>",
        //        Subject = "Hello Marvin Palmer",
        //        Body = "Congratulations Marvin Palmer, you just sent an email with Mailgun!  You are truly awesome!  You can see a record of this email in your logs: https://mailgun.com/cp/log .  You can send up to 300 emails/day from this sandbox server.  Next, you should add your own domain so you can send 10,000 emails/month for free."
        //    };
        //    var result = MailService.SendMessage(message);
        //    Assert.AreEqual(ResponseStatus.Completed, result.ResponseStatus); // 1 is completed
        //}

        [TestMethod]
        public void CanSendMessageToAnanda()
        {
            var anandasEmail = System.Configuration.ConfigurationManager.AppSettings["AnandasEmail"].ToString();
            var siteUrl = "AnandaMvc.Tests";
            var canSendMessage = MailService.SendMessageToAnanda(anandasEmail, siteUrl, "Marvin", "marvinpalmerjr@gmail.com", "615-335-6701");

            Assert.IsTrue(canSendMessage); // 1 is completed
        }

        [TestMethod]
        public void CanSendMessageToVisitor()
        {
            var siteUrl = "AnandaMvc.Tests";
            var canSendMessage = MailService.SendThankYouMessageToVisitor(siteUrl, "Marvin", "marvinpalmerjr@gmail.com");

            Assert.IsTrue(canSendMessage);
        }

        [TestMethod]
        public void CannotSendMessageFromBogusEmail()
        {
            var siteUrl = "AnandaMvc.Tests";
            var canSendMessage = MailService.SendThankYouMessageToVisitor(siteUrl, "Marvin", "marvinpalmerjratgmaildotcom");

            Assert.IsFalse(canSendMessage);
        }
    }
}
