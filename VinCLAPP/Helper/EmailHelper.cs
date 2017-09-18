using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using VinCLAPP.Model;

namespace VinCLAPP.Helper
{
    internal class EmailHelper
    {
        public static void SendEmail(MailAddress toAddress, string subject, string body)
        {
            var smtpServerAddress = ConfigHelper.SMTPServer;

            var defaultFromEmail = ConfigHelper.DefaultFromEmail;

            var displayName = ConfigHelper.DisplayName;

            var fromAddress = new MailAddress(defaultFromEmail, displayName);

            var message = new MailMessage(fromAddress, toAddress)
                              {
                                  From = fromAddress,
                                  Body = body,
                                  IsBodyHtml = true,
                                  Subject = "POST CL - " + subject
                              };


            var client = new SmtpClient(smtpServerAddress, 587);

            var ntlmAuthentication = new NetworkCredential(ConfigHelper.DefaultFromEmail, ConfigHelper.TrackEmailPass);

            client.Credentials = ntlmAuthentication;

            client.Send(message);
        } 

        public static void SendConfirmationEmailForSignUp(PostClCustomer customer)
        {
            try
            {
                string smtpServerAddress = ConfigHelper.SMTPServer;

                string defaultFromEmail = ConfigHelper.DefaultFromEmail;

                string displayName = ConfigHelper.DisplayName;

                var fromAddress = new MailAddress(defaultFromEmail, displayName);

                var message = new MailMessage
                {
                    From = fromAddress,
                    IsBodyHtml = true,
                    Body = CreateConfirmationBodyEmailForWelcomeCustomer(customer),
                    Subject = "There is a new customer has been added to QuickBook."
                };

                foreach (var tmp in ConfigHelper.ConfirmationEmail.Split(new string[]{","},StringSplitOptions.None))
                {
                    if (!string.IsNullOrEmpty(tmp))
                        message.To.Add(new MailAddress(tmp));

                }
                var client = new SmtpClient()
                {
                    Host = smtpServerAddress,
                    Port = 587,
                    EnableSsl = true
                };

                var ntlmAuthentication = new NetworkCredential(ConfigHelper.DefaultFromEmail, ConfigHelper.TrackEmailPass);


                client.Credentials = ntlmAuthentication;


                client.Send(message);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }



        }
          public static void SendConfirmationEmailForPayment(BillingCustomer customer)
        {
            try
            {
                string smtpServerAddress = ConfigHelper.SMTPServer;

                string defaultFromEmail = ConfigHelper.DefaultFromEmail;

                string displayName = ConfigHelper.DisplayName;

                var fromAddress = new MailAddress(defaultFromEmail, displayName);

                var message = new MailMessage
                {
                    From = fromAddress,
                    IsBodyHtml = true,
                    Body = CreateConfirmationBodyEmailForCreditCardPayment(customer),
                    Subject = "Customer " + customer.FirstName +  " " + customer.LastName + " has made a payment. Payment successfully!!!!!!"
                };

                foreach (var tmp in ConfigHelper.ConfirmationEmail.Split(new string[]{","},StringSplitOptions.None))
                {
                    if (!string.IsNullOrEmpty(tmp))
                        message.To.Add(new MailAddress(tmp));

                }
                var client = new SmtpClient()
                {
                    Host = smtpServerAddress,
                    Port = 587,
                    EnableSsl = true
                };

                var ntlmAuthentication = new NetworkCredential(ConfigHelper.DefaultFromEmail, ConfigHelper.TrackEmailPass);


                client.Credentials = ntlmAuthentication;


                client.Send(message);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }



        }

          public static void SendConfirmationEmailForPaymentToCustomer(MailAddress address, BillingCustomer customer)
          {
              try
              {
                  string smtpServerAddress = ConfigHelper.SMTPServer;

                  string defaultFromEmail = ConfigHelper.DefaultFromEmail;

                  string displayName = ConfigHelper.DisplayName;

                  var fromAddress = new MailAddress(defaultFromEmail, displayName);

                  var message = new MailMessage
                  {
                      From = fromAddress,
                      IsBodyHtml = true,
                      Body = CreateConfirmationBodyEmailForCustomerCreditCardPayment(customer),
                      Subject = "Hi " + customer.FirstName + " " + customer.LastName + ". Payment Confirmation!"
                  };
                    message.To.Add(address);
                
                  var client = new SmtpClient()
                  {
                      Host = smtpServerAddress,
                      Port = 587,
                      EnableSsl = true
                  };

                  var ntlmAuthentication = new NetworkCredential(ConfigHelper.DefaultFromEmail, ConfigHelper.TrackEmailPass);


                  client.Credentials = ntlmAuthentication;


                  client.Send(message);

              }
              catch (Exception ex)
              {
                  throw new Exception(ex.Message);
              }



          }


          public static void SendConfirmationEmailForDataFeedSetup(PostClDataFeed postClDatafeed)
          {
              try
              {
                  string smtpServerAddress = ConfigHelper.SMTPServer;

                  string defaultFromEmail = ConfigHelper.DefaultFromEmail;

                  string displayName = ConfigHelper.DisplayName;

                  var fromAddress = new MailAddress(defaultFromEmail, displayName);

                  var message = new MailMessage
                  {
                      From = fromAddress,
                      IsBodyHtml = true,
                      Body = CreateConfirmationBodyEmailForDataFeedSetup(postClDatafeed),
                      Subject = "Pending Setup Feed Request"
                  };

                  foreach (var tmp in ConfigHelper.ConfirmationFeedEmail.Split(new string[] { "," }, StringSplitOptions.None))
                  {
                      if (!string.IsNullOrEmpty(tmp))
                          message.To.Add(new MailAddress(tmp));

                  }
                  var client = new SmtpClient()
                  {
                      Host = smtpServerAddress,
                      Port = 587,
                      EnableSsl = true
                  };

                  var ntlmAuthentication = new NetworkCredential(ConfigHelper.DefaultFromEmail, ConfigHelper.TrackEmailPass);


                  client.Credentials = ntlmAuthentication;


                  client.Send(message);

              }
              catch (Exception ex)
              {
                  throw new Exception(ex.Message);
              }



          }

