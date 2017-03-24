using AnandaMvc.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AnandaMvc.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /Contact/

        public ActionResult Index(string inputFirstName, string inputEmail, string inputPhone)
        {
            if (!String.IsNullOrEmpty(inputEmail) && !String.IsNullOrEmpty(inputFirstName))
            {
                SendMessageToAnanda(inputFirstName, inputEmail, inputPhone);
                SendTextToAnanda(inputFirstName, inputEmail, inputPhone);
                SendMessageToVisitor(inputFirstName, inputEmail, inputPhone);

                return View("ThankYou");
            }
            else
            { 
                ViewBag.Title = "Talk to Ananda";
                return View();
            }
        }

        public ActionResult ThankYou()
        {
            return View();
        }

        private void SendMessageToAnanda(string inputFirstName, string inputEmail, string inputPhone)
        {
            var anandasEmail = System.Configuration.ConfigurationManager.AppSettings["AnandasEmail"].ToString();
            var siteUrl = Request.Url.AbsoluteUri;
            var canSend = MailService.SendMessageToAnanda(anandasEmail, siteUrl, inputFirstName, inputEmail, inputPhone);
            if (!canSend) throw new Exception("Failed to send message to Ananda");
        }

        private void SendTextToAnanda(string inputFirstName, string inputEmail, string inputPhone)
        {
            var anandasEmail = System.Configuration.ConfigurationManager.AppSettings["AnandasCell"].ToString();
            var canSend = MailService.SendTextToAnanda(anandasEmail, inputFirstName, inputEmail, inputPhone);
            if (!canSend) throw new Exception("Failed to send text to Ananda");
        }

        private void SendMessageToVisitor(string inputFirstName, string inputEmail, string inputPhone)
        {
            var siteUrl = Request.Url.AbsoluteUri;
            var canSend = MailService.SendThankYouMessageToVisitor(siteUrl, inputFirstName, inputEmail);
            if (!canSend) throw new Exception("Failed to send message to visitor");
        }
    }
}
