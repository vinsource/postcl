using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;
using iTextSharp.text.pdf.qrcode;
using Vinclapp.Craigslist;
using VinCLAPP.DatabaseModel;
using VinCLAPP.Forms;
using VinCLAPP.Model;
using vincontrol.Application.Forms.InventoryManagement;
using vincontrol.Application.Forms.VehicleLogManagement;
using vincontrol.Constant;
using Account = VinCLAPP.DatabaseModel.Account;
using City = VinCLAPP.Model.City;
using Dealer = VinCLAPP.Model.Dealer;

namespace VinCLAPP.Helper
{
    public sealed class DataHelper
    {
      
        public static void InitializeGlobalInventoryVariable(int dealerId)
        {
            var context = new CLDMSEntities();

            var dtInventory =
                context.Inventories.Where(x => x.DealerId == dealerId).ToList();

            var date30DaysCompare = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-30);
            var dtAccount =
                context.Accounts.FirstOrDefault(x => x.AccountId == GlobalVar.CurrentAccount.AccountId);

            var trackingList =
                context.Trackings.Where(x => x.DealerId == GlobalVar.CurrentDealer.DealerId && x.AddedDate > date30DaysCompare)
                    .OrderByDescending(x => x.AddedDate);

            var fullTracking = new List<ShortTrackingAds>();

            var hashSet = new HashSet<int>();

            foreach (var tmp in trackingList)
            {
                var uniqueId = Convert.ToInt32(tmp.ListingId + "" + tmp.CityId);
              
                var record = new ShortTrackingAds()
                {
                    ListingId = tmp.ListingId.GetValueOrDefault(),
                    CityId = tmp.CityId.GetValueOrDefault(),
                    CreatedDate = tmp.AddedDate.GetValueOrDefault(),
                    TotalAds = 1
                };
                
                if (!hashSet.Contains(uniqueId))
                {
                    fullTracking.Add(record);
                    hashSet.Add(uniqueId);
                }
                else
                {
                    fullTracking.FirstOrDefault(x => x.ListingId == tmp.ListingId && x.CityId == tmp.CityId).TotalAds++;
                }

                
            }



            var dtCityList =context.SelectedCities.Where(x => x.AccountId == GlobalVar.CurrentAccount.AccountId) ;
       
            if (dtAccount != null)
            {
                GlobalVar.TrialInfo = new TrialInfo
                    {
                        IsTrial = dtAccount.Trial ?? true,
                        StartUsingTime = dtAccount.TrialStartDate
                    };
            }
            GlobalVar.CurrentDealer.CityList = new List<City>();

            GlobalVar.CurrentDealer.Inventory = new List<VehicleInfo>();

            if (dtCityList.Any())
            {
                
                var firstCity = dtCityList.FirstOrDefault(x => x.CityId == dtAccount.FirstCity);
                
                var restCity = dtCityList.Where(x => x.CityId != dtAccount.FirstCity);

                GlobalVar.CurrentDealer.CityList.Add(new Model.City()
                {
                    CityID = firstCity.CityId,
                    CityName = firstCity.City.CityName,
                    CraigsListCityURL = firstCity.City.CraigsListCityURL,
                    SubCity = firstCity.City.SubCity.Value==1,
                    CLIndex = firstCity.City.CLIndex.Value,
                    isCurrentlyUsed = false,
                    Position = GlobalVar.CurrentDealer.CityList.Count + 1,
                    AreaAbbr = firstCity.City.AreaAbbr,
                    SubAbbr = firstCity.City.SubAbbr
                });

                foreach (var tmp in restCity)
                {
                    var city = new City
                    {
                        CityID = tmp.CityId,
                        CityName = tmp.City.CityName,
                        CraigsListCityURL = tmp.City.CraigsListCityURL,
                        SubCity = tmp.City.SubCity.Value==1,
                        CLIndex = tmp.City.CLIndex.Value,
                        isCurrentlyUsed = false,
                        Position = GlobalVar.CurrentDealer.CityList.Count + 1,
                        AreaAbbr = tmp.City.AreaAbbr,
                        SubAbbr = tmp.City.SubAbbr
                    };

                    GlobalVar.CurrentDealer.CityList.Add(city);
                }
                if (GlobalVar.CurrentDealer.CityList.Count > 0)
                    GlobalVar.CurrentDealer.CityList.First().isCurrentlyUsed = true;
            }

           

