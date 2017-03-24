using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace AnandaMvc.Code
{
    public class MailService
    {
        public static bool SendMessageToAnanda(string anandasEmail, string siteUrl, string inputName, string inputEmail, string inputPhone)
        {
            MailMessage message = new MailMessage();

            message.To = anandasEmail;
            message.From = new StringBuilder().Append(inputName).Append("<").Append(inputEmail).Append(">").ToString(); 
            message.Subject = "A new message from visitor to AnandaConversations site";
            message.Body = new StringBuilder().Append("A visitor to the AnandaConversations site at:").Append(Environment.NewLine)
                .Append("'").Append(siteUrl).Append("'").Append(Environment.NewLine)
                .Append("completed the contact form with the following information:")
                .Append(Environment.NewLine).Append(Environment.NewLine)
                .Append("First Name: ").Append(inputName).Append(Environment.NewLine)
                .Append("Email: ").Append(inputEmail).Append(Environment.NewLine)
                .Append("Phone: ").Append(inputPhone).Append(Environment.NewLine)
                .ToString();

            if (IsValidEmail(inputEmail))
            {
                return (MailService.SendMessage(message).ResponseStatus == ResponseStatus.Completed);
            }
            return false;
        }

        public static bool SendTextToAnanda(string anandasEmail, string inputName, string inputEmail, string inputPhone)
        {
            MailMessage message = new MailMessage();

            message.To = anandasEmail;
            message.From = new StringBuilder().Append(inputName).Append("<").Append(inputEmail).Append(">").ToString(); 
            message.Subject = string.Empty;
            message.Body = new StringBuilder().Append("A visitor to the your site ").Append(Environment.NewLine)
                .Append("submitted this info:")
                .Append(Environment.NewLine).Append(Environment.NewLine)
                .Append("First Name: ").Append(inputName).Append(Environment.NewLine)
                .Append("Email: ").Append(inputEmail).Append(Environment.NewLine)
                .Append("Phone: ").Append(inputPhone).Append(Environment.NewLine)
                .ToString();

            if (IsValidEmail(inputEmail))
            {
                return (MailService.SendMessage(message).ResponseStatus == ResponseStatus.Completed);
            }
            return false;
        }

        public static bool SendThankYouMessageToVisitor(string siteUrl, string inputName, string inputEmail)
        {
            MailMessage message = new MailMessage();

            message.To = new StringBuilder().Append(inputName).Append("<").Append(inputEmail).Append(">").ToString();
            message.From = "noreply@anandaconversations.brinkster.com"; // 
            message.Subject = "Thank you for contacting Ananda!";
            message.Body = new StringBuilder()
                .Append("Dear ").Append(inputName).Append(", ")
                .Append(Environment.NewLine)
                .Append("Thank you for reaching out to me.  I apologize for this automatically-generated response.  I will be in touch ")
                .Append("with you personally very soon.")
                .Append(Environment.NewLine)
                .Append("Affectionately, ").Append(Environment.NewLine)
                .Append("Ananda").ToString();

            if (IsValidEmail(inputEmail))
            {
                return (MailService.SendMessage(message).ResponseStatus == ResponseStatus.Completed);
            }
            return false;
        }

        public static IRestResponse SendMessage(MailMessage message)
        {
            // validate the email addresses before sending

            RestClient client = new RestClient();
            client.BaseUrl = "https://api.mailgun.net/v2";
            client.Authenticator = new HttpBasicAuthenticator("api", "");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "sandbox19040.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", message.From);
            request.AddParameter("to", message.To);
            request.AddParameter("subject", message.Subject);
            request.AddParameter("text", message.Body);
            request.Method = Method.POST;
            return client.Execute(request);
        }

        private static bool IsValidEmail(string emailAddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailAddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            } 
        }
    }
}
