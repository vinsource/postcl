using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using ClappManagementSystem.Model;

namespace ClappManagementSystem.Helper
{
    public class ComputerAccountHelper
    {
        private const string SPACE = " ";

        public static string ColorTemplate;

        public ComputerAccountHelper()
        {
            //
            // TODO: Add constructor logic here
            //
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
        public static string GenerateHTMLImageCode(WhitmanEntepriseMasterVehicleInfo vehicle)
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


            builder.Append("<table align=\"center\" width=\"750\" cellpadding=\"10\" cellspacing=\"0\" bgcolor=\"" +
                           ColorTemplate + "\">");

            builder.Append("<tr><td colspan=\"5\" align=\"center\" > <img src=\"http://vinlineup.com/468x60.gif\" /></td></tr>");


            builder.Append("<tr  bgcolor=\"" + ColorTemplate + "\">");

            builder.Append("<td ></td>");

            builder.Append(" <td colspan=\"2\">");

            builder.Append("<table width=\"725\" cellspacing=\"0\" cellpadding=\"0\">");
            builder.Append("<tr>");
            builder.Append("<td>");

            builder.Append("<a href=\"" + vehicle.WebSiteURL +
                           "\" target=\"_blank\"><img border=\"0\" src=\"" + vehicle.LogoURL +
                           "\"/></a>");
            builder.Append("</td>");

            if (vehicle.DealerId != 44670 && vehicle.DealerId != 15986)
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
                if (vehicle.DealerId == 1200)
                {
                    builder.Append(
                        "<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/ClientInfoRequest?" +
                        queryString +
                        "\" target=\"_blank\"><img border=\"0\" src=\"http://webpics.us/alpha/cListThemes/newportcoastauto/emailUsAt.png\" /></a><br />");
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
                           "\" height=\"0\" background=\"http://webpics.us/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("<td  bgcolor=\"" + ColorTemplate +
                           "\" background=\"http://webpics.us/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("<td  bgcolor=\"" + ColorTemplate +
                           "\" background=\"http://webpics.us/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("<td bgcolor=\"" + ColorTemplate +
                           "\" background=\"http://webpics.us/alpha/cListThemes/gloss/shadow.png\"></td>");
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
                            "</font></td>");
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
                           "\" target=\"_blank\"><img src=\"http://webpics.us/alpha/content/images/vin-trade-icon.gif\" width=\"150\" /></a></td>");

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
                    Globalvar.BufferMasterVehicleList.Where(x => x.DealerId == vehicle.DealerId)
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

        //public static string GenerateRandomTextBelowPic()
        //{
        //    var builder = new StringBuilder();

        //    string belowPicChunk =
        //        Globalvar..First(t => t.ChunkName.Equals("BelowPicChunk")).ChunkStringValue;



        //    var random = new Random();

        //    string[] belowPicChunkList = belowPicChunk.Split(new String[] { "," }, StringSplitOptions.RemoveEmptyEntries);

        //    builder.Append(belowPicChunkList.ElementAt(random.Next(0, belowPicChunkList.Count())));

        //    return builder.ToString();

        //}


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

            builder.Append("  <tr><td align=\"center\"><h1><font color=\"red\">Call " + vehicle.PhoneNumber + "</font></h1></td></tr>");

            builder.Append("    <tr>");

            builder.Append("<td align=\"center\">");

            builder.Append(vehicle.ModelYear + SPACE + vehicle.Make + SPACE + vehicle.Model + SPACE + "<br />");

            builder.Append("  <img  width=\"805\" src=\"" + vehicle.FirstImageUrl + "\">");

            builder.Append("</td>");

            builder.Append("</tr>");

            builder.Append(" <tr><td align=\"center\"><font color=\"red\"><h1>Call " + vehicle.PhoneNumber + "</h1></font> " + vehicle.DealershipName + "</td></tr>");

            builder.Append(" <tr>");

            builder.Append(" <td align=\"center\">");

            builder.Append("  Vehicle Location:<br />");

            builder.Append("  <font color=\"red\">" + vehicle.DealershipName + "</font><br />");