          public static void SendConfirmationEmailForPaymentToVendor(PostClDataFeed postClDatafeed)
          {
              try
              {
                  string smtpServerAddress = ConfigHelper.SMTPServer;

                  string defaultFromEmail = ConfigHelper.DefaultFromEmail;

                  string displayName = ConfigHelper.DisplayName;

                  var fromAddress = new MailAddress(defaultFromEmail, displayName);

                  var message = new MailMessage
                  {
                      From = fromAddress,
                      IsBodyHtml = true,
                      Body = CreateConfirmationBodyEmailForDataFeedSetupForVendor(postClDatafeed),
                      Subject = "Pending Setup Feed Request"
                  };

                  message.To.Add(new MailAddress(postClDatafeed.VendorEmail));

                  message.To.Add(new MailAddress(GlobalVar.CurrentAccount.AccountName));

                  foreach (var tmp in ConfigHelper.ConfirmationFeedEmail.Split(new string[] { "," }, StringSplitOptions.None))
                  {
                      if (!string.IsNullOrEmpty(tmp))
                          message.CC.Add(new MailAddress(tmp));

                  }

                 
                  var client = new SmtpClient()
                  {
                      Host = smtpServerAddress,
                      Port = 587,
                      EnableSsl = true
                  };

                  var ntlmAuthentication = new NetworkCredential(ConfigHelper.DefaultFromEmail, ConfigHelper.TrackEmailPass);


                  client.Credentials = ntlmAuthentication;


                  client.Send(message);

              }
              catch (Exception ex)
              {
                  throw new Exception(ex.Message);
              }



          }



          public static void SendChangePasswordEmail(MailAddress toAddress,PostClCustomer customer)
          {
              try
              {
                  string smtpServerAddress = ConfigHelper.SMTPServer;

                  string defaultFromEmail = ConfigHelper.DefaultFromEmail;

                  string displayName = ConfigHelper.DisplayName;

                  var fromAddress = new MailAddress(defaultFromEmail, displayName);

                  var message = new MailMessage(fromAddress, toAddress)
                  {
                      IsBodyHtml = true,
                      Body = CreateBodyEmailForChangePassword(customer),
                      Subject = "POSTCL. Password Recovery."
                  };


                  var client = new SmtpClient()
                  {
                      Host = smtpServerAddress,
                      Port = 587,
                      EnableSsl = true
                  };

                  var ntlmAuthentication = new NetworkCredential(ConfigHelper.DefaultFromEmail, ConfigHelper.TrackEmailPass);


                  client.Credentials = ntlmAuthentication;


                  client.Send(message);

              }
              catch (Exception ex)
              {
                  throw new Exception(ex.Message);
              }



          }

        public static void SendWelcomeEmail(MailAddress toAddress,PostClCustomer customer)
        {
            try
            {
                string smtpServerAddress = ConfigHelper.SMTPServer;

                string defaultFromEmail = ConfigHelper.DefaultFromEmail;

                string displayName = ConfigHelper.DisplayName;

                var fromAddress = new MailAddress(defaultFromEmail, displayName);

                var message = new MailMessage(fromAddress, toAddress)
                {
                    IsBodyHtml = true,
                    Body = CreateBodyEmailForWelcomeCustomer(customer),
                    Subject = "Welcome to POSTCL. Thanks you for signing up to use POSTCL program. "
                };

             
                var client = new SmtpClient()
                {
                    Host = smtpServerAddress,
                    Port = 587,
                    EnableSsl = true
                };

                var ntlmAuthentication = new NetworkCredential(ConfigHelper.DefaultFromEmail, ConfigHelper.TrackEmailPass);


                client.Credentials = ntlmAuthentication;


                client.Send(message);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }



        }

