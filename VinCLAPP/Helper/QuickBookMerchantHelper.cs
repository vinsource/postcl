using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Xml;

namespace VinCLAPP.Helper
{
    public class QuickBookMerchantHelper
    {
        //public static string CreateQuickBookXmlRequest(string amount, string creditCardNumber, int expirationMonth, int expirationYear, string nameOnCard, string addressInfo, string postalCode)
        //{

        //    var builder = new StringBuilder();

        //    builder.AppendLine("<?xml version=\"1.0\"?>");

        //    builder.AppendLine("<?qbmsxml version=\"4.5\"?>");

        //    builder.AppendLine("<QBMSXML>");

        //    builder.AppendLine("<SignonMsgsRq>");

        //    builder.AppendLine("<SignonDesktopRq>");

        //    builder.AppendLine("<ClientDateTime>" + String.Format("{0:s}", DateTime.Now) + "</ClientDateTime>");

        //    builder.AppendLine("<ApplicationLogin>vinclapp.vehicleinventorynetwork.com</ApplicationLogin>");

        //    builder.AppendLine("<ConnectionTicket>" + System.Configuration.ConfigurationManager.AppSettings["QuickBookConnectionTicket"].ToString(CultureInfo.InvariantCulture) + "</ConnectionTicket>");

        //    builder.AppendLine("<Language>English</Language>");

        //    builder.AppendLine("<AppID>711131140</AppID>");

        //    builder.AppendLine("<AppVer>1.0</AppVer>");

        //    builder.AppendLine("</SignonDesktopRq>");

        //    builder.AppendLine("</SignonMsgsRq>");

        //    builder.AppendLine("<QBMSXMLMsgsRq>");

        //    builder.AppendLine("<CustomerCreditCardChargeRq>");

        //    builder.AppendLine("<TransRequestID>" + GenerateTransID() + "</TransRequestID>");

        //    builder.AppendLine("<CreditCardNumber>" + creditCardNumber + "</CreditCardNumber>");

        //    builder.AppendLine("<ExpirationMonth>" + expirationMonth + "</ExpirationMonth>");

        //    builder.AppendLine("<ExpirationYear>" + expirationYear + "</ExpirationYear>");

        //    builder.AppendLine("<IsCardPresent>false</IsCardPresent>");

        //    builder.AppendLine("<Amount>" + amount + "</Amount>");

        //    builder.AppendLine("<NameOnCard>" + nameOnCard + "</NameOnCard>");

        //    builder.AppendLine("<CreditCardAddress>" + addressInfo + "</CreditCardAddress>");

        //    builder.AppendLine("<CreditCardPostalCode>" + postalCode + "</CreditCardPostalCode>");

        //    builder.AppendLine("<SalesTaxAmount>0.00</SalesTaxAmount>");

        //    builder.AppendLine("</CustomerCreditCardChargeRq>");

        //    builder.AppendLine("</QBMSXMLMsgsRq>");

        //    builder.AppendLine("</QBMSXML>");


        //    return builder.ToString();
        //}

        private static string GenerateTransID()
        {
            const string charsToUse = "1234567890";

            var random = new Random();

            string firstString = "";

            for (int i = 0; i < 10; i++)
                firstString += charsToUse[random.Next(charsToUse.Length)].ToString(CultureInfo.InvariantCulture);

            return "VCA" + firstString;
        }

        public static XmlDocument MakeApiCall(string requestXml, string apiServerUrl)
        {
            return send_request(apiServerUrl, requestXml);
        }

        public static XmlDocument send_request(string url, string requestXml)
        {
            var xmldomResponse = new XmlDocument();
            try
            {
                string strResponse = "";

                //Prepare the HTTP request
                var objRequest = (HttpWebRequest) WebRequest.Create(url);
                objRequest.Method = "POST";
                objRequest.ContentLength = requestXml.Length;
                objRequest.ContentType = "application/x-qbmsxml";


                // Send the Request
                StreamWriter myWriter = null;
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(requestXml);
                myWriter.Close();

                var objResponse = (HttpWebResponse) objRequest.GetResponse();
                var sr = new StreamReader(objResponse.GetResponseStream());

                strResponse = sr.ReadToEnd();
                if (strResponse != "")
                {
                    xmldomResponse.LoadXml(strResponse);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return xmldomResponse;
        }
    }
}