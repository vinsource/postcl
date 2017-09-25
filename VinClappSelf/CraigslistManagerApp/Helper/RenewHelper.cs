using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using WatiN.Core;
using WatiN.Core.Native.Windows;

namespace CraigslistManagerApp.Helper
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

        public static void RenewAdsProcessByEmailAccount(int computerId)
        {
            var totalrenewList =SQLHelper.PopulateEmailHavingRenewAds(computerId);
                          
            foreach (var eAccount in totalrenewList)
            {
                if (_browser != null && IsAlreadyLogin())
                {
                    System.Threading.Thread.Sleep(2000);

                    _browser.Span(Find.ById("ef")).Links.First().Click();

                    System.Threading.Thread.Sleep(6000);

                    _browser.ClearCache();

                    _browser.ClearCookies();

                }

                var ieProcesses = Process.GetProcessesByName("iexplore");

                if (ieProcesses.Any())
                {
                    foreach (Process ie in ieProcesses)
                    {
                        ie.Kill();
                    }
                }

                System.Threading.Thread.Sleep(2000);

                //var networkManagement = new NetworkManagement();

                //networkManagement.SetIP(eAccount);

                System.Threading.Thread.Sleep(8000);

                try
                {
                    
                    if (_browser == null)
                    {
                        _browser = new IE("https://accounts.craigslist.org/");

                    }
                    else
                    {
                        _browser.GoTo("https://accounts.craigslist.org/");
                    }


                    _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);
                   
                    System.Threading.Thread.Sleep(2000);
                    
                    _browser.TextField(Find.ByName("inputEmailHandle")).TypeText(eAccount.CraigslistAccount);

                    _browser.TextField(Find.ByName("inputPassword")).TypeText(eAccount.CraigsListPassword);

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
                    numberofPages = numberofPages + numberofFilterpage / 2;

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
                                            SQLHelper.ShowAdsButEligibleForRenewFromClAccountManagement(
                                                clPostingId);
                                        }
                                      
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
                                SQLHelper.PinkAdInClAccountManagement(
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
                                            SQLHelper.ShowAdsButEligibleForRenewFromClAccountManagement(
                                                clPostingId);
                                        }
                                        
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


                    throw new Exception(ex.Message);

                    System.Threading.Thread.Sleep(2000);

                }

            


            }


         
        }
    }
}
