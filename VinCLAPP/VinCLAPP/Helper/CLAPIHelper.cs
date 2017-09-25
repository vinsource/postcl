using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using Vinclapp.Craigslist;
using VinCLAPP.Model;

namespace VinCLAPP.Helper
{
    public class ClapiHelper
    {
        private static string CreateXmlCraiglistRequest(IEnumerable<VehicleInfo> vehicleList)
        {
            var webRequest = new WebClient();

            var builder = new StringBuilder();

            builder.AppendLine("<?xml version=\"1.0\"?>");

            builder.AppendLine("<rdf:RDF xmlns=\"http://purl.org/rss/1.0/\" xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\" xmlns:cl=\"http://www.craigslist.org/about/cl-bulk-ns/1.0\">");

            builder.AppendLine("<channel>");

            builder.AppendLine("<items>");

            foreach (var vehicle in vehicleList)
            {
                builder.AppendLine("<rdf:li rdf:resource=\"" + vehicle.ListingId + "_" + vehicle.PostingCityId + "\"/>");
            }
            
            builder.AppendLine("</items>");

            //builder.AppendLine("<cl:auth username=\"desmond@fullertonautosquare.com\" password=\"bloody10\" accountID=\"43613\"/>");

            builder.AppendLine("<cl:auth username=\"" + GlobalVar.CurrentAccount.APIUsername + "\" password=\"" + GlobalVar.CurrentAccount.APIPassword + "\" accountID=\"" + GlobalVar.CurrentAccount.APIAccountId + "\"/>");

            builder.AppendLine("</channel>");

            foreach (var vehicle in vehicleList)
            {
                builder.AppendLine("<item rdf:about=\"" + vehicle.ListingId + "_" + vehicle.PostingCityId + "\">");

                builder.AppendLine("<cl:category>ctd</cl:category>");

                var selectedCity =
                    GlobalVar.CurrentDealer.CityList.FirstOrDefault(x => x.CityID == vehicle.PostingCityId);

                if (selectedCity != null)
                {
                    builder.AppendLine("<cl:area>" + selectedCity.AreaAbbr + "</cl:area>");

                    if (selectedCity.SubCity && !String.IsNullOrEmpty(selectedCity.SubAbbr))
                    {
                        builder.AppendLine("<cl:subarea>" + selectedCity.SubAbbr + "</cl:subarea>");
                    }
                }

                builder.AppendLine("<cl:price>" + vehicle.SalePrice + "</cl:price>");

                if (String.IsNullOrEmpty(GlobalVar.CurrentDealer.CityOveride))
                    builder.AppendLine("<cl:neighborhood>" + vehicle.City + "</cl:neighborhood>");
                else
                    builder.AppendLine("<cl:neighborhood>" + GlobalVar.CurrentDealer.CityOveride + "</cl:neighborhood>");

                if (String.IsNullOrEmpty(vehicle.CarImageUrl)) return null;

                string[] carImage = vehicle.CarImageUrl.Split(new[] { ",", "|" }, StringSplitOptions.RemoveEmptyEntries);
                var index = 1;

                foreach (var tmp in carImage)
                {
                    builder.AppendLine("<cl:image position=\"" + index + "\">" + DownloadImageToBase64(webRequest, tmp) + "</cl:image>");

                    index++;

                    if (index > 24)
                        break;

                }

                if (vehicle.Tranmission.Contains("Automatic"))
                {
                    builder.AppendLine("<cl:auto_basics auto_make_model=\"" + vehicle.Make + " " + vehicle.Model + "\" auto_miles =\"" + vehicle.Mileage + "\"  auto_trans_auto=\"1\" auto_trans_manual=\"0\" auto_vin=\"" + vehicle.Vin + "\"  auto_year =\"" + vehicle.ModelYear + "\"/>");
                }
                else
                {
                    builder.AppendLine("<cl:auto_basics auto_make_model=\"" + vehicle.Make + " " + vehicle.Model + "\" auto_miles =\"" + vehicle.Mileage + "\"  auto_trans_auto=\"0\" auto_trans_manual=\"1\" auto_vin=\"" + vehicle.Vin + "\" auto_year =\"" + vehicle.ModelYear + "\"/>");

                }

                builder.AppendLine("<cl:replyEmail privacy=\"C\">sales@fullertonautosquare.com</cl:replyEmail>");

                builder.AppendLine("<title>" + vehicle.ModelYear + " " + vehicle.Make + " " + vehicle.Model + " " + vehicle.Trim + "</title>");

                builder.AppendLine("<description><![CDATA[" + MakeBodyAdsDescription(vehicle) + "]]></description>");

                builder.AppendLine("</item>");
            }

           

            builder.AppendLine("</rdf:RDF>");

            return builder.ToString();
        }


