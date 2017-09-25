using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using CraigslistManagerApp.DatabaseModel;
using CraigslistManagerApp.Helper;
using CraigslistManagerApp.Model;
using System.Linq;

namespace CraigslistManagerApp
{
    public sealed class SQLHelper
    {
        public static List<ComputerAccount> GetComputerList()
        {
            var context = new whitmanenterprisecraigslistEntities();

            if(context.vinclappdealerschedules.Any())
  
            return context.vinclapppcschedules.ToList().Select(tmp => new ComputerAccount()
                {
                   ComputerId = tmp.PC.GetValueOrDefault()
                }).ToList();

            return  new List<ComputerAccount>();
        }

        public static void InitializeGlobalCurrentComputer(int computerId)
        {
            var context = new whitmanenterprisecraigslistEntities();

            clsVariables.currentComputer=new WhitmanEnterpriseComputer {CityList = new List<WhitmanEnterpriseCity>()};
            
            clsVariables.computeremailaccountList = new List<CraigsListEmailAccount>();

            var cityListPerPcId = from b in context.whitmanenterprisecraigslistcities
                                    select new
                                  {

                                      b.CityID,
                                      b.CityName,
                                      b.CraigsListCityURL,
                                      b.SubCity,
                                      b.CLIndex,
                                      

                                  };

            var emailListPerPcId = context.vinclappemailaccounts.Where(x => x.Computer == computerId).Where(x=>x.Active==true);

            foreach (var tmp in cityListPerPcId)
            {
                clsVariables.currentComputer.CityList.Add(new WhitmanEnterpriseCity()
                {
                    CityID = tmp.CityID,
                    CityName = tmp.CityName,
                    CraigsListCityURL = tmp.CraigsListCityURL,
                    SubCity = tmp.SubCity.GetValueOrDefault(),
                    CLIndex = tmp.CLIndex.GetValueOrDefault()


                });
            }

            int position = 1;

            foreach (var tmp in emailListPerPcId)
            {
                if (!String.IsNullOrEmpty(tmp.AccountName) &&  !String.IsNullOrEmpty(tmp.AccountPassword))
                {
                    var clAccount = new CraigsListEmailAccount()
                    {
                        CraigslistAccount = tmp.AccountName,
                        CraigsListPassword = tmp.AccountPassword,
                        CraigsAccountPhoneNumber = tmp.Phone,
                        Proxy = tmp.ProxyIP,
                        isCurrentlyUsed = false,
                        IntervalofAds = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["IntervalOfAds"].ToString(CultureInfo.InvariantCulture)),
                        Position = position++,
                        DefaultGateWay=tmp.DefaultGateway,
                        Dns = tmp.Dns

                    };

                    clsVariables.computeremailaccountList.Add(clAccount);
                }
            }

            if(clsVariables.computeremailaccountList.Any())
                clsVariables.computeremailaccountList.First().isCurrentlyUsed = true;

        }

