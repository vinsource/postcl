using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Data;
using System.IO;
using DataFeedsVinControlConversion.Helper;
using System.Collections;
using DataFeedsVinControlConversion.com.chrome.platform.AutomotiveDescriptionService6;
using HtmlAgilityPack;

namespace DataFeedsVinControlConversion
{
    public sealed class CommonHelper
    {

        const string SPACE = " ";
        private const double RADIO = 3958.75587; // Mean radius of Earth in Miles
        public CommonHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string RemoveSpecialCharactersForMSRP(string input)
        {
            if (!String.IsNullOrEmpty(input))
            {

                if (input.Contains("."))
                    input = input.Substring(0, input.IndexOf("."));

                return Regex.Replace(input, "[^a-zA-Z0-9]+", "", RegexOptions.Compiled);
            } return "0";


        }


        public static string getDriveXMLFileFromVehicleInfo(VehicleInformation vehicleInformation)
        {
            GenericEquipment[] list_genericEquip = vehicleInformation.genericEquipment;

            string drive = "Any Drive";

            foreach (GenericEquipment ge in list_genericEquip)
            {
                if (ge.categoryId == 1040 || ge.categoryId == 1041 || ge.categoryId == 1042 || ge.categoryId == 1043)
                {
                    drive = ge.categoryId.ToString();
                    break;
                }

            }
            XmlNode driveNode = XMLHelper.selectOneElement("Drive", @"C:\WheelDrive.xml", "Name=" + drive);

            if (driveNode != null)

                return driveNode.Attributes["Value"].Value;

            return "";

        }

     


        public static int DistanceBetweenPlaces(double sLatitude, double sLongitude, double eLatitude,
                              double eLongitude)
        {
            var sLatitudeRadians = sLatitude * (Math.PI / 180.0);
            var sLongitudeRadians = sLongitude * (Math.PI / 180.0);
            var eLatitudeRadians = eLatitude * (Math.PI / 180.0);
            var eLongitudeRadians = eLongitude * (Math.PI / 180.0);

            var dLongitude = eLongitudeRadians - sLongitudeRadians;
            var dLatitude = eLatitudeRadians - sLatitudeRadians;

            var result1 = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
                          Math.Cos(sLatitudeRadians) * Math.Cos(eLatitudeRadians) *
                          Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);

            // Using 3956 as the number of miles around the earth
            var result2 = 3956.0 * 2.0 *
                          Math.Atan2(Math.Sqrt(result1), Math.Sqrt(1.0 - result1));

            return Convert.ToInt32(result2);
        }


        private static double convertToRadians(double val)
        {

            return val * Math.PI / 180;
        }


        public static int getNumberOfPageToRun(string stringTotalResult, out int totalresultReturn)
        {

            string pureNumber = "";

            int totalResult = 0;

            int resultReturn = 0;

            totalresultReturn = 0;

            if (stringTotalResult.Contains(","))
            {
                string[] splitResult = stringTotalResult.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string tmp in splitResult)
                {
                    if (!tmp.Equals(","))
                        pureNumber += tmp;
                }

            }
            else
                pureNumber = stringTotalResult;


            bool flag = int.TryParse(pureNumber, out totalResult);

            if (flag)
            {
                if (totalResult == 0)
                    resultReturn = totalResult;
                else
                {
                    resultReturn = totalResult / 25 + 1;
                }
            }

