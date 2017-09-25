using System;
using System.Collections.Generic;
using System.Linq;
using ClappManagementSystem.DatabaseModel;
using ClappManagementSystem.Model;

namespace ClappManagementSystem.Helper
{
    public sealed class MySqlHelper
    {
         public static void InitializeSettings()
         {
             Globalvar.DealerList = new List<Dealer>();

             Globalvar.CityList = new List<City>();

             Globalvar.ComputerList = new List<Computer>();

             var context = new whitmanenterprisecraigslistEntities();

             var allInventory = context.whitmanenterprisecraigslistinventories.ToList();

             var allDealer = context.whitmanenterprisedealerlists.Where(x => x.Active == true).ToList();

             var dealerScheduleList = context.vinclappdealerschedules.ToList();

             if(dealerScheduleList.Any())

             Globalvar.SplitSchedules = dealerScheduleList.First().Schedules.GetValueOrDefault();

             foreach (var tmp in allDealer)
             {
               
                 int numberOfCars = 0;

                 if (allInventory.Any(x => x.DealershipId == tmp.VincontrolId))
                 {

                     var preNumberOfCars =
                        allInventory.Where(x =>x.DealershipId == tmp.VincontrolId && !String.IsNullOrEmpty(x.CarImageUrl));

                     foreach (var pretmp in preNumberOfCars)
                     {
                         int qualifiedImagesCount = 0;
                         
                         string tmpString = pretmp.CarImageUrl.Replace(" ", "%20");

                         if (tmpString.Contains("|") || tmpString.Contains(","))
                         {

                             string[] totalImage = tmpString.Split(new[] { "|", "," },
                                                                   StringSplitOptions.RemoveEmptyEntries);

                             if(totalImage.Any())

                                qualifiedImagesCount = totalImage.Count();
                         }
                         if (qualifiedImagesCount >= 1)
                             numberOfCars++;
                     }

                  

                 }

                 var d = new Dealer()
                 {
                     DealerId = Convert.ToInt32(tmp.VincontrolId),
                     DealershipName = tmp.DealershipName,
                     State = tmp.State,
                     City = tmp.City,
                     ZipCode = tmp.ZipCode,
                     Phone = tmp.PhoneNumber,
                     NumberOfCars = numberOfCars,
                     Round=numberOfCars %10==0? numberOfCars/10:numberOfCars/10+1,
                     ScheduleCityList = new List<ScheduleCity>(),

                     
                 };

                 if (dealerScheduleList.Any(x => x.DealerId == d.DealerId))
                 {
                     foreach (var scheduleTmp in dealerScheduleList.Where(x => x.DealerId == d.DealerId))
                     {
                         d.ScheduleCityList.Add(new ScheduleCity()
                             {
                                 City = scheduleTmp.CityId.GetValueOrDefault(),
                                 Price = scheduleTmp.Price.GetValueOrDefault(),
                                 Split = scheduleTmp.Split.GetValueOrDefault(),
                                 Schedules = scheduleTmp.Schedules.GetValueOrDefault(),
                             });
                     }
                 }

                 Globalvar.DealerList.Add(d);
             }


             foreach (var citytmp in context.whitmanenterprisecraigslistcities.ToList())
             {
                 Globalvar.CityList.Add(new City()
                     {
                         CityID = citytmp.CityID,
                         CityName = citytmp.CityName,
                         CLIndex = citytmp.CLIndex.GetValueOrDefault(),
                         CraigsListCityURL = citytmp.CraigsListCityURL,
                         SubCity = citytmp.SubCity.GetValueOrDefault()

                     }
                     );

             }


             foreach (var computertmp in context.whitmanenterprisecomputeraccounts.ToList())
             {
                 Globalvar.ComputerList.Add(new Computer()
                 {
                    PcAccount = computertmp.AccountPC,
                    CityId                    = computertmp.CityId,
                    DealerId = computertmp.DealerId
                 }
                 );

             }

         }

         //public static void InitializeSchedule()
         //{
         //    Globalvar.DealerList = new List<Dealer>();

         //    Globalvar.CityList = new List<City>();

         //    Globalvar.ComputerList = new List<Computer>();

         //    var context = new whitmanenterprisecraigslistEntities();

         //    var allInventory = context.whitmanenterprisecraigslistinventories.ToList();

         //    var allDealer = context.whitmanenterprisedealerlists.Where(x => x.Active == true).ToList();