        private static string CreateXmlCraiglistRequest(VehicleInfo vehicle)
        {
            var webRequest = new WebClient();

            var builder = new StringBuilder();

            builder.AppendLine("<?xml version=\"1.0\"?>");

            builder.AppendLine("<rdf:RDF xmlns=\"http://purl.org/rss/1.0/\" xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\" xmlns:cl=\"http://www.craigslist.org/about/cl-bulk-ns/1.0\">");

            builder.AppendLine("<channel>");

            builder.AppendLine("<items>");

            builder.AppendLine("<rdf:li rdf:resource=\"" + vehicle.ListingId + "_" + vehicle.PostingCityId + "\"/>");

            builder.AppendLine("</items>");

            //builder.AppendLine("<cl:auth username=\"desmond@fullertonautosquare.com\" password=\"bloody10\" accountID=\"43613\"/>");

            builder.AppendLine("<cl:auth username=\"" + GlobalVar.CurrentAccount.APIUsername + "\" password=\"" + GlobalVar.CurrentAccount.APIPassword + "\" accountID=\"" + GlobalVar.CurrentAccount.APIAccountId + "\"/>");

            builder.AppendLine("</channel>");

            builder.AppendLine("<item rdf:about=\"" + vehicle.ListingId + "_" + vehicle.PostingCityId + "\">");

            builder.AppendLine("<cl:category>ctd</cl:category>");

            var selectedCity =
                     GlobalVar.CurrentDealer.CityList.FirstOrDefault(x => x.CityID == vehicle.PostingCityId);

            if (selectedCity != null)
            {
                builder.AppendLine("<cl:area>" + selectedCity.AreaAbbr + "</cl:area>");

                if (selectedCity.SubCity && !String.IsNullOrEmpty(selectedCity.SubAbbr))
                {
                    builder.AppendLine("<cl:subarea>" + selectedCity.SubAbbr + "</cl:subarea>");
                }
            }

            builder.AppendLine("<cl:price>" + vehicle.SalePrice + "</cl:price>");

            if (String.IsNullOrEmpty(GlobalVar.CurrentDealer.CityOveride))
                builder.AppendLine("<cl:neighborhood>" + vehicle.City + "</cl:neighborhood>");
            else
                builder.AppendLine("<cl:neighborhood>" + GlobalVar.CurrentDealer.CityOveride + "</cl:neighborhood>");

            if(String.IsNullOrEmpty(vehicle.CarImageUrl)) return null;
            
            string[] carImage = vehicle.CarImageUrl.Split(new[] { ",", "|" },StringSplitOptions.RemoveEmptyEntries);
            var index = 1;

            foreach (var tmp in carImage)
            {
                builder.AppendLine("<cl:image position=\"" + index + "\">" + DownloadImageToBase64(webRequest, tmp) + "</cl:image>");

                index++;

                if (index > 24)
                    break;

            }

            if (vehicle.Tranmission.Contains("Automatic"))
            {
                builder.AppendLine("<cl:auto_basics auto_make_model=\"" + vehicle.Make + " " + vehicle.Model + "\" auto_miles =\"" + vehicle.Mileage + "\"  auto_trans_auto=\"1\" auto_trans_manual=\"0\" auto_vin=\"" + vehicle.Vin + "\"  auto_year =\"" + vehicle.ModelYear + "\"/>");
            }
            else
            {
                builder.AppendLine("<cl:auto_basics auto_make_model=\"" + vehicle.Make + " " + vehicle.Model + "\" auto_miles =\"" + vehicle.Mileage + "\"  auto_trans_auto=\"0\" auto_trans_manual=\"1\" auto_vin=\"" + vehicle.Vin + "\" auto_year =\"" + vehicle.ModelYear + "\"/>");

            }

            builder.AppendLine("<cl:replyEmail privacy=\"C\">sales@fullertonautosquare.com</cl:replyEmail>");

            builder.AppendLine("<title>" + vehicle.ModelYear + " " + vehicle.Make + " " + vehicle.Model + " " + vehicle.Trim + "</title>");

            builder.AppendLine("<description><![CDATA[" + MakeBodyAdsDescription(vehicle) + "]]></description>");
            
            builder.AppendLine("</item>");

            builder.AppendLine("</rdf:RDF>");

            return builder.ToString();
        }