        public static void InitializeGlobalComputerAccountInfo(int computerId)
        {
            var context = new whitmanenterprisecraigslistEntities()
                {
                    CommandTimeout = 18000
                };

            var bufferMasterVehicleList
                = new List<WhitmanEntepriseMasterVehicleInfo>();
            
            clsVariables.currentMasterVehicleList = new List<WhitmanEntepriseMasterVehicleInfo>();

            clsVariables.fullMasterVehicleList = new List<WhitmanEntepriseMasterVehicleInfo>();

            clsVariables.currentRenewList = new List<RenewScheduleModel>();

            var hashPast = new HashSet<int>();

            var bufferListDatabase = new List<BufferVehicleModel>();

            var dealerListPerPcId = clsVariables.computerList.Where(x => x.ComputerId == computerId);

            var dtCompareToday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var dtCompare = DateTime.Now.AddHours(-55);

            var checkList = from a in context.vincontrolcraigslisttrackings
                            from b in context.vinclappemailaccounts
                            where
                                a.EmailAccount == b.AccountName && a.AddedDate <= dtCompare &&
                                 a.CLPostingId > 0
                            select new
                            {
                                a.TrackingId,
                                a.ListingId,
                                a.CityId,
                                a.CLPostingId,
                                a.ShowAd,
                                a.EmailAccount,
                                b.AccountPassword,
                                b.Phone,
                                a.AddedDate,
                                a.ExpirationDate,
                                a.HtmlCraigslistUrl
                            };
         

            if (clsVariables.Reload)
            {
                var todayPastList = from a in context.vincontrolcraigslisttrackings
                                    where a.AddedDate <= DateTime.Now && a.AddedDate > dtCompareToday && a.Computer == computerId
                                    select new
                                        {
                                            a.TrackingId,
                                            a.ListingId,
                                            a.CityId,
                                            a.CLPostingId,
                                            a.ShowAd,
                                            a.EmailAccount,
                                            a.AddedDate,
                                            a.ExpirationDate
                                        };

                foreach (var tmp in todayPastList)
                {
                    string uniqueID = tmp.ListingId + "" + tmp.CityId;

                    hashPast.Add(Convert.ToInt32(uniqueID));
                }
            }

            foreach (var tmp in checkList.OrderBy(x => x.AddedDate))
            {
                clsVariables.currentRenewList.Add(new RenewScheduleModel()
                    {
                        ListingId = tmp.ListingId.GetValueOrDefault(),
                        CityId = tmp.CityId.GetValueOrDefault(),
                        TrackingId = tmp.TrackingId,
                        CLPostingId = tmp.CLPostingId.GetValueOrDefault(),
                        CraigslistAccountName = tmp.EmailAccount,
                        CraigslistAccountPassword = tmp.AccountPassword,
                        CraigslistAccountPhone = tmp.Phone,
                        ExpirationDate = tmp.ExpirationDate.GetValueOrDefault(),
                        CraigslistURL = tmp.HtmlCraigslistUrl
                    });
            }

            foreach (var tmp in dealerListPerPcId)
            {
                var invertoryPerId = from a in context.whitmanenterprisecraigslistinventories
                                     from b in context.whitmanenterprisedealerlists
                                     where
                                         b.VincontrolId == tmp.DealerId && a.DealershipId == b.VincontrolId &&
                                         !String.IsNullOrEmpty(a.CarImageUrl)
                                     select new
                                         {
                                             a.ListingID,
                                             a.StockNumber,
                                             a.VINNumber,
                                             a.ModelYear,
                                             a.Make,
                                             a.Model,
                                             a.Trim,
                                             a.Cylinders,
                                             a.BodyType,
                                             a.SalePrice,
                                             a.ExteriorColor,
                                             a.InteriorColor,
                                             a.Mileage,
                                             a.Descriptions,
                                             a.CarImageUrl,
                                             a.Doors,
                                             a.FuelType,
                                             a.Liters,
                                             a.Tranmission,
                                             a.DriveTrain,
                                             a.EngineType,
                                             a.CarsOptions,
                                             a.DefaultImageUrl,
                                             a.OverideTitle,
                                             b.VincontrolId,
                                             b.DealershipName,
                                             b.PhoneNumber,
                                             b.StreetAddress,
                                             b.City,
                                             b.State,
                                             b.ZipCode,
                                             b.LogoURL,
                                             b.WebSiteURL,
                                             b.CreditURL,
                                             b.Email,
                                             b.CityOveride,
                                             b.EmailFormat,
                                             b.TradeInBannerLink



                                         };

                if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday || DateTime.Now.DayOfWeek == DayOfWeek.Thursday ||
                    DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                {
                    if (tmp.DealerId == 7765 || tmp.DealerId == 9843)
                    {

                        var myList = invertoryPerId.ToList();

                        foreach (var vehicle in myList)
                        {
                            int qualifiedImagesCount = 0;

                            string firstImageUrl = "";

                            if (!String.IsNullOrEmpty(vehicle.CarImageUrl))
                            {
                                string tmpString = vehicle.CarImageUrl.Replace(" ", "%20");

                                if (tmpString.Contains("|") || tmpString.Contains(","))
                                {

                                    string[] totalImage = tmpString.Split(new[] {"|", ","},
                                                                          StringSplitOptions.RemoveEmptyEntries);

                                    if (totalImage.Any())
                                    {

                                        firstImageUrl = totalImage.First();
                                        qualifiedImagesCount = totalImage.Count();
                                    }
                                }
                                else
                                {
                                    firstImageUrl = tmpString;
                                }
                            }
                            else
                            {
                                firstImageUrl = vehicle.DefaultImageUrl;
                            }

                            if (qualifiedImagesCount >= 1)
                            {

                                bufferListDatabase.Add(new BufferVehicleModel()
                                    {
                                        ListingID = vehicle.ListingID,
                                        StockNumber = vehicle.StockNumber,
                                        VinNumber = vehicle.VINNumber,
                                        VincontrolId = vehicle.VincontrolId.GetValueOrDefault(),
                                        BodyType = vehicle.BodyType,
                                        CarImageUrl = vehicle.CarImageUrl,
                                        CarsOptions = vehicle.CarsOptions,
                                        City = vehicle.City,
                                        CityOveride = vehicle.CityOveride,
                                        CreditUrl = vehicle.CreditURL,
                                        Cylinders = vehicle.Cylinders,
                                        DealershipName = vehicle.DealershipName,
                                        DefaultImageUrl = vehicle.DefaultImageUrl,
                                        Descriptions = vehicle.Descriptions,
                                        Doors = vehicle.Doors,
                                        DriveTrain = vehicle.DriveTrain,
                                        Email = vehicle.Email,
                                        EmailFormat = vehicle.EmailFormat.GetValueOrDefault(),
                                        EngineType = vehicle.EngineType,
                                        ExteriorColor = vehicle.ExteriorColor,
                                        FuelType = vehicle.FuelType,
                                        InteriorColor = vehicle.InteriorColor,
                                        Liters = vehicle.Liters,
                                        LogoUrl = vehicle.LogoURL,
                                        Make = vehicle.Make,
                                        Mileage = vehicle.Mileage,
                                        Model = vehicle.Model,
                                        ModelYear = vehicle.ModelYear,
                                        PhoneNumber = vehicle.PhoneNumber,
                                        SalePrice = vehicle.SalePrice,
                                        State = vehicle.State,
                                        StreetAddress = vehicle.StreetAddress,
                                        TradeInBannerLink = vehicle.TradeInBannerLink,
                                        Tranmission = vehicle.Tranmission,
                                        Trim = vehicle.Trim,
                                        WebSiteUrl = vehicle.WebSiteURL,
                                        ZipCode = vehicle.ZipCode,
                                        Price = tmp.Price,
                                        CityId = tmp.CityId,
                                        DealerId = tmp.DealerId,
                                        PostingCityId = tmp.CityId,
                                        FirstImageUrl = firstImageUrl,
                                        AddtionalTitle = vehicle.OverideTitle

                                    });

                            }
                        }
                    }
                    else
                    {
                        var myList = invertoryPerId.ToList().Split(2).First();

                        foreach (var vehicle in myList)
                        {
                            int qualifiedImagesCount = 0;

                            string firstImageUrl = "";

                            if (!String.IsNullOrEmpty(vehicle.CarImageUrl))
                            {
                                string tmpString = vehicle.CarImageUrl.Replace(" ", "%20");

                                if (tmpString.Contains("|") || tmpString.Contains(","))
                                {

                                    string[] totalImage = tmpString.Split(new[] { "|", "," },
                                                                          StringSplitOptions.RemoveEmptyEntries);

                                    if (totalImage.Any())
                                    {

                                        firstImageUrl = totalImage.First();
                                        qualifiedImagesCount = totalImage.Count();
                                    }
                                }
                                else
                                {
                                    firstImageUrl = tmpString;
                                }
                            }
                            else
                            {
                                firstImageUrl = vehicle.DefaultImageUrl;
                            }

                            if (qualifiedImagesCount >= 1)
                            {

                                bufferListDatabase.Add(new BufferVehicleModel()
                                {
                                    ListingID = vehicle.ListingID,
                                    StockNumber = vehicle.StockNumber,
                                    VinNumber = vehicle.VINNumber,
                                    VincontrolId = vehicle.VincontrolId.GetValueOrDefault(),
                                    BodyType = vehicle.BodyType,
                                    CarImageUrl = vehicle.CarImageUrl,
                                    CarsOptions = vehicle.CarsOptions,
                                    City = vehicle.City,
                                    CityOveride = vehicle.CityOveride,
                                    CreditUrl = vehicle.CreditURL,
                                    Cylinders = vehicle.Cylinders,
                                    DealershipName = vehicle.DealershipName,
                                    DefaultImageUrl = vehicle.DefaultImageUrl,
                                    Descriptions = vehicle.Descriptions,
                                    Doors = vehicle.Doors,
                                    DriveTrain = vehicle.DriveTrain,
                                    Email = vehicle.Email,
                                    EmailFormat = vehicle.EmailFormat.GetValueOrDefault(),
                                    EngineType = vehicle.EngineType,
                                    ExteriorColor = vehicle.ExteriorColor,
                                    FuelType = vehicle.FuelType,
                                    InteriorColor = vehicle.InteriorColor,
                                    Liters = vehicle.Liters,
                                    LogoUrl = vehicle.LogoURL,
                                    Make = vehicle.Make,
                                    Mileage = vehicle.Mileage,
                                    Model = vehicle.Model,
                                    ModelYear = vehicle.ModelYear,
                                    PhoneNumber = vehicle.PhoneNumber,
                                    SalePrice = vehicle.SalePrice,
                                    State = vehicle.State,
                                    StreetAddress = vehicle.StreetAddress,
                                    TradeInBannerLink = vehicle.TradeInBannerLink,
                                    Tranmission = vehicle.Tranmission,
                                    Trim = vehicle.Trim,
                                    WebSiteUrl = vehicle.WebSiteURL,
                                    ZipCode = vehicle.ZipCode,
                                    Price = tmp.Price,
                                    CityId = tmp.CityId,
                                    DealerId = tmp.DealerId,
                                    PostingCityId = tmp.CityId,
                                    FirstImageUrl = firstImageUrl,
                                    AddtionalTitle = vehicle.OverideTitle

                                });

                            }
                        }
                    }

                }
                else if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday || DateTime.Now.DayOfWeek == DayOfWeek.Friday ||
                         DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                {
                    if (tmp.DealerId == 7765 || tmp.DealerId == 9843)
                    {

                        var myList = invertoryPerId.ToList();

                        foreach (var vehicle in myList)
                        {
                            int qualifiedImagesCount = 0;

                            string firstImageUrl = "";

                            if (!String.IsNullOrEmpty(vehicle.CarImageUrl))
                            {
                                string tmpString = vehicle.CarImageUrl.Replace(" ", "%20");

                                if (tmpString.Contains("|") || tmpString.Contains(","))
                                {

                                    string[] totalImage = tmpString.Split(new[] { "|", "," },
                                                                          StringSplitOptions.RemoveEmptyEntries);

                                    if (totalImage.Any())
                                    {

                                        firstImageUrl = totalImage.First();
                                        qualifiedImagesCount = totalImage.Count();
                                    }
                                }
                                else
                                {
                                    firstImageUrl = tmpString;
                                }
                            }
                            else
                            {
                                firstImageUrl = vehicle.DefaultImageUrl;
                            }

                            if (qualifiedImagesCount >= 1)
                            {

                                bufferListDatabase.Add(new BufferVehicleModel()
                                {
                                    ListingID = vehicle.ListingID,
                                    StockNumber = vehicle.StockNumber,
                                    VinNumber = vehicle.VINNumber,
                                    VincontrolId = vehicle.VincontrolId.GetValueOrDefault(),
                                    BodyType = vehicle.BodyType,
                                    CarImageUrl = vehicle.CarImageUrl,
                                    CarsOptions = vehicle.CarsOptions,
                                    City = vehicle.City,
                                    CityOveride = vehicle.CityOveride,
                                    CreditUrl = vehicle.CreditURL,
                                    Cylinders = vehicle.Cylinders,
                                    DealershipName = vehicle.DealershipName,
                                    DefaultImageUrl = vehicle.DefaultImageUrl,
                                    Descriptions = vehicle.Descriptions,
                                    Doors = vehicle.Doors,
                                    DriveTrain = vehicle.DriveTrain,
                                    Email = vehicle.Email,
                                    EmailFormat = vehicle.EmailFormat.GetValueOrDefault(),
                                    EngineType = vehicle.EngineType,
                                    ExteriorColor = vehicle.ExteriorColor,
                                    FuelType = vehicle.FuelType,
                                    InteriorColor = vehicle.InteriorColor,
                                    Liters = vehicle.Liters,
                                    LogoUrl = vehicle.LogoURL,
                                    Make = vehicle.Make,
                                    Mileage = vehicle.Mileage,
                                    Model = vehicle.Model,
                                    ModelYear = vehicle.ModelYear,
                                    PhoneNumber = vehicle.PhoneNumber,
                                    SalePrice = vehicle.SalePrice,
                                    State = vehicle.State,
                                    StreetAddress = vehicle.StreetAddress,
                                    TradeInBannerLink = vehicle.TradeInBannerLink,
                                    Tranmission = vehicle.Tranmission,
                                    Trim = vehicle.Trim,
                                    WebSiteUrl = vehicle.WebSiteURL,
                                    ZipCode = vehicle.ZipCode,
                                    Price = tmp.Price,
                                    CityId = tmp.CityId,
                                    DealerId = tmp.DealerId,
                                    PostingCityId = tmp.CityId,
                                    FirstImageUrl = firstImageUrl,
                                    AddtionalTitle = vehicle.OverideTitle

                                });

                            }
                        }
                    }
                    else
                    {
                        var myList = invertoryPerId.ToList().Split(2).Last();

                        foreach (var vehicle in myList)
                        {
                            int qualifiedImagesCount = 0;

                            string firstImageUrl = "";

                            if (!String.IsNullOrEmpty(vehicle.CarImageUrl))
                            {
                                string tmpString = vehicle.CarImageUrl.Replace(" ", "%20");

                                if (tmpString.Contains("|") || tmpString.Contains(","))
                                {

                                    string[] totalImage = tmpString.Split(new[] { "|", "," },
                                                                          StringSplitOptions.RemoveEmptyEntries);

                                    if (totalImage.Any())
                                    {

                                        firstImageUrl = totalImage.First();
                                        qualifiedImagesCount = totalImage.Count();
                                    }
                                }
                                else
                                {
                                    firstImageUrl = tmpString;
                                }
                            }
                            else
                            {
                                firstImageUrl = vehicle.DefaultImageUrl;
                            }

                            if (qualifiedImagesCount >= 1)
                            {

                                bufferListDatabase.Add(new BufferVehicleModel()
                                {
                                    ListingID = vehicle.ListingID,
                                    StockNumber = vehicle.StockNumber,
                                    VinNumber = vehicle.VINNumber,
                                    VincontrolId = vehicle.VincontrolId.GetValueOrDefault(),
                                    BodyType = vehicle.BodyType,
                                    CarImageUrl = vehicle.CarImageUrl,
                                    CarsOptions = vehicle.CarsOptions,
                                    City = vehicle.City,
                                    CityOveride = vehicle.CityOveride,
                                    CreditUrl = vehicle.CreditURL,
                                    Cylinders = vehicle.Cylinders,
                                    DealershipName = vehicle.DealershipName,
                                    DefaultImageUrl = vehicle.DefaultImageUrl,
                                    Descriptions = vehicle.Descriptions,
                                    Doors = vehicle.Doors,
                                    DriveTrain = vehicle.DriveTrain,
                                    Email = vehicle.Email,
                                    EmailFormat = vehicle.EmailFormat.GetValueOrDefault(),
                                    EngineType = vehicle.EngineType,
                                    ExteriorColor = vehicle.ExteriorColor,
                                    FuelType = vehicle.FuelType,
                                    InteriorColor = vehicle.InteriorColor,
                                    Liters = vehicle.Liters,
                                    LogoUrl = vehicle.LogoURL,
                                    Make = vehicle.Make,
                                    Mileage = vehicle.Mileage,
                                    Model = vehicle.Model,
                                    ModelYear = vehicle.ModelYear,
                                    PhoneNumber = vehicle.PhoneNumber,
                                    SalePrice = vehicle.SalePrice,
                                    State = vehicle.State,
                                    StreetAddress = vehicle.StreetAddress,
                                    TradeInBannerLink = vehicle.TradeInBannerLink,
                                    Tranmission = vehicle.Tranmission,
                                    Trim = vehicle.Trim,
                                    WebSiteUrl = vehicle.WebSiteURL,
                                    ZipCode = vehicle.ZipCode,
                                    Price = tmp.Price,
                                    CityId = tmp.CityId,
                                    DealerId = tmp.DealerId,
                                    PostingCityId = tmp.CityId,
                                    FirstImageUrl = firstImageUrl,
                                    AddtionalTitle = vehicle.OverideTitle

                                });

                            }
                        }
                    }


                }
                else
                {


                    foreach (var vehicle in invertoryPerId.ToList())
                    {
                        int qualifiedImagesCount = 0;

                        string firstImageUrl = "";

                        if (!String.IsNullOrEmpty(vehicle.CarImageUrl))
                        {
                            string tmpString = vehicle.CarImageUrl.Replace(" ", "%20");

                            if (tmpString.Contains("|") || tmpString.Contains(","))
                            {

                                string[] totalImage = tmpString.Split(new[] {"|", ","},
                                                                      StringSplitOptions.RemoveEmptyEntries);


                                if (totalImage.Any())
                                {

                                    firstImageUrl = totalImage.First();
                                    qualifiedImagesCount = totalImage.Count();
                                }


                            }
                            else
                            {
                                firstImageUrl = tmpString;
                            }
                        }
                        else
                        {
                            firstImageUrl = vehicle.DefaultImageUrl;
                        }

                        if (qualifiedImagesCount >= 1)
                        {

                            bufferListDatabase.Add(new BufferVehicleModel()
                                {
                                    ListingID = vehicle.ListingID,
                                    StockNumber = vehicle.StockNumber,
                                    VinNumber = vehicle.VINNumber,
                                    VincontrolId = vehicle.VincontrolId.GetValueOrDefault(),
                                    BodyType = vehicle.BodyType,
                                    CarImageUrl = vehicle.CarImageUrl,
                                    CarsOptions = vehicle.CarsOptions,
                                    City = vehicle.City,
                                    CityOveride = vehicle.CityOveride,
                                    CreditUrl = vehicle.CreditURL,
                                    Cylinders = vehicle.Cylinders,
                                    DealershipName = vehicle.DealershipName,
                                    DefaultImageUrl = vehicle.DefaultImageUrl,
                                    Descriptions = vehicle.Descriptions,
                                    Doors = vehicle.Doors,
                                    DriveTrain = vehicle.DriveTrain,
                                    Email = vehicle.Email,
                                    EmailFormat = vehicle.EmailFormat.GetValueOrDefault(),
                                    EngineType = vehicle.EngineType,
                                    ExteriorColor = vehicle.ExteriorColor,
                                    FuelType = vehicle.FuelType,
                                    InteriorColor = vehicle.InteriorColor,
                                    Liters = vehicle.Liters,
                                    LogoUrl = vehicle.LogoURL,
                                    Make = vehicle.Make,
                                    Mileage = vehicle.Mileage,
                                    Model = vehicle.Model,
                                    ModelYear = vehicle.ModelYear,
                                    PhoneNumber = vehicle.PhoneNumber,
                                    SalePrice = vehicle.SalePrice,
                                    State = vehicle.State,
                                    StreetAddress = vehicle.StreetAddress,
                                    TradeInBannerLink = vehicle.TradeInBannerLink,
                                    Tranmission = vehicle.Tranmission,
                                    Trim = vehicle.Trim,
                                    WebSiteUrl = vehicle.WebSiteURL,
                                    ZipCode = vehicle.ZipCode,
                                    Price = tmp.Price,
                                    CityId = tmp.CityId,
                                    DealerId = tmp.DealerId,
                                    PostingCityId = tmp.CityId,
                                    FirstImageUrl = firstImageUrl,
                                    AddtionalTitle = vehicle.OverideTitle

                                });

                        }


                    }


                }



            }




            foreach (var vehicle in bufferListDatabase)
            {
                string uniqueID = vehicle.ListingID + "" + vehicle.CityId;

                int compareId = Convert.ToInt32(uniqueID);

                if (!hashPast.Contains(compareId))
                {
                    
                    var addCar = new WhitmanEntepriseMasterVehicleInfo()
                        {
                            ListingId = vehicle.ListingID,
                            StockNumber =
                                String.IsNullOrEmpty(vehicle.StockNumber)
                                    ? ""
                                    : vehicle.StockNumber,
                            Vin =
                                String.IsNullOrEmpty(vehicle.VinNumber)
                                    ? ""
                                    : vehicle.VinNumber,
                            ModelYear =
                                String.IsNullOrEmpty(vehicle.ModelYear)
                                    ? ""
                                    : vehicle.ModelYear,
                            Make =
                                String.IsNullOrEmpty(vehicle.Make)
                                    ? ""
                                    : vehicle.Make,
                            Model =
                                String.IsNullOrEmpty(vehicle.Model)
                                    ? ""
                                    : vehicle.Model,
                            Trim =
                                String.IsNullOrEmpty(vehicle.Trim)
                                    ? ""
                                    : vehicle.Trim,
                            Cylinder =
                                String.IsNullOrEmpty(vehicle.Cylinders)
                                    ? ""
                                    : vehicle.Cylinders,
                            BodyType =
                                String.IsNullOrEmpty(vehicle.BodyType)
                                    ? ""
                                    : vehicle.BodyType,
                            SalePrice =
                                String.IsNullOrEmpty(vehicle.SalePrice)
                                    ? ""
                                    : vehicle.SalePrice,
                            ExteriorColor =
                                String.IsNullOrEmpty(vehicle.ExteriorColor)
                                    ? ""
                                    : vehicle.ExteriorColor,
                            InteriorColor =
                                String.IsNullOrEmpty(vehicle.InteriorColor)
                                    ? ""
                                    : vehicle.InteriorColor,
                            Mileage =
                                String.IsNullOrEmpty(vehicle.Mileage)
                                    ? ""
                                    : vehicle.Mileage,
                            Description =
                                String.IsNullOrEmpty(vehicle.Descriptions)
                                    ? ""
                                    : vehicle.Descriptions,
                            CarImageUrl =
                                String.IsNullOrEmpty(vehicle.CarImageUrl)
                                    ? ""
                                    : vehicle.CarImageUrl.Replace("|", ","),
                            Door =
                                String.IsNullOrEmpty(vehicle.Doors)
                                    ? ""
                                    : vehicle.Doors,
                            Fuel =
                                String.IsNullOrEmpty(vehicle.FuelType)
                                    ? ""
                                    : vehicle.FuelType,
                            Litters =
                                String.IsNullOrEmpty(vehicle.Liters)
                                    ? ""
                                    : vehicle.Liters,
                            Tranmission =
                                String.IsNullOrEmpty(vehicle.Tranmission)
                                    ? ""
                                    : vehicle.Tranmission,
                            WheelDrive =
                                String.IsNullOrEmpty(vehicle.DriveTrain)
                                    ? ""
                                    : vehicle.DriveTrain,
                            Engine =
                                String.IsNullOrEmpty(vehicle.EngineType)
                                    ? ""
                                    : vehicle.EngineType,
                            Options =
                                String.IsNullOrEmpty(vehicle.CarsOptions)
                                    ? ""
                                    : vehicle.CarsOptions,
                            DefaultImageUrl =
                                String.IsNullOrEmpty(
                                    vehicle.DefaultImageUrl)
                                    ? ""
                                    : vehicle.DefaultImageUrl,
                            VincontrolId =
                                String.IsNullOrEmpty(
                                    vehicle.VincontrolId.ToString(CultureInfo.InvariantCulture))
                                    ? ""
                                    : vehicle.VincontrolId.ToString(CultureInfo.InvariantCulture),
                            DealershipName =
                                String.IsNullOrEmpty(
                                    vehicle.DealershipName)
                                    ? ""
                                    : vehicle.DealershipName,
                            PhoneNumber =
                                String.IsNullOrEmpty(vehicle.PhoneNumber)
                                    ? ""
                                    : vehicle.PhoneNumber,
                            StreetAddress =
                                String.IsNullOrEmpty(vehicle.StreetAddress)
                                    ? ""
                                    : vehicle.StreetAddress,
                            City =
                                String.IsNullOrEmpty(vehicle.City)
                                    ? ""
                                    : vehicle.City,
                            State =
                                String.IsNullOrEmpty(vehicle.State)
                                    ? ""
                                    : vehicle.State,
                            ZipCode =
                                String.IsNullOrEmpty(vehicle.ZipCode)
                                    ? ""
                                    : vehicle.ZipCode,
                            LogoUrl =
                                String.IsNullOrEmpty(vehicle.LogoUrl)
                                    ? ""
                                    : vehicle.LogoUrl,
                            WebSiteUrl =
                                String.IsNullOrEmpty(vehicle.WebSiteUrl)
                                    ? ""
                                    : vehicle.WebSiteUrl,
                            CreditUrl =
                                String.IsNullOrEmpty(vehicle.CreditUrl)
                                    ? ""
                                    : vehicle.CreditUrl,
                            Email =
                                String.IsNullOrEmpty(vehicle.Email)
                                    ? ""
                                    : vehicle.Email,
                            CityOveride =
                                String.IsNullOrEmpty(vehicle.CityOveride)
                                    ? ""
                                    : vehicle.CityOveride,
                            EmailFormat =
                                vehicle.EmailFormat,
                            AddtionalTitle =
                                String.IsNullOrEmpty(vehicle.AddtionalTitle)
                                    ? ""
                                    : vehicle.AddtionalTitle,

                            Price = vehicle.Price,

                            PostingCityId = vehicle.CityId,
                            DealerId =
                                vehicle.DealerId,
                            FirstImageUrl = vehicle.FirstImageUrl,

                            TradeInBannerLink = vehicle.TradeInBannerLink,
                            
                            CraigslistExist = false,
                            
                            CraigslistCityUrl =
                                clsVariables.currentComputer.CityList.First(x => x.CityID == vehicle.CityId)
                                            .CraigsListCityURL

                        };



                    if (
                        clsVariables.currentRenewList.Any(
                            x => x.ListingId == addCar.ListingId && x.CityId == addCar.PostingCityId))
                    {



                        var searchResult =
                            clsVariables.currentRenewList.First(
                                x => x.ListingId == addCar.ListingId && x.CityId == addCar.PostingCityId);
                        addCar.CraigslistUrl = searchResult.CraigslistURL;
                        addCar.CraigslistAccountName = searchResult.CraigslistAccountName;
                        addCar.CraigslistAccountPassword = searchResult.CraigslistAccountPassword;
                        addCar.CraigslistAccountPhone = searchResult.CraigslistAccountPhone;
                        addCar.CLPostingId = searchResult.CLPostingId;
                        addCar.AdTrackingId = searchResult.TrackingId;


                    }

                    bufferMasterVehicleList.Add(addCar);
                    clsVariables.fullMasterVehicleList.Add(addCar);


                }
                else
                {

                    var addCar = new WhitmanEntepriseMasterVehicleInfo()
                        {

                            ListingId = vehicle.ListingID,
                            StockNumber =
                                String.IsNullOrEmpty(vehicle.StockNumber)
                                    ? ""
                                    : vehicle.StockNumber,
                            Vin =
                                String.IsNullOrEmpty(vehicle.VinNumber)
                                    ? ""
                                    : vehicle.VinNumber,
                            ModelYear =
                                String.IsNullOrEmpty(vehicle.ModelYear)
                                    ? ""
                                    : vehicle.ModelYear,
                            Make =
                                String.IsNullOrEmpty(vehicle.Make)
                                    ? ""
                                    : vehicle.Make,
                            Model =
                                String.IsNullOrEmpty(vehicle.Model)
                                    ? ""
                                    : vehicle.Model,
                            Trim =
                                String.IsNullOrEmpty(vehicle.Trim)
                                    ? ""
                                    : vehicle.Trim,
                            Cylinder =
                                String.IsNullOrEmpty(vehicle.Cylinders)
                                    ? ""
                                    : vehicle.Cylinders,
                            BodyType =
                                String.IsNullOrEmpty(vehicle.BodyType)
                                    ? ""
                                    : vehicle.BodyType,
                            SalePrice =
                                String.IsNullOrEmpty(vehicle.SalePrice)
                                    ? ""
                                    : vehicle.SalePrice,
                            ExteriorColor =
                                String.IsNullOrEmpty(vehicle.ExteriorColor)
                                    ? ""
                                    : vehicle.ExteriorColor,
                            InteriorColor =
                                String.IsNullOrEmpty(vehicle.InteriorColor)
                                    ? ""
                                    : vehicle.InteriorColor,
                            Mileage =
                                String.IsNullOrEmpty(vehicle.Mileage)
                                    ? ""
                                    : vehicle.Mileage,
                            Description =
                                String.IsNullOrEmpty(vehicle.Descriptions)
                                    ? ""
                                    : vehicle.Descriptions,
                            CarImageUrl =
                                String.IsNullOrEmpty(vehicle.CarImageUrl)
                                    ? ""
                                    : vehicle.CarImageUrl.Replace("|", ","),
                            Door =
                                String.IsNullOrEmpty(vehicle.Doors)
                                    ? ""
                                    : vehicle.Doors,
                            Fuel =
                                String.IsNullOrEmpty(vehicle.FuelType)
                                    ? ""
                                    : vehicle.FuelType,
                            Litters =
                                String.IsNullOrEmpty(vehicle.Liters)
                                    ? ""
                                    : vehicle.Liters,
                            Tranmission =
                                String.IsNullOrEmpty(vehicle.Tranmission)
                                    ? ""
                                    : vehicle.Tranmission,
                            WheelDrive =
                                String.IsNullOrEmpty(vehicle.DriveTrain)
                                    ? ""
                                    : vehicle.DriveTrain,
                            Engine =
                                String.IsNullOrEmpty(vehicle.EngineType)
                                    ? ""
                                    : vehicle.EngineType,
                            Options =
                                String.IsNullOrEmpty(vehicle.CarsOptions)
                                    ? ""
                                    : vehicle.CarsOptions,
                            DefaultImageUrl =
                                String.IsNullOrEmpty(
                                    vehicle.DefaultImageUrl)
                                    ? ""
                                    : vehicle.DefaultImageUrl,
                            VincontrolId =
                                String.IsNullOrEmpty(
                                    vehicle.VincontrolId.ToString(CultureInfo.InvariantCulture))
                                    ? ""
                                    : vehicle.VincontrolId.ToString(CultureInfo.InvariantCulture),
                            DealershipName =
                                String.IsNullOrEmpty(
                                    vehicle.DealershipName)
                                    ? ""
                                    : vehicle.DealershipName,
                            PhoneNumber =
                                String.IsNullOrEmpty(vehicle.PhoneNumber)
                                    ? ""
                                    : vehicle.PhoneNumber,
                            StreetAddress =
                                String.IsNullOrEmpty(vehicle.StreetAddress)
                                    ? ""
                                    : vehicle.StreetAddress,
                            City =
                                String.IsNullOrEmpty(vehicle.City)
                                    ? ""
                                    : vehicle.City,
                            State =
                                String.IsNullOrEmpty(vehicle.State)
                                    ? ""
                                    : vehicle.State,
                            ZipCode =
                                String.IsNullOrEmpty(vehicle.ZipCode)
                                    ? ""
                                    : vehicle.ZipCode,
                            LogoUrl =
                                String.IsNullOrEmpty(vehicle.LogoUrl)
                                    ? ""
                                    : vehicle.LogoUrl,
                            WebSiteUrl =
                                String.IsNullOrEmpty(vehicle.WebSiteUrl)
                                    ? ""
                                    : vehicle.WebSiteUrl,
                            CreditUrl =
                                String.IsNullOrEmpty(vehicle.CreditUrl)
                                    ? ""
                                    : vehicle.CreditUrl,
                            Email =
                                String.IsNullOrEmpty(vehicle.Email)
                                    ? ""
                                    : vehicle.Email,
                            CityOveride =
                                String.IsNullOrEmpty(vehicle.CityOveride)
                                    ? ""
                                    : vehicle.CityOveride,
                            EmailFormat =
                                vehicle.EmailFormat,
                            AddtionalTitle =
                              String.IsNullOrEmpty(vehicle.AddtionalTitle)
                                  ? ""
                                  : vehicle.AddtionalTitle,

                            Price = vehicle.Price,

                            PostingCityId = vehicle.CityId,
                            DealerId =
                                vehicle.DealerId,
                            FirstImageUrl = vehicle.FirstImageUrl,

                            TradeInBannerLink = vehicle.TradeInBannerLink,
                            CraigslistExist = false,
                            CraigslistCityUrl =
                                clsVariables.currentComputer.CityList.First(x => x.CityID == vehicle.CityId)
                                            .CraigsListCityURL



                        };

                    clsVariables.fullMasterVehicleList.Add(addCar);
                }
            }



            foreach (
                var tmp in bufferMasterVehicleList.Where(x => x.CLPostingId > 0).OrderBy(x => x.CraigslistAccountName))
            {
                tmp.AutoID = clsVariables.currentMasterVehicleList.Count + 1;

                clsVariables.currentMasterVehicleList.Add(tmp);
            }

            if (clsVariables.ReloadRenew == false)
            {
                foreach (var tmp in bufferMasterVehicleList.Where(x => x.CLPostingId == 0))
                {
                    tmp.AutoID = clsVariables.currentMasterVehicleList.Count + 1;

                    clsVariables.currentMasterVehicleList.Add(tmp);
                }

            }



         

        }

