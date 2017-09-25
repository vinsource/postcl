using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using VinCLAPP.Service.Data;

namespace VinCLAPP.Service.Handlers
{
    /// <summary>
    /// Summary description for VinClappImageHandler
    /// </summary>
    public class VinClappImageHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string function = context.Request.QueryString["Function"];
            switch (function)
            {
                case "GetImageUrl":
                    GetImageUrl(context);
                    break;
                case "GetImageUrlList":
                    GetImageUrlList(context);
                    break;
                case "SaveImageUrl":
                    SaveImageUrl(context);
                    break;
                default:
                    UploadImage(context);
                    break;
            }
        }

        private void GetImageUrlList(HttpContext context)
        {
            string listingId = context.Request.QueryString["ListingId"];
            int result;
            if (int.TryParse(listingId, out result))
            {
                var carImageUrLs = GetImageURLs(result);
                if (!String.IsNullOrEmpty(carImageUrLs.ImageURLs))
                {
                    var list = new List<ServerImage>();

                    var fullImageUrLs = CommonHelper.GetArrayFromString(carImageUrLs.ImageURLs);
                    var thumbnailImageUrLs = CommonHelper.GetArrayFromString(carImageUrLs.ThumnailURLs);
                    int count = fullImageUrLs.Length;
                    int countThumbnail = thumbnailImageUrLs.Length;
                    for (int i = 0; i < count; i++)
                    {
                        var image = new ServerImage
                        {
                            FileUrl = fullImageUrLs[i],
                            ThumbnailUrl = i >= countThumbnail ? String.Empty : thumbnailImageUrLs[i]
                        };
                        list.Add(image);
                    }

                    var js = new JavaScriptSerializer();
                    context.Response.Write(js.Serialize(list));
                }
                else
                {
                    context.Response.Write("0");
                }
            }
        }

        private void SaveImageUrl(HttpContext context)
        {
            if (context.Request.QueryString["InventoryStatus"] != null && context.Request.QueryString["ListingId"] != null)
            {
                int inventoryStatus = int.Parse(context.Request.QueryString["InventoryStatus"].ToString(CultureInfo.InvariantCulture));
                int listingId = int.Parse(context.Request.QueryString["ListingId"].ToString(CultureInfo.InvariantCulture));

                var reader = new StreamReader(context.Request.InputStream);
                string result = reader.ReadToEnd();
                var js = new JavaScriptSerializer();
                var carImage = js.Deserialize<SavedCarImage>(result);

                var image = new ImageViewModel
                {
                    InventoryStatus = inventoryStatus,
                    ListingId = listingId,
                    ImageUploadFiles = carImage.FileUrLs,
                    ThumbnailImageUploadFiles = carImage.ThumbnailUrLs
                };

                ReplaceCarImageURL(image);
                context.Response.Write("Successful");
            }
        }

        private void GetImageUrl(HttpContext context)
        {
            var fileName = context.Request.QueryString["uploadedfile"];
            var dealerId = context.Request.QueryString["DealerId"];
            var vin = context.Request.QueryString["Vin"];

            var image = new ServerImage
            {
                FileUrl = string.Format("{0}DealerImages/{1}/{2}/NormalSizeImages/{3}", GetWebAppRoot(), dealerId, vin, fileName),
                ThumbnailUrl = string.Format("{0}DealerImages/{1}/{2}/ThumbnailSizeImages/{3}", GetWebAppRoot(), dealerId, vin, fileName)
            };
            var js = new JavaScriptSerializer();
            context.Response.Write(js.Serialize(image));
        }

        private void UploadImage(HttpContext context)
        {
            string dealerId = context.Request.QueryString["DealerId"];
            string vin = context.Request.QueryString["Vin"];
            int overlay = Convert.ToInt32(context.Request.QueryString["Overlay"]);

            string fileName = context.Request.QueryString["uploadedfile"];

            string imageFileName = fileName;
            //string imageFileName = ListingId + "-" + FileName;

            ImageDirectories imageDirectories = CreateFolder(dealerId, vin);
            string resultFileName = imageDirectories.FullSizeDirectory.FullName + "/" + imageFileName;
            using (FileStream fileStream = File.Create(resultFileName))
            {
                var bytes = new byte[4096]; //100MB max
                int totalBytesRead;
                while ((totalBytesRead = context.Request.InputStream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    fileStream.Write(bytes, 0, totalBytesRead);
                }
            }

            var image = ProcessImage(dealerId, vin, imageFileName, imageDirectories.FullSizeDirectory, imageDirectories.ThumbnailDirectory);

            var js = new JavaScriptSerializer();
            context.Response.Write(js.Serialize(image));
        }

        private ServerImage ProcessImage(string dealerId, string vin, string imageFileName, DirectoryInfo dirNormal, DirectoryInfo dirThumbnail)
        {
            //    if (overlay == 1)
            //    {
            //        ImageHelper.OverlayImage(
            //            GetWebAppRoot() + "DealerImages/" + dealerId + "/" + vin + "/NormalSizeImages/" +
            //            imageFileName, dirNormal.FullName + "/" + imageFileName, dealerId);
            //    }

            ImageResizer.ImageBuilder.Current.Build(dirNormal.FullName + "/" + imageFileName, dirThumbnail.FullName + "/" + imageFileName, new ImageResizer.ResizeSettings("maxwidth=260&maxheight=260&format=jpg"));

            return new ServerImage
            {
                FileUrl = GetWebAppRoot() + "DealerImages/" + dealerId + "/" + vin + "/NormalSizeImages/" + imageFileName,
                ThumbnailUrl = GetWebAppRoot() + "DealerImages/" + dealerId + "/" + vin + "/ThumbnailSizeImages/" + imageFileName
            };
        }

        public string GetWebAppRoot()
        {
            return String.Format("{0}://{1}:{2}/", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.Port);
            //return String.Format("{0}://{1}/", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Host);
        }

        private static ImageDirectories CreateFolder(string dealerId, string vin)
        {
            var physicalImagePath = HttpContext.Current.Server.MapPath("/DealerImages") + "/" + dealerId + "/" + vin + "/NormalSizeImages";

            var physicalImageThumbNailPath = HttpContext.Current.Server.MapPath("/DealerImages") + "/" + dealerId + "/" + vin + "/ThumbnailSizeImages";

            var dirNormal = new DirectoryInfo(physicalImagePath);

            var dirThumbnail = new DirectoryInfo(physicalImageThumbNailPath);

            if (!dirNormal.Exists)
                dirNormal.Create();

            if (!dirThumbnail.Exists)
                dirThumbnail.Create();

            return new ImageDirectories { FullSizeDirectory = dirNormal, ThumbnailDirectory = dirThumbnail };
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public static CarImage GetImageURLs(int listingId)
        {
            var context = new vinclappEntities();
            var row =
                context.vinclappinventories.FirstOrDefault(x => x.ListingID == listingId);
            if (row == null)
            {
                return null;
            }
            else
            {
                return new CarImage()
                {
                    ImageURLs = row.CarImageUrl,
                    ThumnailURLs = row.ThumbnailImageURL
                };
            }

        }


        public static void ReplaceCarImageURL(ImageViewModel image)
        {
            if (image.InventoryStatus == 1)
            {
                //if (image.ImageUploadFiles.Contains(":80"))
                //{
                //    image.ImageUploadFiles = image.ImageUploadFiles.Replace(":8082", "");
                //    image.ImageUploadFiles = image.ImageUploadFiles.Replace(":80", "");
                //    image.ThumbnailImageUploadFiles = image.ThumbnailImageUploadFiles.Replace(":8082", "");
                //    image.ThumbnailImageUploadFiles = image.ThumbnailImageUploadFiles.Replace(":80", "");
                //}
                UpdateCarImageURL(image.ListingId, image.ImageUploadFiles, image.ThumbnailImageUploadFiles);
            }
            else if (image.InventoryStatus == -1)
            {
                //if (image.ImageUploadFiles.Contains(":80"))
                //{
                //    image.ImageUploadFiles = image.ImageUploadFiles.Replace(":8082", "");
                //    image.ImageUploadFiles = image.ImageUploadFiles.Replace(":80", "");
                //    image.ThumbnailImageUploadFiles = image.ThumbnailImageUploadFiles.Replace(":8082", "");
                //    image.ThumbnailImageUploadFiles = image.ThumbnailImageUploadFiles.Replace(":80", "");

                //}
                UpdateCarImageSoldURL(image.ListingId, image.ImageUploadFiles, image.ThumbnailImageUploadFiles);

            }
        }

        public static void UpdateCarImageURL(int ListingId, string CarImageUrl, string ThumbnailImageUrl)
        {
            using (var context = new vinclappEntities())
            {

                var searchResult = context.vinclappinventories.FirstOrDefault(x => x.ListingID == ListingId);

                searchResult.CarImageUrl = CarImageUrl;

                searchResult.ThumbnailImageURL = ThumbnailImageUrl;

                searchResult.LastUpdated = DateTime.Now;

                context.SaveChanges();
            }

        }

        public static void UpdateCarImageSoldURL(int ListingId, string CarImageUrl, string ThumbnailImageUrl)
        {
            using (var context = new vinclappEntities())
            {

                var searchResult = context.vinclappinventories.FirstOrDefault(x => x.ListingID == ListingId);

                searchResult.CarImageUrl = CarImageUrl;

                searchResult.ThumbnailImageURL = ThumbnailImageUrl;

                searchResult.LastUpdated = DateTime.Now;

                context.SaveChanges();
            }

        }
    }
}