using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Net;
using System.Windows.Forms;
using HiQPdf;
using Vinclapp.Craigslist;
using VinCLAPP.Model;

namespace VinCLAPP.Helper
{
    public class ImageHelper
    {
        public static ImageModel GenerateRunTimeImageBlob(VehicleInfo vehicle, City city, Template temp, bool hasPrice,
                                                          string dealerTitle)
        {
            var imageModel = new ImageModel();

            try
            {
                WebsitesScreenshot.WebsitesScreenshot _Obj;

                WebsitesScreenshot.WebsitesScreenshot _BottomObj;

                _Obj =
                    new WebsitesScreenshot.WebsitesScreenshot(
                        ConfigurationManager.AppSettings["WebScreenShotSerialKey"].ToString(
                            CultureInfo.InvariantCulture));

                _BottomObj =
                    new WebsitesScreenshot.WebsitesScreenshot(
                        ConfigurationManager.AppSettings["WebScreenShotSerialKey"].ToString(
                            CultureInfo.InvariantCulture));


                WebsitesScreenshot.WebsitesScreenshot.Result _Result;

                WebsitesScreenshot.WebsitesScreenshot.Result _BottomResult;

                string firstImageString = CommonHelper.GenerateHtmlImageCode(vehicle, temp.Outlook, temp.BackGround,
                                                                             temp.Insert, hasPrice, dealerTitle);

                _Result =
                    _Obj.CaptureHTML("<html><body>" +
                                     firstImageString +
                                     "</body></html>");

                string secondImageString = CommonHelper.GenerateHtmlImageCodeForSecondBottomImage(vehicle, temp.Outlook,
                                                                                                  temp.BackGround,
                                                                                                  temp.Insert, hasPrice,
                                                                                                  dealerTitle);

                if (!String.IsNullOrEmpty(secondImageString))

                    _BottomResult =
                        _BottomObj.CaptureHTML("<html><body>" + secondImageString + "</body></html>");
                else
                {
                    _BottomResult = _BottomObj.CaptureHTML("<html><body>" + firstImageString + "</body></html>");
                }


                if (_Result == WebsitesScreenshot.WebsitesScreenshot.Result.Captured &&
                    _BottomResult == WebsitesScreenshot.WebsitesScreenshot.Result.Captured)
                {
                    _Obj.ImageFormat = WebsitesScreenshot.WebsitesScreenshot.ImageFormats.JPG;

                    _BottomObj.ImageFormat = WebsitesScreenshot.WebsitesScreenshot.ImageFormats.JPG;

                    _Obj.DelaySeconds = 7;

                    _BottomObj.DelaySeconds = 7;

                    Bitmap _Image = _Obj.GetImage();

                    Bitmap _BottomImage = _BottomObj.GetImage();

                    var stream = new MemoryStream();

                    var bottomStream = new MemoryStream();

                    _Image.Save(stream, ImageFormat.Jpeg);

                    _BottomImage.Save(bottomStream, ImageFormat.Jpeg);

                    byte[] bytes = stream.ToArray();

                    byte[] bottombytes = bottomStream.ToArray();

                    imageModel.BottomImage = bottombytes;

                    imageModel.TopImage = bytes;

                    stream.Dispose();

                    stream.Close();

                    stream.Close();

                    bottomStream.Close();

                    _Obj.Dispose();

                    _BottomObj.Dispose();
                }
            }
            catch (Exception ex)
            {
                return imageModel;
            }


            return imageModel;
        }

        public static ImageModel GenerateRunTimePhysicalImageByComputerAccount(VehicleInfo vehicle)
        {
            var imageModel = new ImageModel {PhysicalImageUrl = new List<string>()};

          
            string[] carImage = vehicle.CarImageUrl.Split(new[] {",", "|"},
                                                          StringSplitOptions.RemoveEmptyEntries);

            var request = new WebClient();

         
            string physicalImagePath = @"C:\ImageWarehouse" + "\\" + vehicle.Vin;

            try
            {
                var dirNormal = new DirectoryInfo(physicalImagePath);

                if (!dirNormal.Exists)
                    dirNormal.Create();

                int index = 1;

                foreach (var tmp in carImage)
                {
                    string singleImagePath = dirNormal + "\\" + index + ".jpg";

                    ComputerAccountHelper.DownloadImage(request,tmp, singleImagePath);

                    imageModel.PhysicalImageUrl.Add(singleImagePath);

                    index++;

                    if (imageModel.PhysicalImageUrl.Count > 24)
                        break;
                }
              
              
            }
            catch (Exception ex)
            {
                
            }
            return imageModel;
        }
    }
}