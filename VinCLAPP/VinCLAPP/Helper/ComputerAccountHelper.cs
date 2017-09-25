using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Vinclapp.Craigslist;
using VinCLAPP.Model;

namespace VinCLAPP.Helper
{
    public class ComputerAccountHelper
    {
        public static string GenerateHtmlImageCodeOverlay(VehicleInfo vehicle, string imgUrl)
        {
            var builder = new StringBuilder();

            builder.Append("<html>");

            builder.Append("<head>");

            builder.Append("  <title></title>");

            builder.Append("  <style type=\"text/css\">");

            builder.Append(
                " .image-container {    width: 560px;height: 430px;      font-family: sans-serif;     overflow: hidden;   position: relative;padding: 20px;background: url('http://vinclapp.net/alpha/bg.jpg') top left no-repeat;padding-bottom: 0px;}");

            builder.Append(".image {text-align: center;background: #222;}.image img {height: 350px;}");

            builder.Append(
                "  .top-overlay {clear: both;overflow: hidden;background: url('http://vinclapp.net/alpha/red-repeat.jpg') top left repeat-x;text-shadow: 0px 0px 4px darkred;color: #ffffff;padding: 10px;width: 540px;margin: 0 auto;}");

            builder.Append(
                ".top-overlay h1, h3 {display: inline-block;padding: 0;margin: 0;text-shadow: 1px 1px 3px black;margin: 0px;}.top-overlay h1 {float: right;text-align: right;width: 35%;font-size: 1.2em;}.top-overlay h3 {float: left;width: 65%;font-size: 1.0em;}.bottom-overlay {text-align: center;color: white;padding: 5px;font-size: .9em;}");

            builder.Append(
                ".thumbnail {width: 261px;overflow: hidden;float: left;background: #000;text-align: center;}.thumbnail img {height: 181px;}.vehicle-info {width:48%;position: absolute;top: 20px;right: 20px;margin: 0;padding: 0;list-style-type: none;overflow: hidden;}.top-description {overflow: hidden;}.vehicle-info h4 {margin: 0; padding: 0;}.vehicle-info li {padding: 7px;padding-left: 8px;background: #333;color: white;font-size: .7em;}.vehicle-info li:nth-child(2n) {background: #444;}");

            builder.Append(
                " .vehicle-info .header {background: url('http://vinclapp.net/alpha/red-repeat.jpg') top left repeat-x;text-shadow: 0px 0px 4px darkred;color: white;margin-bottom: 12px;}");

            builder.Append(
                " .bottom-description {background: black;color: white;margin-bottom: 0;margin-top:0;}.bottom-description h1 {text-align: center;background: url('red-repeat.jpg') top left repeat-x;text-shadow: 0px 0px 4px darkred;padding: 10px;margin: 0px;margin-top: 12px;font-size: 1.4em;}.bottom-description p {font-size: .70em;overflow: hidden;margin-top: 0px;padding: 10px;background: #222222;border-top: 1px black solid;}</style></head>");

            builder.Append("<body>");

            builder.Append("<div class=\"image-container\">");

            builder.Append("  <div class=\"top-overlay\">");

            builder.Append(" <h3>" + GlobalVar.CurrentDealer.DealershipName + "</h3>");

            builder.Append("<h1>" + GlobalVar.CurrentDealer.PhoneNumber + "</h1>");

            builder.Append("  </div>");

            builder.Append("  <div class=\"image\"><img src=" + imgUrl + "></div>");

            builder.Append("  <div class=\"bottom-overlay\">");

            if(String.IsNullOrEmpty(GlobalVar.CurrentDealer.DealerTitle))

            builder.Append(GlobalVar.CurrentDealer.StreetAddress + " " + GlobalVar.CurrentDealer.City + ", " +
                           GlobalVar.CurrentDealer.State + " " + GlobalVar.CurrentDealer.ZipCode);
            else
            {
                builder.Append(GlobalVar.CurrentDealer.DealerTitle);
            }

            builder.Append(" </div>");

            builder.Append("</div>");

            builder.Append("</body>");

            builder.Append("</html>");


            return builder.ToString();
        }