        public static void InitializeGiantInventory(int computerId)
        {
            var context = new whitmanenterprisecraigslistEntities()
                {
                    CommandTimeout = 18000
                };
            var bufferMasterVehicleList
                = new List<WhitmanEntepriseMasterVehicleInfo>();

            clsVariables.currentMasterVehicleList = new List<WhitmanEntepriseMasterVehicleInfo>();

            clsVariables.fullMasterVehicleList = new List<WhitmanEntepriseMasterVehicleInfo>();

            clsVariables.currentRenewList = new List<RenewScheduleModel>();

            var matchPast = new List<BufferMatch>();

            var dtCompareToday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var bufferListDatabase = new List<BufferVehicleModel>();

            var splitPart = context.vinclapppcschedules.First(x => x.PC == computerId).Schedule.GetValueOrDefault() - 1;

            var todayPastList = from a in context.vincontrolcraigslisttrackings
                                where a.AddedDate <= DateTime.Now && a.AddedDate > dtCompareToday
                                select new
                                    {
                                        a.ListingId,
                                        a.CityId,
                                    };

            foreach (var tmp in todayPastList)
            {
                matchPast.Add(new BufferMatch()
                {
                    CityId = tmp.CityId.GetValueOrDefault(),
                    ListingID = tmp.ListingId.GetValueOrDefault()
                })
                ;
            }

        var finalInventory = from a in context.whitmanenterprisecraigslistinventories
                                 from b in context.whitmanenterprisedealerlists
                                 from c in context.vinclappdealerschedules
                                 where
                                     a.DealershipId == c.DealerId
                                     &&
                                     b.VincontrolId == c.DealerId
                                     &&
                                     !String.IsNullOrEmpty(a.CarImageUrl)
                                     &&
                                     b.Active == true && c.Split == false
                                 select new
                                     {
                                         a.ListingID,
                                         a.StockNumber,
                                         a.VINNumber,
                                         a.ModelYear,
                                         a.Make,
                                         a.Model,
                                         a.Trim,
                                         a.Cylinders,
                                         a.BodyType,
                                         a.SalePrice,
                                         a.ExteriorColor,
                                         a.InteriorColor,
                                         a.Mileage,
                                         a.Descriptions,
                                         a.CarImageUrl,
                                         a.Doors,
                                         a.FuelType,
                                         a.Liters,
                                         a.Tranmission,
                                         a.DriveTrain,
                                         a.EngineType,
                                         a.CarsOptions,
                                         a.DefaultImageUrl,
                                         a.OverideTitle,
                                         b.VincontrolId,
                                         b.DealershipName,
                                         b.PhoneNumber,
                                         b.StreetAddress,
                                         b.City,
                                         b.State,
                                         b.ZipCode,
                                         b.LogoURL,
                                         b.WebSiteURL,
                                         b.CreditURL,
                                         b.Email,
                                         b.CityOveride,
                                         b.EmailFormat,
                                         b.TradeInBannerLink,
                                         c.CityId,
                                         c.Price,
                                         c.Split,
                                         c.Schedules

                                     };

            var splitHalfInventory = from a in context.whitmanenterprisecraigslistinventories
                                     from b in context.whitmanenterprisedealerlists
                                     from c in context.vinclappdealerschedules
                                     where
                                         a.DealershipId == c.DealerId
                                         &&
                                         b.VincontrolId == c.DealerId
                                         &&
                                         !String.IsNullOrEmpty(a.CarImageUrl)
                                         &&
                                         b.Active == true && c.Split == true
                                     select new
                                         {
                                             a.ListingID,
                                             a.StockNumber,
                                             a.VINNumber,
                                             a.ModelYear,
                                             a.Make,
                                             a.Model,
                                             a.Trim,
                                             a.Cylinders,
                                             a.BodyType,
                                             a.SalePrice,
                                             a.ExteriorColor,
                                             a.InteriorColor,
                                             a.Mileage,
                                             a.Descriptions,
                                             a.CarImageUrl,
                                             a.Doors,
                                             a.FuelType,
                                             a.Liters,
                                             a.Tranmission,
                                             a.DriveTrain,
                                             a.EngineType,
                                             a.CarsOptions,
                                             a.DefaultImageUrl,
                                             a.OverideTitle,
                                             b.VincontrolId,
                                             b.DealershipName,
                                             b.PhoneNumber,
                                             b.StreetAddress,
                                             b.City,
                                             b.State,
                                             b.ZipCode,
                                             b.LogoURL,
                                             b.WebSiteURL,
                                             b.CreditURL,
                                             b.Email,
                                             b.CityOveride,
                                             b.EmailFormat,
                                             b.TradeInBannerLink,
                                             c.CityId,
                                             c.Price,
                                             c.Split,
                                             c.Schedules

                                         };

            var myList = finalInventory.ToList();

            if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday || DateTime.Now.DayOfWeek == DayOfWeek.Thursday ||
                DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                if (splitHalfInventory.Any())
                {

                    myList.AddRange(splitHalfInventory.Split(2).First());

                    foreach (var vehicle in myList)
                    {
                        int qualifiedImagesCount = 0;

                        string firstImageUrl = "";

                        if (!String.IsNullOrEmpty(vehicle.CarImageUrl))
                        {
                            string tmpString = vehicle.CarImageUrl.Replace(" ", "%20");

                            if (tmpString.Contains("|") || tmpString.Contains(","))
                            {

                                string[] totalImage = tmpString.Split(new[] {"|", ","},
                                                                      StringSplitOptions.RemoveEmptyEntries);

                                if (totalImage.Any())
                                {

                                    firstImageUrl = totalImage.First();
                                    qualifiedImagesCount = totalImage.Count();
                                }
                            }
                            else
                            {
                                firstImageUrl = tmpString;
                            }
                        }
                        else
                        {
                            firstImageUrl = vehicle.DefaultImageUrl;
                        }

                        if (qualifiedImagesCount >= 1)
                        {

                            bufferListDatabase.Add(new BufferVehicleModel()
                                {
                                    ListingID = vehicle.ListingID,
                                    StockNumber = vehicle.StockNumber,
                                    VinNumber = vehicle.VINNumber,
                                    VincontrolId = vehicle.VincontrolId.GetValueOrDefault(),
                                    BodyType = vehicle.BodyType,
                                    CarImageUrl = vehicle.CarImageUrl,
                                    CarsOptions = vehicle.CarsOptions,
                                    City = vehicle.City,
                                    CityOveride = vehicle.CityOveride,
                                    CreditUrl = vehicle.CreditURL,
                                    Cylinders = vehicle.Cylinders,
                                    DealershipName = vehicle.DealershipName,
                                    DefaultImageUrl = vehicle.DefaultImageUrl,
                                    Descriptions = vehicle.Descriptions,
                                    Doors = vehicle.Doors,
                                    DriveTrain = vehicle.DriveTrain,
                                    Email = vehicle.Email,
                                    EmailFormat = vehicle.EmailFormat.GetValueOrDefault(),
                                    EngineType = vehicle.EngineType,
                                    ExteriorColor = vehicle.ExteriorColor,
                                    FuelType = vehicle.FuelType,
                                    InteriorColor = vehicle.InteriorColor,
                                    Liters = vehicle.Liters,
                                    LogoUrl = vehicle.LogoURL,
                                    Make = vehicle.Make,
                                    Mileage = vehicle.Mileage,
                                    Model = vehicle.Model,
                                    ModelYear = vehicle.ModelYear,
                                    PhoneNumber = vehicle.PhoneNumber,
                                    SalePrice = vehicle.SalePrice,
                                    State = vehicle.State,
                                    StreetAddress = vehicle.StreetAddress,
                                    TradeInBannerLink = vehicle.TradeInBannerLink,
                                    Tranmission = vehicle.Tranmission,
                                    Trim = vehicle.Trim,
                                    WebSiteUrl = vehicle.WebSiteURL,
                                    ZipCode = vehicle.ZipCode,
                                    Price = vehicle.Price.GetValueOrDefault(),
                                    CityId = vehicle.CityId.GetValueOrDefault(),
                                    DealerId = vehicle.VincontrolId.GetValueOrDefault(),
                                    PostingCityId = vehicle.CityId.GetValueOrDefault(),
                                    FirstImageUrl = firstImageUrl,
                                    AddtionalTitle = vehicle.OverideTitle,
                                    Schedules = vehicle.Schedules.GetValueOrDefault()

                                });

                        }
                    }
                }
                else
                {
                    foreach (var vehicle in myList)
                    {
                        int qualifiedImagesCount = 0;

                        string firstImageUrl = "";

                        if (!String.IsNullOrEmpty(vehicle.CarImageUrl))
                        {
                            string tmpString = vehicle.CarImageUrl.Replace(" ", "%20");

                            if (tmpString.Contains("|") || tmpString.Contains(","))
                            {

                                string[] totalImage = tmpString.Split(new[] { "|", "," },
                                                                      StringSplitOptions.RemoveEmptyEntries);

                                if (totalImage.Any())
                                {

                                    firstImageUrl = totalImage.First();
                                    qualifiedImagesCount = totalImage.Count();
                                }
                            }
                            else
                            {
                                firstImageUrl = tmpString;
                            }
                        }
                        else
                        {
                            firstImageUrl = vehicle.DefaultImageUrl;
                        }

                        if (qualifiedImagesCount >= 1)
                        {

                            bufferListDatabase.Add(new BufferVehicleModel()
                            {
                                ListingID = vehicle.ListingID,
                                StockNumber = vehicle.StockNumber,
                                VinNumber = vehicle.VINNumber,
                                VincontrolId = vehicle.VincontrolId.GetValueOrDefault(),
                                BodyType = vehicle.BodyType,
                                CarImageUrl = vehicle.CarImageUrl,
                                CarsOptions = vehicle.CarsOptions,
                                City = vehicle.City,
                                CityOveride = vehicle.CityOveride,
                                CreditUrl = vehicle.CreditURL,
                                Cylinders = vehicle.Cylinders,
                                DealershipName = vehicle.DealershipName,
                                DefaultImageUrl = vehicle.DefaultImageUrl,
                                Descriptions = vehicle.Descriptions,
                                Doors = vehicle.Doors,
                                DriveTrain = vehicle.DriveTrain,
                                Email = vehicle.Email,
                                EmailFormat = vehicle.EmailFormat.GetValueOrDefault(),
                                EngineType = vehicle.EngineType,
                                ExteriorColor = vehicle.ExteriorColor,
                                FuelType = vehicle.FuelType,
                                InteriorColor = vehicle.InteriorColor,
                                Liters = vehicle.Liters,
                                LogoUrl = vehicle.LogoURL,
                                Make = vehicle.Make,
                                Mileage = vehicle.Mileage,
                                Model = vehicle.Model,
                                ModelYear = vehicle.ModelYear,
                                PhoneNumber = vehicle.PhoneNumber,
                                SalePrice = vehicle.SalePrice,
                                State = vehicle.State,
                                StreetAddress = vehicle.StreetAddress,
                                TradeInBannerLink = vehicle.TradeInBannerLink,
                                Tranmission = vehicle.Tranmission,
                                Trim = vehicle.Trim,
                                WebSiteUrl = vehicle.WebSiteURL,
                                ZipCode = vehicle.ZipCode,
                                Price = vehicle.Price.GetValueOrDefault(),
                                CityId = vehicle.CityId.GetValueOrDefault(),
                                DealerId = vehicle.VincontrolId.GetValueOrDefault(),
                                PostingCityId = vehicle.CityId.GetValueOrDefault(),
                                FirstImageUrl = firstImageUrl,
                                AddtionalTitle = vehicle.OverideTitle,
                                Schedules = vehicle.Schedules.GetValueOrDefault()

                            });

                        }
                    }
                }

            }
            else if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday || DateTime.Now.DayOfWeek == DayOfWeek.Friday ||
                     DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {

                if (splitHalfInventory.Any())
                {
                    myList.AddRange(splitHalfInventory.Split(2).Last());

                    foreach (var vehicle in myList)
                    {
                        int qualifiedImagesCount = 0;

                        string firstImageUrl = "";

                        if (!String.IsNullOrEmpty(vehicle.CarImageUrl))
                        {
                            string tmpString = vehicle.CarImageUrl.Replace(" ", "%20");

                            if (tmpString.Contains("|") || tmpString.Contains(","))
                            {

                                string[] totalImage = tmpString.Split(new[] {"|", ","},
                                                                      StringSplitOptions.RemoveEmptyEntries);

                                if (totalImage.Any())
                                {

                                    firstImageUrl = totalImage.First();
                                    qualifiedImagesCount = totalImage.Count();
                                }
                            }
                            else
                            {
                                firstImageUrl = tmpString;
                            }
                        }
                        else
                        {
                            firstImageUrl = vehicle.DefaultImageUrl;
                        }

                        if (qualifiedImagesCount >= 1)
                        {

                            bufferListDatabase.Add(new BufferVehicleModel()
                                {
                                    ListingID = vehicle.ListingID,
                                    StockNumber = vehicle.StockNumber,
                                    VinNumber = vehicle.VINNumber,
                                    VincontrolId = vehicle.VincontrolId.GetValueOrDefault(),
                                    BodyType = vehicle.BodyType,
                                    CarImageUrl = vehicle.CarImageUrl,
                                    CarsOptions = vehicle.CarsOptions,
                                    City = vehicle.City,
                                    CityOveride = vehicle.CityOveride,
                                    CreditUrl = vehicle.CreditURL,
                                    Cylinders = vehicle.Cylinders,
                                    DealershipName = vehicle.DealershipName,
                                    DefaultImageUrl = vehicle.DefaultImageUrl,
                                    Descriptions = vehicle.Descriptions,
                                    Doors = vehicle.Doors,
                                    DriveTrain = vehicle.DriveTrain,
                                    Email = vehicle.Email,
                                    EmailFormat = vehicle.EmailFormat.GetValueOrDefault(),
                                    EngineType = vehicle.EngineType,
                                    ExteriorColor = vehicle.ExteriorColor,
                                    FuelType = vehicle.FuelType,
                                    InteriorColor = vehicle.InteriorColor,
                                    Liters = vehicle.Liters,
                                    LogoUrl = vehicle.LogoURL,
                                    Make = vehicle.Make,
                                    Mileage = vehicle.Mileage,
                                    Model = vehicle.Model,
                                    ModelYear = vehicle.ModelYear,
                                    PhoneNumber = vehicle.PhoneNumber,
                                    SalePrice = vehicle.SalePrice,
                                    State = vehicle.State,
                                    StreetAddress = vehicle.StreetAddress,
                                    TradeInBannerLink = vehicle.TradeInBannerLink,
                                    Tranmission = vehicle.Tranmission,
                                    Trim = vehicle.Trim,
                                    WebSiteUrl = vehicle.WebSiteURL,
                                    ZipCode = vehicle.ZipCode,
                                    Price = vehicle.Price.GetValueOrDefault(),
                                    CityId = vehicle.CityId.GetValueOrDefault(),
                                    DealerId = vehicle.VincontrolId.GetValueOrDefault(),
                                    PostingCityId = vehicle.CityId.GetValueOrDefault(),
                                    FirstImageUrl = firstImageUrl,
                                    AddtionalTitle = vehicle.OverideTitle,
                                    Schedules = vehicle.Schedules.GetValueOrDefault()

                                });

                        }
                    }
                }
                else
                {
                    foreach (var vehicle in myList)
                    {
                        int qualifiedImagesCount = 0;

                        string firstImageUrl = "";

                        if (!String.IsNullOrEmpty(vehicle.CarImageUrl))
                        {
                            string tmpString = vehicle.CarImageUrl.Replace(" ", "%20");

                            if (tmpString.Contains("|") || tmpString.Contains(","))
                            {

                                string[] totalImage = tmpString.Split(new[] { "|", "," },
                                                                      StringSplitOptions.RemoveEmptyEntries);

                                if (totalImage.Any())
                                {

                                    firstImageUrl = totalImage.First();
                                    qualifiedImagesCount = totalImage.Count();
                                }
                            }
                            else
                            {
                                firstImageUrl = tmpString;
                            }
                        }
                        else
                        {
                            firstImageUrl = vehicle.DefaultImageUrl;
                        }

                        if (qualifiedImagesCount >= 1)
                        {

                            bufferListDatabase.Add(new BufferVehicleModel()
                            {
                                ListingID = vehicle.ListingID,
                                StockNumber = vehicle.StockNumber,
                                VinNumber = vehicle.VINNumber,
                                VincontrolId = vehicle.VincontrolId.GetValueOrDefault(),
                                BodyType = vehicle.BodyType,
                                CarImageUrl = vehicle.CarImageUrl,
                                CarsOptions = vehicle.CarsOptions,
                                City = vehicle.City,
                                CityOveride = vehicle.CityOveride,
                                CreditUrl = vehicle.CreditURL,
                                Cylinders = vehicle.Cylinders,
                                DealershipName = vehicle.DealershipName,
                                DefaultImageUrl = vehicle.DefaultImageUrl,
                                Descriptions = vehicle.Descriptions,
                                Doors = vehicle.Doors,
                                DriveTrain = vehicle.DriveTrain,
                                Email = vehicle.Email,
                                EmailFormat = vehicle.EmailFormat.GetValueOrDefault(),
                                EngineType = vehicle.EngineType,
                                ExteriorColor = vehicle.ExteriorColor,
                                FuelType = vehicle.FuelType,
                                InteriorColor = vehicle.InteriorColor,
                                Liters = vehicle.Liters,
                                LogoUrl = vehicle.LogoURL,
                                Make = vehicle.Make,
                                Mileage = vehicle.Mileage,
                                Model = vehicle.Model,
                                ModelYear = vehicle.ModelYear,
                                PhoneNumber = vehicle.PhoneNumber,
                                SalePrice = vehicle.SalePrice,
                                State = vehicle.State,
                                StreetAddress = vehicle.StreetAddress,
                                TradeInBannerLink = vehicle.TradeInBannerLink,
                                Tranmission = vehicle.Tranmission,
                                Trim = vehicle.Trim,
                                WebSiteUrl = vehicle.WebSiteURL,
                                ZipCode = vehicle.ZipCode,
                                Price = vehicle.Price.GetValueOrDefault(),
                                CityId = vehicle.CityId.GetValueOrDefault(),
                                DealerId = vehicle.VincontrolId.GetValueOrDefault(),
                                PostingCityId = vehicle.CityId.GetValueOrDefault(),
                                FirstImageUrl = firstImageUrl,
                                AddtionalTitle = vehicle.OverideTitle,
                                Schedules = vehicle.Schedules.GetValueOrDefault()

                            });

                        }
                    }
                }
            }
            else
            {
                if (splitHalfInventory.Any())
                {

                    myList.AddRange(splitHalfInventory);

                    foreach (var vehicle in myList)
                    {
                        int qualifiedImagesCount = 0;

                        string firstImageUrl = "";

                        if (!String.IsNullOrEmpty(vehicle.CarImageUrl))
                        {
                            string tmpString = vehicle.CarImageUrl.Replace(" ", "%20");

                            if (tmpString.Contains("|") || tmpString.Contains(","))
                            {

                                string[] totalImage = tmpString.Split(new[] {"|", ","},
                                                                      StringSplitOptions.RemoveEmptyEntries);

                                if (totalImage.Any())
                                {

                                    firstImageUrl = totalImage.First();
                                    qualifiedImagesCount = totalImage.Count();
                                }
                            }
                            else
                            {
                                firstImageUrl = tmpString;
                            }
                        }
                        else
                        {
                            firstImageUrl = vehicle.DefaultImageUrl;
                        }

                        if (qualifiedImagesCount >= 1)
                        {

                            bufferListDatabase.Add(new BufferVehicleModel()
                                {
                                    ListingID = vehicle.ListingID,
                                    StockNumber = vehicle.StockNumber,
                                    VinNumber = vehicle.VINNumber,
                                    VincontrolId = vehicle.VincontrolId.GetValueOrDefault(),
                                    BodyType = vehicle.BodyType,
                                    CarImageUrl = vehicle.CarImageUrl,
                                    CarsOptions = vehicle.CarsOptions,
                                    City = vehicle.City,
                                    CityOveride = vehicle.CityOveride,
                                    CreditUrl = vehicle.CreditURL,
                                    Cylinders = vehicle.Cylinders,
                                    DealershipName = vehicle.DealershipName,
                                    DefaultImageUrl = vehicle.DefaultImageUrl,
                                    Descriptions = vehicle.Descriptions,
                                    Doors = vehicle.Doors,
                                    DriveTrain = vehicle.DriveTrain,
                                    Email = vehicle.Email,
                                    EmailFormat = vehicle.EmailFormat.GetValueOrDefault(),
                                    EngineType = vehicle.EngineType,
                                    ExteriorColor = vehicle.ExteriorColor,
                                    FuelType = vehicle.FuelType,
                                    InteriorColor = vehicle.InteriorColor,
                                    Liters = vehicle.Liters,
                                    LogoUrl = vehicle.LogoURL,
                                    Make = vehicle.Make,
                                    Mileage = vehicle.Mileage,
                                    Model = vehicle.Model,
                                    ModelYear = vehicle.ModelYear,
                                    PhoneNumber = vehicle.PhoneNumber,
                                    SalePrice = vehicle.SalePrice,
                                    State = vehicle.State,
                                    StreetAddress = vehicle.StreetAddress,
                                    TradeInBannerLink = vehicle.TradeInBannerLink,
                                    Tranmission = vehicle.Tranmission,
                                    Trim = vehicle.Trim,
                                    WebSiteUrl = vehicle.WebSiteURL,
                                    ZipCode = vehicle.ZipCode,
                                    Price = vehicle.Price.GetValueOrDefault(),
                                    CityId = vehicle.CityId.GetValueOrDefault(),
                                    DealerId = vehicle.VincontrolId.GetValueOrDefault(),
                                    PostingCityId = vehicle.CityId.GetValueOrDefault(),
                                    FirstImageUrl = firstImageUrl,
                                    AddtionalTitle = vehicle.OverideTitle,
                                    Schedules = vehicle.Schedules.GetValueOrDefault()

                                });

                        }
                    }
                }
                else
                {
                    foreach (var vehicle in myList)
                    {
                        int qualifiedImagesCount = 0;

                        string firstImageUrl = "";

                        if (!String.IsNullOrEmpty(vehicle.CarImageUrl))
                        {
                            string tmpString = vehicle.CarImageUrl.Replace(" ", "%20");

                            if (tmpString.Contains("|") || tmpString.Contains(","))
                            {

                                string[] totalImage = tmpString.Split(new[] { "|", "," },
                                                                      StringSplitOptions.RemoveEmptyEntries);

                                if (totalImage.Any())
                                {

                                    firstImageUrl = totalImage.First();
                                    qualifiedImagesCount = totalImage.Count();
                                }
                            }
                            else
                            {
                                firstImageUrl = tmpString;
                            }
                        }
                        else
                        {
                            firstImageUrl = vehicle.DefaultImageUrl;
                        }

                        if (qualifiedImagesCount >= 1)
                        {

                            bufferListDatabase.Add(new BufferVehicleModel()
                            {
                                ListingID = vehicle.ListingID,
                                StockNumber = vehicle.StockNumber,
                                VinNumber = vehicle.VINNumber,
                                VincontrolId = vehicle.VincontrolId.GetValueOrDefault(),
                                BodyType = vehicle.BodyType,
                                CarImageUrl = vehicle.CarImageUrl,
                                CarsOptions = vehicle.CarsOptions,
                                City = vehicle.City,
                                CityOveride = vehicle.CityOveride,
                                CreditUrl = vehicle.CreditURL,
                                Cylinders = vehicle.Cylinders,
                                DealershipName = vehicle.DealershipName,
                                DefaultImageUrl = vehicle.DefaultImageUrl,
                                Descriptions = vehicle.Descriptions,
                                Doors = vehicle.Doors,
                                DriveTrain = vehicle.DriveTrain,
                                Email = vehicle.Email,
                                EmailFormat = vehicle.EmailFormat.GetValueOrDefault(),
                                EngineType = vehicle.EngineType,
                                ExteriorColor = vehicle.ExteriorColor,
                                FuelType = vehicle.FuelType,
                                InteriorColor = vehicle.InteriorColor,
                                Liters = vehicle.Liters,
                                LogoUrl = vehicle.LogoURL,
                                Make = vehicle.Make,
                                Mileage = vehicle.Mileage,
                                Model = vehicle.Model,
                                ModelYear = vehicle.ModelYear,
                                PhoneNumber = vehicle.PhoneNumber,
                                SalePrice = vehicle.SalePrice,
                                State = vehicle.State,
                                StreetAddress = vehicle.StreetAddress,
                                TradeInBannerLink = vehicle.TradeInBannerLink,
                                Tranmission = vehicle.Tranmission,
                                Trim = vehicle.Trim,
                                WebSiteUrl = vehicle.WebSiteURL,
                                ZipCode = vehicle.ZipCode,
                                Price = vehicle.Price.GetValueOrDefault(),
                                CityId = vehicle.CityId.GetValueOrDefault(),
                                DealerId = vehicle.VincontrolId.GetValueOrDefault(),
                                PostingCityId = vehicle.CityId.GetValueOrDefault(),
                                FirstImageUrl = firstImageUrl,
                                AddtionalTitle = vehicle.OverideTitle,
                                Schedules = vehicle.Schedules.GetValueOrDefault()

                            });

                        }
                    }
                }

            }