            if (dtCityList.Any())
            {

                foreach (var cityTmp in dtCityList)
                {
                    foreach (var tmp in dtInventory)
                    {
                        var vehicle = new VehicleInfo
                            {
                              
                                ListingId = tmp.ListingID,
                                StockNumber = String.IsNullOrEmpty(tmp.StockNumber) ? "" : tmp.StockNumber,
                                Vin = String.IsNullOrEmpty(tmp.VINNumber) ? "" : tmp.VINNumber,
                                ModelYear = String.IsNullOrEmpty(tmp.ModelYear) ? "" : tmp.ModelYear,
                                Make = String.IsNullOrEmpty(tmp.Make) ? "" : tmp.Make,
                                Model = String.IsNullOrEmpty(tmp.Model) ? "" : tmp.Model,
                                Trim = String.IsNullOrEmpty(tmp.Trim) ? "" : tmp.Trim,
                                Cylinder = String.IsNullOrEmpty(tmp.Cylinders) ? "" : tmp.Cylinders,
                                BodyType = String.IsNullOrEmpty(tmp.BodyType) ? "" : tmp.BodyType,
                              
                                ExteriorColor = String.IsNullOrEmpty(tmp.ExteriorColor) ? "" : tmp.ExteriorColor,
                                InteriorColor = String.IsNullOrEmpty(tmp.InteriorColor) ? "" : tmp.InteriorColor,
                                Mileage = String.IsNullOrEmpty(tmp.Mileage) ? "" : tmp.Mileage,
                                Description = String.IsNullOrEmpty(tmp.Descriptions) ? "" : tmp.Descriptions,
                                CarImageUrl = String.IsNullOrEmpty(tmp.CarImageUrl) ? "" : tmp.CarImageUrl,
                                Door = String.IsNullOrEmpty(tmp.Doors) ? "" : tmp.Doors,
                                Fuel = String.IsNullOrEmpty(tmp.FuelType) ? "" : tmp.FuelType,
                                Litters = String.IsNullOrEmpty(tmp.Liters) ? "" : tmp.Liters,
                                Tranmission = String.IsNullOrEmpty(tmp.Tranmission) ? "" : tmp.Tranmission,
                                WheelDrive = String.IsNullOrEmpty(tmp.DriveTrain) ? "" : tmp.DriveTrain,
                                Engine = String.IsNullOrEmpty(tmp.EngineType) ? "" : tmp.EngineType,
                                Options = String.IsNullOrEmpty(tmp.CarsOptions) ? "" : tmp.CarsOptions,
                                DefaultImageUrl = String.IsNullOrEmpty(tmp.DefaultImageUrl) ? "" : tmp.DefaultImageUrl,
                                PostingCityId = cityTmp.City.CityID,
                                DealerId = GlobalVar.CurrentDealer.DealerId,
                                CraigslistCityUrl = cityTmp.City.CraigsListCityURL,
                                CityIndex = cityTmp.City.CLIndex.GetValueOrDefault(),
                                SubAbr = cityTmp.City.SubAbbr,
                                DealershipName = GlobalVar.CurrentDealer.DealershipName,
                                PhoneNumber = GlobalVar.CurrentDealer.PhoneNumber,
                                StreetAddress = GlobalVar.CurrentDealer.StreetAddress,
                                City = GlobalVar.CurrentDealer.City,
                                State = GlobalVar.CurrentDealer.State,
                                ZipCode = GlobalVar.CurrentDealer.ZipCode,
                                VinListingId=tmp.VincontrolListingId.GetValueOrDefault()
                                

                            };

                        var lastDateStamp =
                            fullTracking.FirstOrDefault(x => x.CityId ==vehicle.PostingCityId && x.ListingId ==vehicle.ListingId);



                        if (lastDateStamp != null)
                        {
                            var dateCompare = new DateTime(lastDateStamp.CreatedDate.Year,
                                lastDateStamp.CreatedDate.Month, lastDateStamp.CreatedDate.Day);
                            var subtractedDay = DateTime.Now.Subtract(dateCompare).Days;


                            vehicle.LastPosted = subtractedDay;


                            vehicle.TotalAds = lastDateStamp.TotalAds;
                        }
                        else
                        {
                            vehicle.LastPosted = -1;
                        }



                        int salePrice = 0;
                        Int32.TryParse(tmp.SalePrice,out salePrice);
                        vehicle.SalePrice = salePrice;

                        if (!String.IsNullOrEmpty(vehicle.CarImageUrl))
                        {
                            string[] totalImages = vehicle.CarImageUrl.Split(new[] { "|", "," },
                                                                             StringSplitOptions.RemoveEmptyEntries);

                            vehicle.FirstImageUrl = totalImages.First();

                            vehicle.Pictures = totalImages.Count();

                        }
                        else
                        {
                            vehicle.FirstImageUrl = vehicle.DefaultImageUrl;

                            vehicle.Pictures = 0;
                        }

                        if(vehicle.Pictures > 0)
                            GlobalVar.CurrentDealer.Inventory.Add(vehicle);
                    }
                }
            }
            else
            {
                foreach (var tmp in dtInventory)
                {
                    var vehicle = new VehicleInfo
                    {
                      
                        ListingId = tmp.ListingID,
                        StockNumber = String.IsNullOrEmpty(tmp.StockNumber) ? "" : tmp.StockNumber,
                        Vin = String.IsNullOrEmpty(tmp.VINNumber) ? "" : tmp.VINNumber,
                        ModelYear = String.IsNullOrEmpty(tmp.ModelYear) ? "" : tmp.ModelYear,
                        Make = String.IsNullOrEmpty(tmp.Make) ? "" : tmp.Make,
                        Model = String.IsNullOrEmpty(tmp.Model) ? "" : tmp.Model,
                        Trim = String.IsNullOrEmpty(tmp.Trim) ? "" : tmp.Trim,
                        Cylinder = String.IsNullOrEmpty(tmp.Cylinders) ? "" : tmp.Cylinders,
                        BodyType = String.IsNullOrEmpty(tmp.BodyType) ? "" : tmp.BodyType,
                       
                        ExteriorColor = String.IsNullOrEmpty(tmp.ExteriorColor) ? "" : tmp.ExteriorColor,
                        InteriorColor = String.IsNullOrEmpty(tmp.InteriorColor) ? "" : tmp.InteriorColor,
                        Mileage = String.IsNullOrEmpty(tmp.Mileage) ? "" : tmp.Mileage,
                        Description = String.IsNullOrEmpty(tmp.Descriptions) ? "" : tmp.Descriptions,
                        CarImageUrl = String.IsNullOrEmpty(tmp.CarImageUrl) ? "" : tmp.CarImageUrl,
                        Door = String.IsNullOrEmpty(tmp.Doors) ? "" : tmp.Doors,
                        Fuel = String.IsNullOrEmpty(tmp.FuelType) ? "" : tmp.FuelType,
                        Litters = String.IsNullOrEmpty(tmp.Liters) ? "" : tmp.Liters,
                        Tranmission = String.IsNullOrEmpty(tmp.Tranmission) ? "" : tmp.Tranmission,
                        WheelDrive = String.IsNullOrEmpty(tmp.DriveTrain) ? "" : tmp.DriveTrain,
                        Engine = String.IsNullOrEmpty(tmp.EngineType) ? "" : tmp.EngineType,
                        Options = String.IsNullOrEmpty(tmp.CarsOptions) ? "" : tmp.CarsOptions,
                        DefaultImageUrl = String.IsNullOrEmpty(tmp.DefaultImageUrl) ? "" : tmp.DefaultImageUrl,
                        DealerId = GlobalVar.CurrentDealer.DealerId,
                        DealershipName = GlobalVar.CurrentDealer.DealershipName,
                        PhoneNumber = GlobalVar.CurrentDealer.PhoneNumber,
                        StreetAddress = GlobalVar.CurrentDealer.StreetAddress,
                        City = GlobalVar.CurrentDealer.City,
                        State = GlobalVar.CurrentDealer.State,
                        ZipCode = GlobalVar.CurrentDealer.ZipCode,
                        PostingCityId = 0,
                        VinListingId = tmp.VincontrolListingId.GetValueOrDefault()
                   

                    };

                    var lastDateStamp =
                           fullTracking.FirstOrDefault(x => x.CityId == vehicle.PostingCityId && x.ListingId == vehicle.ListingId);

                    if (lastDateStamp != null)
                    {
                         var dateCompare = new DateTime(lastDateStamp.CreatedDate.Year, lastDateStamp.CreatedDate.Month, lastDateStamp.CreatedDate.Day);
                         var subtractedDay = DateTime.Now.Subtract(dateCompare).Days;

                      
                         vehicle.LastPosted = subtractedDay;
                      
                        
                        vehicle.TotalAds = lastDateStamp.TotalAds;
                    }
                    else
                    {
                        vehicle.LastPosted = -1;
                    }



                    if (!String.IsNullOrEmpty(vehicle.CarImageUrl))
                    {
                        string[] totalImages = vehicle.CarImageUrl.Split(new[] {"|", ","},
                                                                         StringSplitOptions.RemoveEmptyEntries);

                        vehicle.FirstImageUrl = totalImages.First();

                        vehicle.Pictures = totalImages.Count();

                    }
                    else
                    {
                        vehicle.FirstImageUrl = vehicle.DefaultImageUrl;

                        vehicle.Pictures = 0;
                    }
                    int salePrice = 0;
                    Int32.TryParse(tmp.SalePrice, out salePrice);
                    vehicle.SalePrice = salePrice;

                    if (vehicle.Pictures > 0)
                        GlobalVar.CurrentDealer.Inventory.Add(vehicle);
                }
            }

