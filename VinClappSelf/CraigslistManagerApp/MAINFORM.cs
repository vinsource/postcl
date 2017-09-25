using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using CraigslistManagerApp.Helper;
using CraigslistManagerApp.Model;
using WatiN.Core;
using System.Windows.Forms;
using System.Deployment.Application;
using WatiN.Core.Native.Windows;

namespace CraigslistManagerApp
{
    public partial class Mainform : System.Windows.Forms.Form
    {
        private List<WhitmanEntepriseMasterVehicleInfo> _subMasterVehicleList = null;

        private IE _browser;

        private AnimationForm _animationForm;

        private System.Windows.Forms.Form _lForm;

        private System.Windows.Forms.Form _tForm;

        private int _flags;

        private int _postingAds =0 ;

        private int _renewAds=0;

        private int _computerId = 0;

        public Mainform()
        {
            InitializeComponent();
        }

        private void Form1Load(object sender, EventArgs e)
        {
            clsVariables.computerList = SQLHelper.GetComputerList();
           
            if (clsVariables.computerList.Any())
            {

                try
                {
                    clsVariables.chunkList = SQLHelper.GetChunkString();

                    clsVariables.CurrentProcessingDate = new DateTime();

                    var r = Screen.PrimaryScreen.WorkingArea;

                    StartPosition = FormStartPosition.Manual;

                    this.Width = 450;

                    this.Height = 1000;


                    if (ApplicationDeployment.IsNetworkDeployed)
                    {
                        Version myVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion;

                        lblVersion.Text = string.Format("VERSION: v{0}.{1}.{2}.{3}", myVersion.Major, myVersion.Minor,
                                                        myVersion.Build, myVersion.Revision);
                    }

                 

                        foreach (
                            var tmp in
                                clsVariables.computerList.OrderBy(x => x.ComputerId)
                                            .Select(x => x.ComputerId)
                                            .Distinct())
                        {
                            cbComputer.Items.Add(tmp);
                        }
                
                    if (cbComputer.Items.Count > 0)
                        cbComputer.SelectedIndex = 0;

                    const string physicalImagePath = @"C:\ImageWarehouse";

                    var dirNormal = new DirectoryInfo(physicalImagePath);

                    if (dirNormal.Exists)
                        Directory.Delete(physicalImagePath, true);


                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("There is no schedule associated with any computer", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }

        private void CraigslistLoginByComputerAccountAfterFirstTime()
        {

            var vehicle = _subMasterVehicleList.ElementAt(0);

            if (_browser == null)
            {
                _browser = new IE("https://accounts.craigslist.org/");

                _browser.ClearCookies();
            }
            else
            {
                _browser.GoTo("https://accounts.craigslist.org/");

            }

            if (IsAlreadyLogin())
            {
                System.Threading.Thread.Sleep(2000);

                _browser.Span(Find.ById("ef")).Links.First().Click();

                System.Threading.Thread.Sleep(3000);

            }

            _browser.TextField(Find.ByName("inputEmailHandle")).TypeText(vehicle.CraigslistAccountName);

            _browser.TextField(Find.ByName("inputPassword")).TypeText(vehicle.CraigslistAccountPassword);

            txtCuEmail.Text = vehicle.CraigslistAccountName;

            txtCuPass.Text = vehicle.CraigslistAccountPassword;

            txtCuPhone.Text = vehicle.CraigslistAccountPhone;

            System.Threading.Thread.Sleep(2000);

            _browser.Buttons.First().Click();

            System.Threading.Thread.Sleep(2000);


        }

        private void CraigslistLoginByComputerAccount()
        {
            var ieProcesses = Process.GetProcessesByName("iexplore");

            if (ieProcesses.Any() && ieProcesses.Count() > 5)
            {
                foreach (Process ie in ieProcesses)
                {
                    ie.Kill();
                }
            }

            if (clsVariables.computeremailaccountList.Any())
            {
                if (_browser == null)
                {
                    _browser = new IE("https://accounts.craigslist.org/");

                }
                else
                {
                    _browser.ClearCache();

                    _browser.ClearCookies();

                    _browser.Close();

                    _browser = null;

                    System.Threading.Thread.Sleep(2000);

                    _browser = new IE("https://accounts.craigslist.org/");

                }

                _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);


                if (IsAlreadyLogin())
                {
                    System.Threading.Thread.Sleep(2000);

                    _browser.Span(Find.ById("ef")).Links.First().Click();

                    System.Threading.Thread.Sleep(3000);

                }
                               

                System.Threading.Thread.Sleep(3000);

                var vehicle = _subMasterVehicleList.ElementAt(0);

                if (vehicle.CLPostingId > 0)
                {
                    
                    
                    if (_browser == null)
                    {
                        _browser = new IE(clsVariables.currentComputer.CityList.First(x => x.CityID == vehicle.PostingCityId).
                            CraigsListCityURL +
                        "ctd/");

                    }
                    else
                    {

                        _browser.GoTo(
                            clsVariables.currentComputer.CityList.First(x => x.CityID == vehicle.PostingCityId).
                                CraigsListCityURL +
                            "ctd/");
                    }


                    _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);

                    _browser.GoTo(
              vehicle.CraigslistCityUrl + "search/ctd?zoomToPosting=&query=" + vehicle.CLPostingId + "&srchType=A&minAsk=&maxAsk=");
                    
                    if (_browser.Html.Contains("Found: "))
                    {
                        System.Threading.Thread.Sleep(1000);

                        if (_browser == null)
                        {
                            _browser = new IE("https://accounts.craigslist.org/");

                            _browser.ClearCookies();
                        }
                        else
                        {
                           _browser.GoTo("https://accounts.craigslist.org/");

                        }

                        if (IsAlreadyLogin())
                        {
                            System.Threading.Thread.Sleep(2000);

                            _browser.Span(Find.ById("ef")).Links.First().Click();

                            System.Threading.Thread.Sleep(3000);

                        }

                        _browser.TextField(Find.ByName("inputEmailHandle")).TypeText(vehicle.CraigslistAccountName);

                        _browser.TextField(Find.ByName("inputPassword")).TypeText(vehicle.CraigslistAccountPassword);

                        txtCuEmail.Text = vehicle.CraigslistAccountName;

                        txtCuPass.Text = vehicle.CraigslistAccountPassword;

                        txtCuPhone.Text = vehicle.CraigslistAccountPhone;
                        
                        System.Threading.Thread.Sleep(2000);

                        _browser.Buttons.First().Click();

                        System.Threading.Thread.Sleep(2000);


                        _browser.GoTo("https://post.craigslist.org/manage/" + vehicle.CLPostingId);

                        if (_browser.Html.Contains("There appears to be a problem with this posting"))
                        {
                            //var searchResult =
                            //    context.vincontrolcraigslisttrackings.First(
                            //        x => x.TrackingId == vehicle.AdTrackingId);

                            //if (searchResult != null)
                            //{

                            //    searchResult.ShowAd = true;

                            //    searchResult.CheckDate = DateTime.Now;

                            //    searchResult.CLPostingId = 0;
                            //}


                            vehicle.CLPostingId = 0;

                            _subMasterVehicleList.ElementAt(0).CLPostingId = 0;

                            clsVariables.currentMasterVehicleList.First(x => x.AutoID == vehicle.AutoID).CLPostingId = 0;


                            //context.SaveChanges();
                            if (_browser == null)
                            {
                                _browser = new IE("https://accounts.craigslist.org/");

                                _browser.ClearCookies();
                            }
                            else
                            {
                                _browser.GoTo("https://accounts.craigslist.org/");

                            }

                            if (IsAlreadyLogin())
                            {
                                System.Threading.Thread.Sleep(2000);

                                _browser.Span(Find.ById("ef")).Links.First().Click();

                                System.Threading.Thread.Sleep(3000);

                            }

                            var emailAccount = clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed);

                            txtCuEmail.Text = emailAccount.CraigslistAccount;

                            txtCuPass.Text = emailAccount.CraigsListPassword;

                            txtCuPhone.Text = emailAccount.CraigsAccountPhoneNumber;

                            _browser.TextField(Find.ByName("inputEmailHandle")).TypeText(emailAccount.CraigslistAccount);

                            _browser.TextField(Find.ByName("inputPassword")).TypeText(emailAccount.CraigsListPassword);

                            System.Threading.Thread.Sleep(3000);

                            _browser.Buttons.First().Click();
                            System.Threading.Thread.Sleep(3000);
                        }
                        


                    }
                    else
                    {
                        SQLHelper.DeleteCurrentTracking(vehicle.AdTrackingId);

                        vehicle.CLPostingId = 0;

                        _subMasterVehicleList.ElementAt(0).CLPostingId = 0;

                        clsVariables.currentMasterVehicleList.First(x => x.AutoID == vehicle.AutoID).CLPostingId = 0;

                      

                        
                        if (_browser == null)
                        {
                            _browser = new IE("https://accounts.craigslist.org/");

                            _browser.ClearCookies();
                        }
                        else
                        {
                            _browser.GoTo("https://accounts.craigslist.org/");

                        }

                        if (IsAlreadyLogin())
                        {
                            System.Threading.Thread.Sleep(2000);

                            _browser.Span(Find.ById("ef")).Links.First().Click();

                            System.Threading.Thread.Sleep(3000);

                        }

                        var emailAccount = clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed);

                        txtCuEmail.Text = emailAccount.CraigslistAccount;

                        txtCuPass.Text = emailAccount.CraigsListPassword;

                        txtCuPhone.Text = emailAccount.CraigsAccountPhoneNumber;

                        _browser.TextField(Find.ByName("inputEmailHandle")).TypeText(emailAccount.CraigslistAccount);

                        _browser.TextField(Find.ByName("inputPassword")).TypeText(emailAccount.CraigsListPassword);

                        System.Threading.Thread.Sleep(3000);

                        _browser.Buttons.First().Click();

                        System.Threading.Thread.Sleep(2000);

                    }


                   

                }
                else
                {
                    System.Threading.Thread.Sleep(1000);

                    var emailAccount = clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed);

                    txtCuEmail.Text = emailAccount.CraigslistAccount;

                    txtCuPass.Text = emailAccount.CraigsListPassword;

                    txtCuPhone.Text = emailAccount.CraigsAccountPhoneNumber;

                    _browser.TextField(Find.ByName("inputEmailHandle")).TypeText(emailAccount.CraigslistAccount);

                    _browser.TextField(Find.ByName("inputPassword")).TypeText(emailAccount.CraigsListPassword);

                    System.Threading.Thread.Sleep(3000);

                    _browser.Buttons.First().Click();

                    System.Threading.Thread.Sleep(2000);
                }

