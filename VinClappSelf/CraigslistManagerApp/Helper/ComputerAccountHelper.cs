using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using CraigslistManagerApp.DatabaseModel;
using CraigslistManagerApp.Model;

namespace CraigslistManagerApp.Helper
{
    public class ComputerAccountHelper
    {
        private const string SPACE = " ";

        public static string ColorTemplate;

        public static void DownloadImage(WebClient request, string webpath, string localPath)
        {
            byte[] byteStream = request.DownloadData(webpath);

            using (var fs = new FileStream(localPath, FileMode.Create))
            {
                fs.Write(byteStream, 0, byteStream.Length);

            }
        }


        private static List<string> RandomzieTable(WhitmanEntepriseMasterVehicleInfo vehicle, string fontColor,
                                                   string insertTemplate)
        {

            var list = new List<string>();

            list.Add(GenerateOptions(vehicle, fontColor, insertTemplate));
            list.Add(GenerateDescription(vehicle, fontColor, insertTemplate));
            list.Add(GenerateImages(vehicle, fontColor, insertTemplate));

            return list.OrderBy(t => Guid.NewGuid()).ToList();


        }

        public static string GenerateHtmlImageCode(WhitmanEntepriseMasterVehicleInfo vehicle)
        {
            const string fontColor = "black";

            const string insertTemplate = "white";
            //switch (insertTemplate)
            //{
            //    case "gray":
            //        fontColor = "lavenderblush";
            //        break;
            //    case "white":
            //        fontColor = "black";
            //        break;
            //    default:
            //        break;
            //}

            var random = new Random();

            int randomNumber = random.Next(1000, 9999);

            var randomBackColorList = new List<string>();

            randomBackColorList.AddRange(new String[] { "black", "blue", "red", "maroon", "green", "navy" });

            int colorrandomIndex = randomNumber % randomBackColorList.Count();

            ColorTemplate = randomBackColorList.ElementAt(colorrandomIndex);


            var builder = new StringBuilder();

            var listRandom = RandomzieTable(vehicle, fontColor, insertTemplate);


            string queryString = "Stock=" + vehicle.StockNumber + "&Vin=" + vehicle.Vin + "&partner=" +
                                 vehicle.DealerId + randomNumber;


            builder.Append("<table align=\"center\" width=\"1500\" cellpadding=\"10\" cellspacing=\"0\" bgcolor=\"" +
                           ColorTemplate + "\">");

            builder.Append("<tr><td colspan=\"5\" align=\"center\" > <img src=\"http://vinlineup.com/468x60.gif\" /></td></tr>");
           

            builder.Append("<tr  bgcolor=\"" + ColorTemplate + "\">");

            builder.Append("<td ></td>");

            builder.Append(" <td colspan=\"2\">");

            builder.Append("<table width=\"725\" cellspacing=\"0\" cellpadding=\"0\">");
            builder.Append("<tr>");
            builder.Append("<td>");

            builder.Append("<a href=\"" + vehicle.WebSiteUrl +
                           "\" target=\"_blank\"><img border=\"0\" src=\"" + vehicle.LogoUrl +
                           "\"/></a>");
            builder.Append("</td>");

            if (vehicle.DealerId!=44670 &&vehicle.DealerId!=15986 )
            {

                builder.Append("<td align=\"right\" >");

                builder.Append(
                    " <font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h2>" +
                    vehicle.DealershipName + "</h2></font><br />");

                builder.Append(
                    "<font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h2>" +
                    vehicle.PhoneNumber + "</h2></font><br />");

                if (!String.IsNullOrEmpty(vehicle.CityOveride.Trim()))
                    builder.Append(
                        "  <font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                        vehicle.CityOveride + "</font><br />");
                if (vehicle.DealerId==1200)
                {
                    builder.Append(
                        "<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/ClientInfoRequest?" +
                        queryString +
                        "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/newportcoastauto/emailUsAt.png\" /></a><br />");
                }


                builder.Append("</td>");
            }

            builder.Append("</tr>");
            builder.Append("</table>");
            builder.Append("</td>");
            builder.Append("<td></td>");

            builder.Append("</tr>");

            builder.Append("<tr>");
            builder.Append("<td  bgcolor=\"" + ColorTemplate +
                           "\" height=\"0\" background=\"http://vinclapp.net/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("<td  bgcolor=\"" + ColorTemplate +
                           "\" background=\"http://vinclapp.net/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("<td  bgcolor=\"" + ColorTemplate +
                           "\" background=\"http://vinclapp.net/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("<td bgcolor=\"" + ColorTemplate +
                           "\" background=\"http://vinclapp.net/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("</tr>");

            builder.Append("<tr>");

            builder.Append("<td bgcolor=\"" + ColorTemplate + "\"></td>");

            builder.Append("<td bgcolor=\"" + insertTemplate + "\">");

            builder.Append("<table>");
            builder.Append("<tr>");
            builder.Append("<td bgcolor=\"" + insertTemplate + "\">");

            builder.Append(GenerateThumbnail(vehicle));

            builder.Append("  </td>");

            builder.Append(" </tr>");
            builder.Append(" <tr>");
            builder.Append("  <td bgcolor=\"" + insertTemplate +
                           "\"><font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                           GenerateRandomTextBelowPic() + "</font></td>");
            builder.Append(" </tr>");
            builder.Append("</table>");

            builder.Append("</td>");



            builder.Append("<td bgcolor=\"" + insertTemplate + "\">");
            builder.Append("<table width=\"550\" cellspacing=\"0\">");
            builder.Append("<tr>");
            builder.Append("<td bgcolor=\"" + insertTemplate + "\" rowspan=\"2\">");

            builder.Append(GenerateInfoOrder(vehicle, fontColor));


            builder.Append(" </td>");
            builder.Append("<td bgcolor=\"" + insertTemplate + "\" align=\"center\">");

            //if (!String.IsNullOrEmpty(vehicle.SalePrice))
            //{

            //    double price = 0;

            //    bool validPrice = Double.TryParse(vehicle.SalePrice, out price);

            //    bool hasPrice = vehicle.Price && validPrice && (price > 0);

            //    if (hasPrice)
            //        builder.Append("<font size=\"+5\" color=\"" + fontColor +
            //                       "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + price.ToString("C") +
            //                       "</font>");
            //    else
            //        builder.Append("<font size=\"+3\" color=\"" + fontColor +
            //                       "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + "CALL FOR PRICE" +
            //                       "</font>");
            //}
            //else
            //    builder.Append("<font size=\"+3\" color=\"" + fontColor +
            //                   "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + "CALL FOR PRICE" + "</font>");
            if (randomNumber % 3 == 0)
            {

                builder.Append(
                    "<img border=\"0\" src=\"http://webpics.us/DealerLogo/red.gif\"  />");
            }
            else if (randomNumber % 3 == 1)
            {

                builder.Append(
                    "<img border=\"0\" src=\"http://webpics.us/DealerLogo/green.gif\"  />");
            }
            else if (randomNumber % 3 == 2)
            {

                builder.Append(
                    "<img border=\"0\" src=\"http://webpics.us/DealerLogo/blue.gif\"  />");
            }

            builder.Append("</td>");
            builder.Append("</tr>");

            builder.Append("<tr bgcolor=\"" + insertTemplate + "\"><td bgcolor=\"" + insertTemplate + "\"></td></tr>");

            builder.Append(" </table>");
            builder.Append("</td>");

            builder.Append(" <td bgcolor=\"" + ColorTemplate + "\"></td>");
            builder.Append(" </tr>");
            builder.Append(" <tr>");

            builder.Append(" <td bgcolor=\"" + ColorTemplate + "\"></td>");
            builder.Append("  <td colspan=\"2\"  bgcolor=\"" + insertTemplate + "\" >");
            builder.Append("<table cellspacing=\"0\">");

            builder.Append(" <tr>");
            builder.Append("<td bgcolor=\"" + insertTemplate + "\" width=\"170\">");


            builder.Append(" <font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                           vehicle.DealershipName + "</font><br />");

            builder.Append("<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h3>" +
                          vehicle.PhoneNumber + "</h3></font><br />");
            builder.Append("  <font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                          vehicle.StreetAddress + "</font><br />");
            builder.Append(" <font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                           vehicle.City + ", " + vehicle.State + " " +
                          vehicle.ZipCode + "</font><br />");
            builder.Append("</td>");


            //OPTIONS BEGIN

            builder.Append(listRandom.ElementAt(0));
            //OPTIONS END

            builder.Append("</tr>");

            builder.Append("<tr>");

            builder.Append("<td bgcolor=\"" + insertTemplate + "\">");




            //DESCRIPTION BEGINS

            builder.Append(listRandom.ElementAt(1));
            //DESCRIPTION END

            builder.Append("</tr>");


            builder.Append(" <tr>");
            builder.Append("<td bgcolor=\"" + insertTemplate +
                           "\"><a href=\"http://www.carfax.com/VehicleHistory/p/Report.cfx?partner=DVW_1&vin=" +
                           vehicle.Vin +
                           "\" target=\"_blank\"><img src=\"http://vinclapp.net/alpha/content/images/vin-trade-icon.gif\" width=\"150\" /></a></td>");

            //IMAGES BEGIN
            builder.Append(listRandom.ElementAt(2));
            //IMAGE ENDS

            builder.Append("</tr>");
            builder.Append(" <tr>");
            builder.Append("<td valign=\"top\" bgcolor=\"" + insertTemplate + "\"></td>");
            builder.Append("<td valign=\"top\" bgcolor=\"" + insertTemplate + "\"></td>");
            builder.Append("</tr>");

            builder.Append(" <tr>");
            builder.Append(" <td bgcolor=\"" + insertTemplate + "\"></a></td>");
            builder.Append("  </tr>");
            builder.Append(" <tr>");
            builder.Append("   <td bgcolor=\"" + insertTemplate + "\"></td>");
            builder.Append("</tr>");
            builder.Append(" </table>");
            builder.Append(" </td>");
            builder.Append(" <td bgcolor=\"" + ColorTemplate + "\"></td>");
            builder.Append(" </tr>");
            builder.Append(" <tr bgcolor=\"" + ColorTemplate + "\">");
            builder.Append("<td bgcolor=\"" + ColorTemplate + "\"></td>");
            builder.Append(" <td bgcolor=\"" + ColorTemplate + "\" align=\"left\"><font color=\"" + fontColor +
                           "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">Price is subject to change without notice</font></td>");
            builder.Append(" <td bgcolor=\"" + ColorTemplate +
                           "\" align=\"right\"></td>");
            builder.Append("<td></td>");
            builder.Append("  </tr>");
            builder.Append("</table>");

            return builder.ToString();
        }


        public static string GenerateHtmlImageCodeForSecondBottomImage(WhitmanEntepriseMasterVehicleInfo vehicle)
        {

            var builder = new StringBuilder();

            try
            {
                

                var filterList =
                    clsVariables.fullMasterVehicleList.Where(x => x.DealerId == vehicle.DealerId)
                                .OrderBy(x => Guid.NewGuid())
                                .Take(4);

                var firstvehilce = filterList.ElementAt(0);

                var secondvehilce = filterList.ElementAt(1);

                var thirdtvehilce = filterList.ElementAt(2);

                var fourthvehilce = filterList.ElementAt(3);

                builder.Append(
                    "<table bgcolor=\"" + ColorTemplate +
                    "\" align=\"center\" width=\"750\" cellpadding=\"10\" cellspacing=\"0\" >");

                builder.Append("<tr  bgcolor=\"" + ColorTemplate + "\">");


                builder.Append(" <td></td>");


                builder.Append(" <td colspan=\"2\">");
                builder.Append("  <table width=\"725\" cellspacing=\"0\" cellpadding=\"0\">");
                builder.Append("<tr>");
                builder.Append(
                    " <td><b><font size=\"5\" color=\"white\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">View Our Inventory!</font></b></td>");
                builder.Append("  </tr>");
                builder.Append("  <td bgcolor=\"#666\" colspan=\"2\">");
                builder.Append("<table cellspacing=\"0\" cellpadding=\"5\" width=\"726\">");
                builder.Append(" <tr>");

                builder.Append("  <td><img src=\"" + firstvehilce.FirstImageUrl +
                               "\" width=\"165\" height=\"140\" /></td>");
                builder.Append("  <td><img src=\"" + secondvehilce.FirstImageUrl +
                               "\" width=\"165\" height=\"140\" /></td>");
                builder.Append("  <td><img src=\"" + thirdtvehilce.FirstImageUrl +
                               "\" width=\"165\" height=\"140\" /></td>");
                builder.Append("  <td><img src=\"" + fourthvehilce.FirstImageUrl +
                               "\" width=\"165\" height=\"140\" /></td>");
                builder.Append("</tr>");

                builder.Append(" <tr>");

                builder.Append("  <td><font size=\"2\" color=\"white\">" + firstvehilce.ModelYear + SPACE +
                               firstvehilce.Make + SPACE + firstvehilce.Model + "</font></td>");
                builder.Append("  <td><font size=\"2\" color=\"white\"> " + secondvehilce.ModelYear + SPACE +
                               secondvehilce.Make + SPACE + secondvehilce.Model + "</font></td>");
                builder.Append("  <td><font size=\"2\" color=\"white\">" + thirdtvehilce.ModelYear + SPACE +
                               thirdtvehilce.Make + SPACE + thirdtvehilce.Model + "</font></td>");
                builder.Append("  <td><font size=\"2\" color=\"white\">" + fourthvehilce.ModelYear + SPACE +
                               fourthvehilce.Make + SPACE + fourthvehilce.Model + "</font></td>");
                builder.Append("</tr>");
                builder.Append("                        </table>");
                builder.Append(" </td>");
                builder.Append("    </tr>   ");
                builder.Append(" </table>");
                builder.Append(" </td>");
                builder.Append(" <td bgcolor=\"" + ColorTemplate + "\"></td>");
                builder.Append("</tr>");
                builder.Append(" <tr bgcolor=\"" + ColorTemplate + "\">");
                builder.Append("   <td></td>");
                builder.Append(" <td></td>");
                builder.Append(" </tr>");
                builder.Append("</table>");
                //}
            }
            catch (Exception)
            {

                return null;
            }




            return builder.ToString();
        }




        public static string GenerateHtmlImageCodeForCaliforniaBeemerByComputerAccount(WhitmanEntepriseMasterVehicleInfo vehicle)
        {
            var builder = new StringBuilder();

            builder.Append("<!doctype html>");

            builder.Append("<html lang=\"eng\">");

            builder.Append("<head>");

            builder.Append("  <title>Craigslist Tempalte</title>");

            builder.Append("  <style type=\"text/css\">");

            builder.Append(" h1, h2, h3, h4, h5 {padding: 0; margin: 0;}");

            builder.Append("  body {font-weight: bold;}");

            builder.Append("  ul {display: inline-block}");

            builder.Append("  ul li {text-align: left}");

            builder.Append("  </style>");

            builder.Append("</head>");

            builder.Append("<body>");

            builder.Append(" <table border=\"1\" width=\"900\" align=\"center\" cellpadding=\"5\">");

            builder.Append(" <tr><td align=\"center\"><h1>Price: <font color=\"red\">Please Call</h1></font></td></tr>");

            builder.Append("  <tr><td align=\"center\"><h1><font color=\"red\">Call "+vehicle.PhoneNumber+"</font></h1></td></tr>");

            builder.Append("    <tr>");

            builder.Append("<td align=\"center\">");

            builder.Append(vehicle.ModelYear + SPACE + vehicle.Make + SPACE + vehicle.Model + SPACE +"<br />");

            builder.Append("  <img  width=\"805\" src=\""+vehicle.FirstImageUrl+"\">");

            builder.Append("</td>");

            builder.Append("</tr>");

            builder.Append(" <tr><td align=\"center\"><font color=\"red\"><h1>Call "+vehicle.PhoneNumber+"</h1></font> "+vehicle.DealershipName+"</td></tr>");

            builder.Append(" <tr>");

            builder.Append(" <td align=\"center\">");

            builder.Append("  Vehicle Location:<br />");

            builder.Append("  <font color=\"red\">" + vehicle.DealershipName + "</font><br />");

            builder.Append("  <a href=\""+vehicle.WebSiteUrl+"\">"+vehicle.WebSiteUrl+"</a><br />");

            builder.Append(vehicle.StreetAddress + SPACE +vehicle.City +", " + vehicle.State + SPACE + vehicle.ZipCode);

            builder.Append("</td>");

            builder.Append("  </tr>");

            builder.Append(" <tr>");

            builder.Append("  <td align=\"center\">");

            builder.Append("  Vehicle Information:<br />");

            builder.Append("  <ul>");

            builder.Append(GenerateInfoOrderForCaliforniaBeemer(vehicle));
            

            builder.Append(" </ul>");



            builder.Append("</td>");

            builder.Append(" </tr>");

            builder.Append(" <tr>");

            builder.Append("<td align=\"center\">");

            builder.Append("  Vehicle Photos:<br />");

            builder.Append(GenerateImagesForCaliforniaBeemer(vehicle));

            builder.Append("</td>");

            builder.Append("    </tr>");

            builder.Append(" </table>");

            builder.Append("</body>");

            builder.Append("</html>");

       

            return builder.ToString();
        }
        public static string GenerateHtmlImageCodeForAudiByComputerAccount(WhitmanEntepriseMasterVehicleInfo vehicle)
        {
            const string fontColor = "black";

            const string colorTemplate = "black";

            const string insertTemplate = "white";

            var builder = new StringBuilder();

            var listRandom = GetImagesForAudi(vehicle, fontColor);

            var random = new Random();

            int randomNumber = random.Next(1000, 9999);

            string queryString = "Stock=" + vehicle.StockNumber + "&Vin=" + vehicle.Vin + "&partner=" +
                                 vehicle.DealerId + randomNumber;


            builder.Append("<table align=\"center\" width=\"750\" cellpadding=\"10\" cellspacing=\"0\" bgcolor=\"" +
                           colorTemplate + "\">");

            builder.Append("<tr  bgcolor=\"" + colorTemplate + "\">");

            builder.Append("<td ></td>");

            builder.Append(" <td colspan=\"2\">");

            builder.Append("<table width=\"725\" cellspacing=\"0\" cellpadding=\"0\">");
            builder.Append("<tr>");
            builder.Append("<td>");

            builder.Append("<a href=\"" + vehicle.WebSiteUrl +
                           "\" target=\"_blank\"><img border=\"0\" src=\"" + vehicle.LogoUrl +
                           "\"/></a>");
            builder.Append("</td>");


            builder.Append("<td align=\"right\" >");

            builder.Append("<font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h2>" +
                           vehicle.DealershipName + "</h2></font><br />");

            builder.Append("<font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h2>" +
                           vehicle.PhoneNumber + "</h2></font><br />");

            if (!String.IsNullOrEmpty(vehicle.CityOveride.Trim()))
                builder.Append("<font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                               vehicle.CityOveride.Trim() + "</font><br />");

            if (vehicle.DealerId==1200)
            {

                builder.Append("<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/ClientInfoRequest?" +
                               queryString +
                               "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/newportcoastauto/emailUsAt.png\" /></a><br />");
            }


            builder.Append("</td>");


            builder.Append("</tr>");
            builder.Append("</table>");
            builder.Append("</td>");
            builder.Append("<td></td>");

            builder.Append("</tr>");

            builder.Append("<tr>");
            builder.Append("<td  bgcolor=\"" + colorTemplate +
                           "\" height=\"0\" background=\"http://vinclapp.net/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("<td  bgcolor=\"" + colorTemplate +
                           "\" background=\"http://vinclapp.net/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("<td  bgcolor=\"" + colorTemplate +
                           "\" background=\"http://vinclapp.net/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("<td bgcolor=\"" + colorTemplate +
                           "\" background=\"http://vinclapp.net/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("</tr>");

            builder.Append("<tr>");

            builder.Append("<td bgcolor=\"" + colorTemplate + "\"></td>");

            builder.Append("<td bgcolor=\"" + insertTemplate + "\">");

            builder.Append("<table>");
            builder.Append("<tr>");
            builder.Append("<td bgcolor=\"" + insertTemplate + "\">");

            builder.Append(GenerateThumbnail(vehicle));

            builder.Append("  </td>");

            builder.Append(" </tr>");
            builder.Append(" <tr>");
            builder.Append("  <td bgcolor=\"" + insertTemplate +
                           "\"><font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                           GenerateRandomTextBelowPic() + "</font></td>");
            builder.Append(" </tr>");
            builder.Append("</table>");

            builder.Append("</td>");


            builder.Append("<td bgcolor=\"" + insertTemplate + "\">");
            builder.Append("<table width=\"550\" cellspacing=\"0\">");
            builder.Append("<tr>");
            builder.Append("<td bgcolor=\"" + insertTemplate + "\" rowspan=\"2\">");

            builder.Append(GenerateInfoOrder(vehicle, fontColor));


            builder.Append(" </td>");
            builder.Append("<td bgcolor=\"" + insertTemplate + "\" align=\"center\">");

            //if (!String.IsNullOrEmpty(vehicle.SalePrice))
            //{

            //    double price = 0;

            //    bool validPrice = Double.TryParse(vehicle.SalePrice, out price);

            //    bool hasPrice = vehicle.Price && validPrice && (price > 0);

            //    if (hasPrice)
            //        builder.Append("<font size=\"+5\" color=\"" + fontColor +
            //                       "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + price.ToString("C") +
            //                       "</font>");
            //    else
            //    {
            //        builder.Append("<font size=\"+3\" color=\"" + fontColor +
            //                       "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + "CALL FOR PRICE" +
            //                       "</font>");
            //        if (vehicle.DealerId.Equals("4310"))
            //        {
            //            string textMessageLink = clsVariables.TextMessageLink.Replace("PLACEVIN", vehicle.Vin);
            //            builder.Append("<a href=\"" + textMessageLink +
            //                           "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/dealerimages/TextPrice.png\" /></a><br/>");
            //        }
            //    }
            //}
            //else
            //{
            //    builder.Append("<font size=\"+3\" color=\"" + fontColor +
            //                   "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + "CALL FOR PRICE" + "</font>");
            //    if (vehicle.DealerId.Equals("4310"))
            //    {
            //        string textMessageLink = clsVariables.TextMessageLink.Replace("PLACEVIN", vehicle.Vin);
            //        builder.Append("<a href=\"" + textMessageLink +
            //                       "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/dealerimages/TextPrice.png\" /></a><br/>");
            //    }

            //}
            if (randomNumber % 3 == 0)
            {

                builder.Append(
                    "<img border=\"0\" src=\"http://webpics.us/DealerLogo/red.gif\"  />");
            }
            else if (randomNumber % 3 == 1)
            {

                builder.Append(
                    "<img border=\"0\" src=\"http://webpics.us/DealerLogo/green.gif\"  />");
            }
            else if (randomNumber % 3 == 2)
            {

                builder.Append(
                    "<img border=\"0\" src=\"http://webpics.us/DealerLogo/blue.gif\"  />");
            }

            builder.Append("</td>");
            builder.Append("</tr>");

            builder.Append("<tr bgcolor=\"" + insertTemplate + "\"><td bgcolor=\"" + insertTemplate + "\"></td></tr>");

            builder.Append(" </table>");
            builder.Append("</td>");

            builder.Append(" <td bgcolor=\"" + colorTemplate + "\"></td>");
            builder.Append(" </tr>");
            builder.Append(" <tr>");

            builder.Append(" <td bgcolor=\"" + colorTemplate + "\"></td>");
            builder.Append("  <td colspan=\"2\"  bgcolor=\"" + insertTemplate + "\" >");
            builder.Append("<table cellspacing=\"0\">");

            builder.Append(" <tr>");
            builder.Append("<td bgcolor=\"" + insertTemplate + "\" width=\"170\">");

            builder.Append("<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                           vehicle.StreetAddress + "</font><br />");
            builder.Append("<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                           vehicle.City + ", " + vehicle.State + " " +
                          vehicle.ZipCode + "</font><br />");
            builder.Append("</td>");


            //FIRST IMAGE BEGIN

            builder.Append(listRandom.ElementAt(0));
            ////FIRST IMAGE END

            builder.Append("</tr>");

            builder.Append("<tr>");

            builder.Append("<td bgcolor=\"" + insertTemplate + "\">");



            builder.Append("</td>");

            ////SECOND IMAGE BEGINS

            builder.Append(listRandom.ElementAt(1));


            ////SECOND IMAGE END

            builder.Append("</tr>");


            builder.Append("<tr>");

            builder.Append("<td bgcolor=\"" + insertTemplate + "\">");



            builder.Append("</td>");

            ////SECOND IMAGE BEGINS

            builder.Append(listRandom.ElementAt(2));


            ////SECOND IMAGE END

            builder.Append("</tr>");

            builder.Append("<tr>");

            builder.Append("<td bgcolor=\"" + insertTemplate + "\">");



            builder.Append("</td>");

            ////SECOND IMAGE BEGINS

            builder.Append(listRandom.ElementAt(3));


            ////SECOND IMAGE END

            builder.Append("</tr>");

            builder.Append("<tr>");

            builder.Append("<td bgcolor=\"" + insertTemplate + "\">");



            builder.Append("</td>");

            ////SECOND IMAGE BEGINS

            builder.Append(listRandom.ElementAt(4));


            ////SECOND IMAGE END

            builder.Append("</tr>");



            builder.Append(" <tr>");
            builder.Append("<td bgcolor=\"" + insertTemplate +
                           "\"><a href=\"http://www.carfax.com/VehicleHistory/p/Report.cfx?partner=DVW_1&vin=" +
                           vehicle.Vin +
                           "\" target=\"_blank\"><img src=\"http://vinclapp.net/alpha/cListThemes/gloss/carfax_logo.jpg\" width=\"150\" /></a></td>");

            ////THRID IMAGE BEGIN
            builder.Append(listRandom.ElementAt(5));

            ////THIRD IMAGE ENDS

            builder.Append("</tr>");

            builder.Append(" <tr>");
            builder.Append("<td valign=\"top\" bgcolor=\"" + insertTemplate + "\"></td>");
            builder.Append("</tr>");


            builder.Append(" <tr>");
            builder.Append(" <td bgcolor=\"" + insertTemplate + "\"></a></td>");
            builder.Append("  </tr>");
            builder.Append(" <tr>");
            builder.Append("   <td bgcolor=\"" + insertTemplate + "\"></td>");
            builder.Append("</tr>");
            builder.Append(" </table>");
            builder.Append(" </td>");
            builder.Append(" <td bgcolor=\"" + colorTemplate + "\"></td>");
            builder.Append(" </tr>");
            builder.Append(" <tr bgcolor=\"" + colorTemplate + "\">");
            builder.Append("<td bgcolor=\"" + colorTemplate + "\"></td>");

            builder.Append(" <td bgcolor=\"" + colorTemplate + "\" align=\"left\"><font color=\"" + fontColor +
                           "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">Price is subject to change without notice</font></td>");
            builder.Append(" <td bgcolor=\"" + colorTemplate +
                           "\" align=\"right\"><a href=\"http://vehicleinventorynetwork.com/\"><img border=\"0\" src=\"http://webpics.us/vinclapplogo/vin-logo-white2.png\" /></a></td>");
            builder.Append("<td></td>");
            builder.Append("  </tr>");
            builder.Append("</table>");



            return builder.ToString();
        }

        public static string GenerateCraiglistContentByImage(WhitmanEntepriseMasterVehicleInfo vehicle)
        {
            var builder = new StringBuilder();

            var random = new Random();

            int randomNumber = random.Next(1000, 9999);

            var domainRandomList = new List<String>();

            domainRandomList.AddRange(new String[]
                                        {
                                            "http://dealerp.us/SmartImageCenter/",
                                            "http://dealerpic.us/SmartImageCenter/",
                                            "http://dealerpict.us/SmartImageCenter/",
                                            "http://dealerpicture.us/SmartImageCenter/",
                                            "http://imageusme.us/SmartImageCenter/",
                                            "http://imagingshare.us/SmartImageCenter/",
                                            "http://imgdesign.us/SmartImageCenter/",
                                            "http://imgdomain.us/SmartImageCenter/",
                                            "http://imgstore.us/SmartImageCenter/",
                                            "http://NOPIC.US/SmartImageCenter/",
                                            "http://PICDESIGN.US/SmartImageCenter/",
                                            "http://PICDOMAIN.US/SmartImageCenter/",
                                            "http://PICLOADER.US/SmartImageCenter/",
                                            "http://BOBSIMAGE.US/SmartImageCenter/",
                                            "http://SEEIMAGE.US/SmartImageCenter/",
                                            "http://CARSFORYOU.US/SmartImageCenter/",
                                      
                                        });

            int domainandomIndex = randomNumber % domainRandomList.Count();

            string randomDomain = domainRandomList.ElementAt(domainandomIndex);

            string modifiedTrim = vehicle.Trim.Replace(" ", "-");
            modifiedTrim = modifiedTrim.Replace("/", "");


            string defaultUrl = "http://vinlineup.com/" + vehicle.DealershipName.Replace(" ", "-") + "/" + vehicle.DealerId + "/" + vehicle.ModelYear + "-" + vehicle.Make.Replace(" ", "-") + "-" + vehicle.Model.Replace(" ", "-") + "-" + modifiedTrim + "/" + vehicle.Vin;

            string defaultDealerUrl = "http://vinlineup.com/" + vehicle.DealershipName.Replace(" ", "-") + "/" + vehicle.DealerId;


        
            builder.Append("<font size=\"-1\" color=\"beige\">" + GenerateRandomTextFromBookOnTop() + "</font><br/>");

            if (vehicle.DealerId == 2584 || vehicle.DealerId == 1541 || vehicle.DealerId == 1200 || vehicle.DealerId == 113738 || vehicle.DealerId == 3738 || vehicle.DealerId == 44670 || vehicle.DealerId == 15896 || vehicle.DealerId == 11828 || vehicle.DealerId == 7180 || vehicle.DealerId == 15986 || vehicle.DealerId == 2650 || vehicle.DealerId == 2299 || vehicle.DealerId == 18498 || vehicle.DealerId==115896)
            {

                //builder.Append("<a href=\"" + vehicle.WebSiteURL + "\" target=\"_blank\"><img src=\"" +
                //               vehicle.HTMLImageURL.Substring(0,
                //                                              vehicle.HTMLImageURL.IndexOf(",",
                //                                                                           System.StringComparison.
                //                                                                               Ordinal)) +
                //               "\" /></a><br/>");

                if(vehicle.DealerId!=1200)

                    builder.Append("<a href=\"" + vehicle.WebSiteUrl + "\" target=\"_blank\"><img src=\"" + randomDomain + "ImageHandler/SmartImageFetch.ashx?adsId=" + vehicle.TrackingId + "\" /></a><br/>");
                else
                {
                    string newportUrl = "http://newportcoastauto.com/inventory/detail/"  + vehicle.Vin;
                    builder.Append("<a href=\"" + newportUrl + "\" target=\"_blank\"><img src=\"" + randomDomain + "ImageHandler/SmartImageFetch.ashx?adsId=" + vehicle.TrackingId + "\" /></a><br/>");
                    //builder.Append("<a href=\"" + "http://newportcoastauto.com/profile.php?vin=" + vehicle.Vin + "\" target=\"_blank\"><img src=\"" + randomDomain + "ImageHandler/SmartImageFetch.ashx?adsId=" + vehicle.TrackingId + "\" /></a><br/>");
                }
            }
            else

                
                builder.Append("<a href=\"" + defaultUrl + "\" target=\"_blank\"><img src=\"" + randomDomain + "ImageHandler/SmartImageFetch.ashx?adsId=" + vehicle.TrackingId + "\" /></a><br/>");
              

            if(vehicle.DealerId!=115896)

                builder.Append("<a href=\"" + defaultDealerUrl + "\" target=\"_blank\"><img src=\"" + randomDomain + "ImageHandler/SmartBImageFetch.ashx?adsId=" + vehicle.TrackingId + "\" /></a><br/>");
            else
            {
                builder.Append("<a href=\"" + vehicle.WebSiteUrl + "\" target=\"_blank\"><img src=\"" + randomDomain + "ImageHandler/SmartBImageFetch.ashx?adsId=" + vehicle.TrackingId + "\" /></a><br/>");
            }

            //builder.Append("<font size=\"-1\" color=\"beige\">" + GenerateRandomTextFromBook(vehicle.TradeInBannerLink) + "</font><br/>");

 
          

            return builder.ToString();
        }

        public static string GenerateCraiglistContentBySimpleText(WhitmanEntepriseMasterVehicleInfo vehicle)
        {
            var builder = new StringBuilder();
            
            var random = new Random();

            var firstRandom = random.Next(0, 2);

            //if (firstRandom % 2 == 0)
            //{

            //    builder.Append("Vehicle Year : " + vehicle.ModelYear + "<br>");
            //    builder.Append("Vehicle Make : " + vehicle.Make + "<br>");
            //    builder.Append("Vehicle Model : " + vehicle.Model + "<br>");
            //    if (!String.IsNullOrEmpty(vehicle.Trim))
            //        builder.Append("Vehicle Trim : " + vehicle.Trim + "<br>");
            //    if (!String.IsNullOrEmpty(vehicle.Mileage))
            //        builder.Append("Vehicle Mileage :" + vehicle.Mileage + "<br>");
            //    if (!String.IsNullOrEmpty(vehicle.ExteriorColor))
            //        builder.Append("Exterior Color :" + vehicle.ExteriorColor + "<br>");
            //    if (!String.IsNullOrEmpty(vehicle.BodyType))
            //        builder.Append("BodyType :" + vehicle.BodyType + "<br>");
            //    builder.Append("Vehicle Stock : " + vehicle.StockNumber + "<br>");
            //    builder.Append("Vehicle Vin : " + vehicle.Vin + "<br>");
            //    if (!String.IsNullOrEmpty(vehicle.Description))
            //        builder.Append("Vehicle Description :" + vehicle.Description + "<br>");

            //}
            //else if (firstRandom % 2 == 1)
            //{
            //    builder.Append("Year : " + vehicle.ModelYear + "<br>");
            //    builder.Append("Make : " + vehicle.Make + "<br>");
            //    builder.Append("Model : " + vehicle.Model + "<br>");
            //    if (!String.IsNullOrEmpty(vehicle.Trim))
            //        builder.Append("Trim : " + vehicle.Trim + "<br>");
            //    if (!String.IsNullOrEmpty(vehicle.Mileage))
            //        builder.Append("Mileage :" + vehicle.Mileage + "<br>");
            //    if (!String.IsNullOrEmpty(vehicle.ExteriorColor))
            //        builder.Append("Exterior Color : " + vehicle.ExteriorColor + "<br>");
            //    if (!String.IsNullOrEmpty(vehicle.BodyType))
            //        builder.Append("BodyType :" + vehicle.BodyType + "<br>");
            //    builder.Append("Stock : " + vehicle.StockNumber + "<br>");
            //    builder.Append("Vin : " + vehicle.Vin + "<br>");
            //    if (!String.IsNullOrEmpty(vehicle.Description))
            //        builder.Append("Description :" + vehicle.Description + "<br>");
                
            //}

            //builder.Append("Engine : " + vehicle.Engine + "<br>");
            //builder.Append("Body Type : " + vehicle.BodyType + "<br><br>");
            


            
            var dealerSenetenceList = new List<String>();
            
            dealerSenetenceList.AddRange(new String[]
                {
                    "Click here go to our inventory",
                    "See more info, click here",
                    "See complete our inventory",
                    "Go to our website",
                    "See more cars",
                    "Pre-Owned Inventory",
                    "Click for more info",
                    "Click to get a quote",
                    "Value your trade (Click here)",
                    "See more details (Click here)",
                    "Click here to apply for credit",
                    "Large inventory to choose from (See all)",
                    "Easy financing (click here)",
                    "NO time for waiting. Call us now",
                    "See more than what you can imagine",
                    "View all inventory",
                    "See more info about this vehicle",
                    "See more info about this car",
                    "See more info about " + vehicle.ModelYear + " " + vehicle.Make + " " + vehicle.Model + " " +
                    vehicle.Trim,
                    "See more info about " + vehicle.ModelYear + " " + vehicle.Make,
                    "See more info about " + vehicle.Make,
                    "See more info about " + vehicle.Model,
                    "See more info about this dealer",
                    "See more info about " + vehicle.DealershipName,
                    "Click here or call " + vehicle.PhoneNumber,
                    "See more pictures (Click here)",
                    "Click for info",
                    "See us online (Click here)",
                    "Visit us online (Click here)",
                    "Schedule a test drive (Click here)",
                    "Click for price, mileage, photos, and more info ","View additional info (including price and mileage) for this vehicle "

                });



            if (vehicle.DealerId == 1200)
            {
                string newportUrl = "http://newportcoastauto.com/inventory/detail/" + vehicle.Vin;
                builder.Append("<b><a href=\"" + newportUrl + "\" target=\"_blank\">" + dealerSenetenceList.ElementAt(random.Next(0, dealerSenetenceList.Count())) + "</a></b><br/>");

            }
            else 
            {
                var websiteurl = "http://www." + vehicle.WebSiteUrl;
                builder.Append("<a href=\"" + websiteurl + "\" >" +
                               dealerSenetenceList.ElementAt(random.Next(0, dealerSenetenceList.Count())) +
                               "</a><br/>");
            }

            builder.Append("<br>");


            builder.Append("Call us at <b>" +vehicle.PhoneNumber +" </b> for more information.<br>");

            builder.Append("Hope to see you soon!");
            ;
            
            
            //var surroundedCityList = new List<string>();

            //if (vehicle.PostingCityId == 1)
            //{

            //    surroundedCityList.AddRange(new string[]
            //        {
            //            "Coto de Caza", "Villa Park", "Laguna Beach", "Anaheim", "Brea", "Buena Park", "Costa Mesa",
            //            "Cypress", "Dana Point", "Fountain Valley", "Fullerton", "Garden Grove", "Huntington Beach",
            //            "Irvine", "La Habra", "La Palma", "Laguna Hills", "Laguna Niguel", "Laguna Woods", "Lake Forest"
            //            , "Los Alamitos", "Mission Viejo", "Newport Beach", "Orange", "Placentia",
            //            "Rancho Santa Margarita",
            //            "San Clemente", "San Juan Capistrano", "Santa Ana", "Seal Beach", "Stanton", "Tustin",
            //            "Westminster", "Yorba Linda", "Ladera Ranch", "Midway City", "Rossmoor",


            //        });
            //}
            //else if (vehicle.PostingCityId == 3)
            //{
            //    surroundedCityList.AddRange(new string[]
            //        {
            //           "Carlsbad","Chula Vista","Coronado","Del Mar","El Cajon","Encinitas","Escondido","Imperial Beach","La Mesa",
            //           "Lemon Grove","National City","Oceanside","Poway","San Diego","San Marcos","Santee","Solana Beach","Vista"


            //        });
            //}
            //else if (vehicle.PostingCityId == 10)
            //{
            //    surroundedCityList.AddRange(new string[]
            //        {
            //           "Los Angeles","Long Beach","Glendale","Santa Clarita","Lancaster","Palmdale","Pomona","Torrance","Pasadena",
            //           "El Monte","Downey","Inglewood","West Covina","Norwalk","Burbank"


            //        });
            //}
            //else if (vehicle.PostingCityId == 12)
            //{
            //    surroundedCityList.AddRange(new string[]
            //        {
            //           "Banning","Beaumont","Temecula","San Jacinto","Riverside","Rancho Mirage","Palm Springs","Palm Desert","Norco",
            //           "Murrieta","Moreno Valley","La Quinta","Lake Elsinore","Cathedral City","Canyon Lake"


            //        });
            //}

            //if (surroundedCityList.Count < 20)
            //{
            //    foreach (var tmp in surroundedCityList.Shuffle().Take(surroundedCityList.Count))
            //    {
            //        builder.Append(tmp + ", ");
            //    }

            //}
            //else
            //{
            //    foreach (var tmp in surroundedCityList.Shuffle().Take(20))
            //    {
            //        builder.Append(tmp + ", ");
            //    }
            //}

        

            builder.Append("<br/>");
            builder.Append("<br/>");

            builder.Append(CommonHelper.GenerateRandomTextFromBook() +"<br/>");

            builder.Append("FreshStart" + "<br/>");

            return builder.ToString();


            //else
            //{
            //    builder.Append("<h1><a href=\"" + defaultUrl + "\" target=\"_blank\">" +
            //                   dealerSenetenceList.ElementAt(random.Next(0, dealerSenetenceList.Count())) +
            //                   "</a></h1><br/>");

            //}

            ////if (vehicle.DealerId == 1200)
            ////    builder.Append("<a href=\"" + defaultUrl + "\" target=\"_blank\">" +
            ////              dealerSenetenceList.ElementAt(random.Next(0, dealerSenetenceList.Count())) +
            ////              "</a><br/>");

            ////else
            ////{
            ////    builder.Append("<a href=\"" + vehicle.WebSiteUrl + "\" target=\"_blank\">" +
            ////                dealerSenetenceList.ElementAt(random.Next(0, dealerSenetenceList.Count())) +
            ////                "</a><br/>");
            ////}

           

            //builder.Append("<hr>");



            //if (secondtRandom % 3 == 0)
            //{
            //    foreach (var tmp in squareRandom)
            //    {
            //        if (!String.IsNullOrEmpty(tmp.SalePrice)&&tmp.Price)
            //        {


            //            decimal price;

            //            bool validPrice = Decimal.TryParse(tmp.SalePrice, out price);

            //            validPrice = validPrice && (price > 0);

            //            if (validPrice)
            //            {
            //                string salePrice = price.ToString("C");

            //                builder.Append("Also available : " + tmp.ModelYear + " " +
            //                   tmp.Make + " " + tmp.Model + " " +
            //                   tmp.Trim + " " + tmp.ExteriorColor + " stock#" + tmp.StockNumber +" " + salePrice + "<br>");
            //            }
            //            else
            //            {
            //                builder.Append("Also available : " + tmp.ModelYear + " " +
            //                   tmp.Make + " " + tmp.Model + " " +
            //                   tmp.Trim + " " + tmp.ExteriorColor + " stock#" + tmp.StockNumber + " " + "<br>");
            //            }
            //        }
            //        else
            //        {
                        
            //        builder.Append("Also available : " + tmp.ModelYear + " " +
            //                      tmp.Make + " " + tmp.Model + " " +
            //                      tmp.Trim + " " + tmp.ExteriorColor + " stock#" + tmp.StockNumber + " " + "<br>");
            //        }

                 

            //    }
           

            //}
            //else if (secondtRandom % 3 == 1)
            //{
            //    foreach (var tmp in squareRandom)
            //    {
            //        if (!String.IsNullOrEmpty(tmp.SalePrice) && tmp.Price)
            //        {


            //            decimal price;

            //            bool validPrice = Decimal.TryParse(tmp.SalePrice, out price);

            //            validPrice = validPrice && (price > 0);

            //            if (validPrice)
            //            {
            //                string salePrice = price.ToString("C");

            //                builder.Append("We also have : " + tmp.ModelYear + " " +
            //                   tmp.Make + " " + tmp.Model + " " +
            //                   tmp.Trim + " " + tmp.ExteriorColor +  " stock#" + tmp.StockNumber + " " + salePrice + "<br>");
            //            }
            //            else
            //            {
            //                builder.Append("We also have : " + tmp.ModelYear + " " +
            //                  tmp.Make + " " + tmp.Model + " " +
            //                  tmp.Trim + " " + tmp.ExteriorColor + " stock#" + tmp.StockNumber + "<br>");
            //            }
            //        }
            //        else
            //        {

            //            builder.Append("We also have : " + tmp.ModelYear + " " +
            //                     tmp.Make + " " + tmp.Model + " " +
            //                     tmp.Trim + " " + tmp.ExteriorColor + " stock#" + tmp.StockNumber +  "<br>");
            //        }


                   

            //    }
              

            //}
            //else
            //{

            //    foreach (var tmp in squareRandom)
            //    {
            //        if (!String.IsNullOrEmpty(tmp.SalePrice) && tmp.Price)
            //        {


            //            decimal price;

            //            bool validPrice = Decimal.TryParse(tmp.SalePrice, out price);

            //            validPrice = validPrice && (price > 0);

            //            if (validPrice)
            //            {
            //                string salePrice = price.ToString("C");

            //                builder.Append("Other : " + tmp.ModelYear + " " +
            //                   tmp.Make + " " + tmp.Model + " " +
            //                   tmp.Trim + " " + tmp.ExteriorColor +  " stock#" + tmp.StockNumber +" "+  salePrice + "<br>");
            //            }
            //            else
            //            {
            //                builder.Append("Other : " + tmp.ModelYear + " " +
            //                          tmp.Make + " " + tmp.Model + " " +
            //                          tmp.Trim + " " + tmp.ExteriorColor + " stock#" + tmp.StockNumber + "<br>");
            //            }
            //        }
            //        else
            //        {


            //            builder.Append("Other : " + tmp.ModelYear + " " +
            //                           tmp.Make + " " + tmp.Model + " " +
            //                           tmp.Trim + " " + tmp.ExteriorColor + " stock#" + tmp.StockNumber  +"<br>");
            //        }




            //    }
               
              

            //}


     
        }

        public static string GenerateInfoOrder(WhitmanEntepriseMasterVehicleInfo vehicle, string fontColor)
        {
            var builder = new StringBuilder();

            builder.Append("<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                           vehicle.ModelYear + "</font> ");
            builder.Append("<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                           vehicle.Make + "</font> ");
            builder.Append("<font color=\"" + fontColor + "\"face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                           vehicle.Model + "</font> ");
            builder.Append("<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                           vehicle.Trim + " </font><br />");

            var infoRandomList = new List<String>();

            infoRandomList.AddRange(new String[]
                                        {
                                            "<font color=\"" + fontColor +
                                            "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + "VIN: " +
                                            vehicle.Vin + "</font><br />",
                                            "<font color=\"" + fontColor +
                                            "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + "Stock #" +
                                            vehicle.StockNumber + "</font><br />",
                                            "<font color=\"" + fontColor +
                                            "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                                            "Interior Color: " + vehicle.InteriorColor + "</font><br />",
                                            "<font color=\"" + fontColor +
                                            "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                                            "Exterior Color: " + vehicle.ExteriorColor + "</font><br />",
                                            "<font color=\"" + fontColor +
                                            "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + "Tranmission: " +
                                            vehicle.Tranmission + "</font><br />",
                                            "<font color=\"" + fontColor +
                                            "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + "Mileage " +
                                            vehicle.Mileage + "</font><br />"
                                        });

            var randomInfoOrder = infoRandomList.OrderBy(t => Guid.NewGuid()).ToArray<string>();

            foreach (string tmp in randomInfoOrder)
            {
                builder.Append(tmp);
            }

            return builder.ToString();
        }
        public static string GenerateInfoOrderForCaliforniaBeemer(WhitmanEntepriseMasterVehicleInfo vehicle)
        {
            var builder = new StringBuilder();

            var infoRandomList = new List<String>();

            infoRandomList.AddRange(new String[]
                                        {
                                            "<li>Mileage: <em>"+vehicle.Mileage+"</em></li> ",
                                            "<li>Body Style: <em>"+vehicle.BodyType+"</em></li> ",
                                             "<li>Stock#: <em>"+vehicle.StockNumber+"</em></li> ",
                                            "<li>Engine: <em>"+vehicle.Engine+"</em></li> ",
                                             "<li>VIN: <em>"+vehicle.Vin+"</em></li> ",
                                            "<li>Exterior Color: <em>"+vehicle.ExteriorColor+"</em></li> ",
                                             "<li>Transmission: <em>"+vehicle.Tranmission+"</em></li> ",
                                            "<li>Type: <em>Used</em></li> ",
                                  
                                           
                                        });

            var randomInfoOrder = infoRandomList.OrderBy(t => Guid.NewGuid()).ToArray<string>();

            foreach (string tmp in randomInfoOrder)
            {
                builder.Append(tmp);
            }

            return builder.ToString();
        }

      

        public static string GenerateCraiglistTitleByComputerAccount(WhitmanEntepriseMasterVehicleInfo vehicle)
        {
            var builder = new StringBuilder();
            
            var random = new Random();

            //var frontList = new List<string>();
           
            //frontList.AddRange(new string[]
            //    {
            //        "✰✪✰✪✰",
            //         "*******",
            //         "▄♦▄♦▄♦▄",
            //         "~~~~~",
            //         "◄►",
            //         "!!!!!!!^^^^!!!!!"
            //    });

            //string frontChunkNode = clsVariables.chunkList.First(t => t.ChunkName.Equals("FrontChunk")).ChunkStringValue;

            //frontList.AddRange(frontChunkNode.Split(new String[] { "," }, StringSplitOptions.None));

            //int year = Convert.ToInt32(vehicle.ModelYear);

            //if (year < DateTime.Now.Year - 1)
            //{
            //    frontList.Add("Used");

            //}

            //string middleChunkNode = clsVariables.chunkList.First(t => t.ChunkName.Equals("MiddleChunk")).ChunkStringValue;

            ////string endChunkNode = clsVariables.chunkList.First(t => t.ChunkName.Equals("EndChunk")).ChunkStringValue;

            ////string[] middleChunkList = middleChunkNode.Split(new String[] { "," }, StringSplitOptions.None);


            ////string[] endChunkList = endChunkNode.Split(new String[] { "," }, StringSplitOptions.None);

            
            var afterMakeRandomList = new List<String>();

            afterMakeRandomList.AddRange(new String[]
                {
                    String.IsNullOrEmpty(vehicle.ExteriorColor) ? "" : vehicle.ExteriorColor,
                    String.IsNullOrEmpty(vehicle.BodyType) ? "" : vehicle.BodyType,
                    String.IsNullOrEmpty(vehicle.Engine) ? "" : vehicle.Engine,
                    String.IsNullOrEmpty(vehicle.Tranmission) ? "" : vehicle.Tranmission,

                });

            var ranNumber = random.Next(0, 2);

            if (ranNumber % 2 == 0)
            {

                if (vehicle.Make.Equals("BMW") && vehicle.Model.ToLower().Contains("series"))
                {
                    if (!String.IsNullOrEmpty(vehicle.Trim))
                        builder.Append(vehicle.ModelYear + SPACE + vehicle.Make + SPACE +
                                       vehicle.Trim);
                    else
                    {
                        builder.Append(vehicle.ModelYear + SPACE + vehicle.Make + SPACE +
                                       vehicle.Model);
                    }
                }
                else
                {


                    if (!String.IsNullOrEmpty(vehicle.Trim))
                        builder.Append(vehicle.ModelYear + SPACE + vehicle.Make + SPACE +
                                       vehicle.Model + SPACE +
                                       vehicle.Trim);
                    else
                    {
                        builder.Append(vehicle.ModelYear + SPACE + vehicle.Make + SPACE +
                                       vehicle.Model);
                    }
                }
            }
            else
            {
                if (vehicle.Make.Equals("BMW") && vehicle.Model.ToLower().Contains("series"))
                {
                    if (!String.IsNullOrEmpty(vehicle.Trim))
                        builder.Append(vehicle.StockNumber + SPACE + vehicle.ModelYear +SPACE + vehicle.Make + SPACE  + vehicle.Trim);
                    else
                    {
                        builder.Append(vehicle.StockNumber + SPACE + vehicle.ModelYear + SPACE + vehicle.Make + SPACE + vehicle.Model);
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(vehicle.Trim))
                        builder.Append(vehicle.StockNumber + SPACE + vehicle.ModelYear + SPACE + vehicle.Make + SPACE + vehicle.Model+ SPACE + vehicle.Trim);
                    else
                    {
                        builder.Append(vehicle.StockNumber + SPACE + vehicle.ModelYear + SPACE + vehicle.Make + SPACE + vehicle.Model);
                    }
                }
            }




            if (String.IsNullOrEmpty(vehicle.AddtionalTitle))
            {
                if (!String.IsNullOrEmpty(vehicle.ExteriorColor))
                {
                    builder.Append(SPACE + afterMakeRandomList.ElementAt(random.Next(0, afterMakeRandomList.Count())) + SPACE);
                }
            }
            else
            {
                builder.Append(SPACE + vehicle.AddtionalTitle);
            }

            if (vehicle.DealerId == 10215 || vehicle.DealerId == 50000 || vehicle.DealerId == 50001)
            {
                builder.Append(SPACE  + vehicle.SalePrice);
            }
            
            return builder.ToString();

        }


        public static string GenerateRandomTextBelowPic()
        {
            var builder = new StringBuilder();

            string belowPicChunk =
                clsVariables.chunkList.First(t => t.ChunkName.Equals("BelowPicChunk")).ChunkStringValue;



            var random = new Random();

            string[] belowPicChunkList = belowPicChunk.Split(new String[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            builder.Append(belowPicChunkList.ElementAt(random.Next(0, belowPicChunkList.Count())));

            return builder.ToString();

        }

        public static string GenerateImagesForCaliforniaBeemer(WhitmanEntepriseMasterVehicleInfo vehicle)
        {
            var builder = new StringBuilder();
           
            string tmp = vehicle.CarImageUrl;

            var totalImage =
                tmp.Split(new String[] { "|", ",", " " }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var extractList = totalImage.Where(itmp => !vehicle.FirstImageUrl.Contains(itmp)).ToList();

            builder.Append(" <img width=\"400\" src=\"" + vehicle.FirstImageUrl + "\">");

            if (extractList.Count > 9)
            {
                var randomList = extractList.Take(9);

                foreach (var itmp in randomList)
                {
                    builder.Append(" <img width=\"400\" src=\"" + itmp + "\">");
                }
            }
            else
            {
                foreach (var itmp in extractList)
                {
                    builder.Append(" <img width=\"400\" src=\"" + itmp + "\">");
                }
            }



            return builder.ToString();

        }


        public static string GenerateOptions(WhitmanEntepriseMasterVehicleInfo vehicle, string fontColor,
                                             string insertTemplate)
        {
            string tmp = vehicle.Options;

            string[] totalOption = tmp.Split(new String[] { "|", "," }, StringSplitOptions.RemoveEmptyEntries);

            var random10Options = totalOption.OrderBy(t => Guid.NewGuid()).ToList().Take(10);


            var builder = new StringBuilder();

            builder.Append("<td valign=\"top\" bgcolor=\"" + insertTemplate + "\">");

            builder.Append("<font color=\"" + fontColor +
                           "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\" ><h3>Options</h3>");

            builder.Append("<table cellspacing=\"0\">");


            int count = 0;

            while (count < random10Options.Count())
            {
                if (count % 2 == 0)
                    builder.Append("<tr>");
                builder.Append("<td bgcolor=\"" + insertTemplate + "\">" + random10Options.ElementAt(count) + "</td>");
                if (random10Options.Count() > count + 1)
                    builder.Append("<td bgcolor=\"" + insertTemplate + "\">" + random10Options.ElementAt(count + 1) +
                                   "</td>");

                if (count % 2 == 0)
                    builder.Append("<tr>");
                builder.Append("</tr>");

                count = count + 2;
            }

            builder.Append("</font>");

            builder.Append("</table>");

            builder.Append("</td>");

            return builder.ToString();
        }


        public static List<string> GetImagesForAudi(WhitmanEntepriseMasterVehicleInfo vehicle, string fontColor)
        {

            var filterImage = new List<string>
                                  {
                                      "http://webpics.us/WaltersAudiLogo/A3lease.png",
                                      "http://webpics.us/WaltersAudiLogo/A4lease.png",
                                      "http://webpics.us/WaltersAudiLogo/A5lease.png",
                                      "http://webpics.us/WaltersAudiLogo/A6lease.png",
                                      "http://webpics.us/WaltersAudiLogo/A7lease.png",
                                      "http://webpics.us/WaltersAudiLogo/A8lease.png"
                                  };

            var list = new List<string>();

            foreach (var imgTmp in filterImage)
            {

                var builder = new StringBuilder();

                builder.Append("<td valign=\"top\" bgcolor=\"white\">");

                builder.Append("<table>");

                builder.Append("<tr>");

                builder.Append("<td>");

                builder.Append("<a href=\"" + vehicle.WebSiteUrl +
                               "\" target=\"_blank\"><img border=\"0\" src=\"" + imgTmp.Trim() +
                               "\" width=\"500\"   /></a>");

                builder.Append("</td>");

                builder.Append("</tr>");

                builder.Append("</table>");

                builder.Append("</font></td>");

                list.Add(builder.ToString());

            }

            return list;

        }

        public static string GenerateImages(WhitmanEntepriseMasterVehicleInfo vehicle, string fontColor, string insertTemplate)
        {
            string tmp = vehicle.CarImageUrl;

            var totalImage =
                tmp.Split(new String[] { "|", "," }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var random = new Random();

            var builder = new StringBuilder();

            builder.Append("<td valign=\"top\" bgcolor=\"" + insertTemplate + "\">");

            builder.Append("<font color=\"" + fontColor +
                           "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h3>Images</h3>");

            builder.Append("<table>");

            int count = 0;

            int randomNumber = random.Next(1000, 9999);

            string queryString = "Stock=" + vehicle.StockNumber + "&Vin=" + vehicle.Vin + "&partner=" +
                                 vehicle.DealerId + randomNumber;

            if (totalImage.Count() >= 4)
            {
                var randomOptions = totalImage.OrderBy(t => Guid.NewGuid()).ToList().Take(4);

                while (count < randomOptions.Count())
                {
                    if (count % 2 == 0)
                        builder.Append("<tr>");
                    builder.Append("<td>");


                    if (vehicle.DealerId==4310 ||vehicle.DealerId==5780 ||vehicle.DealerId==1660)


                        builder.Append(
                            "<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" +
                            queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" +
                            randomOptions.ElementAt(count).Trim() + "\" width=\"250\"   /></a>");

                    else
                        builder.Append("<a href=\"" + vehicle.WebSiteUrl +
                                       "\" target=\"_blank\"><img border=\"0\" src=\"" +
                                       randomOptions.ElementAt(count).Trim() + "\" width=\"250\"   /></a>");




                    builder.Append("</td>");
                    builder.Append("<td>");

                    if (vehicle.DealerId == 4310 || vehicle.DealerId == 5780 || vehicle.DealerId == 1660)



                        builder.Append(
                            "<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" +
                            queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" +
                            randomOptions.ElementAt(count + 1).Trim() + "\" width=\"250\"   /></a>");

                    else
                        builder.Append("<a href=\"" + vehicle.WebSiteUrl +
                                       "\" target=\"_blank\"><img border=\"0\" src=\"" +
                                       randomOptions.ElementAt(count + 1).Trim() + "\" width=\"250\"   /></a>");




                    builder.Append("</td>");

                    if (count % 2 == 0)
                        builder.Append("<tr>");
                    builder.Append("</tr>");

                    count = count + 2;
                }
            }
            else
            {
                if (totalImage.Count() == 1)
                {
                    builder.Append("<tr>");
                    builder.Append("<td>");
                    if (vehicle.DealerId == 4310 || vehicle.DealerId == 5780 || vehicle.DealerId == 1660)


                        builder.Append(
                            "<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" +
                            queryString + "\" target=\"_blank\"><img  border=\"0\" src=\"" +
                            totalImage.ElementAt(0).Trim() + "\" width=\"250\"   /></a>");

                    else
                        builder.Append("<a href=\"" + vehicle.WebSiteUrl +
                                       "\" target=\"_blank\"><img  border=\"0\" src=\"" + totalImage.ElementAt(0).Trim() +
                                       "\" width=\"250\"   /></a>");




                    builder.Append("</td>");
                    builder.Append("</tr>");
                }
                else if (totalImage.Count() == 2)
                {
                    builder.Append("<tr>");
                    builder.Append("<td>");
                    if (vehicle.DealerId == 4310 || vehicle.DealerId == 5780 || vehicle.DealerId == 1660)


                        builder.Append(
                            "<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" +
                            queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" +
                            totalImage.ElementAt(0).Trim() + "\" width=\"250\"   /></a>");


                    else
                        builder.Append("<a href=\"" + vehicle.WebSiteUrl +
                                       "\" target=\"_blank\"><img border=\"0\" src=\"" + totalImage.ElementAt(0).Trim() +
                                       "\" width=\"250\"   /></a>");




                    builder.Append("</td>");
                    builder.Append("<td>");


                    if (vehicle.DealerId == 4310 || vehicle.DealerId == 5780 || vehicle.DealerId == 1660)


                        builder.Append(
                            "<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" +
                            queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" +
                            totalImage.ElementAt(1).Trim() + "\" width=\"250\"   /></a>");



                    else
                        builder.Append("<a href=\"" + vehicle.WebSiteUrl +
                                       "\" target=\"_blank\"><img border=\"0\" src=\"" + totalImage.ElementAt(1).Trim() +
                                       "\" width=\"250\"   /></a>");


                    builder.Append("</td>");
                    builder.Append("</tr>");
                }
                else if (totalImage.Count() == 3)
                {
                    builder.Append("<tr>");
                    builder.Append("<td>");

                    if (vehicle.DealerId == 4310 || vehicle.DealerId == 5780 || vehicle.DealerId == 1660)

                        builder.Append(
                            "<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" +
                            queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" +
                            totalImage.ElementAt(0).Trim() + "\" width=\"250\"   /></a>");



                    else
                        builder.Append("<a href=\"" + vehicle.WebSiteUrl +
                                       "\" target=\"_blank\"><img border=\"0\" src=\"" + totalImage.ElementAt(0).Trim() +
                                       "\" width=\"250\"   /></a>");



                    builder.Append("</td>");
                    builder.Append("<td>");

                    if (vehicle.DealerId == 4310 || vehicle.DealerId == 5780 || vehicle.DealerId == 1660)

                        builder.Append(
                            "<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" +
                            queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" +
                            totalImage.ElementAt(1).Trim() + "\" width=\"250\"   /></a>");



                    else
                        builder.Append("<a href=\"" + vehicle.WebSiteUrl +
                                       "\" target=\"_blank\"><img border=\"0\" src=\"" + totalImage.ElementAt(1).Trim() +
                                       "\" width=\"250\"   /></a>");



                    builder.Append("</td>");
                    builder.Append("</tr>");
                    builder.Append("<tr>");
                    builder.Append("<td>");
                    if (vehicle.DealerId == 4310 || vehicle.DealerId == 5780 || vehicle.DealerId == 1660)


                        builder.Append(
                            "<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" +
                            queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" +
                            totalImage.ElementAt(2).Trim() + "\" width=\"250\"   /></a>");



                    else
                        builder.Append("<a href=\"" + vehicle.WebSiteUrl +
                                       "\" target=\"_blank\"><img border=\"0\" src=\"" + totalImage.ElementAt(2).Trim() +
                                       "\" width=\"250\"   /></a>");

                    builder.Append("</td>");
                    builder.Append("</tr>");
                }
                else
                {
                    builder.Append("<tr>");
                    builder.Append("<td>");
                    if (String.IsNullOrEmpty(vehicle.DefaultImageUrl))
                        builder.Append("<a href=\"" + vehicle.WebSiteUrl +
                                       "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/gloss/no-image-available.jpg\" width=\"250\"   /></a>");
                    else
                        builder.Append(
                            "<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" +
                            queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" + vehicle.DefaultImageUrl +
                            "\" width=\"250\"   /></a>");
                    builder.Append("</td>");
                    builder.Append("</tr>");
                }
            }


            builder.Append("</table>");

            builder.Append("</font></td>");

            return builder.ToString();
        }

        public static string GenerateThumbnail(WhitmanEntepriseMasterVehicleInfo vehicle)
        {
            string tmp = vehicle.CarImageUrl;

            string[] totalImage = tmp.Split(new String[] { "|", "," }, StringSplitOptions.RemoveEmptyEntries);

            var random = new Random();

            int randomNumber = random.Next(1000, 9999);

            string queryString = "Stock=" + vehicle.StockNumber + "&Vin=" + vehicle.Vin + "&partner=" +
                                 vehicle.DealerId + randomNumber;


            var builder = new StringBuilder();

            if (totalImage.Any())
            {
                if (vehicle.DealerId == 4310 || vehicle.DealerId == 5780 || vehicle.DealerId == 1660)


                    builder.Append("<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" +
                                   queryString + "\" target=\"_blank\"><img src=\"" + totalImage.First() +
                                   "\" height=\"150\" /></a>");

                else
                    builder.Append("<a href=\"" + vehicle.WebSiteUrl +
                                   "\" target=\"_blank\"><img src=\"" + totalImage.First() + "\" height=\"150\" /></a>");



            }
            else
            {
                if (String.IsNullOrEmpty(vehicle.DefaultImageUrl))
                    builder.Append("<a href=\"" + vehicle.WebSiteUrl +
                                   "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/gloss/no-image-available.jpg\" height=\"150\"   /></a>");
                else
                {
                    if (vehicle.DealerId == 4310 || vehicle.DealerId == 5780 || vehicle.DealerId == 1660)

                        builder.Append(
                            "<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" +
                            queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" + vehicle.DefaultImageUrl +
                            "\" height=\"150\"   /></a>");

                    else
                        builder.Append("<a href=\"" + vehicle.WebSiteUrl +
                                       "\" target=\"_blank\"><img border=\"0\" src=\"" + vehicle.DefaultImageUrl +
                                       "\" height=\"150\"   /></a>");


                }
            }

            return builder.ToString();
        }

        public static string GenerateDescription(WhitmanEntepriseMasterVehicleInfo vehicle, string fontColor,
                                                 string insertTemplate)
        {

            var builder = new StringBuilder();

            bool hasDescription = !String.IsNullOrEmpty(vehicle.Description);

            if (hasDescription)

                builder.Append("<td valign=\"top\" width=\"550\" bgcolor=\"" + insertTemplate + "\"><font color=\"" +
                               fontColor +
                               "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h3>Descriptions</h3>" +
                               vehicle.Description + "</font></td>");

            else
                builder.Append("<td valign=\"top\" width=\"550\" bgcolor=\"" + insertTemplate + "\"><font color=\"" +
                               fontColor +
                               "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h3>Descriptions</h3>" +
                               "CALL US FOR MORE INFO ABOUT VEHICLE" + "</font></td>");

            return builder.ToString();
        }



        public static string GenerateImageURL()
        {
            const string randomNumber = "AzByCxDwEvFuGtHsIrJqKpLoMnNmOlPkQjRiShTgUfVeWdXcYbZa1234567890";

            var random = new Random();

            var builder = new StringBuilder();


            for (int i = 0; i < 15; i++)
                builder.Append(randomNumber[random.Next(randomNumber.Length)].ToString(CultureInfo.InvariantCulture));

            return builder.ToString();



        }

        public static string GenerateImageFileName()
        {
            const string randomNumber = "MnNmOlPkQjRiShTyCxDwEvFugUfVeWdXcYbZaAzByCxDwEvFuGtHsIrJqKpLo1234567890";

            var random = new Random();

            var builder = new StringBuilder();

            for (int i = 0; i < 12; i++)
                builder.Append(randomNumber[random.Next(randomNumber.Length)].ToString(CultureInfo.InvariantCulture));

            builder.Append(".jpg");

            return builder.ToString();



        }
        public static string GenerateBottomImageFileName()
        {
            const string randomNumber = "MnNmOliShTyCxvFuGtHsIrJqKpL6789o12345DwEvFugUfVeWlPkQjRdXPkQjRcYbZaAzByCxDwE0";

            var random = new Random();

            var builder = new StringBuilder();

            for (int i = 0; i < 12; i++)
                builder.Append(randomNumber[random.Next(randomNumber.Length)].ToString(CultureInfo.InvariantCulture));

            builder.Append(".jpg");

            return builder.ToString();



        }

        public static string GenerateRandomTextFromBook(WhitmanEntepriseMasterVehicleInfo vehicle)
        {

            var builder = new StringBuilder();

            string charsToUse = clsVariables.chunkList.First(t => t.ChunkName.Equals("CrazyLetters")).ChunkStringValue;


            try
            {

                var random = new Random();
                for (var i = 0; i < 100; i++)
                    builder.Append(charsToUse[random.Next(charsToUse.Length)].ToString(CultureInfo.InvariantCulture));



                return builder.ToString();
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                //Console.WriteLine("The file could not be read:");
                //Console.WriteLine(e.Message);
            }
            return builder.ToString();
        }


        public static string GenerateRandomTextFromBookOnTop()
        {

            var builder = new StringBuilder();

            const string charsToUse = "~!@#$%^&*()AzByCxDwEvFuGtHsIrJqKpLoMnNmOlPkQjRiShTgUfVeWdXcYbZa1234567890";

            try
            {

                var random = new Random();

                for (int i = 0; i < 150; i++)
                    builder.Append(charsToUse[random.Next(charsToUse.Length)].ToString(CultureInfo.InvariantCulture));
                return builder.ToString();
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return builder.ToString();
        }


        public static string GenerateHtmlImageCodeOverlay(WhitmanEntepriseMasterVehicleInfo vehicle,string imgUrl)
        {
            var builder = new StringBuilder();

            builder.Append("<html>");

            builder.Append("<head>");

            builder.Append("  <title></title>");

            builder.Append("  <style type=\"text/css\">");

            builder.Append(" .image-container {    width: 560px;height: 430px;      font-family: sans-serif;     overflow: hidden;   position: relative;padding: 20px;background: url('http://vinclapp.net/alpha/bg.jpg') top left no-repeat;padding-bottom: 0px;}");

            builder.Append(".image {text-align: center;background: #222;}.image img {height: 350px;}");

            builder.Append("  .top-overlay {clear: both;overflow: hidden;background: url('http://vinclapp.net/alpha/red-repeat.jpg') top left repeat-x;text-shadow: 0px 0px 4px darkred;color: #ffffff;padding: 10px;width: 540px;margin: 0 auto;}");

            builder.Append(".top-overlay h1, h3 {display: inline-block;padding: 0;margin: 0;text-shadow: 1px 1px 3px black;margin: 0px;}.top-overlay h1 {float: right;text-align: right;width: 35%;font-size: 1.2em;}.top-overlay h3 {float: left;width: 65%;font-size: 1.0em;}.bottom-overlay {text-align: center;color: white;padding: 5px;font-size: .9em;}");

            builder.Append(".thumbnail {width: 261px;overflow: hidden;float: left;background: #000;text-align: center;}.thumbnail img {height: 181px;}.vehicle-info {width:48%;position: absolute;top: 20px;right: 20px;margin: 0;padding: 0;list-style-type: none;overflow: hidden;}.top-description {overflow: hidden;}.vehicle-info h4 {margin: 0; padding: 0;}.vehicle-info li {padding: 7px;padding-left: 8px;background: #333;color: white;font-size: .7em;}.vehicle-info li:nth-child(2n) {background: #444;}");

            builder.Append(" .vehicle-info .header {background: url('http://vinclapp.net/alpha/red-repeat.jpg') top left repeat-x;text-shadow: 0px 0px 4px darkred;color: white;margin-bottom: 12px;}");

            builder.Append(" .bottom-description {background: black;color: white;margin-bottom: 0;margin-top:0;}.bottom-description h1 {text-align: center;background: url('red-repeat.jpg') top left repeat-x;text-shadow: 0px 0px 4px darkred;padding: 10px;margin: 0px;margin-top: 12px;font-size: 1.4em;}.bottom-description p {font-size: .70em;overflow: hidden;margin-top: 0px;padding: 10px;background: #222222;border-top: 1px black solid;}</style></head>");

            builder.Append("<body>");

            builder.Append("<div class=\"image-container\">");

            builder.Append("  <div class=\"top-overlay\">");

            builder.Append(" <h3>"+vehicle.DealershipName+"</h3>");

            builder.Append("<h1>"+vehicle.PhoneNumber+"</h1>");

            builder.Append("  </div>");

            builder.Append("  <div class=\"image\"><img src=" + imgUrl + "></div>");

            builder.Append("  <div class=\"bottom-overlay\">");

            builder.Append(vehicle.StreetAddress +" " + vehicle.City + ", "  +vehicle.State + " " + vehicle.ZipCode);

            builder.Append(" </div>");

            builder.Append("</div>");

            builder.Append("</body>");

            builder.Append("</html>");


            return builder.ToString();
        }

        public static string GenerateHtmlImageCodeOverlayForFullerton(WhitmanEntepriseMasterVehicleInfo vehicle, string imgUrl)
        {
            var builder = new StringBuilder();

            builder.Append("<html>");

            builder.Append("<head>");

            builder.Append("  <title></title>");

            builder.Append("  <style type=\"text/css\">");

            builder.Append(" #super-simple {width: 600px;height: 450px;background: white;overflow: hidden;position: relative;}");

            builder.Append("   #super-simple .image-box {height: 393px;width: 600px;display: table-cell;vertical-align: middle;text-align: center;}");

            builder.Append(" #super-simple .image-box img {max-width: 100%;max-height: 100%;vertical-align: middle;}");


            builder.Append(" #super-simple .footer {text-align: center;position: absolute;bottom: 0;padding-top: 10px;padding-bottom: 10px;width: 100%;font-size: 2em;font-weight: bold;color: black;background: rgba(255,255,255,1);}");

            builder.Append("</style>");

            builder.Append("</head>");

            builder.Append("<body>");

            builder.Append("<div id=\"super-simple\">");



            builder.Append("  <div class=\"image-box\">");

            builder.Append(" <img src=\""+imgUrl+"\"/>");

            builder.Append(" </div>");

            builder.Append("  <div class=\"footer\">");

            builder.Append("Stock# " + vehicle.StockNumber + "    Call Now! - " + vehicle.PhoneNumber);

            builder.Append(" </div>");

            builder.Append("</div>");

            
            builder.Append("</body>");

            builder.Append("</html>");


            return builder.ToString();
        }

        public static string GenerateHtmlImageCodeOverlayForRandomSimilarCars(WhitmanEntepriseMasterVehicleInfo vehicle, string imgUrl)
        {
            var builder = new StringBuilder();

            builder.Append("<html>");

            builder.Append("<head>");

            builder.Append("  <title></title>");

            builder.Append("  <style type=\"text/css\">");

            builder.Append(" #super-simple {width: 600px;height: 450px;background: white;overflow: hidden;position: relative;}");

            builder.Append("   #super-simple .image-box {height: 393px;width: 600px;display: table-cell;vertical-align: middle;text-align: center;}");

            builder.Append(" #super-simple .image-box img {max-width: 100%;max-height: 100%;vertical-align: middle;}");


            builder.Append(" #super-simple .footer {text-align: center;position: absolute;bottom: 0;padding-top: 10px;padding-bottom: 10px;width: 100%;font-size: 2em;font-weight: bold;color: black;background: rgba(255,255,255,1);}");

            builder.Append("</style>");

            builder.Append("</head>");

            builder.Append("<body>");

            builder.Append("<div id=\"super-simple\">");

       
            builder.Append("  <div class=\"image-box\">");

            builder.Append(" <img src=\"" + imgUrl + "\"/>");

            builder.Append(" </div>");

            builder.Append("  <div class=\"footer\">");

            builder.Append("Stock# " +vehicle.StockNumber +"    Call Now! - " + vehicle.PhoneNumber);

            builder.Append(" </div>");

            builder.Append("</div>");


            builder.Append("</body>");

            builder.Append("</html>");


            return builder.ToString();
        }

        public static string GenerateHtmlImageCodeSnapshotInfo(WhitmanEntepriseMasterVehicleInfo vehicle)
        {
            var builder = new StringBuilder();
            
            builder.Append("<html>");

            builder.Append("<head>");

            builder.Append("  <title></title>");

            builder.Append("  <style type=\"text/css\">");

            builder.Append(" .image-container {    width: 560px;height: 430px;      font-family: sans-serif;     overflow: hidden;   position: relative;padding: 20px;background: url('http://vinclapp.net/alpha/bg.jpg') top left no-repeat;padding-bottom: 0px;}");

            builder.Append(".image {text-align: center;background: #222;}.image img {height: 350px;}");

            builder.Append("  .top-overlay {clear: both;overflow: hidden;background: url('http://vinclapp.net/alpha/red-repeat.jpg') top left repeat-x;text-shadow: 0px 0px 4px darkred;color: #ffffff;padding: 10px;width: 540px;margin: 0 auto;}");

            builder.Append(".top-overlay h1, h3 {display: inline-block;padding: 0;margin: 0;text-shadow: 1px 1px 3px black;margin: 0px;}.top-overlay h1 {float: right;text-align: right;width: 35%;font-size: 1.2em;}.top-overlay h3 {float: left;width: 65%;font-size: 1.0em;}.bottom-overlay {text-align: center;color: white;padding: 5px;font-size: .9em;}");

            builder.Append(".thumbnail {width: 261px;overflow: hidden;float: left;background: #000;text-align: center;}.thumbnail img {height: 181px;}.vehicle-info {width:48%;position: absolute;top: 20px;right: 20px;margin: 0;padding: 0;list-style-type: none;overflow: hidden;}.top-description {overflow: hidden;}.vehicle-info h4 {margin: 0; padding: 0;}.vehicle-info li {padding: 7px;padding-left: 8px;background: #333;color: white;font-size: .7em;}.vehicle-info li:nth-child(2n) {background: #444;}");

            builder.Append(" .vehicle-info .header {background: url('http://vinclapp.net/alpha/red-repeat.jpg') top left repeat-x;text-shadow: 0px 0px 4px darkred;color: white;margin-bottom: 12px;}");

            builder.Append(" .bottom-description {background: black;color: white;margin-bottom: 0;margin-top:0;}.bottom-description h1 {text-align: center;background: url('red-repeat.jpg') top left repeat-x;text-shadow: 0px 0px 4px darkred;padding: 10px;margin: 0px;margin-top: 12px;font-size: 1.4em;}.bottom-description p {font-size: .70em;overflow: hidden;margin-top: 0px;padding: 10px;background: #222222;border-top: 1px black solid;}</style></head>");

            builder.Append("<body>");
            builder.Append("<div class=\"image-container\">");

            builder.Append("  <div class=\"top-description\">");

            builder.Append("    <div class=\"thumbnail\">");

            builder.Append("      <img src=\"" + vehicle.FirstImageUrl + "\">");

            builder.Append(" </div>");

            builder.Append("    <ul class=\"vehicle-info\">");

            builder.Append("   <li class=\"header\"><h4>" + vehicle.ModelYear + " " + vehicle.Make + " " + vehicle.Model + " " + vehicle.Trim + "</h4></li>");

            builder.Append("  <li><b>Sale Price: </b>" + vehicle.SalePrice + "</li>");

            builder.Append("  <li><b>Mileage: </b>" + vehicle.Mileage + "</li>");

            builder.Append("  <li><b>Ext. Color: </b>" + vehicle.ExteriorColor + "e</li>");

            builder.Append("  <li><b>Transmission: </b>" + vehicle.Tranmission + "</li>");

            builder.Append("    <li><b>Stock: </b>" + vehicle.StockNumber + "</li>");
            builder.Append("   </ul>");

            builder.Append("  </div>");

            builder.Append("  <div class=\"bottom-description\">");

            //builder.Append("<h1>" + vehicle.WebSiteUrl+ "</h1>");

            builder.Append("     <h1>For more information call Dealer at " + vehicle.PhoneNumber + "</h1>");

           

            builder.Append("<h3>Price is subject to change without notice</h3>");

            builder.Append(" </div>");

            builder.Append("</div>");

            builder.Append("</body>");

            builder.Append("</html>");



            return builder.ToString();
        }

        public static string GenerateHtmlImageCodeSquare4Pictures(List<WhitmanEntepriseMasterVehicleInfo> squareRandom)
        {
            

            var builder = new StringBuilder();

            builder.Append("<html>");

            builder.Append("<head>");

            builder.Append("<title>VinPage</title>");

            builder.Append("  <style type=\"text/css\">");

            builder.Append(".container {width: 560px;height: 410px;padding: 20px;background: black;font-family: arial;}");


            builder.Append(".image-box {width: 265px;margin: 6px;height: 190px;background: white;display: inline-block;overflow: hidden;}");

            builder.Append(".image-box .header {height: 18px;padding-top: 2px;background: green;color: white;font-weight: bold;text-align: center;text-shadow: 0px 0px 4px darkgreen;}");

            builder.Append(
                ".image-box .footer {height: 18px;padding-top: 2px;background: #ffffff;background: -moz-linear-gradient(top, #ffffff 0%, #f3f3f3 50%, #ededed 51%, #ffffff 100%); background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#ffffff), color-stop(50%,#f3f3f3), color-stop(51%,#ededed), color-stop(100%,#ffffff));background: -webkit-linear-gradient(top, #ffffff 0%,#f3f3f3 50%,#ededed 51%,#ffffff 100%); background: -o-linear-gradient(top, #ffffff 0%,#f3f3f3 50%,#ededed 51%,#ffffff 100%); background: -ms-linear-gradient(top, #ffffff 0%,#f3f3f3 50%,#ededed 51%,#ffffff 100%); background: linear-gradient(to bottom, #ffffff 0%,#f3f3f3 50%,#ededed 51%,#ffffff 100%);filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#ffffff', endColorstr='#ffffff',GradientType=0 ); text-align: center;font-size: .6em;}");

            builder.Append(".image {height: 150px;text-align: center;}.image img {height: 100%;} ");
            builder.Append("</style> ");

            builder.Append("</head>");
            builder.Append(" <body>");



            builder.Append(" <div class=\"container\">");

            builder.Append("<div class=\"image-box one\"> ");
            if (String.IsNullOrEmpty(squareRandom.ElementAt(0).SalePrice))
                builder.Append(" 	<div class=\"header\">Call for Price</div>");
            else
            {
                if (squareRandom.ElementAt(0).SalePrice.Equals("0"))
                    builder.Append(" 	<div class=\"header\">Call for Price</div>");
                else

                builder.Append(" 	<div class=\"header\">Sale Price : " + squareRandom.ElementAt(0).SalePrice + "</div>");    
            }

            builder.Append("<div class=\"image\"><img src=\"" + squareRandom.ElementAt(0).FirstImageUrl + "\"></img></div> ");

            string firstCarTitle = squareRandom.ElementAt(0).ModelYear + " " + squareRandom.ElementAt(0).Make + " " +
                                   squareRandom.ElementAt(0).Model + " " + squareRandom.ElementAt(0).Trim;

            if (firstCarTitle.Length > 32)
            {
                if (squareRandom.ElementAt(0).Make.ToLower().Equals("bmw") ||
                    squareRandom.ElementAt(0).Make.ToLower().Equals("mercedes-benz"))
                {
                    firstCarTitle = squareRandom.ElementAt(0).ModelYear + " " + squareRandom.ElementAt(0).Make + " " +
                                    squareRandom.ElementAt(0).Trim;
                }
                else
                {
                    firstCarTitle = squareRandom.ElementAt(0).ModelYear + " " + squareRandom.ElementAt(0).Make + " " +
                                    squareRandom.ElementAt(0).Model;
                }
            }

            builder.Append("<div class=\"footer\">" + firstCarTitle +"</div>	 ");
            builder.Append(" </div>");

            builder.Append("<div class=\"image-box two\"> ");
            if (String.IsNullOrEmpty(squareRandom.ElementAt(1).SalePrice))
                builder.Append(" 	<div class=\"header\">Call for Price</div>");
            else
            {
                if (squareRandom.ElementAt(1).SalePrice.Equals("0"))
                    builder.Append(" 	<div class=\"header\">Call for Price</div>");
                else
                builder.Append(" 	<div class=\"header\">Sale Price : " + squareRandom.ElementAt(1).SalePrice + "</div>");
            }
            builder.Append("<div class=\"image\"><img src=\"" + squareRandom.ElementAt(1).FirstImageUrl + "\"></img></div> ");

            string secondCarTitle = squareRandom.ElementAt(1).ModelYear + " " + squareRandom.ElementAt(1).Make + " " +
                                 squareRandom.ElementAt(1).Model + " " + squareRandom.ElementAt(1).Trim;

            if (secondCarTitle.Length > 32)
            {
                if (squareRandom.ElementAt(1).Make.ToLower().Equals("bmw") ||
                    squareRandom.ElementAt(1).Make.ToLower().Equals("mercedes-benz"))
                {
                    secondCarTitle = squareRandom.ElementAt(1).ModelYear + " " + squareRandom.ElementAt(1).Make + " " +
                                    squareRandom.ElementAt(1).Trim;
                }
                else
                {
                    secondCarTitle = squareRandom.ElementAt(1).ModelYear + " " + squareRandom.ElementAt(1).Make + " " +
                                    squareRandom.ElementAt(1).Model;
                }
            }

            builder.Append("<div class=\"footer\">" + secondCarTitle + "</div>	 ");
            builder.Append(" </div>");

            builder.Append("<div class=\"image-box three\"> ");
            if (String.IsNullOrEmpty(squareRandom.ElementAt(2).SalePrice))
                builder.Append(" 	<div class=\"header\">Call for Price</div>");
            else
            {
                if (squareRandom.ElementAt(2).SalePrice.Equals("0"))
                    builder.Append(" 	<div class=\"header\">Call for Price</div>");
                else
                builder.Append(" 	<div class=\"header\">Sale Price : " + squareRandom.ElementAt(2).SalePrice + "</div>");
            }
            builder.Append("<div class=\"image\"><img src=\"" + squareRandom.ElementAt(2).FirstImageUrl + "\"></img></div> ");

            string thirdCarTitle = squareRandom.ElementAt(2).ModelYear + " " + squareRandom.ElementAt(2).Make + " " +
                                 squareRandom.ElementAt(2).Model + " " + squareRandom.ElementAt(2).Trim;

            if (thirdCarTitle.Length > 32)
            {
                if (squareRandom.ElementAt(2).Make.ToLower().Equals("bmw") ||
                    squareRandom.ElementAt(2).Make.ToLower().Equals("mercedes-benz"))
                {
                    thirdCarTitle = squareRandom.ElementAt(2).ModelYear + " " + squareRandom.ElementAt(2).Make + " " +
                                    squareRandom.ElementAt(2).Trim;
                }
                else
                {
                    thirdCarTitle = squareRandom.ElementAt(2).ModelYear + " " + squareRandom.ElementAt(2).Make + " " +
                                    squareRandom.ElementAt(2).Model;
                }
            }

            builder.Append("<div class=\"footer\">" + thirdCarTitle + "</div>	 ");
            builder.Append(" </div>");

            builder.Append("<div class=\"image-box four\"> ");
            if (String.IsNullOrEmpty(squareRandom.ElementAt(3).SalePrice))
                builder.Append(" 	<div class=\"header\">Call for Price</div>");
            else
            {
                if (squareRandom.ElementAt(3).SalePrice.Equals("0"))
                    builder.Append(" 	<div class=\"header\">Call for Price</div>");
                else
                builder.Append(" 	<div class=\"header\">Sale Price : " + squareRandom.ElementAt(3).SalePrice + "</div>");
            }
            builder.Append("<div class=\"image\"><img src=\"" + squareRandom.ElementAt(3).FirstImageUrl + "\"></img></div> ");

            string fourthCarTitle = squareRandom.ElementAt(3).ModelYear + " " + squareRandom.ElementAt(3).Make + " " +
                                 squareRandom.ElementAt(3).Model + " " + squareRandom.ElementAt(3).Trim;

            if (fourthCarTitle.Length > 32)
            {
                if (squareRandom.ElementAt(3).Make.ToLower().Equals("bmw") ||
                    squareRandom.ElementAt(3).Make.ToLower().Equals("mercedes-benz"))
                {
                    fourthCarTitle = squareRandom.ElementAt(3).ModelYear + " " + squareRandom.ElementAt(3).Make + " " +
                                    squareRandom.ElementAt(3).Trim;
                }
                else
                {
                    fourthCarTitle = squareRandom.ElementAt(3).ModelYear + " " + squareRandom.ElementAt(3).Make + " " +
                                    squareRandom.ElementAt(3).Model;
                }
            }

            builder.Append("<div class=\"footer\">" + fourthCarTitle + "</div>	 ");
            builder.Append(" </div>");

            builder.Append("</div> ");
            builder.Append(" </body>");
            builder.Append(" </html>");


            return builder.ToString();
        }


        public static string GenerateHtmlImageCodeSnapshotInfoLayout1(WhitmanEntepriseMasterVehicleInfo vehicle)
        {
            var builder = new StringBuilder();


            string websiteurl = "";
            if (!String.IsNullOrEmpty(vehicle.WebSiteUrl) && vehicle.WebSiteUrl.IndexOf(".com/", System.StringComparison.Ordinal) > 0)

              websiteurl =vehicle.WebSiteUrl.Substring(0,vehicle.WebSiteUrl.IndexOf("/", System.StringComparison.Ordinal));

            builder.Append("<html>");

            builder.Append("<head>");

            builder.Append("  <title></title>");

            builder.Append("  <style type=\"text/css\">");

            builder.Append(
                " .ad-container {width: 600px;height: 450px;background: white;overflow: hidden;position: relative;border: 1px solid black;}.ad-container .image-box {display: table-cell;vertical-align: middle;text-align: center;}.ad-container .image-box img {max-width: 100%;max-height: 100%;vertical-align: middle;}.image-box.double img{width: 49%;}/* Layout 1 */#super-simple .image-box {height: 393px;width: 600px;}#super-simple .footer {text-align: center;position: absolute;bottom: 0;padding-top: 10px;padding-bottom: 10px;width: 100%;font-size: 2em;font-weight: bold;color: black;background: rgba(255,255,255,1);}/* Layout 2 */#image-only .image-box {height: 365px;width: 600px;}#image-only .header, #image-only .footer {padding-top: 8px;padding-bottom: 8px;text-align: center;font-size: 1.5em;/*text-shadow: 2px 1px 1px black;*/font-weight: bolder;/*color: white;*/background: white;}/* Layout 3 */#two-image .image-box {height: 221px;width: 600px;}#two-image .header, #two-image .footer {padding-top: 8px;padding-bottom: 8px;text-align: center;font-size: 1.5em;/*text-shadow: 2px 1px 1px black;*/font-weight: bolder;/*color: white;*/background: white;}#two-image .content {padding: 10px;font-size: .9em}/* Layout 4 */#stacked-double .image-box {height: 442px;width: 300px;float: left;}#stacked-double .header, #stacked-double .footer {padding-top: 8px;padding-bottom: 8px;text-align: center;font-size: 1.5em;/*text-shadow: 2px 1px 1px black;*/font-weight: bolder;/*color: white;*/background: white;}#stacked-double .header {width: 300px;float: right;}#stacked-double .content {padding: 10px;font-size: .9em;width: 275px;float: right;overflow: hidden;}#stacked-double ul {font-size: 1.4em;list-style-type: none;margin: 0;padding: 0;padding-bottom: 30px;padding-top: 30px;text-align: center;}");

            builder.Append("  </style></head><body>");
            builder.Append("<div id=\"image-only\" class=\"ad-container\"><div class=\"header\">Visit us at " + websiteurl + "!</div><div class=\"image-box\"><img src=\"" + vehicle.FirstImageUrl + "\"></div><div class=\"footer\">Call Now! - " + vehicle.PhoneNumber + "</div></div>");

            builder.Append("</body>");

            builder.Append("</html>");


            return builder.ToString();
        }
        public static string GenerateHtmlImageCodeSnapshotInfoLayout2(WhitmanEntepriseMasterVehicleInfo vehicle)
        {
            var builder = new StringBuilder();

            builder.Append("<html>");

            builder.Append("<head>");

            builder.Append("  <title></title>");

            builder.Append("  <style type=\"text/css\">");

            builder.Append(" .ad-container {width: 600px;height: 450px;background: white;overflow: hidden;position: relative;border: 1px solid black;}.ad-container .image-box {display: table-cell;vertical-align: middle;text-align: center;}.ad-container .image-box img {max-width: 100%;max-height: 100%;vertical-align: middle;}.image-box.double img{width: 49%;}/* Layout 1 */#super-simple .image-box {height: 393px;width: 600px;}#super-simple .footer {text-align: center;position: absolute;bottom: 0;padding-top: 10px;padding-bottom: 10px;width: 100%;font-size: 2em;font-weight: bold;color: black;background: rgba(255,255,255,1);}/* Layout 2 */#image-only .image-box {height: 365px;width: 600px;}#image-only .header, #image-only .footer {padding-top: 8px;padding-bottom: 8px;text-align: center;font-size: 1.5em;/*text-shadow: 2px 1px 1px black;*/font-weight: bolder;/*color: white;*/background: white;}/* Layout 3 */#two-image .image-box {height: 221px;width: 600px;}#two-image .header, #two-image .footer {padding-top: 8px;padding-bottom: 8px;text-align: center;font-size: 1.5em;/*text-shadow: 2px 1px 1px black;*/font-weight: bolder;/*color: white;*/background: white;}#two-image .content {padding: 10px;font-size: .9em}/* Layout 4 */#stacked-double .image-box {height: 442px;width: 300px;float: left;}#stacked-double .header, #stacked-double .footer {padding-top: 8px;padding-bottom: 8px;text-align: center;font-size: 1.5em;/*text-shadow: 2px 1px 1px black;*/font-weight: bolder;/*color: white;*/background: white;}#stacked-double .header {width: 300px;float: right;}#stacked-double .content {padding: 10px;font-size: .9em;width: 275px;float: right;overflow: hidden;}#stacked-double ul {font-size: 1.4em;list-style-type: none;margin: 0;padding: 0;padding-bottom: 30px;padding-top: 30px;text-align: center;}");

            builder.Append("  </style></head><body>");

            builder.Append("<div id=\"two-image\" class=\"ad-container\"><div class=\"header\">Visit us at  " + vehicle.WebSiteUrl+ "</div><div class=\"image-box double\"><img src=\"" + vehicle.FirstImageUrl + "\"></div><div class=\"content\"><p>" + vehicle.Description + "</p></div><div class=\"footer\">Call Now! - " + vehicle.PhoneNumber + "</div></div>");

            builder.Append("</body>");

            builder.Append("</html>");


            return builder.ToString();
        }
    }









}