            GlobalVar.CurrentDealer.Inventory = GlobalVar.CurrentDealer.Inventory.OrderByDescending(x => x.LastPosted).ToList();

            var index = 1;
            foreach (var tmp in GlobalVar.CurrentDealer.Inventory)
            {
                tmp.AutoId = index++;
            }

            GlobalVar.CurrentDealer.SimpleInventory = new List<SimpleVehicleInfo>();

            foreach (var tmp in GlobalVar.CurrentDealer.Inventory)
            {
                var vehicle = new SimpleVehicleInfo
                {
                    ListingId = tmp.ListingId,
                    AutoId = tmp.AutoId,
                    Year = String.IsNullOrEmpty(tmp.ModelYear) ? "" : tmp.ModelYear,
                    Stock = String.IsNullOrEmpty(tmp.StockNumber) ? "" : tmp.StockNumber,
                    Vin = String.IsNullOrEmpty(tmp.Vin) ? "" : tmp.Vin,
                    Make = String.IsNullOrEmpty(tmp.Make) ? "" : tmp.Make,
                    Model = String.IsNullOrEmpty(tmp.Model) ? "" : tmp.Model,
                    Trim = String.IsNullOrEmpty(tmp.Trim) ? "" : tmp.Trim,
                    SalePrice = tmp.SalePrice,
                    ExteriorColor = String.IsNullOrEmpty(tmp.ExteriorColor) ? "" : tmp.ExteriorColor,
                    Mileage = String.IsNullOrEmpty(tmp.Mileage) ? "" : tmp.Mileage,
                    PostingCity =
                        tmp.PostingCityId == 0
                            ? ""
                            : GlobalVar.CurrentDealer.CityList.First(x => x.CityID == tmp.PostingCityId).CityName,
                    PostingCityId = tmp.PostingCityId,
                    Pictures = tmp.Pictures,
                    TotalAds = tmp.TotalAds

                };


                if (tmp.LastPosted == 0)
                {
                    vehicle.LastPosted = "Today";
                }
                if (tmp.LastPosted == 1)
                {
                    vehicle.LastPosted = "1 day";
                }
                if (tmp.LastPosted > 1)
                {
                    vehicle.LastPosted = tmp.LastPosted + " days";
                }


                GlobalVar.CurrentDealer.SimpleInventory.Add(vehicle);
            }


        }

        public static void InitializeGlobalDealerInfoVariable(int dealerId)
        {
            var context = new CLDMSEntities();

            DatabaseModel.Dealer dealer = context.Dealers.FirstOrDefault(x => x.DealerId == dealerId);

            GlobalVar.CurrentDealer = new Dealer
                {
                    DealerId = dealer.DealerId,
                    VincontrolId = dealer.DealerId,
                    DealershipName = dealer.Name,
                    PhoneNumber = dealer.Phone,
                    StreetAddress = dealer.StreetAddress,
                    City = dealer.City,
                    State = dealer.State,
                    ZipCode = dealer.ZipCode,
                    LogoUrl = dealer.LogoURL,
                    WebSiteUrl = dealer.WebSiteURL,
                    CreditUrl = dealer.CreditURL,
                    LeadEmail = dealer.LeadEmail,
                    CityOveride = dealer.CityOveride,
                    DealerTitle = dealer.DealerTitle,
                    EndingSentence = dealer.EndingSentence,
                    EmailFormat = dealer.EmailFormat.GetValueOrDefault(),
                    PostWithPrice = dealer.PostWithPrice.GetValueOrDefault(),
                    DelayTimer = dealer.DelayTimer.GetValueOrDefault(),
                    
                    
                };
        }

