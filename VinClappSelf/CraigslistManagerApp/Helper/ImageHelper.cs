using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using CraigslistManagerApp.Model;
using HiQPdf;

namespace CraigslistManagerApp.Helper
{
    public class ImageHelper
    {
        public static ImageModel GenerateRunTimeImageBlobByComputerAccount(WhitmanEntepriseMasterVehicleInfo vehicle)
        {

            var imageModel = new ImageModel();

            try
            {
                WebsitesScreenshot.WebsitesScreenshot bottomObj = null;
                if (vehicle.DealerId != 17716 && vehicle.DealerId != 14853)
                {

                    var _Obj = new WebsitesScreenshot.WebsitesScreenshot(
                        ConfigurationManager.AppSettings["WebScreenShotSerialKey"].ToString
                            (CultureInfo.InvariantCulture));

                    bottomObj =
                        new WebsitesScreenshot.WebsitesScreenshot(
                            ConfigurationManager.AppSettings["WebScreenShotSerialKey"].ToString
                                (CultureInfo.InvariantCulture));

                 
                    WebsitesScreenshot.WebsitesScreenshot.Result result;

                    WebsitesScreenshot.WebsitesScreenshot.Result bottomResult;

                    if (vehicle.DealerId == 113738 || vehicle.DealerId == 3738)
                    {
                        var firstImageString = ComputerAccountHelper.GenerateHtmlImageCodeForAudiByComputerAccount(
                          vehicle);

                        result =
                            _Obj.CaptureHTML("<html><body>" + firstImageString + "</body></html>");

                        var secondImageString = ComputerAccountHelper.GenerateHtmlImageCodeForSecondBottomImage(
                            vehicle);

                        if (!String.IsNullOrEmpty(secondImageString))

                            bottomResult =
                                bottomObj.CaptureHTML("<html><body>" + secondImageString + "</body></html>");
                        else
                        {
                            bottomResult = bottomObj.CaptureHTML("<html><body>" + firstImageString + "</body></html>");
                        }
                    }

                    else
                    {
                        var firstImageString = ComputerAccountHelper.GenerateHtmlImageCode(
                            vehicle);


                        result =
                            _Obj.CaptureHTML("<html><body>" +
                                             firstImageString + "</body></html>");

                        var secondImageString = ComputerAccountHelper.GenerateHtmlImageCodeForSecondBottomImage(
                              vehicle);


                        if (!String.IsNullOrEmpty(secondImageString))

                            bottomResult =
                                bottomObj.CaptureHTML("<html><body>" +
                                                       secondImageString + "</body></html>");
                        else
                        {
                            bottomResult = bottomObj.CaptureHTML("<html><body>" + firstImageString + "</body></html>");
                        }
                    }



                    if (result == WebsitesScreenshot.WebsitesScreenshot.Result.Captured &&
                        bottomResult == WebsitesScreenshot.WebsitesScreenshot.Result.Captured)
                    {
                        _Obj.ImageFormat = WebsitesScreenshot.
                            WebsitesScreenshot.ImageFormats.JPG;

                        bottomObj.ImageFormat = WebsitesScreenshot.
                            WebsitesScreenshot.ImageFormats.JPG;

                        _Obj.DelaySeconds = 10;

                        bottomObj.DelaySeconds = 10;


                        var _Image = _Obj.GetImage();

                        var _BottomImage = bottomObj.GetImage();

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

                        bottomObj.Dispose();

                    }
                }
                else
                {
                    bottomObj =
                        new WebsitesScreenshot.WebsitesScreenshot(
                            System.Configuration.ConfigurationManager.AppSettings["WebScreenShotSerialKey"].ToString
                                (CultureInfo.InvariantCulture));

                    WebsitesScreenshot.WebsitesScreenshot.Result _BottomResult;

                    _BottomResult =
                        bottomObj.CaptureHTML("<html><body>" +
                                               ComputerAccountHelper.GenerateHtmlImageCodeForSecondBottomImage(
                                                   vehicle) + "</body></html>");

                    if (_BottomResult == WebsitesScreenshot.WebsitesScreenshot.Result.Captured)
                    {
                        bottomObj.ImageFormat = WebsitesScreenshot.
                            WebsitesScreenshot.ImageFormats.JPG;

                        bottomObj.DelaySeconds = 5;


                        var bottomImage = bottomObj.GetImage();

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

                        bottomImage.Save(bottomStream, System.Drawing.Imaging.ImageFormat.Jpeg);

                        var bytes = stream.ToArray();


                        var bottombytes = bottomStream.ToArray();

                        imageModel.BottomImage = bottombytes;

                        imageModel.TopImage = bytes;

                        stream.Dispose();

                        stream.Close();

                        bottomStream.Dispose();

                        bottomStream.Close();

                        imageObject.Dispose();

                        bottomObj.Dispose();
                    }

                }
            }
            catch (Exception ex)
            {
                //string body = "Error = " + ex.Message + ex.Source + ex.InnerException + ex.TargetSite + ex.StackTrace +
                //             "**************************************************************************************" +
                //             _subMasterVehicleList.ElementAt(0).ListingId + "----" +
                //             _subMasterVehicleList.ElementAt(0).DealerId;

                //if (_subMasterVehicleList.Any())
                //    _subMasterVehicleList.RemoveAt(0);

                //timerPostAccount.Enabled = true;

                //return imageModel;

            }


            return imageModel;
        }

