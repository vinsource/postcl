using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using Vinclapp.Craigslist;
using VinCLAPP.Forms;
using WatiN.Core;
using System.Windows.Forms;
using VinCLAPP.Helper;
using VinCLAPP.Model;
using VinCLAPP.DatabaseModel;
using Form = System.Windows.Forms.Form;

namespace VinCLAPP
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected string PostedStatus = "";

        private const int FreewayIsuzu = 32083;

        private DateTime _startRunning;

        private IE _browser = null;

        private List<VehicleInfo> _wholeList = null;

        //private const string SingleLogonWarning = "You logged in from another computer. Do you want to continue?";

        private int _numberofSelectedCars;

        private XmlDocument _apiResponse = new XmlDocument();

        private List<XmlDocument> _apiResponseList = new List<XmlDocument>();

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

        private bool IsAlreadyLogin()
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

        private void CraigslistLogin()
        {
            if (_browser == null)
                _browser = new IE("https://accounts.craigslist.org/");
            else
            {
                CloseInternetExplorers();

                _browser = null;

                _browser = new IE("https://accounts.craigslist.org/");

            }

            _browser.ShowWindow(WatiN.Core.Native.Windows.NativeMethods.WindowShowStyle.ShowMaximized);


            if (IsAlreadyLogin())
            {
                System.Threading.Thread.Sleep(2000);

                _browser.Span(Find.ById("ef")).Links.First().Click();

                System.Threading.Thread.Sleep(3000);


            }

            System.Threading.Thread.Sleep(2000);

            var emailAccount = GlobalVar.CurrentDealer.EmailAccountList.First(t => t.IsCurrentlyUsed);

            _browser.TextField(Find.ByName("inputEmailHandle")).Value = emailAccount.CraigslistAccount;

            _browser.TextField(Find.ByName("inputPassword")).Value = emailAccount.CraigsListPassword;

            System.Threading.Thread.Sleep(2000);


            _browser.Buttons.First().Click();

            if (!IsAlreadyLogin())
            {
                timerPost10.Enabled = false;

                btnPause.Text = "UnPause";

                btnPause.BackColor = Color.Blue;

                var lForm = new LoginWarningForm();

                lForm.Show();

                lForm.Activate();

                _browser.ShowWindow(WatiN.Core.Native.Windows.NativeMethods.WindowShowStyle.ShowMaximized);

            }
            else
                RunPattern();



        }

        private void SaveSetting()
        {
            var context = new CLDMSEntities();

            var dealer = context.Dealers.First(o => o.DealerId == GlobalVar.CurrentDealer.DealerId);

            dealer.CityOveride = String.IsNullOrEmpty(txtDCity.Text) ? "" : txtDCity.Text.Trim();

            dealer.EndingSentence = String.IsNullOrEmpty(txtEndingSentence.Text) ? "" : txtEndingSentence.Text.Trim();

            dealer.PostWithPrice = cbPrice.Checked;

            dealer.LastUpdated = DateTime.Now;

            context.SaveChanges();

            GlobalVar.CurrentDealer.CityOveride = dealer.CityOveride;

            GlobalVar.CurrentDealer.DealerTitle = dealer.DealerTitle;

            GlobalVar.CurrentDealer.EndingSentence = dealer.EndingSentence;

            GlobalVar.CurrentDealer.PostWithPrice = dealer.PostWithPrice.GetValueOrDefault();
        }

        private void BtnPostClick(object sender, EventArgs e)
        {
            SaveSetting();

            if (!GlobalVar.CurrentDealer.EmailAccountList.Any())
            {
                MessageBox.Show("You must have craiglist email accounts before posting ads. Please go to Tools/Email to add emails.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!GlobalVar.CurrentDealer.CityList.Any())
            {
                MessageBox.Show(
                    "You must choose at least one city before posting ads. Please go to Tools/Posting Cities to choose what cities you want to post",
                    "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else
            {
                if (_browser != null)
                {
                    _browser.ClearCache();
                    _browser.ForceClose();
                    _browser = null;
                }

                btnPause.Enabled = true;

                _wholeList = new List<VehicleInfo>();

                var list = (BindingList<SimpleVehicleInfo>)dGridInventory.DataSource;

                foreach (var item in list)
                {
                    if (item.IsSelected)
                    {
                        _wholeList.Add(GlobalVar.CurrentDealer.Inventory.First(x => x.AutoId == item.AutoId));
                    }
                }

                if (_wholeList.Any())
                {
                    var cForm = CraigslistCreditCardForm.Instance(this);

                    cForm.Show();

                    cForm.Activate();

                }
                else
                {
                    MessageBox.Show("You must choose at least one vehicle to post", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }



            }


        }

        public void RunFirstExecute()
        {
            if (_wholeList.Any())
            {
                _numberofSelectedCars = _wholeList.Count;

                var firstOrDefault = GlobalVar.CurrentDealer.CityList.FirstOrDefault(x => x.isCurrentlyUsed);
                if (firstOrDefault != null)
                    lblCity.Text = firstOrDefault.CityName;

                lblSupposedPosts.Text = _wholeList.Count.ToString(CultureInfo.InvariantCulture);

                lblEstimatedTimeLeft.Text = (_wholeList.Count() * 4).ToString(CultureInfo.InvariantCulture) + " minutes";

                if (GlobalVar.CurrentDealer.DelayTimer > 0)

                    timerPost10.Interval = GlobalVar.CurrentDealer.DelayTimer;


                CraigslistLogin();
            }

        }

        public void RunFirstExecuteNewVersion()
        {
            if (_wholeList.Any())
            {
                var vehicle = _wholeList.ElementAt(0);

                var citySelected = GlobalVar.CurrentDealer.CityList.First(x => x.CityID == vehicle.PostingCityId);

                lblCity.Text = citySelected.CityName;

                _numberofSelectedCars = _wholeList.Count;

                lblSupposedPosts.Text = _wholeList.Count.ToString(CultureInfo.InvariantCulture);

                lblEstimatedTimeLeft.Text = (_wholeList.Count() * 3).ToString(CultureInfo.InvariantCulture) + " minutes";

                try
                {
                    progressPostingBar.Value = 0;
                    btnPost.Enabled = false;
                    btnPost.Text = "Running";
                    bgNewVersion.RunWorkerAsync();

                }
                catch (Exception)
                {
                    btnPost.Enabled = true;
                    btnPost.Text = "Post";
                    MessageBox.Show("Please contact CLDMS support for more information");
                }

            }

        }

        private void bgNewVersion_DoWork(object sender, DoWorkEventArgs e)
        {

            int index = 0;
            var clService = new CraigslistService();
            foreach (var vehicle in _wholeList)
            {
                try
                {

                    var citySelected = GlobalVar.CurrentDealer.CityList.First(x => x.CityID == vehicle.PostingCityId);

                    vehicle.PostingCityName = citySelected.CityName;

                    vehicle.CityOveride = String.IsNullOrEmpty(GlobalVar.CurrentDealer.CityOveride)
                        ? citySelected.CityName
                        : GlobalVar.CurrentDealer.CityOveride.ToUpperInvariant();

                    vehicle.EndingSentence = GlobalVar.CurrentDealer.EndingSentence;

                    vehicle.LeadEmail = GlobalVar.CurrentDealer.LeadEmail;

                    vehicle.CraigslistCityUrl = citySelected.CraigsListCityURL;

                    vehicle.DealerWebUrl = GlobalVar.CurrentDealer.WebSiteUrl;

                    vehicle.ContactName = GlobalVar.CurrentAccount.FirstName + " " + GlobalVar.CurrentAccount.LastName;

                    if (cbPrice.Checked)
                        vehicle.SalePrice = 0;

                    bgNewVersion.ReportProgress(0, vehicle);

                    var imageModel = ImageHelper.GenerateRunTimePhysicalImageByComputerAccount(vehicle);

                    bgNewVersion.ReportProgress(2, vehicle);

                    if (imageModel.PhysicalImageUrl.Any())
                    {
                        var creditCardInfo = new CreditCardInfo()
                        {
                            Address = Properties.Settings.Default.cardaddress,
                            CardNumber = Properties.Settings.Default.cardnumber,
                            City = Properties.Settings.Default.cardcity,
                            ContactEmail = Properties.Settings.Default.cardcontactemail,
                            ContactName = Properties.Settings.Default.cardcontactname,
                            ContactPhone = Properties.Settings.Default.cardcontactphone,
                            Country = "US",
                            ExpirationMonth = Properties.Settings.Default.expirationmonth,
                            ExpirationYear = Properties.Settings.Default.expirationyear,
                            FirstName = Properties.Settings.Default.cardfirstname,
                            LastName = Properties.Settings.Default.cardlastname,
                            State = Properties.Settings.Default.cardstate,
                            Postal = Properties.Settings.Default.cardzipcode,
                            VerificationNumber = Properties.Settings.Default.securitycode,
                            ListingId = vehicle.ListingId,


                        };

                        var confirmationPayment =
                            clService.PostingAdsOnCraigslist(
                                GlobalVar.CurrentDealer.EmailAccountList.First().CraigslistAccount,
                                GlobalVar.CurrentDealer.EmailAccountList.First().CraigsListPassword, vehicle,
                                creditCardInfo);

                        bgNewVersion.ReportProgress(9, vehicle);

                        if (confirmationPayment.Status == CraigslistPostingStatus.EmailVerification)
                        {
                            vehicle.ProgessStatus = CraigslistPostingStatus.EmailVerification;
                            vehicle.ErrorMessage = confirmationPayment.ErrorMessage;
                            bgNewVersion.ReportProgress(10, vehicle);
                            break;
                        }

                        if (confirmationPayment.Status == CraigslistPostingStatus.PaymentError)
                        {
                            vehicle.ProgessStatus = CraigslistPostingStatus.PaymentError;
                            vehicle.ErrorMessage = confirmationPayment.ErrorMessage;
                            bgNewVersion.ReportProgress(10, vehicle);
                            break;
                        }

                        if (confirmationPayment.Status == CraigslistPostingStatus.Success &&
                            confirmationPayment.PaymentId > 0 && confirmationPayment.PostingId > 0)
                        {
                            vehicle.ProgessStatus = CraigslistPostingStatus.Success;
                            var clModel = new CraigsListTrackingModel
                            {
                                ListingId = vehicle.ListingId,
                                CityId = citySelected.CityID,
                                DealerId = GlobalVar.CurrentDealer.DealerId,
                                EmailAccount =
                                    GlobalVar.CurrentDealer.EmailAccountList.First(x => x.IsCurrentlyUsed)
                                        .CraigslistAccount,
                                HtmlCraigslistUrl =
                                    String.IsNullOrEmpty(vehicle.SubAbr)
                                        ? vehicle.CraigslistCityUrl + "ctd/" + confirmationPayment.PostingId + ".html"
                                        : vehicle.CraigslistCityUrl + vehicle.SubAbr + "/ctd/" +
                                          confirmationPayment.PostingId + ".html",
                                VinListingId = vehicle.VinListingId,
                                CityName = vehicle.PostingCityName

                            };

                            DataHelper.AddNewTracking(clModel);

                            vehicle.HtmlCraigslistUrl = clModel.HtmlCraigslistUrl;

                        }
                        index = index + 1;
                        vehicle.AdsPoistion = index;
                        bgNewVersion.ReportProgress(10, vehicle);
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message + " " + ex.StackTrace, "Message", MessageBoxButtons.OK,
                    //    MessageBoxIcon.Warning);

                }
            }



        }

        private void bgNewVersion_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            var vehicle = (VehicleInfo)e.UserState;

            lblProcessing.Visible = true;

            int ix = e.ProgressPercentage;

            if (ix < 2)
            {

                lblProcessing.Text = "Donwloading images for " + vehicle.ModelYear + " " +
                                     " " + vehicle.Make + " " + vehicle.Model + " " +

                                     vehicle.Trim + ". Stock : " + vehicle.StockNumber + ".";
            }

            else if (ix >= 2 && ix < 9)
            {
                lblProcessing.Text = "Posting " + vehicle.ModelYear + " " + vehicle.Make + " " + vehicle.Model + " " +
                                     vehicle.Trim + ". Stock : " + vehicle.StockNumber + " on Craigslist in " +
                                     vehicle.PostingCityName + ".";


            }

            else if (ix >= 9 && ix < 10)
            {
                lblProcessing.Text = "Storing tracking number.";
            }
            else if (ix == 10)
            {
                if (vehicle.ProgessStatus == CraigslistPostingStatus.EmailVerification)
                {
                    lblProcessing.Text = "Email verification is required by craigslist. Login to your email, and follow craigslist instructions to finish the first purchase";

                    var list = (BindingList<SimpleVehicleInfo>)dGridInventory.DataSource;

                    foreach (var item in list)
                    {
                        if (item.IsSelected)
                        {
                            item.IsSelected = false;
                        }
                    }
                    return;
                }


                if (vehicle.ProgessStatus == CraigslistPostingStatus.PaymentError)
                {
                    lblProcessing.Text = "There was a payment error with " + vehicle.ModelYear + " " + vehicle.Make + " " + vehicle.Model + " " +
                                     vehicle.Trim + ". Stock : " + vehicle.StockNumber + ". CLDMS will try to process later.";

                    txtError.Text = vehicle.ErrorMessage;


                    var rowindex = GetIndexOfRowWithAutoId(vehicle.AutoId);

                    dGridInventory.Rows[rowindex].DefaultCellStyle.BackColor = Color.Yellow;

                    lblTotalPost.Text = LogicHelper.GetDailyUse().ToString(CultureInfo.InvariantCulture);

                    lblSupposedPosts.Text = (_numberofSelectedCars - vehicle.AdsPoistion).ToString(CultureInfo.InvariantCulture);

                    lblEstimatedTimeLeft.Text = ((_numberofSelectedCars - vehicle.AdsPoistion) * 2).ToString(CultureInfo.InvariantCulture) +
                                                " minutes";

                    return;
                }

                if (vehicle.ProgessStatus == CraigslistPostingStatus.Success)
                {
                    lblProcessing.Text = vehicle.ModelYear + " " + vehicle.Make + " " + vehicle.Model + " " +
                                   vehicle.Trim + " was posted successfully on Craigslist in " +
                                   vehicle.PostingCityName + ".";

                    var rowindex = GetIndexOfRowWithAutoId(vehicle.AutoId);

                    dGridInventory.Rows[rowindex].DefaultCellStyle.BackColor = Color.Red;

                    dGridInventory.Rows[rowindex].Cells["AdsLink"].Value = "Click";

                    dGridInventory.Rows[rowindex].Cells["LastPosted"].Value = "Today";

                    dGridInventory.Rows[rowindex].Cells["AdsLink"].Tag = vehicle.HtmlCraigslistUrl;

                    dGridInventory.Rows[rowindex].Cells["IsSelected"].Value = false;

                    lblTotalPost.Text = LogicHelper.GetDailyUse().ToString(CultureInfo.InvariantCulture);

                    lblSupposedPosts.Text = (_numberofSelectedCars - vehicle.AdsPoistion).ToString(CultureInfo.InvariantCulture);

                    lblEstimatedTimeLeft.Text = ((_numberofSelectedCars - vehicle.AdsPoistion) * 2).ToString(CultureInfo.InvariantCulture) +
                                                " minutes";
                }


                if (progressPostingBar.Value > 100)
                {
                    progressPostingBar.Value = 100;

                }
                else
                {
                    var progressPercentage = (vehicle.AdsPoistion * 100) / _numberofSelectedCars;

                    progressPostingBar.Value = progressPercentage;
                }


            }

        }

        private void bgNewVersion_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var index = 0;

            var runPaymentError = false;
            
            for (int i = 0; i < dGridInventory.Rows.Count; i += 1)
            {
                if (dGridInventory.Rows[i].DefaultCellStyle.BackColor == Color.Yellow)
                {
                    runPaymentError = true;
                    break;

                }
            }

            if (runPaymentError)
            {
                _wholeList = new List<VehicleInfo>();

                var list = (BindingList<SimpleVehicleInfo>)dGridInventory.DataSource;

                foreach (var item in list)
                {
                    if (item.IsSelected)
                    {
                        _wholeList.Add(GlobalVar.CurrentDealer.Inventory.First(x => x.AutoId == item.AutoId));
                    }
                }

                foreach (var vehicle in _wholeList)
                {
                    try
                    {
                        var citySelected = GlobalVar.CurrentDealer.CityList.First(x => x.CityID == vehicle.PostingCityId);

                        vehicle.PostingCityName = citySelected.CityName;

                        vehicle.CityOveride = String.IsNullOrEmpty(GlobalVar.CurrentDealer.CityOveride)
                            ? citySelected.CityName
                            : GlobalVar.CurrentDealer.CityOveride.ToUpperInvariant();

                        vehicle.EndingSentence = GlobalVar.CurrentDealer.EndingSentence;

                        vehicle.LeadEmail = GlobalVar.CurrentDealer.LeadEmail;

                        vehicle.CraigslistCityUrl = citySelected.CraigsListCityURL;

                        vehicle.DealerWebUrl = GlobalVar.CurrentDealer.WebSiteUrl;

                        bgNewVersion.ReportProgress(0, vehicle);

                        var imageModel = ImageHelper.GenerateRunTimePhysicalImageByComputerAccount(vehicle);

                        bgNewVersion.ReportProgress(2, vehicle);

                        if (imageModel.PhysicalImageUrl.Any())
                        {
                            var creditCardInfo = new CreditCardInfo()
                            {
                                Address = Properties.Settings.Default.cardaddress,
                                CardNumber = Properties.Settings.Default.cardnumber,
                                City = Properties.Settings.Default.cardcity,
                                ContactEmail = Properties.Settings.Default.cardcontactemail,
                                ContactName = Properties.Settings.Default.cardcontactname,
                                ContactPhone = Properties.Settings.Default.cardcontactphone,
                                Country = "US",
                                ExpirationMonth = Properties.Settings.Default.expirationmonth,
                                ExpirationYear = Properties.Settings.Default.expirationyear,
                                FirstName = Properties.Settings.Default.cardfirstname,
                                LastName = Properties.Settings.Default.cardlastname,
                                State = Properties.Settings.Default.cardstate,
                                Postal = Properties.Settings.Default.cardzipcode,
                                VerificationNumber = Properties.Settings.Default.securitycode,
                                ListingId = vehicle.ListingId


                            };

                            var clService = new CraigslistService();

                            var confirmationPayment =
                                clService.PostingAdsOnCraigslist(
                                    GlobalVar.CurrentDealer.EmailAccountList.First().CraigslistAccount,
                                    GlobalVar.CurrentDealer.EmailAccountList.First().CraigsListPassword, vehicle,
                                    creditCardInfo);

                            bgNewVersion.ReportProgress(9, vehicle);

                            if (confirmationPayment.Status == CraigslistPostingStatus.EmailVerification)
                            {
                                vehicle.ProgessStatus = CraigslistPostingStatus.EmailVerification;
                                bgNewVersion.ReportProgress(10, vehicle);
                                break;
                            }

                            if (confirmationPayment.Status == CraigslistPostingStatus.PaymentError)
                            {
                                vehicle.ProgessStatus = CraigslistPostingStatus.PaymentError;
                                bgNewVersion.ReportProgress(10, vehicle);
                            }

                            if (confirmationPayment.Status == CraigslistPostingStatus.Success &&
                                confirmationPayment.PaymentId > 0 && confirmationPayment.PostingId > 0)
                            {
                                vehicle.ProgessStatus = CraigslistPostingStatus.Success;
                                var clModel = new CraigsListTrackingModel
                                {
                                    ListingId = vehicle.ListingId,
                                    CityId = citySelected.CityID,
                                    DealerId = GlobalVar.CurrentDealer.DealerId,
                                    EmailAccount =
                                        GlobalVar.CurrentDealer.EmailAccountList.First(x => x.IsCurrentlyUsed)
                                            .CraigslistAccount,
                                    HtmlCraigslistUrl =
                                        String.IsNullOrEmpty(vehicle.SubAbr)
                                            ? vehicle.CraigslistCityUrl + "ctd/" + confirmationPayment.PostingId + ".html"
                                            : vehicle.CraigslistCityUrl + vehicle.SubAbr + "/ctd/" +
                                              confirmationPayment.PostingId + ".html"

                                };

                                DataHelper.AddNewTracking(clModel);

                                vehicle.HtmlCraigslistUrl = clModel.HtmlCraigslistUrl;

                            }
                            index = index + 1;
                            vehicle.AdsPoistion = index;
                            bgNewVersion.ReportProgress(10, vehicle);
                        }
                    }
                    catch (Exception)
                    {


                    }


                }
            }
            else
            {
                var hasError = _wholeList.Any(i => i.ProgessStatus != CraigslistPostingStatus.Success);
                if (hasError)
                {
                    var firstError = _wholeList.First(i => i.ProgessStatus != CraigslistPostingStatus.Success);
                    if (firstError.ProgessStatus == CraigslistPostingStatus.EmailVerification)
                    {
                        lblProcessing.Text = "Email Verification Failed. " + firstError.ErrorMessage;
                        MessageBox.Show("Email Verification Failed. Please copy link and paste to your browser to see in detail " + firstError.ErrorMessage);
                    }
                    else if (firstError.ProgessStatus == CraigslistPostingStatus.PaymentError)
                    {
                        lblProcessing.Text = "Payment Failed. " + firstError.ErrorMessage;
                        MessageBox.Show("Payment Failed. Please copy link and paste to your browser to see in detail " + firstError.ErrorMessage);
                    }
                    else
                    {
                        lblProcessing.Text = "Failed. " + firstError.ErrorMessage;
                    }
                }
                else
                {
                    lblProcessing.Text = "Thanks for using CLDMS. Have a good day";
                    
                }

                btnPost.Enabled = true;

                btnPost.Text = "Post";
            }

        }

        private void RunPatternNewVersion()
        {
            try
            {
                var vehicle = _wholeList.ElementAt(0);

                var imageModel = ImageHelper.GenerateRunTimePhysicalImageByComputerAccount(vehicle);

                if (imageModel.PhysicalImageUrl.Any())
                {
                    timerPost10.Enabled = true;

                    var citySelected = GlobalVar.CurrentDealer.CityList.First(x => x.CityID == vehicle.PostingCityId);

                    vehicle.CityOveride = String.IsNullOrEmpty(GlobalVar.CurrentDealer.CityOveride) ? citySelected.CityName : GlobalVar.CurrentDealer.CityOveride.ToUpperInvariant();

                    vehicle.EndingSentence = GlobalVar.CurrentDealer.EndingSentence;

                    vehicle.LeadEmail = GlobalVar.CurrentDealer.LeadEmail;

                    vehicle.ContactName = GlobalVar.CurrentAccount.FirstName + " " + GlobalVar.CurrentAccount.LastName;

                    var creditCardInfo = new CreditCardInfo()
                    {
                        Address = Properties.Settings.Default.cardaddress,
                        CardNumber = Properties.Settings.Default.cardnumber,
                        City = Properties.Settings.Default.cardcity,
                        ContactEmail = Properties.Settings.Default.cardcontactemail,
                        ContactName = Properties.Settings.Default.cardcontactname,
                        ContactPhone = Properties.Settings.Default.cardcontactphone,
                        Country = "US",
                        ExpirationMonth = Properties.Settings.Default.expirationmonth,
                        ExpirationYear = Properties.Settings.Default.expirationyear,
                        FirstName = Properties.Settings.Default.cardfirstname,
                        LastName = Properties.Settings.Default.cardlastname,
                        State = Properties.Settings.Default.cardstate,
                        Postal = Properties.Settings.Default.cardzipcode,
                        VerificationNumber = Properties.Settings.Default.securitycode,
                        ListingId = vehicle.ListingId


                    };
                    var clService = new CraigslistService();
                    MessageBox.Show(vehicle.ContactName);
                    var confirmationPayment = clService.PostingAdsOnCraigslist(GlobalVar.CurrentDealer.EmailAccountList.First().CraigslistAccount, GlobalVar.CurrentDealer.EmailAccountList.First().CraigsListPassword, vehicle, creditCardInfo);

                    if (confirmationPayment.PaymentId > 0 && confirmationPayment.PostingId > 0)
                    {

                        var clModel = new CraigsListTrackingModel
                        {
                            ListingId = vehicle.ListingId,
                            CityId = citySelected.CityID,
                            DealerId = GlobalVar.CurrentDealer.DealerId,
                            EmailAccount = GlobalVar.CurrentDealer.EmailAccountList.First(x => x.IsCurrentlyUsed).CraigslistAccount,
                            HtmlCraigslistUrl = String.IsNullOrEmpty(vehicle.SubAbr) ? vehicle.CraigslistCityUrl + "ctd/" + confirmationPayment.PostingId + ".html" : vehicle.CraigslistCityUrl + vehicle.SubAbr + "/ctd/" + confirmationPayment.PostingId + ".html"

                        };

                        DataHelper.AddNewTracking(clModel);

                        _wholeList.ElementAt(0).HtmlCraigslistUrl = clModel.HtmlCraigslistUrl;

                    }
                }


            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message + " "  + ex.StackTrace, "Message", MessageBoxButtons.OK,
                //    MessageBoxIcon.Warning);

            }
        }

        private void RunPattern()
        {
            try
            {
                GlobalVar.CurrentDealer.CityOveride = txtDCity.Text.Trim();

                GlobalVar.CurrentDealer.EmailAccountList.First(t => t.IsCurrentlyUsed).IntervalofAds--;

                var vehicle = _wholeList.ElementAt(0);

                var citySelected = GlobalVar.CurrentDealer.CityList.First(x => x.CityID == vehicle.PostingCityId);

                lblCity.Text = citySelected.CityName;

                string title = CommonHelper.GenerateCraiglistTitle(vehicle, cbPrice.Checked);

                string index = "0";

                if (citySelected.SubCity)
                    index = citySelected.CLIndex.ToString(CultureInfo.InvariantCulture);

                /////GO TO CRAIGLIST PAGE

                if (_browser == null)
                {
                    _browser = new IE(citySelected.CraigsListCityURL);

                    _browser.ShowWindow(WatiN.Core.Native.Windows.NativeMethods.WindowShowStyle.ShowMaximized);
                }
                else
                {
                    CloseInternetExplorers();

                    System.Threading.Thread.Sleep(1000);

                    _browser = new IE();

                    System.Threading.Thread.Sleep(2000);

                    _browser.GoTo(citySelected.CraigsListCityURL);

                    _browser.ShowWindow(WatiN.Core.Native.Windows.NativeMethods.WindowShowStyle.ShowMaximized);
                }

                System.Threading.Thread.Sleep(1000);
                //GO TO POST CLASSIFIED

                _browser.Link(Find.ById("post")).Click();

                //MessageBox.Show("Step 1");

                System.Threading.Thread.Sleep(1000);

                if (IsAlreadyLogin())
                {

                    //GO TO TYPE
                    //MessageBox.Show("Step 2");
                    _browser.RadioButton(Find.ByValue("fsd")).Checked = true;

                    System.Threading.Thread.Sleep(1000);

                    //CATEGORY

                    _browser.RadioButton(Find.ByValue("146")).Checked = true;

                    //SUB AREA
                    if (citySelected.SubCity)
                    {
                        _browser.RadioButton(Find.ByValue(index)).Checked = true;

                    }

                    if (citySelected.CraigsListCityURL.Equals("http://sfbay.craigslist.org/"))
                    {
                        _browser.RadioButton(Find.ByValue("0")).Checked = true;


                    }
                    //MessageBox.Show("Step 3");
                    //CONTENT
                    System.Threading.Thread.Sleep(1000);



                    _browser.TextField(Find.ById("contact_phone")).Value = GlobalVar.CurrentDealer.PhoneNumber;

                    _browser.TextField(Find.ById("contact_name")).Value = Properties.Settings.Default.cardcontactname;

                    _browser.CheckBox(Find.ByName("contact_phone_ok")).Checked = true;
                    _browser.CheckBox(Find.ByName("contact_text_ok")).Checked = true;

                    _browser.TextField(Find.ById("PostingTitle")).Value = title;


                    //_browser.RadioButton(Find.ById("A")).Checked = true;

                    if (cbPrice.Checked)
                        _browser.TextField(Find.ById("Ask")).Value = "";
                    else

                        _browser.TextField(Find.ById("Ask")).Value = vehicle.SalePrice.ToString();


                    if (String.IsNullOrEmpty(GlobalVar.CurrentDealer.CityOveride))
                        _browser.TextField(Find.ById("GeographicArea")).Value = citySelected.CityName;
                    else
                        _browser.TextField(Find.ById("GeographicArea")).Value =
                            GlobalVar.CurrentDealer.CityOveride.ToUpperInvariant();

                    _browser.TextField(Find.ById("postal_code")).Value = vehicle.ZipCode;

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

                    _browser.SelectList(Find.ById("auto_title_status")).Options.FirstOrDefault(x => x.Value == "1").Select();

                    _browser.SelectList(Find.ByIndex(2))
                       .Options.FirstOrDefault(x => x.Value == "excellent").Select();

                    if (vehicle.Tranmission.ToLower().Contains("automatic"))
                        _browser.CheckBox(Find.ById("auto_trans_auto")).Checked = true;
                    else if (vehicle.Tranmission.ToLower().Contains("manual"))

                        _browser.CheckBox(Find.ById("auto_trans_manual")).Checked = true;
                    else
                    {
                        _browser.CheckBox(Find.ById("auto_trans_manual")).Checked = true;
                        _browser.CheckBox(Find.ById("auto_trans_auto")).Checked = true;
                    }
                    _browser.CheckBox(Find.ById("see_my_other")).Checked = true;


                    //_browser.TextField(Find.ById("xstreet0")).Value = vehicle.StreetAddress;

                    //System.Threading.Thread.Sleep(1000);

                    //_browser.TextField(Find.ById("city")).Value = vehicle.City;

                    //System.Threading.Thread.Sleep(1000);

                    //_browser.TextField(Find.ById("region")).Value = vehicle.State;

                    //System.Threading.Thread.Sleep(1000);

                    //_browser.CheckBox(Find.ByName("outsideContactOK")).Checked = true;


                    System.Threading.Thread.Sleep(1000);

                    _browser.Button(Find.ByName("go")).Click();

                    //System.Threading.Thread.Sleep(1000);

                    //_browser.Button(Find.ByClass("continue bigbutton")).Click();


                    System.Threading.Thread.Sleep(2000);


                    _browser.Button(Find.ByValue("add image")).Click();

                    var imageModel = ImageHelper.GenerateRunTimePhysicalImageByComputerAccount(vehicle);


                    if (imageModel.PhysicalImageUrl.Any())
                    {
                        System.Threading.Thread.Sleep(2000);

                        foreach (var uploadLocalImage in imageModel.PhysicalImageUrl)
                        {
                            _browser.FileUpload(Find.ByName("file")).Set(uploadLocalImage);

                            System.Threading.Thread.Sleep(4000);

                        }

                        _browser.Button(Find.ByValue("Done with Images")).Click();

                        System.Threading.Thread.Sleep(5000);

                        _browser.Button(Find.ByName("go")).Click();

                        int clStatus = CraigsListHelper.DetecStatusFromURL(_browser.Uri.AbsoluteUri);


                        if (clStatus == 2 || clStatus == 3)
                        {
                            var rowindex = GetIndexOfRowWithAutoId(vehicle.AutoId);
                            dGridInventory.Rows[rowindex].DefaultCellStyle.BackColor = Color.Red;

                            if (_wholeList.Any())
                                _wholeList.RemoveAt(0);

                            timerPost10.Enabled = false;

                            btnPause.Text = "UnPause";

                            if (clStatus == 2)
                            {

                                string phoneNumber =
                                    GlobalVar.CurrentDealer.EmailAccountList.First(x => x.IsCurrentlyUsed).
                                        CraigsAccountPhoneNumber;

                                try
                                {
                                    if (!String.IsNullOrEmpty(phoneNumber))
                                    {
                                        if (phoneNumber.Contains("(") && phoneNumber.Contains(")"))
                                        {
                                            phoneNumber = phoneNumber.Replace("(", "");
                                            phoneNumber = phoneNumber.Replace(")", "");
                                        }

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
                                catch (Exception)
                                {

                                }


                            }


                            var tForm =
                                new PhoneVerificationForm(clStatus);

                            tForm.Show();

                            tForm.Activate();
                        }
                        else
                        {
                            timerPost10.Enabled = true;
                            //lblTotalPost.Text = (LogicHelper.GetDailyUse() + 1).ToString(CultureInfo.InvariantCulture);

                            System.Threading.Thread.Sleep(3000);

                            _browser.Button(Find.ByName("go")).Click();
                            System.Threading.Thread.Sleep(2000);

                            _browser.TextField(Find.ById("cardNumber")).Value = Properties.Settings.Default.cardnumber;

                            _browser.TextField(Find.ByName("cvNumber")).Value = Properties.Settings.Default.securitycode;

                            var expirationmonth = "";

                            var expirationyear = "";

                            if (Properties.Settings.Default.expirationmonth < 10)
                                expirationmonth = "0" + Properties.Settings.Default.expirationmonth;
                            else
                            {
                                expirationmonth = "" + Properties.Settings.Default.expirationmonth;
                            }

                            expirationyear = Properties.Settings.Default.expirationyear.ToString(CultureInfo.InvariantCulture);

                            _browser.SelectList(Find.ByName("expMonth"))
                                .Options.FirstOrDefault(x => x.Value == expirationmonth).Select();

                            _browser.SelectList(Find.ByName("expYear"))
                                .Options.FirstOrDefault(x => x.Value == expirationyear).Select();

                            _browser.TextField(Find.ByName("cardFirstName")).Value =
                                Properties.Settings.Default.cardfirstname;

                            _browser.TextField(Find.ByName("cardLastName")).Value =
                                Properties.Settings.Default.cardlastname;

                            _browser.TextField(Find.ByName("cardAddress")).Value =
                                Properties.Settings.Default.cardaddress;

                            _browser.TextField(Find.ByName("cardCity")).Value = Properties.Settings.Default.cardcity;

                            _browser.TextField(Find.ByName("cardState")).Value = Properties.Settings.Default.cardstate;

                            _browser.TextField(Find.ByName("cardPostal")).Value =
                                Properties.Settings.Default.cardzipcode;

                            _browser.TextField(Find.ByName("contactName")).Value =
                                Properties.Settings.Default.cardcontactname;

                            _browser.TextField(Find.ByName("contactPhone")).Value =
                                Properties.Settings.Default.cardcontactphone;

                            _browser.TextField(Find.ByName("contactEmail")).Value =
                                Properties.Settings.Default.cardcontactemail;

                            System.Threading.Thread.Sleep(2000);

                            _browser.Button(Find.ByName("finishForm")).Click();

                            System.Threading.Thread.Sleep(45000);

                            //EXTRA


                            _browser.GoTo("https://accounts.craigslist.org/");

                            _browser.Links.First(x => x.Url.Contains("https://post.craigslist.org/manage")).Click();

                            System.Threading.Thread.Sleep(1000);

                            _browser.GoTo(_browser.Links[3].Url);

                            System.Threading.Thread.Sleep(2000);

                            var clModel = new CraigsListTrackingModel
                            {
                                ListingId = vehicle.ListingId,
                                CityId = citySelected.CityID,
                                DealerId = GlobalVar.CurrentDealer.DealerId,
                                EmailAccount =
                                    GlobalVar.CurrentDealer.EmailAccountList.First(x => x.IsCurrentlyUsed)
                                        .CraigslistAccount,
                                HtmlCraigslistUrl = _browser.Url,

                            };


                            DataHelper.AddNewTracking(clModel);

                            _browser.Close();

                            var rowindex = GetIndexOfRowWithAutoId(vehicle.AutoId);

                            dGridInventory.Rows[rowindex].DefaultCellStyle.BackColor = Color.Red;

                            dGridInventory.Rows[rowindex].Cells["AdsLink"].Value = "Click";

                            dGridInventory.Rows[rowindex].Cells["AdsLink"].Tag = clModel.HtmlCraigslistUrl;


                            if (progressPostingBar.Value > 100)
                            {
                                progressPostingBar.Value = 100;

                            }
                            else
                            {
                                var progressPercentage = ((_numberofSelectedCars - (_wholeList.Count - 1)) * 100) /
                                                         _numberofSelectedCars;

                                progressPostingBar.Value = progressPercentage;
                            }

                        }






                    }


                }
                else
                {
                    //_browser.Close();

                    //_browser = null;

                    CraigslistLogin();

                }


            }


            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message + " "  + ex.StackTrace, "Message", MessageBoxButtons.OK,
                //    MessageBoxIcon.Warning);

            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            pbPicLoad.Visible = true;

            btnPost.Enabled = true;
            btnPost.Text = "Post";

            Refresh();

            var account = DataHelper.CheckAccountStatus(txtUsername.Text, txtPassword.Text);

            if (account.IsExist)
            {
                if (!account.Active)
                {
                    MessageBox.Show("Your account is deactivated. Please make a payment or contact POSTCL to support", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (account.IsFirstLogon)
                {
                    DataHelper.InitializeGlobalDealerInfoVariable(account.DealerId);

                    var sForm = ChangePassForm.Instance(this);

                    sForm.Show();

                    sForm.Activate();

                    Refresh();


                }
                else if (account.IsTrial)
                {
                    DataHelper.InitializeGlobalDealerInfoVariable(GlobalVar.CurrentAccount.DealerId);

                    var sForm = CreditCardAuthorizeForm.Instance(this);

                    sForm.Show();

                    sForm.Activate();

                }
                else
                {
                    AfterLogging();
                }
            }
            else
            {
                pbPicLoad.Visible = false;
                MessageBox.Show("Wrong username and/or password", "Critical Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }

        }

        public void AfterLogging()
        {
            pbPicLoad.Visible = true;

            txtUsername.Enabled = false;

            txtPassword.Enabled = false;

            btnLogin.Enabled = false;

            pbPicLoad.Visible = true;

            EmailToolStripMenuItem.Enabled = false;

            btnAddInventory.Enabled = true;

            btnDelete.Enabled = true;

            linkEditEnding.Visible = true;

            btnView.Enabled = true;

            ckAllSelect.Checked = false;

            ckOnlyTruck.Checked = false;

            this.Refresh();

            bgWorker.RunWorkerAsync();
        }

        public void RefreshInventoryForChangesFromSubForm()
        {
            pbPicLoad.Visible = true;

            txtUsername.Enabled = false;

            txtPassword.Enabled = false;

            btnLogin.Enabled = false;

            pbPicLoad.Visible = true;

            EmailToolStripMenuItem.Enabled = false;

            btnAddInventory.Enabled = true;

            btnDelete.Enabled = true;

            //btnRenew.Enabled = true;

            btnView.Enabled = true;

            this.Refresh();

            bgWorkerChangeCity.RunWorkerAsync();
        }

        private void BgWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DataHelper.InitializeGlobalDealerInfoVariable(GlobalVar.CurrentAccount.DealerId);
                DataHelper.InitializeGlobalEmailAccountVariable(GlobalVar.CurrentAccount.AccountId);
                DataHelper.InitializeGlobalInventoryVariable(GlobalVar.CurrentDealer.DealerId);

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BgWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnLogin.Enabled = true;
            txtUsername.Enabled = true;
            txtPassword.Enabled = true;
            btnPost.Enabled = true;
            //NOTE: Only available for Freeway Isuzu
            if (GlobalVar.CurrentDealer.DealerId.Equals(FreewayIsuzu)) btnPostAPI.Enabled = true;
            else btnPostAPI.Enabled = false;
            btnPause.Enabled = true;
            //btnSettingSave.Enabled = true;

            EmailToolStripMenuItem.Enabled = true;
            lblTotalPost.Text = LogicHelper.GetDailyUse().ToString(CultureInfo.InvariantCulture);
            //labelTrial.Text = String.Format("You are using trial version - {0} days left.", TrialHelper.TrialDaysLeft());
            //labelTrial.Visible = GlobalVar.TrialInfo.IsTrial;

            if (!e.Cancelled)
            {
                lblDealerName.Visible = true;

                lblDealerAddress.Visible = true;

                lblPhone.Visible = true;

                lblDealerName.Text = GlobalVar.CurrentDealer.DealershipName;

                lblDealerAddress.Text = GlobalVar.CurrentDealer.StreetAddress + " " + GlobalVar.CurrentDealer.City + "," + GlobalVar.CurrentDealer.State + " " + GlobalVar.CurrentDealer.ZipCode;

                lblPhone.Text = GlobalVar.CurrentDealer.PhoneNumber;

                txtDCity.Text = GlobalVar.CurrentDealer.CityOveride;

                txtEndingSentence.Text = GlobalVar.CurrentDealer.EndingSentence;

                cbPrice.Checked = GlobalVar.CurrentDealer.PostWithPrice;

                dGridInventory.DataSource = new SortableBindingList<SimpleVehicleInfo>(GlobalVar.CurrentDealer.SimpleInventory);

                //dGridInventory.Sort(dGridInventory.Columns["LastPosted"], ListSortDirection.Descending);

                dGridInventory.Visible = true;

                pbPicLoad.Visible = false;

                if (cbRemember.Checked)
                {
                    Properties.Settings.Default.Username = txtUsername.Text;
                    Properties.Settings.Default.Password = txtPassword.Text;
                    Properties.Settings.Default.Save();
                }

                if (GlobalVar.CurrentDealer.CityList.Any())
                {

                    var firstOrDefault = GlobalVar.CurrentDealer.CityList.First(x => x.isCurrentlyUsed);

                    if (firstOrDefault != null)
                    {
                        lblCity.Text = firstOrDefault.CityName;
                    }
                }
                this.Refresh();

            }
        }

        private void EmailToolStripMenuItemClick(object sender, EventArgs e)
        {

            if (GlobalVar.CurrentDealer == null)
                MessageBox.Show("Please login first to access this feature. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                var cForm = CraigsListAccountForm.Instance();

                cForm.Show();

                cForm.Activate();
            }
        }

        private void dGridInventory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dGridInventory.Columns["Edit"] == null || dGridInventory.Columns["ViewImage"] == null || dGridInventory.Columns["UploadImage"] == null
                || dGridInventory.Columns["AdsLink"] == null || dGridInventory.Columns["TotalAds"] == null) return;
            // Ignore clicks that are not on button cells.  
            if (e.RowIndex < 0 || (e.ColumnIndex != dGridInventory.Columns["Edit"].Index
                && e.ColumnIndex != dGridInventory.Columns["UploadImage"].Index
                && e.ColumnIndex != dGridInventory.Columns["ViewImage"].Index
                && e.ColumnIndex != dGridInventory.Columns["AdsLink"].Index
                && e.ColumnIndex != dGridInventory.Columns["TotalAds"].Index)) return;

            var dealerId = GlobalVar.CurrentDealer.DealerId;
            string imageServiceUrl = ConfigurationManager.AppSettings["ServerImageURL"].ToString(CultureInfo.InvariantCulture);
            const int inventoryStatus = 1;
            int listingId = int.Parse(dGridInventory.Rows[e.RowIndex].Cells["ListingID"].Value.ToString());
            int cityId = int.Parse(dGridInventory.Rows[e.RowIndex].Cells["PostingCityId"].Value.ToString());
            var vin = dGridInventory.Rows[e.RowIndex].Cells["Vin"].Value.ToString();

            if (e.ColumnIndex == dGridInventory.Columns["UploadImage"].Index)
            {
                var window = new UploadPicture(dealerId, imageServiceUrl, inventoryStatus, listingId, vin);
                window.Show();
            }
            else if (e.ColumnIndex == dGridInventory.Columns["ViewImage"].Index)
            {
                var window = new ViewImage(dealerId, imageServiceUrl, inventoryStatus, listingId, vin);
                window.Show();
            }
            else if (e.ColumnIndex == dGridInventory.Columns["Edit"].Index)
            {
                var form = new VinDecodeForm(String.IsNullOrEmpty(vin) ? DecodeType.Manual : DecodeType.VIN, listingId, this);
                form.Show();
            }
            else if (e.ColumnIndex == dGridInventory.Columns["AdsLink"].Index)
            {
                if (dGridInventory.Rows[e.RowIndex].Cells["AdsLink"].Value != null)
                {
                    var cForm = WebForm.Instance(dGridInventory.Rows[e.RowIndex].Cells["AdsLink"].Tag.ToString());

                    cForm.Show();

                    cForm.Activate();
                }
            }
            else if (e.ColumnIndex == dGridInventory.Columns["TotalAds"].Index)
            {
                if (dGridInventory.Rows[e.RowIndex].Cells["TotalAds"].Value != null)
                {
                    var totalAds = Convert.ToInt32(dGridInventory.Rows[e.RowIndex].Cells["TotalAds"].Value.ToString());

                    if (totalAds > 0)
                    {

                        var cForm = TrackingAdsForm.Instance(listingId, cityId);

                        cForm.Show();

                        cForm.Activate();
                    }
                }
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to delete selected items ?", "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var list = (BindingList<SimpleVehicleInfo>)dGridInventory.DataSource;
                var selectedItem = list.Where(i => i.IsSelected).Select(j => j.ListingId).ToList();
                RemoveFromGridView(list, selectedItem);
                DataHelper.RemoveVehicleInfo(selectedItem);
            }
        }

        private static void RemoveFromGridView(BindingList<SimpleVehicleInfo> list, List<int> selectedItem)
        {
            var length = list.Count;
            for (int count = length - 1; count >= 0; count--)
            {
                if (selectedItem.Contains(list[count].ListingId))
                {
                    list.RemoveAt(count);
                }
            }
        }

        private void editBillingToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (GlobalVar.CurrentDealer == null)
                MessageBox.Show("Please login first to access this feature. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                var sForm = CreditCardAuthorizeForm.Instance(this);

                sForm.Show();

                sForm.Activate();
            }

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dGridInventory.AutoGenerateColumns = false;
            dGridInventory.CellClick += new DataGridViewCellEventHandler(dGridInventory_CellClick);
            if (Properties.Settings.Default.Username != string.Empty)
            {
                txtUsername.Text = Properties.Settings.Default.Username;
                txtPassword.Text = Properties.Settings.Default.Password;
                cbRemember.Checked = true;
            }
            try
            {
                const string physicalImagePath = @"C:\ImageWarehouse";

                var dirNormal = new DirectoryInfo(physicalImagePath);

                if (dirNormal.Exists)
                    Directory.Delete(physicalImagePath, true);
            }
            catch (Exception)
            {


            }
        }

        private void linkSignup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var sForm = ClientForm.Instance();

            sForm.Show();

            sForm.Activate();
        }

        private void postingCitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (GlobalVar.CurrentDealer == null)
                MessageBox.Show("Please login first to access this feature. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                var sForm = CityChoiceForm.Instance(this);

                sForm.Show();

                sForm.Activate();
            }

        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (btnPause.Text.Equals("Pause"))
            {
                timerPost10.Enabled = false;
                timerPause.Enabled = true;
                btnPause.BackColor = Color.Red;
                btnPause.Text = "UnPause";
            }
            else
            {
                timerPost10.Enabled = true;
                timerPause.Enabled = false;
                btnPause.BackColor = Color.Gray;
                btnPause.Text = "Pause";
            }
        }

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            if (txtUsername.Text.Trim().Length == 0)
                errorProvider1.SetError(txtUsername,
                "Please enter the username.");
            else
                errorProvider1.SetError(txtUsername, "");
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassword.Text.Trim().Length == 0)
                errorProvider2.SetError(txtPassword,
                "Please enter the password.");
            else
                errorProvider2.SetError(txtPassword, "");
        }

        private void timerPost10_Tick(object sender, EventArgs e)
        {
            try
            {
                //lblSupposedPosts.Text = _wholeList.Count.ToString(CultureInfo.InvariantCulture);

                //lblEstimatedTimeLeft.Text = (_wholeList.Count() * 3).ToString(CultureInfo.InvariantCulture) + " minutes";

                RunFirstExecuteNewVersion();
                //if (_wholeList.Any())
                //{
                //    _wholeList.RemoveAt(0);

                //    if (_wholeList.Count == 0)
                //    {
                //        //CloseInternetExplorers();

                //        //_browser = null;

                //        timerPost10.Enabled = false;

                //        MessageBox.Show("Thanks for using PostCL. Have a good day", "Message",MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    }
                //    else
                //    {
                //        //if (progressPostingBar.Value > 100)
                //        //{
                //        //    progressPostingBar.Value = 100;

                //        //}
                //        //else
                //        //{
                //        //    var progressPercentage = (_wholeList.Count*100)/_numberofSelectedCars;

                //        //    progressPostingBar.Value = progressPercentage;
                //        //}
                //        lblSupposedPosts.Text = _wholeList.Count.ToString(CultureInfo.InvariantCulture);

                //        lblEstimatedTimeLeft.Text = (_wholeList.Count() * 3).ToString(CultureInfo.InvariantCulture) + " minutes";

                //        RunFirstExecuteNewVersion();
                //        //RunPatternNewVersion();

                //        //RunPattern();

                //        //if (GlobalVar.CurrentDealer.EmailAccountList.First(t => t.IsCurrentlyUsed).IntervalofAds > 0)
                //        //{
                //        //    RunPattern();

                //        //}

                //        //else
                //        //{
                //        //    int currentPostion =
                //        //        GlobalVar.CurrentDealer.EmailAccountList.First(t => t.IsCurrentlyUsed).Position;

                //        //    GlobalVar.CurrentDealer.EmailAccountList.First(t => t.IsCurrentlyUsed).IntervalofAds =
                //        //        Convert.ToInt32(
                //        //            System.Configuration.ConfigurationManager.AppSettings["IntervalOfAds"].ToString(
                //        //                CultureInfo.InvariantCulture));

                //        //    GlobalVar.CurrentDealer.EmailAccountList.First(t => t.IsCurrentlyUsed).IsCurrentlyUsed =
                //        //        false;

                //        //    if (currentPostion == GlobalVar.CurrentDealer.EmailAccountList.Count)
                //        //        GlobalVar.CurrentDealer.EmailAccountList.First(t => t.Position == 1).IsCurrentlyUsed
                //        //            = true;
                //        //    else
                //        //        GlobalVar.CurrentDealer.EmailAccountList.First(t => t.Position == currentPostion + 1)
                //        //            .IsCurrentlyUsed = true;

                //        //    _browser.Close();

                //        //    _browser = null;

                //        //    CraigslistLogin();

                //        //}
                //    }

                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException + ex.StackTrace, "Message", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (GlobalVar.CurrentAccount != null)
                DataHelper.ResetIpForSingleLogOn(GlobalVar.CurrentAccount.AccountId);

            if (_browser != null)
            {
                _browser.Close();

                _browser = null;
            }
        }

        private void cbRemember_CheckedChanged(object sender, EventArgs e)
        {

            if (cbRemember.Checked)
            {
                Properties.Settings.Default.Username = txtUsername.Text;
                Properties.Settings.Default.Password = txtPassword.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Username = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Save();
            }

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalVar.CurrentDealer == null)
                MessageBox.Show("Please login first to access this feature. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                var sForm = ClientForm.Instance("Edit");

                sForm.Show();

                sForm.Activate();
            }

        }

        private void btnAddInventory_Click(object sender, EventArgs e)
        {
            var form = new VinDecodeForm(DecodeType.VIN, this);
            form.Show();
        }

        private void ckAll_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ckAllSelect_Click(object sender, EventArgs e)
        {
            var checkbox = (System.Windows.Forms.CheckBox)sender;
            if (GlobalVar.CurrentDealer == null)
            {
                checkbox.Checked = false;
                MessageBox.Show("Please login first to access this feature. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var list = (BindingList<SimpleVehicleInfo>)dGridInventory.DataSource;

                if (checkbox.Checked)
                {
                    foreach (var item in list)
                    {
                        item.IsSelected = true;
                    }
                }
                else
                {
                    foreach (var item in list)
                    {
                        item.IsSelected = false;
                    }
                }
                ckOnlyTruck.Checked = false;
                dGridInventory.Refresh();
            }
        }

        private void ckOnlyTruck_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (System.Windows.Forms.CheckBox)sender;
            if (GlobalVar.CurrentDealer == null)
            {
                checkbox.CheckState = CheckState.Unchecked;
                MessageBox.Show("Please login first to access this feature. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                var list = (BindingList<SimpleVehicleInfo>)dGridInventory.DataSource;


                if (checkbox.Checked)
                {
                    foreach (var item in list.Where(i => i.IsTruck))
                    {
                        item.IsSelected = true;
                    }
                }
                else
                {
                    foreach (var item in list.Where(i => i.IsTruck))
                    {
                        item.IsSelected = false;
                    }
                }
                ckAllSelect.CheckState = CheckState.Unchecked;
                dGridInventory.Refresh();
            }

        }

        private void ckOnlyTruck_Click(object sender, EventArgs e)
        {
            var checkbox = (System.Windows.Forms.CheckBox)sender;
            if (GlobalVar.CurrentDealer == null)
            {
                checkbox.Checked = false;
                MessageBox.Show("Please login first to access this feature. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var list = (BindingList<SimpleVehicleInfo>)dGridInventory.DataSource;

                if (checkbox.Checked)
                {
                    foreach (var item in list)
                    {
                        item.IsSelected = item.IsTruck;
                    }
                }
                else
                {
                    foreach (var item in list.Where(i => i.IsTruck))
                    {
                        item.IsSelected = false;
                    }
                }
                ckAllSelect.Checked = false;
                dGridInventory.Refresh();
            }

        }

        private void rbCityToCar_CheckedChanged(object sender, EventArgs e)
        {
            if (GlobalVar.CurrentDealer.Inventory.Any())
            {

                GlobalVar.CurrentDealer.Inventory =
                    GlobalVar.CurrentDealer.Inventory.OrderBy(x => x.PostingCityId).ToList();

                GlobalVar.CurrentDealer.SimpleInventory =
                    GlobalVar.CurrentDealer.SimpleInventory.OrderBy(x => x.PostingCityId).ToList();


                int index = 1;

                foreach (var tmp in GlobalVar.CurrentDealer.Inventory)
                {
                    tmp.AutoId = index++;

                }

                index = 1;

                foreach (var tmp in GlobalVar.CurrentDealer.SimpleInventory)
                {
                    tmp.AutoId = index++;
                }

                dGridInventory.DataSource = new BindingList<SimpleVehicleInfo>(GlobalVar.CurrentDealer.SimpleInventory);
            }
        }

        private void rbCarToCity_CheckedChanged(object sender, EventArgs e)
        {
            if (GlobalVar.CurrentDealer.Inventory.Any())
            {
                GlobalVar.CurrentDealer.Inventory = GlobalVar.CurrentDealer.Inventory.OrderBy(x => x.ListingId).ToList();

                GlobalVar.CurrentDealer.SimpleInventory = GlobalVar.CurrentDealer.SimpleInventory.OrderBy(x => x.ListingId).ToList();



                int index = 1;

                foreach (var tmp in GlobalVar.CurrentDealer.Inventory)
                {
                    tmp.AutoId = index++;

                }

                index = 1;

                foreach (var tmp in GlobalVar.CurrentDealer.SimpleInventory)
                {
                    tmp.AutoId = index++;
                }

                dGridInventory.DataSource = new BindingList<SimpleVehicleInfo>(GlobalVar.CurrentDealer.SimpleInventory);
            }
        }

        private void bgWorkerChangeCity_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DataHelper.InitializeGlobalInventoryVariable(GlobalVar.CurrentDealer.DealerId);

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bgWorkerChangeCity_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BgWorkerRunWorkerCompleted(sender, e);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            var cForm = WebForm.Instance();

            cForm.Show();

            cForm.Activate();
        }

        private void lnlfogotPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var cForm = EmailForgetPassowrd.Instance();

            cForm.Show();

            cForm.Activate();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cForm = AboutForm.Instance();

            cForm.Show();

            cForm.Activate();
        }

        private void datafeedToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (GlobalVar.CurrentDealer == null)
                MessageBox.Show("Please login first to access this feature. ", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            else
            {
                var cForm = DataFeedForm.Instance();

                cForm.Show();

                cForm.Activate();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var vehicle = GlobalVar.CurrentDealer.Inventory.First();

            var imageModel = ImageHelper.GenerateRunTimePhysicalImageByComputerAccount(vehicle);
        }

        private void craigslistFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalVar.CurrentDealer == null)
                MessageBox.Show("Please login first to access this feature. ", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            else
            {
                var cForm = CraigslistCreditCardForm.Instance();

                cForm.Show();

                cForm.Activate();
            }
        }

        private void craigslistAPIAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalVar.CurrentDealer == null)
                MessageBox.Show("Please login first to access this feature. ", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            else
            {
                var cForm = APIForm.Instance();

                cForm.Show();

                cForm.Activate();
            }
        }

        private void btnPostAPI_Click(object sender, EventArgs e)
        {
            SaveSetting();

            if (String.IsNullOrEmpty(GlobalVar.CurrentAccount.APIAccountId) || String.IsNullOrEmpty(GlobalVar.CurrentAccount.APIUsername) || String.IsNullOrEmpty(GlobalVar.CurrentAccount.APIPassword))
            {
                MessageBox.Show("You must have an API bulk Craigslist account to post.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!GlobalVar.CurrentDealer.CityList.Any())
            {
                MessageBox.Show(
                    "You must choose at least one city before posting ads. Please go to Tools/Posting Cities to choose what cities you want to post",
                    "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else
            {
                if (_browser != null)
                {
                    _browser.ClearCache();
                    _browser.ForceClose();
                    _browser = null;
                }

                btnPause.Enabled = true;

                _wholeList = new List<VehicleInfo>();

                var list = (BindingList<SimpleVehicleInfo>)dGridInventory.DataSource;

                foreach (var item in list)
                {
                    if (item.IsSelected)
                    {
                        _wholeList.Add(GlobalVar.CurrentDealer.Inventory.First(x => x.AutoId == item.AutoId));
                    }
                }

                if (!_wholeList.Any())
                {
                    MessageBox.Show("Please choose at least one car to post", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    var dailyLimitLeft = GlobalVar.CurrentAccount.DailyLimit - LogicHelper.GetDailyUse();

                    if (_wholeList.Count > dailyLimitLeft && !GlobalVar.CurrentDealer.DealerId.Equals(FreewayIsuzu))
                    {
                        MessageBox.Show("You are trying to post " + _wholeList.Count + " ads. However, your daily limit left is " + dailyLimitLeft + " ads.");
                    }
                    else
                    {
                        try
                        {
                            btnPostAPI.Enabled = false;
                            bgAPICall.RunWorkerAsync();

                        }
                        catch (Exception)
                        {
                            btnPostAPI.Enabled = true;
                            MessageBox.Show("Please contact POSTCL support for more information");
                        }

                    }


                }

            }
        }

        private int DoBulkPosting(List<VehicleInfo> list, int totalOfCars, int indexProgress)
        {
            var webRequest = new WebClient();

            var builder = new StringBuilder();

            builder.AppendLine("<?xml version=\"1.0\"?>");

            builder.AppendLine("<rdf:RDF xmlns=\"http://purl.org/rss/1.0/\" xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\" xmlns:cl=\"http://www.craigslist.org/about/cl-bulk-ns/1.0\">");

            builder.AppendLine("<channel>");

            builder.AppendLine("<items>");

            foreach (var vehicle in list)
            {
                builder.AppendLine("<rdf:li rdf:resource=\"" + vehicle.ListingId + "_" + vehicle.PostingCityId + "\"/>");
            }

            builder.AppendLine("</items>");

            builder.AppendLine("<cl:auth username=\"" + GlobalVar.CurrentAccount.APIUsername + "\" password=\"" +
                               GlobalVar.CurrentAccount.APIPassword + "\" accountID=\"" +
                               GlobalVar.CurrentAccount.APIAccountId + "\"/>");

            builder.AppendLine("</channel>");

            foreach (var vehicle in list)
            {
                try
                {
                    
                    SetControlPropertyValue(lblProcessing, "text", String.Format("(Total: {0}) - ({1})", totalOfCars, indexProgress) + " Posting " + vehicle.ModelYear + " " + vehicle.Make + " " + vehicle.Model + " " +
                                     vehicle.Trim + ". Stock : " + vehicle.StockNumber + ".");
                    var subBuilder = new StringBuilder();
                    subBuilder.AppendLine("<item rdf:about=\"" + vehicle.ListingId + "_" + vehicle.PostingCityId + "\">");

                    subBuilder.AppendLine("<cl:category>ctd</cl:category>");

                    var selectedCity = GlobalVar.CurrentDealer.CityList.FirstOrDefault(x => x.CityID == vehicle.PostingCityId);
                    if (selectedCity != null)
                    {
                        subBuilder.AppendLine("<cl:area>" + selectedCity.AreaAbbr + "</cl:area>");

                        if (selectedCity.SubCity && !String.IsNullOrEmpty(selectedCity.SubAbbr))
                        {
                            subBuilder.AppendLine("<cl:subarea>" + selectedCity.SubAbbr + "</cl:subarea>");
                        }
                    }
                    //subBuilder.AppendLine("<cl:neighborhood>" + GlobalVar.CurrentDealer.CityOveride + "</cl:neighborhood>");

                    subBuilder.AppendLine("<cl:price>" + vehicle.SalePrice + "</cl:price>");

                    if (String.IsNullOrEmpty(GlobalVar.CurrentDealer.CityOveride))
                        subBuilder.AppendLine("<cl:neighborhood>" + vehicle.City + "</cl:neighborhood>");
                    else
                        subBuilder.AppendLine("<cl:neighborhood>" + GlobalVar.CurrentDealer.CityOveride + "</cl:neighborhood>");

                    if (String.IsNullOrEmpty(vehicle.CarImageUrl))
                    {
                        SetControlPropertyValue(lblProcessing, "text", lblProcessing.Text + " (Failed: Missing images)");
                        return indexProgress;
                    }

                    string[] carImage = vehicle.CarImageUrl.Split(new[] { ",", "|" }, StringSplitOptions.RemoveEmptyEntries);
                    var index = 1;

                    foreach (var tmp in carImage)
                    {
                        subBuilder.AppendLine("<cl:image position=\"" + index + "\">" + ClapiHelper.DownloadImageToBase64(webRequest, tmp) + "</cl:image>");
                        index++;
                        if (index >= 24) break;
                    }

                    var strAutoBasics = string.Empty;
                    strAutoBasics += String.Format("auto_make_model=\"{0}\"", vehicle.Make + " " + vehicle.Model);

                    var strBodyStyle = string.Empty;
                    if (vehicle.BodyType.ToLower().Contains("suv")) strBodyStyle = "SUV";
                    else if (vehicle.BodyType.ToLower().Contains("bus")) strBodyStyle = "bus";
                    else if (vehicle.BodyType.ToLower().Contains("convertible")) strBodyStyle = "convertible";
                    else if (vehicle.BodyType.ToLower().Contains("coupe")) strBodyStyle = "coupe";
                    else if (vehicle.BodyType.ToLower().Contains("hatchback")) strBodyStyle = "hatchback";
                    else if (vehicle.BodyType.ToLower().Contains("mini")) strBodyStyle = "mini-van";
                    else if (vehicle.BodyType.ToLower().Contains("offroad")) strBodyStyle = "offroad";
                    else if (vehicle.BodyType.ToLower().Contains("pickup")) strBodyStyle = "pickup";
                    else if (vehicle.BodyType.ToLower().Contains("sedan")) strBodyStyle = "sedan";
                    else if (vehicle.BodyType.ToLower().Contains("truck")) strBodyStyle = "truck";
                    else if (vehicle.BodyType.ToLower().Contains("van")) strBodyStyle = "van";
                    else if (vehicle.BodyType.ToLower().Contains("wagon")) strBodyStyle = "wagon";
                    else strBodyStyle = "other";
                    strAutoBasics += String.Format(" auto_bodytype=\"{0}\"", strBodyStyle);

                    var strCylinders = string.Empty;
                    if (vehicle.Cylinder.Contains("10")) strCylinders = "10 cylinders";
                    else if (vehicle.Cylinder.Contains("12")) strCylinders = "12 cylinders";
                    else if (vehicle.Cylinder.Contains("3")) strCylinders = "3 cylinders";
                    else if (vehicle.Cylinder.Contains("4")) strCylinders = "4 cylinders";
                    else if (vehicle.Cylinder.Contains("5")) strCylinders = "5 cylinders";
                    else if (vehicle.Cylinder.Contains("6")) strCylinders = "6 cylinders";
                    else if (vehicle.Cylinder.Contains("8")) strCylinders = "8 cylinders";
                    else strCylinders = "other";
                    strAutoBasics += String.Format(" auto_cylinders=\"{0}\"", strCylinders);

                    var strFuelType = string.Empty;
                    if (vehicle.Fuel.ToLower().Contains("diesel")) strFuelType = "diesel";
                    else if (vehicle.Fuel.ToLower().Contains("electric")) strFuelType = "electric";
                    else if (vehicle.Fuel.ToLower().Contains("gas")) strFuelType = "gas";
                    else if (vehicle.Fuel.ToLower().Contains("hybrid")) strFuelType = "hybrid";
                    else strFuelType = "other";
                    strAutoBasics += String.Format(" auto_fuel_type=\"{0}\"", strFuelType);

                    strAutoBasics += String.Format(" auto_miles=\"{0}\"", CommonHelper.RemoveSpecialCharactersForMsrp(vehicle.Mileage));

                    var strPaint = string.Empty;
                    if (vehicle.ExteriorColor.ToLower().Contains("black")) strPaint = "black";
                    else if (vehicle.ExteriorColor.ToLower().Contains("blue")) strPaint = "blue";
                    else if (vehicle.ExteriorColor.ToLower().Contains("brown")) strPaint = "brown";
                    else if (vehicle.ExteriorColor.ToLower().Contains("green")) strPaint = "green";
                    else if (vehicle.ExteriorColor.ToLower().Contains("grey")) strPaint = "grey";
                    else if (vehicle.ExteriorColor.ToLower().Contains("orange")) strPaint = "orange";
                    else if (vehicle.ExteriorColor.ToLower().Contains("purple")) strPaint = "purple";
                    else if (vehicle.ExteriorColor.ToLower().Contains("red")) strPaint = "red";
                    else if (vehicle.ExteriorColor.ToLower().Contains("silver")) strPaint = "silver";
                    else if (vehicle.ExteriorColor.ToLower().Contains("white")) strPaint = "white";
                    else if (vehicle.ExteriorColor.ToLower().Contains("yellow")) strPaint = "yellow";
                    else strPaint = "custom";
                    strAutoBasics += String.Format(" auto_paint=\"{0}\"", strPaint);

                    strAutoBasics += String.Format(" auto_title_status=\"{0}\"", "clean");

                    strAutoBasics += String.Format(" auto_transmission=\"{0}\"", vehicle.Tranmission.ToLower().Contains("automatic") ? "automatic" : "manual");
                    strAutoBasics += String.Format(" auto_vin=\"{0}\"", vehicle.Vin);
                    strAutoBasics += String.Format(" auto_year=\"{0}\"", vehicle.ModelYear);
                    subBuilder.AppendLine(String.Format("<cl:auto_basics {0}/>", strAutoBasics));

                    subBuilder.AppendLine("<cl:replyEmail privacy=\"C\">" + GlobalVar.CurrentDealer.LeadEmail + "</cl:replyEmail>");

                    subBuilder.AppendLine("<title>" + vehicle.ModelYear + " " + vehicle.Make + " " + vehicle.Model + " " + vehicle.Trim + "</title>");

                    subBuilder.AppendLine("<description><![CDATA[" + ClapiHelper.MakeBodyAdsDescription(vehicle) + "]]></description>");

                    subBuilder.AppendLine("</item>");

                    builder.AppendLine(subBuilder.ToString());

                    SetControlPropertyValue(progressPostingBar, "value", ((indexProgress * 100) / totalOfCars));
                    indexProgress++;

                    SetControlPropertyValue(lblProcessing, "text", lblProcessing.Text + " (Done)");
                }
                catch (Exception ex)
                {
                    SetControlPropertyValue(lblProcessing, "text", String.Format(" (Failed: {0})", ex.Message));
                }
                
            }

            builder.AppendLine("</rdf:RDF>");

            var bodyContent = builder.ToString();
            //_apiResponse = ClapiHelper.MakeApiCall(bodyContent);
            
            var response = ClapiHelper.MakeApiCall(bodyContent);
            if(VerifyPostedStatus(response)){
                _apiResponseList.Add(response);
                return indexProgress;
            }

            return 0;
        }

        private void bgAPICall_DoWork(object sender, DoWorkEventArgs e)
        {
            int indexProgress = 1;
            int pageIndex = 0;
            int pageSize = 1;
            int totalOfCars = _wholeList.Count;
            _startRunning = DateTime.Now;
            while (indexProgress > 0 && pageIndex < (totalOfCars / pageSize) + 1)
            {
                indexProgress = DoBulkPosting(_wholeList.Skip((pageIndex) * pageSize).Take(pageSize).ToList(), totalOfCars, indexProgress);
                pageIndex++;
            }
        }

        private void bgAPICall_DoWork_Backup(object sender, DoWorkEventArgs e)
        {
            int indexProgress = 1; int totalOfCars = _wholeList.Count;
            
            var webRequest = new WebClient();

            var builder = new StringBuilder();

            builder.AppendLine("<?xml version=\"1.0\"?>");

            builder.AppendLine("<rdf:RDF xmlns=\"http://purl.org/rss/1.0/\" xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\" xmlns:cl=\"http://www.craigslist.org/about/cl-bulk-ns/1.0\">");

            builder.AppendLine("<channel>");

            builder.AppendLine("<items>");

            foreach (var vehicle in _wholeList)
            {
                builder.AppendLine("<rdf:li rdf:resource=\"" + vehicle.ListingId + "_" + vehicle.PostingCityId + "\"/>");
            }

            builder.AppendLine("</items>");

            builder.AppendLine("<cl:auth username=\"" + GlobalVar.CurrentAccount.APIUsername + "\" password=\"" +
                               GlobalVar.CurrentAccount.APIPassword + "\" accountID=\"" +
                               GlobalVar.CurrentAccount.APIAccountId + "\"/>");

            builder.AppendLine("</channel>");
            _startRunning = DateTime.Now;

            foreach (var vehicle in _wholeList)
            {
                SetControlPropertyValue(lblProcessing, "text", String.Format("(Total: {0}) - ({1})", totalOfCars, indexProgress) + " Posting " + vehicle.ModelYear + " " + vehicle.Make + " " + vehicle.Model + " " +
                                     vehicle.Trim + ". Stock : " + vehicle.StockNumber + "...");
                var subBuilder = new StringBuilder();
                subBuilder.AppendLine("<item rdf:about=\"" + vehicle.ListingId + "_" + vehicle.PostingCityId + "\">");

                subBuilder.AppendLine("<cl:category>ctd</cl:category>");

                var selectedCity = GlobalVar.CurrentDealer.CityList.FirstOrDefault(x => x.CityID == vehicle.PostingCityId);
                if (selectedCity != null)
                {
                    subBuilder.AppendLine("<cl:area>" + selectedCity.AreaAbbr + "</cl:area>");

                    if (selectedCity.SubCity && !String.IsNullOrEmpty(selectedCity.SubAbbr))
                    {
                        subBuilder.AppendLine("<cl:subarea>" + selectedCity.SubAbbr + "</cl:subarea>");
                    }
                }
                //subBuilder.AppendLine("<cl:neighborhood>" + GlobalVar.CurrentDealer.CityOveride + "</cl:neighborhood>");

                subBuilder.AppendLine("<cl:price>" + vehicle.SalePrice + "</cl:price>");

                if (String.IsNullOrEmpty(GlobalVar.CurrentDealer.CityOveride))
                    subBuilder.AppendLine("<cl:neighborhood>" + vehicle.City + "</cl:neighborhood>");
                else
                    subBuilder.AppendLine("<cl:neighborhood>" + GlobalVar.CurrentDealer.CityOveride + "</cl:neighborhood>");

                if (String.IsNullOrEmpty(vehicle.CarImageUrl)) return;

                string[] carImage = vehicle.CarImageUrl.Split(new[] { ",", "|" }, StringSplitOptions.RemoveEmptyEntries);
                var index = 1;

                foreach (var tmp in carImage)
                {
                    subBuilder.AppendLine("<cl:image position=\"" + index + "\">" + ClapiHelper.DownloadImageToBase64(webRequest, tmp) + "</cl:image>");

                    index++;

                    if (index >= 24)
                    {
                    //    indexProgress++;

                    //    if (progressPostingBar.Value > 100)
                    //    {
                    //        //progressPostingBar.Value = 100;
                    //        SetControlPropertyValue(progressPostingBar, "value", 100);
                    //    }
                    //    else
                    //    {
                    //        var progressPercentage = ((indexProgress * 100) / _wholeList.Count);

                    //        //progressPostingBar.Value = progressPercentage;
                    //        SetControlPropertyValue(progressPostingBar, "value", progressPercentage);
                    //    }


                        break;
                    }

                }

                var strAutoBasics = string.Empty;
                strAutoBasics += String.Format("auto_make_model=\"{0}\"", vehicle.Make + " " + vehicle.Model);

                var strBodyStyle = string.Empty;
                if (vehicle.BodyType.ToLower().Contains("suv")) strBodyStyle = "SUV";
                else if (vehicle.BodyType.ToLower().Contains("bus")) strBodyStyle = "bus";
                else if (vehicle.BodyType.ToLower().Contains("convertible")) strBodyStyle = "convertible";
                else if (vehicle.BodyType.ToLower().Contains("coupe")) strBodyStyle = "coupe";
                else if (vehicle.BodyType.ToLower().Contains("hatchback")) strBodyStyle = "hatchback";
                else if (vehicle.BodyType.ToLower().Contains("mini")) strBodyStyle = "mini-van";
                else if (vehicle.BodyType.ToLower().Contains("offroad")) strBodyStyle = "offroad";
                else if (vehicle.BodyType.ToLower().Contains("pickup")) strBodyStyle = "pickup";
                else if (vehicle.BodyType.ToLower().Contains("sedan")) strBodyStyle = "sedan";
                else if (vehicle.BodyType.ToLower().Contains("truck")) strBodyStyle = "truck";
                else if (vehicle.BodyType.ToLower().Contains("van")) strBodyStyle = "van";
                else if (vehicle.BodyType.ToLower().Contains("wagon")) strBodyStyle = "wagon";
                else strBodyStyle = "other";
                strAutoBasics += String.Format(" auto_bodytype=\"{0}\"", strBodyStyle);

                var strCylinders = string.Empty;
                if (vehicle.Cylinder.Contains("10")) strCylinders = "10 cylinders";
                else if (vehicle.Cylinder.Contains("12")) strCylinders = "12 cylinders";
                else if (vehicle.Cylinder.Contains("3")) strCylinders = "3 cylinders";
                else if (vehicle.Cylinder.Contains("4")) strCylinders = "4 cylinders";
                else if (vehicle.Cylinder.Contains("5")) strCylinders = "5 cylinders";
                else if (vehicle.Cylinder.Contains("6")) strCylinders = "6 cylinders";
                else if (vehicle.Cylinder.Contains("8")) strCylinders = "8 cylinders";
                else strCylinders = "other";
                strAutoBasics += String.Format(" auto_cylinders=\"{0}\"", strCylinders);

                var strFuelType = string.Empty;
                if (vehicle.Fuel.ToLower().Contains("diesel")) strFuelType = "diesel";
                else if (vehicle.Fuel.ToLower().Contains("electric")) strFuelType = "electric";
                else if (vehicle.Fuel.ToLower().Contains("gas")) strFuelType = "gas";
                else if (vehicle.Fuel.ToLower().Contains("hybrid")) strFuelType = "hybrid";
                else strFuelType = "other";
                strAutoBasics += String.Format(" auto_fuel_type=\"{0}\"", strFuelType);

                strAutoBasics += String.Format(" auto_miles=\"{0}\"", CommonHelper.RemoveSpecialCharactersForMsrp(vehicle.Mileage));

                var strPaint = string.Empty;
                if (vehicle.ExteriorColor.ToLower().Contains("black")) strPaint = "black";
                else if (vehicle.ExteriorColor.ToLower().Contains("blue")) strPaint = "blue";
                else if (vehicle.ExteriorColor.ToLower().Contains("brown")) strPaint = "brown";
                else if (vehicle.ExteriorColor.ToLower().Contains("green")) strPaint = "green";
                else if (vehicle.ExteriorColor.ToLower().Contains("grey")) strPaint = "grey";
                else if (vehicle.ExteriorColor.ToLower().Contains("orange")) strPaint = "orange";
                else if (vehicle.ExteriorColor.ToLower().Contains("purple")) strPaint = "purple";
                else if (vehicle.ExteriorColor.ToLower().Contains("red")) strPaint = "red";
                else if (vehicle.ExteriorColor.ToLower().Contains("silver")) strPaint = "silver";
                else if (vehicle.ExteriorColor.ToLower().Contains("white")) strPaint = "white";
                else if (vehicle.ExteriorColor.ToLower().Contains("yellow")) strPaint = "yellow";
                else strPaint = "custom";
                strAutoBasics += String.Format(" auto_paint=\"{0}\"", strPaint);

                strAutoBasics += String.Format(" auto_title_status=\"{0}\"", "clean");

                strAutoBasics += String.Format(" auto_transmission=\"{0}\"", vehicle.Tranmission.ToLower().Contains("automatic") ? "automatic" : "manual");
                strAutoBasics += String.Format(" auto_vin=\"{0}\"", vehicle.Vin);
                strAutoBasics += String.Format(" auto_year=\"{0}\"", vehicle.ModelYear);
                subBuilder.AppendLine(String.Format("<cl:auto_basics {0}/>", strAutoBasics));

                subBuilder.AppendLine("<cl:replyEmail privacy=\"C\">" + GlobalVar.CurrentDealer.LeadEmail + "</cl:replyEmail>");

                subBuilder.AppendLine("<title>" + vehicle.ModelYear + " " + vehicle.Make + " " + vehicle.Model + " " + vehicle.Trim + "</title>");

                subBuilder.AppendLine("<description><![CDATA[" + ClapiHelper.MakeBodyAdsDescription(vehicle) + "]]></description>");

                subBuilder.AppendLine("</item>");

                builder.AppendLine(subBuilder.ToString());

                SetControlPropertyValue(progressPostingBar, "value", ((indexProgress * 100) / _wholeList.Count));
                indexProgress++;
            }

            builder.AppendLine("</rdf:RDF>");

            var bodyContent = builder.ToString();
            //_apiResponse = ClapiHelper.MakeApiCall(bodyContent);
            _apiResponseList.Add(ClapiHelper.MakeApiCall(bodyContent));
        }

        private bool VerifyPostedStatus(XmlDocument doc)
        {
            if (doc == null) { PostedStatus = "FAILED. It indicates an unexpected error at post time"; return false; }
            var namespaceManager = new XmlNamespaceManager(doc.NameTable);
            namespaceManager.AddNamespace("rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
            namespaceManager.AddNamespace("cl", "http://www.craigslist.org/about/cl-bulk-ns/1.0");
            var postedMainNode = doc.SelectSingleNode("/rdf:RDF", namespaceManager).ChildNodes;
            foreach (XmlNode tmp in postedMainNode)
            {
                if (tmp.Name.Equals("item"))
                {
                    // tmp.InnerText
                    switch (tmp.InnerText)
                    {
                        case "NOT_VALID": PostedStatus = "NOT_VALID. It appears in post and validate modes where the validation failed"; return false; break;
                        case "INSUFFICIENT_BLOCKS": PostedStatus = "INSUFFICIENT_BLOCKS. It is reported when the accountID supplied does not have enough remaining blocks to create a posting in the city/category and is not an invoiced account, more blocks must be purchased before the posting is processed"; return false; break;
                        case "CREDIT_LIMIT_REACHED": PostedStatus = "CREDIT_LIMIT_REACHED. It indicates the paid posting account (accountID) has reached its set credit limit for posting via invoicing"; return false; break;
                        default: PostedStatus = "POSTED"; return true; break;
                    }
                }
            }

            PostedStatus = "FAILED. It indicates an unexpected error at post time";
            return false;
        }

        private void bgAPICall_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnPostAPI.Enabled = true;
            foreach (var response in _apiResponseList)
            {
                try
                {
                    //richTextBox1.Text = _apiResponse.InnerXml;
                    if (response == null) continue;
                    var namespaceManager = new XmlNamespaceManager(response.NameTable);
                    namespaceManager.AddNamespace("rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
                    namespaceManager.AddNamespace("cl", "http://www.craigslist.org/about/cl-bulk-ns/1.0");

                    var postedMainNode = response.SelectSingleNode("/rdf:RDF", namespaceManager).ChildNodes;

                    foreach (XmlNode tmp in postedMainNode)
                    {
                        if (tmp.Name.Equals("item"))
                        {
                            var listingandcity = tmp.Attributes[0].InnerText;

                            var listingId = Convert.ToInt32(listingandcity.Split(new string[] { "_" }, StringSplitOptions.None).First());
                            var cityId = Convert.ToInt32(listingandcity.Split(new string[] { "_" }, StringSplitOptions.None).Last());

                            var itemChildNodes = tmp.ChildNodes;

                            foreach (XmlNode minorItem in itemChildNodes)
                            {
                                if (minorItem.Name.Equals("cl:postingID"))
                                {
                                    var postingid = Convert.ToInt64(minorItem.InnerText);
                                    var craigslisturl = String.Empty;

                                    var citySelected = GlobalVar.CurrentDealer.CityList.FirstOrDefault(x => x.CityID == cityId);

                                    if (!String.IsNullOrEmpty(citySelected.SubAbbr))
                                    {
                                        craigslisturl = citySelected.CraigsListCityURL + citySelected.SubAbbr + "/ctd/" + postingid + ".html";
                                    }
                                    else
                                    {
                                        craigslisturl = citySelected.CraigsListCityURL + "ctd/" + postingid + ".html";
                                    }

                                    var clModel = new CraigsListTrackingModel
                                    {
                                        ListingId = listingId,
                                        CityId = cityId,
                                        DealerId = GlobalVar.CurrentDealer.DealerId,
                                        EmailAccount = GlobalVar.CurrentAccount.APIUsername,
                                        HtmlCraigslistUrl = craigslisturl,
                                        ClPostingId = postingid
                                    };

                                    DataHelper.AddNewTrackingFromApi(clModel);

                                    var vehicle = _wholeList.FirstOrDefault(x => x.ListingId == listingId && x.PostingCityId == cityId);

                                    if (vehicle != null)
                                    {
                                        var rowindex = GetIndexOfRowWithAutoId(vehicle.AutoId);

                                        dGridInventory.Rows[rowindex].DefaultCellStyle.BackColor = Color.Red;

                                        dGridInventory.Rows[rowindex].Cells["AdsLink"].Value = "Click";

                                        dGridInventory.Rows[rowindex].Cells["AdsLink"].Tag = craigslisturl;

                                        dGridInventory.Rows[rowindex].Cells["LastPosted"].Value = DateTime.Now;

                                        dGridInventory.Rows[rowindex].Cells[0].Value = false;
                                    }
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    //throw;
                    EmailHelper.SendEmail(new System.Net.Mail.MailAddress("david@vincontrol.com"), "ERROR: Bulk Posting API", ex.Message + " - " + ex.StackTrace); 
                }
                
            }

            if (String.IsNullOrEmpty(PostedStatus) || PostedStatus == "POSTED")
            {
                lblProcessing.Text = lblProcessing.Text = String.Format("Durration: {0} minutes - Thanks for using CLDMS. Have a good day", (DateTime.Now - _startRunning).Minutes);
                MessageBox.Show("Posting succesfully!!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                lblProcessing.Text = lblProcessing.Text = String.Format("Durration: {0} minutes - Only {1} item(s) are posted, the left have issue {2}", (DateTime.Now - _startRunning).Minutes, _apiResponseList.Count, PostedStatus);
                MessageBox.Show(PostedStatus, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            _wholeList.Clear();
            _apiResponseList.Clear();
            var cForm = WebForm.Instance();

            cForm.Show();

            cForm.Activate();
        }

        private int GetIndexOfRowWithAutoId(int id)
        {
            for (int i = 0; i < dGridInventory.Rows.Count; i += 1)
            {
                var cell = dGridInventory.Rows[i].Cells["AutoId"]; // or.DataBoundItem;
                var autoId = Convert.ToInt32(cell.Value.ToString());
                if (autoId == id)
                {
                    return i;
                }
            }

            return 0;


        }

        private void linkEditEnding_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (GlobalVar.CurrentDealer != null)
            {
                var cForm = HTMLEditorEndingSentence.Instance(this);

                cForm.Show();

                cForm.Activate();
            }

        }

        private void btnDailyReport_Click(object sender, EventArgs e)
        {
            if (GlobalVar.CurrentDealer == null)
                MessageBox.Show("Please login first to access this feature. ", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            else
            {
                var cForm = CalendarForm.Instance();

                cForm.Show();

                cForm.Activate();
            }


        }

        private void btnSupport_Click(object sender, EventArgs e)
        {
            if (GlobalVar.CurrentDealer == null)
                MessageBox.Show("Please login first to access this feature. ", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            else
            {
                var cForm = SupportForm.Instance();

                cForm.Show();

                cForm.Activate();
            }

        }

        private void videoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("http://youtu.be/p7c5mfPMMP4");

            }
            catch (Exception)
            {


            }
        }

        delegate void SetControlValueCallback(System.Windows.Forms.Control oControl, string propName, object propValue);

        private void SetControlPropertyValue(System.Windows.Forms.Control oControl, string propName, object propValue)
        {

            if (oControl.InvokeRequired)
            {

                SetControlValueCallback d = new SetControlValueCallback(SetControlPropertyValue);

                oControl.Invoke(d, new object[] { oControl, propName, propValue });

            }

            else
            {

                Type t = oControl.GetType();

                var props = t.GetProperties();

                foreach (var p in props)
                {

                    if (p.Name.ToUpper() == propName.ToUpper())
                    {

                        p.SetValue(oControl, propValue, null);

                    }

                }

            }

        }

    }
}
