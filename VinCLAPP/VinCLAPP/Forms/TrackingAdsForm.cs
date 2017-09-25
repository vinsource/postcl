using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VinCLAPP.Helper;
using VinCLAPP.Model;
using WatiN.Core;
using Form = System.Windows.Forms.Form;

namespace VinCLAPP.Forms
{
    public partial class TrackingAdsForm : Form
    {
        private int _listingId;
        private int _cityId;
        private List<SimpleClTrackingModel> _todayList;
        private IE _browser = null;
        public TrackingAdsForm()
        {
            InitializeComponent();
        }

        public TrackingAdsForm(int listingId)
        {
            InitializeComponent();
            _listingId = listingId;
        }

        public TrackingAdsForm(int listingId,int cityId)
        {
            InitializeComponent();
            _listingId = listingId;
            _cityId = cityId;
        }

        private void TrackingAdsForm_Load(object sender, EventArgs e)
        {
            _todayList = new List<SimpleClTrackingModel>();

            _todayList = DataHelper.GetPostedAds(_listingId, GlobalVar.CurrentDealer.DealerId);

            if (_todayList.Where(x => x.CityId == _cityId).Any())
            {

                dGridViewSameCity.DataSource =
                    new SortableBindingList<SimpleClTrackingModel>(_todayList.Where(x => x.CityId == _cityId).ToList());

               
            }

            if (_todayList.Where(x => x.CityId != _cityId).Any())
            {

                dGridViewOtherCities.DataSource =
                    new SortableBindingList<SimpleClTrackingModel>(_todayList.Where(x => x.CityId != _cityId).ToList());

              
            }





        }

        private void dGridViewToday_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
           
                var listingid =
                    Convert.ToInt32(dGridViewSameCity.Rows[e.RowIndex].Cells["ListingId"].Value.ToString());

                Clbrowser.Url =
                    new Uri(_todayList.First(x => x.CityId == _cityId && x.ListingId == listingid).HtmlCraigslistUrl);
           

        }


        private void dGridViewOtherCities_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
           
                var listingid =
                     Convert.ToInt32(
                         dGridViewOtherCities.Rows[e.RowIndex].Cells["ListingIdOtherCities"].Value.ToString());

                Clbrowser.Url =
                    new Uri(_todayList.First(x => x.CityId != _cityId && x.ListingId == listingid).HtmlCraigslistUrl);
           
       
               
           
        }

        private void CraigslistLogin(int trackingId)
        {
            if (_browser == null)
                _browser = new IE("https://accounts.craigslist.org/");
            else
            {
                _browser.ClearCache();

                _browser.Close();

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

            var trackingads = DataHelper.GetAds(trackingId);
            
            if (trackingads != null)
            {

               

                var emailAccount = DataHelper.GetEmail(trackingads.EmailAccount);

                if (emailAccount != null)
                {
                    
                    _browser.TextField(Find.ByName("inputEmailHandle")).Value = emailAccount.Email;

                    _browser.TextField(Find.ByName("inputPassword")).Value = emailAccount.Password;

                    System.Threading.Thread.Sleep(2000);
                    
                    _browser.Buttons.First().Click();

                    System.Threading.Thread.Sleep(2000);
                    
                    _browser.GoTo("https://post.craigslist.org/manage/" + trackingads.CLPostingId);

                    System.Threading.Thread.Sleep(1000);

                    if (_browser.Buttons.Any(x => x.Value == "Edit this Posting"))
                    {
                        _browser.Button(Find.ByValue("Edit this Posting")).Click();

                        System.Threading.Thread.Sleep(1000);

                        _browser.TextField(Find.ById("Ask")).Value =
                            GlobalVar.CurrentDealer.Inventory.FirstOrDefault(x => x.ListingId == trackingads.ListingId)
                                .SalePrice.ToString(CultureInfo.InvariantCulture);

                        System.Threading.Thread.Sleep(1000);

                        _browser.Button(Find.ByValue("Continue")).Click();

                        System.Threading.Thread.Sleep(1000);

                        _browser.Button(Find.ByValue("Continue")).Click();

                        System.Threading.Thread.Sleep(1000);

                        _browser.Button(Find.ByValue("Done with Images")).Click();


                        System.Threading.Thread.Sleep(1000);

                        _browser.Button(Find.ByValue("Continue")).Click();


                    }
                    else
                    {
                        MessageBox.Show(
                 "This ads is not eligible for editing.",
                 "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show(
                 "Email account associated with this ad no longer exist in CLDMS system",
                 "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }

    public class ShortEmailAccount
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