        public static void InitializeGlobalEmailAccountVariable(int accountId)
        {
            var context = new CLDMSEntities();

            var emailAccountList =
                context.Emails.Where(x => x.AccountAutoId == accountId);

            GlobalVar.CurrentDealer.EmailAccountList = new List<EmailAccount>();

            foreach (var email in emailAccountList.OrderBy(i => i.DateAdded))
            {
                var tmp = new EmailAccount
                    {
                        CraigslistAccount = email.EmailAddress,
                        CraigsListPassword = email.EmailPassword,
                        CraigsAccountPhoneNumber = email.PhoneNumber,
                        IsCurrentlyUsed = false,
                        IntervalofAds =
                            Convert.ToInt32(
                                ConfigurationManager.AppSettings["IntervalOfAds"].ToString(CultureInfo.InvariantCulture)),
                        DealerId = email.DealerId.GetValueOrDefault(),
                        Position = GlobalVar.CurrentDealer.EmailAccountList.Count + 1,
                    };

                GlobalVar.CurrentDealer.EmailAccountList.Add(tmp);
            }

            if (GlobalVar.CurrentDealer.EmailAccountList.Count > 0)
                GlobalVar.CurrentDealer.EmailAccountList.First().IsCurrentlyUsed = true;
         
        }

        public static bool CheckDealerExist(int dealerId)
        {
            using (var context = new CLDMSEntities())
            {
                if (context.Dealers.Any(x => x.DealerId == dealerId))
                    return true;
            }
            return false;
        }

        public static int CheckDealerNameExist(string dealerName)
        {
            using (var context = new CLDMSEntities())
            {
                if (context.Dealers.Any(x => x.Name.ToUpper().Equals(dealerName.ToLower())))
                    return context.Dealers.First(x => x.Name.ToUpper().Equals(dealerName.ToLower())).DealerId;
            }
            return 0;
        }
        public static bool CheckAccountNameExist(string accountName)
        {
            using (var context = new CLDMSEntities())
            {
                if (context.Accounts.Any(x => x.AccountName.ToLower().Equals(accountName.ToLower())))
                    return true;
            }
            return false;
        }

        public static PostClCustomer GetCustomerFromAccountName(string accountName)
        {
            var customer = new PostClCustomer();
            using (var context = new CLDMSEntities())
            {
                var findCustomer =
                    context.Accounts.First(x => x.AccountName.ToLower().Equals(accountName.ToLower()));

                if (findCustomer != null)
                {
                    customer = new PostClCustomer()
                        {
                            CustomerEmail = findCustomer.AccountName,
                            TemporaryPassword = findCustomer.AccountPassword,
                            
                        };
                }
            }
            return customer;
        }


        public static PostClCustomer ChangeTemporayPassword(string accountName)
        {
            var customer = new PostClCustomer();
            using (var context = new CLDMSEntities())
            {
                var findCustomer =
                    context.Accounts.First(x => x.AccountName.ToLower().Equals(accountName.ToLower()));

              

                if (findCustomer != null)
                {
                    findCustomer.AccountPassword = RandomPassword.Generate(10);
                    findCustomer.IsFirstLogOn = true;

                    context.SaveChanges();
                    customer = new PostClCustomer()
                    {
                        CustomerEmail = findCustomer.AccountName,
                        TemporaryPassword = findCustomer.AccountPassword,

                    };

                    
                }
            }
            return customer;
        }

        public static bool AddNewDealer(PostClCustomer customer)
        {
            var dealerId = CheckDealerNameExist(customer.DealerName.Trim());
            bool success = false;
            if (dealerId > 0)
            {
                using (var context = new CLDMSEntities())
                {
                    customer.TemporaryPassword = RandomPassword.Generate(10);
                    var account = new Account()
                    {
                        AccountName = customer.CustomerEmail,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        PersonalPhone = customer.CustomerPhone,
                        AccountPassword =   customer.TemporaryPassword,
                        Active = true,
                        DateCreated = DateTime.Now,
                        DealerId = dealerId,
                        LastUpdated = DateTime.Now,
                        IsFirstLogOn = true,
                        Quota = 400,
                        Trial = true,
                        TrialStartDate = DateTime.Now,
                        QuickBookAccountName = customer.QuickBookAccountName,
                        QuickbookAccountId = customer.QuickBookAccountId,
                        DailyLimit = customer.DailyLimit

                    };
                    context.Accounts.Add(account);

                    context.SaveChanges();

                    return true;
                }
            }
            else
            {
                using (var context = new CLDMSEntities())
                {

                    using (var scope = new TransactionScope())
                    {
                        try
                        {
                            var newDealer = new DatabaseModel.Dealer()
                                {
                                    Active = true,
                                    City = customer.DealerCity,
                                    DateAdded = DateTime.Now,
                                    LastUpdated = DateTime.Now,
                                    Name = customer.DealerName,
                                    LeadEmail = customer.LeadEmail,
                                    EmailFormat = customer.LeadFormat,
                                    Phone = customer.DealerPhone,
                                    StreetAddress = customer.DealerStreetAddress,
                                    State = customer.DealerState,
                                    ZipCode = customer.DealerZipCode,
                                    WebSiteURL = customer.WebSiteAddress,


                                };

                            context.Dealers.Add(newDealer);

                            context.SaveChanges();

                            customer.TemporaryPassword = RandomPassword.Generate(10);
                            
                            var account = new Account()
                                {
                                    AccountName = customer.CustomerEmail,
                                    AccountPassword = customer.TemporaryPassword,
                                    FirstName = customer.FirstName,
                                    LastName = customer.LastName,
                                    PersonalPhone = customer.CustomerPhone,
                                    Active = true,
                                    DateCreated = DateTime.Now,
                                    DealerId = newDealer.DealerId,
                                    LastUpdated = DateTime.Now,
                                    IsFirstLogOn = true,
                                    Quota = 400,
                                    Trial = false,
                                    TrialStartDate = DateTime.Now,
                                    QuickBookAccountName = customer.QuickBookAccountName,
                                    QuickbookAccountId = customer.QuickBookAccountId,
                                    DailyLimit = customer.DailyLimit
                                };



                            context.Accounts.Add(account);

                            context.SaveChanges();

                            scope.Complete();

                            success = true;
                        }
                        catch (Exception)
                        {
                            return success;
                        }


                    }
                   

                }
                return success;
            }

        }



