using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Data;
using CraigslistManagerApp.Model;

namespace CraigslistManagerApp
{
    public sealed class CommonHelper
    {

        const string SPACE = " ";

        public CommonHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static string TrimString(string s, int maxSize)
        {
            if (s == null)
                return string.Empty;

            int count = Math.Min(s.Length, maxSize);
            return s.Substring(0, count);
        }

        private static List<string> RandomzieTable(WhitmanEntepriseVehicleInfo vehicle,string fontColor,string insertTemplate)
        {

            var list = new List<string>();

            list.Add(GenerateOptions(vehicle, fontColor, insertTemplate));
            list.Add(GenerateDescription(vehicle, fontColor, insertTemplate));
            list.Add(GenerateImages(vehicle, fontColor, insertTemplate));
            
            return list.OrderBy(t => Guid.NewGuid()).ToList();
            

        }

        public static string GenerateSimiplifiedCraiglistContent(WhitmanEntepriseVehicleInfo vehicle, string colorTemplate, string insertTemplate, bool hasPrice, string dealerTitle)
        {
            string fontColor = "lavenderblush";
            switch (insertTemplate)
            {
                case "gray":
                    fontColor = "lavenderblush";
                    break;
                case "white":
                    fontColor = "black";
                    break;
                default:
                    break;
            }


            var builder = new StringBuilder();

            List<string> listRandom = GetRandomImages(vehicle, fontColor, insertTemplate);

            Random random = new Random();

            int randomNumber = random.Next(1000, 9999);

            string queryString = "Stock=" + vehicle.StockNumber + "&Vin=" + vehicle.Vin + "&partner=" + clsVariables.currentDealer.DealerId + randomNumber;


            builder.Append("<table align=\"center\" width=\"750\" cellpadding=\"10\" cellspacing=\"0\" bgcolor=\"" + colorTemplate + "\">");

            builder.Append("<tr  bgcolor=\"" + colorTemplate + "\">");

            builder.Append("<td ></td>");

            builder.Append(" <td colspan=\"2\">");

            builder.Append("<table width=\"725\" cellspacing=\"0\" cellpadding=\"0\">");
            builder.Append("<tr>");
            builder.Append("<td>");

            builder.Append("<a href=\"" + clsVariables.currentDealer.WebSiteURL + "\" target=\"_blank\"><img border=\"0\" src=\"" + clsVariables.currentDealer.LogoURL + "\"/></a>");
            builder.Append("</td>");

            if (!clsVariables.currentDealer.DealerId.Equals("44670") && !clsVariables.currentDealer.DealerId.Equals("15986"))
            {

                builder.Append("<td align=\"right\" >");

                builder.Append("<font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h2>" + clsVariables.currentDealer.DealershipName + "</h2></font><br />");

                builder.Append("<font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h2>" + clsVariables.currentDealer.PhoneNumber + "</h2></font><br />");

                if (!String.IsNullOrEmpty(dealerTitle.Trim()))
                    builder.Append("<font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + dealerTitle + "</font><br />");

                if (clsVariables.currentDealer.DealerId.Equals("1200"))
                {

                    builder.Append("<a href=\"http://vinclapp.net/Request/ClientInfoRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/newportcoastauto/emailUsAt.png\" /></a><br />");
                }


                builder.Append("</td>");
            }

            builder.Append("</tr>");
            builder.Append("</table>");
            builder.Append("</td>");
            builder.Append("<td></td>");

            builder.Append("</tr>");

            builder.Append("<tr>");
            builder.Append("<td  bgcolor=\"" + colorTemplate + "\" height=\"0\" background=\"http://vinclapp.net/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("<td  bgcolor=\"" + colorTemplate + "\" background=\"http://vinclapp.net/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("<td  bgcolor=\"" + colorTemplate + "\" background=\"http://vinclapp.net/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("<td bgcolor=\"" + colorTemplate + "\" background=\"http://vinclapp.net/alpha/cListThemes/gloss/shadow.png\"></td>");
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
            builder.Append("  <td bgcolor=\"" + insertTemplate + "\"><font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + GenerateRandomTextBelowPic() + "</font></td>");
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

            if (!String.IsNullOrEmpty(vehicle.SalePrice))
            {

                double price = 0;

                bool validPrice = Double.TryParse(vehicle.SalePrice, out price);

                hasPrice = (!hasPrice) && validPrice && (price > 0);

                if (hasPrice)
                    builder.Append("<font size=\"+5\" color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + price.ToString("C") + "</font>");
                else
                {
                    builder.Append("<font size=\"+3\" color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + "CALL FOR PRICE" + "</font>");
                    if (clsVariables.currentDealer.DealerId.Equals("4310"))
                    {
                        string textMessageLink = clsVariables.TextMessageLink.Replace("PLACEVIN", vehicle.Vin);
                        builder.Append("<a href=\"" + textMessageLink + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/dealerimages/TextPrice.png\" /></a><br/>");
                    }
                }
            }
            else
            {
                builder.Append("<font size=\"+3\" color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + "CALL FOR PRICE" + "</font>");
                if (clsVariables.currentDealer.DealerId.Equals("4310"))
                {
                    string textMessageLink = clsVariables.TextMessageLink.Replace("PLACEVIN", vehicle.Vin);
                    builder.Append("<a href=\"" + textMessageLink + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/dealerimages/TextPrice.png\" /></a><br/>");
                }

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
            
            builder.Append("<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + clsVariables.currentDealer.StreetAddress + "</font><br />");
            builder.Append("<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + clsVariables.currentDealer.City+ ", " + clsVariables.currentDealer.State + " "  + clsVariables.currentDealer.ZipCode + "</font><br />");
            builder.Append("</td>");


            //FIRST IMAGE BEGIN

            builder.Append(listRandom.ElementAt(0));
            ////FIRST IMAGE END

            builder.Append("</tr>");

            builder.Append("<tr>");

            builder.Append("<td bgcolor=\"" + insertTemplate + "\">");


            builder.Append("<a href=\"" + clsVariables.currentDealer.WebSiteURL + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/newportcoastauto/visitOurSite.png\" /></a><br/>");
            builder.Append("<a href=\"http://vinclapp.net/Request/ClientInfoRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/newportcoastauto/moreInfo.png\" /></a><br/>");
            builder.Append("<a href=\"http://vinclapp.net/Request/ClientInfoRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/newportcoastauto/emailUs.png\" /></a><br/>");
            builder.Append("<a href=\"http://vinclapp.net/Request/ClientInfoRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/newportcoastauto/getQuote.png\" /></a><br/>");
            builder.Append("<a href=\"" + clsVariables.currentDealer.CreditURL + "\" target=\"_blank\"><img  border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/newportcoastauto/applyForCredit2.png\" /></a><br/>");
            builder.Append("</td>");

            ////SECOND IMAGE BEGINS

            builder.Append(listRandom.ElementAt(1));
            ////SECOND IMAGE END

            builder.Append("</tr>");


            builder.Append(" <tr>");
            builder.Append("<td bgcolor=\"" + insertTemplate + "\"><a href=\"http://www.carfax.com/VehicleHistory/p/Report.cfx?partner=DVW_1&vin=" + vehicle.Vin + "\" target=\"_blank\"><img src=\"http://vinclapp.net/alpha/cListThemes/gloss/carfax_logo.jpg\" width=\"150\" /></a></td>");

            ////THRID IMAGE BEGIN
            builder.Append(listRandom.ElementAt(2));
          
            ////THIRD IMAGE ENDS

            builder.Append("</tr>");

            builder.Append(" <tr>");
            builder.Append("<td valign=\"top\" bgcolor=\"" + insertTemplate + "\"></td>");
            builder.Append("<td valign=\"top\" bgcolor=\"" + insertTemplate + "\"><font size=\"1\" color=\"" + fontColor + "\" face=\"MS Serif, New York, serif\"> " + GenerateRandomTextFromBook() + "</font></td>");
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
            builder.Append(" <td bgcolor=\"" + colorTemplate + "\"></td>");
            builder.Append(" <td bgcolor=\"" + colorTemplate + "\" align=\"right\"><a href=\"http://vehicleinventorynetwork.com/\"><img border=\"0\" src=\"http://vinclapp.net/vinclapplogo/vin-logo-white2.png\" /></a></td>");
            builder.Append("<td></td>");
            builder.Append("  </tr>");
            builder.Append("</table>");
            builder.Append("<br/>" + "1010" + clsVariables.currentDealer.DealerId +  " " + vehicle.StockNumber);
            builder.Append("<br/>" + "Price is subject to change without notice");
          

            return builder.ToString();
        }


        public static string GenerateStandardCraiglistContent(WhitmanEntepriseVehicleInfo vehicle, string colorTemplate, string insertTemplate, bool hasPrice, string dealerTitle)
        {
            string fontColor = "lavenderblush";
            switch (insertTemplate)
            {
                case "gray":
                    fontColor = "lavenderblush";
                    break;
                case "white":
                    fontColor = "black";
                    break;
                default:
                    break;
            }


            var builder = new StringBuilder();

            var listRandom = RandomzieTable(vehicle, fontColor, insertTemplate);

            var random = new Random();

            int randomNumber = random.Next(1000, 9999);

            string queryString = "Stock=" + vehicle.StockNumber + "&Vin=" + vehicle.Vin + "&partner=" + clsVariables.currentDealer.DealerId + randomNumber;


            builder.Append("<table align=\"center\" width=\"750\" cellpadding=\"10\" cellspacing=\"0\" bgcolor=\"" + colorTemplate + "\">");

            builder.Append("<tr  bgcolor=\"" + colorTemplate + "\">");

            builder.Append("<td ></td>");

            builder.Append(" <td colspan=\"2\">");

            builder.Append("<table width=\"725\" cellspacing=\"0\" cellpadding=\"0\">");
            builder.Append("<tr>");
            builder.Append("<td>");

            builder.Append("<a href=\"" + clsVariables.currentDealer.WebSiteURL + "\" target=\"_blank\"><img border=\"0\" src=\"" + clsVariables.currentDealer.LogoURL + "\"/></a>");
            builder.Append("</td>");


            if (!clsVariables.currentDealer.DealerId.Equals("44670") && !clsVariables.currentDealer.DealerId.Equals("15986"))
            {

                builder.Append("<td align=\"right\" >");

                builder.Append(" <font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h2>" + clsVariables.currentDealer.DealershipName + "</h2></font><br />");

                builder.Append("<font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h2>" + clsVariables.currentDealer.PhoneNumber + "</h2></font><br />");

                if (!String.IsNullOrEmpty(dealerTitle.Trim()))
                    builder.Append("  <font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + dealerTitle + "</font><br />");

                if (clsVariables.currentDealer.DealerId.Equals("1200"))
                {

                    builder.Append("<a href=\"http://vinclapp.net/Request/ClientInfoRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/newportcoastauto/emailUsAt.png\" /></a><br />");
                }


                builder.Append("</td>");
            }

            builder.Append("</tr>");
            builder.Append("</table>");
            builder.Append("</td>");
            builder.Append("<td></td>");

            builder.Append("</tr>");

            builder.Append("<tr>");
            builder.Append("<td  bgcolor=\"" + colorTemplate + "\" height=\"0\" background=\"http://vinclapp.net/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("<td  bgcolor=\"" + colorTemplate + "\" background=\"http://vinclapp.net/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("<td  bgcolor=\"" + colorTemplate + "\" background=\"http://vinclapp.net/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("<td bgcolor=\"" + colorTemplate + "\" background=\"http://vinclapp.net/alpha/cListThemes/gloss/shadow.png\"></td>");
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
            builder.Append("  <td bgcolor=\"" + insertTemplate + "\"><font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + GenerateRandomTextBelowPic() + "</font></td>");
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

            if (!String.IsNullOrEmpty(vehicle.SalePrice))
            {

                double price = 0;

                bool validPrice = Double.TryParse(vehicle.SalePrice, out price);

                hasPrice = (!hasPrice) && validPrice && (price > 0);

                if (hasPrice)
                    builder.Append("<font size=\"+5\" color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + price.ToString("C") + "</font>");
                else
                {
                    builder.Append("<font size=\"+3\" color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + "CALL FOR PRICE" + "</font>");
                    if (clsVariables.currentDealer.DealerId.Equals("4310"))
                    {
                        string textMessageLink = clsVariables.TextMessageLink.Replace("PLACEVIN", vehicle.Vin);
                        builder.Append("<a href=\"" + textMessageLink + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/dealerimages/TextPrice.png\" /></a><br/>");
                    }

                }
            }
            else
            {
                builder.Append("<font size=\"+3\" color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + "CALL FOR PRICE" + "</font>");
                if (clsVariables.currentDealer.DealerId.Equals("4310"))
                {
                    string textMessageLink = clsVariables.TextMessageLink.Replace("PLACEVIN", vehicle.Vin);
                    builder.Append("<a href=\"" + textMessageLink + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/dealerimages/TextPrice.png\" /></a><br/>");
                }

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


            builder.Append(" <font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + clsVariables.currentDealer.DealershipName + "</font><br />");

            builder.Append("<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h3>" + clsVariables.currentDealer.PhoneNumber + "</h3></font><br />");
            builder.Append("  <font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + clsVariables.currentDealer.StreetAddress + "</font><br />");
            builder.Append(" <font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + clsVariables.currentDealer.City + ", " + clsVariables.currentDealer.State + " " + clsVariables.currentDealer.ZipCode + "</font><br />");
            builder.Append("</td>");


            //OPTIONS BEGIN

            builder.Append(listRandom.ElementAt(0));
            //OPTIONS END

            builder.Append("</tr>");

            builder.Append("<tr>");

            builder.Append("<td bgcolor=\"" + insertTemplate + "\">");


            builder.Append("<a href=\"" + clsVariables.currentDealer.WebSiteURL + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/newportcoastauto/visitOurSite.png\" /></a><br/>");
            builder.Append("<a href=\"http://vinclapp.net/Request/ClientInfoRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/newportcoastauto/moreInfo.png\" /></a><br/>");
            builder.Append("<a href=\"http://vinclapp.net/Request/ClientInfoRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/newportcoastauto/emailUs.png\" /></a><br/>");
            builder.Append("<a href=\"http://vinclapp.net/Request/ClientInfoRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/newportcoastauto/getQuote.png\" /></a><br/>");
            builder.Append("<a href=\"" + clsVariables.currentDealer.CreditURL + "\" target=\"_blank\"><img  border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/newportcoastauto/applyForCredit2.png\" /></a><br/>");
            builder.Append("</td>");


            //DESCRIPTION BEGINS

            builder.Append(listRandom.ElementAt(1));
            //DESCRIPTION END

            builder.Append("</tr>");


            builder.Append(" <tr>");
            builder.Append("<td bgcolor=\"" + insertTemplate + "\"><a href=\"http://www.carfax.com/VehicleHistory/p/Report.cfx?partner=DVW_1&vin=" + vehicle.Vin + "\" target=\"_blank\"><img src=\"http://vinclapp.net/alpha/cListThemes/gloss/carfax_logo.jpg\" width=\"150\" /></a></td>");

            //IMAGES BEGIN
            builder.Append(listRandom.ElementAt(2));
            //IMAGE ENDS

            builder.Append("</tr>");


            builder.Append(" <tr>");
            builder.Append("<td valign=\"top\" bgcolor=\"" + insertTemplate + "\"></td>");
            builder.Append("<td valign=\"top\" bgcolor=\"" + insertTemplate + "\"><font size=\"1\" color=\"" + fontColor + "\" face=\"MS Serif, New York, serif\"> " + GenerateRandomTextFromBook() + "</font></td>");
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
            builder.Append(" <td bgcolor=\"" + colorTemplate + "\"></td>");
            builder.Append(" <td bgcolor=\"" + colorTemplate + "\" align=\"right\"><a href=\"http://vehicleinventorynetwork.com/\"><img border=\"0\" src=\"http://vinclapp.net/vinclapplogo/vin-logo-white2.png\" /></a></td>");
            builder.Append("<td></td>");
            builder.Append("  </tr>");
            builder.Append("</table>");
            builder.Append("<br/>" + "1010" + clsVariables.currentDealer.DealerId + " " + vehicle.StockNumber);
            builder.Append("<br/>" + "Price is subject to change without notice");
            

            return builder.ToString();
        }


        public static string GenerateHtmlImageCode(WhitmanEntepriseVehicleInfo vehicle,string colorTemplate, string insertTemplate, bool hasPrice, string dealerTitle)
        {
            string fontColor = "lavenderblush";
            switch (insertTemplate)
            {
                case "gray":
                    fontColor = "lavenderblush";
                    break;
                case "white":
                    fontColor = "black";
                    break;
                default:
                    break;
            }

            var random = new Random();

            int randomNumber = random.Next(1000, 9999);

            var randomBackColorList = new List<string>();

            randomBackColorList.AddRange(new String[] { "black", "blue", "red", "maroon", "green", "navy" });

            int colorrandomIndex = randomNumber % randomBackColorList.Count();

            colorTemplate = randomBackColorList.ElementAt(colorrandomIndex);


            var builder = new StringBuilder();

            var listRandom = RandomzieTable(vehicle, fontColor, insertTemplate);

            string queryString = "Stock=" + vehicle.StockNumber+ "&Vin=" + vehicle.Vin + "&partner=" + clsVariables.currentDealer.DealerId + randomNumber;


            builder.Append("<table align=\"center\" width=\"750\" cellpadding=\"10\" cellspacing=\"0\" bgcolor=\"" + colorTemplate + "\">");

            builder.Append("<tr  bgcolor=\"" + colorTemplate + "\">");

            builder.Append("<td ></td>");

            builder.Append(" <td colspan=\"2\">");

            builder.Append("<table width=\"725\" cellspacing=\"0\" cellpadding=\"0\">");
            builder.Append("<tr>");
            builder.Append("<td>");

            builder.Append("<a href=\"" + clsVariables.currentDealer.WebSiteURL + "\" target=\"_blank\"><img border=\"0\" src=\"" + clsVariables.currentDealer.LogoURL + "\"/></a>");
            builder.Append("</td>");

            if (!clsVariables.currentDealer.DealerId.Equals("44670") && !clsVariables.currentDealer.DealerId.Equals("15986"))
            {

                builder.Append("<td align=\"right\" >");

                builder.Append(" <font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h2>" + clsVariables.currentDealer.DealershipName + "</h2></font><br />");

                builder.Append("<font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h2>" + clsVariables.currentDealer.PhoneNumber + "</h2></font><br />");

                if (!String.IsNullOrEmpty(dealerTitle.Trim()))
                    builder.Append("  <font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + dealerTitle + "</font><br />");
                if (clsVariables.currentDealer.DealerId.Equals("1200"))
                {
                    builder.Append("<a href=\"http://vinclapp.net/Request/ClientInfoRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/newportcoastauto/emailUsAt.png\" /></a><br />");
                }


                builder.Append("</td>");
            }

            builder.Append("</tr>");
            builder.Append("</table>");
            builder.Append("</td>");
            builder.Append("<td></td>");

            builder.Append("</tr>");

            builder.Append("<tr>");
            builder.Append("<td  bgcolor=\"" + colorTemplate + "\" height=\"0\" background=\"http://vinclapp.net/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("<td  bgcolor=\"" + colorTemplate + "\" background=\"http://vinclapp.net/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("<td  bgcolor=\"" + colorTemplate + "\" background=\"http://vinclapp.net/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("<td bgcolor=\"" + colorTemplate + "\" background=\"http://vinclapp.net/alpha/cListThemes/gloss/shadow.png\"></td>");
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
            builder.Append("  <td bgcolor=\"" + insertTemplate + "\"><font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + GenerateRandomTextBelowPic() + "</font></td>");
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

            //    hasPrice = (!hasPrice) && validPrice && (price > 0);

            //    if (hasPrice)
            //        builder.Append("<font size=\"+5\" color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + price.ToString("C") + "</font>");
            //    else
            //        builder.Append("<font size=\"+3\" color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + "CALL FOR PRICE" + "</font>");
            //}
            //else
            //    builder.Append("<font size=\"+3\" color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + "CALL FOR PRICE" + "</font>");

            if (randomNumber % 3 == 0)
            {

                builder.Append(
                    "<img border=\"0\" src=\"http://vinclapp.net/DealerLogo/red.gif\"  />");
            }
            else if (randomNumber % 3 == 1)
            {

                builder.Append(
                    "<img border=\"0\" src=\"http://vinclapp.net/DealerLogo/green.gif\"  />");
            }
            else if (randomNumber % 3 == 2)
            {

                builder.Append(
                    "<img border=\"0\" src=\"http://vinclapp.net/DealerLogo/blue.gif\"  />");
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


            builder.Append(" <font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + clsVariables.currentDealer.DealershipName + "</font><br />");

            builder.Append("<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h3>" + clsVariables.currentDealer.PhoneNumber + "</h3></font><br />");
            builder.Append("  <font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + clsVariables.currentDealer.StreetAddress + "</font><br />");
            builder.Append(" <font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + clsVariables.currentDealer.City + ", " + clsVariables.currentDealer.State + " " + clsVariables.currentDealer.ZipCode + "</font><br />");
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
            builder.Append("<td bgcolor=\"" + insertTemplate + "\"><a href=\"http://www.carfax.com/VehicleHistory/p/Report.cfx?partner=DVW_1&vin=" + vehicle.Vin + "\" target=\"_blank\"><img src=\"http://vinclapp.net/alpha/cListThemes/gloss/carfax_logo.jpg\" width=\"150\" /></a></td>");

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
            builder.Append(" <td bgcolor=\"" + colorTemplate + "\"></td>");
            builder.Append(" </tr>");
            builder.Append(" <tr bgcolor=\"" + colorTemplate + "\">");
            builder.Append("<td bgcolor=\"" + colorTemplate + "\"></td>");
            builder.Append(" <td bgcolor=\"" + colorTemplate + "\" align=\"left\"><font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">Price is subject to change without notice</font></td>");
            builder.Append(" <td bgcolor=\"" + colorTemplate + "\" align=\"right\"><a href=\"http://vehicleinventorynetwork.com/\"><img border=\"0\" src=\"http://vinclapp.net/vinclapplogo/vin-logo-white2.png\" /></a></td>");
            builder.Append("<td></td>");
            builder.Append("  </tr>");
            builder.Append("</table>");
            
            return builder.ToString();
        }

        public static string GenerateHtmlImageCodeForSecondBottomImage(WhitmanEntepriseVehicleInfo vehicle, string colorTemplate, string insertTemplate, bool hasPrice, string dealerTitle)
        {
            string fontcolor = "lavenderblush";
            switch (insertTemplate)
            {
                case "gray":
                    fontcolor = "lavenderblush";
                    break;
                case "white":
                    fontcolor = "black";
                    break;
                default:
                    break;
            }

            var builder = new StringBuilder();

            var random = new Random();
            
            var firstvehilce =
                clsVariables.currentDealer.Inventory.ElementAt(random.Next(0,
                                                                           clsVariables.currentDealer.Inventory.Count -
                                                                           1));

            var secondvehilce =
    clsVariables.currentDealer.Inventory.ElementAt(random.Next(0,
                                                               clsVariables.currentDealer.Inventory.Count -
                                                               1));

            var thirdtvehilce =
    clsVariables.currentDealer.Inventory.ElementAt(random.Next(0,
                                                               clsVariables.currentDealer.Inventory.Count -
                                                               1));

            var fourthvehilce =
    clsVariables.currentDealer.Inventory.ElementAt(random.Next(0,
                                                               clsVariables.currentDealer.Inventory.Count -
                                                               1));


            builder.Append(
                "<table bgcolor=\"" + colorTemplate + "\" align=\"center\" width=\"750\" cellpadding=\"10\" cellspacing=\"0\" >");

            builder.Append("<tr  bgcolor=\"" + colorTemplate + "\">");


            builder.Append(" <td></td>");


            builder.Append(" <td colspan=\"2\">");
            builder.Append("  <table width=\"725\" cellspacing=\"0\" cellpadding=\"0\">");
            builder.Append("<tr>");
            builder.Append(" <td><b><font size=\"5\" color=\"white\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">View Our Inventory!</font></b></td>");
            builder.Append("  </tr>");
            builder.Append("  <td bgcolor=\"#666\" colspan=\"2\">");
            builder.Append("<table cellspacing=\"0\" cellpadding=\"5\" width=\"726\">");
            builder.Append(" <tr>");

            builder.Append("  <td><img src=\"" + firstvehilce.FirstImageUrl + "\" width=\"165\" height=\"140\" /></td>");
            builder.Append("  <td><img src=\"" + secondvehilce.FirstImageUrl + "\" width=\"165\" height=\"140\" /></td>");
            builder.Append("  <td><img src=\"" + thirdtvehilce.FirstImageUrl + "\" width=\"165\" height=\"140\" /></td>");
            builder.Append("  <td><img src=\"" + fourthvehilce.FirstImageUrl + "\" width=\"165\" height=\"140\" /></td>");
            builder.Append("</tr>");

            builder.Append(" <tr>");

            builder.Append("  <td><font size=\"2\" color=\"white\">" + firstvehilce.ModelYear + SPACE + firstvehilce.Make + SPACE + firstvehilce.Model + "</font></td>");
            builder.Append("  <td><font size=\"2\" color=\"white\"> " + secondvehilce.ModelYear + SPACE + secondvehilce.Make + SPACE + secondvehilce.Model + "</font></td>");
            builder.Append("  <td><font size=\"2\" color=\"white\">" + thirdtvehilce.ModelYear + SPACE + thirdtvehilce.Make + SPACE + thirdtvehilce.Model + "</font></td>");
            builder.Append("  <td><font size=\"2\" color=\"white\">" + fourthvehilce.ModelYear + SPACE + fourthvehilce.Make + SPACE + fourthvehilce.Model + "</font></td>");
            builder.Append("</tr>");
            builder.Append("                        </table>");
            builder.Append(" </td>");
            builder.Append("    </tr>   ");
            builder.Append(" </table>");
            builder.Append(" </td>");
            builder.Append(" <td bgcolor=\""+colorTemplate + "\"></td>");
            builder.Append("</tr>");
            builder.Append(" <tr bgcolor=\"" + colorTemplate + "\">");
            builder.Append("   <td></td>");
            builder.Append(" <td></td>");
          
            builder.Append(" </tr>");
            builder.Append("</table>");
       

            return builder.ToString();
        }
        
        
        
        
        public static string GenerateHtmlImageCodeForAudi(WhitmanEntepriseVehicleInfo vehicle, bool hasPrice, string dealerTitle)
        {
            string fontColor = "black";

            string colorTemplate = "black";

            string insertTemplate = "white";

            var builder = new StringBuilder();

            var listRandom = GetImagesForAudi(vehicle, fontColor);

            var random = new Random();

            int randomNumber = random.Next(1000, 9999);

            string queryString = "Stock=" + vehicle.StockNumber + "&Vin=" + vehicle.Vin + "&partner=" + clsVariables.currentDealer.DealerId + randomNumber;


            builder.Append("<table align=\"center\" width=\"750\" cellpadding=\"10\" cellspacing=\"0\" bgcolor=\"" + colorTemplate + "\">");

            builder.Append("<tr  bgcolor=\"" + colorTemplate + "\">");

            builder.Append("<td ></td>");

            builder.Append(" <td colspan=\"2\">");

            builder.Append("<table width=\"725\" cellspacing=\"0\" cellpadding=\"0\">");
            builder.Append("<tr>");
            builder.Append("<td>");

            builder.Append("<a href=\"" + clsVariables.currentDealer.WebSiteURL + "\" target=\"_blank\"><img border=\"0\" src=\"" + clsVariables.currentDealer.LogoURL + "\"/></a>");
            builder.Append("</td>");


            builder.Append("<td align=\"right\" >");

            builder.Append("<font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h2>" + clsVariables.currentDealer.DealershipName + "</h2></font><br />");

            builder.Append("<font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h2>" + clsVariables.currentDealer.PhoneNumber + "</h2></font><br />");

            if (!String.IsNullOrEmpty(dealerTitle.Trim()))
                builder.Append("<font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + dealerTitle + "</font><br />");

            if (clsVariables.currentDealer.DealerId.Equals("1200"))
            {

                builder.Append("<a href=\"http://vinclapp.net/Request/ClientInfoRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/newportcoastauto/emailUsAt.png\" /></a><br />");
            }


            builder.Append("</td>");


            builder.Append("</tr>");
            builder.Append("</table>");
            builder.Append("</td>");
            builder.Append("<td></td>");

            builder.Append("</tr>");

            builder.Append("<tr>");
            builder.Append("<td  bgcolor=\"" + colorTemplate + "\" height=\"0\" background=\"http://vinclapp.net/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("<td  bgcolor=\"" + colorTemplate + "\" background=\"http://vinclapp.net/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("<td  bgcolor=\"" + colorTemplate + "\" background=\"http://vinclapp.net/alpha/cListThemes/gloss/shadow.png\"></td>");
            builder.Append("<td bgcolor=\"" + colorTemplate + "\" background=\"http://vinclapp.net/alpha/cListThemes/gloss/shadow.png\"></td>");
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
            builder.Append("  <td bgcolor=\"" + insertTemplate + "\"><font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + GenerateRandomTextBelowPic() + "</font></td>");
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

            //    hasPrice = (!hasPrice) && validPrice && (price > 0);

            //    if (hasPrice)
            //        builder.Append("<font size=\"+5\" color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + price.ToString("C") + "</font>");
            //    else
            //    {
            //        builder.Append("<font size=\"+3\" color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + "CALL FOR PRICE" + "</font>");
            //        if (clsVariables.currentDealer.DealerId.Equals("4310"))
            //        {
            //            string textMessageLink = clsVariables.TextMessageLink.Replace("PLACEVIN", vehicle.Vin);
            //            builder.Append("<a href=\"" + textMessageLink + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/dealerimages/TextPrice.png\" /></a><br/>");
            //        }
            //    }
            //}
            //else
            //{
            //    builder.Append("<font size=\"+3\" color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + "CALL FOR PRICE" + "</font>");
            //    if (clsVariables.currentDealer.DealerId.Equals("4310"))
            //    {
            //        string textMessageLink = clsVariables.TextMessageLink.Replace("PLACEVIN", vehicle.Vin);
            //        builder.Append("<a href=\"" + textMessageLink + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/dealerimages/TextPrice.png\" /></a><br/>");
            //    }

            //}

            //if (!String.IsNullOrEmpty(vehicle.SalePrice))
            //{

            //    double price = 0;

            //    bool validPrice = Double.TryParse(vehicle.SalePrice, out price);

            //    hasPrice = (!hasPrice) && validPrice && (price > 0);

            //    if (hasPrice)
            //        builder.Append("<font size=\"+5\" color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + price.ToString("C") + "</font>");
            //    else
            //        builder.Append("<font size=\"+3\" color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + "CALL FOR PRICE" + "</font>");
            //}
            //else
            //    builder.Append("<font size=\"+3\" color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + "CALL FOR PRICE" + "</font>");

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

            builder.Append("<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + clsVariables.currentDealer.StreetAddress + "</font><br />");
            builder.Append("<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + clsVariables.currentDealer.City + ", " + clsVariables.currentDealer.State + " " + clsVariables.currentDealer.ZipCode + "</font><br />");
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
            builder.Append("<td bgcolor=\"" + insertTemplate + "\"><a href=\"http://www.carfax.com/VehicleHistory/p/Report.cfx?partner=DVW_1&vin=" + vehicle.Vin + "\" target=\"_blank\"><img src=\"http://vinclapp.net/alpha/cListThemes/gloss/carfax_logo.jpg\" width=\"150\" /></a></td>");

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
            
            builder.Append(" <td bgcolor=\"" + colorTemplate + "\" align=\"left\"><font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">Price is subject to change without notice</font></td>");
            builder.Append(" <td bgcolor=\"" + colorTemplate + "\" align=\"right\"><a href=\"http://vehicleinventorynetwork.com/\"><img border=\"0\" src=\"http://webpics.us/vinclapplogo/vin-logo-white2.png\" /></a></td>");
            builder.Append("<td></td>");
            builder.Append("  </tr>");
            builder.Append("</table>");
       


            return builder.ToString();
        }

        

        public static string GenerateCraiglistContentByImage(WhitmanEntepriseVehicleInfo vehicle)
        {
            var builder = new StringBuilder();

            string modifiedTrim = vehicle.Trim.Replace(" ", "-");
            modifiedTrim = modifiedTrim.Replace("/", "");

            string defaultUrl = "http://vinlineup.com/" + clsVariables.currentDealer.DealershipName.Replace(" ", "-") + "/" + clsVariables.currentDealer.DealerId + "/" + vehicle.ModelYear + "-" + vehicle.Make.Replace(" ", "-") + "-" + vehicle.Model.Replace(" ", "-") + "-" + modifiedTrim + "/" + vehicle.Vin;

            string defaultDealerUrl = "http://vinlineup.com/" + clsVariables.currentDealer.DealershipName.Replace(" ", "-") + "/" + clsVariables.currentDealer.DealerId;
            
           
            //builder.Append("<table width=\"750\" cellpadding=\"10\" cellspacing=\"0\">");
            //builder.Append(" <tr>");
            //builder.Append(" <td>");
           
            //builder.Append(string.Format("<a href=\"{0}\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/newportcoastauto/visitOurSite.png\" /></a><br/>", clsVariables.currentDealer.WebSiteURL.ToString()));
           
            //builder.Append(" </td>");
            //builder.Append(" <td>");

            //if (clsVariables.currentDealer.DealerId.Equals("4310"))
            //{
            //    string textMessageLink = clsVariables.TextMessageLink.Replace("PLACEVIN", vehicle.Vin);
            //    builder.Append("<a href=\"" + textMessageLink + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/dealerimages/TextPrice.png\" /></a><br/>");
            //}
            //else
            //    builder.Append("<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/ClientInfoRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/newportcoastauto/moreInfo.png\" /></a><br/>");
            //builder.Append(" </td>");
            //builder.Append(" <td>");
            //builder.Append("<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/ClientInfoRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/newportcoastauto/emailUs.png\" /></a><br/>");
            //builder.Append(" </td>");
            //builder.Append(" <td>");

            //if (clsVariables.currentDealer.DealerId.Equals("4310"))
            //{
            //    string textMessageLink = clsVariables.TextMessageLink.Replace("PLACEVIN", vehicle.Vin);
            //    builder.Append("<a href=\"" + textMessageLink + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/dealerimages/Textoffer.png\" /></a><br/>");
            //}
            //else
            //{

            //    builder.Append("<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/ClientInfoRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/newportcoastauto/getQuote.png\" /></a><br/>");

        
            //}
                
            //builder.Append(" </td>");
            //builder.Append(" <td>");

           
            //    builder.Append("<a href=\"" + clsVariables.currentDealer.CreditURL + "\" target=\"_blank\"><img  border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/newportcoastauto/applyForCredit2.png\" /></a><br/>");
         
            //builder.Append(" </td>");
            //builder.Append(" </tr>");
            //builder.Append("</table>");

            builder.Append("<font size=\"-1\" color=\"beige\">" + GenerateRandomTextFromBookOnTop() + "</font><br/>");



            if (clsVariables.currentDealer.DealerId.Equals("2584") || clsVariables.currentDealer.DealerId.Equals("1541") ||
                clsVariables.currentDealer.DealerId.Equals("1200") ||
                clsVariables.currentDealer.DealerId.Equals("3738") ||
                clsVariables.currentDealer.DealerId.Equals("44670") ||
                clsVariables.currentDealer.DealerId.Equals("15896") || clsVariables.currentDealer.DealerId.Equals("11828") || clsVariables.currentDealer.DealerId.Equals("7180") || clsVariables.currentDealer.DealerId.Equals("15986") || clsVariables.currentDealer.DealerId.Equals("2650") || clsVariables.currentDealer.DealerId.Equals("2299"))

                builder.Append("<a href=\"" + clsVariables.currentDealer.WebSiteURL + "\" target=\"_blank\"><img src=\"" + vehicle.HTMLImageURL.Substring(0, vehicle.HTMLImageURL.IndexOf(",", System.StringComparison.Ordinal)) + "\" /></a><br/>");

            else

                builder.Append("<a href=\"" + defaultUrl+  "\" target=\"_blank\"><img src=\"" + vehicle.HTMLImageURL.Substring(0, vehicle.HTMLImageURL.IndexOf(",", System.StringComparison.Ordinal)) + "\" /></a><br/>");

            builder.Append("<a href=\"" + defaultDealerUrl + "\"  target=\"_blank\"><img src=\"" + vehicle.HTMLImageURL.Substring(vehicle.HTMLImageURL.IndexOf(",", System.StringComparison.Ordinal) + 1) + "\" /></a><br/>");
       
            builder.Append("<font size=\"-1\" color=\"beige\">" + GenerateRandomTextFromBook() + "</font><br/>");
                       

            var trackingid = Convert.ToUInt64(Convert.ToInt64(clsVariables.currentDealer.DealerId) * DateTime.Now.Year * DateTime.Now.Month * DateTime.Now.Day);


            builder.Append("<br/>" + trackingid);
           

        
            return builder.ToString();
        }



     

        public static string GenerateInfoOrder(WhitmanEntepriseVehicleInfo vehicle,string fontColor)
        {
            var builder = new StringBuilder();

            Random random = new Random();

            builder.Append("<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +vehicle.ModelYear + "</font> ");
            builder.Append("<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + vehicle.Make + "</font> ");
            builder.Append("<font color=\"" + fontColor + "\"face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +vehicle.Model + "</font> ");
            builder.Append("<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + vehicle.Trim + " </font><br />");


            var infoRandomList = new List<String>();

            infoRandomList.AddRange(new String[]
                    {"<font color=\""+fontColor+"\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">"+ "VIN: "+ vehicle.Vin + "</font><br />",
                     "<font color=\""+fontColor+"\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">"+ "Stock #"+ vehicle.StockNumber + "</font><br />",
                     "<font color=\""+fontColor+"\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">"+"Interior Color: " + vehicle.InteriorColor + "</font><br />",
                     "<font color=\""+fontColor+"\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">"+"Exterior Color: " +  vehicle.ExteriorColor + "</font><br />",
                    "<font color=\""+fontColor+"\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">"+"Tranmission: "+ vehicle.Tranmission + "</font><br />",
                    "<font color=\""+fontColor+"\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">"+"Mileage " +  vehicle.Mileage + "</font><br />"});
                      
            var randomInfoOrder=  infoRandomList.OrderBy(t => Guid.NewGuid()).ToArray<string>();

            foreach (string tmp in randomInfoOrder)
            {
                builder.Append(tmp);
            }

            return builder.ToString();
        }





        public static string GenerateCraiglistTitle(WhitmanEntepriseVehicleInfo vehicle, bool checkPrice)
        {
            var builder = new StringBuilder();

            //string frontChunkNode = clsVariables.chunkList.First(t => t.ChunkName.Equals("FrontChunk")).ChunkStringValue;

            string middleChunkNode = clsVariables.chunkList.First(t => t.ChunkName.Equals("MiddleChunk")).ChunkStringValue;
            
            string endChunkNode = clsVariables.chunkList.First(t => t.ChunkName.Equals("EndChunk")).ChunkStringValue;

            var random = new Random();

            //var yearRandomList = new List<String>();

            //yearRandomList.AddRange(new String[]
            //        {vehicle.ModelYear.ToString(),
            //        vehicle.ModelYear.ToString().Substring(2)});
            var frontRandomList = new List<String>();
            var afterMakeRandomList = new List<String>();

            afterMakeRandomList.AddRange(new String[]
                    {String.IsNullOrEmpty(vehicle.Trim)?"":vehicle.Trim,
                    String.IsNullOrEmpty(vehicle.ExteriorColor)?"":vehicle.ExteriorColor,
                    String.IsNullOrEmpty(vehicle.BodyType)?"":vehicle.BodyType,
                     String.IsNullOrEmpty(vehicle.Engine)?"":vehicle.Engine,
                    });


            //string[] frontChunkList = frontChunkNode.Split(new String[] { "," }, StringSplitOptions.None);

            string[] middleChunkList = middleChunkNode.Split(new String[] { "," }, StringSplitOptions.None);


            string[] endChunkList = endChunkNode.Split(new String[] { "," }, StringSplitOptions.None);


            //builder.Append(frontChunkList.ElementAt(random.Next(0, frontChunkList.Count())) + SPACE);

          
            //builder.Append(yearRandomList.ElementAt(random.Next(0, yearRandomList.Count())) + SPACE);


            int year = Convert.ToInt32(vehicle.ModelYear);

            if (year < DateTime.Now.Year - 1)
            {

                frontRandomList.AddRange(new String[]
                                             {
                                                 "Used",
                                                 ""
                                             });


            }
            if (frontRandomList.Any())
            {
                if (String.IsNullOrEmpty(frontRandomList.ElementAt(random.Next(0, frontRandomList.Count()))))
                {
                    builder.Append(vehicle.ModelYear + SPACE + vehicle.Make + SPACE + vehicle.Model + SPACE);
                }
                else
                {
                    builder.Append(frontRandomList.ElementAt(random.Next(0, frontRandomList.Count())) + SPACE +
                                   vehicle.ModelYear + SPACE + vehicle.Make + SPACE + vehicle.Model + SPACE);
                }
            }
            else
            {
                builder.Append(vehicle.ModelYear + SPACE + vehicle.Make + SPACE + vehicle.Model + SPACE);
            }

            builder.Append(afterMakeRandomList.ElementAt(random.Next(0, afterMakeRandomList.Count())) + SPACE);

            builder.Append(middleChunkList.ElementAt(random.Next(0, middleChunkList.Count())) + SPACE);

            if (!clsVariables.currentDealer.DealerId.Equals("10215"))
            {
                builder.Append(endChunkList.ElementAt(random.Next(0, endChunkList.Count())));
            }
            else
            {
                builder.Append(vehicle.SalePrice);
            }

            //if(!checkPrice)

            //    builder.Append(vehicle.SalePrice);

            return builder.ToString();

        }
       
        public static string GenerateRandomTextFromBook()
        {

            var builder = new StringBuilder();
                       

            string charsToUse = clsVariables.chunkList.First(t => t.ChunkName.Equals("CrazyLetters")).ChunkStringValue;

            try
            {
                
                var random = new Random();
              
                for (int i = 0; i < 10; i++)
                    builder.Append(charsToUse[random.Next(charsToUse.Length)].ToString(CultureInfo.InvariantCulture)) ;

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


        public static string GenerateRandomTextBelowPic()
        {
            var builder = new StringBuilder();

             string belowPicChunk = clsVariables.chunkList.First(t => t.ChunkName.Equals("BelowPicChunk")).ChunkStringValue;

           

            var random = new Random();

            string[] belowPicChunkList = belowPicChunk.Split(new String[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            builder.Append(belowPicChunkList.ElementAt(random.Next(0, belowPicChunkList.Count())));

            return builder.ToString();

        }

        public static string GenerateOptions(WhitmanEntepriseVehicleInfo vehicle,string fontColor,string insertTemplate)
        {
            string tmp = vehicle.Options;

            string[] totalOption = tmp.Split(new String[] {"|","," }, StringSplitOptions.RemoveEmptyEntries);

            var random10Options = totalOption.OrderBy(t => Guid.NewGuid()).ToList().Take(10);

                     
            StringBuilder builder = new StringBuilder();

            builder.Append("<td valign=\"top\" bgcolor=\"" + insertTemplate + "\">");

            builder.Append("<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\" ><h3>Options</h3>");

            builder.Append("<table cellspacing=\"0\">");

            int count = 0;

            while (count < random10Options.Count())
            {
                if (count % 2 == 0)
                    builder.Append("<tr>");
                builder.Append("<td bgcolor=\"" + insertTemplate + "\">" + random10Options.ElementAt(count) + "</td>");
                if(random10Options.Count()>count+1)
                    builder.Append("<td bgcolor=\"" + insertTemplate + "\">" + random10Options.ElementAt(count + 1) + "</td>");

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


        public static List<string> GetImagesForAudi(WhitmanEntepriseVehicleInfo vehicle, string fontColor)
        {
          
            List<string> filterImage = new List<string>();
                        
            filterImage.Add("http://webpics.us/WaltersAudiLogo/A3lease.png");

            filterImage.Add("http://webpics.us/WaltersAudiLogo/A4lease.png");

            filterImage.Add("http://webpics.us/WaltersAudiLogo/A5lease.png");

            filterImage.Add("http://webpics.us/WaltersAudiLogo/A6lease.png");

            filterImage.Add("http://webpics.us/WaltersAudiLogo/A7lease.png");

            filterImage.Add("http://webpics.us/WaltersAudiLogo/A8lease.png");

            var list = new List<string>();

            foreach (string imgTmp in filterImage)
            {

                var builder = new StringBuilder();

                builder.Append("<td valign=\"top\" bgcolor=\"white\">");

                builder.Append("<table>");

                builder.Append("<tr>");

                builder.Append("<td>");

                builder.Append("<a href=\"" + clsVariables.currentDealer.WebSiteURL + "\" target=\"_blank\"><img border=\"0\" src=\"" + imgTmp.Trim() + "\" width=\"500\"   /></a>");

                builder.Append("</td>");

                builder.Append("</tr>");

                builder.Append("</table>");

                builder.Append("</font></td>");

                list.Add(builder.ToString());

            }

            return list;

        }
        

        public static List<string> GetRandomImages(WhitmanEntepriseVehicleInfo vehicle, string fontColor, string insertTemplate)
        {
            string tmp = vehicle.CarImageUrl;

            List<string> totalImage = tmp.Split(new String[] { "|", "," }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var random = new Random();

            var list = new List<string>();


            int randomNumber = random.Next(1000, 9999);

            string queryString = "Stock=" + vehicle.StockNumber + "&Vin=" + vehicle.Vin + "&partner=" + clsVariables.currentDealer.DealerId + randomNumber;
            bool hasHeader = true;

            if (totalImage.Count() >= 3)
            {
                var randomOptions = totalImage.OrderBy(t => Guid.NewGuid()).ToList().Take(3);
                
                foreach (string imgTmp in randomOptions)
                {

                    StringBuilder builder = new StringBuilder();

                    builder.Append("<td valign=\"top\" bgcolor=\"" + insertTemplate + "\">");

                    if (hasHeader)
                    {

                        builder.Append("<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h3>Images</h3>");

                        hasHeader = false;
                    }

                    builder.Append("<table>");

                    builder.Append("<tr>");

                    builder.Append("<td>");

                    if (clsVariables.currentDealer.DealerId.Equals("4310") || clsVariables.currentDealer.DealerId.Equals("5780") || clsVariables.currentDealer.DealerId.Equals("1660"))

                        builder.Append("<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" + imgTmp.Trim() + "\" width=\"500\"   /></a>");

                    else
                        builder.Append("<a href=\"" + clsVariables.currentDealer.WebSiteURL + "\" target=\"_blank\"><img border=\"0\" src=\"" + imgTmp.Trim() + "\" width=\"500\"   /></a>");

                        
                   

                    builder.Append("</td>");

                    builder.Append("</tr>");

                    builder.Append("</table>");

                    builder.Append("</font></td>");

                    list.Add(builder.ToString());


                }
            }
            else
            {
                if (totalImage.Count() == 1)
                {

                    for (int i = 0; i < 3; i++)
                    {
                        StringBuilder builder = new StringBuilder();

                        builder.Append("<td valign=\"top\" bgcolor=\"" + insertTemplate + "\">");

                        if (hasHeader)
                        {

                            builder.Append("<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h3>Images</h3>");

                            hasHeader = false;
                        }

                        builder.Append("<table>");

                        builder.Append("<tr>");

                        builder.Append("<td>");


                        if (clsVariables.currentDealer.DealerId.Equals("4310") || clsVariables.currentDealer.DealerId.Equals("5780") || clsVariables.currentDealer.DealerId.Equals("1660"))

                            builder.Append("<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" + totalImage.ElementAt(0).Trim() + "\" width=\"500\"   /></a>");

                        else
                            builder.Append("<a href=\"" + clsVariables.currentDealer.WebSiteURL + "\" target=\"_blank\"><img border=\"0\" src=\"" + totalImage.ElementAt(0).Trim() + "\" width=\"500\"   /></a>");


                     
                        builder.Append("</td>");

                        builder.Append("</tr>");

                        builder.Append("</table>");

                        builder.Append("</font></td>");

                        list.Add(builder.ToString());
                    }

                }
                else if (totalImage.Count() == 2)
                {
                    foreach (string imgTmp in totalImage)
                    {

                        StringBuilder builder = new StringBuilder();

                        builder.Append("<td valign=\"top\" bgcolor=\"" + insertTemplate + "\">");

                        if (hasHeader)
                        {

                            builder.Append("<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h3>Images</h3>");

                            hasHeader = false;
                        }

                        builder.Append("<table>");

                        builder.Append("<tr>");

                        builder.Append("<td>");


                        if (clsVariables.currentDealer.DealerId.Equals("4310") || clsVariables.currentDealer.DealerId.Equals("5780") || clsVariables.currentDealer.DealerId.Equals("1660"))


                            builder.Append("<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" + imgTmp.Trim() + "\" width=\"500\"   /></a>");

                        else
                            builder.Append("<a href=\"" + clsVariables.currentDealer.WebSiteURL + "\" target=\"_blank\"><img border=\"0\" src=\"" + imgTmp.Trim() + "\" width=\"500\"   /></a>");



                        builder.Append("</td>");

                        builder.Append("</tr>");

                        builder.Append("</table>");

                        builder.Append("</font></td>");

                        list.Add(builder.ToString());


                    }

                    list.Add(list.Last());


                }

                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        StringBuilder builder = new StringBuilder();

                        builder.Append("<td valign=\"top\" bgcolor=\"" + insertTemplate + "\">");

                        if (hasHeader)
                        {

                            builder.Append("<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h3>Images</h3>");

                            hasHeader = false;
                        }

                        builder.Append("<table>");

                        builder.Append("<tr>");

                        builder.Append("<td>");

                        if (String.IsNullOrEmpty(vehicle.DefaultImageURL))

                            builder.Append("<a href=\"" + clsVariables.currentDealer.WebSiteURL + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/gloss/no-image-available.jpg\" width=\"500\"   /></a>");
                        else
                            builder.Append("<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" + vehicle.DefaultImageURL + "\" width=\"500\"   /></a>");
                        builder.Append("</td>");

                        builder.Append("</tr>");

                        builder.Append("</table>");

                        builder.Append("</font></td>");

                        list.Add(builder.ToString());

                    }

                }
            }

            return list;

        }

        public static string GenerateImages(WhitmanEntepriseVehicleInfo vehicle,string fontColor,string insertTemplate)
        {
            string tmp = vehicle.CarImageUrl;

            List<string> totalImage = tmp.Split(new String[] { "|",","," " }, StringSplitOptions.RemoveEmptyEntries).ToList();

            Random random = new Random();

            StringBuilder builder = new StringBuilder();

            builder.Append("<td valign=\"top\" bgcolor=\"" + insertTemplate + "\">");

            builder.Append("<font color=\""+fontColor+"\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h3>Images</h3>");

            builder.Append("<table>");

            int count = 0;

            int randomNumber = random.Next(1000, 9999);

            string queryString = "Stock=" + vehicle.StockNumber + "&Vin=" + vehicle.Vin + "&partner=" + clsVariables.currentDealer.DealerId + randomNumber;
            
            if (totalImage.Count() >= 4)
            {
                var randomOptions = totalImage.OrderBy(t => Guid.NewGuid()).ToList().Take(4);
                
                while (count < randomOptions.Count())
                {
                    if (count % 2 == 0)
                        builder.Append("<tr>");
                    builder.Append("<td>");


                    if (clsVariables.currentDealer.DealerId.Equals("4310") || clsVariables.currentDealer.DealerId.Equals("5780") || clsVariables.currentDealer.DealerId.Equals("1660"))


                        builder.Append("<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" + randomOptions.ElementAt(count).Trim() + "\" width=\"250\"   /></a>");

                    else
                        builder.Append("<a href=\"" + clsVariables.currentDealer.WebSiteURL + "\" target=\"_blank\"><img border=\"0\" src=\"" + randomOptions.ElementAt(count).Trim() + "\" width=\"250\"   /></a>");



                
                    builder.Append("</td>");
                    builder.Append("<td>");

                    if (clsVariables.currentDealer.DealerId.Equals("4310") || clsVariables.currentDealer.DealerId.Equals("5780") || clsVariables.currentDealer.DealerId.Equals("1660"))


                        builder.Append("<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" + randomOptions.ElementAt(count + 1).Trim() + "\" width=\"250\"   /></a>");
               
                    else
                        builder.Append("<a href=\"" + clsVariables.currentDealer.WebSiteURL + "\" target=\"_blank\"><img border=\"0\" src=\"" + randomOptions.ElementAt(count + 1).Trim() + "\" width=\"250\"   /></a>");




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
                    if (clsVariables.currentDealer.DealerId.Equals("4310") || clsVariables.currentDealer.DealerId.Equals("5780") || clsVariables.currentDealer.DealerId.Equals("1660"))


                        builder.Append("<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" + queryString + "\" target=\"_blank\"><img  border=\"0\" src=\"" + totalImage.ElementAt(0).Trim() + "\" width=\"250\"   /></a>");

                    else
                        builder.Append("<a href=\"" + clsVariables.currentDealer.WebSiteURL + "\" target=\"_blank\"><img  border=\"0\" src=\"" + totalImage.ElementAt(0).Trim() + "\" width=\"250\"   /></a>");



                  
                    builder.Append("</td>");
                    builder.Append("</tr>");
                }
                else if (totalImage.Count() == 2)
                {
                    builder.Append("<tr>");
                    builder.Append("<td>");
                    if (clsVariables.currentDealer.DealerId.Equals("4310") || clsVariables.currentDealer.DealerId.Equals("5780") || clsVariables.currentDealer.DealerId.Equals("1660"))


                        builder.Append("<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" + totalImage.ElementAt(0).Trim() + "\" width=\"250\"   /></a>");
       

                    else
                        builder.Append("<a href=\"" + clsVariables.currentDealer.WebSiteURL + "\" target=\"_blank\"><img border=\"0\" src=\"" + totalImage.ElementAt(0).Trim() + "\" width=\"250\"   /></a>");




                           builder.Append("</td>");
                    builder.Append("<td>");


                    if (clsVariables.currentDealer.DealerId.Equals("4310") || clsVariables.currentDealer.DealerId.Equals("5780") || clsVariables.currentDealer.DealerId.Equals("1660"))


                        builder.Append("<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" + totalImage.ElementAt(1).Trim() + "\" width=\"250\"   /></a>");
                  


                    else
                        builder.Append("<a href=\"" + clsVariables.currentDealer.WebSiteURL + "\" target=\"_blank\"><img border=\"0\" src=\"" + totalImage.ElementAt(1).Trim() + "\" width=\"250\"   /></a>");


                    builder.Append("</td>");
                    builder.Append("</tr>");
                }
                else if (totalImage.Count() == 3)
                {
                    builder.Append("<tr>");
                    builder.Append("<td>");

                    if (clsVariables.currentDealer.DealerId.Equals("4310") || clsVariables.currentDealer.DealerId.Equals("5780") || clsVariables.currentDealer.DealerId.Equals("1660"))


                        builder.Append("<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" + totalImage.ElementAt(0).Trim() + "\" width=\"250\"   /></a>");
             


                    else
                        builder.Append("<a href=\"" + clsVariables.currentDealer.WebSiteURL + "\" target=\"_blank\"><img border=\"0\" src=\"" + totalImage.ElementAt(0).Trim() + "\" width=\"250\"   /></a>");



                       builder.Append("</td>");
                    builder.Append("<td>");

                    if (clsVariables.currentDealer.DealerId.Equals("4310") || clsVariables.currentDealer.DealerId.Equals("5780") || clsVariables.currentDealer.DealerId.Equals("1660"))


                        builder.Append("<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" + totalImage.ElementAt(1).Trim() + "\" width=\"250\"   /></a>");
                


                    else
                        builder.Append("<a href=\"" + clsVariables.currentDealer.WebSiteURL + "\" target=\"_blank\"><img border=\"0\" src=\"" + totalImage.ElementAt(1).Trim() + "\" width=\"250\"   /></a>");



                     builder.Append("</td>");
                    builder.Append("</tr>");
                    builder.Append("<tr>");
                    builder.Append("<td>");
                    if (clsVariables.currentDealer.DealerId.Equals("4310") || clsVariables.currentDealer.DealerId.Equals("5780") || clsVariables.currentDealer.DealerId.Equals("1660"))


                        builder.Append("<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" + totalImage.ElementAt(2).Trim() + "\" width=\"250\"   /></a>");



                    else
                        builder.Append("<a href=\"" + clsVariables.currentDealer.WebSiteURL + "\" target=\"_blank\"><img border=\"0\" src=\"" + totalImage.ElementAt(2).Trim() + "\" width=\"250\"   /></a>");

                    builder.Append("</td>");
                    builder.Append("</tr>");
                }
                else
                {
                    builder.Append("<tr>");
                    builder.Append("<td>");
                    if(String.IsNullOrEmpty(vehicle.DefaultImageURL))
                        builder.Append("<a href=\"" + clsVariables.currentDealer.WebSiteURL + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/gloss/no-image-available.jpg\" width=\"250\"   /></a>");
                    else
                        builder.Append("<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" + vehicle.DefaultImageURL + "\" width=\"250\"   /></a>");
                    builder.Append("</td>");
                    builder.Append("</tr>");
                }
            }


            builder.Append("</table>");

            builder.Append("</font></td>");

            return builder.ToString();
        }

        public static string GenerateThumbnail(WhitmanEntepriseVehicleInfo vehicle)
        {
            string tmp = vehicle.CarImageUrl;

            string[] totalImage = tmp.Split(new String[] { "|",","," " }, StringSplitOptions.RemoveEmptyEntries);

            var random = new Random();

            int randomNumber = random.Next(1000, 9999);

            string queryString = "Stock=" + vehicle.StockNumber + "&Vin=" + vehicle.Vin + "&partner=" + clsVariables.currentDealer.DealerId + randomNumber;
  

            var builder = new StringBuilder();

            if (totalImage.Any())
            {
                if (clsVariables.currentDealer.DealerId.Equals("4310") || clsVariables.currentDealer.DealerId.Equals("5780") || clsVariables.currentDealer.DealerId.Equals("1660"))


                    builder.Append("<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" + queryString + "\" target=\"_blank\"><img src=\"" + totalImage.First() + "\" height=\"150\" /></a>");

                else
                    builder.Append("<a href=\"" + clsVariables.currentDealer.WebSiteURL + "\" target=\"_blank\"><img src=\"" + totalImage.First() + "\" height=\"150\" /></a>");


               
            }
            else
            {
                if (String.IsNullOrEmpty(vehicle.DefaultImageURL))
                    builder.Append("<a href=\"" + clsVariables.currentDealer.WebSiteURL + "\" target=\"_blank\"><img border=\"0\" src=\"http://vinclapp.net/alpha/cListThemes/gloss/no-image-available.jpg\" height=\"150\"   /></a>");
                else
                {

                    if (clsVariables.currentDealer.DealerId.Equals("4310") || clsVariables.currentDealer.DealerId.Equals("5780") || clsVariables.currentDealer.DealerId.Equals("1660"))


                        builder.Append("<a href=\"http://webpics.us/WhitmanCraigslistServer/Request/DetailRequest?" + queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" + vehicle.DefaultImageURL + "\" height=\"150\"   /></a>");

                    else
                        builder.Append("<a href=\"" + clsVariables.currentDealer.WebSiteURL + "\" target=\"_blank\"><img border=\"0\" src=\"" + vehicle.DefaultImageURL + "\" height=\"150\"   /></a>");

                  
                }
            }
            
            return builder.ToString();
        }

        public static string GenerateDescription(WhitmanEntepriseVehicleInfo vehicle,string fontColor,string insertTemplate)
        {

            var builder = new StringBuilder();

            bool hasDescription = !String.IsNullOrEmpty(vehicle.Description);

            if (hasDescription)

                builder.Append("<td valign=\"top\" width=\"550\" bgcolor=\"" + insertTemplate + "\"><font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h3>Descriptions</h3>" + vehicle.Description + "</font></td>");

            else
                builder.Append("<td valign=\"top\" width=\"550\" bgcolor=\"" + insertTemplate + "\"><font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h3>Descriptions</h3>" + "CALL US FOR MORE INFO ABOUT VEHICLE" + "</font></td>");

            return builder.ToString();
        }



        public static string GenerateImageURL(WhitmanEntepriseVehicleInfo vehicle)
        {
            const string randomNumber = "AzByCxDwEvFuGtHsIrJqKpLoMnNmOlPkQjRiShTgUfVeWdXcYbZa1234567890";

            //string Vin = vehicle.Vin;

            //string stockNumber = vehicle.StockNumber;

            var random = new Random();

            var builder = new StringBuilder();

            //for (int i = 0; i < 15; i++)
            //    builder.Append(randomNumber[random.Next(randomNumber.Length)].ToString());

            //builder.Append("/");

            //builder.Append(clsVariables.currentDealer.DealerId);

            //builder.Append("/");

            for (int i = 0; i < 15; i++)
                builder.Append(randomNumber[random.Next(randomNumber.Length)].ToString(CultureInfo.InvariantCulture));

            //builder.Append("/");

            //builder.Append(Vin);

            return builder.ToString();



        }

        public static string GenerateImageFileName(WhitmanEntepriseVehicleInfo vehicle)
        {
            const string randomNumber = "MnNmOlPkQjRiShTyCxDwEvFugUfVeWdXcYbZaAzByCxDwEvFuGtHsIrJqKpLo1234567890";

            var random = new Random();

            var builder = new StringBuilder();

          
            for (int i = 0; i < 12; i++)
                builder.Append(randomNumber[random.Next(randomNumber.Length)].ToString(CultureInfo.InvariantCulture));

            builder.Append(".jpg");

            return builder.ToString();



        }

        public static string GenerateBottomImageFileName(WhitmanEntepriseVehicleInfo vehicle)
        {
            const string randomNumber = "DwEvFuGtHsIrJqXcYbZKiShTyCxDwEvFugUfVepLo1234567890MnNmOlPkQjRWdaAzByCx";

            var random = new Random();

            var builder = new StringBuilder();


            for (int i = 0; i < 12; i++)
                builder.Append(randomNumber[random.Next(randomNumber.Length)].ToString(CultureInfo.InvariantCulture));

            builder.Append(".jpg");

            return builder.ToString();



        }
        public static DataTable initalCraigslistTable()
        {
            DataTable workTable = new DataTable("Craiglist");


            workTable.Columns.Add("dealerid", typeof(String));
            workTable.Columns.Add("vin", typeof(String));
            workTable.Columns.Add("stocknumber", typeof(String));
            workTable.Columns.Add("type", typeof(String));
            workTable.Columns.Add("year", typeof(String));
            workTable.Columns.Add("make", typeof(String));
            workTable.Columns.Add("model", typeof(String));
            workTable.Columns.Add("trimlevel", typeof(String));
            workTable.Columns.Add("price", typeof(String));
            workTable.Columns.Add("exteriorcolor", typeof(String));
            workTable.Columns.Add("interiorcolor", typeof(String));
            workTable.Columns.Add("cylinders", typeof(String));
            workTable.Columns.Add("liters", typeof(String));
            workTable.Columns.Add("odometer", typeof(String));
            workTable.Columns.Add("fueltype", typeof(String));
            workTable.Columns.Add("transmissiontype", typeof(String));
            workTable.Columns.Add("drivetype", typeof(String));
            workTable.Columns.Add("options", typeof(String));
            workTable.Columns.Add("description", typeof(String));
            workTable.Columns.Add("webspecial", typeof(String));
            workTable.Columns.Add("msrp", typeof(String));


            workTable.Columns.Add("web1", typeof(String));
            workTable.Columns.Add("web2", typeof(String));
            workTable.Columns.Add("web3", typeof(String));
            workTable.Columns.Add("newused", typeof(String));
            workTable.Columns.Add("onholddate", typeof(String));
            workTable.Columns.Add("picurl", typeof(String));

            return workTable;

        }


        public static DataTable convertToCsvTableFromVinControl(DataTable dtInventory,int dealerId)
        {
            DataTable dtCraigsList = initalCraigslistTable();

            foreach (DataRow drRow in dtInventory.Rows)
            {
                DataRow dr = dtCraigsList.NewRow();

                dr["dealerid"] = dealerId;
                dr["vin"] = drRow.Field<string>("VINNumber");
                dr["stocknumber"] = drRow.Field<string>("StockNumber");
                dr["type"] = drRow.Field<string>("BodyType");


                dr["year"] = drRow.Field<int>("ModelYear").ToString();
                dr["make"] = drRow.Field<string>("Make");
                dr["model"] = drRow.Field<string>("Model");
                dr["trimlevel"] = drRow.Field<string>("Trim");
                dr["price"] = drRow.Field<string>("SalePrice");


                dr["exteriorcolor"] = drRow.Field<string>("ExteriorColor");
                dr["interiorcolor"] = drRow.Field<string>("InteriorColor");
                dr["cylinders"] = drRow.Field<string>("Cylinders");
                dr["liters"] = drRow.Field<string>("Liters");
                dr["odometer"] = drRow.Field<string>("Mileage");


                dr["fueltype"] = drRow.Field<string>("FuelType");
                dr["transmissiontype"] = drRow.Field<string>("Tranmission");
                dr["drivetype"] = drRow.Field<string>("DriveTrain");
                dr["options"] = drRow.Field<string>("CarsOptions");
                dr["description"] = drRow.Field<string>("Descriptions");


                dr["webspecial"] = "";
                dr["msrp"] = drRow.Field<string>("MSRP");

                dr["web1"] = "";
                dr["web2"] = "";
                dr["web3"] = "";
                dr["newused"] = "Used";
                dr["onholddate"] = "";
                dr["picurl"] = drRow.Field<string>("CarImageUrl");

                dtCraigsList.Rows.Add(dr);
            }

            return dtCraigsList;
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


        public static string GenerateHtmlImageCodeForCaliforniaBeemerByComputerAccount(WhitmanEntepriseVehicleInfo vehicle)
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

            builder.Append("  <tr><td align=\"center\"><h1><font color=\"red\">Call " + clsVariables.currentDealer.PhoneNumber + "</font></h1></td></tr>");

            builder.Append("    <tr>");

            builder.Append("<td align=\"center\">");

            builder.Append(vehicle.ModelYear + SPACE + vehicle.Make + SPACE + vehicle.Model + SPACE + "<br />");

            builder.Append("  <img  width=\"805\" src=\"" + vehicle.FirstImageUrl + "\">");

            builder.Append("</td>");

            builder.Append("</tr>");

            builder.Append(" <tr><td align=\"center\"><font color=\"red\"><h1>Call " + clsVariables.currentDealer.PhoneNumber + "</h1></font> " + clsVariables.currentDealer.DealershipName + "</td></tr>");

            builder.Append(" <tr>");

            builder.Append(" <td align=\"center\">");

            builder.Append("  Vehicle Location:<br />");

            builder.Append("  <font color=\"red\">" + clsVariables.currentDealer.DealershipName + "</font><br />");

            builder.Append("  <a href=\"" + clsVariables.currentDealer.WebSiteURL + "\">" + clsVariables.currentDealer.WebSiteURL + "</a><br />");

            builder.Append(clsVariables.currentDealer.StreetAddress + SPACE + clsVariables.currentDealer.StreetAddress + ", " + clsVariables.currentDealer.State + SPACE + clsVariables.currentDealer.ZipCode);

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

        public static string GenerateInfoOrderForCaliforniaBeemer(WhitmanEntepriseVehicleInfo vehicle)
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

        public static string GenerateImagesForCaliforniaBeemer(WhitmanEntepriseVehicleInfo vehicle)
        {
            var builder = new StringBuilder();

            string tmp = vehicle.CarImageUrl;

            var totalImage =
                tmp.Split(new String[] { "|", ",", " " }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var extractList = totalImage.Skip(1).ToList();

            builder.Append(" <img width=\"400\" src=\"" + totalImage.First()+ "\">");

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

        
    }







    

}