        public static string GenerateHtmlImageCodeSnapshotInfo(VehicleInfo vehicle)
        {
            var builder = new StringBuilder();

            builder.Append("<html>");

            builder.Append("<head>");

            builder.Append("  <title></title>");

            builder.Append("  <style type=\"text/css\">");

            builder.Append(
                " .image-container {    width: 560px;height: 430px;      font-family: sans-serif;     overflow: hidden;   position: relative;padding: 20px;background: url('http://vinclapp.net/alpha/bg.jpg') top left no-repeat;padding-bottom: 0px;}");

            builder.Append(".image {text-align: center;background: #222;}.image img {height: 350px;}");

            builder.Append(
                "  .top-overlay {clear: both;overflow: hidden;background: url('http://vinclapp.net/alpha/red-repeat.jpg') top left repeat-x;text-shadow: 0px 0px 4px darkred;color: #ffffff;padding: 10px;width: 540px;margin: 0 auto;}");

            builder.Append(
                ".top-overlay h1, h3 {display: inline-block;padding: 0;margin: 0;text-shadow: 1px 1px 3px black;margin: 0px;}.top-overlay h1 {float: right;text-align: right;width: 35%;font-size: 1.2em;}.top-overlay h3 {float: left;width: 65%;font-size: 1.0em;}.bottom-overlay {text-align: center;color: white;padding: 5px;font-size: .9em;}");

            builder.Append(
                ".thumbnail {width: 261px;overflow: hidden;float: left;background: #000;text-align: center;}.thumbnail img {height: 181px;}.vehicle-info {width:48%;position: absolute;top: 20px;right: 20px;margin: 0;padding: 0;list-style-type: none;overflow: hidden;}.top-description {overflow: hidden;}.vehicle-info h4 {margin: 0; padding: 0;}.vehicle-info li {padding: 7px;padding-left: 8px;background: #333;color: white;font-size: .7em;}.vehicle-info li:nth-child(2n) {background: #444;}");

            builder.Append(
                " .vehicle-info .header {background: url('http://vinclapp.net/alpha/red-repeat.jpg') top left repeat-x;text-shadow: 0px 0px 4px darkred;color: white;margin-bottom: 12px;}");

            builder.Append(
                " .bottom-description {background: black;color: white;margin-bottom: 0;margin-top:0;}.bottom-description h1 {text-align: center;background: url('red-repeat.jpg') top left repeat-x;text-shadow: 0px 0px 4px darkred;padding: 10px;margin: 0px;margin-top: 12px;font-size: 1.4em;}.bottom-description p {font-size: .70em;overflow: hidden;margin-top: 0px;padding: 10px;background: #222222;border-top: 1px black solid;}</style></head>");

            builder.Append("<body>");
            builder.Append("<div class=\"image-container\">");

            builder.Append("  <div class=\"top-description\">");

            builder.Append("    <div class=\"thumbnail\">");

            builder.Append("      <img src=\"" + vehicle.FirstImageUrl + "\">");

            builder.Append(" </div>");

            builder.Append("    <ul class=\"vehicle-info\">");

            builder.Append("   <li class=\"header\"><h4>" + vehicle.ModelYear + " " + vehicle.Make + " " + vehicle.Model +
                           " " + vehicle.Trim + "</h4></li>");

            builder.Append("  <li><b>Sale Price: </b>" + vehicle.SalePrice + "</li>");

            builder.Append("  <li><b>Mileage: </b>" + vehicle.Mileage + "</li>");

            builder.Append("  <li><b>Ext. Color: </b>" + vehicle.ExteriorColor + "e</li>");

            builder.Append("  <li><b>Transmission: </b>" + vehicle.Tranmission + "</li>");

            builder.Append("    <li><b>Stock: </b>" + vehicle.StockNumber + "</li>");
            builder.Append("   </ul>");

            builder.Append("  </div>");

            builder.Append("<div class=\"bottom-description\">");

            builder.Append("<h1>" + GlobalVar.CurrentDealer.WebSiteUrl + "</h1>");

            builder.Append("<h1>For more information call Dealer at " + GlobalVar.CurrentDealer.PhoneNumber +"</h1>");

            //builder.Append("<h3>Price is subject to change without notice</h3>");

            builder.Append(" </div>");

            builder.Append("</div>");

            builder.Append("</body>");

            builder.Append("</html>");


            return builder.ToString();
        }

