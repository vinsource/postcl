using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Vinclapp.Craigslist;
using VinCLAPP.Model;
using System.Text.RegularExpressions;
namespace VinCLAPP
{
    public sealed class CommonHelper
    {
        private const string Space = " ";


        public static String MaskInput(String input, int charactersToShowAtEnd)
        {
            if (input.Length < charactersToShowAtEnd)
            {
                charactersToShowAtEnd = input.Length;
            }
            String endCharacters = input.Substring(input.Length - charactersToShowAtEnd);
            return String.Format(
              "{0}{1}",
              "".PadLeft(input.Length - charactersToShowAtEnd, '*'),
              endCharacters
              );
        }

        public static string UpperFirstLetterOfEachWord(string value)
        {
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
        }

        public static bool EmailTests(string email)
        {
            const string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                                   + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                                   + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }

        public static string RemoveSpecialCharactersForMsrp(string input)
        {
            if (!String.IsNullOrEmpty(input))
            {

                if (input.Contains("."))
                    input = input.Substring(0, input.IndexOf("."));

                return Regex.Replace(input, "[^a-zA-Z0-9]+", "", RegexOptions.Compiled);
            } return "0";


        }

        private static List<string> RandomzieTable(VehicleInfo vehicle, string fontColor, string insertTemplate)
        {
            var list = new List<string>();

            list.Add(GenerateOptions(vehicle, fontColor, insertTemplate));
            list.Add(GenerateDescription(vehicle, fontColor, insertTemplate));
            list.Add(GenerateImages(vehicle, fontColor, insertTemplate));

            return list.OrderBy(t => Guid.NewGuid()).ToList();
        }

        public static string GenerateSimiplifiedCraiglistContent(VehicleInfo vehicle, string colorTemplate,
                                                                 string insertTemplate, bool hasPrice,
                                                                 string dealerTitle)
        {
            string fontColor = "lavenderblush";
            switch (insertTemplate)
            {
                case "Gray":
                    fontColor = "lavenderblush";
                    break;
                case "White":
                    fontColor = "black";
                    break;
                default:
                    break;
            }


            var builder = new StringBuilder();

            List<string> listRandom = GetRandomImages(vehicle, fontColor, insertTemplate);

            var random = new Random();

            int randomNumber = random.Next(1000, 9999);

            string queryString = "Vin=" + vehicle.Vin + "&partner=" + GlobalVar.CurrentDealer.DealerId + randomNumber;


            builder.Append("<table align=\"center\" width=\"750\" cellpadding=\"10\" cellspacing=\"0\" bgcolor=\"" +
                           colorTemplate + "\">");

            builder.Append("<tr  bgcolor=\"" + colorTemplate + "\">");

            builder.Append("<td ></td>");

            builder.Append(" <td colspan=\"2\">");

            builder.Append("<table width=\"725\" cellspacing=\"0\" cellpadding=\"0\">");
            builder.Append("<tr>");
            builder.Append("<td>");

            builder.Append("<a href=\"" + GlobalVar.CurrentDealer.WebSiteUrl +
                           "\" target=\"_blank\"><img border=\"0\" src=\"" + GlobalVar.CurrentDealer.LogoUrl +
                           "\"/></a>");
            builder.Append("</td>");


            builder.Append("<td align=\"right\" >");

            builder.Append("<font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h2>" +
                           GlobalVar.CurrentDealer.DealershipName + "</h2></font><br />");

            builder.Append("<font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h2>" +
                           GlobalVar.CurrentDealer.PhoneNumber + "</h2></font><br />");

            if (!String.IsNullOrEmpty(dealerTitle.Trim()))
                builder.Append("<font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                               dealerTitle + "</font><br />");

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

            //if (vehicle.SalePrice>0)
            //{
            //    double price = 0;

            //    bool validPrice = Double.TryParse(vehicle.SalePrice, out price);

            //    hasPrice = (!hasPrice) && validPrice && (price > 0);

            //    if (hasPrice)
            //        builder.Append("<font size=\"+5\" color=\"" + fontColor +
            //                       "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + price.ToString("C") +
            //                       "</font>");
            //    else
            //    {
            //        builder.Append("<img border=\"0\" src=\"http://vinwindow.net/DealerLogo/blue.gif\"  />");
            //    }
            //}
            //else
            //{
            //    builder.Append("<font size=\"+3\" color=\"" + fontColor +
            //                   "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + "CALL FOR PRICE" + "</font>");
            //}

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
                           GlobalVar.CurrentDealer.StreetAddress + "</font><br />");
            builder.Append("<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                           GlobalVar.CurrentDealer.City + ", " + GlobalVar.CurrentDealer.State + " " +
                           GlobalVar.CurrentDealer.ZipCode + "</font><br />");
            builder.Append("</td>");


            //FIRST IMAGE BEGIN

            builder.Append(listRandom.ElementAt(0));
            ////FIRST IMAGE END

            builder.Append("</tr>");

            builder.Append("<tr>");

            builder.Append("<td bgcolor=\"" + insertTemplate + "\">");


            builder.Append("<a href=\"" + GlobalVar.CurrentDealer.WebSiteUrl +
                           "\" target=\"_blank\"><img border=\"0\" src=\"http://webpics.us/alpha/cListThemes/newportcoastauto/visitOurSite.png\" /></a><br/>");
            builder.Append("<a href=\"http://webpics.us/VinClappWeb/Email/ScheduleRequest?" + queryString +
                           "\" target=\"_blank\"><img border=\"0\" src=\"http://webpics.us/alpha/cListThemes/newportcoastauto/moreInfo.png\" /></a><br/>");
            builder.Append("<a href=\"http://webpics.us/VinClappWeb/Email/ScheduleRequest?" + queryString +
                           "\" target=\"_blank\"><img border=\"0\" src=\"http://webpics.us/alpha/cListThemes/newportcoastauto/emailUs.png\" /></a><br/>");
            builder.Append("<a href=\"http://webpics.us/VinClappWeb/Email/ScheduleRequest?" + queryString +
                           "\" target=\"_blank\"><img border=\"0\" src=\"http://webpics.us/alpha/cListThemes/newportcoastauto/getQuote.png\" /></a><br/>");
            builder.Append("<a href=\"" + GlobalVar.CurrentDealer.CreditUrl +
                           "\" target=\"_blank\"><img  border=\"0\" src=\"http://webpics.us/alpha/cListThemes/newportcoastauto/applyForCredit2.png\" /></a><br/>");
            builder.Append("</td>");

            ////SECOND IMAGE BEGINS

            builder.Append(listRandom.ElementAt(1));
            ////SECOND IMAGE END

            builder.Append("</tr>");


            builder.Append(" <tr>");
            builder.Append("<td bgcolor=\"" + insertTemplate +
                           "\"><a href=\"http://www.carfax.com/VehicleHistory/p/Report.cfx?partner=DVW_1&vin=" +
                           vehicle.Vin +
                           "\" target=\"_blank\"><img src=\"http://webpics.us/alpha/cListThemes/gloss/carfax_logo.jpg\" width=\"150\" /></a></td>");

            ////THRID IMAGE BEGIN
            builder.Append(listRandom.ElementAt(2));

            ////THIRD IMAGE ENDS

            builder.Append("</tr>");

            builder.Append(" <tr>");
            builder.Append("<td valign=\"top\" bgcolor=\"" + insertTemplate + "\"></td>");
            builder.Append("<td valign=\"top\" bgcolor=\"" + insertTemplate + "\"><font size=\"1\" color=\"" + fontColor +
                           "\" face=\"MS Serif, New York, serif\"></font></td>");
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

            builder.Append(" <td bgcolor=\"" + colorTemplate +
                           "\" align=\"right\"><a href=\"http://www.whitmanenterprise.net/\">V<span style=\"color: red;\">I</span>N</a> | Vehicle Inventory Network, LLC.</a></td>");
            builder.Append("<td></td>");
            builder.Append("  </tr>");
            builder.Append("</table>");

            return builder.ToString();
        }

