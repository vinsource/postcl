using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Clapp.Services.Business.DataHelper;
using Clapp.Services.Business.Log;
using WatiN.Core;
using WatiN.Core.Native.Windows;

namespace Clapp.Services.Business.IEHelper
{
    public class RenewHelper
    {
        private static IE _browser;

        public static void CloseInternetExplorers()
        {
            var processes = from process in Process.GetProcesses()
                            where process.ProcessName == "iexplore"
                            select process;

            if (processes.Any())
            {

                foreach (var process in processes)
                {
                    while (!process.HasExited)
                    {
                        process.Kill();
                        process.WaitForExit();
                    }
                }
            }
        }

        public static void RenewAdsProcess(int splitPart)
        {
            var totalrenewList = MySqlHelper.PopulateAvailableRenewAds();

            var renewList = totalrenewList.Skip(totalrenewList.Count * splitPart / 2).Take(totalrenewList.Count / 2).ToList();
            
            var ieProcesses = Process.GetProcessesByName("iexplore");

            if (ieProcesses.Any() && ieProcesses.Count() > 5)
            {
                foreach (Process ie in ieProcesses)
                {
                    ie.Kill();
                }
            }

            if (_browser != null)
            {
                _browser.ClearCache();

                _browser.ClearCookies();

                _browser.Close();

                _browser = null;

            }

     
            if (_browser!=null && IsAlreadyLogin())
            {
                System.Threading.Thread.Sleep(2000);

                _browser.Span(Find.ById("ef")).Links.First().Click();

                System.Threading.Thread.Sleep(3000);

            }

            

            foreach (var vehicle in renewList)
            {

                try
                {
                    if (_browser == null)
                    {
                        _browser = new IE(vehicle.CraigslistCityUrl + "search/ctd?zoomToPosting=&query=" + vehicle.CLPostingId + "&srchType=A&minAsk=&maxAsk=");

                    }
                    else
                    {
                        _browser.GoToNoWait(vehicle.CraigslistCityUrl + "search/ctd?zoomToPosting=&query=" + vehicle.CLPostingId + "&srchType=A&minAsk=&maxAsk=");
                    }

                    _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);

                    System.Threading.Thread.Sleep(2000);

                    if (_browser.Html.Contains("Found: "))
                    {


                        if (vehicle.AutoID == 1 || (renewList.Any(x => x.AutoID == vehicle.AutoID - 1) && !renewList.First(x => x.AutoID == vehicle.AutoID - 1).CraigslistAccountName.Equals(vehicle.CraigslistAccountName)))
                        {

                            if (_browser == null)
                            {
                                _browser = new IE("https://accounts.craigslist.org/");

                            }
                            else
                            {
                                _browser.GoTo("https://accounts.craigslist.org/");

                            }

                            if (_browser != null && IsAlreadyLogin())
                            {
                                System.Threading.Thread.Sleep(2000);

                                _browser.Span(Find.ById("ef")).Links.First().Click();

                                System.Threading.Thread.Sleep(3000);

                                _browser.ClearCache();

                                _browser.ClearCookies();

                                _browser.Close();

                                _browser = null;

                            }


                            if (_browser == null)
                            {
                                _browser = new IE("https://accounts.craigslist.org/");

                            }
                            else
                            {
                                _browser.GoTo("https://accounts.craigslist.org/");

                            }
                        

                            _browser.TextField(Find.ByName("inputEmailHandle")).TypeText(vehicle.CraigslistAccountName);

                            _browser.TextField(Find.ByName("inputPassword")).TypeText(vehicle.CraigslistAccountPassword);

                            System.Threading.Thread.Sleep(2000);

                            _browser.Buttons.First().Click();

                        }
                        else
                        {
                            if(_browser!=null)
                                _browser.GoTo("https://accounts.craigslist.org/");
                            else
                            {
                                _browser = new IE("https://accounts.craigslist.org/");
                            }

                            if (!IsAlreadyLogin())
                            {
                                _browser.ClearCache();

                                _browser.ClearCookies();

                                _browser.Close();

                                _browser = null;

                                _browser = new IE("https://accounts.craigslist.org/");

                                _browser.TextField(Find.ByName("inputEmailHandle")).TypeText(vehicle.CraigslistAccountName);

                                _browser.TextField(Find.ByName("inputPassword")).TypeText(vehicle.CraigslistAccountPassword);

                                System.Threading.Thread.Sleep(2000);

                                _browser.Buttons.First().Click();

                            }
                        }

                        System.Threading.Thread.Sleep(2000);

                        _browser.GoTo("https://post.craigslist.org/manage/" + vehicle.CLPostingId);

                        
                        if (_browser.Buttons.Any(x => x.Value == "Renew this Posting"))
                        {
                            System.Threading.Thread.Sleep(1000);

                            _browser.Button(Find.ByValue("Renew this Posting")).ClickNoWait();

                            System.Threading.Thread.Sleep(4000);

                            MySqlHelper.ShowAdsButEligibleForRenew(vehicle.AdTrackingId);

                            //ServiceLog.CraigslistErrorLog(vehicle.AdTrackingId.ToString(CultureInfo.InvariantCulture) + " == RENEWED");
                        }
                        else 
                        {
                            MySqlHelper.ShowAdsButIneligibleForRenew(vehicle.AdTrackingId);

                            _browser.GoTo("https://accounts.craigslist.org/");

                            System.Threading.Thread.Sleep(2000);

                            _browser.Span(Find.ById("ef")).Links.First().Click();

                            System.Threading.Thread.Sleep(3000);

                            _browser.ClearCache();

                            _browser.ClearCookies();

                            _browser.Close();

                            _browser = null;

                            //.CraigslistErrorLog(vehicle.AdTrackingId.ToString(CultureInfo.InvariantCulture) + " == NOT ELIGIBLE TO RENEW");
                        }


                    }
                    else
                    {
                        MySqlHelper.DeleteTracking(vehicle.AdTrackingId);


                    }

                    System.Threading.Thread.Sleep(10000);
                }
                catch (Exception ex)
                {
                    //throw new Exception(ex.Message);
                }
               
            }


            

        }