            var splitInventoryBasedOnPc =
                bufferListDatabase.Split(bufferListDatabase.First().Schedules).ElementAt(splitPart).ToList();

            foreach (var vehicle in splitInventoryBasedOnPc)
            {
                if (!matchPast.Any(x=>x.ListingID==vehicle.ListingID && x.CityId==vehicle.CityId))
                {

                    var addCar = new WhitmanEntepriseMasterVehicleInfo()
                        {
                            ListingId = vehicle.ListingID,
                            StockNumber =
                                String.IsNullOrEmpty(vehicle.StockNumber)
                                    ? ""
                                    : vehicle.StockNumber,
                            Vin =
                                String.IsNullOrEmpty(vehicle.VinNumber)
                                    ? ""
                                    : vehicle.VinNumber,
                            ModelYear =
                                String.IsNullOrEmpty(vehicle.ModelYear)
                                    ? ""
                                    : vehicle.ModelYear,
                            Make =
                                String.IsNullOrEmpty(vehicle.Make)
                                    ? ""
                                    : vehicle.Make,
                            Model =
                                String.IsNullOrEmpty(vehicle.Model)
                                    ? ""
                                    : vehicle.Model,
                            Trim =
                                String.IsNullOrEmpty(vehicle.Trim)
                                    ? ""
                                    : vehicle.Trim,
                            Cylinder =
                                String.IsNullOrEmpty(vehicle.Cylinders)
                                    ? ""
                                    : vehicle.Cylinders,
                            BodyType =
                                String.IsNullOrEmpty(vehicle.BodyType)
                                    ? ""
                                    : vehicle.BodyType,
                            SalePrice =
                                String.IsNullOrEmpty(vehicle.SalePrice)
                                    ? ""
                                    : vehicle.SalePrice,
                            ExteriorColor =
                                String.IsNullOrEmpty(vehicle.ExteriorColor)
                                    ? ""
                                    : vehicle.ExteriorColor,
                            InteriorColor =
                                String.IsNullOrEmpty(vehicle.InteriorColor)
                                    ? ""
                                    : vehicle.InteriorColor,
                            Mileage =
                                String.IsNullOrEmpty(vehicle.Mileage)
                                    ? ""
                                    : vehicle.Mileage,
                            Description =
                                String.IsNullOrEmpty(vehicle.Descriptions)
                                    ? ""
                                    : vehicle.Descriptions,
                            CarImageUrl =
                                String.IsNullOrEmpty(vehicle.CarImageUrl)
                                    ? ""
                                    : vehicle.CarImageUrl.Replace("|", ","),
                            Door =
                                String.IsNullOrEmpty(vehicle.Doors)
                                    ? ""
                                    : vehicle.Doors,
                            Fuel =
                                String.IsNullOrEmpty(vehicle.FuelType)
                                    ? ""
                                    : vehicle.FuelType,
                            Litters =
                                String.IsNullOrEmpty(vehicle.Liters)
                                    ? ""
                                    : vehicle.Liters,
                            Tranmission =
                                String.IsNullOrEmpty(vehicle.Tranmission)
                                    ? ""
                                    : vehicle.Tranmission,
                            WheelDrive =
                                String.IsNullOrEmpty(vehicle.DriveTrain)
                                    ? ""
                                    : vehicle.DriveTrain,
                            Engine =
                                String.IsNullOrEmpty(vehicle.EngineType)
                                    ? ""
                                    : vehicle.EngineType,
                            Options =
                                String.IsNullOrEmpty(vehicle.CarsOptions)
                                    ? ""
                                    : vehicle.CarsOptions,
                            DefaultImageUrl =
                                String.IsNullOrEmpty(
                                    vehicle.DefaultImageUrl)
                                    ? ""
                                    : vehicle.DefaultImageUrl,
                            VincontrolId =
                                String.IsNullOrEmpty(
                                    vehicle.VincontrolId.ToString(CultureInfo.InvariantCulture))
                                    ? ""
                                    : vehicle.VincontrolId.ToString(CultureInfo.InvariantCulture),
                            DealershipName =
                                String.IsNullOrEmpty(
                                    vehicle.DealershipName)
                                    ? ""
                                    : vehicle.DealershipName,
                            PhoneNumber =
                                String.IsNullOrEmpty(vehicle.PhoneNumber)
                                    ? ""
                                    : vehicle.PhoneNumber,
                            StreetAddress =
                                String.IsNullOrEmpty(vehicle.StreetAddress)
                                    ? ""
                                    : vehicle.StreetAddress,
                            City =
                                String.IsNullOrEmpty(vehicle.City)
                                    ? ""
                                    : vehicle.City,
                            State =
                                String.IsNullOrEmpty(vehicle.State)
                                    ? ""
                                    : vehicle.State,
                            ZipCode =
                                String.IsNullOrEmpty(vehicle.ZipCode)
                                    ? ""
                                    : vehicle.ZipCode,
                            LogoUrl =
                                String.IsNullOrEmpty(vehicle.LogoUrl)
                                    ? ""
                                    : vehicle.LogoUrl,
                            WebSiteUrl =
                                String.IsNullOrEmpty(vehicle.WebSiteUrl)
                                    ? ""
                                    : vehicle.WebSiteUrl,
                            CreditUrl =
                                String.IsNullOrEmpty(vehicle.CreditUrl)
                                    ? ""
                                    : vehicle.CreditUrl,
                            Email =
                                String.IsNullOrEmpty(vehicle.Email)
                                    ? ""
                                    : vehicle.Email,
                            CityOveride =
                                String.IsNullOrEmpty(vehicle.CityOveride)
                                    ? ""
                                    : vehicle.CityOveride,
                            EmailFormat =
                                vehicle.EmailFormat,
                            AddtionalTitle =
                                String.IsNullOrEmpty(vehicle.AddtionalTitle)
                                    ? ""
                                    : vehicle.AddtionalTitle,

                            Price = vehicle.Price,

                            PostingCityId = vehicle.CityId,
                            DealerId =
                                vehicle.DealerId,
                            FirstImageUrl = vehicle.FirstImageUrl,

                            TradeInBannerLink = vehicle.TradeInBannerLink,

                            CraigslistExist = false,

                            CraigslistCityUrl =
                                clsVariables.currentComputer.CityList.First(x => x.CityID == vehicle.CityId)
                                            .CraigsListCityURL

                        };

                    bufferMasterVehicleList.Add(addCar);



                }

            }

