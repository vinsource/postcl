using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Clapp.Services.Business.Log;
using Clapp.Services.Business.Model;

namespace Clapp.Services.Business.DataHelper
{
    public class MySqlHelper
    {

        public static List<WhitmanEntepriseMasterVehicleInfo> PopulateTodayRenewAdsForReport()
        {
            var context = new whitmanenterprisecraigslistEntities()
            {
                CommandTimeout = 18000
            };

            var bufferMasterVehicleList
                = new List<WhitmanEntepriseMasterVehicleInfo>();

            var dtCompare = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var checkList =
                         context.vincontrolcraigslisttrackings.Where(
                             a =>
                             a.CLPostingId > 0 && a.AddedDate >= dtCompare && a.Renew == true).Select(a => new
                             {
                                 a.TrackingId,
                                 a.CityId,
                                 a.CLPostingId,
                                 a.Computer,
                                 a.ShowAd,
                                 a.CheckDate,
                                 a.AddedDate,
                                 a.HtmlCraigslistUrl
                             });

            foreach (var ads in checkList)
            {
                var addCar = new WhitmanEntepriseMasterVehicleInfo()
                {
                    AutoID = bufferMasterVehicleList.Count + 1,
                    CLPostingId = ads.CLPostingId.GetValueOrDefault(),
                    AdTrackingId = ads.TrackingId,
                    CraigslistUrl = ads.HtmlCraigslistUrl
                };
                bufferMasterVehicleList.Add(addCar);
            }


            return bufferMasterVehicleList;
        }


        public static List<WhitmanEntepriseMasterVehicleInfo> PopulateTodayNewPostAdsForReport()
        {
            var context = new whitmanenterprisecraigslistEntities()
            {
                CommandTimeout = 18000
            };

            var bufferMasterVehicleList
                = new List<WhitmanEntepriseMasterVehicleInfo>();

            var dtCompare = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddHours(8);

            var cityList = context.whitmanenterprisecraigslistcities.ToList();

            var checkList =
                         context.vincontrolcraigslisttrackings.Where(
                             a =>
                             a.CLPostingId > 0 && a.AddedDate >= dtCompare && a.Renew == false).Select(a => new
                             {
                                 a.TrackingId,
                                 a.CityId,
                                 a.CLPostingId,
                                 a.Computer,
                                 a.ShowAd,
                                 a.CheckDate,
                                 a.AddedDate,
                                 a.HtmlCraigslistUrl
                             });

            foreach (var ads in checkList)
            {
                var addCar = new WhitmanEntepriseMasterVehicleInfo()
                {
                    AutoID = bufferMasterVehicleList.Count + 1,
                    CLPostingId = ads.CLPostingId.GetValueOrDefault(),
                    AdTrackingId = ads.TrackingId,
                    CraigslistCityUrl = cityList.First(x => x.CityID == ads.CityId).CraigsListCityURL,
                    CraigslistUrl = ads.HtmlCraigslistUrl
                };
                bufferMasterVehicleList.Add(addCar);
            }



            return bufferMasterVehicleList;
        }

        public static List<WhitmanEntepriseMasterVehicleInfo> PopulateAvailableClickThroughAds()
        {
            var context = new whitmanenterprisecraigslistEntities()
            {
                CommandTimeout = 18000
            };

            var bufferMasterVehicleList
                = new List<WhitmanEntepriseMasterVehicleInfo>();

            var cityList = context.whitmanenterprisecraigslistcities.ToList();

            var checkList = from a in context.vincontrolcraigslisttrackings
                            where
                                a.DealerId==3738||a.DealerId==113738||a.DealerId==115896||a.DealerId==44670||a.DealerId==144670||
                                a.DealerId==15896||a.DealerId==11828||a.DealerId==111828||a.DealerId==7180||a.DealerId==112650||
                                a.DealerId==2650||a.DealerId==15986||a.DealerId==115986 
                            select new
                            {
                                a.TrackingId,
                                a.ListingId,
                                a.CityId,
                                a.CLPostingId,
                                a.ShowAd,
                                a.EmailAccount,
                                a.DealerId,
                                a.AddedDate,
                                a.ExpirationDate,
                                a.HtmlCraigslistUrl
                            };

            foreach (var ads in checkList.OrderBy(x => x.EmailAccount))
            {
                var addCar = new WhitmanEntepriseMasterVehicleInfo()
                {
                    AutoID = bufferMasterVehicleList.Count + 1,
                    CraigslistUrl = ads.HtmlCraigslistUrl,
                    CraigslistAccountName = ads.EmailAccount,
                    CLPostingId = ads.CLPostingId.GetValueOrDefault(),
                    AdTrackingId = ads.TrackingId,
                    CraigslistCityUrl = cityList.First(x => x.CityID == ads.CityId).CraigsListCityURL
                };
                bufferMasterVehicleList.Add(addCar);
            }

            return bufferMasterVehicleList.Where(x=>!String.IsNullOrEmpty(x.CraigslistUrl)).ToList();
        }