        public static void UpdateDealerInfo(PostClCustomer customer)
        {
               using (var context = new CLDMSEntities())
               {
                   var findDealer =
                       context.Dealers.First(x => x.DealerId == GlobalVar.CurrentDealer.DealerId);

                   if (findDealer != null)
                   {

                       findDealer.WebSiteURL = customer.WebSiteAddress;

                       findDealer.Phone = customer.DealerPhone;

                       findDealer.LeadEmail = customer.LeadEmail;
                   }
                   var findAccount =
                       context.Accounts.First(x => x.DealerId == GlobalVar.CurrentAccount.AccountId);
                   if (findAccount != null)
                   {

                       findAccount.DailyLimit = customer.DailyLimit;

                       GlobalVar.CurrentAccount.DailyLimit = customer.DailyLimit;

                   }


                   context.SaveChanges();
               }

            

        }

        public static bool CheckEmailAccountExist(int dealerId)
        {
            using (var context = new CLDMSEntities())
            {
                if (context.Accounts.Any(o => o.AccountId == dealerId))
                    return true;
            }
            return false;
        }

        public static void AddListEmailAccount(List<Email> emailList)
        {
            using (var context = new CLDMSEntities())
            {
                foreach (Email email in emailList)
                    context.Emails.Add(email);

                context.SaveChanges();
            }
        }

        public static void AddEmailAccount(Email email)
        {
            using (var context = new CLDMSEntities())
            {
                context.Emails.Add(email);

                context.SaveChanges();
            }
        }

        public static void DeleteAllEmailAccount(int accountId)
        {
            using (var context = new CLDMSEntities())
            {
                IQueryable<Email> result =
                    context.Emails.Where(x => x.AccountAutoId == accountId);

                foreach (Email acct in result)
                {
                    context.Emails.Remove(acct);
                }
                context.SaveChanges();
            }
        }
        
        public static int GetDealerIdFromLogin(string username, string password)
        {
            var context = new CLDMSEntities();

            var result =
                context.Accounts.FirstOrDefault(
                    o => o.AccountName == username.Trim() && o.AccountPassword == password.Trim());

            GlobalVar.CurrentAccount = new Model.Account()
                {
                    AccountId = result.AccountId,
                     FirstName = result.FirstName,
                     LastName = result.LastName,
                     PersonalPhone = result.PersonalPhone,
                    AccountName = result.AccountName,
                    AccountPassword = result.AccountPassword,
                    Active = result.Active.GetValueOrDefault(),
                    DealerId = result.DealerId.GetValueOrDefault(),
                    IsCurrentlyUsed = true,
                    Quota = result.Quota.GetValueOrDefault(),
                    QuickBookAccountName = result.QuickBookAccountName,
                    QuickBookAccountId = result.QuickbookAccountId
                };

            return
                context.Accounts.FirstOrDefault(
                    o => o.AccountName == username.Trim() && o.AccountPassword == password.Trim()).DealerId.Value;
        }

        public static void AddNewTracking(CraigsListTrackingModel clModel)
        {
            try
            {
                long cLPostingId =
                    Convert.ToInt64(clModel.HtmlCraigslistUrl.Substring(
                        clModel.HtmlCraigslistUrl.LastIndexOf("/", StringComparison.Ordinal) + 1).Replace(
                            ".html", ""));

                var context = new CLDMSEntities();

                var clTracking = new Tracking()
                    {
                        CityId = clModel.CityId,
                        DealerId = clModel.DealerId,
                        EmailAccount = clModel.EmailAccount,
                        ExpirationDate = DateTime.Now.AddDays(30),
                        AccountId = GlobalVar.CurrentAccount.AccountId,
                        AddedDate = DateTime.Now,
                        ListingId = clModel.ListingId,
                        Computer = CommonHelper.GetIp() + " " +CommonHelper.GetComputerName(),
                        AdsUrl = clModel.HtmlCraigslistUrl,
                        CLPostingId = cLPostingId
                    };

                context.Trackings.Add(clTracking);

                context.SaveChanges();
               
                if (clModel.VinListingId > 0)
                {
                    var vehicleLogForm = new VehicleLogManagementForm();
                    var inventoryManagementForm = new InventoryManagementForm();

                    var inventory = inventoryManagementForm.GetInventory(clModel.VinListingId);

                    if (inventory!= null)
                    {
                      vehicleLogForm.AddVehicleLog(clModel.VinListingId, null,
                              Constanst.VehicleLogSentence.CraigslistByCldms.
                              Replace("USER", GlobalVar.CurrentAccount.FirstName + " " + GlobalVar.CurrentAccount.LastName).
                              Replace("CITY", clModel.CityName), null);
                    }

                }

            
              
            }
            catch (Exception ex)
            {
            
            }
        }

