using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using ClappManagementSystem.Model;
using HiQPdf;

namespace ClappManagementSystem.Helper
{
    public class ImageHelper
    {

        public static ImageModel GenerateRunTimeImageBlobByComputerAccount(WhitmanEntepriseMasterVehicleInfo vehicle)
        {

            var imageModel = new ImageModel();

            WebsitesScreenshot.WebsitesScreenshot _Obj = null;

            WebsitesScreenshot.WebsitesScreenshot _BottomObj = null;

            try
            {
                if (vehicle.DealerId != 18498)
                {

                    _Obj =
                        new WebsitesScreenshot.WebsitesScreenshot(
                            System.Configuration.ConfigurationManager.AppSettings["WebScreenShotSerialKey"].ToString
                                (CultureInfo.InvariantCulture));

                    _BottomObj =
                        new WebsitesScreenshot.WebsitesScreenshot(
                            System.Configuration.ConfigurationManager.AppSettings["WebScreenShotSerialKey"].ToString
                                (CultureInfo.InvariantCulture));

                    //Dynamic FTP ACCOUNT



                    WebsitesScreenshot.WebsitesScreenshot.Result _Result;

                    WebsitesScreenshot.WebsitesScreenshot.Result _BottomResult;

                    if (vehicle.DealerId == 113738 || vehicle.DealerId == 3738)
                    {
                        var firstImageString = ComputerAccountHelper.GenerateHtmlImageCodeForAudiByComputerAccount(
                          vehicle);

                        _Result =
                            _Obj.CaptureHTML("<html><body>" + firstImageString + "</body></html>");

                        var secondImageString = ComputerAccountHelper.GenerateHtmlImageCodeForSecondBottomImage(
                            vehicle);

                        if (!String.IsNullOrEmpty(secondImageString))

                            _BottomResult =
                                _BottomObj.CaptureHTML("<html><body>" + secondImageString + "</body></html>");
                        else
                        {
                            _BottomResult = _BottomObj.CaptureHTML("<html><body>" + firstImageString + "</body></html>");
                        }
                    }

                    else
                    {
                        var firstImageString = ComputerAccountHelper.GenerateHTMLImageCode(
                            vehicle);


                        _Result =
                            _Obj.CaptureHTML("<html><body>" +
                                             firstImageString + "</body></html>");

                        var secondImageString = ComputerAccountHelper.GenerateHtmlImageCodeForSecondBottomImage(
                              vehicle);


                        if (!String.IsNullOrEmpty(secondImageString))

                            _BottomResult =
                                _BottomObj.CaptureHTML("<html><body>" +
                                                       secondImageString + "</body></html>");
                        else
                        {
                            _BottomResult = _BottomObj.CaptureHTML("<html><body>" + firstImageString + "</body></html>");
                        }
                    }



                    if (_Result == WebsitesScreenshot.WebsitesScreenshot.Result.Captured &&
                        _BottomResult == WebsitesScreenshot.WebsitesScreenshot.Result.Captured)
                    {
                        _Obj.ImageFormat = WebsitesScreenshot.
                            WebsitesScreenshot.ImageFormats.JPG;

                        _BottomObj.ImageFormat = WebsitesScreenshot.
                            WebsitesScreenshot.ImageFormats.JPG;

                        _Obj.DelaySeconds = 10;

                        _BottomObj.DelaySeconds = 10;


                        var _Image = _Obj.GetImage();

                        var _BottomImage = _BottomObj.GetImage();

                        var stream = new MemoryStream();

                        var bottomStream = new MemoryStream();

                        _Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);

                        _BottomImage.Save(bottomStream, System.Drawing.Imaging.ImageFormat.Jpeg);

                        var bytes = stream.ToArray();

                        var bottombytes = bottomStream.ToArray();

                        imageModel.BottomImage = bottombytes;

                        imageModel.TopImage = bytes;

                        stream.Dispose();

                        stream.Close();

                        bottomStream.Dispose();

                        bottomStream.Close();

                        _Obj.Dispose();

                        _BottomObj.Dispose();

                    }
                }
                else
                {
                    _BottomObj =
                        new WebsitesScreenshot.WebsitesScreenshot(
                            System.Configuration.ConfigurationManager.AppSettings["WebScreenShotSerialKey"].ToString
                                (CultureInfo.InvariantCulture));

                    WebsitesScreenshot.WebsitesScreenshot.Result _BottomResult;

                    _BottomResult =
                        _BottomObj.CaptureHTML("<html><body>" +
                                               ComputerAccountHelper.GenerateHtmlImageCodeForSecondBottomImage(
                                                   vehicle) + "</body></html>");

                    if (_BottomResult == WebsitesScreenshot.WebsitesScreenshot.Result.Captured)
                    {
                        _BottomObj.ImageFormat = WebsitesScreenshot.
                            WebsitesScreenshot.ImageFormats.JPG;

                        _BottomObj.DelaySeconds = 5;


                        var _BottomImage = _BottomObj.GetImage();

                        var htmlToImageConverter = new HtmlToImage();

                        htmlToImageConverter.SerialNumber = ConfigurationManager.AppSettings["PDFSerialNumber"];
                        // set browser width
                        htmlToImageConverter.BrowserWidth = 1200;

                        // set HTML Load timeout
                        htmlToImageConverter.HtmlLoadedTimeout = 5;

                        // set whether the resulted image is transparent 
                        htmlToImageConverter.TransparentImage = false;

                        System.Drawing.Image imageObject = null;

                        string htmlCode =
                            ComputerAccountHelper.GenerateHtmlImageCodeForCaliforniaBeemerByComputerAccount(vehicle);

                        imageObject = htmlToImageConverter.ConvertHtmlToImage(htmlCode, null)[0];

                        var stream = new MemoryStream();

                        var bottomStream = new MemoryStream();

                        imageObject.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);

                        _BottomImage.Save(bottomStream, System.Drawing.Imaging.ImageFormat.Jpeg);

                        var bytes = stream.ToArray();


                        var bottombytes = bottomStream.ToArray();

                        imageModel.BottomImage = bottombytes;

                        imageModel.TopImage = bytes;

                        stream.Dispose();

                        stream.Close();

                        bottomStream.Dispose();

                        bottomStream.Close();

                        imageObject.Dispose();

                        _BottomObj.Dispose();
                    }

                }



            }
            catch (Exception ex)
            {
                imageModel.BottomImage = null;
                imageModel.TopImage = null;
                return imageModel;

            }


            return imageModel;
        }
    }
}