         //    var dealerScheduleList = context.vinclappdealerschedules.ToList();

         //    foreach (var tmp in allDealer)
         //    {

         //        int numberOfCars = 0;

         //        if (allInventory.Any(x => x.DealershipId == tmp.VincontrolId))
         //        {

         //            var preNumberOfCars =
         //               allInventory.Where(x => x.DealershipId == tmp.VincontrolId && !String.IsNullOrEmpty(x.CarImageUrl));

         //            foreach (var pretmp in preNumberOfCars)
         //            {
         //                int qualifiedImagesCount = 0;

         //                string tmpString = pretmp.CarImageUrl.Replace(" ", "%20");

         //                if (tmpString.Contains("|") || tmpString.Contains(","))
         //                {

         //                    string[] totalImage = tmpString.Split(new[] { "|", "," },
         //                                                          StringSplitOptions.RemoveEmptyEntries);

         //                    if (totalImage.Any())

         //                        qualifiedImagesCount = totalImage.Count();
         //                }
         //                if (qualifiedImagesCount >= 1)
         //                    numberOfCars++;
         //            }



         //        }

         //        var d = new Dealer()
         //        {
         //            DealerId = Convert.ToInt32(tmp.VincontrolId),
         //            DealershipName = tmp.DealershipName,
         //            State = tmp.State,
         //            City = tmp.City,
         //            ZipCode = tmp.ZipCode,
         //            Phone = tmp.PhoneNumber,
         //            NumberOfCars = numberOfCars,
         //            Round = numberOfCars % 10 == 0 ? numberOfCars / 10 : numberOfCars / 10 + 1,
         //            ScheduleCityList = new List<ScheduleCity>(),


         //        };

         //        if (dealerScheduleList.Any(x => x.DealerId == d.DealerId))
         //        {
         //            foreach (var scheduleTmp in dealerScheduleList.Where(x => x.DealerId == d.DealerId))
         //            {
         //                d.ScheduleCityList.Add(new ScheduleCity()
         //                {
         //                    City = scheduleTmp.CityId.GetValueOrDefault(),
         //                    Price = scheduleTmp.Price.GetValueOrDefault(),
         //                    Split = scheduleTmp.Split.GetValueOrDefault(),
         //                    Schedules = scheduleTmp.Schedules.GetValueOrDefault(),
         //                });
         //            }
         //        }

         //        Globalvar.DealerList.Add(d);
         //    }


         //    foreach (var citytmp in context.whitmanenterprisecraigslistcities.ToList())
         //    {
         //        Globalvar.CityList.Add(new City()
         //        {
         //            CityID = citytmp.CityID,
         //            CityName = citytmp.CityName,
         //            CLIndex = citytmp.CLIndex.GetValueOrDefault(),
         //            CraigsListCityURL = citytmp.CraigsListCityURL,
         //            SubCity = citytmp.SubCity.GetValueOrDefault()

         //        }
         //            );

         //    }


         //    foreach (var computertmp in context.whitmanenterprisecomputeraccounts.ToList())
         //    {
         //        Globalvar.ComputerList.Add(new Computer()
         //        {
         //            PcAccount = computertmp.AccountPC,
         //            CityId = computertmp.CityId,
         //            DealerId = computertmp.DealerId
         //        }
         //        );

         //    }

         //}

        public static List<GridScheuleModel> GetComputerInfoList()
        {
            var returnList = new List<GridScheuleModel>();

            foreach (var computertmp in Globalvar.ComputerList.Select(x=>x.PcAccount).Distinct())
            {
                var dealerList = Globalvar.ComputerList.Where(x => x.PcAccount == computertmp);

                int totalCarsPerSchedule = 0;

                foreach (var dealer in dealerList)
                {
                    totalCarsPerSchedule += Globalvar.DealerList.First(x => x.DealerId == dealer.DealerId).NumberOfCars;

                }

             


                var gridRecord = new GridScheuleModel()
                                            {
                                                Computer = computertmp,
                                                Cars = totalCarsPerSchedule,
                                                Rounds = totalCarsPerSchedule % 10 == 0 ? totalCarsPerSchedule / 10 : totalCarsPerSchedule / 10 + 1 ,
                                             
                                              
                                            };

                returnList.Add(gridRecord);
            }

            return returnList;

        }
    }
}
