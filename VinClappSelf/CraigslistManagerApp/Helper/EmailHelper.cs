using System.Configuration;
using System.Globalization;
using System.Net.Mail;

namespace CraigslistManagerApp.Helper
{
    public class EmailHelper
    {
        public static void SendEmail(MailAddress toAddress, string subject, string body)
        {

            var smtpServerAddress =
                     ConfigurationManager.AppSettings["SMTPServer"].ToString(CultureInfo.InvariantCulture);

            var defaultFromEmail =
                ConfigurationManager.AppSettings["DefaultFromEmail"].ToString(CultureInfo.InvariantCulture);

            var displayName = ConfigurationManager.AppSettings["DisplayName"].ToString(CultureInfo.InvariantCulture);

            var fromAddress = new MailAddress(defaultFromEmail, displayName);

            var message = new MailMessage(fromAddress, toAddress)
                {
                    From = fromAddress,
                    Body = body,
                    IsBodyHtml = true,
                    Subject = "VINCLAPP FULL SERVICE  - " + subject
                };


            var client = new SmtpClient()
            {
                Host = smtpServerAddress,
                Port = 587,
                EnableSsl = true
            };
            var ntlmAuthentication =
                new System.Net.NetworkCredential(
                    System.Configuration.ConfigurationManager.AppSettings["DefaultFromEmail"].ToString(
                        CultureInfo.InvariantCulture),
                    System.Configuration.ConfigurationManager.AppSettings["TrackEmailPass"].ToString(
                        CultureInfo.InvariantCulture));

            client.Credentials = ntlmAuthentication;

            client.Send(message);
        }

        public static void SendErrorEmail(MailAddress toAddress, string subject, string body)
        {

            var smtpServerAddress =
                     ConfigurationManager.AppSettings["SMTPServer"].ToString(CultureInfo.InvariantCulture);

            var defaultFromEmail =
                ConfigurationManager.AppSettings["DefaultFromEmailForError"].ToString(CultureInfo.InvariantCulture);

            var displayName = ConfigurationManager.AppSettings["DisplayName"].ToString(CultureInfo.InvariantCulture);

            var fromAddress = new MailAddress(defaultFromEmail, displayName);

            var message = new MailMessage(fromAddress, toAddress)
                {
                    From = fromAddress,
                    Body = body,
                    IsBodyHtml = true,
                    Subject = "VINCLAPP FULL SERVICE  - " + subject
                };


            var client = new SmtpClient()
            {
                Host = smtpServerAddress,
                Port = 587,
                EnableSsl = true
            };

            var ntlmAuthentication =
                new System.Net.NetworkCredential(
                    System.Configuration.ConfigurationManager.AppSettings["DefaultFromEmailForError"].ToString(
                        CultureInfo.InvariantCulture),
                    System.Configuration.ConfigurationManager.AppSettings["TrackEmailPass"].ToString(
                        CultureInfo.InvariantCulture));

            client.Credentials = ntlmAuthentication;

            client.Send(message);
        }
    }
}