        public static List<CraigsListEmailAccount> PopulateIps()
        {
            var context = new whitmanenterprisecraigslistEntities()
            {
                CommandTimeout = 18000
            };


            var result = new List<CraigsListEmailAccount>();

            int position = 1;

            var emailListPerPcId = context.vinclappemailaccounts.Where(x => x.Active == true && !String.IsNullOrEmpty(x.ProxyIP) && x.DefaultGateway.Equals("12.31.180.129"));

            foreach (var tmp in emailListPerPcId)
            {
                if (!String.IsNullOrEmpty(tmp.AccountName) && !String.IsNullOrEmpty(tmp.AccountPassword))
                {
                    var clAccount = new CraigsListEmailAccount()
                    {
                        CraigslistAccount = tmp.AccountName,
                        CraigsListPassword = tmp.AccountPassword,
                        CraigsAccountPhoneNumber = tmp.Phone,
                        Proxy = tmp.ProxyIP,
                        isCurrentlyUsed = false,
                        Position = position++,
                        DefaultGateWay = tmp.DefaultGateway,
                        Dns = tmp.Dns

                    };

                    result.Add(clAccount);
                }
            }

            if (result.Any())
                result.First().isCurrentlyUsed = true;

            return result;
        }


        public static List<WhitmanEntepriseMasterVehicleInfo> PopulateAvailableRenewAds()
        {
            var context = new whitmanenterprisecraigslistEntities()
                {
                    CommandTimeout = 18000
                };

            var bufferMasterVehicleList
                = new List<WhitmanEntepriseMasterVehicleInfo>();

            var dtCompare = DateTime.Now.AddHours(-72);

            var cityList = context.whitmanenterprisecraigslistcities.ToList();

            var checkList = from a in context.vincontrolcraigslisttrackings
                            from b in context.vinclappemailaccounts
                            where
                                a.EmailAccount == b.AccountName && a.AddedDate <= dtCompare  && a.CLPostingId > 0 
                            select new
                                {
                                    a.TrackingId,
                                    a.ListingId,
                                    a.CityId,
                                    a.CLPostingId,
                                    a.ShowAd,
                                    a.EmailAccount,
                                    a.DealerId,
                                    b.AccountPassword,
                                    b.Phone,
                                    a.AddedDate,
                                    a.ExpirationDate,
                                    a.HtmlCraigslistUrl
                                };

            foreach (var ads in checkList.OrderBy(x => x.EmailAccount))
            {
                var addCar = new WhitmanEntepriseMasterVehicleInfo()
                    {
                        AutoID = bufferMasterVehicleList.Count + 1,
                        CraigslistUrl = ads.HtmlCraigslistUrl,
                        CraigslistAccountName = ads.EmailAccount,
                        CraigslistAccountPassword = ads.AccountPassword,
                        CraigslistAccountPhone = ads.Phone,
                        CLPostingId = ads.CLPostingId.GetValueOrDefault(),
                        AdTrackingId = ads.TrackingId,
                        CraigslistCityUrl = cityList.First(x=>x.CityID==ads.CityId).CraigsListCityURL
                    };
                bufferMasterVehicleList.Add(addCar);
            }

      

            return bufferMasterVehicleList;
        }

        public static List<WhitmanEntepriseMasterVehicleInfo> PopulateEmailHavingRenewAds()
        {
            var context = new whitmanenterprisecraigslistEntities()
            {
                CommandTimeout = 18000
            };

            var bufferMasterVehicleList
                = new List<WhitmanEntepriseMasterVehicleInfo>();

            //var dtCompare = DateTime.Now.AddHours(-55);

            var checkList = from a in context.vincontrolcraigslisttrackings
                            from b in context.vinclappemailaccounts
                            where
                                a.EmailAccount == b.AccountName && a.CLPostingId > 0
                            select new
                            {
                                a.TrackingId,
                                a.ListingId,
                                a.CityId,
                                a.CLPostingId,
                                a.ShowAd,
                                a.EmailAccount,
                                a.DealerId,
                                b.AccountPassword,
                                b.Phone,
                                a.AddedDate,
                                a.ExpirationDate,
                                a.HtmlCraigslistUrl
                            };

            var hashSet = new HashSet<string>();

            foreach (var ads in checkList.OrderBy(x => x.EmailAccount))
            {
                if (!hashSet.Contains(ads.EmailAccount))
                {
                    var addCar = new WhitmanEntepriseMasterVehicleInfo()
                        {
                            AutoID = bufferMasterVehicleList.Count + 1,
                            CraigslistAccountName = ads.EmailAccount,
                            CraigslistAccountPassword = ads.AccountPassword,

                        };
                    bufferMasterVehicleList.Add(addCar);
                }

                hashSet.Add(ads.EmailAccount);
            }



            return bufferMasterVehicleList;
        }