            builder.Append("  <a href=\"" + vehicle.WebSiteURL + "\">" + vehicle.WebSiteURL + "</a><br />");

            builder.Append(vehicle.StreetAddress + SPACE + vehicle.City + ", " + vehicle.State + SPACE + vehicle.ZipCode);

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

            builder.Append("<a href=\"" + vehicle.WebSiteURL +
                           "\" target=\"_blank\"><img border=\"0\" src=\"" + vehicle.LogoURL +
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

            if (vehicle.DealerId == 1200)
            {

                builder.Append("<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/ClientInfoRequest?" +
                               queryString +
                               "\" target=\"_blank\"><img border=\"0\" src=\"http://webpics.us/alpha/cListThemes/newportcoastauto/emailUsAt.png\" /></a><br />");
            }


            builder.Append("</td>");


            builder.Append("</tr>");
            builder.Append("</table>");
            builder.Append("</td>");
            builder.Append("<td></td>");

            builder.Append("</tr>");

            builder.Append("<tr>");
            builder.Append("<td  bgcolor=\"" + colorTemplate +
                           "\" height=\"0\" background=\"http://webpics.us/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("<td  bgcolor=\"" + colorTemplate +
                           "\" background=\"http://webpics.us/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("<td  bgcolor=\"" + colorTemplate +
                           "\" background=\"http://webpics.us/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("<td bgcolor=\"" + colorTemplate +
                           "\" background=\"http://webpics.us/alpha/cListThemes/gloss/shadow.png\"></td>");
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
                            "</font></td>");
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
            //                           "\" target=\"_blank\"><img border=\"0\" src=\"http://webpics.us/alpha/cListThemes/dealerimages/TextPrice.png\" /></a><br/>");
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
            //                       "\" target=\"_blank\"><img border=\"0\" src=\"http://webpics.us/alpha/cListThemes/dealerimages/TextPrice.png\" /></a><br/>");
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
                           "\" target=\"_blank\"><img src=\"http://webpics.us/alpha/cListThemes/gloss/carfax_logo.jpg\" width=\"150\" /></a></td>");

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
                                            "<li>Mileage: <em>" + vehicle.Mileage + "</em></li> ",
                                            "<li>Body Style: <em>" + vehicle.BodyType + "</em></li> ",
                                            "<li>Stock#: <em>" + vehicle.StockNumber + "</em></li> ",
                                            "<li>Engine: <em>" + vehicle.Engine + "</em></li> ",
                                            "<li>VIN: <em>" + vehicle.Vin + "</em></li> ",
                                            "<li>Exterior Color: <em>" + vehicle.ExteriorColor + "</em></li> ",
                                            "<li>Transmission: <em>" + vehicle.Tranmission + "</em></li> ",
                                            "<li>Type: <em>Used</em></li> ",


                                        });

            var randomInfoOrder = infoRandomList.OrderBy(t => Guid.NewGuid()).ToArray<string>();

            foreach (string tmp in randomInfoOrder)
            {
                builder.Append(tmp);
            }

            return builder.ToString();
        }

      
        public static string GenerateImagesForCaliforniaBeemer(WhitmanEntepriseMasterVehicleInfo vehicle)
        {
            var builder = new StringBuilder();

            string tmp = vehicle.CarImageUrl;

            var totalImage =
                tmp.Split(new String[] {"|", ",", " "}, StringSplitOptions.RemoveEmptyEntries).ToList();

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

            string[] totalOption = tmp.Split(new String[] {"|", ","}, StringSplitOptions.RemoveEmptyEntries);

            var random10Options = totalOption.OrderBy(t => Guid.NewGuid()).ToList().Take(10);


            var builder = new StringBuilder();

            builder.Append("<td valign=\"top\" bgcolor=\"" + insertTemplate + "\">");

            builder.Append("<font color=\"" + fontColor +
                           "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\" ><h3>Options</h3>");

            builder.Append("<table cellspacing=\"0\">");


            int count = 0;

            while (count < random10Options.Count())
            {
                if (count%2 == 0)
                    builder.Append("<tr>");
                builder.Append("<td bgcolor=\"" + insertTemplate + "\">" + random10Options.ElementAt(count) + "</td>");
                if (random10Options.Count() > count + 1)
                    builder.Append("<td bgcolor=\"" + insertTemplate + "\">" + random10Options.ElementAt(count + 1) +
                                   "</td>");

                if (count%2 == 0)
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

                builder.Append("<a href=\"" + vehicle.WebSiteURL +
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

        public static string GenerateImages(WhitmanEntepriseMasterVehicleInfo vehicle, string fontColor,
                                            string insertTemplate)
        {
            string tmp = vehicle.CarImageUrl;

            var totalImage =
                tmp.Split(new String[] {"|", ","}, StringSplitOptions.RemoveEmptyEntries).ToList();

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
                    if (count%2 == 0)
                        builder.Append("<tr>");
                    builder.Append("<td>");


                    if (vehicle.DealerId == 4310 || vehicle.DealerId == 5780 || vehicle.DealerId == 1660)


                        builder.Append(
                            "<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" +
                            queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" +
                            randomOptions.ElementAt(count).Trim() + "\" width=\"250\"   /></a>");

                    else
                        builder.Append("<a href=\"" + vehicle.WebSiteURL +
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
                        builder.Append("<a href=\"" + vehicle.WebSiteURL +
                                       "\" target=\"_blank\"><img border=\"0\" src=\"" +
                                       randomOptions.ElementAt(count + 1).Trim() + "\" width=\"250\"   /></a>");




                    builder.Append("</td>");

                    if (count%2 == 0)
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
                        builder.Append("<a href=\"" + vehicle.WebSiteURL +
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
                        builder.Append("<a href=\"" + vehicle.WebSiteURL +
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
                        builder.Append("<a href=\"" + vehicle.WebSiteURL +
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
                        builder.Append("<a href=\"" + vehicle.WebSiteURL +
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
                        builder.Append("<a href=\"" + vehicle.WebSiteURL +
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
                        builder.Append("<a href=\"" + vehicle.WebSiteURL +
                                       "\" target=\"_blank\"><img border=\"0\" src=\"" + totalImage.ElementAt(2).Trim() +
                                       "\" width=\"250\"   /></a>");

                    builder.Append("</td>");
                    builder.Append("</tr>");
                }
                else
                {
                    builder.Append("<tr>");
                    builder.Append("<td>");
                    if (String.IsNullOrEmpty(vehicle.DefaultImageURL))
                        builder.Append("<a href=\"" + vehicle.WebSiteURL +
                                       "\" target=\"_blank\"><img border=\"0\" src=\"http://webpics.us/alpha/cListThemes/gloss/no-image-available.jpg\" width=\"250\"   /></a>");
                    else
                        builder.Append(
                            "<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" +
                            queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" + vehicle.DefaultImageURL +
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

            string[] totalImage = tmp.Split(new String[] {"|", ","}, StringSplitOptions.RemoveEmptyEntries);

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
                    builder.Append("<a href=\"" + vehicle.WebSiteURL +
                                   "\" target=\"_blank\"><img src=\"" + totalImage.First() + "\" height=\"150\" /></a>");



            }
            else
            {
                if (String.IsNullOrEmpty(vehicle.DefaultImageURL))
                    builder.Append("<a href=\"" + vehicle.WebSiteURL +
                                   "\" target=\"_blank\"><img border=\"0\" src=\"http://webpics.us/alpha/cListThemes/gloss/no-image-available.jpg\" height=\"150\"   /></a>");
                else
                {
                    if (vehicle.DealerId == 4310 || vehicle.DealerId == 5780 || vehicle.DealerId == 1660)

                        builder.Append(
                            "<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" +
                            queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" + vehicle.DefaultImageURL +
                            "\" height=\"150\"   /></a>");

                    else
                        builder.Append("<a href=\"" + vehicle.WebSiteURL +
                                       "\" target=\"_blank\"><img border=\"0\" src=\"" + vehicle.DefaultImageURL +
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





    }
}



