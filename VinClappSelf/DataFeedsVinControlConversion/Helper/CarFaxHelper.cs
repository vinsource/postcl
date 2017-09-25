using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using HtmlAgilityPack;

namespace DataFeedsVinControlConversion.Helper
{
    public sealed class CarFaxHelper
    {
        private static string createCarFaxXMLRequest(string Vin)
        {

            var builder = new StringBuilder();
            builder.AppendLine("<carfax-request>");
            builder.AppendLine("<vin>" + Vin + "</vin>");
            builder.AppendLine("<product-data-id>" + ConfigurationManager.AppSettings["CaxFaxProductDataId"] + "</product-data-id>");
            builder.AppendLine("<username>" + "C471803" + "</username>");
            builder.AppendLine("<password>" + "70911" + "</password>");
            builder.AppendLine("<purchase>Y</purchase>");
            builder.AppendLine("<report-type>VHR</report-type>");
            builder.AppendLine("<online-listing>Y</online-listing>");
            builder.AppendLine("</carfax-request>");

            return builder.ToString();
        }

        public static XmlDocument MakeAPICall(string Vin)
        {
            var requestBody = createCarFaxXMLRequest(Vin);

            var xmlDoc = new XmlDocument();

            var apiServerURL = ConfigurationManager.AppSettings["CaxFaxServerURL"];

            var request = (HttpWebRequest) WebRequest.Create(apiServerURL);

            request.Method = "POST";

            request.ContentType = "text/xml";

            var encoding = new UTF8Encoding();

            var dataLen = encoding.GetByteCount(requestBody);

            var utf8Bytes = new byte[dataLen];

            Encoding.UTF8.GetBytes(requestBody, 0, requestBody.Length, utf8Bytes, 0);

            Stream str = null;
            try
            {
                //Set the request Stream
                str = request.GetRequestStream();
                //Write the request to the Request Steam
                str.Write(utf8Bytes, 0, utf8Bytes.Length);
                str.Close();
                //Get response into stream
                var response = request.GetResponse();
                str = response.GetResponseStream();

                // Get Response into String
                var sr = new StreamReader(str);
                xmlDoc.LoadXml(sr.ReadToEnd());
                sr.Close();
                str.Close();
            }
            catch (Exception Ex)
            {
                //System.Web.HttpContext.Current.Response.Write("Errors=" + Ex.Message);
            }
            return xmlDoc;
        }



        public static int RunCarFaxAndSaveOwner(string Vin)
        {

            var doc = MakeAPICall(Vin);

            var root = doc["carfax-response"];

            int numberOwner = 0;

            if (!String.IsNullOrEmpty(root.InnerText))
            {
                if (root["vin-info"] != null)
                {
                    try
                    {
                        bool flag =
                     Int32.TryParse(
                         root["vin-info"]["number-of-owners-indicator"]["number-of-owners-indicator-value"].
                             InnerText, out numberOwner);
                    }
                    catch (Exception)
                    {

                        return numberOwner;
                    }

                 

                }




            }
            return numberOwner;






        }

        public static string GetVinNumberFromCarFaxLink(string caxFaxURL)
        {

            /* TYPELINK =1. SHOW ME CAR FAX. 
             * TYPELINK =2. CARFAX HELPS. 
             */

            var htmlWeb = new HtmlWeb();

            var htmlDoc = new HtmlDocument();

            try
            {

                htmlDoc = htmlWeb.Load(caxFaxURL);

                HtmlNode selectNodes_CarVIN = htmlDoc.DocumentNode.SelectSingleNode("//span[@class='vin']");

                if(selectNodes_CarVIN==null)
                    selectNodes_CarVIN = htmlDoc.DocumentNode.SelectSingleNode("//dd[@id='vinNumber']");

                ////BUG AND ERROR HERE. CHECK LATER
                if (selectNodes_CarVIN != null)
                    return selectNodes_CarVIN.InnerText.Trim();

            }
            catch (Exception ex)
            {
                //ServiceLog.ErrorLog("EXCEPTION IN GETVINNUMBER FUNCTION AT LINK IS " + caxFaxURL + ".....DETAILS" + ex.InnerException);

            }

            return "";

        }



    }
}