        public static void RenewAdsProcessByEmailAccount()
        {

            var totalrenewList =
                MySqlHelper.PopulateEmailHavingRenewAds()
                           .OrderByDescending(x => x.CraigslistAccountName);
                           //.Split(7)
                           //.ElementAt(splitPart - 1);

            ServiceLog.CraigslistErrorLog(totalrenewList.Count() + "------------");


            foreach (var eAccount in totalrenewList)
            {
                try
                {
                    System.Threading.Thread.Sleep(2000);


                    if (_browser == null)
                    {
                        _browser = new IE("https://accounts.craigslist.org/");

                    }
                    else
                    {
                        _browser.GoTo("https://accounts.craigslist.org/");
                    }
                    _browser.ShowWindow(NativeMethods.WindowShowStyle.Hide);
                    System.Threading.Thread.Sleep(2000);

                    if (_browser != null && IsAlreadyLogin())
                    {
                        System.Threading.Thread.Sleep(2000);

                        _browser.Span(Find.ById("ef")).Links.First().Click();

                        System.Threading.Thread.Sleep(6000);

                        _browser.ClearCache();

                        _browser.GoTo("https://accounts.craigslist.org/");

                    }
                  

                    //_browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);

                    System.Threading.Thread.Sleep(2000);

                    _browser.TextField(Find.ByName("inputEmailHandle")).TypeText(eAccount.CraigslistAccountName);

                    _browser.TextField(Find.ByName("inputPassword")).TypeText(eAccount.CraigslistAccountPassword);

                    System.Threading.Thread.Sleep(2000);

                    _browser.Buttons.First().Click();

                    int numberofPages = 1;

                    int numberofFilterpage = 0;

                    foreach (var tmp in _browser.Links)
                    {
                        if (!String.IsNullOrEmpty(tmp.Url) && tmp.Url.Contains("filter_page"))
                        {
                            numberofFilterpage++;
                        }

                    }
                    numberofPages = numberofPages + numberofFilterpage/2;

                    ServiceLog.CraigslistErrorLog("**** NUMBER OF PAGES = " + numberofPages + "****");


                    if (numberofPages > 0)
                    {

                        for (int i = 1; i <= numberofPages; i++)
                        {
                            if (i == 1)
                            {
                                var allDeleteTab = _browser.TableCells.ToArray();

                                var listofDeleted = new List<long>();

                                foreach (var deleteTmp in allDeleteTab)
                                {
                                    if (!String.IsNullOrEmpty(deleteTmp.GetAttributeValue("style")) &&
                                        deleteTmp.GetAttributeValue("style").Contains("border: 1px solid pink") &&
                                        !String.IsNullOrEmpty(deleteTmp.Text))
                                    {
                                        long clpostingId = 0;
                                        Int64.TryParse(deleteTmp.Text, out clpostingId);

                                        if (clpostingId > 0)
                                            listofDeleted.Add(clpostingId);


                                    }
                                }

                                MySqlHelper.PinkAdInClAccountManagement(
                                    listofDeleted);

                                System.Threading.Thread.Sleep(2000);

                                if (_browser.Buttons.Any(x => x.Value == "renew"))
                                {

                                    do
                                    {
                                        var tmp = _browser.Buttons.First(x => x.Value == "renew");

                                        System.Threading.Thread.Sleep(3000);

                                        tmp.Click();

                                        var clPostingIdString =
                                            _browser.Url.Substring(
                                                _browser.Url.LastIndexOf("/", System.StringComparison.Ordinal) +
                                                1);

                                        long clPostingId = 0;

                                        Int64.TryParse(clPostingIdString, out clPostingId);

                                        if (clPostingId > 0)
                                        {
                                            MySqlHelper.ShowAdsButEligibleForRenewFromClAccountManagement(
                                                clPostingId);
                                        }
                                        ServiceLog.CraigslistErrorLog(clPostingIdString + "Renew------------");
                                        System.Threading.Thread.Sleep(8000);

                                        _browser.GoTo("https://accounts.craigslist.org/");

                                        System.Threading.Thread.Sleep(3000);
                                    } while (_browser.Buttons.Any(x => x.Value == "renew"));


                                }


                            }
                            else
                            {
                                _browser.GoTo("https://accounts.craigslist.org/?filter_page=" + i +
                                              "&filter_cat=0&filter_date=0&filter_active=0&show_tab=postings");

                                var allDeleteTab = _browser.TableCells.ToArray();

                                var listofDeleted = new List<long>();

                                foreach (var deleteTmp in allDeleteTab)
                                {
                                    if (!String.IsNullOrEmpty(deleteTmp.GetAttributeValue("style")) &&
                                        deleteTmp.GetAttributeValue("style").Contains("border: 1px solid pink") &&
                                        !String.IsNullOrEmpty(deleteTmp.Text))
                                    {
                                        long clpostingId = 0;
                                        Int64.TryParse(deleteTmp.Text, out clpostingId);

                                        if (clpostingId > 0)
                                            listofDeleted.Add(clpostingId);


                                    }
                                }
                                MySqlHelper.PinkAdInClAccountManagement(
                                    listofDeleted);


                                System.Threading.Thread.Sleep(2000);

                                if (_browser.Buttons.Any(x => x.Value == "renew"))
                                {
                                    do
                                    {
                                        var tmp = _browser.Buttons.First(x => x.Value == "renew");

                                        System.Threading.Thread.Sleep(3000);

                                        tmp.Click();

                                        var clPostingIdString =
                                            _browser.Url.Substring(
                                                _browser.Url.LastIndexOf("/", System.StringComparison.Ordinal) +
                                                1);

                                        long clPostingId = 0;

                                        Int64.TryParse(clPostingIdString, out clPostingId);

                                        if (clPostingId > 0)
                                        {
                                            MySqlHelper.ShowAdsButEligibleForRenewFromClAccountManagement(
                                                clPostingId);
                                        }
                                        ServiceLog.CraigslistErrorLog(clPostingIdString + "Renew------------");
                                        System.Threading.Thread.Sleep(8000);

                                        _browser.GoTo("https://accounts.craigslist.org/?filter_page=" + i +
                                                      "&filter_cat=0&filter_date=0&filter_active=0&show_tab=postings");


                                        System.Threading.Thread.Sleep(3000);
                                    } while (_browser.Buttons.Any(x => x.Value == "renew"));


                                }
                            }
                        }
                    }

                }

                catch
                    (Exception ex)
                {

                    ServiceLog.CraigslistErrorLog(ex.Message);

                    ServiceLog.CraigslistErrorLog(ex.StackTrace);

                    if (_browser != null)
                    {
                        _browser.ClearCache();

                        _browser.ClearCookies();

                        _browser = new IE("https://accounts.craigslist.org/");
                    }
                    else
                    {
                        _browser.GoTo("https://accounts.craigslist.org/");
                    }


                    System.Threading.Thread.Sleep(2000);

                }




            }

            //if (_browser != null)
            //{
            //    _browser.ClearCache();

            //    _browser.ClearCookies();

            //    _browser.Close();

            //}

            CloseInternetExplorers();

        }

        private static bool IsAlreadyLogin()
        {


            bool flagLogin = false;

            foreach (Link link in _browser.Links)
            {
                if (!String.IsNullOrEmpty(link.Text))
                {
                    if (link.Text.Trim().Contains("logout") || link.Text.Trim().Contains("log out"))
                    {
                        flagLogin = true;
                        break;
                    }
                }


            }

            return flagLogin;

        }
    }
}