            foreach (var tmp in bufferMasterVehicleList.Select(x => x.DealerId).Distinct())
            {
                foreach (var vehicle in bufferListDatabase.Where(x => x.VincontrolId == tmp))
                {
                    var addCar = new WhitmanEntepriseMasterVehicleInfo()
                        {
                            ListingId = vehicle.ListingID,
                            StockNumber =
                                String.IsNullOrEmpty(vehicle.StockNumber)
                                    ? ""
                                    : vehicle.StockNumber,
                            Vin =
                                String.IsNullOrEmpty(vehicle.VinNumber)
                                    ? ""
                                    : vehicle.VinNumber,
                            ModelYear =
                                String.IsNullOrEmpty(vehicle.ModelYear)
                                    ? ""
                                    : vehicle.ModelYear,
                            Make =
                                String.IsNullOrEmpty(vehicle.Make)
                                    ? ""
                                    : vehicle.Make,
                            Model =
                                String.IsNullOrEmpty(vehicle.Model)
                                    ? ""
                                    : vehicle.Model,
                            Trim =
                                String.IsNullOrEmpty(vehicle.Trim)
                                    ? ""
                                    : vehicle.Trim,
                            Cylinder =
                                String.IsNullOrEmpty(vehicle.Cylinders)
                                    ? ""
                                    : vehicle.Cylinders,
                            BodyType =
                                String.IsNullOrEmpty(vehicle.BodyType)
                                    ? ""
                                    : vehicle.BodyType,
                            SalePrice =
                                String.IsNullOrEmpty(vehicle.SalePrice)
                                    ? ""
                                    : vehicle.SalePrice,
                            ExteriorColor =
                                String.IsNullOrEmpty(vehicle.ExteriorColor)
                                    ? ""
                                    : vehicle.ExteriorColor,
                            InteriorColor =
                                String.IsNullOrEmpty(vehicle.InteriorColor)
                                    ? ""
                                    : vehicle.InteriorColor,
                            Mileage =
                                String.IsNullOrEmpty(vehicle.Mileage)
                                    ? ""
                                    : vehicle.Mileage,
                            Description =
                                String.IsNullOrEmpty(vehicle.Descriptions)
                                    ? ""
                                    : vehicle.Descriptions,
                            CarImageUrl =
                                String.IsNullOrEmpty(vehicle.CarImageUrl)
                                    ? ""
                                    : vehicle.CarImageUrl.Replace("|", ","),
                            Door =
                                String.IsNullOrEmpty(vehicle.Doors)
                                    ? ""
                                    : vehicle.Doors,
                            Fuel =
                                String.IsNullOrEmpty(vehicle.FuelType)
                                    ? ""
                                    : vehicle.FuelType,
                            Litters =
                                String.IsNullOrEmpty(vehicle.Liters)
                                    ? ""
                                    : vehicle.Liters,
                            Tranmission =
                                String.IsNullOrEmpty(vehicle.Tranmission)
                                    ? ""
                                    : vehicle.Tranmission,
                            WheelDrive =
                                String.IsNullOrEmpty(vehicle.DriveTrain)
                                    ? ""
                                    : vehicle.DriveTrain,
                            Engine =
                                String.IsNullOrEmpty(vehicle.EngineType)
                                    ? ""
                                    : vehicle.EngineType,
                            Options =
                                String.IsNullOrEmpty(vehicle.CarsOptions)
                                    ? ""
                                    : vehicle.CarsOptions,
                            DefaultImageUrl =
                                String.IsNullOrEmpty(
                                    vehicle.DefaultImageUrl)
                                    ? ""
                                    : vehicle.DefaultImageUrl,
                            VincontrolId =
                                String.IsNullOrEmpty(
                                    vehicle.VincontrolId.ToString(CultureInfo.InvariantCulture))
                                    ? ""
                                    : vehicle.VincontrolId.ToString(CultureInfo.InvariantCulture),
                            DealershipName =
                                String.IsNullOrEmpty(
                                    vehicle.DealershipName)
                                    ? ""
                                    : vehicle.DealershipName,
                            PhoneNumber =
                                String.IsNullOrEmpty(vehicle.PhoneNumber)
                                    ? ""
                                    : vehicle.PhoneNumber,
                            StreetAddress =
                                String.IsNullOrEmpty(vehicle.StreetAddress)
                                    ? ""
                                    : vehicle.StreetAddress,
                            City =
                                String.IsNullOrEmpty(vehicle.City)
                                    ? ""
                                    : vehicle.City,
                            State =
                                String.IsNullOrEmpty(vehicle.State)
                                    ? ""
                                    : vehicle.State,
                            ZipCode =
                                String.IsNullOrEmpty(vehicle.ZipCode)
                                    ? ""
                                    : vehicle.ZipCode,
                            LogoUrl =
                                String.IsNullOrEmpty(vehicle.LogoUrl)
                                    ? ""
                                    : vehicle.LogoUrl,
                            WebSiteUrl =
                                String.IsNullOrEmpty(vehicle.WebSiteUrl)
                                    ? ""
                                    : vehicle.WebSiteUrl,
                            CreditUrl =
                                String.IsNullOrEmpty(vehicle.CreditUrl)
                                    ? ""
                                    : vehicle.CreditUrl,
                            Email =
                                String.IsNullOrEmpty(vehicle.Email)
                                    ? ""
                                    : vehicle.Email,
                            CityOveride =
                                String.IsNullOrEmpty(vehicle.CityOveride)
                                    ? ""
                                    : vehicle.CityOveride,
                            EmailFormat =
                                vehicle.EmailFormat,
                            AddtionalTitle =
                                String.IsNullOrEmpty(vehicle.AddtionalTitle)
                                    ? ""
                                    : vehicle.AddtionalTitle,

                            Price = vehicle.Price,

                            PostingCityId = vehicle.CityId,
                            DealerId =
                                vehicle.DealerId,
                            FirstImageUrl = vehicle.FirstImageUrl,

                            TradeInBannerLink = vehicle.TradeInBannerLink,

                            CraigslistExist = false,

                            CraigslistCityUrl =
                                clsVariables.currentComputer.CityList.First(x => x.CityID == vehicle.CityId)
                                            .CraigsListCityURL

                        };



                    clsVariables.fullMasterVehicleList.Add(addCar);


                }
            }