        public static void SendMail(MailAddress toEmail, string subject, string body, string file)
        {
            try
            {
                var client = SmtpClient();

                var message = MailMessage(toEmail, subject, (body));

                if (!String.IsNullOrEmpty(file))
                {
                    var pdfHelper = new PdfHelper();
                    var workStream = pdfHelper.WritePdf(file);
                    var attach = new Attachment(workStream, String.Format("Report_{0}.pdf", DateTime.Now.ToString("MM.dd.yyyy")), "application/pdf");
                    /* Attach the newly created email attachment */
                    message.Attachments.Add(attach);
                }

                client.Send(message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void SendExcelMail(string fileName,byte[] byteArray)
        {
            try
            {
                string smtpServerAddress = ConfigHelper.SMTPServer;

                string defaultFromEmail = ConfigHelper.DefaultFromEmail;

                string displayName = ConfigHelper.DisplayName;

                var fromAddress = new MailAddress(defaultFromEmail, displayName);

                var toAddress = new MailAddress(GlobalVar.CurrentAccount.AccountName);

                var message = new MailMessage(fromAddress, toAddress)
                {
                    IsBodyHtml = true,
                    Body = CreateBodyEmailFofTrackingReport(),
                    Subject = "Tracking Report From CLDMS",

                };
                var ms = new MemoryStream(byteArray);
         

                var attach = new Attachment(ms, fileName);
                /* Attach the newly created email attachment */
                message.Attachments.Add(attach);

                var client = new SmtpClient()
                {
                    Host = smtpServerAddress,
                    Port = 587,
                    EnableSsl = true
                };

                var ntlmAuthentication = new NetworkCredential(ConfigHelper.DefaultFromEmail, ConfigHelper.TrackEmailPass);


                client.Credentials = ntlmAuthentication;


                client.Send(message);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

          
        }

        public static void SendSupportMail(string subject, string custommessage)
        {
            try
            {
                string smtpServerAddress = ConfigHelper.SMTPServer;

                string defaultFromEmail = ConfigHelper.DefaultFromEmail;

                string displayName = ConfigHelper.DisplayName;

                var fromAddress = new MailAddress(defaultFromEmail, displayName);

                var message = new MailMessage()
                {
                    From = fromAddress,
                    IsBodyHtml = false,
                    Body = CreateBodyEmailForSupport(subject,custommessage),
                    Subject = "Support Question Ticket From CLDMS",

                };

                foreach (var tmp in ConfigHelper.ConfirmationEmail.Split(new string[] { "," }, StringSplitOptions.None))
                {
                    if (!string.IsNullOrEmpty(tmp))
                        message.To.Add(new MailAddress(tmp));

                }
             
                var client = new SmtpClient()
                {
                    Host = smtpServerAddress,
                    Port = 587,
                    EnableSsl = true
                };

                var ntlmAuthentication = new NetworkCredential(ConfigHelper.DefaultFromEmail, ConfigHelper.TrackEmailPass);


                client.Credentials = ntlmAuthentication;


                client.Send(message);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
        private static string CreateBodyEmailFofTrackingReport()
        {
            var builder = new StringBuilder();

            builder.AppendFormat("    <html>");
            builder.AppendFormat("<head>");

            builder.AppendFormat("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            builder.AppendFormat("<title>PostCL</title>");

            builder.AppendFormat(" <style type=\"text/css\">");
            builder.AppendFormat(" </style>");

            builder.AppendFormat("</head>");
            builder.AppendFormat("<body>");

            builder.AppendFormat("<table style=\"font-family: Arial, sans-serif; font-size: .9em; border: 1px solid black; padding-top: 15px;\" cellpadding=\"20\" width=\"600\" cellspacing=\"0\" bgcolor=\"#dad0c7\">");
            builder.AppendFormat("  <tr>");

            builder.AppendFormat("<td style=\"padding-bottom: 0; padding-top: 0;\">");
            builder.AppendFormat(" <table cellpadding=\"20\" style=\"font-family: Arial, sans-serif;\">");

            builder.AppendFormat("<tr>");
            builder.AppendFormat("  <td width=\"260\"><img src=\"http://cldms.com//Content/img/logo.png\" width=\"260\"></td><td style=\"padding: 0;\"><h1 style=\"font-size: 2.5em; margin-top: 25px; color: #0f0a06; text-shadow: 2px 2px 3px white; font-style: italic; font-weight: bold; margin-bottom: 0;\">Tracking Report!</h1></td>");

            builder.AppendFormat(" </tr>");
            builder.AppendFormat(" </table>");

            builder.AppendFormat(" </td>");
            builder.AppendFormat(" </tr>");

            builder.AppendFormat(" <tr id=\"main-content\" align=\"center\">");
            builder.AppendFormat(" <td style=\"padding-top: 0;\">");

            builder.AppendFormat("      <table style=\"font-family: Arial, sans-serif; font-size: .9em;\" width=\"560\" cellpadding=\"20\">");
            builder.AppendFormat(" <tr>");

            builder.AppendFormat("<td id=\"header\" align=\"center\" style=\"padding-top: 0;\">");

            builder.AppendFormat(" <p style=\"background: white; border: 1px solid #c3b8af; padding: 10px; text-align: left;\">");
            builder.AppendFormat("  <span style=\"font-weight: bold; font-size: 1.2em;\">View Tracking Report in attachement!</span><br />");

            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//pricing\">Pricing</a>");

            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//faq\">FAQ</a>");
            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//download\">Installation Instructions</a>");

            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//tos\">Terms of Service</a>");
            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//privacy\">Privacy Policy</a>");

            builder.AppendFormat("  <br>- <a href=\"http://vincontrol.com\">VinControl</a>");
            builder.AppendFormat("     <br /><br />");

            builder.AppendFormat(" </p>");
            builder.AppendFormat(" </td>");


            builder.AppendFormat(" </tr>");
            builder.AppendFormat(" </table>");

            builder.AppendFormat(" </td>");
            builder.AppendFormat("  </tr>");

            builder.AppendFormat("<tr>");
            builder.AppendFormat("<td id=\"bottom\" align=\"center\" bgcolor=\"#191008\" style=\"color: #9a0f15;\">");

            builder.AppendFormat(" <a href=\"http://cldms.com/\" style=\"color: white;\">postcl.com</a> | <a href=\"http://cldms.com//about\" style=\"color: white;\">about us</a> | <a href=\"http://cldms.com//home\" style=\"color: white;\">signup</a> | <a href=\"\" style=\"color: white;\">View in browser</a>");
            builder.AppendFormat("</td>");

            builder.AppendFormat(" </tr>");
            builder.AppendFormat(" </table>");

            builder.AppendFormat("</body>");
            builder.AppendFormat("</html>");

            return builder.ToString();
        }

        private static string CreateBodyEmailForSupport(string subject, string message)
        {
            var builder = new StringBuilder();


            builder.AppendLine("Dealer Name: " + GlobalVar.CurrentDealer.DealershipName );
            builder.AppendLine("Dealer Address: " + GlobalVar.CurrentDealer.StreetAddress );
            builder.AppendLine("Dealer City: " + GlobalVar.CurrentDealer.City);
            builder.AppendLine("Dealer Zipcode: " + GlobalVar.CurrentDealer.ZipCode);
            builder.AppendLine("Email Contact : " + GlobalVar.CurrentAccount.AccountName);
            builder.AppendLine("Phone : " + GlobalVar.CurrentDealer.PhoneNumber );
            builder.AppendLine("Subject : " + subject);
            builder.AppendLine("Message : " + message );
         

            return builder.ToString();
        }

        private static string CreateBodyEmailForChangePassword(PostClCustomer customer)
        {
            var builder = new StringBuilder();

            builder.AppendFormat("    <html>");
            builder.AppendFormat("<head>");

            builder.AppendFormat("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            builder.AppendFormat("<title>PostCL</title>");

            builder.AppendFormat(" <style type=\"text/css\">");
            builder.AppendFormat(" </style>");

            builder.AppendFormat("</head>");
            builder.AppendFormat("<body>");

            builder.AppendFormat("<table style=\"font-family: Arial, sans-serif; font-size: .9em; border: 1px solid black; padding-top: 15px;\" cellpadding=\"20\" width=\"600\" cellspacing=\"0\" bgcolor=\"#dad0c7\">");
            builder.AppendFormat("  <tr>");

            builder.AppendFormat("<td style=\"padding-bottom: 0; padding-top: 0;\">");
            builder.AppendFormat(" <table cellpadding=\"20\" style=\"font-family: Arial, sans-serif;\">");

            builder.AppendFormat("<tr>");
            builder.AppendFormat("  <td width=\"260\"><img src=\"http://cldms.com//Content/img/logo.png\" width=\"260\"></td><td style=\"padding: 0;\"><h1 style=\"font-size: 2.5em; margin-top: 25px; color: #0f0a06; text-shadow: 2px 2px 3px white; font-style: italic; font-weight: bold; margin-bottom: 0;\">Temporary password!</h1></td>");

            builder.AppendFormat(" </tr>");
            builder.AppendFormat(" </table>");

            builder.AppendFormat(" </td>");
            builder.AppendFormat(" </tr>");

            builder.AppendFormat(" <tr id=\"main-content\" align=\"center\">");
            builder.AppendFormat(" <td style=\"padding-top: 0;\">");

            builder.AppendFormat("      <table style=\"font-family: Arial, sans-serif; font-size: .9em;\" width=\"560\" cellpadding=\"20\">");
            builder.AppendFormat(" <tr>");

            builder.AppendFormat("<td id=\"header\" align=\"center\" style=\"padding-top: 0;\">");
            
            builder.AppendFormat(" <p style=\"background: white; border: 1px solid #c3b8af; padding: 10px; text-align: left;\">");
            builder.AppendFormat("  <span style=\"font-weight: bold; font-size: 1.2em;\">Registration Successful!</span><br />");

            builder.AppendFormat("  <br>- Username :  " + customer.CustomerEmail);
            builder.AppendFormat("  <br>- Temporary Password : " + customer.TemporaryPassword);
            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//pricing\">Pricing</a>");

            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//faq\">FAQ</a>");
            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//download\">Installation Instructions</a>");

            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//tos\">Terms of Service</a>");
            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//privacy\">Privacy Policy</a>");

            builder.AppendFormat("  <br>- <a href=\"http://vincontrol.com\">VinControl</a>");
            builder.AppendFormat("     <br /><br />");

            builder.AppendFormat(" </p>");
            builder.AppendFormat(" </td>");


            builder.AppendFormat(" </tr>");
            builder.AppendFormat(" </table>");

            builder.AppendFormat(" </td>");
            builder.AppendFormat("  </tr>");

            builder.AppendFormat("<tr>");
            builder.AppendFormat("<td id=\"bottom\" align=\"center\" bgcolor=\"#191008\" style=\"color: #9a0f15;\">");

            builder.AppendFormat(" <a href=\"http://cldms.com/\" style=\"color: white;\">postcl.com</a> | <a href=\"http://cldms.com//about\" style=\"color: white;\">about us</a> | <a href=\"http://cldms.com//home\" style=\"color: white;\">signup</a> | <a href=\"\" style=\"color: white;\">View in browser</a>");
            builder.AppendFormat("</td>");

            builder.AppendFormat(" </tr>");
            builder.AppendFormat(" </table>");

            builder.AppendFormat("</body>");
            builder.AppendFormat("</html>");

            return builder.ToString();
        }
   
        private static string CreateBodyEmailForWelcomeCustomer(PostClCustomer customer)
        {
            var builder = new StringBuilder();

            builder.AppendFormat("    <html>");
            builder.AppendFormat("<head>");

            builder.AppendFormat("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            builder.AppendFormat("<title>PostCL</title>");

            builder.AppendFormat(" <style type=\"text/css\">");
            builder.AppendFormat(" </style>");

            builder.AppendFormat("</head>");
            builder.AppendFormat("<body>");

            builder.AppendFormat("<table style=\"font-family: Arial, sans-serif; font-size: .9em; border: 1px solid black; padding-top: 15px;\" cellpadding=\"20\" width=\"600\" cellspacing=\"0\" bgcolor=\"#dad0c7\">");
            builder.AppendFormat("  <tr>");

            builder.AppendFormat("<td style=\"padding-bottom: 0; padding-top: 0;\">");
            builder.AppendFormat(" <table cellpadding=\"20\" style=\"font-family: Arial, sans-serif;\">");

            builder.AppendFormat("<tr>");
            builder.AppendFormat("  <td width=\"260\"><img src=\"http://cldms.com//Content/img/logo.png\" width=\"260\"></td><td style=\"padding: 0;\"><h1 style=\"font-size: 2.5em; margin-top: 25px; color: #0f0a06; text-shadow: 2px 2px 3px white; font-style: italic; font-weight: bold; margin-bottom: 0;\">Welcome!</h1></td>");

            builder.AppendFormat(" </tr>");
            builder.AppendFormat(" </table>");

            builder.AppendFormat(" </td>");
            builder.AppendFormat(" </tr>");

            builder.AppendFormat(" <tr id=\"main-content\" align=\"center\">");
            builder.AppendFormat(" <td style=\"padding-top: 0;\">");

            builder.AppendFormat("      <table style=\"font-family: Arial, sans-serif; font-size: .9em;\" width=\"560\" cellpadding=\"20\">");
            builder.AppendFormat(" <tr>");

            builder.AppendFormat("<td id=\"header\" align=\"center\" style=\"padding-top: 0;\">");
            builder.AppendFormat("  <h3 style=\"font-weight: normal; margin-top: 5px; color: #9a0f15; text-shadow: 1px 1px 3px white; font-size: 1.3em\">Please download and log in to start posting!</h3>");

            builder.AppendFormat(" <p style=\"background: white; border: 1px solid #c3b8af; padding: 10px; text-align: left;\">");
            builder.AppendFormat("  <span style=\"font-weight: bold; font-size: 1.2em;\">Registration Successful!</span><br />");

            builder.AppendFormat(" If you have any other questions please consult our <a href=\"http://cldms.com//faq\">FAQ's</a> or send us an email at cldms@vincontrol.com. Other information can be found below:<br>");
            builder.AppendFormat("  <br>- Username :  " + customer.CustomerEmail);
            builder.AppendFormat("  <br>- Temporary Password : " + customer.TemporaryPassword);
            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//pricing\">Pricing</a>");

            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//faq\">FAQ</a>");
            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//download\">Installation Instructions</a>");

            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//tos\">Terms of Service</a>");
            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//privacy\">Privacy Policy</a>");

            builder.AppendFormat("  <br>- <a href=\"http://vincontrol.com\">VinControl</a>");
            builder.AppendFormat("     <br /><br />");

            builder.AppendFormat(" </p>");
            builder.AppendFormat(" </td>");


            builder.AppendFormat(" </tr>");
            builder.AppendFormat(" </table>");

            builder.AppendFormat(" </td>");
            builder.AppendFormat("  </tr>");

            builder.AppendFormat("<tr>");
            builder.AppendFormat("<td id=\"bottom\" align=\"center\" bgcolor=\"#191008\" style=\"color: #9a0f15;\">");

            builder.AppendFormat(" <a href=\"http://cldms.com/\" style=\"color: white;\">postcl.com</a> | <a href=\"http://cldms.com//about\" style=\"color: white;\">about us</a> | <a href=\"http://cldms.com//home\" style=\"color: white;\">signup</a> | <a href=\"\" style=\"color: white;\">View in browser</a>");
            builder.AppendFormat("</td>");

            builder.AppendFormat(" </tr>");
            builder.AppendFormat(" </table>");

            builder.AppendFormat("</body>");
            builder.AppendFormat("</html>");

            return builder.ToString();
        }

        private static string CreateConfirmationBodyEmailForWelcomeCustomer(PostClCustomer customer)
        {
            var builder = new StringBuilder();

            builder.AppendFormat("    <html>");
            builder.AppendFormat("<head>");

            builder.AppendFormat("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            builder.AppendFormat("<title>PostCL</title>");

            builder.AppendFormat(" <style type=\"text/css\">");
            builder.AppendFormat(" </style>");

            builder.AppendFormat("</head>");
            builder.AppendFormat("<body>");

            builder.AppendFormat("<table style=\"font-family: Arial, sans-serif; font-size: .9em; border: 1px solid black; padding-top: 15px;\" cellpadding=\"20\" width=\"600\" cellspacing=\"0\" bgcolor=\"#dad0c7\">");
            builder.AppendFormat("  <tr>");

            builder.AppendFormat("<td style=\"padding-bottom: 0; padding-top: 0;\">");
            builder.AppendFormat(" <table cellpadding=\"20\" style=\"font-family: Arial, sans-serif;\">");

            builder.AppendFormat("<tr>");
            builder.AppendFormat("  <td width=\"260\"><img src=\"http://cldms.com//Content/img/logo.png\" width=\"260\"></td><td style=\"padding: 0;\"><h1 style=\"font-size: 2.5em; margin-top: 25px; color: #0f0a06; text-shadow: 2px 2px 3px white; font-style: italic; font-weight: bold; margin-bottom: 0;\">Welcome!</h1></td>");

            builder.AppendFormat(" </tr>");
            builder.AppendFormat(" </table>");

            builder.AppendFormat(" </td>");
            builder.AppendFormat(" </tr>");

            builder.AppendFormat(" <tr id=\"main-content\" align=\"center\">");
            builder.AppendFormat(" <td style=\"padding-top: 0;\">");

            builder.AppendFormat("      <table style=\"font-family: Arial, sans-serif; font-size: .9em;\" width=\"560\" cellpadding=\"20\">");
            builder.AppendFormat(" <tr>");

            builder.AppendFormat("<td id=\"header\" align=\"center\" style=\"padding-top: 0;\">");
            builder.AppendFormat("  <h3 style=\"font-weight: normal; margin-top: 5px; color: #9a0f15; text-shadow: 1px 1px 3px white; font-size: 1.3em\">Please download and log in to start posting!</h3>");

            builder.AppendFormat(" <p style=\"background: white; border: 1px solid #c3b8af; padding: 10px; text-align: left;\">");
            builder.AppendFormat("  <span style=\"font-weight: bold; font-size: 1.2em;\">Registration Successful!</span><br />");

            builder.AppendFormat(" If you have any other questions please consult our <a href=\"http://cldms.com//faq\">FAQ's</a> or send us an email at cldms@vincontrol.com. Other information can be found below:<br>");
            builder.AppendFormat("  <br>- Username :  " + customer.CustomerEmail);
            builder.AppendFormat("  <br>- Temporary Password : " + customer.TemporaryPassword);
            builder.AppendFormat("  <br>- Customer Name: " + customer.FirstName + " " +customer.LastName);
            builder.AppendFormat("  <br>- Customer Email: " + customer.CustomerEmail);
            builder.AppendFormat("  <br>- Dealer Name: " + customer.DealerName);
            builder.AppendFormat("  <br>- Dealer Address: " + customer.DealerStreetAddress);
            builder.AppendFormat("  <br>- Dealer City: " + customer.DealerCity);
            builder.AppendFormat("  <br>- Dealer Zipcode: " + customer.DealerZipCode);
            
            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//pricing\">Pricing</a>");

            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//faq\">FAQ</a>");
            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//download\">Installation Instructions</a>");

            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//tos\">Terms of Service</a>");
            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//privacy\">Privacy Policy</a>");

            builder.AppendFormat("  <br>- <a href=\"http://vincontrol.com\">VinControl</a>");
            builder.AppendFormat("     <br /><br />");

            builder.AppendFormat(" </p>");
            builder.AppendFormat(" </td>");


            builder.AppendFormat(" </tr>");
            builder.AppendFormat(" </table>");

            builder.AppendFormat(" </td>");
            builder.AppendFormat("  </tr>");

            builder.AppendFormat("<tr>");
            builder.AppendFormat("<td id=\"bottom\" align=\"center\" bgcolor=\"#191008\" style=\"color: #9a0f15;\">");

            builder.AppendFormat(" <a href=\"http://cldms.com/\" style=\"color: white;\">postcl.com</a> | <a href=\"http://cldms.com//about\" style=\"color: white;\">about us</a> | <a href=\"http://cldms.com//home\" style=\"color: white;\">signup</a> | <a href=\"\" style=\"color: white;\">View in browser</a>");
            builder.AppendFormat("</td>");

            builder.AppendFormat(" </tr>");
            builder.AppendFormat(" </table>");

            builder.AppendFormat("</body>");
            builder.AppendFormat("</html>");

            return builder.ToString();
        }


        private static string CreateConfirmationBodyEmailForCreditCardPayment(BillingCustomer customer)
        {
            var builder = new StringBuilder();

            builder.AppendFormat("    <html>");
            builder.AppendFormat("<head>");

            builder.AppendFormat("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            builder.AppendFormat("<title>PostCL</title>");

            builder.AppendFormat(" <style type=\"text/css\">");
            builder.AppendFormat(" </style>");

            builder.AppendFormat("</head>");
            builder.AppendFormat("<body>");

            builder.AppendFormat("<table style=\"font-family: Arial, sans-serif; font-size: .9em; border: 1px solid black; padding-top: 15px;\" cellpadding=\"20\" width=\"600\" cellspacing=\"0\" bgcolor=\"#dad0c7\">");
            builder.AppendFormat("  <tr>");

            builder.AppendFormat("<td style=\"padding-bottom: 0; padding-top: 0;\">");
            builder.AppendFormat(" <table cellpadding=\"20\" style=\"font-family: Arial, sans-serif;\">");

            builder.AppendFormat("<tr>");
            builder.AppendFormat("  <td width=\"260\"><img src=\"http://cldms.com//Content/img/logo.png\" width=\"150\"></td><td style=\"padding: 0;\"><h1 style=\"font-size: 2.5em; margin-top: 25px; color: #0f0a06; text-shadow: 2px 2px 3px white; font-style: italic; font-weight: bold; margin-bottom: 0;\">Payment Confirmation!</h1></td>");

            builder.AppendFormat(" </tr>");
            builder.AppendFormat(" </table>");

            builder.AppendFormat(" </td>");
            builder.AppendFormat(" </tr>");

            builder.AppendFormat(" <tr id=\"main-content\" align=\"center\">");
            builder.AppendFormat(" <td style=\"padding-top: 0;\">");

            builder.AppendFormat("      <table style=\"font-family: Arial, sans-serif; font-size: .9em;\" width=\"560\" cellpadding=\"20\">");
            builder.AppendFormat(" <tr>");

            builder.AppendFormat("<td id=\"header\" align=\"center\" style=\"padding-top: 0;\">");
            
            builder.AppendFormat(" <p style=\"background: white; border: 1px solid #c3b8af; padding: 10px; text-align: left;\">");
            builder.AppendFormat("  <span style=\"font-weight: bold; font-size: 1.2em;\">Payment Successful!</span><br />");

            builder.AppendFormat("  <br>- Customer Name: " + customer.FirstName + " " + customer.LastName);
            builder.AppendFormat("  <br>- Customer Email: " + customer.CustomerEmail);
            builder.AppendFormat("  <br>- Selected Package: " + customer.SelectedPackage.QuickBookPackageName);
            builder.AppendFormat("  <br>- Dealer Name: " + customer.DealerName);
            builder.AppendFormat("  <br>- Dealer Address: " + customer.DealerStreetAddress);
            builder.AppendFormat("  <br>- Dealer City: " + customer.DealerCity);
            builder.AppendFormat("  <br>- Dealer Zipcode: " + customer.DealerZipCode);
            builder.AppendFormat("  <br>- Credit Card Number:  " + CommonHelper.MaskInput(customer.CardNumber, 4));
            builder.AppendFormat("  <br>- Amount : " + QuickBookHelper.GetTotalCharge(customer).ToString("C"));

            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//pricing\">Pricing</a>");

            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//faq\">FAQ</a>");
            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//download\">Installation Instructions</a>");

            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//tos\">Terms of Service</a>");
            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//privacy\">Privacy Policy</a>");

            builder.AppendFormat("  <br>- <a href=\"http://vincontrol.com\">VinControl</a>");
            builder.AppendFormat("     <br /><br />");

            builder.AppendFormat(" </p>");
            builder.AppendFormat(" </td>");


            builder.AppendFormat(" </tr>");
            builder.AppendFormat(" </table>");

            builder.AppendFormat(" </td>");
            builder.AppendFormat("  </tr>");

            builder.AppendFormat("<tr>");
            builder.AppendFormat("<td id=\"bottom\" align=\"center\" bgcolor=\"#191008\" style=\"color: #9a0f15;\">");

            builder.AppendFormat(" <a href=\"http://cldms.com/\" style=\"color: white;\">postcl.com</a> | <a href=\"http://cldms.com//about\" style=\"color: white;\">about us</a> | <a href=\"http://cldms.com//home\" style=\"color: white;\">signup</a> | <a href=\"\" style=\"color: white;\">View in browser</a>");
            builder.AppendFormat("</td>");

            builder.AppendFormat(" </tr>");
            builder.AppendFormat(" </table>");

            builder.AppendFormat("</body>");
            builder.AppendFormat("</html>");

            return builder.ToString();
        }


        private static string CreateConfirmationBodyEmailForCustomerCreditCardPayment(BillingCustomer customer)
        {
            var builder = new StringBuilder();

            builder.AppendFormat("    <html>");
            builder.AppendFormat("<head>");

            builder.AppendFormat("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            builder.AppendFormat("<title>PostCL</title>");

            builder.AppendFormat(" <style type=\"text/css\">");
            builder.AppendFormat(" </style>");

            builder.AppendFormat("</head>");
            builder.AppendFormat("<body>");

            builder.AppendFormat("<table style=\"font-family: Arial, sans-serif; font-size: .9em; border: 1px solid black; padding-top: 15px;\" cellpadding=\"20\" width=\"600\" cellspacing=\"0\" bgcolor=\"#dad0c7\">");
            builder.AppendFormat("  <tr>");

            builder.AppendFormat("<td style=\"padding-bottom: 0; padding-top: 0;\">");
            builder.AppendFormat(" <table cellpadding=\"20\" style=\"font-family: Arial, sans-serif;\">");

            builder.AppendFormat("<tr>");
            builder.AppendFormat("  <td width=\"260\"><img src=\"http://cldms.com//Content/img/logo.png\" width=\"150\"></td><td style=\"padding: 0;\"><h1 style=\"font-size: 2.5em; margin-top: 25px; color: #0f0a06; text-shadow: 2px 2px 3px white; font-style: italic; font-weight: bold; margin-bottom: 0;\">Payment Confirmation!</h1></td>");

            builder.AppendFormat(" </tr>");
            builder.AppendFormat(" </table>");

            builder.AppendFormat(" </td>");
            builder.AppendFormat(" </tr>");

            builder.AppendFormat(" <tr id=\"main-content\" align=\"center\">");
            builder.AppendFormat(" <td style=\"padding-top: 0;\">");

            builder.AppendFormat("      <table style=\"font-family: Arial, sans-serif; font-size: .9em;\" width=\"560\" cellpadding=\"20\">");
            builder.AppendFormat(" <tr>");

            builder.AppendFormat("<td id=\"header\" align=\"center\" style=\"padding-top: 0;\">");
            builder.AppendFormat("  <h3 style=\"font-weight: normal; margin-top: 5px; color: #9a0f15; text-shadow: 1px 1px 3px white; font-size: 1.3em\">Thank you for your online payment to POSTCL. This e-mail is confirmation of the transaction.!</h3>");

            builder.AppendFormat(" <p style=\"background: white; border: 1px solid #c3b8af; padding: 10px; text-align: left;\">");
            builder.AppendFormat("  <span style=\"font-weight: bold; font-size: 1.2em;\">Payment Successful!</span><br />");

            builder.AppendFormat("  <br>- Customer Name: " + customer.FirstName + " " + customer.LastName);
           builder.AppendFormat("   <br>-  Credit Card Number:  " +CommonHelper.MaskInput(customer.CardNumber,4));
           builder.AppendFormat("  <br>- Amount : " + QuickBookHelper.GetTotalCharge(customer).ToString("C"));

            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//pricing\">Pricing</a>");

            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//faq\">FAQ</a>");
            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//download\">Installation Instructions</a>");

            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//tos\">Terms of Service</a>");
            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//privacy\">Privacy Policy</a>");

            builder.AppendFormat("  <br>- <a href=\"http://vincontrol.com\">VinControl</a>");
            builder.AppendFormat("     <br /><br />");

            builder.AppendFormat(" </p>");
            builder.AppendFormat(" </td>");


            builder.AppendFormat(" </tr>");
            builder.AppendFormat(" </table>");

            builder.AppendFormat(" </td>");
            builder.AppendFormat("  </tr>");

            builder.AppendFormat("<tr>");
            builder.AppendFormat("<td id=\"bottom\" align=\"center\" bgcolor=\"#191008\" style=\"color: #9a0f15;\">");

            builder.AppendFormat(" <a href=\"http://cldms.com/\" style=\"color: white;\">postcl.com</a> | <a href=\"http://cldms.com//about\" style=\"color: white;\">about us</a> | <a href=\"http://cldms.com//home\" style=\"color: white;\">signup</a> | <a href=\"\" style=\"color: white;\">View in browser</a>");
            builder.AppendFormat("</td>");

            builder.AppendFormat(" </tr>");
            builder.AppendFormat(" </table>");

            builder.AppendFormat("</body>");
            builder.AppendFormat("</html>");

            return builder.ToString();
        }


        private static string CreateConfirmationBodyEmailForDataFeedSetup(PostClDataFeed postClDataFeed)
        {
            var builder = new StringBuilder();

            builder.AppendFormat("    <html>");
            builder.AppendFormat("<head>");

            builder.AppendFormat("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            builder.AppendFormat("<title>PostCL</title>");

            builder.AppendFormat(" <style type=\"text/css\">");
            builder.AppendFormat(" </style>");

            builder.AppendFormat("</head>");
            builder.AppendFormat("<body>");

            builder.AppendFormat("<table style=\"font-family: Arial, sans-serif; font-size: .9em; border: 1px solid black; padding-top: 15px;\" cellpadding=\"20\" width=\"600\" cellspacing=\"0\" bgcolor=\"#dad0c7\">");
            builder.AppendFormat("  <tr>");

            builder.AppendFormat("<td style=\"padding-bottom: 0; padding-top: 0;\">");
            builder.AppendFormat(" <table cellpadding=\"20\" style=\"font-family: Arial, sans-serif;\">");

            builder.AppendFormat("<tr>");
            builder.AppendFormat("  <td width=\"260\"><img src=\"http://cldms.com//Content/img/logo.png\" width=\"150\"></td><td style=\"padding: 0;\"><h1 style=\"font-size: 2.5em; margin-top: 25px; color: #0f0a06; text-shadow: 2px 2px 3px white; font-style: italic; font-weight: bold; margin-bottom: 0;\">Pending Datafeed Setup!</h1></td>");

            builder.AppendFormat(" </tr>");
            builder.AppendFormat(" </table>");

            builder.AppendFormat(" </td>");
            builder.AppendFormat(" </tr>");

            builder.AppendFormat(" <tr id=\"main-content\" align=\"center\">");
            builder.AppendFormat(" <td style=\"padding-top: 0;\">");

            builder.AppendFormat("      <table style=\"font-family: Arial, sans-serif; font-size: .9em;\" width=\"560\" cellpadding=\"20\">");
            builder.AppendFormat(" <tr>");

            builder.AppendFormat("<td id=\"header\" align=\"center\" style=\"padding-top: 0;\">");

            builder.AppendFormat(" <p style=\"background: white; border: 1px solid #c3b8af; padding: 10px; text-align: left;\">");
            builder.AppendFormat("  <span style=\"font-weight: bold; font-size: 1.2em;\">Pending datafeed!</span><br />");

    
            builder.AppendFormat("  <br>- Dealer Name: " + GlobalVar.CurrentDealer.DealershipName);
            builder.AppendFormat("  <br>- Dealer Address: " + GlobalVar.CurrentDealer.StreetAddress);
            builder.AppendFormat("  <br>- Dealer City: " + GlobalVar.CurrentDealer.City);
            builder.AppendFormat("  <br>- Dealer Zipcode: " + GlobalVar.CurrentDealer.ZipCode);
            builder.AppendFormat("  <br>- Datafeed Vendor: " + postClDataFeed.VendorName);
            builder.AppendFormat("  <br>- Vendor Email: " + postClDataFeed.VendorEmail);
            builder.AppendFormat("  <br>- Vendor Phone: " + postClDataFeed.VendorPhone);
            builder.AppendFormat("  <br>- Custom Message: " + postClDataFeed.CustomMessage);
            builder.AppendFormat("  <br>- Contact person at dealership: " + postClDataFeed.YourName);
            
            

            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//pricing\">Pricing</a>");

            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//faq\">FAQ</a>");
            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//download\">Installation Instructions</a>");

            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//tos\">Terms of Service</a>");
            builder.AppendFormat("  <br>- <a href=\"http://cldms.com//privacy\">Privacy Policy</a>");

            builder.AppendFormat("  <br>- <a href=\"http://vincontrol.com\">VinControl</a>");
            builder.AppendFormat("     <br /><br />");

            builder.AppendFormat(" </p>");
            builder.AppendFormat(" </td>");


            builder.AppendFormat(" </tr>");
            builder.AppendFormat(" </table>");

            builder.AppendFormat(" </td>");
            builder.AppendFormat("  </tr>");

            builder.AppendFormat("<tr>");
            builder.AppendFormat("<td id=\"bottom\" align=\"center\" bgcolor=\"#191008\" style=\"color: #9a0f15;\">");

            builder.AppendFormat(" <a href=\"http://cldms.com/\" style=\"color: white;\">postcl.com</a> | <a href=\"http://cldms.com//about\" style=\"color: white;\">about us</a> | <a href=\"http://cldms.com//home\" style=\"color: white;\">signup</a> | <a href=\"\" style=\"color: white;\">View in browser</a>");
            builder.AppendFormat("</td>");

            builder.AppendFormat(" </tr>");
            builder.AppendFormat(" </table>");

            builder.AppendFormat("</body>");
            builder.AppendFormat("</html>");

            return builder.ToString();
        }


        private static string CreateConfirmationBodyEmailForDataFeedSetupForVendor(PostClDataFeed postClDataFeed)
        {
            var builder = new StringBuilder();

            builder.AppendFormat("<br> Hello, ");

            builder.AppendFormat("<br>");


            builder.AppendFormat("<br>  This is an email from PostCL on the behalf of " + postClDataFeed.YourName +
                                 " at " + GlobalVar.CurrentDealer.DealershipName + ", located at " +
                                 GlobalVar.CurrentDealer.StreetAddress + " " + GlobalVar.CurrentDealer.City + ", " +
                                 GlobalVar.CurrentDealer.State + " " + GlobalVar.CurrentDealer.ZipCode +
                                 ". They would like you to send their dealership’s inventory to PostCL. ");
            
            builder.AppendFormat("<br> Below is a pre-generated paragraph that they have digitally signed authorizing this export:");

            builder.AppendFormat("<br>  “I would like to send my dealership’s (" + GlobalVar.CurrentDealer.DealershipName + ") inventory to PostCL. A representative from PostCL will contact you shortly in order to help set up this export.");
         
            return builder.ToString();
        }

        #region Private Methods
        private static SmtpClient SmtpClient()
        {
            var client = new SmtpClient(ConfigHelper.SMTPServer, 587);
            var networkCredential = new NetworkCredential(ConfigHelper.DefaultFromEmail, ConfigHelper.TrackEmailPass);
            client.Credentials = networkCredential;
            //client.EnableSsl = true;
            return client;
        }

        private static MailMessage MailMessage(MailAddress toEmail, string subject, string body)
        {
            var fromEmail = new MailAddress(ConfigHelper.DefaultFromEmail, ConfigHelper.DisplayName);

            var message = new MailMessage(fromEmail, toEmail)
            {
                From = fromEmail,
                Body = body,
                IsBodyHtml = true,
                Subject = subject
            };
            
            return message;
        }
        #endregion
    }
}