            return resultReturn;
        }


        public static List<VinControlVehicle> getOnlineVehicleList()
        {
            string basicURL = "http://www.extremecarstrucks.com/view-inventory/";

            var htmlWeb = new HtmlWeb();

            var htmlDoc = new HtmlAgilityPack.HtmlDocument();

            htmlWeb.AutoDetectEncoding = true;

            var list = new List<VinControlVehicle>();

            try
            {
                htmlDoc = htmlWeb.Load(basicURL);

                HtmlAgilityPack.HtmlNode selectNode_TotalResult = htmlDoc.DocumentNode.SelectSingleNode("//span[@class='accent-color1']");

                int totalResultReturn = 0;

                int maxPage = getNumberOfPageToRun(selectNode_TotalResult.InnerText, out totalResultReturn);

                Hashtable hash = new Hashtable();

                for (int index = 1; index <= maxPage; index++)
                {
                    if (index > 1)
                        htmlDoc = htmlWeb.Load(basicURL + "/page-" + index);

                    HtmlAgilityPack.HtmlNodeCollection selectImageNode = htmlDoc.DocumentNode.SelectNodes("//td[@class='inventory-photo']");

                    HtmlAgilityPack.HtmlNodeCollection selectInfoNode = htmlDoc.DocumentNode.SelectNodes("//td[@class='inventory-details']");

                    if (selectImageNode != null && selectInfoNode != null)
                    {

                        for (int i = 0; i < selectImageNode.Count; i++)
                        {

                            var car = new VinControlVehicle();

                            HtmlNode linkNode = selectImageNode[i].SelectSingleNode(".//a");

                            HtmlNode stockNode = selectInfoNode[i].SelectSingleNode(".//td[@style='vertical-align: top; width: 40%; padding-left: 10px;']//div");

                            if (linkNode != null && stockNode != null)
                            {

                                string tmp = stockNode.InnerText.Trim().Replace(Environment.NewLine, "");

                                if (!String.IsNullOrEmpty(tmp))
                                {

                                    tmp = tmp.Replace("&nbsp;", "");

                                    string stockNumber = tmp.Substring(tmp.IndexOf(":") + 1).Trim();

                                    var htmlSubDoc = new HtmlDocument();

                                    htmlSubDoc = htmlWeb.Load(linkNode.Attributes["href"].Value);

                                    var selectSubImageNode =
                                        htmlSubDoc.DocumentNode.SelectSingleNode("//div[@class='slider-viewport']");

                                    var selectMonthlyPaymentNode =
                                        htmlSubDoc.DocumentNode.SelectSingleNode("//div[@class='accent-color1']");



                                    if (selectSubImageNode != null && selectMonthlyPaymentNode != null)
                                    {

                                        var subimageNode = selectSubImageNode.SelectNodes(".//a");

                                        if (subimageNode != null)
                                        {

                                            string imageURL = "";

                                            car.SalePrice =
                                                selectMonthlyPaymentNode.InnerText.Substring(
                                                    selectMonthlyPaymentNode.InnerText.LastIndexOf('.') + 1);


                                            foreach (var thnode in subimageNode)
                                            {
                                                if (!String.IsNullOrEmpty(thnode.Attributes["href"].Value))

                                                    imageURL += thnode.Attributes["href"].Value + ",";
                                            }


                                            if (!String.IsNullOrEmpty(imageURL))
                                                imageURL = imageURL.Substring(0, imageURL.Length - 1);

                                            car.StockNumber = stockNumber;

                                            car.CarImageUrl = imageURL;

                                            list.Add(car);
                                        }

                                    }

                                }
                            }





                        }
                    }



                }

                return list;



            }
            catch (Exception ex)
            {
                //return null;
                throw new Exception("*******EXCEPTION IN GETLISTVEHICLEINFORMATIONONLINE IN WESTCOAST FUNCTION AT LINK IS********************" + basicURL + "**************DETAILS" + ex.InnerException + "AND MESSAGE IS " + ex.Message);

            }

        }


        public static XmlDocument loadXMLDocument()
        {
            try
            {
                XmlTextReader reader = new XmlTextReader("http://www.extremecarstrucks.com/inventory.xml?ID=12655");

                XmlDocument urlDoc = new XmlDocument();

                urlDoc.Load(reader);

                return urlDoc;


            }
            catch (System.Exception ex)
            {
                throw ex;

            }

        }


        public static string GenerateHTMLImageCode()
        {
            var builder = new StringBuilder();

            builder.Append("<html>");

            builder.Append("<head>");
            builder.Append("  <title></title>");

            builder.Append("</head>");
            builder.Append("<style type=\"text/css\">");

            builder.Append("html, body, div {");
            builder.Append("  padding: 0;");

            builder.Append("  margin: 0;");
            builder.Append("}");

            builder.Append("div {overflow: hidden;}");
            builder.Append("  .image-wrap {");

            builder.Append("    display: inline-block;");
            builder.Append("    position: relative;");

            builder.Append("  }");
            builder.Append(".image-wrap img.car-photo{");

            builder.Append("  }");
            builder.Append("  .overlay-top {");

            builder.Append("    width: 100%;");
            builder.Append("    background: white;");

            builder.Append("    text-align: center;");
            builder.Append(" padding-top: 2%;");

            builder.Append(" }");
            builder.Append("  .overlay-top img {");

            builder.Append("    width: 100%;");
            builder.Append("}");

            builder.Append("  .overlay-bottom {");
            builder.Append("    font-size: 1.5em;");

            builder.Append("    width: 100%;");
            builder.Append("    color: white;");

            builder.Append("   font-weight: bold;");

            builder.Append("  }");

            builder.Append("  .overlay-bottom img{");

            builder.Append("   width: 100%;");
            builder.Append("}");

            builder.Append("</style>");
            builder.Append("<body>");

            builder.Append("<div class=\"image-wrap\">");
            builder.Append("  <div class=\"overlay-top\"><img src=\"http://vinwindow.net/header.jpg\" /></div>");

            builder.Append("  <img class=\"car-photo\" src=\"http://vincontrol.com/DealerImages/3636/1N4AA5AP1BC817558/NormalSizeImages/19328-01-DSCN2350.JPG\" />");
            builder.Append("  <div class=\"overlay-bottom\"><img src=\"http://vinwindow.net/foot.jpg\" /></div>");

            builder.Append("</div>");
            builder.Append("</body>");

            builder.Append("</html>");
         
            return builder.ToString();
        }


    }









}