            foreach (var tmp in bufferMasterVehicleList)
            {
                tmp.AutoID = clsVariables.currentMasterVehicleList.Count + 1;

                clsVariables.currentMasterVehicleList.Add(tmp);
            }


            
        }

        public static void InitializeSplitComputer(int computerId)
        {
            var context = new whitmanenterprisecraigslistEntities()
            {
                CommandTimeout = 18000
            };

            var bufferMasterVehicleList
                = new List<WhitmanEntepriseMasterVehicleInfo>();

            clsVariables.currentMasterVehicleList = new List<WhitmanEntepriseMasterVehicleInfo>();

            clsVariables.fullMasterVehicleList = new List<WhitmanEntepriseMasterVehicleInfo>();

            clsVariables.currentRenewList = new List<RenewScheduleModel>();

            var hashPast = new HashSet<int>();

            var bufferListDatabase = new List<BufferVehicleModel>();

            var dealerListPerPcId = clsVariables.computerList.Where(x => x.ComputerId == computerId);

            var dtCompareToday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            //var dtCompareNextDay = dtCompareToday.AddDays(-10);

            var dtCompare = DateTime.Now.AddHours(-55);

            //var checkList = from a in context.vincontrolcraigslisttrackings
            //                from b in context.vinclappemailaccounts
            //                where
            //                    a.EmailAccount == b.AccountName && a.AddedDate <= dtCompare &&
            //                    a.AddedDate > dtCompareNextDay && a.CLPostingId > 0
            //                select new
            //                    {
            //                        a.TrackingId,
            //                        a.ListingId,
            //                        a.CityId,
            //                        a.CLPostingId,
            //                        a.ShowAd,
            //                        a.EmailAccount,
            //                        b.AccountPassword,
            //                        b.Phone,
            //                        a.AddedDate,
            //                        a.ExpirationDate,
            //                        a.HtmlCraigslistUrl
            //                    };

            var checkList = from a in context.vincontrolcraigslisttrackings
                            from b in context.vinclappemailaccounts
                            where
                                a.EmailAccount == b.AccountName && a.AddedDate <= dtCompare &&
                                 a.CLPostingId > 0
                            select new
                            {
                                a.TrackingId,
                                a.ListingId,
                                a.CityId,
                                a.CLPostingId,
                                a.ShowAd,
                                a.EmailAccount,
                                b.AccountPassword,
                                b.Phone,
                                a.AddedDate,
                                a.ExpirationDate,
                                a.HtmlCraigslistUrl
                            };


            if (clsVariables.Reload)
            {
                var todayPastList = from a in context.vincontrolcraigslisttrackings
                                    where a.AddedDate <= DateTime.Now && a.AddedDate > dtCompareToday && a.Computer == computerId
                                    select new
                                    {
                                        a.TrackingId,
                                        a.ListingId,
                                        a.CityId,
                                        a.CLPostingId,
                                        a.ShowAd,
                                        a.EmailAccount,
                                        a.AddedDate,
                                        a.ExpirationDate
                                    };

                foreach (var tmp in todayPastList)
                {
                    string uniqueID = tmp.ListingId + "" + tmp.CityId;

                    hashPast.Add(Convert.ToInt32(uniqueID));
                }
            }

            foreach (var tmp in checkList.OrderBy(x => x.AddedDate))
            {
                clsVariables.currentRenewList.Add(new RenewScheduleModel()
                {
                    ListingId = tmp.ListingId.GetValueOrDefault(),
                    CityId = tmp.CityId.GetValueOrDefault(),
                    TrackingId = tmp.TrackingId,
                    CLPostingId = tmp.CLPostingId.GetValueOrDefault(),
                    CraigslistAccountName = tmp.EmailAccount,
                    CraigslistAccountPassword = tmp.AccountPassword,
                    CraigslistAccountPhone = tmp.Phone,
                    ExpirationDate = tmp.ExpirationDate.GetValueOrDefault(),
                    CraigslistURL = tmp.HtmlCraigslistUrl
                });
            }

            foreach (var tmp in dealerListPerPcId)
            {
                var invertoryPerId = from a in context.whitmanenterprisecraigslistinventories
                                     from b in context.whitmanenterprisedealerlists
                                     where
                                         b.VincontrolId == tmp.DealerId && a.DealershipId == b.VincontrolId &&
                                         !String.IsNullOrEmpty(a.CarImageUrl)
                                     select new
                                     {
                                         a.ListingID,
                                         a.StockNumber,
                                         a.VINNumber,
                                         a.ModelYear,
                                         a.Make,
                                         a.Model,
                                         a.Trim,
                                         a.Cylinders,
                                         a.BodyType,
                                         a.SalePrice,
                                         a.ExteriorColor,
                                         a.InteriorColor,
                                         a.Mileage,
                                         a.Descriptions,
                                         a.CarImageUrl,
                                         a.Doors,
                                         a.FuelType,
                                         a.Liters,
                                         a.Tranmission,
                                         a.DriveTrain,
                                         a.EngineType,
                                         a.CarsOptions,
                                         a.DefaultImageUrl,
                                         a.OverideTitle,
                                         b.VincontrolId,
                                         b.DealershipName,
                                         b.PhoneNumber,
                                         b.StreetAddress,
                                         b.City,
                                         b.State,
                                         b.ZipCode,
                                         b.LogoURL,
                                         b.WebSiteURL,
                                         b.CreditURL,
                                         b.Email,
                                         b.CityOveride,
                                         b.EmailFormat,
                                         b.TradeInBannerLink



                                     };

                if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday || DateTime.Now.DayOfWeek == DayOfWeek.Thursday ||
                    DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                {
                    if (tmp.DealerId == 7765 || tmp.DealerId == 9843)
                    {

                        var myList = invertoryPerId.ToList();

                        foreach (var vehicle in myList)
                        {
                            int qualifiedImagesCount = 0;

                            string firstImageUrl = "";

                            if (!String.IsNullOrEmpty(vehicle.CarImageUrl))
                            {
                                string tmpString = vehicle.CarImageUrl.Replace(" ", "%20");

                                if (tmpString.Contains("|") || tmpString.Contains(","))
                                {

                                    string[] totalImage = tmpString.Split(new[] { "|", "," },
                                                                          StringSplitOptions.RemoveEmptyEntries);

                                    if (totalImage.Any())
                                    {

                                        firstImageUrl = totalImage.First();
                                        qualifiedImagesCount = totalImage.Count();
                                    }
                                }
                                else
                                {
                                    firstImageUrl = tmpString;
                                }
                            }
                            else
                            {
                                firstImageUrl = vehicle.DefaultImageUrl;
                            }

                            if (qualifiedImagesCount >= 1)
                            {

                                bufferListDatabase.Add(new BufferVehicleModel()
                                {
                                    ListingID = vehicle.ListingID,
                                    StockNumber = vehicle.StockNumber,
                                    VinNumber = vehicle.VINNumber,
                                    VincontrolId = vehicle.VincontrolId.GetValueOrDefault(),
                                    BodyType = vehicle.BodyType,
                                    CarImageUrl = vehicle.CarImageUrl,
                                    CarsOptions = vehicle.CarsOptions,
                                    City = vehicle.City,
                                    CityOveride = vehicle.CityOveride,
                                    CreditUrl = vehicle.CreditURL,
                                    Cylinders = vehicle.Cylinders,
                                    DealershipName = vehicle.DealershipName,
                                    DefaultImageUrl = vehicle.DefaultImageUrl,
                                    Descriptions = vehicle.Descriptions,
                                    Doors = vehicle.Doors,
                                    DriveTrain = vehicle.DriveTrain,
                                    Email = vehicle.Email,
                                    EmailFormat = vehicle.EmailFormat.GetValueOrDefault(),
                                    EngineType = vehicle.EngineType,
                                    ExteriorColor = vehicle.ExteriorColor,
                                    FuelType = vehicle.FuelType,
                                    InteriorColor = vehicle.InteriorColor,
                                    Liters = vehicle.Liters,
                                    LogoUrl = vehicle.LogoURL,
                                    Make = vehicle.Make,
                                    Mileage = vehicle.Mileage,
                                    Model = vehicle.Model,
                                    ModelYear = vehicle.ModelYear,
                                    PhoneNumber = vehicle.PhoneNumber,
                                    SalePrice = vehicle.SalePrice,
                                    State = vehicle.State,
                                    StreetAddress = vehicle.StreetAddress,
                                    TradeInBannerLink = vehicle.TradeInBannerLink,
                                    Tranmission = vehicle.Tranmission,
                                    Trim = vehicle.Trim,
                                    WebSiteUrl = vehicle.WebSiteURL,
                                    ZipCode = vehicle.ZipCode,
                                    Price = tmp.Price,
                                    CityId = tmp.CityId,
                                    DealerId = tmp.DealerId,
                                    PostingCityId = tmp.CityId,
                                    FirstImageUrl = firstImageUrl,
                                    AddtionalTitle = vehicle.OverideTitle

                                });

                            }
                        }
                    }
                    else
                    {
                        var myList = invertoryPerId.ToList().Split(2).First();

                        foreach (var vehicle in myList)
                        {
                            int qualifiedImagesCount = 0;

                            string firstImageUrl = "";

                            if (!String.IsNullOrEmpty(vehicle.CarImageUrl))
                            {
                                string tmpString = vehicle.CarImageUrl.Replace(" ", "%20");

                                if (tmpString.Contains("|") || tmpString.Contains(","))
                                {

                                    string[] totalImage = tmpString.Split(new[] { "|", "," },
                                                                          StringSplitOptions.RemoveEmptyEntries);

                                    if (totalImage.Any())
                                    {

                                        firstImageUrl = totalImage.First();
                                        qualifiedImagesCount = totalImage.Count();
                                    }
                                }
                                else
                                {
                                    firstImageUrl = tmpString;
                                }
                            }
                            else
                            {
                                firstImageUrl = vehicle.DefaultImageUrl;
                            }

                            if (qualifiedImagesCount >= 1)
                            {

                                bufferListDatabase.Add(new BufferVehicleModel()
                                {
                                    ListingID = vehicle.ListingID,
                                    StockNumber = vehicle.StockNumber,
                                    VinNumber = vehicle.VINNumber,
                                    VincontrolId = vehicle.VincontrolId.GetValueOrDefault(),
                                    BodyType = vehicle.BodyType,
                                    CarImageUrl = vehicle.CarImageUrl,
                                    CarsOptions = vehicle.CarsOptions,
                                    City = vehicle.City,
                                    CityOveride = vehicle.CityOveride,
                                    CreditUrl = vehicle.CreditURL,
                                    Cylinders = vehicle.Cylinders,
                                    DealershipName = vehicle.DealershipName,
                                    DefaultImageUrl = vehicle.DefaultImageUrl,
                                    Descriptions = vehicle.Descriptions,
                                    Doors = vehicle.Doors,
                                    DriveTrain = vehicle.DriveTrain,
                                    Email = vehicle.Email,
                                    EmailFormat = vehicle.EmailFormat.GetValueOrDefault(),
                                    EngineType = vehicle.EngineType,
                                    ExteriorColor = vehicle.ExteriorColor,
                                    FuelType = vehicle.FuelType,
                                    InteriorColor = vehicle.InteriorColor,
                                    Liters = vehicle.Liters,
                                    LogoUrl = vehicle.LogoURL,
                                    Make = vehicle.Make,
                                    Mileage = vehicle.Mileage,
                                    Model = vehicle.Model,
                                    ModelYear = vehicle.ModelYear,
                                    PhoneNumber = vehicle.PhoneNumber,
                                    SalePrice = vehicle.SalePrice,
                                    State = vehicle.State,
                                    StreetAddress = vehicle.StreetAddress,
                                    TradeInBannerLink = vehicle.TradeInBannerLink,
                                    Tranmission = vehicle.Tranmission,
                                    Trim = vehicle.Trim,
                                    WebSiteUrl = vehicle.WebSiteURL,
                                    ZipCode = vehicle.ZipCode,
                                    Price = tmp.Price,
                                    CityId = tmp.CityId,
                                    DealerId = tmp.DealerId,
                                    PostingCityId = tmp.CityId,
                                    FirstImageUrl = firstImageUrl,
                                    AddtionalTitle = vehicle.OverideTitle

                                });

                            }
                        }
                    }

                }
                else if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday || DateTime.Now.DayOfWeek == DayOfWeek.Friday ||
                         DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                {
                    if (tmp.DealerId == 7765 || tmp.DealerId == 9843)
                    {

                        var myList = invertoryPerId.ToList();

                        foreach (var vehicle in myList)
                        {
                            int qualifiedImagesCount = 0;

                            string firstImageUrl = "";

                            if (!String.IsNullOrEmpty(vehicle.CarImageUrl))
                            {
                                string tmpString = vehicle.CarImageUrl.Replace(" ", "%20");

                                if (tmpString.Contains("|") || tmpString.Contains(","))
                                {

                                    string[] totalImage = tmpString.Split(new[] { "|", "," },
                                                                          StringSplitOptions.RemoveEmptyEntries);

                                    if (totalImage.Any())
                                    {

                                        firstImageUrl = totalImage.First();
                                        qualifiedImagesCount = totalImage.Count();
                                    }
                                }
                                else
                                {
                                    firstImageUrl = tmpString;
                                }
                            }
                            else
                            {
                                firstImageUrl = vehicle.DefaultImageUrl;
                            }

                            if (qualifiedImagesCount >= 1)
                            {

                                bufferListDatabase.Add(new BufferVehicleModel()
                                {
                                    ListingID = vehicle.ListingID,
                                    StockNumber = vehicle.StockNumber,
                                    VinNumber = vehicle.VINNumber,
                                    VincontrolId = vehicle.VincontrolId.GetValueOrDefault(),
                                    BodyType = vehicle.BodyType,
                                    CarImageUrl = vehicle.CarImageUrl,
                                    CarsOptions = vehicle.CarsOptions,
                                    City = vehicle.City,
                                    CityOveride = vehicle.CityOveride,
                                    CreditUrl = vehicle.CreditURL,
                                    Cylinders = vehicle.Cylinders,
                                    DealershipName = vehicle.DealershipName,
                                    DefaultImageUrl = vehicle.DefaultImageUrl,
                                    Descriptions = vehicle.Descriptions,
                                    Doors = vehicle.Doors,
                                    DriveTrain = vehicle.DriveTrain,
                                    Email = vehicle.Email,
                                    EmailFormat = vehicle.EmailFormat.GetValueOrDefault(),
                                    EngineType = vehicle.EngineType,
                                    ExteriorColor = vehicle.ExteriorColor,
                                    FuelType = vehicle.FuelType,
                                    InteriorColor = vehicle.InteriorColor,
                                    Liters = vehicle.Liters,
                                    LogoUrl = vehicle.LogoURL,
                                    Make = vehicle.Make,
                                    Mileage = vehicle.Mileage,
                                    Model = vehicle.Model,
                                    ModelYear = vehicle.ModelYear,
                                    PhoneNumber = vehicle.PhoneNumber,
                                    SalePrice = vehicle.SalePrice,
                                    State = vehicle.State,
                                    StreetAddress = vehicle.StreetAddress,
                                    TradeInBannerLink = vehicle.TradeInBannerLink,
                                    Tranmission = vehicle.Tranmission,
                                    Trim = vehicle.Trim,
                                    WebSiteUrl = vehicle.WebSiteURL,
                                    ZipCode = vehicle.ZipCode,
                                    Price = tmp.Price,
                                    CityId = tmp.CityId,
                                    DealerId = tmp.DealerId,
                                    PostingCityId = tmp.CityId,
                                    FirstImageUrl = firstImageUrl,
                                    AddtionalTitle = vehicle.OverideTitle

                                });

                            }
                        }
                    }
                    else
                    {
                        var myList = invertoryPerId.ToList().Split(2).Last();

                        foreach (var vehicle in myList)
                        {
                            int qualifiedImagesCount = 0;

                            string firstImageUrl = "";

                            if (!String.IsNullOrEmpty(vehicle.CarImageUrl))
                            {
                                string tmpString = vehicle.CarImageUrl.Replace(" ", "%20");

                                if (tmpString.Contains("|") || tmpString.Contains(","))
                                {

                                    string[] totalImage = tmpString.Split(new[] { "|", "," },
                                                                          StringSplitOptions.RemoveEmptyEntries);

                                    if (totalImage.Any())
                                    {

                                        firstImageUrl = totalImage.First();
                                        qualifiedImagesCount = totalImage.Count();
                                    }
                                }
                                else
                                {
                                    firstImageUrl = tmpString;
                                }
                            }
                            else
                            {
                                firstImageUrl = vehicle.DefaultImageUrl;
                            }

                            if (qualifiedImagesCount >= 1)
                            {

                                bufferListDatabase.Add(new BufferVehicleModel()
                                {
                                    ListingID = vehicle.ListingID,
                                    StockNumber = vehicle.StockNumber,
                                    VinNumber = vehicle.VINNumber,
                                    VincontrolId = vehicle.VincontrolId.GetValueOrDefault(),
                                    BodyType = vehicle.BodyType,
                                    CarImageUrl = vehicle.CarImageUrl,
                                    CarsOptions = vehicle.CarsOptions,
                                    City = vehicle.City,
                                    CityOveride = vehicle.CityOveride,
                                    CreditUrl = vehicle.CreditURL,
                                    Cylinders = vehicle.Cylinders,
                                    DealershipName = vehicle.DealershipName,
                                    DefaultImageUrl = vehicle.DefaultImageUrl,
                                    Descriptions = vehicle.Descriptions,
                                    Doors = vehicle.Doors,
                                    DriveTrain = vehicle.DriveTrain,
                                    Email = vehicle.Email,
                                    EmailFormat = vehicle.EmailFormat.GetValueOrDefault(),
                                    EngineType = vehicle.EngineType,
                                    ExteriorColor = vehicle.ExteriorColor,
                                    FuelType = vehicle.FuelType,
                                    InteriorColor = vehicle.InteriorColor,
                                    Liters = vehicle.Liters,
                                    LogoUrl = vehicle.LogoURL,
                                    Make = vehicle.Make,
                                    Mileage = vehicle.Mileage,
                                    Model = vehicle.Model,
                                    ModelYear = vehicle.ModelYear,
                                    PhoneNumber = vehicle.PhoneNumber,
                                    SalePrice = vehicle.SalePrice,
                                    State = vehicle.State,
                                    StreetAddress = vehicle.StreetAddress,
                                    TradeInBannerLink = vehicle.TradeInBannerLink,
                                    Tranmission = vehicle.Tranmission,
                                    Trim = vehicle.Trim,
                                    WebSiteUrl = vehicle.WebSiteURL,
                                    ZipCode = vehicle.ZipCode,
                                    Price = tmp.Price,
                                    CityId = tmp.CityId,
                                    DealerId = tmp.DealerId,
                                    PostingCityId = tmp.CityId,
                                    FirstImageUrl = firstImageUrl,
                                    AddtionalTitle = vehicle.OverideTitle

                                });

                            }
                        }
                    }


                }
                else
                {


                    foreach (var vehicle in invertoryPerId.ToList())
                    {
                        int qualifiedImagesCount = 0;

                        string firstImageUrl = "";

                        if (!String.IsNullOrEmpty(vehicle.CarImageUrl))
                        {
                            string tmpString = vehicle.CarImageUrl.Replace(" ", "%20");

                            if (tmpString.Contains("|") || tmpString.Contains(","))
                            {

                                string[] totalImage = tmpString.Split(new[] { "|", "," },
                                                                      StringSplitOptions.RemoveEmptyEntries);


                                if (totalImage.Any())
                                {

                                    firstImageUrl = totalImage.First();
                                    qualifiedImagesCount = totalImage.Count();
                                }


                            }
                            else
                            {
                                firstImageUrl = tmpString;
                            }
                        }
                        else
                        {
                            firstImageUrl = vehicle.DefaultImageUrl;
                        }

                        if (qualifiedImagesCount >= 1)
                        {

                            bufferListDatabase.Add(new BufferVehicleModel()
                            {
                                ListingID = vehicle.ListingID,
                                StockNumber = vehicle.StockNumber,
                                VinNumber = vehicle.VINNumber,
                                VincontrolId = vehicle.VincontrolId.GetValueOrDefault(),
                                BodyType = vehicle.BodyType,
                                CarImageUrl = vehicle.CarImageUrl,
                                CarsOptions = vehicle.CarsOptions,
                                City = vehicle.City,
                                CityOveride = vehicle.CityOveride,
                                CreditUrl = vehicle.CreditURL,
                                Cylinders = vehicle.Cylinders,
                                DealershipName = vehicle.DealershipName,
                                DefaultImageUrl = vehicle.DefaultImageUrl,
                                Descriptions = vehicle.Descriptions,
                                Doors = vehicle.Doors,
                                DriveTrain = vehicle.DriveTrain,
                                Email = vehicle.Email,
                                EmailFormat = vehicle.EmailFormat.GetValueOrDefault(),
                                EngineType = vehicle.EngineType,
                                ExteriorColor = vehicle.ExteriorColor,
                                FuelType = vehicle.FuelType,
                                InteriorColor = vehicle.InteriorColor,
                                Liters = vehicle.Liters,
                                LogoUrl = vehicle.LogoURL,
                                Make = vehicle.Make,
                                Mileage = vehicle.Mileage,
                                Model = vehicle.Model,
                                ModelYear = vehicle.ModelYear,
                                PhoneNumber = vehicle.PhoneNumber,
                                SalePrice = vehicle.SalePrice,
                                State = vehicle.State,
                                StreetAddress = vehicle.StreetAddress,
                                TradeInBannerLink = vehicle.TradeInBannerLink,
                                Tranmission = vehicle.Tranmission,
                                Trim = vehicle.Trim,
                                WebSiteUrl = vehicle.WebSiteURL,
                                ZipCode = vehicle.ZipCode,
                                Price = tmp.Price,
                                CityId = tmp.CityId,
                                DealerId = tmp.DealerId,
                                PostingCityId = tmp.CityId,
                                FirstImageUrl = firstImageUrl,
                                AddtionalTitle = vehicle.OverideTitle

                            });

                        }


                    }


                }



            }




            foreach (var vehicle in bufferListDatabase)
            {
                string uniqueID = vehicle.ListingID + "" + vehicle.CityId;

                int compareId = Convert.ToInt32(uniqueID);

                if (!hashPast.Contains(compareId))
                {

                    var addCar = new WhitmanEntepriseMasterVehicleInfo()
                    {
                        ListingId = vehicle.ListingID,
                        StockNumber =
                            String.IsNullOrEmpty(vehicle.StockNumber)
                                ? ""
                                : vehicle.StockNumber,
                        Vin =
                            String.IsNullOrEmpty(vehicle.VinNumber)
                                ? ""
                                : vehicle.VinNumber,
                        ModelYear =
                            String.IsNullOrEmpty(vehicle.ModelYear)
                                ? ""
                                : vehicle.ModelYear,
                        Make =
                            String.IsNullOrEmpty(vehicle.Make)
                                ? ""
                                : vehicle.Make,
                        Model =
                            String.IsNullOrEmpty(vehicle.Model)
                                ? ""
                                : vehicle.Model,
                        Trim =
                            String.IsNullOrEmpty(vehicle.Trim)
                                ? ""
                                : vehicle.Trim,
                        Cylinder =
                            String.IsNullOrEmpty(vehicle.Cylinders)
                                ? ""
                                : vehicle.Cylinders,
                        BodyType =
                            String.IsNullOrEmpty(vehicle.BodyType)
                                ? ""
                                : vehicle.BodyType,
                        SalePrice =
                            String.IsNullOrEmpty(vehicle.SalePrice)
                                ? ""
                                : vehicle.SalePrice,
                        ExteriorColor =
                            String.IsNullOrEmpty(vehicle.ExteriorColor)
                                ? ""
                                : vehicle.ExteriorColor,
                        InteriorColor =
                            String.IsNullOrEmpty(vehicle.InteriorColor)
                                ? ""
                                : vehicle.InteriorColor,
                        Mileage =
                            String.IsNullOrEmpty(vehicle.Mileage)
                                ? ""
                                : vehicle.Mileage,
                        Description =
                            String.IsNullOrEmpty(vehicle.Descriptions)
                                ? ""
                                : vehicle.Descriptions,
                        CarImageUrl =
                            String.IsNullOrEmpty(vehicle.CarImageUrl)
                                ? ""
                                : vehicle.CarImageUrl.Replace("|", ","),
                        Door =
                            String.IsNullOrEmpty(vehicle.Doors)
                                ? ""
                                : vehicle.Doors,
                        Fuel =
                            String.IsNullOrEmpty(vehicle.FuelType)
                                ? ""
                                : vehicle.FuelType,
                        Litters =
                            String.IsNullOrEmpty(vehicle.Liters)
                                ? ""
                                : vehicle.Liters,
                        Tranmission =
                            String.IsNullOrEmpty(vehicle.Tranmission)
                                ? ""
                                : vehicle.Tranmission,
                        WheelDrive =
                            String.IsNullOrEmpty(vehicle.DriveTrain)
                                ? ""
                                : vehicle.DriveTrain,
                        Engine =
                            String.IsNullOrEmpty(vehicle.EngineType)
                                ? ""
                                : vehicle.EngineType,
                        Options =
                            String.IsNullOrEmpty(vehicle.CarsOptions)
                                ? ""
                                : vehicle.CarsOptions,
                        DefaultImageUrl =
                            String.IsNullOrEmpty(
                                vehicle.DefaultImageUrl)
                                ? ""
                                : vehicle.DefaultImageUrl,
                        VincontrolId =
                            String.IsNullOrEmpty(
                                vehicle.VincontrolId.ToString(CultureInfo.InvariantCulture))
                                ? ""
                                : vehicle.VincontrolId.ToString(CultureInfo.InvariantCulture),
                        DealershipName =
                            String.IsNullOrEmpty(
                                vehicle.DealershipName)
                                ? ""
                                : vehicle.DealershipName,
                        PhoneNumber =
                            String.IsNullOrEmpty(vehicle.PhoneNumber)
                                ? ""
                                : vehicle.PhoneNumber,
                        StreetAddress =
                            String.IsNullOrEmpty(vehicle.StreetAddress)
                                ? ""
                                : vehicle.StreetAddress,
                        City =
                            String.IsNullOrEmpty(vehicle.City)
                                ? ""
                                : vehicle.City,
                        State =
                            String.IsNullOrEmpty(vehicle.State)
                                ? ""
                                : vehicle.State,
                        ZipCode =
                            String.IsNullOrEmpty(vehicle.ZipCode)
                                ? ""
                                : vehicle.ZipCode,
                        LogoUrl =
                            String.IsNullOrEmpty(vehicle.LogoUrl)
                                ? ""
                                : vehicle.LogoUrl,
                        WebSiteUrl =
                            String.IsNullOrEmpty(vehicle.WebSiteUrl)
                                ? ""
                                : vehicle.WebSiteUrl,
                        CreditUrl =
                            String.IsNullOrEmpty(vehicle.CreditUrl)
                                ? ""
                                : vehicle.CreditUrl,
                        Email =
                            String.IsNullOrEmpty(vehicle.Email)
                                ? ""
                                : vehicle.Email,
                        CityOveride =
                            String.IsNullOrEmpty(vehicle.CityOveride)
                                ? ""
                                : vehicle.CityOveride,
                        EmailFormat =
                            vehicle.EmailFormat,
                        AddtionalTitle =
                            String.IsNullOrEmpty(vehicle.AddtionalTitle)
                                ? ""
                                : vehicle.AddtionalTitle,

                        Price = vehicle.Price,

                        PostingCityId = vehicle.CityId,
                        DealerId =
                            vehicle.DealerId,
                        FirstImageUrl = vehicle.FirstImageUrl,

                        TradeInBannerLink = vehicle.TradeInBannerLink,

                        CraigslistExist = false,

                        CraigslistCityUrl =
                            clsVariables.currentComputer.CityList.First(x => x.CityID == vehicle.CityId)
                                        .CraigsListCityURL

                    };



                    if (
                        clsVariables.currentRenewList.Any(
                            x => x.ListingId == addCar.ListingId && x.CityId == addCar.PostingCityId))
                    {



                        var searchResult =
                            clsVariables.currentRenewList.First(
                                x => x.ListingId == addCar.ListingId && x.CityId == addCar.PostingCityId);
                        addCar.CraigslistUrl = searchResult.CraigslistURL;
                        addCar.CraigslistAccountName = searchResult.CraigslistAccountName;
                        addCar.CraigslistAccountPassword = searchResult.CraigslistAccountPassword;
                        addCar.CraigslistAccountPhone = searchResult.CraigslistAccountPhone;
                        addCar.CLPostingId = searchResult.CLPostingId;
                        addCar.AdTrackingId = searchResult.TrackingId;


                    }

                    bufferMasterVehicleList.Add(addCar);
                    clsVariables.fullMasterVehicleList.Add(addCar);


                }
                else
                {

                    var addCar = new WhitmanEntepriseMasterVehicleInfo()
                    {

                        ListingId = vehicle.ListingID,
                        StockNumber =
                            String.IsNullOrEmpty(vehicle.StockNumber)
                                ? ""
                                : vehicle.StockNumber,
                        Vin =
                            String.IsNullOrEmpty(vehicle.VinNumber)
                                ? ""
                                : vehicle.VinNumber,
                        ModelYear =
                            String.IsNullOrEmpty(vehicle.ModelYear)
                                ? ""
                                : vehicle.ModelYear,
                        Make =
                            String.IsNullOrEmpty(vehicle.Make)
                                ? ""
                                : vehicle.Make,
                        Model =
                            String.IsNullOrEmpty(vehicle.Model)
                                ? ""
                                : vehicle.Model,
                        Trim =
                            String.IsNullOrEmpty(vehicle.Trim)
                                ? ""
                                : vehicle.Trim,
                        Cylinder =
                            String.IsNullOrEmpty(vehicle.Cylinders)
                                ? ""
                                : vehicle.Cylinders,
                        BodyType =
                            String.IsNullOrEmpty(vehicle.BodyType)
                                ? ""
                                : vehicle.BodyType,
                        SalePrice =
                            String.IsNullOrEmpty(vehicle.SalePrice)
                                ? ""
                                : vehicle.SalePrice,
                        ExteriorColor =
                            String.IsNullOrEmpty(vehicle.ExteriorColor)
                                ? ""
                                : vehicle.ExteriorColor,
                        InteriorColor =
                            String.IsNullOrEmpty(vehicle.InteriorColor)
                                ? ""
                                : vehicle.InteriorColor,
                        Mileage =
                            String.IsNullOrEmpty(vehicle.Mileage)
                                ? ""
                                : vehicle.Mileage,
                        Description =
                            String.IsNullOrEmpty(vehicle.Descriptions)
                                ? ""
                                : vehicle.Descriptions,
                        CarImageUrl =
                            String.IsNullOrEmpty(vehicle.CarImageUrl)
                                ? ""
                                : vehicle.CarImageUrl.Replace("|", ","),
                        Door =
                            String.IsNullOrEmpty(vehicle.Doors)
                                ? ""
                                : vehicle.Doors,
                        Fuel =
                            String.IsNullOrEmpty(vehicle.FuelType)
                                ? ""
                                : vehicle.FuelType,
                        Litters =
                            String.IsNullOrEmpty(vehicle.Liters)
                                ? ""
                                : vehicle.Liters,
                        Tranmission =
                            String.IsNullOrEmpty(vehicle.Tranmission)
                                ? ""
                                : vehicle.Tranmission,
                        WheelDrive =
                            String.IsNullOrEmpty(vehicle.DriveTrain)
                                ? ""
                                : vehicle.DriveTrain,
                        Engine =
                            String.IsNullOrEmpty(vehicle.EngineType)
                                ? ""
                                : vehicle.EngineType,
                        Options =
                            String.IsNullOrEmpty(vehicle.CarsOptions)
                                ? ""
                                : vehicle.CarsOptions,
                        DefaultImageUrl =
                            String.IsNullOrEmpty(
                                vehicle.DefaultImageUrl)
                                ? ""
                                : vehicle.DefaultImageUrl,
                        VincontrolId =
                            String.IsNullOrEmpty(
                                vehicle.VincontrolId.ToString(CultureInfo.InvariantCulture))
                                ? ""
                                : vehicle.VincontrolId.ToString(CultureInfo.InvariantCulture),
                        DealershipName =
                            String.IsNullOrEmpty(
                                vehicle.DealershipName)
                                ? ""
                                : vehicle.DealershipName,
                        PhoneNumber =
                            String.IsNullOrEmpty(vehicle.PhoneNumber)
                                ? ""
                                : vehicle.PhoneNumber,
                        StreetAddress =
                            String.IsNullOrEmpty(vehicle.StreetAddress)
                                ? ""
                                : vehicle.StreetAddress,
                        City =
                            String.IsNullOrEmpty(vehicle.City)
                                ? ""
                                : vehicle.City,
                        State =
                            String.IsNullOrEmpty(vehicle.State)
                                ? ""
                                : vehicle.State,
                        ZipCode =
                            String.IsNullOrEmpty(vehicle.ZipCode)
                                ? ""
                                : vehicle.ZipCode,
                        LogoUrl =
                            String.IsNullOrEmpty(vehicle.LogoUrl)
                                ? ""
                                : vehicle.LogoUrl,
                        WebSiteUrl =
                            String.IsNullOrEmpty(vehicle.WebSiteUrl)
                                ? ""
                                : vehicle.WebSiteUrl,
                        CreditUrl =
                            String.IsNullOrEmpty(vehicle.CreditUrl)
                                ? ""
                                : vehicle.CreditUrl,
                        Email =
                            String.IsNullOrEmpty(vehicle.Email)
                                ? ""
                                : vehicle.Email,
                        CityOveride =
                            String.IsNullOrEmpty(vehicle.CityOveride)
                                ? ""
                                : vehicle.CityOveride,
                        EmailFormat =
                            vehicle.EmailFormat,
                        AddtionalTitle =
                          String.IsNullOrEmpty(vehicle.AddtionalTitle)
                              ? ""
                              : vehicle.AddtionalTitle,

                        Price = vehicle.Price,

                        PostingCityId = vehicle.CityId,
                        DealerId =
                            vehicle.DealerId,
                        FirstImageUrl = vehicle.FirstImageUrl,

                        TradeInBannerLink = vehicle.TradeInBannerLink,
                        CraigslistExist = false,
                        CraigslistCityUrl =
                            clsVariables.currentComputer.CityList.First(x => x.CityID == vehicle.CityId)
                                        .CraigsListCityURL



                    };

                    clsVariables.fullMasterVehicleList.Add(addCar);
                }
            }



            foreach (
                var tmp in bufferMasterVehicleList.Where(x => x.CLPostingId > 0).OrderBy(x => x.CraigslistAccountName))
            {
                tmp.AutoID = clsVariables.currentMasterVehicleList.Count + 1;

                clsVariables.currentMasterVehicleList.Add(tmp);
            }

            if (clsVariables.ReloadRenew == false)
            {
                foreach (var tmp in bufferMasterVehicleList.Where(x => x.CLPostingId == 0))
                {
                    tmp.AutoID = clsVariables.currentMasterVehicleList.Count + 1;

                    clsVariables.currentMasterVehicleList.Add(tmp);
                }

            }





        }

        public static void InitializeGlobalRenewByComputer(int computerId)
        {
            var context = new whitmanenterprisecraigslistEntities();

            var checkList = from a in context.vincontrolcraigslisttrackings
                            from b in context.vinclappemailaccounts
                            where a.ShowAd == true && a.ExpirationDate < DateTime.Now
                            && a.EmailAccount==b.AccountName && a.Computer==computerId
                            select new
                            {
                                a.TrackingId,
                                a.ListingId,
                                a.CityId,
                                a.CLPostingId,
                                a.ShowAd,
                                a.EmailAccount,
                                b.AccountPassword,
                                a.ExpirationDate
                            };
            clsVariables.currentRenewList =new List<RenewScheduleModel>();

            foreach (var tmp in checkList)
            {
                clsVariables.currentRenewList.Add(new RenewScheduleModel()
                                                      {
                                                          ListingId = tmp.ListingId.GetValueOrDefault(),
                                                          CityId = tmp.CityId.GetValueOrDefault(),
                                                          TrackingId = tmp.TrackingId,
                                                          CLPostingId = tmp.CLPostingId.GetValueOrDefault(),
                                                          CraigslistAccountName                                                          = tmp.EmailAccount,
                                                          CraigslistAccountPassword                                                          = tmp.AccountPassword
                                                      });
            }
            
        }

      
       
      
       
        public static bool CheckDealerExist(string dealerId)
        {
            var context = new whitmanenterprisecraigslistEntities();

            int did = 0;

            bool flag = Int32.TryParse(dealerId, out did);

            if(flag)
            {
                if (context.whitmanenterprisedealerlists.Any(x => x.VincontrolId == did))
                    flag = true;
                else
                {
                    flag = false;
                }
            }

            return flag;
        }

        public static List<WhitmanEnterpriseChunkString> GetChunkString()
        {
            try
            {

                var context = new whitmanenterprisecraigslistEntities();

                var list = new List<WhitmanEnterpriseChunkString>();

                foreach (var drRow in context.whitmanenterprisechunkstrings.ToList())
                {
                    var chunkString = new WhitmanEnterpriseChunkString()
                                          {
                                              ChunkName = drRow.ChunkName,
                                              ChunkStringValue = drRow.ChunkValue

                                          };

                    list.Add(chunkString);
                }

                return list;

            }
            catch (Exception ex)
            {
                throw new Exception("Exception is " + ex.Message);

            }



        }

     

        public static  void UpdateLastTimeUsed(string emailAccount)
        {
            try
            {

                var context = new whitmanenterprisecraigslistEntities();

                if(context.vinclappemailaccounts.Any(x=>x.AccountName==emailAccount))
                {
                    var result = context.vinclappemailaccounts.First(x => x.AccountName == emailAccount);

                    result.LastTimeUsed = DateTime.Now;

                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
               

            }
        }
      
        public static int AddNewTracking(CraigsListTrackingModel clModel,ImageModel imageModel)
        {
            try
            {

                var context = new whitmanenterprisecraigslistEntities();

                var clTracking = new vincontrolcraigslisttracking()
                                     {
                                         CityId = clModel.CityId,
                                         DealerId = clModel.DealerId,
                                         EmailAccount = clModel.EmailAccount,
                                         ShowAd = false,
                                         ExpirationDate = DateTime.Now.AddDays(3),
                                         AddedDate = DateTime.Now,
                                         ListingId = clModel.ListingId,
                                        Renew = false,
                                         Computer = clModel.Computer,
                                     


                                     };

                context.AddTovincontrolcraigslisttrackings(clTracking);

                context.SaveChanges();

                return clTracking.TrackingId;
            }
            catch (Exception ex)
            {
                return 0;

            }
        }

        public static int AddNewTracking(CraigsListTrackingModel clModel,  string htmlLink)
        {
            try
            {
                    long cLPostingId =
                               Convert.ToInt64(htmlLink.Substring(
                                   htmlLink.LastIndexOf("/", System.StringComparison.Ordinal) + 1).Replace(
                                       ".html", ""));

                var context = new whitmanenterprisecraigslistEntities();

                var clTracking = new vincontrolcraigslisttracking()
                {
                    CityId = clModel.CityId,
                    DealerId = clModel.DealerId,
                    EmailAccount = clModel.EmailAccount,
                    ShowAd = false,
                    ExpirationDate = DateTime.Now.AddDays(3),
                    AddedDate = DateTime.Now,
                    ListingId = clModel.ListingId,
                    Renew = false,
                    Computer = clModel.Computer,
                    HtmlCraigslistUrl = htmlLink,
                    CLPostingId = cLPostingId,
                    RunningIP = NetworkManagement.GetIpAddress()


                };

                context.AddTovincontrolcraigslisttrackings(clTracking);

                context.SaveChanges();

                return clTracking.TrackingId;
            }
            catch (Exception ex)
            {
                return 0;

            }
        }

       

       
        public static void UpdateCurrentTracking(int trackingId, string htmlLink)
        {
            try
            {

                var context = new whitmanenterprisecraigslistEntities();

                var searchResult = context.vincontrolcraigslisttrackings.First(x => x.TrackingId == trackingId);

                long cLPostingId =
                               Convert.ToInt64(htmlLink.Substring(
                                   htmlLink.LastIndexOf("/", System.StringComparison.Ordinal) + 1).Replace(
                                       ".html", ""));

                searchResult.HtmlCraigslistUrl = htmlLink;

                searchResult.CLPostingId = cLPostingId;

                context.SaveChanges();

                
            }
            catch (Exception ex)
            {
               

            }
        }

        public static void UpdateRenew(int trackingId)
        {
            try
            {

                var context = new whitmanenterprisecraigslistEntities();

                if (context.vincontrolcraigslisttrackings.Any(x => x.TrackingId == trackingId))
                {

                    var searchResult =
                        context.vincontrolcraigslisttrackings.First(x => x.TrackingId == trackingId);


                    searchResult.AddedDate = DateTime.Now;

                    searchResult.ExpirationDate = DateTime.Now.AddDays(2).AddHours(6);

                    searchResult.ShowAd = true;

                    searchResult.Renew = true;

                    searchResult.CheckDate = DateTime.Now;

                    context.SaveChanges();
                }


            }
            catch (Exception ex)
            {


            }
        }

        public static void DeleteCurrentTracking(int trackingId)
        {
            try
            {

                var context = new whitmanenterprisecraigslistEntities();

                if (context.vincontrolcraigslisttrackings.Any(x => x.TrackingId == trackingId))
                {


                    var searchResult = context.vincontrolcraigslisttrackings.First(x => x.TrackingId == trackingId);

                    context.Attach(searchResult);
                    context.DeleteObject(searchResult);



                    context.SaveChanges();
                }


            }
            catch (Exception ex)
            {


            }
        }


        public static List<WhitmanEntepriseMasterVehicleInfo> GetRandomDesriedVehicles(WhitmanEntepriseMasterVehicleInfo vehicle)
        {
            var squareRandom = new List<WhitmanEntepriseMasterVehicleInfo>();

            if (clsVariables.fullMasterVehicleList.Any(x => x.DealerId == vehicle.DealerId))
            {
                var carsBelongSameDealer = clsVariables.fullMasterVehicleList.Where(x => x.DealerId == vehicle.DealerId);
                if (carsBelongSameDealer.Count() >= 4)
                {
                    if (carsBelongSameDealer.Any(x => x.Make == vehicle.Make))
                    {
                        var randomSameMake = carsBelongSameDealer.Where(x => x.Make == vehicle.Make);
                        
                        if (randomSameMake.Count() >= 4)
                        {
                            squareRandom.AddRange(
                                randomSameMake.ToList().GetDistinctRandom(4));   
                        }
                        else
                        {
                            squareRandom.AddRange(randomSameMake.ToList());

                            squareRandom.AddRange(carsBelongSameDealer.ToList().GetDistinctRandom(2));
                        }
                    }
                    else
                    {
                        squareRandom.AddRange(
                           carsBelongSameDealer.ToList().GetDistinctRandom(4));
                    }




                }
                else
                {
                    squareRandom.AddRange(
                        clsVariables.fullMasterVehicleList.Where(x => x.DealerId == vehicle.DealerId).ToList());
                }
            }

            return squareRandom;
        }

        public static List<CraigsListEmailAccount> PopulateEmailHavingRenewAds(int computerId)
        {
            var context = new whitmanenterprisecraigslistEntities()
                {
                    CommandTimeout = 18000
                };

            var bufferMasterVehicleList
                = new List<CraigsListEmailAccount>();

            var checkList = from a in context.vincontrolcraigslisttrackings
                            from b in context.vinclappemailaccounts
                            where
                                a.EmailAccount == b.AccountName && b.Computer == computerId && a.CLPostingId > 0
                            select new
                                {
                                    b.AccountName,
                                    b.AccountPassword,
                                    b.Phone,
                                    b.ProxyIP,
                                    b.DefaultGateway,
                                    b.Dns
                                };

            var hashSet = new HashSet<string>();

            foreach (var tmp in checkList.OrderBy(x => x.AccountName))
            {
                if (!hashSet.Contains(tmp.AccountName))
                {
                    var clAccount = new CraigsListEmailAccount()
                        {
                            CraigslistAccount = tmp.AccountName,
                            CraigsListPassword = tmp.AccountPassword,
                            CraigsAccountPhoneNumber = tmp.Phone,
                            Proxy = tmp.ProxyIP,
                            DefaultGateWay = tmp.DefaultGateway,
                            Dns = tmp.Dns

                        };
                    bufferMasterVehicleList.Add(clAccount);
                }

                hashSet.Add(tmp.AccountName);
            }
            
            return bufferMasterVehicleList;
        }

        public static void ShowAdsButEligibleForRenewFromClAccountManagement(long clPostingId)
        {
            var context = new whitmanenterprisecraigslistEntities()
            {
                CommandTimeout = 18000
            };

                var searchResult =
                    context.vincontrolcraigslisttrackings.FirstOrDefault(x => x.CLPostingId == clPostingId);

                if (searchResult != null)
                {
                    searchResult.AddedDate = DateTime.Now;

                    searchResult.ExpirationDate = DateTime.Now.AddDays(2).AddHours(6);

                    searchResult.ShowAd = true;

                    searchResult.Renew = true;

                    searchResult.CheckDate = DateTime.Now;

                    context.SaveChanges();
                    
                }



            context.Dispose();

        }

        public static void PinkAdInClAccountManagement(List<long> clPostingIdList)
        {

            var context = new whitmanenterprisecraigslistEntities()
            {
                CommandTimeout = 18000
            };

            foreach (var clPostingId in clPostingIdList)
            {


                    var searchResult =
                        context.vincontrolcraigslisttrackings.FirstOrDefault(x => x.CLPostingId == clPostingId);

                    if (searchResult != null)
                    {


                        searchResult.ShowAd = false;

                        searchResult.Renew = false;

                        searchResult.CLPostingId = 0;

                        searchResult.CheckDate = DateTime.Now;
                    }


              
            }
            context.SaveChanges();
            context.Dispose();

        }

    }
}
