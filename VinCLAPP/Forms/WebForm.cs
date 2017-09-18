using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using VinCLAPP.Helper;
using VinCLAPP.Model;


namespace VinCLAPP.Forms
{
    public partial class WebForm : Form
    {
        private List<SimpleClTrackingModel> _todayList;
        public WebForm()
        {
            InitializeComponent();

          
        }
         public WebForm(string url)
        {
            InitializeComponent();
         
            ClWebBrowser.Url = new Uri(url); 
        }

        private void WebForm_Load(object sender, EventArgs e)
        {
            _todayList = new List<SimpleClTrackingModel>();

            _todayList = DataHelper.GetPostedAdsInToday(GlobalVar.CurrentAccount.AccountId);

            if (_todayList.Any())
            {

                dGridViewToday.DataSource = new BindingList<SimpleClTrackingModel>(_todayList);

            }
            

        }

        private void dGridViewToday_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
                var ListingId = Convert.ToInt32(dGridViewToday.Rows[e.RowIndex].Cells["ListingId"].Value.ToString());

                ClWebBrowser.Url = new Uri(_todayList.First(x => x.ListingId == ListingId).HtmlCraigslistUrl);
           
        }

    }
}