        public static void ShowAdsButIneligibleForRenew(int trackingAdId)
        {
            var context = new whitmanenterprisecraigslistEntities()
            {
                CommandTimeout = 18000
            };
            
            var searchResult =
                           context.vincontrolcraigslisttrackings.First(x => x.TrackingId == trackingAdId);

            if (searchResult != null)
            {
                searchResult.ShowAd = true;

                searchResult.CheckDate = DateTime.Now;

                searchResult.CLPostingId = 0;

                context.SaveChanges();
            }

        }

        public static void ShowAdsButEligibleForRenew(int trackingAdId)
        {
            var context = new whitmanenterprisecraigslistEntities()
            {
                CommandTimeout = 18000
            };

            var searchResult =
                           context.vincontrolcraigslisttrackings.First(x => x.TrackingId == trackingAdId);

            if (searchResult != null)
            {
                searchResult.AddedDate = DateTime.Now;

                searchResult.ExpirationDate = DateTime.Now.AddDays(2).AddHours(6);

                searchResult.ShowAd = true;

                searchResult.CheckDate = DateTime.Now;

                context.SaveChanges();
            }

        }

        public static void ShowAdsButEligibleForRenewFromClAccountManagement(long clPostingId)
        {
            var context = new whitmanenterprisecraigslistEntities()
            {
                CommandTimeout = 18000
            };

            if (context.vincontrolcraigslisttrackings.Any(x => x.CLPostingId == clPostingId))
            {
                var searchResult =
                    context.vincontrolcraigslisttrackings.First(x => x.CLPostingId == clPostingId);

              
                    searchResult.AddedDate = DateTime.Now;

                    searchResult.ExpirationDate = DateTime.Now.AddDays(2).AddHours(6);

                    searchResult.ShowAd = true;

                    searchResult.Renew = true;

                    searchResult.CheckDate = DateTime.Now;

                    context.SaveChanges();
                
            }

        }

        public static void PinkAdInClAccountManagement(List<long> clPostingIdList)
        {
            
            var context = new whitmanenterprisecraigslistEntities()
            {
                CommandTimeout = 18000
            };

            foreach (var clPostingId in clPostingIdList)
            {

                if (context.vincontrolcraigslisttrackings.Any(x => x.CLPostingId == clPostingId))
                {

                    var searchResult =
                        context.vincontrolcraigslisttrackings.First(x => x.CLPostingId == clPostingId);


                    searchResult.ShowAd = false;

                    searchResult.Renew = false;

                    searchResult.CLPostingId = 0;

                    searchResult.CheckDate = DateTime.Now;

                  

                }
            }
            context.SaveChanges();
           

        }

        public static void ShowAds(int trackingAdId)
        {
            var context = new whitmanenterprisecraigslistEntities()
            {
                CommandTimeout = 18000
            };

            var searchResult =
                           context.vincontrolcraigslisttrackings.First(x => x.TrackingId == trackingAdId);

            if (searchResult != null)
            {
               
                searchResult.ShowAd = true;

                searchResult.CheckDate = DateTime.Now;

                context.SaveChanges();
            }

        }

        public static void NonShowAds(int trackingAdId)
        {
            var context = new whitmanenterprisecraigslistEntities()
            {
                CommandTimeout = 18000
            };

            var searchResult =
                           context.vincontrolcraigslisttrackings.First(x => x.TrackingId == trackingAdId);

            if (searchResult != null)
            {

                searchResult.ShowAd = false;

                searchResult.CheckDate = DateTime.Now;

                context.SaveChanges();
            }

        }


        public static void DeleteTracking(int trackingAdId)
        {
            var context = new whitmanenterprisecraigslistEntities()
            {
                CommandTimeout = 18000
            };

            var searchResult =
                           context.vincontrolcraigslisttrackings.First(x => x.TrackingId == trackingAdId);

            if (searchResult != null)
            {

                context.Attach(searchResult);

                context.DeleteObject(searchResult);

                context.SaveChanges();
            }

        }
    }
}