        public static void AddNewTrackingFromApi(CraigsListTrackingModel clModel)
        {
            try
            {
                var context = new CLDMSEntities();

                var clTracking = new Tracking()
                {
                    CityId = clModel.CityId,
                    DealerId = clModel.DealerId,
                    EmailAccount = clModel.EmailAccount,
                 
                    ExpirationDate = DateTime.Now.AddDays(30),
                    AccountId = GlobalVar.CurrentAccount.AccountId,
                    AddedDate = DateTime.Now,
                    ListingId = clModel.ListingId,
                    Computer = CommonHelper.GetIp() + " " + CommonHelper.GetComputerName(),
                    AdsUrl = clModel.HtmlCraigslistUrl,
                    CLPostingId = clModel.ClPostingId
                };

                context.Trackings.Add(clTracking);

                context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        public static void UpdateCurrentTracking(int trackingId, string htmlLink)
        {
            try
            {
                var context = new CLDMSEntities();

                Tracking searchResult =
                    context.Trackings.First(x => x.TrackingId == trackingId);

                long cLPostingId =
                    Convert.ToInt64(htmlLink.Substring(
                        htmlLink.LastIndexOf("/", StringComparison.Ordinal) + 1).Replace(
                            ".html", ""));

                searchResult.AdsUrl = htmlLink;

                searchResult.CLPostingId = cLPostingId;

                context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        internal static void RemoveVehicleInfo(List<int> selectedItem)
        {
            if (selectedItem != null && selectedItem.Any())
            {
                var context = new CLDMSEntities();

                IEnumerable<Inventory> query =
                    context.Inventories.ToList().Where(i => selectedItem.Contains(i.ListingID));

                foreach (var obj in query.ToList())
                {
                    context.Inventories.Remove(obj);
                }

                context.SaveChanges();
            }
        }
        
        public static bool CheckAccountExist(string username, string password)
        {
            using (var context = new CLDMSEntities())
            {
                if (
                    context.Accounts.Any(
                        o =>
                        o.AccountName == username.Trim() && o.AccountPassword == password.Trim() && o.Active == true))
                    return true;
            }
            return false;
        }

        public static bool CheckAccountExistButNotActive(string username, string password)
        {
            using (var context = new CLDMSEntities())
            {
                if (
                    context.Accounts.Any(
                        o =>
                        o.AccountName == username.Trim() && o.AccountPassword == password.Trim() && o.Active == false))
                    return true;
            }
            return false;
        }

        public static bool CheckSingleLogOn(int accountId)
        {
            using (var context = new CLDMSEntities())
            {
                string currentIp = String.Format("{0} {1}", CommonHelper.GetIp(), CommonHelper.GetComputerName());
                var user =
                    context.Accounts.FirstOrDefault(
                        o =>
                        o.AccountId == accountId && o.Active == true);
                if (user != null)
                {
                    if (String.IsNullOrEmpty(user.Ip) || (user.Ip.Equals(currentIp)))
                    {
                        user.Ip = currentIp;
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public static void UpdateIpForSingleLogOn(int accountId)
        {
            using (var context = new CLDMSEntities())
            {
                string currentIp = String.Format("{0} {1}", CommonHelper.GetIp(), CommonHelper.GetComputerName());
                var user =
                    context.Accounts.FirstOrDefault(
                        o =>
                        o.AccountId==accountId);
                if (user != null)
                {
                    user.Ip = currentIp;
                    context.SaveChanges();
                }
            }
        }

        public static void ResetIpForSingleLogOn(int accountId)
        {
            using (var context = new CLDMSEntities())
            {
                var user =
                    context.Accounts.FirstOrDefault(
                        o =>
                        o.AccountId == accountId && o.Active == true);
                if (user != null)
                {
                    user.Ip = string.Empty;
                    context.SaveChanges();
                }
            }
        }

        public static string GenerateTempPassword()
        {
            return Guid.NewGuid().ToString().Substring(0, 8);
        }

        public static void AddNewUserWithTempPassword(string accountName, int accountNumber, int dealerId, int firstCity, bool isTrial)
        {
            using (var context = new CLDMSEntities())
            {
                var existingUser = context.Accounts.FirstOrDefault(i => i.AccountName==accountName && i.DealerId == dealerId);
                if (existingUser == null)
                {
                    var newUser = new Account()
                                      {
                                          AccountName = accountName,
                                          AccountPassword = GenerateTempPassword(),
                                          DealerId = dealerId,
                                          DateCreated = DateTime.Now,
                                          LastUpdated = DateTime.Now,
                                          Active = true,
                                          Quota = 300,
                                          FirstCity = firstCity,
                                          IsFirstLogOn = true,
                                          Trial = isTrial,
                                          TrialStartDate = DateTime.Now
                                      };
                    context.Accounts.Add(newUser);
                    context.SaveChanges();
                }
            }
        }

        public static Model.Account CheckAccountStatus(string accountName, string accountPassword)
        {
            using (var context = new CLDMSEntities())
            {

                var account = new Model.Account();
                
                var existingUser =
                    context.Accounts.FirstOrDefault(
                        i =>
                        i.AccountName==accountName &&i.AccountPassword==accountPassword);
                
                if (existingUser != null)
                {
                    account = new Model.Account()
                        {
                            DealerId = existingUser.DealerId.GetValueOrDefault(),
                            LastName = existingUser.LastName,
                            FirstName = existingUser.FirstName,
                            PersonalPhone = existingUser.PersonalPhone,
                            AccountName = existingUser.AccountName,
                            AccountPassword = existingUser.AccountPassword,
                            AccountId = existingUser.AccountId,
                            Quota = existingUser.Quota.GetValueOrDefault(),
                            Active = existingUser.Active.GetValueOrDefault(),
                            IsExist = true,
                            TrialStartDate = existingUser.TrialStartDate.GetValueOrDefault(),
                            IsTrial = existingUser.Trial.GetValueOrDefault(),
                            SingleLogon = true,
                            DatabaseIp = existingUser.Ip,
                            QuickBookAccountId = existingUser.QuickbookAccountId,
                            QuickBookAccountName = existingUser.QuickBookAccountName,
                            DailyLimit = existingUser.DailyLimit.GetValueOrDefault(),
                            APIAccountId = existingUser.APIAccountId,
                            APIPassword = existingUser.APIPassword,
                            APIUsername = existingUser.APIUsername,
                        };

                    if (existingUser.IsFirstLogOn.HasValue && existingUser.IsFirstLogOn == true)
                    {
                        account.IsFirstLogon = true;
                    }
                  
           

                }
                else
                {
                    account.IsExist = false;
                }
                GlobalVar.CurrentAccount = account;

                return account;
            }
        }

        public static bool CheckFirstLogOn(string accountName, string accountPassword)
        {
            using (var context = new CLDMSEntities())
            {
                var existingUser = context.Accounts.FirstOrDefault(i => i.AccountName.ToLower().Equals(accountName.ToLower()) && i.AccountPassword.Equals(accountPassword));
                if (existingUser != null)
                {
                    return existingUser.IsFirstLogOn.GetValueOrDefault() ;
                }

                return false;
            }
        }

        public static void RemoveTrialPeriod(BillingCustomer customer)
        {
            using (var context = new CLDMSEntities())
            {
                var existingUser =
                    context.Accounts.First(x => x.AccountId == GlobalVar.CurrentAccount.AccountId);
                
                if (existingUser != null)
                {
                    existingUser.Trial = false;

                    if (customer.SelectedPackage.Period.Equals("1 month"))
                    {
                        existingUser.ExpirationDate = DateTime.Now.AddDays(30);
                        existingUser.DataFeedSetUp = customer.OneTimeSetUpFeed;
                    }

                    else if (customer.SelectedPackage.Period.Equals("6 months"))
                    {
                        existingUser.ExpirationDate = DateTime.Now.AddDays(180);
                        existingUser.DataFeedSetUp = true;
                    }

                    else if (customer.SelectedPackage.Period.Equals("1 year"))
                    {
                        existingUser.ExpirationDate = DateTime.Now.AddDays(365);
                        existingUser.DataFeedSetUp = true;
                    }

                    context.SaveChanges();
                }

               
            }
        }

       
        public static List<CraigsListTrackingModel> PopulateEmailHavingRenewAds()
        {
            var context = new CLDMSEntities();
           
            var bufferMasterVehicleList= new List<CraigsListTrackingModel>();

            var checkList = from a in context.Trackings
                            from b in context.Emails
                            where
                                a.EmailAccount == b.EmailAddress && a.CLPostingId > 0  && a.AccountId==GlobalVar.CurrentAccount.AccountId
                            select new
                            {
                                a.TrackingId,
                                a.ListingId,
                                a.CityId,
                                a.CLPostingId,
                                a.EmailAccount,
                                a.DealerId,
                                b.EmailPassword,
                                b.PhoneNumber,
                                a.AddedDate,
                                a.ExpirationDate,
                                a.AdsUrl
                            };

            var hashSet = new HashSet<string>();

            foreach (var ads in checkList.OrderBy(x => x.EmailAccount))
            {
                if (!hashSet.Contains(ads.EmailAccount))
                {
                    var addCar = new CraigsListTrackingModel()
                    {
                        AutoId = bufferMasterVehicleList.Count + 1,
                        EmailAccount = ads.EmailAccount,
                        EmailPassword = ads.EmailPassword,

                    };
                    bufferMasterVehicleList.Add(addCar);
                }

                hashSet.Add(ads.EmailAccount);
            }



            return bufferMasterVehicleList;
        }

        public static List<SimpleClTrackingModel> GetPostedAdsInToday(int accountId)
        {
            var context = new CLDMSEntities();
          

            var bufferMasterVehicleList = new List<SimpleClTrackingModel>();

            var dtToday=new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);

            var dtNextDay =new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day).AddDays(1);

            var checkList = from a in context.Trackings
                            where
                             a.CLPostingId > 0 && a.AddedDate >= dtToday && a.AddedDate < dtNextDay && a.AccountId == accountId
                            select new
                            {
                                a.TrackingId,
                                a.ListingId,
                                a.CityId,
                                a.CLPostingId,
                                a.AdsUrl,
                              
                            };

            var cityList = context.Cities.ToList();

            foreach (var ads in checkList)
            {
                var findCar = GlobalVar.CurrentDealer.Inventory.FirstOrDefault(x => x.ListingId == ads.ListingId);

                if (findCar != null)
                {
                    var addCar = new SimpleClTrackingModel()
                        {
                            ListingId = bufferMasterVehicleList.Count + 1,
                            Title = findCar.ModelYear + " " + findCar.Make + " " + findCar.Model + " " + findCar.Trim,
                            StockNumber = findCar.StockNumber,
                            HtmlCraigslistUrl = ads.AdsUrl,
                            CityName = cityList.FirstOrDefault(x => x.CityID == ads.CityId).CityName,
                            TrackingId = ads.TrackingId
                        };
                    bufferMasterVehicleList.Add(addCar);
                }

            }



            return bufferMasterVehicleList;
        }


        public static List<SimpleClTrackingModel> GetPostedAds(int listingId, int dealerId)
        {
            var context = new CLDMSEntities();
           

            var bufferMasterVehicleList = new List<SimpleClTrackingModel>();

            var dateCompare = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-30);
            
            var checkList = from a in context.Trackings
                            where
                             a.CLPostingId > 0 && a.ListingId == listingId && a.DealerId == dealerId && a.AddedDate > dateCompare
                            select new
                            {
                                a.TrackingId,
                                a.ListingId,
                                a.CityId,
                                a.CLPostingId,
                                a.AdsUrl,

                            };

            var cityList = context.Cities.ToList();

            foreach (var ads in checkList)
            {
                var findCar = GlobalVar.CurrentDealer.Inventory.FirstOrDefault(x => x.ListingId == ads.ListingId);

                if (findCar != null)
                {
                    var addCar = new SimpleClTrackingModel()
                    {
                        ListingId = ads.ListingId.GetValueOrDefault(),
                        Title = findCar.ModelYear + " " + findCar.Make + " " + findCar.Model + " " + findCar.Trim,
                        StockNumber = findCar.StockNumber,
                        HtmlCraigslistUrl = ads.AdsUrl,
                        CityId = ads.CityId.GetValueOrDefault(),
                        CityName = cityList.FirstOrDefault(x => x.CityID == ads.CityId).CityName,
                        TrackingId = ads.TrackingId
                    };
                    bufferMasterVehicleList.Add(addCar);
                }

            }



            return bufferMasterVehicleList;
        }

        public static Tracking GetAds(int trackingId)
        {
            var context = new CLDMSEntities();
          

            var searchResult =
                context.Trackings.FirstOrDefault(x => x.TrackingId == trackingId);


            return searchResult;
        }


        public static ShortEmailAccount GetEmail(string emailAccount)
        {
            var context = new CLDMSEntities();
           

            var searchResult =
                context.Emails.FirstOrDefault(x => x.EmailAddress == emailAccount);

            if (searchResult != null)
            {
                var returnResult = new ShortEmailAccount()
                {
                    Email = searchResult.EmailAddress,
                    Password = searchResult.EmailPassword
                };

                return returnResult;
            }
            var searchBulkResult =
                context.Accounts.FirstOrDefault(x => x.APIUsername == emailAccount);
            if (searchBulkResult != null)
            {
                var returnResult = new ShortEmailAccount()
                {
                    Email = searchBulkResult.APIUsername,
                    Password = searchBulkResult.APIPassword
                };

                return returnResult;
            }
            return null;

        }


        public static void ShowAdsButEligibleForRenew(int trackingAdId)
        {
            var context = new CLDMSEntities();
          

            var searchResult =
                           context.Trackings.FirstOrDefault(x => x.TrackingId == trackingAdId);

            if (searchResult != null)
            {
                searchResult.AddedDate = DateTime.Now;

                searchResult.ExpirationDate = DateTime.Now.AddDays(2).AddHours(6);

                searchResult.CheckDate = DateTime.Now;

                context.SaveChanges();
            }

        }

        public static void ShowAdsButEligibleForRenewFromClAccountManagement(long clPostingId)
        {
            var context = new CLDMSEntities();
        
            var searchResult =
                 context.Trackings.FirstOrDefault(x => x.CLPostingId == clPostingId);

            if (searchResult!=null)
            {
             


                searchResult.AddedDate = DateTime.Now;

                searchResult.ExpirationDate = DateTime.Now.AddDays(2).AddHours(6);

                searchResult.CheckDate = DateTime.Now;

                context.SaveChanges();

            }

        }

        public static void PinkAdInClAccountManagement(List<long> clPostingIdList)
        {

            var context = new CLDMSEntities();
           

            foreach (var clPostingId in clPostingIdList)
            {
                var searchResult =
                      context.Trackings.FirstOrDefault(x => x.CLPostingId == clPostingId);
                if (searchResult!=null)
                {


                    searchResult.CLPostingId = 0;

                    searchResult.CheckDate = DateTime.Now;



                }
            }
            context.SaveChanges();


        }

        //public static void GetLuxuryMakes()
        //{
        //    var context = new CLDMSEntities();
          

        //    var extraMakes = context.addmanufactures.AsEnumerable();

        //    GlobalVar.LuxuryMakesList = new List<IdentifiedString>();

        //    foreach (var addmanufacture in extraMakes)
        //    {

        //        var newId = new IdentifiedString()
        //            {
        //                id = addmanufacture.DivisionId.GetValueOrDefault(),
        //                Value = addmanufacture.DivisionName
        //            };

        //        GlobalVar.LuxuryMakesList.Add(newId);
        //    }

          
        //}

        public static void GetDriveTrains()
        {
            var context = new CLDMSEntities();
           

            var extraDrives = context.WheelDrives.AsEnumerable();

            GlobalVar.DriveTrainList = new List<PostClDriveTrain>();

            foreach (var drive in extraDrives)
            {

                var newId = new PostClDriveTrain()
                {
                   Name = drive.Name,
                   ShortValue = drive.ShortValue,
                   TextValue = drive.TextValue
                };

                GlobalVar.DriveTrainList.Add(newId);
            }

            
        }

        public static IEnumerable<string> GetDataFeedPackage()
        {
            List<string> returnList;
            using (var context = new CLDMSEntities())
            {
                returnList= context.DataFeedVendors.Select(x => x.VendorName).OrderBy(x=>x.Trim()).ToList();

                returnList.Add("Other");
                
            }
            return returnList;

        }


        public static void UpdateDataFeedSetUp(PostClDataFeed datafeed)
        {
            using (var context = new CLDMSEntities())
            {
                var findDealer =
                    context.Dealers.First(x => x.DealerId == GlobalVar.CurrentDealer.DealerId);

                findDealer.VendorEmail = datafeed.VendorEmail;

                findDealer.VendorPhone = datafeed.VendorPhone;

                findDealer.DataFeedVendor = datafeed.VendorName;

                findDealer.CustomMessage = datafeed.CustomMessage;

                findDealer.AuthorizedName = datafeed.YourName;

                context.SaveChanges();
            }
        }

    }
}