        public static ImageModel GenerateRunTimePhysicalImageByComputerAccount(WhitmanEntepriseMasterVehicleInfo vehicle, List<WhitmanEntepriseMasterVehicleInfo> squareRandom )
        {

            var imageModel = new ImageModel {PhysicalImageUrl = new List<string>()};

            var random = new Random();


            string[] carImage = vehicle.CarImageUrl.Split(new string[] {",", "|"},
                                                          StringSplitOptions.RemoveEmptyEntries);

            int number = random.Next(4,6);

            int count = 1;
         
            var physicalImagePath = @"C:\ImageWarehouse" + "\\" + vehicle.DealerId + "\\" + vehicle.Vin;

            try
            {
                var dirNormal = new DirectoryInfo(physicalImagePath);

                if (!dirNormal.Exists)
                    dirNormal.Create();


                var htmlToImageConverter = new HtmlToImage
                    {
                        SerialNumber = ConfigurationManager.AppSettings["PDFSerialNumber"],
                        BrowserWidth = 570,
                        HtmlLoadedTimeout = 15,
                        TransparentImage = false
                    };


                string snapshotfilePath = dirNormal + "\\" + vehicle.StockNumber + "-" + (count++);

                //string square4FilePath = dirNormal + "\\" + vehicle.StockNumber + "-" + (count++);

                var imageObjectSnapshot =
                    htmlToImageConverter.ConvertHtmlToImage(
                        ComputerAccountHelper.GenerateHtmlImageCodeSnapshotInfo(vehicle), null)[0];

                imageObjectSnapshot.Save(snapshotfilePath, System.Drawing.Imaging.ImageFormat.Jpeg);

                //if (squareRandom.Count >= 4)
                //{

                //    var imageObjectSquare =
                //        htmlToImageConverter.ConvertHtmlToImage(
                //            ComputerAccountHelper.GenerateHtmlImageCodeSquare4Pictures(squareRandom), null)[0];

                //    imageObjectSquare.Save(square4FilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                //}



                imageModel.PhysicalImageUrl.Add(snapshotfilePath);

                foreach (string tmp in carImage)
                {


                    string imageFileName = vehicle.StockNumber + "-" + count;

                    var imageObject =
                        htmlToImageConverter.ConvertHtmlToImage(
                            ComputerAccountHelper.GenerateHtmlImageCodeOverlay(vehicle, tmp), null)[0];

                    string filePath = dirNormal + "\\" + imageFileName + ".jpg";



                    imageObject.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);



                    imageModel.PhysicalImageUrl.Add(filePath);

                    count++;

                    break;

                    //if (imageModel.PhysicalImageUrl.Count > number)
                    //    break;

                }


                foreach (var tmp in squareRandom)
                {
                    if (!String.IsNullOrEmpty(tmp.CarImageUrl))
                    {
                        string[] totalImage = tmp.CarImageUrl.Split(new[] { "|", "," }, StringSplitOptions.RemoveEmptyEntries);

                        string imageFileName = vehicle.StockNumber + "-" + count;

                        var imageObject = htmlToImageConverter.ConvertHtmlToImage(ComputerAccountHelper.GenerateHtmlImageCodeOverlayForRandomSimilarCars(tmp, totalImage[0]), null)[0];

                        string filePath = dirNormal + "\\" + imageFileName + ".jpg";

                        imageObject.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);

                        imageModel.PhysicalImageUrl.Add(filePath);

                        count++;
                    }




                }


                //if (squareRandom.Count >= 4)