        public static string GenerateHtmlImageCodeSnapshotInfoLayout1(VehicleInfo vehicle)
        {
            var builder = new StringBuilder();

            builder.Append("<html>");

            builder.Append("<head>");

            builder.Append("  <title></title>");

            builder.Append("  <style type=\"text/css\">");

            builder.Append(
                " .ad-container {width: 600px;height: 450px;background: white;overflow: hidden;position: relative;border: 1px solid black;}.ad-container .image-box {display: table-cell;vertical-align: middle;text-align: center;}.ad-container .image-box img {max-width: 100%;max-height: 100%;vertical-align: middle;}.image-box.double img{width: 49%;}/* Layout 1 */#super-simple .image-box {height: 393px;width: 600px;}#super-simple .footer {text-align: center;position: absolute;bottom: 0;padding-top: 10px;padding-bottom: 10px;width: 100%;font-size: 2em;font-weight: bold;color: black;background: rgba(255,255,255,1);}/* Layout 2 */#image-only .image-box {height: 365px;width: 600px;}#image-only .header, #image-only .footer {padding-top: 8px;padding-bottom: 8px;text-align: center;font-size: 1.5em;/*text-shadow: 2px 1px 1px black;*/font-weight: bolder;/*color: white;*/background: white;}/* Layout 3 */#two-image .image-box {height: 221px;width: 600px;}#two-image .header, #two-image .footer {padding-top: 8px;padding-bottom: 8px;text-align: center;font-size: 1.5em;/*text-shadow: 2px 1px 1px black;*/font-weight: bolder;/*color: white;*/background: white;}#two-image .content {padding: 10px;font-size: .9em}/* Layout 4 */#stacked-double .image-box {height: 442px;width: 300px;float: left;}#stacked-double .header, #stacked-double .footer {padding-top: 8px;padding-bottom: 8px;text-align: center;font-size: 1.5em;/*text-shadow: 2px 1px 1px black;*/font-weight: bolder;/*color: white;*/background: white;}#stacked-double .header {width: 300px;float: right;}#stacked-double .content {padding: 10px;font-size: .9em;width: 275px;float: right;overflow: hidden;}#stacked-double ul {font-size: 1.4em;list-style-type: none;margin: 0;padding: 0;padding-bottom: 30px;padding-top: 30px;text-align: center;}");
          
            builder.Append("  </style></head><body>");

        

            if(String.IsNullOrEmpty(vehicle.FirstImageUrl))
                builder.Append("<div id=\"image-only\" class=\"ad-container\"><div class=\"header\">Visit us at " + GlobalVar.CurrentDealer.WebSiteUrl + "!</div><div class=\"image-box\"><img src=\"http://vinclapp.net/alpha/coming_soon_pic.jpg\"></div><div class=\"footer\">Call Now! - " + GlobalVar.CurrentDealer.PhoneNumber + "</div></div>");
            else
            {
                builder.Append("<div id=\"image-only\" class=\"ad-container\"><div class=\"header\">Visit us at " + GlobalVar.CurrentDealer.WebSiteUrl + "!</div><div class=\"image-box\"><img src=\"" + vehicle.FirstImageUrl + "\"></div><div class=\"footer\">Call Now! - " + GlobalVar.CurrentDealer.PhoneNumber + "</div></div>");
            }
            
            
            builder.Append("</body>");

            builder.Append("</html>");


            return builder.ToString();
        }
        public static string GenerateHtmlImageCodeSnapshotInfoLayout2(VehicleInfo vehicle)
        {
            var builder = new StringBuilder();

            builder.Append("<html>");

            builder.Append("<head>");

            builder.Append("  <title></title>");

            builder.Append("  <style type=\"text/css\">");

            builder.Append(" .ad-container {width: 600px;height: 450px;background: white;overflow: hidden;position: relative;border: 1px solid black;}.ad-container .image-box {display: table-cell;vertical-align: middle;text-align: center;}.ad-container .image-box img {max-width: 100%;max-height: 100%;vertical-align: middle;}.image-box.double img{width: 49%;}/* Layout 1 */#super-simple .image-box {height: 393px;width: 600px;}#super-simple .footer {text-align: center;position: absolute;bottom: 0;padding-top: 10px;padding-bottom: 10px;width: 100%;font-size: 2em;font-weight: bold;color: black;background: rgba(255,255,255,1);}/* Layout 2 */#image-only .image-box {height: 365px;width: 600px;}#image-only .header, #image-only .footer {padding-top: 8px;padding-bottom: 8px;text-align: center;font-size: 1.5em;/*text-shadow: 2px 1px 1px black;*/font-weight: bolder;/*color: white;*/background: white;}/* Layout 3 */#two-image .image-box {height: 221px;width: 600px;}#two-image .header, #two-image .footer {padding-top: 8px;padding-bottom: 8px;text-align: center;font-size: 1.5em;/*text-shadow: 2px 1px 1px black;*/font-weight: bolder;/*color: white;*/background: white;}#two-image .content {padding: 10px;font-size: .9em}/* Layout 4 */#stacked-double .image-box {height: 442px;width: 300px;float: left;}#stacked-double .header, #stacked-double .footer {padding-top: 8px;padding-bottom: 8px;text-align: center;font-size: 1.5em;/*text-shadow: 2px 1px 1px black;*/font-weight: bolder;/*color: white;*/background: white;}#stacked-double .header {width: 300px;float: right;}#stacked-double .content {padding: 10px;font-size: .9em;width: 275px;float: right;overflow: hidden;}#stacked-double ul {font-size: 1.4em;list-style-type: none;margin: 0;padding: 0;padding-bottom: 30px;padding-top: 30px;text-align: center;}");

            builder.Append("  </style></head><body>");
            
            builder.Append("<div id=\"two-image\" class=\"ad-container\"><div class=\"header\">Visit us at  " + GlobalVar.CurrentDealer.WebSiteUrl + "</div><div class=\"image-box double\"><img src=\"" + vehicle.FirstImageUrl + "\"></div><div class=\"content\"><p>" + vehicle.Description + "</p></div><div class=\"footer\">Call Now! - " + GlobalVar.CurrentDealer.PhoneNumber + "</div></div>");

            builder.Append("</body>");

            builder.Append("</html>");


            return builder.ToString();
        }

        public static void DownloadImage(WebClient request, string webpath, string localPath)
        {
            byte[] byteStream = request.DownloadData(webpath);

            using (var fs = new FileStream(localPath, FileMode.Create))
            {
                fs.Write(byteStream, 0, byteStream.Length);

            }
        }

        public static string GenerateCraiglistContentBySimpleText(VehicleInfo vehicle)
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
            
            //if (!String.IsNullOrEmpty(vehicle.Description))
            //    builder.Append("<b>Description : </b><br><br>" + vehicle.Description + "<br><br>");
            
            builder.Append("If you have any questions, please contact us by clicking the ‘reply’ button at the top of this ad.");
            builder.Append("<br>");
            if (!String.IsNullOrEmpty(GlobalVar.CurrentDealer.EndingSentence))
                builder.Append(GlobalVar.CurrentDealer.EndingSentence+"<br>");
            return builder.ToString();
        }
    }
}