using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using Clapp.Services.Business.DataHelper;
using Clapp.Services.Business.Model;
using WatiN.Core;
using WatiN.Core.Native.Windows;

namespace Clapp.Services.Business.IEHelper
{
    public class StaticVariable
    {
        public static List<WhitmanEntepriseMasterVehicleInfo> WalterList;
    }
   
    public class ClickHelper
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

        public static void ClickThroughProcess()
        {
            StaticVariable.WalterList = MySqlHelper.PopulateAvailableClickThroughAds();

            var totalclickthroughList = StaticVariable.WalterList.GetRange(0, StaticVariable.WalterList.Count);

            while (totalclickthroughList.Any())
            {
                try
                {
                    var firstElement = totalclickthroughList.ElementAt(0);

                    _browser = new IE(firstElement.CraigslistUrl);

                    _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);

                    if (_browser.Links.Any(x => x.Url.Contains(".com")))
                    {
                        _browser.GoTo(_browser.Links.First(x => x.Url.Contains(".com")).Url);

                        System.Threading.Thread.Sleep(65000);
                        
                        Console.WriteLine("Click through the ad  " + firstElement.CLPostingId);
                    }

                    
                    CloseInternetExplorers();

                    if (totalclickthroughList.Any())
                        totalclickthroughList.RemoveAt(0);
                    else
                    {
                        totalclickthroughList = StaticVariable.WalterList.GetRange(0, StaticVariable.WalterList.Count);
                    }
                }
                catch (Exception)
                {
                   
                    if (totalclickthroughList.Any())
                        totalclickthroughList.RemoveAt(0);
                    else
                    {
                        totalclickthroughList = StaticVariable.WalterList.GetRange(0, StaticVariable.WalterList.Count);
                    }

                    CloseInternetExplorers();
                }
            }




        }
    }
}
