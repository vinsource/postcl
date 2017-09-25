using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using Clapp.Services.Business.DataHelper;
using Clapp.Services.Business.Log;
using WatiN.Core;
using WatiN.Core.Native.Windows;

namespace Clapp.Services.Business.IEHelper
{
    public class ReportHelper
    {
        private static IE _browser;

        public static void CloseInternetExplorers()
        {
            var processes = from process in Process.GetProcesses()
                            where process.ProcessName == "iexplore"
                            select process;

            foreach (var process in processes)
            {
                while (!process.HasExited)
                {
                    process.Kill();
                    process.WaitForExit();
                }
            }
        }

        public static void ReportProcessRenew()
        {
            CloseInternetExplorers();

            var totaltodayReportList = MySqlHelper.PopulateTodayRenewAdsForReport();

            ServiceLog.CraigslistErrorLog("*****************************************NUMBEROFADTORUN = " + totaltodayReportList.Count + "*******************************************");

            int showedAds = 0;

            int flagedAds = 0;

            foreach (var vehicle in totaltodayReportList)
            {
             
                try
                {
                    if (_browser == null)
                    {

                        _browser =
                            new IE(vehicle.CraigslistUrl);



                    }
                    else
                    {
                        _browser.GoToNoWait(vehicle.CraigslistUrl);

                    }
                    _browser.ShowWindow(NativeMethods.WindowShowStyle.Hide);

                    System.Threading.Thread.Sleep(1000);


                    if (_browser.Html.Contains("This posting has been flagged for removal"))
                    {
                        MySqlHelper.NonShowAds(vehicle.AdTrackingId);

                        ServiceLog.CraigslistErrorLog(vehicle.AdTrackingId.ToString(CultureInfo.InvariantCulture) + " == FLAGGED");
                        flagedAds++;


                    }
                    else
                    {
                        MySqlHelper.ShowAds(vehicle.AdTrackingId);
                        ServiceLog.CraigslistErrorLog(vehicle.AdTrackingId.ToString(CultureInfo.InvariantCulture) + " == SHOWED");
                        showedAds++;
                    }

                    
                   
                    System.Threading.Thread.Sleep(200);
                }
                catch (Exception ex)
                {
                    ServiceLog.CraigslistErrorLog(ex.Message);

                    
                }

            }

            if (_browser != null)
            {
                _browser.ClearCache();

                _browser.ClearCookies();

                _browser.Close();

                _browser = null;

            }

            var percentage =
                   Math.Ceiling(((double)showedAds/
                                 totaltodayReportList.Count()) * 100);
            
            var builder = new StringBuilder();

            builder.Append("NUMBER OF RENEW ADS TODAY = <b>" + totaltodayReportList.Count + ".....");

            builder.Append("NUMBER OF RENEW ADS SHOWED = " + showedAds + ".....");

            builder.Append("NUMBER OF RENEW ADS FLAGGED = " + flagedAds + "....");

            builder.Append("PERCENTAGE = <b>" + percentage.ToString(CultureInfo.InvariantCulture) + "%" + ".......");

            SmsHelper.SendMessageToAtt("Craigslist Report",builder.ToString());

            ServiceLog.CraigslistErrorLog("*****************************************SHOWED = " + showedAds + "*******************************************");

            ServiceLog.CraigslistErrorLog("*****************************************FLAGGED = " + flagedAds + "*******************************************");

            ServiceLog.CraigslistErrorLog("*****************************************DONE*******************************************");
        }

        public static void ReportProcessNewPost()
        {
            CloseInternetExplorers();

            var totaltodayReportList = MySqlHelper.PopulateTodayNewPostAdsForReport();

            ServiceLog.CraigslistErrorLog("*****************************************NUMBEROFADTORUN = " + totaltodayReportList.Count + "*******************************************");

            int showedAds = 0;

            int flagedAds = 0;

            foreach (var vehicle in totaltodayReportList)
            {
              
                try
                {
                    if (_browser == null)
                    {

                        _browser =
                            new IE(vehicle.CraigslistUrl);



                    }
                    else
                    {
                        _browser.GoToNoWait(vehicle.CraigslistUrl);

                    }
                    _browser.ShowWindow(NativeMethods.WindowShowStyle.Hide);

                    System.Threading.Thread.Sleep(1000);


                    if (_browser.Html.Contains("This posting has been flagged for removal"))
                    {
                        MySqlHelper.NonShowAds(vehicle.AdTrackingId);

                        ServiceLog.CraigslistErrorLog(vehicle.AdTrackingId.ToString(CultureInfo.InvariantCulture) + " == FLAGGED");
                        flagedAds++;

                       
                    }
                    else
                    {
                        MySqlHelper.ShowAds(vehicle.AdTrackingId);
                        ServiceLog.CraigslistErrorLog(vehicle.AdTrackingId.ToString(CultureInfo.InvariantCulture) + " == SHOWED");
                        showedAds++;
                    }


                    System.Threading.Thread.Sleep(500);
                }
                catch (Exception ex)
                {
                    ServiceLog.CraigslistErrorLog(ex.Message);


                }

            }

            if (_browser != null)
            {
                _browser.ClearCache();

                _browser.ClearCookies();

                _browser.Close();

                _browser = null;

            }

            var percentage =
                   Math.Ceiling(((double)showedAds /
                                 totaltodayReportList.Count()) * 100);

            var builder = new StringBuilder();

            builder.Append("NUMBER OF NEW POSTS TODAY = <b>" + totaltodayReportList.Count + ".....");

            builder.Append("NUMBER OF NEW POSTS SHOWED = " + showedAds + ".....");

            builder.Append("NUMBER OF NEW POSTS FLAGGED = " + flagedAds + "....");

            builder.Append("PERCENTAGE = <b>" + percentage.ToString(CultureInfo.InvariantCulture) + "%" + ".......");

            SmsHelper.SendMessageToAtt("Craigslist Report", builder.ToString());

            ServiceLog.CraigslistErrorLog("*****************************************SHOWED = " + showedAds + "*******************************************");

            ServiceLog.CraigslistErrorLog("*****************************************FLAGGED = " + flagedAds + "*******************************************");

            ServiceLog.CraigslistErrorLog("*****************************************DONE*******************************************");
        }
    }
}