                //    imageModel.PhysicalImageUrl.Add(square4FilePath);


            }
            catch (Exception)
            {
            }
            return imageModel;
        }

        public static void GenerateRunTimePhysicalImageTesting()
        {

            const string physicalImagePath = @"C:\ImageWarehouse";

            try
            {
                var dirNormal = new DirectoryInfo(physicalImagePath);

                if (!dirNormal.Exists)
                    dirNormal.Create();


                var htmlToImageConverter = new HtmlToImage
                {
                    SerialNumber = ConfigurationManager.AppSettings["PDFSerialNumber"],
                    BrowserWidth = 570,
                    HtmlLoadedTimeout = 15,
                    TransparentImage = false
                };

                const string snapshotfilePath =physicalImagePath+ "/" +  "Test.jpg";

                var imageObjectSnapshot =
                    htmlToImageConverter.ConvertUrlToImage("file:///C:/ExportFeed/craigslist/index.html")[0];

                imageObjectSnapshot.Save(snapshotfilePath, System.Drawing.Imaging.ImageFormat.Jpeg);

                

            }
            catch (Exception)
            {
            }
         
        }

        public static ImageModel GenerateRunTimePhysicalImageForFullertonAutoSquare(WhitmanEntepriseMasterVehicleInfo vehicle, List<WhitmanEntepriseMasterVehicleInfo> squareRandom)
        {

            var imageModel = new ImageModel { PhysicalImageUrl = new List<string>() };

            var random = new Random();
            
            string[] carImage = vehicle.CarImageUrl.Split(new string[] { ",", "|" },
                                                          StringSplitOptions.RemoveEmptyEntries);

            var number = random.Next(4, 6);

            var count = 1;

            var physicalImagePath = @"C:\ImageWarehouse" + "\\" + vehicle.DealerId + "\\" + vehicle.Vin;

            try
            {
                var dirNormal = new DirectoryInfo(physicalImagePath);

                if (!dirNormal.Exists)
                    dirNormal.Create();


                var htmlToImageConverter = new HtmlToImage
                {
                    SerialNumber = ConfigurationManager.AppSettings["PDFSerialNumber"],
                    BrowserWidth = 570,
                    HtmlLoadedTimeout = 15,
                    TransparentImage = false
                };


                //string square4FilePath = dirNormal + "\\" + vehicle.StockNumber + "-" + (count++);

                //if (squareRandom.Count >= 4)
                //{

                //    var imageObjectSquare =
                //        htmlToImageConverter.ConvertHtmlToImage(ComputerAccountHelper.GenerateHtmlImageCodeSquare4Pictures(squareRandom), null)[0];

                //    imageObjectSquare.Save(square4FilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                //}

                foreach (string tmp in carImage)
                {
                    string imageFileName = vehicle.StockNumber + "-" + count;

                    var imageObject = htmlToImageConverter.ConvertHtmlToImage(ComputerAccountHelper.GenerateHtmlImageCodeOverlayForFullerton(vehicle, tmp), null)[0];

                    string filePath = dirNormal + "\\" + imageFileName + ".jpg";

                    imageObject.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);

                    imageModel.PhysicalImageUrl.Add(filePath);

                    count++;

                    break;

                    //if (imageModel.PhysicalImageUrl.Count > number)
                    //    break;

                }


                foreach (var tmp in squareRandom)
                {
                    if (!String.IsNullOrEmpty(tmp.CarImageUrl))
                    {
                        string[] totalImage = tmp.CarImageUrl.Split(new[] { "|", "," },StringSplitOptions.RemoveEmptyEntries);

                        string imageFileName = vehicle.StockNumber + "-" + count;

                        var imageObject = htmlToImageConverter.ConvertHtmlToImage(ComputerAccountHelper.GenerateHtmlImageCodeOverlayForFullerton(tmp, totalImage[0]), null)[0];

                        string filePath = dirNormal + "\\" + imageFileName + ".jpg";

                        imageObject.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);

                        imageModel.PhysicalImageUrl.Add(filePath);

                        count++;
                    }

                }

                //if (squareRandom.Count >= 4)
                //    imageModel.PhysicalImageUrl.Add(square4FilePath);


            }
            catch (Exception)
            {
            }
            return imageModel;
        }

        public static ImageModel GenerateRunTimePhysicalImageByComputerAccount(WhitmanEntepriseMasterVehicleInfo vehicle)
        {
            var imageModel = new ImageModel { PhysicalImageUrl = new List<string>() };

            var random = new Random();
            
            string[] carImage = vehicle.CarImageUrl.Split(new[] { ",", "|" },
                                                         StringSplitOptions.RemoveEmptyEntries);

            var request = new WebClient();

            var number = random.Next(2, 4);
            
            string physicalImagePath = @"C:\MyImages\" + vehicle.DealerId + "\\" + vehicle.Vin;

            try
            {
                var dirNormal = new DirectoryInfo(physicalImagePath);

                if (!dirNormal.Exists)
                    dirNormal.Create();


                //var htmlToImageConverter = new HtmlToImage
                //{
                //    SerialNumber = ConfigurationManager.AppSettings["PDFSerialNumber"],
                //    BrowserWidth = 570,
                //    TransparentImage = false
                //};



                //string snapshotfilePath = dirNormal + "\\" + vehicle.Vin + ".jpg";

                //System.Drawing.Image imageObjectSnapshot =    htmlToImageConverter.ConvertHtmlToImage(
                //      ComputerAccountHelper.GenerateHtmlImageCodeSnapshotInfoLayout1(vehicle), null)[0];

     

                //imageObjectSnapshot.Save(snapshotfilePath, ImageFormat.Jpeg);

                //imageModel.PhysicalImageUrl.Add(snapshotfilePath);

                for (int i = 0; i < carImage.Length; i++)
                {
                    string singleImagePath = dirNormal + "\\" + (i+1) + ".jpg";

                    ComputerAccountHelper.DownloadImage(request, carImage[i], singleImagePath);

                    imageModel.PhysicalImageUrl.Add(singleImagePath);

                    if (imageModel.PhysicalImageUrl.Count > number)
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