        public static string DownloadImageToBase64(WebClient request, string webpath)
        {
            byte[] byteStream = request.DownloadData(webpath);

            string base64 = Convert.ToBase64String(byteStream);

            return base64;
        }

        public static string MakeBodyAdsDescription(VehicleInfo vehicle)
        {
            var builder = new StringBuilder();
            builder.Append("<b>Seller : </b>" + GlobalVar.CurrentDealer.DealershipName + "<br>");
            builder.Append("<b>" + GlobalVar.CurrentDealer.StreetAddress + " " +
                         GlobalVar.CurrentDealer.City + ", " + GlobalVar.CurrentDealer.State + " " +
                         GlobalVar.CurrentDealer.ZipCode + "</b><br>");
            builder.Append("<b>Contact Number : </b>" + GlobalVar.CurrentDealer.PhoneNumber + "<br>");
            builder.Append("<b>Please visit us at " + GlobalVar.CurrentDealer.WebSiteUrl + "</b><br><br>");
            builder.Append("<b>Vehicle Year : </b>" + vehicle.ModelYear + "<br>");
            builder.Append("<b>Vehicle Make : </b>" + vehicle.Make + "<br>");
            builder.Append("<b>Vehicle Model : </b>" + vehicle.Model + "<br>");
            if (!String.IsNullOrEmpty(vehicle.Trim))
                builder.Append("<b>Vehicle Trim : </b>" + vehicle.Trim + "<br>");
            if (!String.IsNullOrEmpty(vehicle.ExteriorColor))
                builder.Append("<b>Exterior Color : </b>" + vehicle.ExteriorColor + "<br>");
            if (!String.IsNullOrEmpty(vehicle.Tranmission))
                builder.Append("<b>Transmission  : </b>" + vehicle.Tranmission + "<br>");
            if (!String.IsNullOrEmpty(vehicle.StockNumber))
                builder.Append("<b>Stock : </b>" + vehicle.StockNumber + "<br>");
            if (!String.IsNullOrEmpty(vehicle.Vin))
                builder.Append("<b>Vin : </b>" + vehicle.Vin + "<br><br>");
            if (!String.IsNullOrEmpty(vehicle.Options))
                builder.Append("<b>Options : </b><br><br>" + vehicle.Options + "<br><br>");

            if (!String.IsNullOrEmpty(vehicle.Description))
                builder.Append("<b>Description : </b><br><br>" + vehicle.Description + "<br><br>");

            builder.Append("If you have any questions, please contact us by clicking the 'reply' button at the top of this ad.");
            builder.Append("<br>");
            if (!String.IsNullOrEmpty(GlobalVar.CurrentDealer.EndingSentence))
                builder.Append(GlobalVar.CurrentDealer.EndingSentence + "<br>");
            return builder.ToString();

        }
        
        public static XmlDocument MakeApiCall(VehicleInfo vehicle)
        {
            var requestBody = CreateXmlCraiglistRequest(vehicle);

            var xmlDoc = new XmlDocument();

            var apiServerURL = "https://post.craigslist.org/bulk-rss/validate";

            var request = (HttpWebRequest)WebRequest.Create(apiServerURL);

            request.ProtocolVersion = System.Net.HttpVersion.Version10;
            
            request.Method = "POST";

            request.ContentType = "application/x-www-form-urlencoded";

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
            catch (WebException ex)
            {
                throw new Exception(ex.Message);



            }
            return xmlDoc;
        }