                try
                {
                   

                    if (!IsAlreadyLogin())
                    {
                     
                        _lForm = new LoginWarningForm(vehicle);
                     
                        _lForm.Show();

                        _lForm.Location = new Point(0, 0);

                        _lForm.Activate();

                        timerPause.Enabled = true;

                        timerPostAccount.Enabled = false;

                        btnPause.Text = "UnPause";

                    

                        if (vehicle.CLPostingId > 0)
                        {
                            var body =
                                "Please check computer " + _computerId + ". You have a login warning -----. " +
                                vehicle.CraigslistAccountName +
                                " / " +

                                vehicle.CraigslistAccountPassword;

                            try
                            {
                                EmailHelper.SendEmail(new MailAddress("vinclapp@vincontrol.com"), "LOGIN WARNING NOTIFICATION", body);
                            }
                            catch (Exception)
                            {
                                
                                
                            }

                            


                        }
                        else
                        {
                            var body =
                                "Please check computer " + _computerId + ". You have a login warning -----. " +
                                clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).CraigslistAccount +
                                " / " +

                                clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).CraigsListPassword;
                            try
                            {
                                EmailHelper.SendEmail(new MailAddress("vinclapp@vincontrol.com"), "LOGIN WARNING NOTIFICATION", body);
                            }
                            catch (Exception)
                            {


                            }
                        }




                    } 
                    else
                    {
                        if (vehicle.CLPostingId > 0)
                        {
                            RunPatternForRenew();
                        }
                        else
                        {
                            if (_subMasterVehicleList.Any(x => x.CLPostingId > 0))
                            
                            {
                                //timerPostAccount.Interval = 30000;

                                timerPostAccount.Enabled = true;

                                RemoveItemFromFromAutoId(vehicle.AutoID);

                                var cloneVehicle = (WhitmanEntepriseMasterVehicleInfo)vehicle.Clone();

                                cloneVehicle.AutoID = clsVariables.currentMasterVehicleList.Max(x => x.AutoID) + 1;

                                cloneVehicle.CLPostingId = 0;

                                cloneVehicle.CraigslistAccountName = "";

                                cloneVehicle.CraigslistAccountPassword = "";
                                _subMasterVehicleList.RemoveAt(0);

                                _subMasterVehicleList.Add(cloneVehicle);

                                clsVariables.currentMasterVehicleList.Add(cloneVehicle);

                                var citySelected = clsVariables.currentComputer.CityList.First(x => x.CityID == cloneVehicle.PostingCityId);

                                string tmp = cloneVehicle.CarImageUrl;

                                string totalImage = "";

                                if (String.IsNullOrEmpty(tmp))
                                    totalImage = "0";
                                else
                                {
                                    string[] totalImages = tmp.Split(new String[] { "|", "," }, StringSplitOptions.RemoveEmptyEntries);
                                    totalImage = totalImages.Count().ToString(CultureInfo.InvariantCulture);

                                }

                                string[] row = { cloneVehicle.AutoID.ToString(CultureInfo.InvariantCulture), String.IsNullOrEmpty(citySelected.CityName) ? "" : citySelected.CityName
                               , cloneVehicle.DealershipName, cloneVehicle.StockNumber, cloneVehicle.ModelYear + "  " + cloneVehicle.Make + "  " + cloneVehicle.Model
                               ,cloneVehicle.CLPostingId.ToString(CultureInfo.InvariantCulture),cloneVehicle.CraigslistAccountName,cloneVehicle.CraigslistAccountPassword
                               ,cloneVehicle.SalePrice,cloneVehicle.ListingId.ToString(CultureInfo.InvariantCulture),cloneVehicle.Vin
                               ,totalImage,cloneVehicle.AddtionalTitle};

                                var listViewItem = new ListViewItem(row);

                                lvInventory.Items.Add(listViewItem);


                                var textPostingAds =
                                    Convert.ToInt32(
                                        lblPostingAds.Text.Substring(
                                            lblPostingAds.Text.IndexOf("/", System.StringComparison.Ordinal) + 1).Trim()) +
                                    1;

                                var textRenewAds =
                                    Convert.ToInt32(
                                        lblRenewAds.Text.Substring(
                                            lblRenewAds.Text.IndexOf("/", System.StringComparison.Ordinal) + 1).Trim()) -
                                    1;

                                lblPostingAds.Text = "Posting Ads = " + _postingAds + " / " + textPostingAds;

                                lblRenewAds.Text = "Renewing Ads = " + _renewAds + " / " + textRenewAds;

                                _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);

                                
                            }
                            else
                            {
                                RunPatternByComputerAccount();
                            }
                        }

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("You have a login warning error. Please wait for a while.", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   
                    timerPause.Enabled = true;

                    timerPostAccount.Enabled = false;
                   
                    if (vehicle.CLPostingId > 0)
                    {
                        var body =
                            "Please check computer " + _computerId + ". You have a login warning -----. " +
                            vehicle.CraigslistAccountName +
                            " / " +

                            vehicle.CraigslistAccountPassword;

                        try
                        {
                            EmailHelper.SendEmail(new MailAddress("vinclapp@vincontrol.com"), "LOGIN WARNING NOTIFICATION", body);
                        }
                        catch (Exception)
                        {


                        }
                    }
                    else
                    {
                        var body =
                            "Please check computer " + _computerId + ". You have a login warning -----. " +
                            clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).CraigslistAccount +
                            " / " +

                            clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).CraigsListPassword;
                        try
                        {
                            EmailHelper.SendEmail(new MailAddress("vinclapp@vincontrol.com"), "LOGIN WARNING NOTIFICATION", body);
                        }
                        catch (Exception)
                        {


                        }
                    }
                }

               


            }
            else
            {
                MessageBox.Show("Email accounts are not available on this schedule", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        private void TimerPostAccountTick(object sender, EventArgs e)
        {
            try
            {
                if (_subMasterVehicleList.Any())
                {  
                    //var vehicle = _subMasterVehicleList.ElementAt(0);

                    //if (vehicle.CLPostingId > 0)
             //       {
                       
             //           if (txtCuEmail.Text.Equals(vehicle.CraigslistAccountName)&&clsVariables.currentMasterVehicleList.First(x=>x.AutoID==vehicle.AutoID-1).CraigslistAccountName.Equals(vehicle.CraigslistAccountName))
                      
             //           {
                         
             //               if (_browser == null)
             //               {
             //                   _browser =
             //                       new IE(clsVariables.currentComputer.CityList.First(
             //                           x => x.CityID == vehicle.PostingCityId).
             //                                  CraigsListCityURL +
             //                              "ctd/");

             //               }
             //               else
             //               {

             //                   _browser.GoTo(
             //                       clsVariables.currentComputer.CityList.First(x => x.CityID == vehicle.PostingCityId).
             //                           CraigsListCityURL +
             //                       "ctd/");
             //               }


             //               _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);



             //               //_browser.TextField(Find.ById("query")).Value =
             //               //    vehicle.CLPostingId.ToString(CultureInfo.InvariantCulture);

             //               //_browser.RadioButton(Find.ByTitle("search the entire posting")).Checked = true;

             //               //_browser.Button(Find.ByValue("Search")).Click();
             //               _browser.GoTo(
             // vehicle.CraigslistCityUrl + "search/ctd?zoomToPosting=&query=" + vehicle.CLPostingId + "&srchType=A&minAsk=&maxAsk=");


             //               if (_browser.Html.Contains("Found: "))
             //               {
             //                   System.Threading.Thread.Sleep(2000);

             //                   _browser.GoTo("https://post.craigslist.org/manage/" + vehicle.CLPostingId);

             //                   if (_browser.Html.Contains("There appears to be a problem with this posting"))
             //                   {
                                 
             //                       vehicle.CLPostingId = 0;

             //                       _subMasterVehicleList.First(x => x.AutoID == vehicle.AutoID).CLPostingId = 0;

             //                       clsVariables.currentMasterVehicleList.First(x => x.AutoID == vehicle.AutoID).
             //                           CLPostingId = 0;

             //                   }
                               

             //               }
             //               else
             //               {
             //                   SQLHelper.DeleteCurrentTracking(vehicle.AdTrackingId);

             //                   vehicle.CLPostingId = 0;

             //                   _subMasterVehicleList.First(x => x.AutoID == vehicle.AutoID).CLPostingId = 0;

             //                   clsVariables.currentMasterVehicleList.First(x => x.AutoID == vehicle.AutoID).CLPostingId
             //                       = 0;

                                
             //               }
                           
             //               if (vehicle.CLPostingId == 0)
             //               {

             //                   //timerPostAccount.Interval = 5000;

             //                   RemoveItemFromFromAutoId(vehicle.AutoID);

             //                   var cloneVehicle =(WhitmanEntepriseMasterVehicleInfo) vehicle.Clone();

             //                   cloneVehicle.AutoID = clsVariables.currentMasterVehicleList.Max(x => x.AutoID) + 1;

             //                   cloneVehicle.CLPostingId = 0;

             //                   cloneVehicle.CraigslistAccountName = "";

             //                   cloneVehicle.CraigslistAccountPassword = "";

             //                   _subMasterVehicleList.RemoveAt(0);

             //                   _subMasterVehicleList.Add(cloneVehicle);

             //                   clsVariables.currentMasterVehicleList.Add(cloneVehicle);

             //                   var citySelected = clsVariables.currentComputer.CityList.First(x => x.CityID == cloneVehicle.PostingCityId);

             //                   string tmp = cloneVehicle.CarImageUrl;

             //                   string totalImage = "";

             //                   if (String.IsNullOrEmpty(tmp))
             //                       totalImage = "0";
             //                   else
             //                   {
             //                       string[] totalImages = tmp.Split(new String[] { "|", "," }, StringSplitOptions.RemoveEmptyEntries);
             //                       totalImage = totalImages.Count().ToString(CultureInfo.InvariantCulture);

             //                   }

             //                   string[] row = { cloneVehicle.AutoID.ToString(CultureInfo.InvariantCulture), String.IsNullOrEmpty(citySelected.CityName) ? "" : citySelected.CityName
             //                  , cloneVehicle.DealershipName, cloneVehicle.StockNumber, cloneVehicle.ModelYear + "  " + cloneVehicle.Make + "  " + cloneVehicle.Model
             //                  ,cloneVehicle.CLPostingId.ToString(CultureInfo.InvariantCulture),cloneVehicle.CraigslistAccountName,cloneVehicle.CraigslistAccountPassword
             //                  ,cloneVehicle.SalePrice,cloneVehicle.ListingId.ToString(CultureInfo.InvariantCulture),cloneVehicle.Vin
             //                  ,totalImage,cloneVehicle.AddtionalTitle};

             //                   var listViewItem = new ListViewItem(row);

             //                   lvInventory.Items.Add(listViewItem);

             //                   var textPostingAds = Convert.ToInt32(lblPostingAds.Text.Substring(lblPostingAds.Text.IndexOf("/", System.StringComparison.Ordinal) + 1).Trim()) + 1;

             //                   var textRenewAds = Convert.ToInt32(lblRenewAds.Text.Substring(lblRenewAds.Text.IndexOf("/", System.StringComparison.Ordinal) + 1).Trim()) - 1;

             //                   lblPostingAds.Text = "Posting Ads = " + _postingAds + " / " + textPostingAds;

             //                   lblRenewAds.Text = "Renewing Ads = " + _renewAds + " / " + textRenewAds;

             //                   _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);

             //               }
             //               else
             //               {
                             
             //                   //timerPostAccount.Interval =35000;
             //                   //IT NEVER HAPPENS
             //                   if (vehicle.AutoID == 1)
             //                   {
             //                       RunPatternForRenew();
             //                   }

             //                   else 
             //                   {

             //                       if (txtCuEmail.Text.Equals(vehicle.CraigslistAccountName))
             //                       {

             //                           RunPatternForRenew();
             //                       }
             //                       else
             //                       {
             //                           CraigslistLoginByComputerAccountAfterFirstTime();

             //                           RunPatternForRenew();
             //                       }
             //                   }
                               
             //               }

             //           }
             //           else
             //           {
                            
             //               if (_browser == null)
             //               {
             //                   _browser =
             //                       new IE(clsVariables.currentComputer.CityList.First(
             //                           x => x.CityID == vehicle.PostingCityId).
             //                                  CraigsListCityURL +
             //                              "ctd/");

             //               }
             //               else
             //               {

             //                   _browser.GoTo(
             //                       clsVariables.currentComputer.CityList.First(x => x.CityID == vehicle.PostingCityId).
             //                           CraigsListCityURL +
             //                       "ctd/");
             //               }


                            
             //               _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);

             //               //_browser.TextField(Find.ById("query")).Value =
             //               //    vehicle.CLPostingId.ToString(CultureInfo.InvariantCulture);

             //               //_browser.RadioButton(Find.ByTitle("search the entire posting")).Checked = true;

             //               //_browser.Button(Find.ByValue("Search")).Click();

             //               _browser.GoTo(
             //vehicle.CraigslistCityUrl + "search/ctd?zoomToPosting=&query=" + vehicle.CLPostingId + "&srchType=A&minAsk=&maxAsk=");
                                                    
             //               if (_browser.Html.Contains("Found: "))
             //               {
             //                   System.Threading.Thread.Sleep(2000);

             //                   CraigslistLoginByComputerAccountAfterFirstTime();

             //                   _browser.GoTo("https://post.craigslist.org/manage/" + vehicle.CLPostingId);

             //                   if (_browser.Html.Contains("There appears to be a problem with this posting"))
             //                   {
                                   
             //                       vehicle.CLPostingId = 0;

             //                       _subMasterVehicleList.First(x => x.AutoID == vehicle.AutoID).CLPostingId = 0;

             //                       clsVariables.currentMasterVehicleList.First(x => x.AutoID == vehicle.AutoID).CLPostingId
             //                           = 0;

                                  
             //                   }
                                

             //               }
             //               else
             //               {
             //                  SQLHelper.DeleteCurrentTracking(vehicle.AdTrackingId);

             //                   vehicle.CLPostingId = 0;

             //                   _subMasterVehicleList.First(x => x.AutoID == vehicle.AutoID).CLPostingId = 0;

             //                   clsVariables.currentMasterVehicleList.First(x => x.AutoID == vehicle.AutoID).CLPostingId
             //                       = 0;

                              
                               
             //               }

                          

             //               if (vehicle.CLPostingId == 0)
             //               {

             //                   //timerPostAccount.Interval = 4000;


             //                   RemoveItemFromFromAutoId(vehicle.AutoID);

                                
             //                   var cloneVehicle = (WhitmanEntepriseMasterVehicleInfo)vehicle.Clone();

             //                   cloneVehicle.AutoID = clsVariables.currentMasterVehicleList.Max(x => x.AutoID) + 1;
             //                   cloneVehicle.CLPostingId = 0;

             //                   cloneVehicle.CraigslistAccountName = "";

             //                   cloneVehicle.CraigslistAccountPassword = "";
             //                   _subMasterVehicleList.RemoveAt(0);

             //                   _subMasterVehicleList.Add(cloneVehicle);

             //                   clsVariables.currentMasterVehicleList.Add(cloneVehicle);

             //                   var citySelected = clsVariables.currentComputer.CityList.First(x => x.CityID == cloneVehicle.PostingCityId);

             //                   string tmp = cloneVehicle.CarImageUrl;

             //                   string totalImage = "";

             //                   if (String.IsNullOrEmpty(tmp))
             //                       totalImage = "0";
             //                   else
             //                   {
             //                       string[] totalImages = tmp.Split(new String[] { "|", "," }, StringSplitOptions.RemoveEmptyEntries);
             //                       totalImage = totalImages.Count().ToString(CultureInfo.InvariantCulture);

             //                   }

             //                   string[] row = { cloneVehicle.AutoID.ToString(CultureInfo.InvariantCulture), String.IsNullOrEmpty(citySelected.CityName) ? "" : citySelected.CityName
             //                  , cloneVehicle.DealershipName, cloneVehicle.StockNumber, cloneVehicle.ModelYear + "  " + cloneVehicle.Make + "  " + cloneVehicle.Model
             //                  ,cloneVehicle.CLPostingId.ToString(CultureInfo.InvariantCulture),cloneVehicle.CraigslistAccountName,cloneVehicle.CraigslistAccountPassword
             //                  ,cloneVehicle.SalePrice,cloneVehicle.ListingId.ToString(CultureInfo.InvariantCulture),cloneVehicle.Vin
             //                  ,totalImage,cloneVehicle.AddtionalTitle};

             //                   var listViewItem = new ListViewItem(row);

             //                   lvInventory.Items.Add(listViewItem);



             //                   var textPostingAds = Convert.ToInt32(lblPostingAds.Text.Substring(lblPostingAds.Text.IndexOf("/", System.StringComparison.Ordinal) + 1).Trim()) + 1;

             //                   var textRenewAds = Convert.ToInt32(lblRenewAds.Text.Substring(lblRenewAds.Text.IndexOf("/", System.StringComparison.Ordinal) + 1).Trim()) - 1;

             //                   lblPostingAds.Text = "Posting Ads = " + _postingAds + " / " + textPostingAds;

             //                   lblRenewAds.Text = "Renewing Ads = " + _renewAds + " / " + textRenewAds;

             //                   _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);

             //               }
             //               else
             //               {
             //                   if (txtCuEmail.Text.Equals(vehicle.CraigslistAccountName))
             //                   {
             //                       RunPatternForRenew();
             //                   }
             //                   else
             //                   {
             //                       CraigslistLoginByComputerAccountAfterFirstTime();

             //                       RunPatternForRenew();
             //                   }

             //               }



             //           }


             //       }

                    //else
                    //{
                        //timerPostAccount.Interval = 360000;



                        //if (clsVariables.currentMasterVehicleList.Any(x => x.AutoID == vehicle.AutoID-1) && clsVariables.currentMasterVehicleList.First(x => x.AutoID == vehicle.AutoID-1).CLPostingId >0)
                        //{

                        //    if (_browser == null)
                        //    {
                        //        _browser = new IE("https://accounts.craigslist.org/");

                        //    }
                        //    else
                        //    {
                        //        _browser.ClearCache();

                        //        _browser.ClearCookies();

                        //        _browser.Close();

                        //        _browser = null;

                        //        System.Threading.Thread.Sleep(2000);

                        //        _browser = new IE("https://accounts.craigslist.org/");


                        //        if (IsAlreadyLogin())
                        //        {
                        //            System.Threading.Thread.Sleep(2000);

                        //            _browser.Span(Find.ById("ef")).Links.First().Click();

                        //            System.Threading.Thread.Sleep(3000);

                        //        }

                        //    }
    
                        //}



                    if (clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).IntervalofAds > 0)
                    {
                        RunPatternByComputerAccount();
                    }
                    else
                    {
                        int currentPostion =
                            clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).Position;



                        clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).IntervalofAds =
                            Convert.ToInt32(
                                System.Configuration.ConfigurationManager.AppSettings["IntervalOfAds"].ToString(
                                    CultureInfo.InvariantCulture));

                        clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).isCurrentlyUsed = false;

                        if (currentPostion == clsVariables.computeremailaccountList.Count)
                            clsVariables.computeremailaccountList.First(t => t.Position == 1).isCurrentlyUsed = true;
                        else
                            clsVariables.computeremailaccountList.First(t => t.Position == currentPostion + 1).
                                isCurrentlyUsed = true;


                        //TESTING

                        txtIP.Text = clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).Proxy;

                        txtCuEmail.Text =
                            clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).CraigslistAccount;

                        txtCuPhone.Text =
                            clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).CraigsAccountPhoneNumber;

                        txtCuPass.Text =
                            clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).CraigsListPassword;


                        if (_browser != null)
                        {

                            _browser.ClearCache();

                            _browser.ClearCookies();

                            _browser.Close();

                            _browser = null;

                            System.Threading.Thread.Sleep(2000);

                            _browser = new IE("https://accounts.craigslist.org/");

                        }

                        if (_browser != null)
                        {

                            _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);


                            if (IsAlreadyLogin())
                            {
                                System.Threading.Thread.Sleep(2000);

                                if (_browser.Span(Find.ById("ef")) != null)
                                {

                                    _browser.Span(Find.ById("ef")).Links.First().Click();
                                }

                                System.Threading.Thread.Sleep(3000);

                            }
                        }



                        try
                        {
                            //var networkManagement = new NetworkManagement();

                            //    networkManagement.SetIP(clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed));

                            System.Threading.Thread.Sleep(5000);

                            CraigslistLoginByComputerAccount();
                        }
                        catch (Exception etx)
                        {

                            throw new Exception(etx.Message);
                        }
                    }


                }
                else
                {
                  
                    timerPostAccount.Enabled = false;

                    timerPause.Enabled = false;

                    _browser.ClearCache();

                    _browser.ClearCookies();


                    _browser.Close();

                    _browser = null;

                    string body = "Computer " + _computerId +
                                  " was finished. Please reload this computer, so you will never miss any car. Message is approved by Travis";

                    try
                    {
                        EmailHelper.SendEmail(new MailAddress("vinclapp@vincontrol.com"), "Finished Schedule",
                                          body);

                    }
                    catch (Exception)
                    {
                        
                        
                    }

                    
                    lblLastPost.Text = "FINISHED!!!";

                    timerPostAccount.Enabled = false;

                    Refresh();

                    _flags = WinAPI.AW_ACTIVATE | WinAPI.AW_HOR_POSITIVE | WinAPI.AW_SLIDE;

                    lblCount.Visible = false;

                    _animationForm = new AnimationForm(1500, _flags);

                    _animationForm.Show();

                    _animationForm.Activate();

                    _animationForm.Focus();



                }


            }
            catch (Exception ex)
            {
                //string body = "Error = " + ex.Message + ex.Source + ex.InnerException + ex.TargetSite + ex.StackTrace +
                //              "**************************************************************************************" + _subMasterVehicleList.ElementAt(0).ListingId;


                if (_subMasterVehicleList.Any())
                    _subMasterVehicleList.RemoveAt(0);
                timerPostAccount.Enabled = true;

                throw new Exception(ex.Message);
               

                
            }

        }

        private void RunPatternForRenew()
        {
            try
            {
                timerPostAccount.Enabled = true;

                var vehicle = _subMasterVehicleList.ElementAt(0);

                if (vehicle.CLPostingId == 0)
                {
                    timerPostAccount.Interval = 5000;
                    
                    RemoveItemFromFromAutoId(vehicle.AutoID);
                  
                    var cloneVehicle = (WhitmanEntepriseMasterVehicleInfo)vehicle.Clone();

                    cloneVehicle.AutoID = clsVariables.currentMasterVehicleList.Max(x => x.AutoID) + 1;

                    cloneVehicle.CLPostingId = 0;

                    cloneVehicle.CraigslistAccountName = "";

                    cloneVehicle.CraigslistAccountPassword = "";
                    
                    _subMasterVehicleList.RemoveAt(0);

                    _subMasterVehicleList.Add(cloneVehicle);

                    clsVariables.currentMasterVehicleList.Add(cloneVehicle);

                    var citySelected = clsVariables.currentComputer.CityList.First(x => x.CityID == cloneVehicle.PostingCityId);

                    string tmp = cloneVehicle.CarImageUrl;

                    string totalImage = "";

                    if (String.IsNullOrEmpty(tmp))
                        totalImage = "0";
                    else
                    {
                        string[] totalImages = tmp.Split(new String[] { "|", "," }, StringSplitOptions.RemoveEmptyEntries);
                        totalImage = totalImages.Count().ToString(CultureInfo.InvariantCulture);

                    }

                    string[] row = { cloneVehicle.AutoID.ToString(CultureInfo.InvariantCulture), String.IsNullOrEmpty(citySelected.CityName) ? "" : citySelected.CityName
                               , cloneVehicle.DealershipName, cloneVehicle.StockNumber, cloneVehicle.ModelYear + "  " + cloneVehicle.Make + "  " + cloneVehicle.Model
                               ,cloneVehicle.CLPostingId.ToString(CultureInfo.InvariantCulture),cloneVehicle.CraigslistAccountName,cloneVehicle.CraigslistAccountPassword
                               ,cloneVehicle.SalePrice,cloneVehicle.ListingId.ToString(CultureInfo.InvariantCulture),cloneVehicle.Vin
                               ,totalImage,cloneVehicle.AddtionalTitle};

                    var listViewItem = new ListViewItem(row);

                    lvInventory.Items.Add(listViewItem);

                    var textPostingAds = Convert.ToInt32(lblPostingAds.Text.Substring(lblPostingAds.Text.IndexOf("/", System.StringComparison.Ordinal) + 1).Trim()) + 1;

                    var textRenewAds = Convert.ToInt32(lblRenewAds.Text.Substring(lblRenewAds.Text.IndexOf("/", System.StringComparison.Ordinal) + 1).Trim()) - 1;

                    lblPostingAds.Text = "Posting Ads = " + _postingAds + " / " + textPostingAds;

                    lblRenewAds.Text = "Renewing Ads = " + _renewAds + " / " + textRenewAds;

                    _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);
                }
                else
                {

                    if (_browser.Buttons.Any(x => x.Value == "Renew this Posting"))
                    {
                        timerPostAccount.Interval = 35000;

                        System.Threading.Thread.Sleep(1000);

                        _browser.Button(Find.ByValue("Renew this Posting")).Click();

                        SQLHelper.UpdateRenew(vehicle.AdTrackingId);

                        System.Threading.Thread.Sleep(4000);
                        

                        if (vehicle.DealerId == 3738 || vehicle.DealerId == 113738 || vehicle.DealerId == 15896 || vehicle.DealerId == 11828 || vehicle.DealerId == 7180 || vehicle.DealerId == 2650 || vehicle.DealerId == 44670)
                        {
                            if (!String.IsNullOrEmpty(vehicle.CraigslistUrl))
                            {

                                if (_browser == null)
                                {
                                    _browser = new IE(vehicle.CraigslistUrl);
                                }
                                else
                                {
                                    _browser.GoTo(vehicle.CraigslistUrl);
                                }


                                if (_browser.Link(Find.ByIndex(10)) != null)
                                {

                                    if (!_browser.Link(Find.ByIndex(10)).Url.Contains("vinlineup"))
                                    {
                                        _browser.Link(Find.ByIndex(10)).ClickNoWait();
                                        System.Threading.Thread.Sleep(2000);
                                       
                                    }
                                }
                            }
                        }

                        HighlightItemInListViewFromAutoId(vehicle.AutoID);

                        _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);

                        _renewAds++;

                        if (_subMasterVehicleList.Any())
                            _subMasterVehicleList.RemoveAt(0);


                        lblLastPost.Text = DateTime.Now.ToShortTimeString();

                        lblCount.Text = "LEFT = " +
                                        _subMasterVehicleList.Count.ToString(CultureInfo.InvariantCulture);

                        lblRenewAds.Text = "Renewing Ads = " + _renewAds + " / " +
                                           clsVariables.currentMasterVehicleList.Count(x => x.CLPostingId > 0);

                        if (_subMasterVehicleList.Count == 0)
                        {
                            lblLastPost.Text = "FINISHED!!!";

                            timerPostAccount.Enabled = false;

                            Refresh();
                         
                            string body = "Computer " + _computerId +
                                          " was finished. Please reload this computer, so you will never miss any car. Message is approved by Travis";

                            try
                            {
                                EmailHelper.SendEmail(new MailAddress("vinclapp@vincontrol.com"), "Finished Schedule",
                                                  body);
                            }
                            catch (Exception)
                            {
                                
                                
                            }

                            
                        }
                        
                    }
                    else
                    {

                         timerPostAccount.Interval = 5000;

                         RemoveItemFromFromAutoId(vehicle.AutoID);
                       
                        var cloneVehicle = (WhitmanEntepriseMasterVehicleInfo)vehicle.Clone();

                        cloneVehicle.AutoID = clsVariables.currentMasterVehicleList.Max(x => x.AutoID) + 1;

                        cloneVehicle.CLPostingId = 0;

                        cloneVehicle.CraigslistAccountName = "";

                        cloneVehicle.CraigslistAccountPassword = "";

                        _subMasterVehicleList.RemoveAt(0);
                        
                        _subMasterVehicleList.Add(cloneVehicle);

                  
                        clsVariables.currentMasterVehicleList.Add(cloneVehicle);

                        var citySelected = clsVariables.currentComputer.CityList.First(x => x.CityID == cloneVehicle.PostingCityId);

                        string tmp = cloneVehicle.CarImageUrl;

                        string totalImage = "";

                        if (String.IsNullOrEmpty(tmp))
                            totalImage = "0";
                        else
                        {
                            string[] totalImages = tmp.Split(new String[] { "|", "," }, StringSplitOptions.RemoveEmptyEntries);
                            totalImage = totalImages.Count().ToString(CultureInfo.InvariantCulture);

                        }

                        string[] row = { cloneVehicle.AutoID.ToString(CultureInfo.InvariantCulture), String.IsNullOrEmpty(citySelected.CityName) ? "" : citySelected.CityName
                               , cloneVehicle.DealershipName, cloneVehicle.StockNumber, cloneVehicle.ModelYear + "  " + cloneVehicle.Make + "  " + cloneVehicle.Model
                               ,cloneVehicle.CLPostingId.ToString(CultureInfo.InvariantCulture),cloneVehicle.CraigslistAccountName,cloneVehicle.CraigslistAccountPassword
                               ,cloneVehicle.SalePrice,cloneVehicle.ListingId.ToString(CultureInfo.InvariantCulture),cloneVehicle.Vin
                               ,totalImage,cloneVehicle.AddtionalTitle};

                        var listViewItem = new ListViewItem(row);

                        lvInventory.Items.Add(listViewItem);


                        var textPostingAds = Convert.ToInt32(lblPostingAds.Text.Substring(lblPostingAds.Text.IndexOf("/", System.StringComparison.Ordinal) + 1).Trim()) + 1;

                        var textRenewAds = Convert.ToInt32(lblRenewAds.Text.Substring(lblRenewAds.Text.IndexOf("/", System.StringComparison.Ordinal) + 1).Trim()) - 1;

                        lblPostingAds.Text = "Posting Ads = " + _postingAds + " / " + textPostingAds;

                        lblRenewAds.Text = "Renewing Ads = " + _renewAds + " / " + textRenewAds;

                        _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);
                    }


                }
            }
            catch (Exception ex)
            {

                //string body = "Error FROM RENEW = " + ex.Message + ex.Source + ex.InnerException + ex.TargetSite + ex.StackTrace +
                //              "**************************************************************************************" + _subMasterVehicleList.ElementAt(0).ListingId + " at computer " + _computerId;
                if (_subMasterVehicleList.Any())
                    _subMasterVehicleList.RemoveAt(0);
                timerPostAccount.Enabled = true;

            
            }
          

        }

        private void RunPatternByComputerAccount()
        {
            try
            {
             

                var vehicle = _subMasterVehicleList.ElementAt(0);

                var citySelected =
                    clsVariables.currentComputer.CityList.First(x => x.CityID == vehicle.PostingCityId);

                string title = ComputerAccountHelper.GenerateCraiglistTitleByComputerAccount(vehicle);

                string salePrice = "";

                if (!String.IsNullOrEmpty(vehicle.SalePrice))
                {


                    double price;

                    bool validPrice = Double.TryParse(vehicle.SalePrice, out price);

                    validPrice = validPrice && (price > 0);

                    if (validPrice)

                        salePrice = price.ToString(CultureInfo.InvariantCulture);


                }

                string index = "0";

                if (citySelected != null && citySelected.SubCity)
                    index = citySelected.CLIndex.ToString(CultureInfo.InvariantCulture);

                System.Threading.Thread.Sleep(1000);

                /////GO TO CRAIGLIST PAGE

                if (_browser == null)
                {
                    _browser = new IE(citySelected.CraigsListCityURL);

                }
                else
                {

                    _browser.GoTo(citySelected.CraigsListCityURL);


                }
                _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);

                //GO TO POST CLASSIFIED

                _browser.Link(Find.ByIndex(1)).Click();

                System.Threading.Thread.Sleep(1000);

                if (IsAlreadyLogin())
                {

                    //GO TO TYPE

                    _browser.RadioButton(Find.ByValue("fsd")).Checked = true;

                    System.Threading.Thread.Sleep(1000);

                    _browser.RadioButton(Find.ByValue("146")).Checked = true;

                    System.Threading.Thread.Sleep(1000);

                    //SUB AREA
                    if (citySelected.SubCity)
                    {
                        _browser.RadioButton(Find.ByValue(index)).Checked = true;

                    }

                    if (vehicle.CraigslistCityUrl.Equals("http://sfbay.craigslist.org/"))
                    {
                        _browser.RadioButton(Find.ByValue("0")).Checked = true;

                    }

                    //CONTENT
                    System.Threading.Thread.Sleep(1000);

                    //string nameTitle = _browser.Div(Find.ByClass("title row")).TextField(Find.ByClass("req")).Name;

                    _browser.TextField(Find.ById("PostingTitle")).Value = title;

                    //_browser.RadioButton(Find.ById("A")).Checked = true;

                    _browser.TextField(Find.ById("Ask")).Value = vehicle.Price && !String.IsNullOrEmpty(salePrice)
                                                                      ? salePrice
                                                                      : "";

                    _browser.TextField(Find.ById("GeographicArea")).Value = String.IsNullOrEmpty(vehicle.AddtionalTitle)
                                    ? citySelected.CityName
                                    : CommonHelper.TrimString(vehicle.AddtionalTitle, 40);

                    //_browser.TextField(Find.ById("postal_code")).Value = vehicle.ZipCode;

                    System.Threading.Thread.Sleep(1000);

                    //if (vehicle.DealerId == 10215 || vehicle.DealerId == 50000 || vehicle.DealerId == 50001)
                    //{
                    //    //_browser.TextField(p => p.MaxLength == 40).Value = !String.IsNullOrEmpty(vehicle.SalePrice)
                    //    //                                                       ? vehicle.SalePrice
                    //    //                                                       : citySelected.CityName;

                    //    _browser.TextField(Find.ById("Ask")).Value = !String.IsNullOrEmpty(vehicle.SalePrice)
                    //                                                          ? vehicle.SalePrice
                    //                                                          : citySelected.CityName;
                    //}
                    ////else   if (vehicle.DealerId == 29713)
                    ////{

                    ////    _browser.TextField(p => p.MaxLength == 40).Value =
                    ////        String.IsNullOrEmpty(vehicle.AddtionalTitle)
                    ////            ?   citySelected.CityName
                    ////            : CommonHelper.TrimString(vehicle.AddtionalTitle, 40);
                    ////}
                    //else
                    //{

                    //    //_browser.TextField(Find.ByClass("s nreq")).Value = vehicle.Price && !String.IsNullOrEmpty(salePrice)
                    //    //                                                  ? salePrice
                    //    //                                                  : "";

                    //    _browser.TextField(Find.ById("Ask")).Value = vehicle.Price && !String.IsNullOrEmpty(salePrice)
                    //                                                    ? salePrice
                    //                                                    : "";
                    //    if (vehicle.DealerId == 1200)
                    //        _browser.TextField(p => p.MaxLength == 40).Value =
                    //            String.IsNullOrEmpty(vehicle.AddtionalTitle)
                    //                ? citySelected.CityName
                    //                : CommonHelper.TrimString(vehicle.AddtionalTitle, 40);
                    //    else if (vehicle.DealerId == 115896)
                    //    {
                    //        _browser.TextField(p => p.MaxLength == 40).Value = citySelected.CityName + ", " +
                    //                                                           vehicle.State;
                    //    }

                    //    else
                    //    {
                    //        _browser.TextField(p => p.MaxLength == 40).Value = citySelected.CityName;
                    //    }

                    //}

                    //string tmp = _browser.Span(Find.ById("pbctr")).NextSibling.NextSibling.NextSibling.Name;

                    

                    _browser.TextField(Find.ById("PostingBody")).Value =
                        ComputerAccountHelper.GenerateCraiglistContentBySimpleText(vehicle);

                    System.Threading.Thread.Sleep(2000);

                    _browser.SelectList(Find.ById("auto_year"))
                        .Options.FirstOrDefault(x => x.Value == vehicle.ModelYear).Select();


                    _browser.TextField(Find.ById("auto_make_model")).Value =
                        vehicle.Make + " " + vehicle.Model;

                    _browser.TextField(Find.ById("auto_miles")).Value =
                        vehicle.Mileage;

                    _browser.TextField(Find.ById("auto_vin")).Value =
                  vehicle.Vin;

                    if (vehicle.Tranmission.ToLower().Contains("automatic"))
                        _browser.CheckBox(Find.ById("auto_trans_auto")).Checked = true;
                    else if (vehicle.Tranmission.ToLower().Contains("manual"))

                        _browser.CheckBox(Find.ById("auto_trans_manual")).Checked = true;
                    else
                    {
                        _browser.CheckBox(Find.ById("auto_trans_manual")).Checked = true;
                        _browser.CheckBox(Find.ById("auto_trans_auto")).Checked = true;
                    }

                    ////ADD ON MAP

                    //_browser.TextField(Find.ById("xstreet0")).Value = vehicle.StreetAddress;

                    //System.Threading.Thread.Sleep(1000);

                    //_browser.TextField(Find.ById("city")).Value = vehicle.City;

                    //System.Threading.Thread.Sleep(1000);

                    //_browser.TextField(Find.ById("region")).Value = vehicle.State;

                    //System.Threading.Thread.Sleep(1000);

                    //_browser.CheckBox(Find.ByName("outsideContactOK")).Checked = true;


                    //System.Threading.Thread.Sleep(1000);
               

                    ////////ADD ON MAP

                    _browser.Button(Find.ByName("go")).Click();


                    ////ADD ON MAP

                    //System.Threading.Thread.Sleep(1000);

                    //_browser.Button(Find.ByClass("continue bigbutton")).Click();
                    

                    System.Threading.Thread.Sleep(3000);

                    _browser.Button(Find.ByValue("add image")).Click();

                    ImageModel  imageModel = ImageHelper.GenerateRunTimePhysicalImageByComputerAccount(vehicle);

                    System.Threading.Thread.Sleep(2000);

                    if (imageModel.PhysicalImageUrl.Any())
                    {
                        foreach (var uploadLocalImage in imageModel.PhysicalImageUrl)
                        {
                            _browser.FileUpload(Find.ByName("file")).Set(uploadLocalImage);

                            System.Threading.Thread.Sleep(6000);

                        }


                        _browser.Button(Find.ByValue("Done with Images")).Click();

                        System.Threading.Thread.Sleep(5000);

                        _browser.Button(Find.ByName("go")).Click();

                        var clStatus = detecStatusFromURL(_browser.Uri.AbsoluteUri);

                        if (clStatus == 1)
                        {


                            clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).IntervalofAds--;

                            timerPostAccount.Enabled = true;

                        }
                        else if (clStatus == 2 ||
                                 clStatus == 3)
                        {

                            HighlightItemInListViewFromAutoId(vehicle.AutoID);

                            clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).IntervalofAds--;
                       

                            timerPostAccount.Enabled = false;

                            timerPause.Enabled = true;

                            btnPause.Text = "UnPause";


                            if (_subMasterVehicleList.Any())
                                _subMasterVehicleList.RemoveAt(0);

                            if (clStatus == 2)
                            {
                                string phoneNumber =
                                    clsVariables.computeremailaccountList.First(x => x.isCurrentlyUsed).
                                                 CraigsAccountPhoneNumber;

                                string body =
                                    "Please check computer " + _computerId + ". You have a phone verification ----- " +
                                    clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).
                                                 CraigsAccountPhoneNumber;

                                try
                                {
                                    EmailHelper.SendEmail(new MailAddress("vinclapp@vincontrol.com"),
                                                          "EMAIL NOTIFICATION",
                                                          body);

                                }
                                catch (Exception)
                                {

                                }


                                if (!String.IsNullOrEmpty(phoneNumber))
                                {


                                    string firstPart = phoneNumber.Substring(0, 3);

                                    string secondPart =
                                        phoneNumber.Substring(
                                            phoneNumber.IndexOf("-", System.StringComparison.Ordinal) + 1, 3);

                                    string lastPart =
                                        phoneNumber.Substring(
                                            phoneNumber.LastIndexOf("-", System.StringComparison.Ordinal) + 1);

                                    _browser.TextFields[1].Value =
                                        firstPart;

                                    _browser.TextFields[2].Value =
                                        secondPart;

                                    _browser.TextFields.Last().Value =
                                        lastPart;

                                    _browser.RadioButton(Find.ByValue("voice")).Checked = true;

                                }
                            }
                            else
                            {

                                HighlightItemInListViewFromAutoId(vehicle.AutoID);

                                clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).IntervalofAds--;

                                string body =
                                    "Please check computer " + _computerId +
                                    ". You have an email verification for email account " +
                                    clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).
                                                 CraigslistAccount + "-----.Password=" +
                                    clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).
                                                 CraigsListPassword;

                                try
                                {
                                    EmailHelper.SendEmail(new MailAddress("vinclapp@vincontrol.com"),
                                                          "PHONE NOTIFICATION",
                                                          body);
                                }
                                catch (Exception)
                                {

                                }


                            }

                            _tForm = new PhoneVerificationForm(detecStatusFromURL(_browser.Uri.AbsoluteUri),
                                                               "ScheduleMode");

                            _tForm.Location = new Point(0, 0);
                            _tForm.Show();
                            _tForm.Activate();
                        }
                        else
                        {
                            clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).IntervalofAds--;

                            _postingAds++;

                            timerPostAccount.Enabled = true;

                            lblPostingAds.Text = "Posting Ads = " + _postingAds + " / " +
                                                 clsVariables.currentMasterVehicleList.Count(x => x.CLPostingId == 0);


                            HighlightItemInListViewFromAutoId(vehicle.AutoID);


                            string htmlLink = _browser.Links[_browser.Links.Count - 2].Url;

                            var clModel = new CraigsListTrackingModel()
                                {
                                    ListingId = vehicle.ListingId,
                                    CityId = vehicle.PostingCityId,
                                    DealerId = vehicle.DealerId,
                                    EmailAccount =
                                        clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).CraigslistAccount,
                                    Computer = _computerId
                                };

                            SQLHelper.AddNewTracking(clModel, htmlLink);

                            _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);

                            System.Threading.Thread.Sleep(4000);

                            if (_subMasterVehicleList.Any())
                            {
                                _subMasterVehicleList.RemoveAt(0);

                                if (_subMasterVehicleList.Count == 0)
                                {
                                    timerPostAccount.Enabled = false;

                                    timerPause.Enabled = false;

                                    _browser.ClearCookies();

                                    _browser.ClearCache();

                                    _browser.Close();

                                    _browser = null;


                                    string body = "Computer " + _computerId +
                                                  " was finished. Please reload this computer, so you will never miss any car. Message is approved by Travis";

                                    try
                                    {
                                        EmailHelper.SendEmail(new MailAddress("vinclapp@vincontrol.com"),
                                                              "Finished Schedule",
                                                              body);

                                    }
                                    catch (Exception)
                                    {


                                    }

                                    lblLastPost.Text = "FINISHED!!!";

                                    timerPostAccount.Enabled = false;

                                    Refresh();

                                    _flags = WinAPI.AW_ACTIVATE | WinAPI.AW_HOR_POSITIVE | WinAPI.AW_SLIDE;

                                    lblCount.Visible = false;

                                    _animationForm = new AnimationForm(1500, _flags);

                                    _animationForm.Show();

                                    _animationForm.Activate();

                                    _animationForm.Focus();
                                }

                                
                            }


                            lblLastPost.Visible = true;

                            lblLastPost.Text = DateTime.Now.ToShortTimeString();

                            lblCount.Text = "LEFT = " +
                                            _subMasterVehicleList.Count.ToString(CultureInfo.InvariantCulture);


                        }




                    }
                    else
                    {

                     _subMasterVehicleList.RemoveAt(0);
                        timerPostAccount.Enabled = true;


                    }
                }
                else
                {

                    CraigslistLoginByComputerAccount();
                }





            }


            catch
                (Exception
                    ex)
            {
                string body = "Error = " + ex.Message + ex.Source + ex.InnerException + ex.TargetSite +
                              ex.StackTrace +
                              "**************************************************************************************" +
                              _subMasterVehicleList.ElementAt(0).ListingId + "----" +
                              _subMasterVehicleList.ElementAt(0).DealerId;
                if (_subMasterVehicleList.Any())
                    _subMasterVehicleList.RemoveAt(0);

                timerPostAccount.Enabled = true;

        
            }

        }

        private bool IsAlreadyLogin()
        {


            bool flagLogin = false;

            if (_browser.Links.Any())
            {

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
            }

            return flagLogin;

        }
    
        private int detecStatusFromURL(string URL)
        {
            /*
             * 0:NORMAL
             * 1:RAPID
             * 2:VERIFY PHONE NUMBER
             * 
             */
            int result = 0;
            if (URL.Contains("s=postcount"))
                result = 1;
            else if (URL.Contains("s=pn"))
                result = 2;
            else if (URL.Contains("s=mailoop"))
                result = 3;
            return result;


        }

        private void BtnPauseClick(object sender, EventArgs e)
        {
            if (btnPause.Text.Equals("UnPause"))
            {
                timerPostAccount.Enabled = true;
                timerPostAccount.Interval = 250000;
                timerPause.Enabled = false;
                btnPause.BackColor = Color.Gray;
                btnPause.Text = "Pause";

                try
                {
                    int currentPostion =
                         clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).Position;

                    clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).IntervalofAds =
                        Convert.ToInt32(
                            System.Configuration.ConfigurationManager.AppSettings["IntervalOfAds"].ToString(
                                CultureInfo.InvariantCulture));

                    clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).isCurrentlyUsed = false;

                    if (currentPostion == clsVariables.computeremailaccountList.Count)
                        clsVariables.computeremailaccountList.First(t => t.Position == 1).isCurrentlyUsed = true;
                    else
                        clsVariables.computeremailaccountList.First(t => t.Position == currentPostion + 1).
                            isCurrentlyUsed = true;
       
                    //TESTING
                    

                    txtIP.Text = clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).Proxy;

                    txtCuEmail.Text = clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).CraigslistAccount;

                    txtCuPhone.Text = clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).CraigsAccountPhoneNumber;

                    txtCuPass.Text = clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).CraigsListPassword;

               

                    if (_browser != null)
                    {
                        _browser.ClearCache();

                        _browser.ClearCookies();

                        _browser.ForceClose();

                        _browser = null;

                        System.Threading.Thread.Sleep(2000);
                        
                        

                    }

                  

                    if (_browser != null)
                    {

                        _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);


                        if (IsAlreadyLogin())
                        {
                            System.Threading.Thread.Sleep(2000);

                            if (_browser.Span(Find.ById("ef")) != null)
                            {

                                _browser.Span(Find.ById("ef")).Links.First().Click();
                            }

                            System.Threading.Thread.Sleep(3000);

                        }
                    }

                    var ieProcesses = Process.GetProcessesByName("iexplore");

                    if (ieProcesses.Any())
                    {
                        foreach (Process ie in ieProcesses)
                        {
                            ie.Kill();
                        }
                    }


                    //var networkManagement = new NetworkManagement();

                    //networkManagement.SetIP(clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed));

                    System.Threading.Thread.Sleep(8000);

                    CraigslistLoginByComputerAccount();
                }
                catch (Exception ex)
                {
                    
                    throw new Exception(ex.Message + ex.StackTrace);
                }

          


                //TimerPostAccountTick(sender,e);
            }
            else
            {
                timerPostAccount.Enabled = false;
                timerPause.Enabled = true;
                btnPause.Text = "UnPause";
            }

        }

        private void MainformFormClosing(object sender, FormClosingEventArgs e)
        {
            if (_browser != null)
            {

                _browser.ClearCache();

                _browser.ClearCookies();

                _browser.Close();

                _browser = null;

        
            }
        }

        private void BtnPostAcctClick(object sender, EventArgs e)
        {
            var dtCompareToday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var dtCompareNextDay = dtCompareToday.AddDays(1);

            if (clsVariables.CurrentProcessingDate >= dtCompareToday && clsVariables.CurrentProcessingDate < dtCompareNextDay)
            {
                clsVariables.CurrentProcessingDate = new DateTime();

                clsVariables.CurrentProcessingDate = DateTime.Now;
                if (_browser != null)
                {
                    _browser.ClearCache();

                    _browser.ClearCookies();

                    _browser.Close();

                    _browser = null;

                }


                btnPause.Enabled = true;

                _subMasterVehicleList = new List<WhitmanEntepriseMasterVehicleInfo>();

                int focusedIndex = lvInventory.FocusedItem.Index;


                _subMasterVehicleList = clsVariables.currentMasterVehicleList.GetRange(focusedIndex,
                                                                                      clsVariables.
                                                                                          currentMasterVehicleList.
                
                                                                                          Count - focusedIndex);
                
                if (_subMasterVehicleList.Any())
                {
                    lblCount.Text = "LEFT =" + _subMasterVehicleList.Count.ToString(CultureInfo.InvariantCulture);

                    //txtIP.Text = clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).Proxy;

                    //var networkManagement = new NetworkManagement();

                    //networkManagement.SetIP(clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).Proxy);

                    //System.Threading.Thread.Sleep(3000);

                    CraigslistLoginByComputerAccount();
                }
                else
                    MessageBox.Show("Please choose at least one car in inventory", "Message", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
            }
            else
            {
                clsVariables.CurrentProcessingDate = new DateTime();

                clsVariables.CurrentProcessingDate = DateTime.Now;

                BtnLoadPcClick(sender, e);

                if (_browser != null)
                {
                    _browser.ClearCache();

                    _browser.ClearCookies();

                    _browser.Close();

                    _browser = null;
                }
                btnPause.Enabled = true;

                _subMasterVehicleList = new List<WhitmanEntepriseMasterVehicleInfo>();

                int focusedIndex = lvInventory.FocusedItem.Index;


                _subMasterVehicleList = clsVariables.currentMasterVehicleList.GetRange(focusedIndex,
                                                                                      clsVariables.
                                                                                          currentMasterVehicleList.
                                                                                          Count - focusedIndex);
                if (_subMasterVehicleList.Any())
                {
                    lblCount.Text = "LEFT =" + _subMasterVehicleList.Count.ToString(CultureInfo.InvariantCulture);

                    //txtIP.Text = clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).Proxy;

                    //var networkManagement = new NetworkManagement();

                    //networkManagement.SetIP(clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).Proxy);

                    //System.Threading.Thread.Sleep(5000);

                    CraigslistLoginByComputerAccount();
                }
                else
                    MessageBox.Show("Please choose at least one car in inventory", "Message", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
            }
        }

        private void BtnBrandNewPostClick(object sender, EventArgs e)
        {
            var dtCompareToday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var dtCompareNextDay = dtCompareToday.AddDays(1);

            if (clsVariables.CurrentProcessingDate >= dtCompareToday && clsVariables.CurrentProcessingDate < dtCompareNextDay)
            {
                clsVariables.CurrentProcessingDate = new DateTime();

                clsVariables.CurrentProcessingDate = DateTime.Now;
                if (_browser != null)
                {
                    _browser.ClearCache();

                    _browser.ClearCookies();

                    _browser.Close();

                    _browser = null;
                }
                btnPause.Enabled = true;

                _subMasterVehicleList = new List<WhitmanEntepriseMasterVehicleInfo>();

                foreach (var tmp in clsVariables.currentMasterVehicleList)
                {
                    tmp.CLPostingId = 0;
                    tmp.CraigslistAccountName="";
                    tmp.CraigslistAccountPassword = "";
                }

                int focusedIndex = lvInventory.FocusedItem.Index;


                _subMasterVehicleList = clsVariables.currentMasterVehicleList.GetRange(focusedIndex,
                                                                                      clsVariables.
                                                                                          currentMasterVehicleList.
                                                                                          Count - focusedIndex);
                if (_subMasterVehicleList.Any())
                {
                    lblCount.Text = "LEFT =" + _subMasterVehicleList.Count.ToString(CultureInfo.InvariantCulture);

                    txtIP.Text = clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).Proxy;

                    //var networkManagement = new NetworkManagement();

                    //networkManagement.SetIP(clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed));

                    //System.Threading.Thread.Sleep(5000);
                    
                    //TESTING
                    CraigslistLoginByComputerAccount();

                    //RunPatternByComputerAccount();
                }
                else
                    MessageBox.Show("Please choose at least one car in inventory", "Message", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
            }
            else
            {
                clsVariables.CurrentProcessingDate = new DateTime();

                clsVariables.CurrentProcessingDate = DateTime.Now;

                BtnLoadPcClick(sender, e);

                if (_browser != null)
                {
                    _browser.ClearCache();

                    _browser.ClearCookies();

                    _browser.Close();

                    _browser = null;
                }
                btnPause.Enabled = true;

                _subMasterVehicleList = new List<WhitmanEntepriseMasterVehicleInfo>();

                int focusedIndex = lvInventory.FocusedItem.Index;
                foreach (var tmp in clsVariables.currentMasterVehicleList)
                {
                    tmp.CLPostingId = 0;
                    tmp.CraigslistAccountName = "";
                    tmp.CraigslistAccountPassword = "";
                }

                _subMasterVehicleList = clsVariables.currentMasterVehicleList.GetRange(focusedIndex,
                                                                                      clsVariables.
                                                                                          currentMasterVehicleList.
                                                                                          Count - focusedIndex);
                if (_subMasterVehicleList.Any())
                {
                    lblCount.Text = "LEFT =" + _subMasterVehicleList.Count.ToString(CultureInfo.InvariantCulture);

                    txtIP.Text = clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).Proxy;

                    //var networkManagement = new NetworkManagement();

                    //networkManagement.SetIP(clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed));

                    System.Threading.Thread.Sleep(5000);

                    CraigslistLoginByComputerAccount();

                    //RunPatternByComputerAccount();
                }
                else
                    MessageBox.Show("Please choose at least one car in inventory", "Message", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
            }
        }

        private void BtnReloadClick(object sender, EventArgs e)
        {
            bool flag;

            if (cbComputer.SelectedItem != null)
            {
                _computerId = (int)cbComputer.SelectedItem;
                flag = true;
            }
            else
            {

                flag = Int32.TryParse(cbComputer.Text, out _computerId);

                if (!flag)
                {
                    MessageBox.Show("You just entered a wrong schedule", "Critical Error", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    cbComputer.SelectedIndex = 0;
                }
                else
                {
                    if (clsVariables.computerList.All(x => x.ComputerId != _computerId))
                    {
                        flag = false;
                        MessageBox.Show("You just entered a wrong schedule", "Critical Error", MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                        cbComputer.SelectedIndex = 0;
                    }
                }
            }

            if (flag)
            {

                _postingAds = 0;

                _renewAds = 0;

                btnPostAcct.Enabled = true;

                btnBrandNewPost.Enabled = true;

                lblLastPost.Visible = true;

                lblLastPost.Text = DateTime.Now.ToShortTimeString();

                timerPostAccount.Enabled = false;

                _subMasterVehicleList = new List<WhitmanEntepriseMasterVehicleInfo>();

                pbPicLoad.Visible = true;

                clsVariables.Reload = true;

               clsVariables.CurrentProcessingDate = new DateTime();

                clsVariables.CurrentProcessingDate = DateTime.Now;
                
                this.Refresh();
                
                bgWorkerAccount.RunWorkerAsync();
            }
        }


        private void BtnLoadPcClick(object sender, EventArgs e)
        {
            bool flag = false;
            
            if (cbComputer.SelectedItem != null)
            {
                _computerId = (int)cbComputer.SelectedItem;
                flag = true;
            }
            else
            {

                flag = Int32.TryParse(cbComputer.Text, out _computerId);

                if (!flag)
                {
                    MessageBox.Show("You just entered a wrong schedule", "Critical Error", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    cbComputer.SelectedIndex = 0;
                }
                else
                {
                    if (clsVariables.computerList.All(x => x.ComputerId != _computerId))
                    {
                        flag = false;
                        MessageBox.Show("You just entered a wrong schedule", "Critical Error", MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                        cbComputer.SelectedIndex = 0;
                    }
                }
            }

            if (flag)
            {

                _postingAds = 0;

                _renewAds = 0;

                btnPostAcct.Enabled = true;

                btnBrandNewPost.Enabled = true;

                lblLastPost.Visible = true;

                lblLastPost.Text = DateTime.Now.ToShortTimeString();

                timerPostAccount.Enabled = false;

                _subMasterVehicleList = new List<WhitmanEntepriseMasterVehicleInfo>();

                pbPicLoad.Visible = true;

                clsVariables.Reload = false;

                clsVariables.ReloadRenew = false;

                clsVariables.CurrentProcessingDate = new DateTime();

                clsVariables.CurrentProcessingDate = DateTime.Now;
                
                this.Refresh();

       

                bgWorkerAccount.RunWorkerAsync();
            }

            

        }

        private void BgWorkerAccountDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SQLHelper.InitializeGlobalCurrentComputer(_computerId);
                SQLHelper.InitializeGiantInventory(_computerId);
               
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("An error occured while performing operation " + ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void UpdateListInventoryByComputerId()
        {
            lvInventory.Items.Clear();

            cbFilter.Items.Clear();

            foreach (var vehicle in clsVariables.currentMasterVehicleList)
            {
                var citySelected = clsVariables.currentComputer.CityList.First(x => x.CityID == vehicle.PostingCityId);

                string tmp = vehicle.CarImageUrl;

                string totalImage = "";

                if (String.IsNullOrEmpty(tmp))
                    totalImage = "0";
                else
                {
                    string[] totalImages = tmp.Split(new String[] { "|", "," }, StringSplitOptions.RemoveEmptyEntries);
                    totalImage = totalImages.Count().ToString(CultureInfo.InvariantCulture);

                }

                string[] row = { vehicle.AutoID.ToString(CultureInfo.InvariantCulture), String.IsNullOrEmpty(citySelected.CityName) ? "" : citySelected.CityName
                               , vehicle.DealershipName, vehicle.StockNumber, vehicle.ModelYear + "  " + vehicle.Make + "  " + vehicle.Model + " " + vehicle.Trim
                               ,vehicle.CLPostingId.ToString(CultureInfo.InvariantCulture),vehicle.CraigslistAccountName,vehicle.CraigslistAccountPassword
                               ,vehicle.SalePrice,vehicle.ListingId.ToString(CultureInfo.InvariantCulture),vehicle.Vin
                               ,totalImage,vehicle.AddtionalTitle};
                
                var listViewItem = new ListViewItem(row);

                lvInventory.Items.Add(listViewItem);

               
                
            }

            if (lvInventory.Items.Count > 0)
            {
                lvInventory.Items[0].Selected = true;
                lvInventory.Items[0].Focused = true;
            }

            foreach (var tmp in clsVariables.currentMasterVehicleList.Select(x => x.DealershipName).Distinct())
            {
                cbFilter.Items.Add(tmp);
            }
            if (cbFilter.Items.Count > 0)
                cbFilter.SelectedIndex = 0;

        }
        private void BgWorkerAccountRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                UpdateListInventoryByComputerId();

                pbPicLoad.Visible = false;

                lblPostingAds.Text = "Posting Ads = 0 / " + clsVariables.currentMasterVehicleList.Count(x => x.CLPostingId == 0);

                lblRenewAds.Text = "Renewing Ads = 0 / " + clsVariables.currentMasterVehicleList.Count(x => x.CLPostingId > 0);

                lblCount.Text = "LEFT = " + clsVariables.currentMasterVehicleList.Count;

                if (clsVariables.computeremailaccountList.Any())
                {

                    var emailAccount = clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed);

                    txtCuEmail.Text = emailAccount.CraigslistAccount;

                    txtCuPass.Text = emailAccount.CraigsListPassword;

                    txtCuPhone.Text = emailAccount.CraigsAccountPhoneNumber;

                    txtIP.Text = emailAccount.Proxy;

                    //MessageBox.Show(clsVariables.computeremailaccountList.Count.ToString());
                }

                this.Refresh();

            }
        }

        private void TimerPauseTick(object sender, EventArgs e)
        {
            if (btnPause.BackColor == Color.Blue)
            {
                btnPause.BackColor = Color.Red;
            }
            else
            {
                btnPause.BackColor = Color.Blue;
            }
        }

       private void RemoveItemFromFromAutoId(int autoId)
       {
        
           bool flag=false;
           ListViewItem selectedlvItem = null;

           foreach (ListViewItem lvItem in lvInventory.Items)
           {
               if (lvItem.SubItems[0].Text.Equals(autoId.ToString(CultureInfo.InvariantCulture)))
               {
                   selectedlvItem = lvItem;
                   flag = true;
                   break;
               }
           
           }
           if(flag)
               lvInventory.Items.Remove(selectedlvItem);
         

       }

       private void HighlightItemInListViewFromAutoId(int autoId)
       {


           foreach (ListViewItem lvItem in lvInventory.Items)
           {
               if (lvItem.SubItems[0].Text.Equals(autoId.ToString(CultureInfo.InvariantCulture)))
               {

                   lvItem.ForeColor = Color.Red;

                   break;
               }
           }

       }
         
       private void BtnFilterClick(object sender, EventArgs e)
       {
           pbPicLoad.Visible = true;

           bgFilter.RunWorkerAsync();
       }

       private void BgFilterDoWork(object sender, DoWorkEventArgs e)
       {
           try
           {
               var selectedDealer = (string)cbFilter.SelectedItem;

               _subMasterVehicleList=new List<WhitmanEntepriseMasterVehicleInfo>();

               clsVariables.currentMasterVehicleList.RemoveAll(x => x.DealershipName != selectedDealer);

           }
           catch (Exception)
           {
               //System.Windows.Forms.MessageBox.Show("An error occured while performing operation " + ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
           }
       }

       private void BgFilterRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
       {
           if (!e.Cancelled)
           {
               UpdateListInventoryByComputerId();

               pbPicLoad.Visible = false;

               lblPostingAds.Text = "Posting Ads = 0 / " + clsVariables.currentMasterVehicleList.Count(x => x.CLPostingId == 0);

               lblRenewAds.Text = "Renewing Ads = 0 / " + clsVariables.currentMasterVehicleList.Count(x => x.CLPostingId > 0);

               lblCount.Text = "LEFT = " + clsVariables.currentMasterVehicleList.Count;
               this.Refresh();

           }
       }

       private void BtnSkipClick(object sender, EventArgs e)
       {
           pbPicLoad.Visible = true;

           bgSkip.RunWorkerAsync();
       }

       private void BgSkipDoWork(object sender, DoWorkEventArgs e)
       {
           try
           {
               var selectedDealer = (string)cbFilter.SelectedItem;

               _subMasterVehicleList = new List<WhitmanEntepriseMasterVehicleInfo>();

               clsVariables.currentMasterVehicleList.RemoveAll(x => x.DealershipName == selectedDealer);

           }
           catch (Exception)
           {
              
           }
       }

       private void BgSkipRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
       {
           if (!e.Cancelled)
           {
               UpdateListInventoryByComputerId();

               pbPicLoad.Visible = false;

               lblPostingAds.Text = "Posting Ads = 0 / " + clsVariables.currentMasterVehicleList.Count(x => x.CLPostingId == 0);

               lblRenewAds.Text = "Renewing Ads = 0 / " + clsVariables.currentMasterVehicleList.Count(x => x.CLPostingId > 0);

               lblCount.Text = "LEFT = " + clsVariables.currentMasterVehicleList.Count;
               this.Refresh();

           }
       }

       private void btnReloadRenew_Click(object sender, EventArgs e)
       {
           bool flag;

           if (cbComputer.SelectedItem != null)
           {
               _computerId = (int)cbComputer.SelectedItem;
               flag = true;
           }
           else
           {

               flag = Int32.TryParse(cbComputer.Text, out _computerId);

               if (!flag)
               {
                   MessageBox.Show("You just entered a wrong schedule", "Critical Error", MessageBoxButtons.OK,
                                   MessageBoxIcon.Warning);
                   cbComputer.SelectedIndex = 0;
               }
               else
               {
                   if (clsVariables.computerList.All(x => x.ComputerId != _computerId))
                   {
                       flag = false;
                       MessageBox.Show("You just entered a wrong schedule", "Critical Error", MessageBoxButtons.OK,
                                       MessageBoxIcon.Warning);
                       cbComputer.SelectedIndex = 0;
                   }
               }
           }

           if (flag)
           {

               _postingAds = 0;

               _renewAds = 0;

               btnPostAcct.Enabled = true;

               btnBrandNewPost.Enabled = true;

               lblLastPost.Visible = true;

               lblLastPost.Text = DateTime.Now.ToShortTimeString();

               timerPostAccount.Enabled = false;

               _subMasterVehicleList = new List<WhitmanEntepriseMasterVehicleInfo>();

               pbPicLoad.Visible = true;

               clsVariables.Reload = false;

               clsVariables.ReloadRenew = true;

               clsVariables.CurrentProcessingDate = new DateTime();

               clsVariables.CurrentProcessingDate = DateTime.Now;
               
               this.Refresh();

               bgWorkerAccount.RunWorkerAsync();
           }
       }

       private void btnTestingPicture_Click(object sender, EventArgs e)
       {
           var vehicle = clsVariables.currentMasterVehicleList.ElementAt(0);

         
                    var squareRandom =
                        clsVariables.fullMasterVehicleList.Where(x => x.DealerId == vehicle.DealerId)
                                    .ToList()
                                    .GetDistinctRandom(4);

        var imagemodel=ImageHelper.GenerateRunTimePhysicalImageByComputerAccount(vehicle,squareRandom);

           //richTextBox1.Text = imagemodel.TestingHtml;

       }

       private void btnAdvanced_Click(object sender, EventArgs e)
       {
           if (clsVariables.fullMasterVehicleList == null || !clsVariables.fullMasterVehicleList.Any())
           {
               MessageBox.Show("You have to load schedule first", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
           }
           else
           {
               var filterForm = FilterForm.Instance(this);

               filterForm.Show();
           }
           
       }

       //private void rb4min_CheckedChanged(object sender, EventArgs e)
       //{
       //    if (rb4min.Checked)
       //    {
       //        timerPostAccount.Interval = 360000;
       //        clsVariables.DelayTimer = 360000;
       //        clsVariables.DelayTimerPostAccount = 360000;
       //    }
       //}

       //private void rb8min_CheckedChanged(object sender, EventArgs e)
       //{
       //    if (rb8min.Checked)
       //    {
       //        timerPostAccount.Interval = 480000;
       //        clsVariables.DelayTimer = 480000;
       //        clsVariables.DelayTimerPostAccount = 480000;
       //    }
       //}

   
       private void button1_Click_1(object sender, EventArgs e)
       {
           if (_browser == null)
           {
               _browser = new IE("https://login.yahoo.com/config/login_verify2?.intl=us&.src=ym");

           }
           else
           {
               _browser.ClearCache();

               _browser.ClearCookies();

               _browser.Close();

               _browser = null;

               System.Threading.Thread.Sleep(2000);

               _browser = new IE("https://login.yahoo.com/config/login_verify2?.intl=us&.src=ym");

           }

           _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);

           _browser.TextField(Find.ById("username")).TypeText("bradonlee247@yahoo.com");

           _browser.TextField(Find.ById("passwd")).TypeText("bachkhoa123");


           _browser.Button(Find.ById(".save")).Click();

       }

       private void btnChangeIp_Click(object sender, EventArgs e)
       {
           int currentPostion =
                      clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).Position;

           clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).IntervalofAds =
               Convert.ToInt32(
                   System.Configuration.ConfigurationManager.AppSettings["IntervalOfAds"].ToString(
                       CultureInfo.InvariantCulture));

           clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).isCurrentlyUsed = false;

           if (currentPostion == clsVariables.computeremailaccountList.Count)
               clsVariables.computeremailaccountList.First(t => t.Position == 1).isCurrentlyUsed = true;
           else
               clsVariables.computeremailaccountList.First(t => t.Position == currentPostion + 1).
                   isCurrentlyUsed = true;

           //TESTING


           txtIP.Text = clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).Proxy;

           txtCuEmail.Text = clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).CraigslistAccount;

           txtCuPhone.Text = clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).CraigsAccountPhoneNumber;


           try
           {
               txtCuPass.Text = clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).CraigsListPassword;

               //var networkManagement = new NetworkManagement();

               //networkManagement.SetIP(clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed));

               System.Threading.Thread.Sleep(5000);
           }
           catch (Exception ex)
           {
               
               throw new Exception(ex.Message);
           }
           
       }

       private void btnRenew_Click(object sender, EventArgs e)
       {
           bool flag;
           if (cbComputer.SelectedItem != null)
           {
               _computerId = (int) cbComputer.SelectedItem;
               flag = true;

              
           }
           else
           {

               flag = Int32.TryParse(cbComputer.Text, out _computerId);

               if (!flag)
               {
                   MessageBox.Show("You just entered a wrong schedule", "Critical Error", MessageBoxButtons.OK,
                                   MessageBoxIcon.Warning);
                   cbComputer.SelectedIndex = 0;
               }

          


           }


           var totalrenewList = SQLHelper.PopulateEmailHavingRenewAds(_computerId);

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
               
               // var networkManagement = new NetworkManagement();

               //networkManagement.SetIP(eAccount);

               System.Threading.Thread.Sleep(6000);

               txtIP.Text = eAccount.Proxy;

               txtCuEmail.Text = eAccount.CraigslistAccount;

               txtCuPhone.Text = eAccount.CraigsAccountPhoneNumber;

               txtCuPass.Text = eAccount.CraigsListPassword;


            
               try
               {

                   _browser = new IE("https://accounts.craigslist.org/");

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

               catch (Exception ex)
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


                   throw new Exception(ex.Message + ex.StackTrace);


               }

           }


           _flags = WinAPI.AW_ACTIVATE | WinAPI.AW_HOR_POSITIVE | WinAPI.AW_SLIDE;

           _animationForm = new AnimationForm(1500, _flags);
         
       }

      

        
    }
}