        public static string GenerateHtmlImageCode(VehicleInfo vehicle, string outlook, string colorTemplate,
                                                   string insertTemplate, bool hasPrice, string dealerTitle)
        {
            if (outlook.Equals("Standard"))
            {
                string fontColor = "lavenderblush";
                switch (insertTemplate)
                {
                    case "Gray":
                        fontColor = "lavenderblush";
                        break;
                    case "White":
                        fontColor = "black";
                        break;
                    default:
                        break;
                }


                var builder = new StringBuilder();

                List<string> listRandom = RandomzieTable(vehicle, fontColor, insertTemplate);

                var random = new Random();

                int randomNumber = random.Next(1000, 9999);

                string queryString = "Vin=" + vehicle.Vin + "&partner=" + GlobalVar.CurrentDealer.DealerId +
                                     randomNumber;


                builder.Append("<table align=\"center\" width=\"750\" cellpadding=\"10\" cellspacing=\"0\" bgcolor=\"" +
                               colorTemplate + "\">");

                builder.Append("<tr  bgcolor=\"" + colorTemplate + "\">");

                builder.Append("<td ></td>");

                builder.Append(" <td colspan=\"2\">");

                builder.Append("<table width=\"725\" cellspacing=\"0\" cellpadding=\"0\">");
                builder.Append("<tr>");
                builder.Append("<td>");

                builder.Append("<a href=\"" + GlobalVar.CurrentDealer.WebSiteUrl +
                               "\" target=\"_blank\"><img border=\"0\" src=\"" + GlobalVar.CurrentDealer.LogoUrl +
                               "\"/></a>");
                builder.Append("</td>");


                builder.Append("<td align=\"right\" >");

                builder.Append(
                    " <font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h2>" +
                    GlobalVar.CurrentDealer.DealershipName + "</h2></font><br />");

                builder.Append(
                    "<font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h2>" +
                    GlobalVar.CurrentDealer.PhoneNumber + "</h2></font><br />");

                if (!String.IsNullOrEmpty(dealerTitle.Trim()))
                    builder.Append(
                        "  <font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                        dealerTitle + "</font><br />");
                if (GlobalVar.CurrentDealer.DealerId.Equals("1200"))
                {
                    builder.Append("<a href=\"http://webpics.us/VinClappWeb/Email/ScheduleRequest?" + queryString +
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

                //    hasPrice = (!hasPrice) && validPrice && (price > 0);

                //    if (hasPrice)
                //        builder.Append("<font size=\"+5\" color=\"" + fontColor +
                //                       "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + price.ToString("C") +
                //                       "</font>");
                //    else
                //        //builder.Append("<font size=\"+3\" color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + "CALL FOR PRICE" + "</font>");
                //        builder.Append("<img border=\"0\" src=\"http://vinwindow.net/DealerLogo/blue.gif\"  />");
                //}
                //else
                //    // builder.Append("<font size=\"+3\" color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + "CALL FOR PRICE" + "</font>");
                //    builder.Append("<img border=\"0\" src=\"http://vinwindow.net/DealerLogo/blue.gif\"  />");

                builder.Append("</td>");
                builder.Append("</tr>");

                builder.Append("<tr bgcolor=\"" + insertTemplate + "\"><td bgcolor=\"" + insertTemplate +
                               "\"></td></tr>");

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


                builder.Append(" <font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                               GlobalVar.CurrentDealer.DealershipName + "</font><br />");

                builder.Append("<font color=\"" + fontColor +
                               "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h3>" +
                               GlobalVar.CurrentDealer.PhoneNumber + "</h3></font><br />");
                builder.Append("  <font color=\"" + fontColor +
                               "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                               GlobalVar.CurrentDealer.StreetAddress + "</font><br />");
                builder.Append(" <font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                               GlobalVar.CurrentDealer.City + ", " + GlobalVar.CurrentDealer.State + " " +
                               GlobalVar.CurrentDealer.ZipCode + "</font><br />");
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
                               "\" target=\"_blank\"><img src=\"http://webpics.us/alpha/cListThemes/gloss/carfax_logo.jpg\" width=\"150\" /></a></td>");

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
                builder.Append(" <td bgcolor=\"" + colorTemplate + "\" align=\"left\"><font color=\"" + fontColor +
                               "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">Price is subject to change without notice</font></td>");
                builder.Append(" <td bgcolor=\"" + colorTemplate +
                               "\" align=\"right\"><a href=\"http://vehicleinventorynetwork.com/\"><img border=\"0\" src=\"http://vinwindow.net/vinclapplogo/vin-logo-white2.png\" /></a></td>");
                builder.Append("<td></td>");
                builder.Append("  </tr>");
                builder.Append("</table>");

                return builder.ToString();
            }
            else
            {
                string fontColor = "lavenderblush";
                switch (insertTemplate)
                {
                    case "Gray":
                        fontColor = "lavenderblush";
                        break;
                    case "White":
                        fontColor = "black";
                        break;
                    default:
                        break;
                }


                var builder = new StringBuilder();

                List<string> listRandom = GetRandomImages(vehicle, fontColor, insertTemplate);

                var random = new Random();

                int randomNumber = random.Next(1000, 9999);

                string queryString = "Vin=" + vehicle.Vin + "&partner=" + GlobalVar.CurrentDealer.DealerId +
                                     randomNumber;


                builder.Append("<table align=\"center\" width=\"750\" cellpadding=\"10\" cellspacing=\"0\" bgcolor=\"" +
                               colorTemplate + "\">");

                builder.Append("<tr  bgcolor=\"" + colorTemplate + "\">");

                builder.Append("<td ></td>");

                builder.Append(" <td colspan=\"2\">");

                builder.Append("<table width=\"725\" cellspacing=\"0\" cellpadding=\"0\">");
                builder.Append("<tr>");
                builder.Append("<td>");

                builder.Append("<a href=\"" + GlobalVar.CurrentDealer.WebSiteUrl +
                               "\" target=\"_blank\"><img border=\"0\" src=\"" + GlobalVar.CurrentDealer.LogoUrl +
                               "\"/></a>");
                builder.Append("</td>");


                builder.Append("<td align=\"right\" >");

                builder.Append(
                    "<font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h2>" +
                    GlobalVar.CurrentDealer.DealershipName + "</h2></font><br />");

                builder.Append(
                    "<font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h2>" +
                    GlobalVar.CurrentDealer.PhoneNumber + "</h2></font><br />");

                if (!String.IsNullOrEmpty(dealerTitle.Trim()))
                    builder.Append(
                        "<font color=\"lavenderblush\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                        dealerTitle + "</font><br />");

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
                               "\" background=\"http://vincontrol.com/alpha/cListThemes/gloss/shadow.png\"></td>");
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

                //    hasPrice = (!hasPrice) && validPrice && (price > 0);

                //    if (hasPrice)
                //        builder.Append("<font size=\"+5\" color=\"" + fontColor +
                //                       "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + price.ToString("C") +
                //                       "</font>");
                //    else
                //    {
                //        builder.Append("<img border=\"0\" src=\"http://vinwindow.net/DealerLogo/blue.gif\"  />");
                //    }
                //}
                //else
                //{
                //    builder.Append("<img border=\"0\" src=\"http://vinwindow.net/DealerLogo/blue.gif\"  />");
                //}

                builder.Append("</td>");
                builder.Append("</tr>");

                builder.Append("<tr bgcolor=\"" + insertTemplate + "\"><td bgcolor=\"" + insertTemplate +
                               "\"></td></tr>");

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
                               GlobalVar.CurrentDealer.StreetAddress + "</font><br />");
                builder.Append("<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                               GlobalVar.CurrentDealer.City + ", " + GlobalVar.CurrentDealer.State + " " +
                               GlobalVar.CurrentDealer.ZipCode + "</font><br />");
                builder.Append("</td>");


                //FIRST IMAGE BEGIN

                builder.Append(listRandom.ElementAt(0));
                ////FIRST IMAGE END

                builder.Append("</tr>");

                builder.Append("<tr>");

                builder.Append("<td bgcolor=\"" + insertTemplate + "\">");


                builder.Append("<a href=\"" + GlobalVar.CurrentDealer.WebSiteUrl +
                               "\" target=\"_blank\"><img border=\"0\" src=\"http://webpics.us/alpha/cListThemes/newportcoastauto/visitOurSite.png\" /></a><br/>");
                builder.Append("<a href=\"http://webpics.us/VinClappWeb/Email/ScheduleRequest?" + queryString +
                               "\" target=\"_blank\"><img border=\"0\" src=\"http://webpics.us/alpha/cListThemes/newportcoastauto/moreInfo.png\" /></a><br/>");
                builder.Append("<a href=\"http://webpics.us/VinClappWeb/Email/ScheduleRequest?" + queryString +
                               "\" target=\"_blank\"><img border=\"0\" src=\"http://webpics.us/alpha/cListThemes/newportcoastauto/emailUs.png\" /></a><br/>");
                builder.Append("<a href=\"http://webpics.us/VinClappWeb/Email/ScheduleRequest?" + queryString +
                               "\" target=\"_blank\"><img border=\"0\" src=\"http://webpics.us/alpha/cListThemes/newportcoastauto/getQuote.png\" /></a><br/>");
                builder.Append("<a href=\"" + GlobalVar.CurrentDealer.CreditUrl +
                               "\" target=\"_blank\"><img  border=\"0\" src=\"http://webpics.us/alpha/cListThemes/newportcoastauto/applyForCredit2.png\" /></a><br/>");
                builder.Append("</td>");

                ////SECOND IMAGE BEGINS

                builder.Append(listRandom.ElementAt(1));
                ////SECOND IMAGE END

                builder.Append("</tr>");


                builder.Append(" <tr>");
                builder.Append("<td bgcolor=\"" + insertTemplate +
                               "\"><a href=\"http://www.carfax.com/VehicleHistory/p/Report.cfx?partner=DVW_1&vin=" +
                               vehicle.Vin +
                               "\" target=\"_blank\"><img src=\"http://webpics.us/alpha/cListThemes/gloss/carfax_logo.jpg\" width=\"150\" /></a></td>");

                ////THRID IMAGE BEGIN
                builder.Append(listRandom.ElementAt(2));

                ////THIRD IMAGE ENDS

                builder.Append("</tr>");

                builder.Append(" <tr>");
                builder.Append("<td valign=\"top\" bgcolor=\"" + insertTemplate + "\"></td>");
                builder.Append("<td valign=\"top\" bgcolor=\"" + insertTemplate + "\"><font size=\"1\" color=\"" +
                               fontColor + "\" face=\"MS Serif, New York, serif\"> </font></td>");
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
                builder.Append(" <td bgcolor=\"" + colorTemplate +
                               "\" align=\"right\"><a href=\"http://vehicleinventorynetwork.com/\"><img border=\"0\" src=\"http://vinwindow.net/vinclapplogo/vin-logo-white2.png\" /></a></td>");
                builder.Append("<td></td>");
                builder.Append("  </tr>");
                builder.Append("</table>");

                return builder.ToString();
            }
        }

        public static string GenerateHtmlImageCodeForSecondBottomImage(VehicleInfo vehicle, string outlook,
                                                                       string colorTemplate, string insertTemplate,
                                                                       bool hasPrice, string dealerTitle)
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

            VehicleInfo firstvehilce =
                GlobalVar.CurrentDealer.Inventory.ElementAt(random.Next(0,
                                                                        GlobalVar.CurrentDealer.Inventory.Count -
                                                                        1));

            VehicleInfo secondvehilce =
                GlobalVar.CurrentDealer.Inventory.ElementAt(random.Next(0,
                                                                        GlobalVar.CurrentDealer.Inventory.Count -
                                                                        1));

            VehicleInfo thirdtvehilce =
                GlobalVar.CurrentDealer.Inventory.ElementAt(random.Next(0,
                                                                        GlobalVar.CurrentDealer.Inventory.Count -
                                                                        1));

            VehicleInfo fourthvehilce =
                GlobalVar.CurrentDealer.Inventory.ElementAt(random.Next(0,
                                                                        GlobalVar.CurrentDealer.Inventory.Count -
                                                                        1));


            builder.Append(
                "<table bgcolor=\"" + colorTemplate +
                "\" align=\"center\" width=\"750\" cellpadding=\"10\" cellspacing=\"0\" >");

            builder.Append("<tr  bgcolor=\"" + colorTemplate + "\">");


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

            builder.Append("  <td><img src=\"" + firstvehilce.FirstImageUrl + "\" width=\"165\" height=\"140\" /></td>");
            builder.Append("  <td><img src=\"" + secondvehilce.FirstImageUrl + "\" width=\"165\" height=\"140\" /></td>");
            builder.Append("  <td><img src=\"" + thirdtvehilce.FirstImageUrl + "\" width=\"165\" height=\"140\" /></td>");
            builder.Append("  <td><img src=\"" + fourthvehilce.FirstImageUrl + "\" width=\"165\" height=\"140\" /></td>");
            builder.Append("</tr>");

            builder.Append(" <tr>");

            builder.Append("  <td><font size=\"2\" color=\"white\">" + firstvehilce.ModelYear + Space +
                           firstvehilce.Make + Space + firstvehilce.Model + "</font></td>");
            builder.Append("  <td><font size=\"2\" color=\"white\"> " + secondvehilce.ModelYear + Space +
                           secondvehilce.Make + Space + secondvehilce.Model + "</font></td>");
            builder.Append("  <td><font size=\"2\" color=\"white\">" + thirdtvehilce.ModelYear + Space +
                           thirdtvehilce.Make + Space + thirdtvehilce.Model + "</font></td>");
            builder.Append("  <td><font size=\"2\" color=\"white\">" + fourthvehilce.ModelYear + Space +
                           fourthvehilce.Make + Space + fourthvehilce.Model + "</font></td>");
            builder.Append("</tr>");
            builder.Append("                        </table>");
            builder.Append(" </td>");
            builder.Append("    </tr>   ");
            builder.Append(" </table>");
            builder.Append(" </td>");
            builder.Append(" <td bgcolor=\"" + colorTemplate + "\"></td>");
            builder.Append("</tr>");
            builder.Append(" <tr bgcolor=\"" + colorTemplate + "\">");
            builder.Append("   <td></td>");
            builder.Append(" <td></td>");
            builder.Append(" </tr>");
            builder.Append("</table>");


            return builder.ToString();
        }

        public static string GenerateCraiglistContentByImage(VehicleInfo vehicle)
        {
            var builder = new StringBuilder();

            string defaultURL = "http://vinlineup.com/clapp/" + GlobalVar.CurrentDealer.DealershipName.Replace(" ", "-") +
                                "/" + GlobalVar.CurrentDealer.DealerId + "/" + vehicle.ModelYear + "-" +
                                vehicle.Make.Replace(" ", "-") + "-" + vehicle.Model.Replace(" ", "-") + "-" +
                                vehicle.Trim.Replace(" ", "-") + "/" + vehicle.Vin;

            string defaultDealerURL = "http://vinlineup.com/clapp/" +
                                      GlobalVar.CurrentDealer.DealershipName.Replace(" ", "-") + "/" +
                                      GlobalVar.CurrentDealer.DealerId;

            var random = new Random();

            int randomNumber = random.Next(1000, 9999);

            var domainRandomList = new List<String>();

            domainRandomList.AddRange(new[]
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
                });

            int domainandomIndex = randomNumber%domainRandomList.Count();

            string randomDomain = domainRandomList.ElementAt(domainandomIndex);

            builder.Append("<a href=\"" + defaultURL + "\" target=\"_blank\"><img src=\"" + randomDomain +
                           "ImageHandler/SelfSmartImageFetch.ashx?adsId=" + vehicle.TrackingId + "\" /></a><br/>");


            builder.Append("<a href=\"" + defaultDealerURL + "\" target=\"_blank\"><img src=\"" + randomDomain +
                           "ImageHandler/SelfSmartBImageFetch.ashx?adsId=" + vehicle.TrackingId + "\" /></a><br/>");


            builder.Append("<font size=\"-1\" color=\"beige\">" + GenerateRandomTextFromBook() + "</font><br/>" +
                           GlobalVar.CurrentDealer.DealerId + DateTime.Now.Month + DateTime.Now.Year + " " +
                           vehicle.StockNumber);

            return builder.ToString();
        }

        public static string GenerateInfoOrder(VehicleInfo vehicle, string fontColor)
        {
            var builder = new StringBuilder();

            var random = new Random();

            builder.Append("<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                           vehicle.ModelYear + "</font> ");
            builder.Append("<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                           vehicle.Make + "</font> ");
            builder.Append("<font color=\"" + fontColor + "\"face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                           vehicle.Model + "</font> ");
            builder.Append("<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                           vehicle.Trim + " </font><br />");


            var InfoRandomList = new List<String>();

            InfoRandomList.AddRange(new[]
                {
                    "<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + "VIN: " +
                    vehicle.Vin + "</font><br />",
                    "<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" + "Stock #" +
                    vehicle.StockNumber + "</font><br />",
                    "<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                    "Interior Color: " + vehicle.InteriorColor + "</font><br />",
                    "<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                    "Exterior Color: " + vehicle.ExteriorColor + "</font><br />",
                    "<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                    "Tranmission: " + vehicle.Tranmission + "</font><br />",
                    "<font color=\"" + fontColor + "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\">" +
                    "Mileage " + vehicle.Mileage + "</font><br />"
                });

            string[] randomInfoOrder = InfoRandomList.OrderBy(t => Guid.NewGuid()).ToArray();

            foreach (string tmp in randomInfoOrder)
            {
                builder.Append(tmp);
            }

            return builder.ToString();
        }

        public static string GenerateCraiglistTitle(VehicleInfo vehicle, bool checkPrice)
        {
            var builder = new StringBuilder();

            var random = new Random();

            var frontList = new List<string>();

            //frontList.AddRange(new string[]
            //    {
            //        "✰✪✰✪✰",
            //         "*******",
            //         "▄♦▄♦▄♦▄",
            //         "~~~~~",
            //         "◄►",
            //         "!!!!!!!^^^^!!!!!"
            //    });

            //string frontChunkNode = GlobalVar.ChunkList.First(t => t.ChunkName.Equals("FrontChunk")).ChunkStringValue;

            //frontList.AddRange(frontChunkNode.Split(new String[] { "," }, StringSplitOptions.None));

            int year = Convert.ToInt32(vehicle.ModelYear);

            if (year < DateTime.Now.Year - 1)
            {
                frontList.Add("Used");

            }

            //string middleChunkNode = GlobalVar.ChunkList.First(t => t.ChunkName.Equals("MiddleChunk")).ChunkStringValue;

            //string endChunkNode = GlobalVar.ChunkList.First(t => t.ChunkName.Equals("EndChunk")).ChunkStringValue;

            //string[] middleChunkList = middleChunkNode.Split(new String[] { "," }, StringSplitOptions.None);


            //string[] endChunkList = endChunkNode.Split(new String[] { "," }, StringSplitOptions.None);


            var afterMakeRandomList = new List<String>();

            afterMakeRandomList.AddRange(new String[]
                    {
                    String.IsNullOrEmpty(vehicle.ExteriorColor)?"":vehicle.ExteriorColor,
                    String.IsNullOrEmpty(vehicle.BodyType)?"":vehicle.BodyType,
                     String.IsNullOrEmpty(vehicle.Engine)?"":vehicle.Engine,
                     
                    });

            if (vehicle.Make.Equals("BMW") && vehicle.Model.ToLower().Contains("series"))
            {
                if (!String.IsNullOrEmpty(vehicle.Trim))
                    builder.Append(vehicle.ModelYear + Space + vehicle.Make + Space +
                                   vehicle.Trim);
                else
                {
                    builder.Append(vehicle.ModelYear + Space + vehicle.Make + Space +
                                   vehicle.Model);
                }
            }
            else
            {


                if (!String.IsNullOrEmpty(vehicle.Trim))
                    builder.Append(vehicle.ModelYear + Space + vehicle.Make + Space +
                                   vehicle.Model + Space +
                                   vehicle.Trim);
                else
                {
                    builder.Append(vehicle.ModelYear + Space + vehicle.Make + Space +
                                   vehicle.Model);
                }
            }

            //builder.Append(middleChunkList.ElementAt(random.Next(0, middleChunkList.Count())));

            //builder.Append(endChunkList.ElementAt(random.Next(0, endChunkList.Count())) + Space);

            return builder.ToString();
        }

        public static string GenerateRandomTextFromBook()
        {
            var builder = new StringBuilder();


            string charsToUse = GlobalVar.ChunkList.First(t => t.ChunkName.Equals("CrazyLetters")).ChunkStringValue;

            try
            {
                var random = new Random();

                for (int i = 0; i < 100; i++)
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

        public static string GenerateRandomTextBelowPic()
        {
            var builder = new StringBuilder();

            string belowPicChunk =
                GlobalVar.ChunkList.Where(t => t.ChunkName.Equals("BelowPicChunk")).First().ChunkStringValue;


            var random = new Random();

            string[] belowPicChunkList = belowPicChunk.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);

            builder.Append(belowPicChunkList.ElementAt(random.Next(0, belowPicChunkList.Count())));

            return builder.ToString();
        }

        public static string GenerateOptions(VehicleInfo vehicle, string fontColor, string insertTemplate)
        {
            string tmp = vehicle.Options;

            string[] totalOption = tmp.Split(new[] {"|", ","}, StringSplitOptions.RemoveEmptyEntries);

            IEnumerable<string> random10Options = totalOption.OrderBy(t => Guid.NewGuid()).ToList().Take(10);


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

        public static List<string> GetRandomImages(VehicleInfo vehicle, string fontColor, string insertTemplate)
        {
            string tmp = vehicle.CarImageUrl;

            List<string> totalImage = tmp.Split(new[] {"|", ","}, StringSplitOptions.RemoveEmptyEntries).ToList();

            var random = new Random();

            var list = new List<string>();


            int randomNumber = random.Next(1000, 9999);

            string queryString = "Vin=" + vehicle.Vin + "&partner=" + GlobalVar.CurrentDealer.DealerId + randomNumber;
            bool hasHeader = true;

            if (totalImage.Count() >= 3)
            {
                IEnumerable<string> randomOptions = totalImage.OrderBy(t => Guid.NewGuid()).ToList().Take(3);

                foreach (string imgTmp in randomOptions)
                {
                    var builder = new StringBuilder();

                    builder.Append("<td valign=\"top\" bgcolor=\"" + insertTemplate + "\">");

                    if (hasHeader)
                    {
                        builder.Append("<font color=\"" + fontColor +
                                       "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h3>Images</h3>");

                        hasHeader = false;
                    }

                    builder.Append("<table>");

                    builder.Append("<tr>");

                    builder.Append("<td>");

                    builder.Append("<a href=\"http://webpics.us/VinClappWeb/Email/ScheduleRequest?" + queryString +
                                   "\" target=\"_blank\"><img border=\"0\" src=\"" + imgTmp.Trim() +
                                   "\" width=\"500\"   /></a>");

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
                        var builder = new StringBuilder();

                        builder.Append("<td valign=\"top\" bgcolor=\"" + insertTemplate + "\">");

                        if (hasHeader)
                        {
                            builder.Append("<font color=\"" + fontColor +
                                           "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h3>Images</h3>");

                            hasHeader = false;
                        }

                        builder.Append("<table>");

                        builder.Append("<tr>");

                        builder.Append("<td>");

                        builder.Append("<a href=\"http://webpics.us/VinClappWeb/Email/ScheduleRequest?" + queryString +
                                       "\" target=\"_blank\"><img border=\"0\" src=\"" + totalImage.ElementAt(0).Trim() +
                                       "\" width=\"500\"   /></a>");

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
                        var builder = new StringBuilder();

                        builder.Append("<td valign=\"top\" bgcolor=\"" + insertTemplate + "\">");

                        if (hasHeader)
                        {
                            builder.Append("<font color=\"" + fontColor +
                                           "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h3>Images</h3>");

                            hasHeader = false;
                        }

                        builder.Append("<table>");

                        builder.Append("<tr>");

                        builder.Append("<td>");

                        builder.Append("<a href=\"http://webpics.us/VinClappWeb/Email/ScheduleRequest?" + queryString +
                                       "\" target=\"_blank\"><img border=\"0\" src=\"" + imgTmp.Trim() +
                                       "\" width=\"500\"   /></a>");

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
                        var builder = new StringBuilder();

                        builder.Append("<td valign=\"top\" bgcolor=\"" + insertTemplate + "\">");

                        if (hasHeader)
                        {
                            builder.Append("<font color=\"" + fontColor +
                                           "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h3>Images</h3>");

                            hasHeader = false;
                        }

                        builder.Append("<table>");

                        builder.Append("<tr>");

                        builder.Append("<td>");

                        if (String.IsNullOrEmpty(vehicle.DefaultImageUrl))

                            builder.Append("<a href=\"" + GlobalVar.CurrentDealer.WebSiteUrl +
                                           "\" target=\"_blank\"><img border=\"0\" src=\"http://webpics.us/alpha/cListThemes/gloss/no-image-available.jpg\" width=\"500\"   /></a>");
                        else
                            builder.Append("<a href=\"http://webpics.us/VinClappWeb/Email/ScheduleRequest?" +
                                           queryString + "\" target=\"_blank\"><img border=\"0\" src=\"" +
                                           vehicle.DefaultImageUrl + "\" width=\"500\"   /></a>");
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

        public static string GenerateImages(VehicleInfo vehicle, string fontColor, string insertTemplate)
        {
            string tmp = vehicle.CarImageUrl;

            List<string> totalImage = tmp.Split(new[] {"|", ","}, StringSplitOptions.RemoveEmptyEntries).ToList();

            var random = new Random();

            var builder = new StringBuilder();

            builder.Append("<td valign=\"top\" bgcolor=\"" + insertTemplate + "\">");

            builder.Append("<font color=\"" + fontColor +
                           "\" face=\"Trebuchet MS, Arial, Helvetica, sans-serif\"><h3>Images</h3>");

            builder.Append("<table>");

            int count = 0;

            int randomNumber = random.Next(1000, 9999);

            string queryString = "Vin=" + vehicle.Vin + "&partner=" + GlobalVar.CurrentDealer.DealerId + randomNumber;

            if (totalImage.Count() >= 4)
            {
                IEnumerable<string> randomOptions = totalImage.OrderBy(t => Guid.NewGuid()).ToList().Take(4);

                while (count < randomOptions.Count())
                {
                    if (count%2 == 0)
                        builder.Append("<tr>");
                    builder.Append("<td>");


                    builder.Append("<a href=\"http://webpics.us/VinClappWeb/Email/ScheduleRequest?" + queryString +
                                   "\" target=\"_blank\"><img border=\"0\" src=\"" +
                                   randomOptions.ElementAt(count).Trim() + "\" width=\"250\"   /></a>");
                    builder.Append("</td>");
                    builder.Append("<td>");
                    builder.Append("<a href=\"http://webpics.us/VinClappWeb/Email/ScheduleRequest?" + queryString +
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
                    builder.Append("<a href=\"http://webpics.us/VinClappWeb/Email/ScheduleRequest?" + queryString +
                                   "\" target=\"_blank\"><img  border=\"0\" src=\"" + totalImage.ElementAt(0).Trim() +
                                   "\" width=\"250\"   /></a>");
                    builder.Append("</td>");
                    builder.Append("</tr>");
                }
                else if (totalImage.Count() == 2)
                {
                    builder.Append("<tr>");
                    builder.Append("<td>");
                    builder.Append("<a href=\"http://webpics.us/VinClappWeb/Email/ScheduleRequest?" + queryString +
                                   "\" target=\"_blank\"><img border=\"0\" src=\"" + totalImage.ElementAt(0).Trim() +
                                   "\" width=\"250\"   /></a>");
                    builder.Append("</td>");
                    builder.Append("<td>");
                    builder.Append("<a href=\"http://webpics.us/VinClappWeb/Email/ScheduleRequest?" + queryString +
                                   "\" target=\"_blank\"><img border=\"0\" src=\"" + totalImage.ElementAt(1).Trim() +
                                   "\" width=\"250\"   /></a>");
                    builder.Append("</td>");
                    builder.Append("</tr>");
                }
                else if (totalImage.Count() == 3)
                {
                    builder.Append("<tr>");
                    builder.Append("<td>");
                    builder.Append("<a href=\"http://webpics.us/VinClappWeb/Email/ScheduleRequest?" + queryString +
                                   "\" target=\"_blank\"><img border=\"0\" src=\"" + totalImage.ElementAt(0).Trim() +
                                   "\" width=\"250\"   /></a>");
                    builder.Append("</td>");
                    builder.Append("<td>");
                    builder.Append("<a href=\"http://webpics.us/VinClappWeb/Email/ScheduleRequest?" + queryString +
                                   "\" target=\"_blank\"><img border=\"0\" src=\"" + totalImage.ElementAt(1).Trim() +
                                   "\" width=\"250\"   /></a>");
                    builder.Append("</td>");
                    builder.Append("</tr>");
                    builder.Append("<tr>");
                    builder.Append("<td>");
                    builder.Append("<a href=\"http://webpics.us/VinClappWeb/Email/ScheduleRequest?" + queryString +
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
                        builder.Append("<a href=\"" + GlobalVar.CurrentDealer.WebSiteUrl +
                                       "\" target=\"_blank\"><img border=\"0\" src=\"http://webpics.us/alpha/cListThemes/gloss/no-image-available.jpg\" width=\"250\"   /></a>");
                    else
                        builder.Append("<a href=\"http://webpics.us/VinClappWeb/Email/ScheduleRequest?" + queryString +
                                       "\" target=\"_blank\"><img border=\"0\" src=\"" + vehicle.DefaultImageUrl +
                                       "\" width=\"250\"   /></a>");
                    builder.Append("</td>");
                    builder.Append("</tr>");
                }
            }


            builder.Append("</table>");

            builder.Append("</font></td>");

            return builder.ToString();
        }

        public static string GenerateThumbnail(VehicleInfo vehicle)
        {
            string tmp = vehicle.CarImageUrl;

            string[] totalImage = tmp.Split(new[] {"|", ","}, StringSplitOptions.RemoveEmptyEntries);

            var random = new Random();

            int randomNumber = random.Next(1000, 9999);

            string queryString = "Vin=" + vehicle.Vin + "&partner=" + GlobalVar.CurrentDealer.DealerId + randomNumber;


            var builder = new StringBuilder();

            if (totalImage != null && totalImage.Count() > 0)
                builder.Append("<a href=\"http://webpics.us/VinClappWeb/Email/ScheduleRequest?" + queryString +
                               "\" target=\"_blank\"><img src=\"" + totalImage.First() + "\" height=\"150\" /></a>");
            else
            {
                if (String.IsNullOrEmpty(vehicle.DefaultImageUrl))
                    builder.Append("<a href=\"" + GlobalVar.CurrentDealer.WebSiteUrl +
                                   "\" target=\"_blank\"><img border=\"0\" src=\"http://webpics.us/alpha/cListThemes/gloss/no-image-available.jpg\" height=\"150\"   /></a>");
                else
                    builder.Append("<a href=\"http://webpics.us/VinClappWeb/Email/ScheduleRequest?" + queryString +
                                   "\" target=\"_blank\"><img border=\"0\" src=\"" + vehicle.DefaultImageUrl +
                                   "\" height=\"150\"   /></a>");
            }

            return builder.ToString();
        }

        public static string GenerateDescription(VehicleInfo vehicle, string fontColor, string insertTemplate)
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

        public static string GenerateImageUrl(VehicleInfo vehicle)
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

        public static string GenerateImageFileName(VehicleInfo vehicle)
        {
            const string randomNumber = "AzByCxDwEvFuGtHsIrJqKpLoMnNmOlPkQjRiShTgUfVeWdXcYbZa1234567890";

            //string stockNumber = vehicle.StockNumber;

            var random = new Random();

            var builder = new StringBuilder();

            //builder.Append(stockNumber);

            //builder.Append("-");

            for (int i = 0; i < 12; i++)
                builder.Append(randomNumber[random.Next(randomNumber.Length)].ToString(CultureInfo.InvariantCulture));

            builder.Append(".jpg");

            return builder.ToString();
        }

        public static string GenerateBottomImageFileName(VehicleInfo vehicle)
        {
            const string randomNumber = "DwEvFuGtHsIrJqXcYbZKiShTyCxDwEvFugUfVepLo1234567890MnNmOlPkQjRWdaAzByCx";

            var random = new Random();

            var builder = new StringBuilder();


            for (int i = 0; i < 12; i++)
                builder.Append(randomNumber[random.Next(randomNumber.Length)].ToString(CultureInfo.InvariantCulture));

            builder.Append(".jpg");

            return builder.ToString();
        }

        public static string GetIp()
        {
            IPHostEntry ipHost = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            return ipAddr.ToString();
        }

        public static string GetComputerName()
        {
            return SystemInformation.ComputerName;
        }
    }
}