        public static XmlDocument MakeApiCall(List<VehicleInfo> vehicleList)
        {
            var requestBody = CreateXmlCraiglistRequest(vehicleList);

            var xmlDoc = new XmlDocument();

            var apiServerURL = "https://post.craigslist.org/bulk-rss/validate";

            var request = (HttpWebRequest)WebRequest.Create(apiServerURL);

            request.ProtocolVersion = System.Net.HttpVersion.Version10;

            request.Method = "POST";

            request.ContentType = "application/x-www-form-urlencoded";

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
            catch (WebException ex)
            {
                throw new Exception(ex.Message);



            }
            return xmlDoc;
        }

        public static XmlDocument MakeApiCall(string requestBody)
        {
            var xmlDoc = new XmlDocument();

            var apiServerURL = "https://post.craigslist.org/bulk-rss/post";

            //var requestHelper = new WebRequestHelper();
            //var request = requestHelper.GetRequest("POST", "application/x-www-form-urlencoded", "https://post.craigslist.org/bulk-rss/post", requestBody);
            //xmlDoc.LoadXml(requestHelper.UnPackResponse(request.GetResponse()));
            
            //HttpWebRequest request = null;
            //Uri uri = new Uri("https://post.craigslist.org/bulk-rss/post");
            //request = (HttpWebRequest)WebRequest.Create(uri);
            //request.ProtocolVersion = System.Net.HttpVersion.Version10;
            //request.KeepAlive = true;
            //request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = requestBody.Length;
            //var streamWriter = new StreamWriter(request.GetRequestStream());
            //streamWriter.Write(requestBody);
            //streamWriter.Close();

            //// Get the response.
            //using (var response = (HttpWebResponse)request.GetResponse())
            //{
            //    // Have some cookies.
            //    var status = (int)response.StatusCode;
            //    // Read the response
            //    var streamReader = new StreamReader(response.GetResponseStream());
            //    var result = streamReader.ReadToEnd();
            //    streamReader.Close();
            //}
            int timeout = 200000;
            var request = (HttpWebRequest)WebRequest.Create(apiServerURL);

            request.ProtocolVersion = System.Net.HttpVersion.Version10;

            request.Method = "POST";

            request.ContentType = "application/x-www-form-urlencoded";

            request.Accept = "application/xml";

            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 1.0.3705; .NET CLR 1.1.4322; Media Center PC 4.0)";

            request.KeepAlive = true;

            //request.AllowAutoRedirect = false;

            request.Timeout = timeout;

            request.ReadWriteTimeout = timeout;

            //request.ServicePoint.ConnectionLimit = 1;

            //request.ServicePoint.UseNagleAlgorithm = true;
            request.ServicePoint.Expect100Continue = true;
            request.ServicePoint.MaxIdleTime = timeout;
            //request.ServicePoint.ConnectionLeaseTimeout = -1;
            ServicePointManager.CheckCertificateRevocationList = true;
            ServicePointManager.DefaultConnectionLimit = ServicePointManager.DefaultPersistentConnectionLimit;

            var encoding = new UTF8Encoding();

            var dataLen = encoding.GetByteCount(requestBody);

            //request.ContentLength = dataLen;

            var utf8Bytes = new byte[dataLen];

            Encoding.UTF8.GetBytes(requestBody, 0, requestBody.Length, utf8Bytes, 0);
            
            try
            {
                //Set the request Stream
                using (Stream str = request.GetRequestStream())
                {
                    //Write the request to the Request Steam
                    str.Write(utf8Bytes, 0, utf8Bytes.Length);
                    str.Close();
                    //Get response into stream
                    using (var response = request.GetResponse())
                    {
                        var substr = response.GetResponseStream();

                        // Get Response into String
                        using (var sr = new StreamReader(substr))
                        {
                            xmlDoc.LoadXml(sr.ReadToEnd());
                            sr.Close();
                        }

                        substr.Close();
                        str.Close();
                    }

                }


            }
            catch (WebException ex)
            {
                //throw;
                return null;
            }

            return xmlDoc;
        }
    }
}
