using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClappManagementSystem.DatabaseModel;
using ClappManagementSystem.Forms;
using ClappManagementSystem.Helper;
using ClappManagementSystem.Model;
using HtmlAgilityPack;
using WatiN.Core;
using Form = System.Windows.Forms.Form;
using WatiN.Core.Native.Windows;
using Excel = Microsoft.Office.Interop.Excel; 

namespace ClappManagementSystem
{
    public partial class ScheudeForm : Form
    {
        public ScheudeForm()
        {
            InitializeComponent();
        }

        private IE _browser = null;

        private List<TrackingCLAccount> _trackingList = new List<TrackingCLAccount>();

        private List<TrackingDealer> _trackingdealerList = new List<TrackingDealer>();

        private int _computerId;
        private int _reportComputerId;
        private int _dealerId;
        private int _cityId;
        private int _runAutoId;
        private int _progressPercentage;
        
        private void ScheudeFormLoad(object sender, EventArgs e)
        {
         
            lvDealership.Items.Clear();

            lvDealership.Items.AddRange(Globalvar.DealerList.OrderBy(x => x.DealerId).ToArray());

            if (lvDealership.Items.Count > 0)

                lvDealership.SelectedIndex = 0;

            var dtCompare = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var context = new whitmanenterprisecraigslistEntities();

            Globalvar.GridScheduleList = new List<GridScheuleModel>();

            Globalvar.TrackingList = new List<TrackingDealer>();

            var soFarList = from a in context.vincontrolcraigslisttrackings
                            where a.CLPostingId > 0 && a.AddedDate >= dtCompare && a.AddedDate < DateTime.Now
                            select new
                            {
                                a.TrackingId,
                                a.Computer,
                                a.ShowAd,
                                a.DealerId,
                                a.AddedDate,
                                a.Renew

                            };

            foreach (var tmp in soFarList)
            {
                var trackingDealer = new TrackingDealer()
                {
                    TrackingId = tmp.TrackingId,
                    Computer = tmp.Computer.GetValueOrDefault(),
                    ShowedAd = tmp.ShowAd.GetValueOrDefault(),
                    DealerId = tmp.DealerId.GetValueOrDefault(),
                    RenewAd = tmp.Renew.GetValueOrDefault(),
                };

                Globalvar.TrackingList.Add(trackingDealer);
            }


            dGridDailyView.Rows.Clear();

            int newPosts = 0;

            foreach (var tmp in Globalvar.DealerList.OrderBy(x => x.DealerId))
            {
                int numberOfSupposedAds = 0;



                var newRow = new DataGridViewRow();

                newRow.CreateCells(dGridDailyView);
                newRow.Cells[0].Value = tmp.DealerId;
                newRow.Cells[1].Value = tmp.DealershipName;
                newRow.Cells[2].Value = tmp.NumberOfCars;
                newRow.Cells[3].Value = Globalvar.TrackingList.Count(x => x.DealerId == tmp.DealerId);
                newRow.Cells[5].Value = Globalvar.TrackingList.Count(x => x.DealerId == tmp.DealerId && x.RenewAd == true);
                newRow.Cells[6].Value = Globalvar.TrackingList.Count(x => x.DealerId == tmp.DealerId && x.ShowedAd == true);


                if (tmp.ScheduleCityList.Any(x => x.City == 1))
                {
                    numberOfSupposedAds += tmp.NumberOfCars;
                    newRow.Cells[7].Value = 1;
                }
                if (tmp.ScheduleCityList.Any(x => x.City == 10))
                {
                    numberOfSupposedAds += tmp.NumberOfCars;
                    newRow.Cells[8].Value = 1;
                }
                if (tmp.ScheduleCityList.Any(x => x.City == 12))
                {
                    numberOfSupposedAds += tmp.NumberOfCars;
                    newRow.Cells[9].Value = 1;
                }
                if (tmp.ScheduleCityList.Any(x => x.City == 3))
                {
                    numberOfSupposedAds += tmp.NumberOfCars;
                    newRow.Cells[10].Value = 1;
                }
                if (tmp.ScheduleCityList.Any(x => x.City == 8))
                {
                    numberOfSupposedAds += tmp.NumberOfCars;
                    newRow.Cells[11].Value = 1;
                }
                if (tmp.ScheduleCityList.Any(x => x.City == 13))
                {
                    numberOfSupposedAds += tmp.NumberOfCars;
                    newRow.Cells[12].Value = 1;
                }
                if (tmp.ScheduleCityList.Any(x => x.City == 9))
                {
                    numberOfSupposedAds += tmp.NumberOfCars;
                    newRow.Cells[13].Value = 1;
                }


                if (tmp.ScheduleCityList.Any(x => x.Split == true))
                {
                    newRow.Cells[2].Style.BackColor = System.Drawing.Color.SteelBlue;

                    if (numberOfSupposedAds > 0)
                    {
                        newRow.Cells[3].Value = (numberOfSupposedAds / 2) + " / " +
                                  Globalvar.TrackingList.Count(x => x.DealerId == tmp.DealerId);

                        if (numberOfSupposedAds / 2 -
                                                Globalvar.TrackingList.Count(x => x.DealerId == tmp.DealerId)>0)

                        newRow.Cells[4].Value = numberOfSupposedAds / 2 -
                                                Globalvar.TrackingList.Count(x => x.DealerId == tmp.DealerId);
                        else
                        {
                            newRow.Cells[4].Value = 0;
                        }


                        newPosts += numberOfSupposedAds / 2 - Globalvar.TrackingList.Count(x => x.DealerId == tmp.DealerId);
                    }

                    else
                    {
                        newRow.Cells[4].Value = 0;
                    }

                    newRow.Cells[14].Value = 1;

                }
                else
                {
                    if (numberOfSupposedAds > 0)
                    {
                        newRow.Cells[3].Value = numberOfSupposedAds + " / " +
                                       Globalvar.TrackingList.Count(x => x.DealerId == tmp.DealerId);

                        if (numberOfSupposedAds -
                                                Globalvar.TrackingList.Count(x => x.DealerId == tmp.DealerId) > 0)

                        newRow.Cells[4].Value = numberOfSupposedAds -
                                                Globalvar.TrackingList.Count(x => x.DealerId == tmp.DealerId);

                        else
                        {
                            newRow.Cells[4].Value = 0;
                        }
                        
                        newPosts += numberOfSupposedAds -
                                   Globalvar.TrackingList.Count(x => x.DealerId == tmp.DealerId);
                    }

                    else
                    {
                        newRow.Cells[4].Value = 0;
                    }

                    newRow.Cells[14].Value = 0;
                }

               

                if (tmp.ScheduleCityList.Any(x => x.Price == true))
                    newRow.Cells[15].Value = 1;



                dGridDailyView.Rows.Add(newRow);
            }


            lblTotalInventory.Text = Globalvar.DealerList.Sum(x => x.NumberOfCars).ToString(CultureInfo.InvariantCulture);

            lblRenewPost.Text = Globalvar.TrackingList.Count(x => x.RenewAd == true).ToString();

            lblNewPostTotal.Text = newPosts.ToString(CultureInfo.InvariantCulture);

            lblAverage.Text = Math.Ceiling((double)Globalvar.DealerList.Sum(x => x.NumberOfCars) / Globalvar.SplitSchedules).ToString();

            nuNumberSchedules.Value = Globalvar.SplitSchedules;
        }

        private void BtnForwardClick(object sender, EventArgs e)
        {
            var cityList = GetCityList();

            if (!String.IsNullOrEmpty(cityList))
            {

                var selectedDealer = (Dealer) lvDealership.SelectedItem;

                if (!CheckDealerExistInList(selectedDealer, cityList))
                {
                    if (cbNoPrice.Checked)
                        lvSelected.Items.Add(selectedDealer.DealerId + " - " +
                                             cityList.Substring(0, cityList.Length - 1) + " - " + " No Price ");
                    else
                    {
                        lvSelected.Items.Add(selectedDealer.DealerId + " - " +
                                             cityList.Substring(0, cityList.Length - 1) + " - " + " Price ");
                    }


                    var numberofCity =
                        cityList.Split(new string[] {","}, StringSplitOptions.RemoveEmptyEntries).Count() - 1;

                    cbOrangeCounty.Checked = false;

                    cbLongBeach.Checked = false;

                    cbNorthSanDiego.Checked = false;

                    cbInlandEmpire.Checked = false;

                    cbVentura.Checked = false;

                    cbPalmSpring.Checked = false;

                    cbLaCentral.Checked = false;

                    cbAntelopeValley.Checked = false;

                    cbSanGabriel.Checked = false;

                    cbPhoenixEast.Checked = false;

                    cbPhoneixNorth.Checked = false;

                    cbPhoneixSouth.Checked = false;

                    cbWestside.Checked = false;

                    cbEastSanDiego.Checked = false;

                    cbCitySanDiego.Checked = false;

                    lblTotalRound.Text =
                        (Convert.ToInt32(lblTotalRound.Text) + selectedDealer.Round*numberofCity).ToString();

                    lblTotalCars.Text =
                        (Convert.ToInt32(lblTotalCars.Text) + selectedDealer.NumberOfCars*numberofCity).ToString();
                }
            }
        }

        private bool CheckDealerExistInList(Dealer selectedDealer, string CityList)
        {

            var arraySyting = CityList.Substring(0, CityList.Length - 2);

            var array = arraySyting.Split(new string[] {","}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var tmp in lvSelected.Items)
            {
                if (tmp.ToString().Contains(selectedDealer.DealerId.ToString()))
                {
                    foreach (var city in array)
                    {
                        if (tmp.ToString().Contains(city.ToString(CultureInfo.InvariantCulture).Trim()))
                            return true;
                    }

                }
            }
            return false;

        }

        private string GetCityList()
        {
            var builder = new StringBuilder();

            if (cbOrangeCounty.Checked)
                builder.Append(cbOrangeCounty.Text + ", ");

            if (cbLongBeach.Checked)
                builder.Append(cbLongBeach.Text + ", ");

            if (cbInlandEmpire.Checked)
                builder.Append(cbInlandEmpire.Text + ", ");

            if (cbNorthSanDiego.Checked)
                builder.Append(cbNorthSanDiego.Text + ", ");

            if (cbVentura.Checked)
                builder.Append(cbVentura.Text + ", ");

            if (cbPalmSpring.Checked)
                builder.Append(cbPalmSpring.Text + ", ");

            if (cbLaCentral.Checked)
                builder.Append(cbLaCentral.Text + ", ");

            if (cbAntelopeValley.Checked)
                builder.Append(cbAntelopeValley.Text + ", ");

            if (cbSanGabriel.Checked)
                builder.Append(cbSanGabriel.Text + ", ");

            if (cbPhoneixNorth.Checked)
                builder.Append(cbPhoneixNorth.Text + ", ");

            if (cbPhoneixSouth.Checked)
                builder.Append(cbPhoneixSouth.Text + ", ");

            if (cbPhoenixEast.Checked)
                builder.Append(cbPhoenixEast.Text + ", ");

            if (cbEastSanDiego.Checked)
                builder.Append(cbEastSanDiego.Text + ", ");

            if (cbCitySanDiego.Checked)
                builder.Append(cbCitySanDiego.Text + ", ");

            if (cbWestside.Checked)
                builder.Append(cbWestside.Text + ", ");

            return builder.ToString();

        }

        private void BtnCityClearClick(object sender, EventArgs e)
        {
            cbOrangeCounty.Checked = false;

            cbLongBeach.Checked = false;

            cbNorthSanDiego.Checked = false;

            cbInlandEmpire.Checked = false;

            cbVentura.Checked = false;

            cbPalmSpring.Checked = false;

            cbLaCentral.Checked = false;

            cbAntelopeValley.Checked = false;

            cbSanGabriel.Checked = false;

            cbCitySanDiego.Checked = false;

            cbEastSanDiego.Checked = false;

            cbWestside.Checked = false;

            cbNoPrice.Checked = false;

        }

        private void BtnScheduleClick(object sender, EventArgs e)
        {
            if (lvSelected.Items.Count > 0)
            {
                var context = new whitmanenterprisecraigslistEntities();

                foreach (var tmp in lvSelected.Items)
                {
                    var dealerId = Convert.ToInt32(tmp.ToString().Substring(0, tmp.ToString().IndexOf("-")).Trim());

                    var cityString =
                        tmp.ToString().Substring(tmp.ToString().IndexOf("-", System.StringComparison.Ordinal) + 1);

                    cityString = cityString.Substring(0, cityString.IndexOf("-", System.StringComparison.Ordinal) - 1);

                    var cityArray = cityString.Split(new string[] {","}, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var cityName in cityArray)
                    {
                        var ctString = cityName.Trim();
                        var accountPc = new whitmanenterprisecomputeraccount()
                                            {
                                                AccountPC = Convert.ToInt32(txtAcctPc.Text),
                                                CityId = Globalvar.CityList.First(x => x.CityName == ctString).CityID,
                                                DealerId = dealerId,
                                                LastUpdated = DateTime.Now,
                                                Location = "Irvine",
                                                Price = !tmp.ToString().Contains(" No Price ")

                                            };


                        context.AddTowhitmanenterprisecomputeraccounts(accountPc);

                        context.SaveChanges();
                    }



                }

                Globalvar.ComputerList = new List<Computer>();

                foreach (var computertmp in context.whitmanenterprisecomputeraccounts.ToList())
                {

                    Globalvar.ComputerList.Add(new Computer()
                    {
                        PcAccount = computertmp.AccountPC,
                        CityId = computertmp.CityId,
                        DealerId = computertmp.DealerId
                    }
                    );

                }

                txtAcctPc.Text = (Convert.ToInt32(txtAcctPc.Text) + 1).ToString(CultureInfo.InvariantCulture);

                cbOrangeCounty.Checked = false;

                cbLongBeach.Checked = false;

                cbInlandEmpire.Checked = false;

                cbVentura.Checked = false;

                cbNorthSanDiego.Checked = false;


                cbPalmSpring.Checked = false;

                cbLaCentral.Checked = false;

                cbAntelopeValley.Checked = false;

                cbSanGabriel.Checked = false;

                cbWestside.Checked = false;

                cbEastSanDiego.Checked = false;

                cbCitySanDiego.Checked = false;

                cbNoPrice.Checked = false;

                lvSelected.Items.Clear();

                lblTotalRound.Text = "0";

                lblTotalCars.Text = "0";


            }



        }

        private void BtnClearFinalListClick(object sender, EventArgs e)
        {
            cbNoPrice.Checked = false;
            lvSelected.Items.Clear();
            lblTotalRound.Text = "0";
            lblTotalCars.Text = "0";

        }

        private void BtnClearScheduleClick(object sender, EventArgs e)
        {
            var context = new whitmanenterprisecraigslistEntities();

            foreach (var tmp in context.whitmanenterprisecomputeraccounts.ToList())
            {
                context.Attach(tmp);
                context.DeleteObject(tmp);
            }
            context.SaveChanges();
            txtAcctPc.Text = "1";
            lblTotalRound.Text = "0";
            lblTotalCars.Text = "0";


            MessageBox.Show("Schedule was cleared ", "Message", MessageBoxButtons.OK,
                                                 MessageBoxIcon.Warning);
        }

        private void BtnCheckEmailClick(object sender, EventArgs e)
        {
            var context = new whitmanenterprisecraigslistEntities();

            var emailAccountList = context.vinclappemailaccounts.ToList().OrderBy(a => a.AccountName).ToList();

            foreach (var account in emailAccountList)
            {

                int postingAds = 0;

                int ghostedAds = 0;

                int totalAds = 0;

                if (_browser == null)
                {


                    _browser = new IE("https://accounts.craigslist.org/");

                    _browser.ClearCookies();
                }
                else
                {
                    _browser.ClearCache();

                    _browser.ClearCookies();

                    _browser.Close();

                    _browser = null;

                    _browser = new IE("https://accounts.craigslist.org/");

                }


                if (IsAlreadyLogin())
                {
                    System.Threading.Thread.Sleep(2000);

                    _browser.Span(Find.ById("ef")).Links.First().Click();

                    System.Threading.Thread.Sleep(4000);


                }

                _browser.SizeWindow(500, 1000);

                _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowNoActivate);

                System.Threading.Thread.Sleep(2000);


                _browser.TextField(Find.ByName("inputEmailHandle")).TypeText(account.AccountName);

                _browser.TextField(Find.ByName("inputPassword")).TypeText(account.AccountPassword);

                System.Threading.Thread.Sleep(2000);

                _browser.Button(Find.ByValue("Log In")).Click();

                var htmlWeb = new HtmlWeb();

                var htmlDoc = new HtmlAgilityPack.HtmlDocument();

                htmlWeb.AutoDetectEncoding = true;

                htmlDoc.LoadHtml(_browser.Html);

                var flagNode =
                    htmlDoc.DocumentNode.SelectNodes(
                        ".//td[@style='BORDER-BOTTOM: pink 1px solid; BORDER-LEFT: pink 1px solid; BACKGROUND: #fed; BORDER-TOP: pink 1px solid; BORDER-RIGHT: pink 1px solid']");

                var mainNode =
                    htmlDoc.DocumentNode.SelectNodes(
                        ".//td[@style='BORDER-BOTTOM: lightgreen 1px solid; BORDER-LEFT: lightgreen 1px solid; BACKGROUND: #dfd; BORDER-TOP: lightgreen 1px solid; BORDER-RIGHT: lightgreen 1px solid']//small");

                _trackingList = new List<TrackingCLAccount>();

                var city = "";


                if (mainNode != null && mainNode.Any())
                {

                    foreach (var tmp in mainNode)
                    {
                        var trackAccount = new TrackingCLAccount();
                        if (tmp.InnerText.Contains("trucks - by dealer"))
                        {
                            city = tmp.InnerText.Substring(0, 3);
                        }
                        else
                        {
                            UInt32 number = 0;

                            bool flag = UInt32.TryParse(tmp.InnerText, out number);

                            if (flag)
                            {
                                trackAccount.CLAccount = "";
                                trackAccount.CLPassword = "";
                                trackAccount.PostingId = number;
                                trackAccount.City = city;
                                _trackingList.Add(trackAccount);
                            }

                        }



                    }
                    if (_trackingList.Count > 0)
                    {
                        totalAds = _trackingList.Count;

                        subCloneList = new List<TrackingCLAccount>();

                        subCloneList = _trackingList.GetRange(0, _trackingList.Count);

                        var ghostBuilder = new StringBuilder();

                        var postBuilder = new StringBuilder();

                        foreach (var trackAccount in subCloneList)
                        {
                            if (_browser == null)
                            {
                                if (trackAccount.City.Equals("lax"))
                                    _browser = new IE("http://losangeles.craigslist.org/ctd/");

                                else if (trackAccount.City.Equals("orc"))
                                {
                                    _browser = new IE("http://orangecounty.craigslist.org/ctd/");
                                }
                                else if (trackAccount.City.Equals("inl"))
                                {
                                    _browser = new IE("http://inlandempire.craigslist.org/ctd/");

                                }
                                else if (trackAccount.City.Equals("sdo"))
                                {
                                    _browser = new IE("http://sandiego.craigslist.org/ctd/");

                                }
                                else if (trackAccount.City.Equals("phx"))
                                {
                                    _browser = new IE("http://phoenix.craigslist.org/ctd/");

                                }
                                else if (trackAccount.City.Equals("psp"))
                                {
                                    _browser = new IE("http://palmsprings.craigslist.org/ctd/");

                                }
                                _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);
                            }
                            else
                            {

                                if (trackAccount.City.Equals("lax"))
                                    _browser.GoTo("http://losangeles.craigslist.org/ctd/");

                                else if (trackAccount.City.Equals("orc"))
                                {
                                    _browser.GoTo("http://orangecounty.craigslist.org/ctd/");

                                }
                                else if (trackAccount.City.Equals("inl"))
                                {
                                    _browser.GoTo("http://inlandempire.craigslist.org/ctd/");

                                }
                                else if (trackAccount.City.Equals("sdo"))
                                {
                                    _browser.GoTo("http://sandiego.craigslist.org/ctd/");

                                }
                                else if (trackAccount.City.Equals("phx"))
                                {
                                    _browser.GoTo("http://phoenix.craigslist.org/ctd/");

                                }
                                else if (trackAccount.City.Equals("psp"))
                                {
                                    _browser.GoTo("http://palmsprings.craigslist.org/ctd/");

                                }
                                _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);
                            }
                            //_browser.TextField(Find.ById("query")).Value =
                            //    trackAccount.PostingId.ToString(CultureInfo.InvariantCulture);

                            //_browser.RadioButton(Find.ByTitle("search the entire posting")).Checked = true;

                            //_browser.Button(Find.ByValue("Search")).Click();




                            htmlDoc = new HtmlAgilityPack.HtmlDocument();

                            htmlDoc.LoadHtml(_browser.Html);

                            var subNode = htmlDoc.DocumentNode;

                            if (subNode.InnerText.Contains("Found: 1"))
                            {
                                postingAds++;
                                postBuilder.AppendLine(trackAccount.PostingId.ToString(CultureInfo.InvariantCulture) +
                                                       ",");
                            }
                            else if (subNode.InnerText.Contains("Nothing found for that search"))
                            {
                                ghostedAds++;
                                ghostBuilder.AppendLine(trackAccount.PostingId.ToString(CultureInfo.InvariantCulture) +
                                                        ",");

                            }
                            System.Threading.Thread.Sleep(500);
                        }


                        var vinclapptrackingaccount = new vinclapptrackingemailaccount()
                                                          {
                                                              AccountName = account.AccountName,
                                                              TrackingDate = DateTime.Now,
                                                              GhostingAds = ghostedAds,
                                                              PostingAds = postingAds,
                                                              NumberAds = totalAds,
                                                              Percentage = postingAds*100/totalAds,
                                                              PostingAdsId = postBuilder.ToString(),
                                                              GhostingAdsIds = ghostBuilder.ToString()


                                                          };
                        if (flagNode != null && flagNode.Any())
                        {
                            vinclapptrackingaccount.FlagAds = flagNode.Count()/5;
                            vinclapptrackingaccount.NumberAds += flagNode.Count()/5;

                        }
                        else
                        {
                            vinclapptrackingaccount.FlagAds = 0;
                        }

                        

                        context.AddTovinclapptrackingemailaccounts(vinclapptrackingaccount);





                    }
                    else
                    {
                        var vinclapptrackingaccount = new vinclapptrackingemailaccount()
                                                          {
                                                              AccountName = account.AccountName,
                                                              TrackingDate = DateTime.Now,
                                                              GhostingAds = ghostedAds,
                                                              PostingAds = postingAds,
                                                              NumberAds = totalAds,
                                                              Percentage = 0,
                                                              FlagAds = 0,

                                                          };



                        context.AddTovinclapptrackingemailaccounts(vinclapptrackingaccount);
                    }






                }


                else
                {
                    var vinclapptrackingaccount = new vinclapptrackingemailaccount()
                                                      {
                                                          AccountName = account.AccountName,
                                                          TrackingDate = DateTime.Now,
                                                          GhostingAds = ghostedAds,
                                                          PostingAds = postingAds,
                                                          NumberAds = totalAds,
                                                          Percentage = 0,
                                                          FlagAds = 0,

                                                      };

                 


                    context.AddTovinclapptrackingemailaccounts(vinclapptrackingaccount);
                }

                context.SaveChanges();

            }



            drgWalterTracking.DataSource =
                context.vinclapptrackingemailaccounts.Where(
                    x =>
                    x.TrackingDate.Year == DateTime.Now.Year && x.TrackingDate.Month == DateTime.Now.Month &&
                    x.TrackingDate.Day == DateTime.Now.Day).ToList();



        }

        private void RunPattern(TrackingCLAccount trackAccount)
        {
            if (_browser == null)
            {
                if (trackAccount.City.Equals("lax"))
                    _browser = new IE("http://losangeles.craigslist.org/ctd/");

                else if (trackAccount.City.Equals("orc"))
                {
                    _browser = new IE("http://orangecounty.craigslist.org/ctd/");
                }
                else if (trackAccount.City.Equals("inl"))
                {
                    _browser = new IE("http://inlandempire.craigslist.org/ctd/");

                }
                else if (trackAccount.City.Equals("sdo"))
                {
                    _browser = new IE("http://sandiego.craigslist.org/ctd/");

                }
                _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);
            }
            else
            {

                if (trackAccount.City.Equals("lax"))
                    _browser.GoTo("http://losangeles.craigslist.org/ctd/");

                else if (trackAccount.City.Equals("orc"))
                {
                    _browser.GoTo("http://orangecounty.craigslist.org/ctd/");

                }
                else if (trackAccount.City.Equals("inl"))
                {
                    _browser.GoTo("http://inlandempire.craigslist.org/ctd/");

                }
                else if (trackAccount.City.Equals("sdo"))
                {
                    _browser.GoTo("http://sandiego.craigslist.org/ctd/");

                }
                _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);
            }
            _browser.TextField(Find.ById("query")).Value = trackAccount.PostingId.ToString(CultureInfo.InvariantCulture);

            _browser.RadioButton(Find.ByTitle("search the entire posting")).Checked = true;

            _browser.Button(Find.ByValue("Search")).Click();

            var htmlWeb = new HtmlWeb();

            var htmlDoc = new HtmlAgilityPack.HtmlDocument();

            htmlWeb.AutoDetectEncoding = true;

            htmlDoc.LoadHtml(_browser.Html);


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

        private List<TrackingCLAccount> subCloneList = null;


        private void TimerSerachTick(object sender, EventArgs e)
        {
            if (subCloneList.Count > 0)
            {
                var trackingAccount = subCloneList.ElementAt(0);

                RunPattern(trackingAccount);

                subCloneList.RemoveAt(0);
            }

        }

        private void BtnDealerClick(object sender, EventArgs e)
        {


            foreach (var tmp in _trackingdealerList)
            {
                try
                {
                    if (_browser == null)
                    {

                        _browser = new IE(tmp.CityUrl);


                        _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);
                    }
                    else
                    {

                        _browser.GoTo(tmp.CityUrl);
                    }

                    _browser.TextField(Find.ById("query")).Value =
                        tmp.TrackingId.ToString(CultureInfo.InvariantCulture);

                    _browser.RadioButton(Find.ByTitle("search the entire posting")).Checked = true;

                    _browser.Button(Find.ByValue("Search")).Click();

                    var htmlWeb = new HtmlWeb();

                    var htmlDoc = new HtmlAgilityPack.HtmlDocument();

                    htmlWeb.AutoDetectEncoding = true;

                    htmlDoc.LoadHtml(_browser.Html);


                    var subNode = htmlDoc.DocumentNode.SelectSingleNode(".//h4[@class='ban']");


                    if (subNode != null && subNode.InnerText.Contains("Found: "))
                    {
                        var filterString = subNode.InnerText.Replace("&nbsp; &nbsp; Found: ", "");

                        filterString = filterString.Replace("&nbsp; Next &gt;&gt; Found: ", "");

                        var numberResult = filterString.Substring(0,
                                                                  filterString.IndexOf(" ",
                                                                                       System.StringComparison.Ordinal));

                        tmp.NumberOfShowedAds = Convert.ToInt32(numberResult);


                    }
                    else
                    {
                        tmp.NumberOfShowedAds = 0;
                    }

                    System.Threading.Thread.Sleep(500);
                }



                catch (Exception)
                {


                }
            }

            drgDealerTracking.DataSource = _trackingdealerList;

            lblNumberAds.Text =
                _trackingdealerList.Select(x => x.NumberOfShowedAds).Sum().ToString(CultureInfo.InvariantCulture);

            var percentage =
                Math.Ceiling(((double) _trackingdealerList.Select(x => x.NumberOfShowedAds).Sum()/
                              _trackingdealerList.Select(x => x.NumberOfCars).Sum())*100);

            lblNumberPercent.Text = percentage.ToString(CultureInfo.InvariantCulture);

            lblReportStatus.Visible = true;

            Refresh();

        }


        //private void trackingCalendar_DateSelected(object sender, DateRangeEventArgs e)
        //{
        //    DateTime dt = trackingCalendar.SelectionRange.Start;

        //    lblChooseDate.Text = "Report Date = " + dt.ToShortDateString();

        //    foreach (var tmp in _trackingdealerList)
        //    {
        //        var trackingid =
        //            Convert.ToUInt64(Convert.ToInt64(tmp.DealerId)*dt.Year*dt.Month*dt.Day);

        //        tmp.TrackingId = trackingid;

        //    }

        //    drgDealerTracking.DataSource = _trackingdealerList.OrderBy(x => x.DealerId).ToList();
        //}

        private void MainTabSelectedIndexChanged(object sender, EventArgs e)
        {
            if (mainTab.SelectedTab.Text.Equals("Report"))
            {
                cbComputer.Items.Clear();

                cbReportComputer.Items.Add("Select All");

                foreach (var tmp in Globalvar.ComputerList.OrderBy(x => x.PcAccount).Select(x => x.PcAccount).Distinct()
                    )
                {
                    cbReportComputer.Items.Add(tmp.ToString(CultureInfo.InvariantCulture));
                }

                if (cbReportComputer.Items.Count > 0)
                    cbReportComputer.SelectedIndex = 0;

                _trackingdealerList = new List<TrackingDealer>();

                trackingCalendar.MaxDate = DateTime.Now;

                lblChooseDate.Text = "Report Date = " + DateTime.Now.ToShortDateString();

                var context = new whitmanenterprisecraigslistEntities();


                var dealerList = from a in context.whitmanenterprisecomputeraccounts
                                 from b in context.whitmanenterprisedealerlists
                                 from c in context.whitmanenterprisecraigslistcities

                                 where a.DealerId == b.VincontrolId && a.CityId == c.CityID && b.Active.Value
                                 select new
                                     {
                                         b.VincontrolId,
                                         b.DealershipName,
                                         c.CityName,
                                         c.CityID,
                                         c.CraigsListCityURL,
                                         b.TradeInBannerLink


                                     };



                foreach (var tmp in dealerList)
                {


                    var trackingDealer = new TrackingDealer()
                        {
                            DealerId = Convert.ToInt32(tmp.VincontrolId),
                            City = tmp.CityName,
                            DealershipName = tmp.DealershipName,
                            NumberOfCars = 0,
                            NumberOfShowedAds = 0,
                            TradeInBannerLink = tmp.TradeInBannerLink,
                            CityId = tmp.CityID,
                            CityUrl = tmp.CraigsListCityURL + "ctd/"
                        };


                    if (
                        Globalvar.ComputerList.Any(
                            x => x.DealerId == trackingDealer.DealerId && x.CityId == trackingDealer.CityId))
                    {

                        var accountPc =
                            Globalvar.ComputerList.First(
                                x => x.DealerId == trackingDealer.DealerId && x.CityId == trackingDealer.CityId);

                        trackingDealer.Computer = accountPc.PcAccount;
                    }
                    else
                    {
                        trackingDealer.Computer = 0;
                    }

                    trackingDealer.NumberOfCars =
                        Globalvar.DealerList.First(x => x.DealerId == trackingDealer.DealerId).NumberOfCars;

                    _trackingdealerList.Add(trackingDealer);
                }

                drgDealerTracking.DataSource = _trackingdealerList.OrderBy(x => x.DealerId).ToList();

                lblNumberDealer.Text =
                    _trackingdealerList.Select(x => x.DealerId)
                                       .Distinct()
                                       .Count()
                                       .ToString(CultureInfo.InvariantCulture);

                lblNumberCity.Text =
                    _trackingdealerList.Select(x => x.CityId)
                                       .Distinct()
                                       .Count()
                                       .ToString(CultureInfo.InvariantCulture);

                lblNumberInventory.Text =
                    (_trackingdealerList.Select(x => x.NumberOfCars).Sum()/2+1).ToString(CultureInfo.InvariantCulture);


                lblReportStatus.Visible = false;
                Refresh();

               

            }
            else if (mainTab.SelectedTab.Text.Equals("Email Account"))
            {
                cbComputer.Items.Clear();
                lblEmailStatus.Visible = false;
                rtbErrors.Text = "";

                for (var i = 1; i <= Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MaximumComputer"].ToString(CultureInfo.InvariantCulture)); i++)
                {
                    cbComputer.Items.Add(i);
                }

                if (cbComputer.Items.Count > 0)
                    cbComputer.SelectedIndex = 0;

                var computerId = (int)cbComputer.SelectedItem;

                for (var i = 1; i <= Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MaximumComputer"].ToString(CultureInfo.InvariantCulture)); i++)
                {
                    if (i != computerId)
                        cbTransfer.Items.Add(i);
                }

                if (cbTransfer.Items.Count > 0)
                    cbTransfer.SelectedIndex = 0;

                //if (Globalvar.ComputerList.Any())
                //{

                //    foreach (var tmp in Globalvar.ComputerList.Select(x => x.PcAccount).Distinct())
                //    {
                //        cbComputer.Items.Add(tmp);
                //    }

                //    if (cbComputer.Items.Count > 0)
                //        cbComputer.SelectedIndex = 0;

                //    var computerId = (int) cbComputer.SelectedItem;

                //    foreach (var tmp in Globalvar.ComputerList.Select(x => x.PcAccount).Distinct())
                //    {
                //        if (tmp != computerId)
                //            cbTransfer.Items.Add(tmp);
                //    }

                //    if (cbTransfer.Items.Count > 0)
                //        cbTransfer.SelectedIndex = 0;
                //}
                //else
                //{
                //    MessageBox.Show("You need to have at least a schedule for email");
                //}
            }
            else if (mainTab.SelectedTab.Text.Equals("Schedule"))
            {
                dgridSchedule.Rows.Clear();

                dtPickerForRegenerateImage.MaxDate = DateTime.Now;

                waitBox.Visible = true;

                bgScheduleWorker.RunWorkerAsync();


            }

            else if (mainTab.SelectedTab.Text.Equals("Daily View"))
            {
                var dtCompare = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                var context = new whitmanenterprisecraigslistEntities();

                Globalvar.GridScheduleList = new List<GridScheuleModel>();

                Globalvar.TrackingList = new List<TrackingDealer>();
                var soFarList = from a in context.vincontrolcraigslisttrackings
                                where a.CLPostingId > 0 && a.AddedDate >= dtCompare && a.AddedDate < DateTime.Now
                                select new
                                {
                                    a.TrackingId,
                                    a.Computer,
                                    a.ShowAd,
                                    a.DealerId,
                                    a.AddedDate,
                                    a.Renew

                                };

                foreach (var tmp in soFarList)
                {
                    var trackingDealer = new TrackingDealer()
                    {
                        TrackingId = tmp.TrackingId,
                        Computer = tmp.Computer.GetValueOrDefault(),
                        ShowedAd = tmp.ShowAd.GetValueOrDefault(),
                        DealerId = tmp.DealerId.GetValueOrDefault(),
                        RenewAd = tmp.Renew.GetValueOrDefault(),
                    };

                    Globalvar.TrackingList.Add(trackingDealer);
                }


                dGridDailyView.Rows.Clear();

                int newPosts = 0;

                foreach (var tmp in Globalvar.DealerList.OrderBy(x => x.DealerId))
                {
                    int numberOfSupposedAds = 0;



                    var newRow = new DataGridViewRow();

                    newRow.CreateCells(dGridDailyView);
                    newRow.Cells[0].Value = tmp.DealerId;
                    newRow.Cells[1].Value = tmp.DealershipName;
                    newRow.Cells[2].Value = tmp.NumberOfCars;
                    newRow.Cells[3].Value = Globalvar.TrackingList.Count(x => x.DealerId == tmp.DealerId);
                    newRow.Cells[5].Value = Globalvar.TrackingList.Count(x => x.DealerId == tmp.DealerId && x.RenewAd == true);
                    newRow.Cells[6].Value = Globalvar.TrackingList.Count(x => x.DealerId == tmp.DealerId && x.ShowedAd == true);


                    if (tmp.ScheduleCityList.Any(x => x.City == 1))
                    {
                        numberOfSupposedAds += tmp.NumberOfCars;
                        newRow.Cells[7].Value = 1;
                    }
                    if (tmp.ScheduleCityList.Any(x => x.City == 10))
                    {
                        numberOfSupposedAds += tmp.NumberOfCars;
                        newRow.Cells[8].Value = 1;
                    }
                    if (tmp.ScheduleCityList.Any(x => x.City == 12))
                    {
                        numberOfSupposedAds += tmp.NumberOfCars;
                        newRow.Cells[9].Value = 1;
                    }
                    if (tmp.ScheduleCityList.Any(x => x.City == 3))
                    {
                        numberOfSupposedAds += tmp.NumberOfCars;
                        newRow.Cells[10].Value = 1;
                    }
                    if (tmp.ScheduleCityList.Any(x => x.City == 8))
                    {
                        numberOfSupposedAds += tmp.NumberOfCars;
                        newRow.Cells[11].Value = 1;
                    }
                    if (tmp.ScheduleCityList.Any(x => x.City == 13))
                    {
                        numberOfSupposedAds += tmp.NumberOfCars;
                        newRow.Cells[12].Value = 1;
                    }
                    if (tmp.ScheduleCityList.Any(x => x.City == 9))
                    {
                        numberOfSupposedAds += tmp.NumberOfCars;
                        newRow.Cells[13].Value = 1;
                    }


                    if (tmp.ScheduleCityList.Any(x => x.Split == true))
                    {
                        newRow.Cells[2].Style.BackColor = System.Drawing.Color.SteelBlue;

                        if (numberOfSupposedAds > 0)
                        {
                            newRow.Cells[3].Value = (numberOfSupposedAds / 2) + " / " +
                                      Globalvar.TrackingList.Count(x => x.DealerId == tmp.DealerId);

                            if (numberOfSupposedAds / 2 -
                                                    Globalvar.TrackingList.Count(x => x.DealerId == tmp.DealerId) > 0)

                                newRow.Cells[4].Value = numberOfSupposedAds / 2 -
                                                        Globalvar.TrackingList.Count(x => x.DealerId == tmp.DealerId);
                            else
                            {
                                newRow.Cells[4].Value = 0;
                            }


                            newPosts += numberOfSupposedAds / 2 - Globalvar.TrackingList.Count(x => x.DealerId == tmp.DealerId);
                        }

                        else
                        {
                            newRow.Cells[4].Value = 0;
                        }

                        newRow.Cells[14].Value = 1;

                    }
                    else
                    {
                        if (numberOfSupposedAds > 0)
                        {
                            newRow.Cells[3].Value = numberOfSupposedAds + " / " +
                                           Globalvar.TrackingList.Count(x => x.DealerId == tmp.DealerId);

                            if (numberOfSupposedAds -
                                                    Globalvar.TrackingList.Count(x => x.DealerId == tmp.DealerId) > 0)

                                newRow.Cells[4].Value = numberOfSupposedAds -
                                                        Globalvar.TrackingList.Count(x => x.DealerId == tmp.DealerId);

                            else
                            {
                                newRow.Cells[4].Value = 0;
                            }

                            newPosts += numberOfSupposedAds -
                                       Globalvar.TrackingList.Count(x => x.DealerId == tmp.DealerId);
                        }

                        else
                        {
                            newRow.Cells[4].Value = 0;
                        }

                        newRow.Cells[14].Value = 0;
                    }



                    if (tmp.ScheduleCityList.Any(x => x.Price == true))
                        newRow.Cells[15].Value = 1;



                    dGridDailyView.Rows.Add(newRow);
                }


                lblTotalInventory.Text = Globalvar.DealerList.Sum(x => x.NumberOfCars).ToString(CultureInfo.InvariantCulture);

                lblRenewPost.Text = Globalvar.TrackingList.Count(x => x.RenewAd == true).ToString();

                lblNewPostTotal.Text = newPosts.ToString(CultureInfo.InvariantCulture);

                lblAverage.Text = Math.Ceiling((double)Globalvar.DealerList.Sum(x => x.NumberOfCars) / Globalvar.SplitSchedules).ToString();

                nuNumberSchedules.Value = Globalvar.SplitSchedules;


            }


        }
        private void BgScheduleWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var dtCompare = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var dtCompareNextDay = dtCompare.AddDays(1);

            var context = new whitmanenterprisecraigslistEntities();

            Globalvar.GridScheduleList = new List<GridScheuleModel>();

            Globalvar.TrackingList = new List<TrackingDealer>();

            var soFarList = from a in context.vincontrolcraigslisttrackings
                            where a.AddedDate >= dtCompare && a.AddedDate < dtCompareNextDay
                            select new
                            {
                                a.TrackingId,
                                a.Computer,
                                a.ShowAd

                            };

            foreach (var tmp in soFarList)
            {
                var trackingDealer = new TrackingDealer()
                                         {
                                             TrackingId = tmp.TrackingId,
                                             Computer = tmp.Computer.GetValueOrDefault(),
                                             ShowedAd = tmp.ShowAd.GetValueOrDefault()
                                         };

                Globalvar.TrackingList.Add(trackingDealer);
            }
            
            Globalvar.GridScheduleList = MySqlHelper.GetComputerInfoList();


        }
        private void BgScheduleWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                waitBox.Visible = false;

                foreach (var tmp in Globalvar.GridScheduleList.OrderBy(x=>x.Computer))
                {

                    var newRow = new DataGridViewRow();

                    newRow.CreateCells(dgridSchedule);
                    newRow.Cells[0].Value = tmp.Computer;
                    newRow.Cells[1].Value = tmp.Cars + " cars / " + (tmp.Cars/10 + 1) + " rounds" ;
                    newRow.Cells[2].Value = (tmp.Cars / 2 + 1);
                    newRow.Cells[3].Value = Globalvar.TrackingList.Count(x => x.Computer == tmp.Computer);
                    newRow.Cells[4].Value = Globalvar.TrackingList.Count(x => x.Computer == tmp.Computer && x.ShowedAd);
                    newRow.Cells[4].Style.ForeColor = System.Drawing.Color.Red;
                    newRow.Cells[5].Value = "Update";
                    newRow.Cells[6].Value = "Delete";

                    dgridSchedule.Rows.Add(newRow);
                }

                lblInventoryInSchedule.Text = (Globalvar.GridScheduleList.Sum(x => x.Cars)/2).ToString(CultureInfo.InvariantCulture);

                lblPostedInSchedule.Text = (Globalvar.TrackingList.Count ).ToString(CultureInfo.InvariantCulture);

                lblShowInSchedule.Text = (Globalvar.TrackingList.Count(x => x.ShowedAd) ).ToString(CultureInfo.InvariantCulture);

                var percentage =
                     Math.Ceiling(((double)Globalvar.TrackingList.Count(x => x.ShowedAd) /
                                   Globalvar.TrackingList.Count()) * 100);

                lblSuccessfulInSchedule.Text = percentage.ToString(CultureInfo.InvariantCulture) + "%";

                this.Refresh();
            }
        }

        private void BtnExportClick(object sender, EventArgs e)
        {
           
            DateTime dt = trackingCalendar.SelectionRange.Start;

            var context = new whitmanenterprisecraigslistEntities();

            var dtCompare = new DateTime(dt.Year, dt.Month, dt.Day);

            var dtCompareNextDay = dtCompare.AddDays(1);


            var fullList = from a in context.vincontrolcraigslisttrackings
                           where a.AddedDate >= dtCompare && a.AddedDate < dtCompareNextDay  && a.Renew==false
                                select new
                                {
                                    a.EmailAccount,
                                    a.Computer,
                                    a.ShowAd
                                    
                                    
                                };


            var fullEmailList = from a in fullList.ToList()
                                group a by new { a.EmailAccount, a.Computer } into g
                                select new
                                {
                                    Email = g.Key.EmailAccount,
                                    ComputerPc = g.Key.Computer,
                                    PostedAds = g.Count(),
                                    ShowedAds = g.Count(x => x.ShowAd == true)

                                };

                                
            var trackingEmailList = new List<TrackingEmail>();

            foreach (var tmp in fullEmailList)
            {
                var trackingEmail = new TrackingEmail()
                                        {
                                            Email = tmp.Email,
                                            Computer = tmp.ComputerPc.GetValueOrDefault(),
                                            PostedAds = tmp.PostedAds,
                                            ShowedAds = tmp.ShowedAds

                                            
                                        };

                var percentage =
             Math.Ceiling(((double)trackingEmail.ShowedAds /
                             trackingEmail.PostedAds) * 100);


                trackingEmail.SuccessfulRate = percentage + "%";

                trackingEmailList.Add(trackingEmail);
            }




            var csvWriterDealer = new CsvExport<TrackingEmail>(trackingEmailList.OrderBy(x => x.Computer).ToList());



            string pathDownload = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            string fullFilePathForDealer = pathDownload + "/trackingemail" + "_" + DateTime.Now.ToString("MMddyy") +
                                           DateTime.Now.Millisecond +
                                           ".csv";

            csvWriterDealer.ExportToFileWithHeader(fullFilePathForDealer);


            var xlApp = new Excel.ApplicationClass();

            object missing = System.Reflection.Missing.Value;

            xlApp.Workbooks.OpenText
                (
                    fullFilePathForDealer,
                    Excel.XlPlatform.xlWindows, //Origin
                    1, // Start Row
                    Excel.XlTextParsingType.xlDelimited, //Datatype
                    Excel.XlTextQualifier.xlTextQualifierNone, //TextQualifier
                    false, // Consecutive Deliminators
                    false, // tab
                    false, // semicolon
                    true, //COMMA
                    false, // space
                    false, // other
                    missing, // Other Char
                    missing, // FieldInfo
                    missing, //TextVisualLayout
                    missing, // DecimalSeparator
                    missing, // ThousandsSeparator
                    missing, // TrialingMionusNumbers
                    missing //Local
                );


            xlApp.Visible = true;



        }

        private void BtnServerReportClick(object sender, EventArgs e)
        {
            DateTime dt = trackingCalendar.SelectionRange.Start;

            var context = new whitmanenterprisecraigslistEntities();

            var dtCompare = new DateTime(dt.Year, dt.Month, dt.Day);

            var dtCompareNextDay = dtCompare.AddDays(1);


            var fullList = from a in context.vincontrolcraigslisttrackings
                           where a.AddedDate >= dtCompare && a.AddedDate < dtCompareNextDay
                           select new
                           {
                               a.EmailAccount,
                               a.Computer,
                               a.ShowAd


                           };

            var fullServerList = from a in fullList.ToList()
                                group a by new { a.Computer} into g
                                select new
                                {
                                    ComputerPc = g.Key.Computer,
                                    PostedAds = g.Count(),
                                    ShowedAds = g.Count(x => x.ShowAd == true)

                                };


            var trackingServerList = new List<TrackingServer>();

            foreach (var tmp in fullServerList)
            {
                var trackingServer = new TrackingServer()
                {
                    Computer = tmp.ComputerPc.GetValueOrDefault(),
                    PostedAds = tmp.PostedAds,
                    ShowedAds = tmp.ShowedAds


                };
                var percentage =
          Math.Ceiling(((double)trackingServer.ShowedAds /
                          trackingServer.PostedAds) * 100);


                trackingServer.SuccessfulRate = percentage + "%";
                trackingServerList.Add(trackingServer);
            }

            var csvWriterDealer = new CsvExport<TrackingServer>(trackingServerList.OrderBy(x => x.Computer).ToList());



            string pathDownload = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            string fullFilePathForDealer = pathDownload + "/trackingserver" + "_" + DateTime.Now.ToString("MMddyy") +
                                           DateTime.Now.Millisecond +
                                           ".csv";

            csvWriterDealer.ExportToFileWithHeader(fullFilePathForDealer);


            var xlApp = new Excel.ApplicationClass();

            object missing = System.Reflection.Missing.Value;

            xlApp.Workbooks.OpenText
                (
                    fullFilePathForDealer,
                    Excel.XlPlatform.xlWindows, //Origin
                    1, // Start Row
                    Excel.XlTextParsingType.xlDelimited, //Datatype
                    Excel.XlTextQualifier.xlTextQualifierNone, //TextQualifier
                    false, // Consecutive Deliminators
                    false, // tab
                    false, // semicolon
                    true, //COMMA
                    false, // space
                    false, // other
                    missing, // Other Char
                    missing, // FieldInfo
                    missing, //TextVisualLayout
                    missing, // DecimalSeparator
                    missing, // ThousandsSeparator
                    missing, // TrialingMionusNumbers
                    missing //Local
                );


            xlApp.Visible = true;
        }

        private void CbComputerSelectedIndexChanged(object sender, EventArgs e)
        {
            lblEmailStatus.Visible = false;

            rtbErrors.Text = "";
            var context = new whitmanenterprisecraigslistEntities();

            var computerId = (int) cbComputer.SelectedItem;

            var emailAccountList = context.vinclappemailaccounts.Where(x => x.Computer == computerId).ToList();

            dgridEmail.Rows.Clear();

            foreach (var tmp in emailAccountList)
            {

                var newRow = new DataGridViewRow();
                ;

                newRow.CreateCells(dgridEmail);
                newRow.Cells[0].Value = tmp.AccountName;
                newRow.Cells[1].Value = tmp.AccountPassword;
                newRow.Cells[2].Value = tmp.Phone;
                newRow.Cells[3].Value = tmp.ProxyIP;
                newRow.Cells[4].Value = tmp.Active != null && tmp.Active.Value;
                newRow.Cells[5].Value = String.IsNullOrEmpty(tmp.Notes) ? "" : tmp.Notes;
                newRow.Cells[6].Value = tmp.LastTimeUsed == null
                                            ? ""
                                            : tmp.LastTimeUsed.GetValueOrDefault().ToShortDateString() + " " +
                                              tmp.LastTimeUsed.GetValueOrDefault().ToLongTimeString();

                dgridEmail.Rows.Add(newRow);
            }


            lblMissingPhone.Text =
                emailAccountList.Count(x => String.IsNullOrEmpty(x.Phone)).ToString(CultureInfo.InvariantCulture);

            lblMissingProxy.Text =
                emailAccountList.Count(x => String.IsNullOrEmpty(x.ProxyIP)).ToString(CultureInfo.InvariantCulture);

            lblEmails.Text = emailAccountList.Count.ToString(CultureInfo.InvariantCulture);

            cbTransfer.Items.Clear();


            for (var i = 1; i <= Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MaximumComputer"].ToString(CultureInfo.InvariantCulture)); i++)
            {
                if (i != computerId)
                    cbTransfer.Items.Add(i);
            }

            if (cbTransfer.Items.Count > 0)
                cbTransfer.SelectedIndex = 0;
        }


        private void BtnApplyClick(object sender, EventArgs e)
        {
            var context = new whitmanenterprisecraigslistEntities();

            pbPicLoad.Visible = true;

            Refresh();

            var computerId = (int) cbComputer.SelectedItem;

            var emailAccountList = context.vinclappemailaccounts.Where(x => x.Computer == computerId).ToList();

            var otheremailAccountList = context.vinclappemailaccounts.Where(x => x.Computer != computerId).ToList();

            var flag = true;

            foreach (var tmp in emailAccountList)
            {
                context.Attach(tmp);
                context.DeleteObject(tmp);
            }

            var finalList = new List<vinclappemailaccount>();

            rtbErrors.Text = "";

            foreach (DataGridViewRow row in dgridEmail.Rows)
            {

                if (row.Index < dgridEmail.Rows.Count - 1)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                    {

                        var newRow = new vinclappemailaccount()
                                         {
                                             AccountName =
                                                 row.Cells[0].Value == null ? "" : row.Cells[0].Value.ToString(),
                                             AccountPassword =
                                                 row.Cells[1].Value == null ? "" : row.Cells[1].Value.ToString(),
                                             Computer = computerId,
                                             DateAdded = DateTime.Now,
                                             
                                             Phone = row.Cells[2].Value == null ? "" : row.Cells[2].Value.ToString(),
                                             ProxyIP = row.Cells[3].Value == null ? "" : row.Cells[3].Value.ToString(),
                                             Active = Convert.ToBoolean(row.Cells[4].Value),
                                             Notes = row.Cells[5].Value == null ? "" : row.Cells[5].Value.ToString()
                                         };

                        newRow.Proxy = !String.IsNullOrEmpty(newRow.ProxyIP);

                        finalList.Add(newRow);

                    }
                    else
                    {
                        flag = false;

                        rtbErrors.Text = "There is some missing required information.";

                        break;
                    }
                }
            }

            if (flag)
            {

                foreach (var tmp in finalList)
                {
                    if (otheremailAccountList.Any(x => x.AccountName == tmp.AccountName && x.Computer != computerId))
                    {
                        rtbErrors.SelectionBullet = true;

                        rtbErrors.Text += tmp.AccountName + " is already existed on computer " +
                                          otheremailAccountList.First(
                                              x => x.AccountName == tmp.AccountName).Computer + Environment.NewLine;

                        flag = false;
                    }
                }

                if (flag)
                {
                    foreach (var tmp in finalList)
                    {
                        context.AddTovinclappemailaccounts(tmp);
                    }


                    lblEmailStatus.Visible = true;

                    lblEmailStatus.Text = "Computer " + computerId + " was updated successfully";

                    context.SaveChanges();
                }
            }

            pbPicLoad.Visible = false;


        }

        private void BtnClearEmailClick(object sender, EventArgs e)
        {
            dgridEmail.Rows.Clear();

            var context = new whitmanenterprisecraigslistEntities();

            var computerId = (int) cbComputer.SelectedItem;

            var emailAccountList = context.vinclappemailaccounts.Where(x => x.Computer == computerId).ToList();

            foreach (var tmp in emailAccountList)
            {
                context.Attach(tmp);
                context.DeleteObject(tmp);
            }

            context.SaveChanges();
        }

        private void BtnEmailExportClick(object sender, EventArgs e)
        {
            var context = new whitmanenterprisecraigslistEntities();

            var dtCompare = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var dtCompareNextDay = dtCompare.AddDays(-4);

            var checkList = from a in context.vincontrolcraigslisttrackings
                            where
                                a.ShowAd == true && a.CLPostingId > 0 && a.AddedDate >= dtCompareNextDay &&
                                a.AddedDate < DateTime.Now
                                &&
                                (a.DealerId == 3738 || a.DealerId == 15896 || a.DealerId == 1182 ||
                                 a.DealerId == 7180 || a.DealerId == 2650 || a.DealerId == 44670)
                            select new
                                       {
                                           a.TrackingId,
                                           a.DealerId,
                                           a.CityId,
                                           a.CLPostingId,
                                           a.HtmlCraigslistUrl,
                                           a.CheckDate,
                                           a.AddedDate
                                       };

            var orderList=checkList.OrderBy(x=>x.AddedDate).OrderBy(x=>x.DealerId).ToList();

            drgWalterTracking.DataSource = orderList;

            foreach (var tmp in orderList)
            {

                try
                {
                    if (_browser==null)
                    {
                        _browser =new IE(tmp.HtmlCraigslistUrl);
                    }
                    else
                    {
                        _browser.GoTo(tmp.HtmlCraigslistUrl);
                    }

                   


                    _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);

                    if (_browser.Link(Find.ByIndex(10)) != null)
                    {
                        MessageBox.Show(_browser.Link(Find.ByIndex(10)).Url);

                        if (!_browser.Link(Find.ByIndex(10)).Url.Contains("vinlineup"))
                        {
                            _browser.Link(Find.ByIndex(10)).Click();
                            _browser.Close();
                        }
                    }

                    System.Threading.Thread.Sleep(4000);

                    break;
                    ;
                }
                catch (Exception)
                {

                }
              
            }
        }

        private void BtnScheduleReportClick(object sender, EventArgs e)
        {
            var context = new whitmanenterprisecraigslistEntities();

            var exportList =
                from a in context.whitmanenterprisecomputeraccounts
                from b in context.whitmanenterprisedealerlists
                from c in context.whitmanenterprisecraigslistcities
                where a.DealerId == b.VincontrolId && a.CityId == c.CityID
                select new
                           {
                               a.AccountPC,
                               a.DealerId,
                               b.DealershipName,
                               c.CityID,
                               c.CityName,
                               a.Price

                           };

            var schedule = new List<ScheduelModel>();

            foreach (var tmp in exportList.OrderBy(x => x.AccountPC))
            {
                var scheduleRecord = new ScheduelModel()
                                         {
                                             PCNumber = tmp.AccountPC,
                                             City = tmp.CityName,
                                             DealerId = tmp.DealerId,
                                             DealerName = tmp.DealershipName,
                                             Price = tmp.Price!=null && tmp.Price.GetValueOrDefault()? "Yes" : "No"
                                         };

                schedule.Add(scheduleRecord);
            }

            var csvWriterDealer = new CsvExport<ScheduelModel>(schedule);

            string pathDownload = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            string fullFilePathForDealer = pathDownload + "/ScheduleReport" + "_" + DateTime.Now.ToString("MMddyy") +
                                           DateTime.Now.Millisecond +
                                           ".csv";

            csvWriterDealer.ExportToFileWithHeader(fullFilePathForDealer);


            var xlApp = new Excel.ApplicationClass();
            object missing = System.Reflection.Missing.Value;
            xlApp.Workbooks.OpenText
                (
                    fullFilePathForDealer,
                    Excel.XlPlatform.xlWindows, //Origin
                    1, // Start Row
                    Excel.XlTextParsingType.xlDelimited, //Datatype
                    Excel.XlTextQualifier.xlTextQualifierNone, //TextQualifier
                    false, // Consecutive Deliminators
                    false, // tab
                    false, // semicolon
                    true, //COMMA
                    false, // space
                    false, // other
                    missing, // Other Char
                    missing, // FieldInfo
                    missing, //TextVisualLayout
                    missing, // DecimalSeparator
                    missing, // ThousandsSeparator
                    missing, // TrialingMionusNumbers
                    missing //Local
                );
            xlApp.Visible = true;

        }

        private void BtnClearProxyClick(object sender, EventArgs e)
        {

            foreach (DataGridViewRow row in dgridEmail.Rows)
            {
                row.Cells[3].Value = "";
            }

            BtnApplyClick(sender, e);

        }

        private void BtnStickAdReportClick(object sender, EventArgs e)
        {
            

            DateTime dt = trackingCalendar.SelectionRange.Start;

            var dtCompare = new DateTime(dt.Year, dt.Month, dt.Day);

            var dtCompareNextDay = dtCompare.AddDays(1);

            var context = new whitmanenterprisecraigslistEntities();

            Globalvar.GridScheduleList = new List<GridScheuleModel>();

            Globalvar.TrackingList = new List<TrackingDealer>();

            var soFarList = from a in context.vincontrolcraigslisttrackings
                            where a.AddedDate >= dtCompare && a.AddedDate < dtCompareNextDay
                            select new
                            {
                                a.TrackingId,
                                a.Computer,
                                a.ShowAd

                            };

            if (_reportComputerId == 0)
            {

                foreach (var tmp in soFarList)
                {
                    var trackingDealer = new TrackingDealer()
                        {
                            TrackingId = tmp.TrackingId,
                            Computer = tmp.Computer.GetValueOrDefault(),
                            ShowedAd = tmp.ShowAd.GetValueOrDefault()
                        };

                    Globalvar.TrackingList.Add(trackingDealer);
                }

                Globalvar.GridScheduleList = MySqlHelper.GetComputerInfoList();

                lblInventoryInSchedule.Text =
                    (Globalvar.GridScheduleList.Sum(x => x.Cars)/2).ToString(CultureInfo.InvariantCulture);

                lblTotalAdsForDate.Text = (Globalvar.TrackingList.Count).ToString(CultureInfo.InvariantCulture);

                lblNumberAds.Text =
                    (Globalvar.TrackingList.Count(x => x.ShowedAd)).ToString(CultureInfo.InvariantCulture);

                var percentage =
                    Math.Ceiling(((double) Globalvar.TrackingList.Count(x => x.ShowedAd)/
                                  Globalvar.TrackingList.Count())*100);

                lblNumberPercent.Text = percentage.ToString(CultureInfo.InvariantCulture) + "%";

                lblReportStatus.Visible = true;
            }
            else
            {
                foreach (var tmp in soFarList.Where(x=>x.Computer==_reportComputerId))
                {
                    var trackingDealer = new TrackingDealer()
                    {
                        TrackingId = tmp.TrackingId,
                        Computer = tmp.Computer.GetValueOrDefault(),
                        ShowedAd = tmp.ShowAd.GetValueOrDefault()
                    };

                    Globalvar.TrackingList.Add(trackingDealer);
                }

                Globalvar.GridScheduleList = MySqlHelper.GetComputerInfoList();

                lblInventoryInSchedule.Text =
                    (Globalvar.GridScheduleList.Sum(x => x.Cars) / 2).ToString(CultureInfo.InvariantCulture);

                lblTotalAdsForDate.Text = (Globalvar.TrackingList.Count).ToString(CultureInfo.InvariantCulture);

                lblNumberAds.Text =
                    (Globalvar.TrackingList.Count(x => x.ShowedAd)).ToString(CultureInfo.InvariantCulture);

                var percentage =
                    Math.Ceiling(((double)Globalvar.TrackingList.Count(x => x.ShowedAd) /
                                  Globalvar.TrackingList.Count()) * 100);

                lblNumberPercent.Text = percentage.ToString(CultureInfo.InvariantCulture) + "%";

                lblReportStatus.Visible = true;
            }

        }

        private void BtnTransferClick(object sender, EventArgs e)
        {
            var computerId = (int)cbComputer.SelectedItem;

            var transfercomputerId = (int)cbTransfer.SelectedItem;

            var context = new whitmanenterprisecraigslistEntities();

            foreach (var tmp in context.vinclappemailaccounts.Where(x=>x.Computer==computerId).ToList())
            {
                tmp.Computer = transfercomputerId;
            }

            context.SaveChanges();

            dgridEmail.Rows.Clear();

            lblEmailStatus.Visible = true;

            lblEmailStatus.Text = "Email from computer " + computerId + " was transfered successfully to " + transfercomputerId ;


        }


        private void TrackingCalendarDateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime dt = trackingCalendar.SelectionRange.Start;

            lblChooseDate.Text = "Report Date = " + dt.ToShortDateString();
        }

        private void DgridScheduleCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgridSchedule.Columns["dataGridViewTextBoxColumnView"].Index && e.RowIndex >= 0)
            {
                dgridDealePerSchedule.Rows.Clear();

                var computerId=Convert.ToInt32(dgridSchedule.Rows[e.RowIndex].Cells[0].Value.ToString());

                var griddealerScheuldeList = Globalvar.ComputerList.Where(x => x.PcAccount == computerId);

                foreach (var tmp in griddealerScheuldeList)
                {

                    var newRow = new DataGridViewRow();

                    newRow.CreateCells(dgridDealePerSchedule);
                    newRow.Cells[0].Value = computerId;
                    newRow.Cells[1].Value = tmp.DealerId;
                    newRow.Cells[2].Value =Globalvar.DealerList.First(x=>x.DealerId==tmp.DealerId).DealershipName;
                    newRow.Cells[3].Value = Globalvar.DealerList.First(x => x.DealerId == tmp.DealerId).NumberOfCars;
                    newRow.Cells[4].Value = Globalvar.CityList.First(x => x.CityID == tmp.CityId).CityName;
                    newRow.Cells[5].Value = Globalvar.DealerList.First(x => x.DealerId == tmp.DealerId).Phone;
                    newRow.Cells[6].Value = "Dealer Pics";
                    dgridDealePerSchedule.Rows.Add(newRow);
                }
            }
            else if (e.ColumnIndex == dgridSchedule.Columns["dataGridViewTextBoxColumnDelete"].Index && e.RowIndex >= 0)
            {
                dgridDealePerSchedule.Rows.Clear();

                var computerId = Convert.ToInt32(dgridSchedule.Rows[e.RowIndex].Cells[0].Value.ToString());



                var resultQuestion = MessageBox.Show("Are you sure to delete schedule " + computerId + " ? ",
                                                     "Important Warning",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);

                if (resultQuestion.ToString().Equals("Yes"))
                {

                    var context = new whitmanenterprisecraigslistEntities();

                    var searchResult = context.whitmanenterprisecomputeraccounts.Where(x => x.AccountPC == computerId);

                    foreach (var tmp in searchResult)
                    {
                        context.Attach(tmp);
                        context.DeleteObject(tmp);
                    }
                    context.SaveChanges();

                    Globalvar.ComputerList=new List<Computer>();

                    foreach (var computertmp in context.whitmanenterprisecomputeraccounts.ToList())
                    {
                        
                        Globalvar.ComputerList.Add(new Computer()
                        {
                            PcAccount = computertmp.AccountPC,
                            CityId = computertmp.CityId,
                            DealerId = computertmp.DealerId
                        }
                        );

                    }

                    mainTab.SelectedIndex = 0;

                    txtAcctPc.Text = computerId.ToString(CultureInfo.InvariantCulture);
                }
            }
            else if (e.ColumnIndex == dgridSchedule.Columns["dataGridViewTextBoxColumnUpdate"].Index && e.RowIndex >= 0)
            {
                dgridDealePerSchedule.Rows.Clear();

                var computerId = Convert.ToInt32(dgridSchedule.Rows[e.RowIndex].Cells[0].Value.ToString());

                mainTab.SelectedIndex = 0;

                txtAcctPc.Text = computerId.ToString(CultureInfo.InvariantCulture);

            }
        }

     
        private void DgridDealePerScheduleCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgridDealePerSchedule.Columns["btnFixPicture"].Index && e.RowIndex >= 0)
            {
                _computerId = Convert.ToInt32(dgridDealePerSchedule.Rows[e.RowIndex].Cells[0].Value.ToString());

                _dealerId = Convert.ToInt32(dgridDealePerSchedule.Rows[e.RowIndex].Cells[1].Value.ToString());

                _cityId =
                    Globalvar.CityList.First(x => x.CityName == dgridDealePerSchedule.Rows[e.RowIndex].Cells[4].Value.ToString())
                        .CityID;

                loadPicBox.Visible = true;

                this.Refresh();

                Globalvar.BufferMasterVehicleList = new List<WhitmanEntepriseMasterVehicleInfo>();

                bgReGenerateImageWorker.RunWorkerAsync();
              
             



            }
        }
        private void BgReGenerateImageWorkerDoWork(object sender, DoWorkEventArgs e)
        {

            var dtCompare = new DateTime(dtPickerForRegenerateImage.Value.Year, dtPickerForRegenerateImage.Value.Month, dtPickerForRegenerateImage.Value.Day);

            var dtCompareNextDay = dtCompare.AddDays(1);

            var context = new whitmanenterprisecraigslistEntities();

            var selectedDealer = context.whitmanenterprisecomputeraccounts.First(x => x.AccountPC == _computerId && x.DealerId == _dealerId && x.CityId == _cityId);

            var imageList = from a in context.vincontrolcraigslisttrackings
                            where
                                a.AddedDate >= dtCompare && a.AddedDate < dtCompareNextDay &&
                                a.Computer == _computerId && a.DealerId == _dealerId && a.CityId == _cityId
                            select new
                            {
                                a.TrackingId,
                                a.Computer,
                                a.ListingId
                            };


            var invertoryPerId = from a in context.whitmanenterprisecraigslisdailytinventories
                                 from b in context.whitmanenterprisedealerlists
                                 where
                                     b.VincontrolId == _dealerId && a.DealershipId == b.VincontrolId &&
                                     !String.IsNullOrEmpty(a.CarImageUrl) && a.NewUsed == "Used"
                                 select new
                                 {
                                     a.ListingID,
                                     a.StockNumber,
                                     a.VINNumber,
                                     a.ModelYear,
                                     a.Make,
                                     a.Model,
                                     a.Trim,
                                     a.Cylinders,
                                     a.BodyType,
                                     a.SalePrice,
                                     a.ExteriorColor,
                                     a.InteriorColor,
                                     a.Mileage,
                                     a.Descriptions,
                                     a.CarImageUrl,
                                     a.Doors,
                                     a.FuelType,
                                     a.Liters,
                                     a.Tranmission,
                                     a.DriveTrain,
                                     a.EngineType,
                                     a.CarsOptions,
                                     a.DefaultImageUrl,
                                     b.VincontrolId,
                                     b.DealershipName,
                                     b.PhoneNumber,
                                     b.StreetAddress,
                                     b.City,
                                     b.State,
                                     b.ZipCode,
                                     b.LogoURL,
                                     b.WebSiteURL,
                                     b.CreditURL,
                                     b.Email,
                                     b.CityOveride,
                                     b.EmailFormat,
                                     b.TradeInBannerLink



                                 };

            foreach (var tmp in imageList.ToList())
            {
                if (invertoryPerId.Any(x => x.ListingID == tmp.ListingId))
                {
                    var vehicle = invertoryPerId.First(x => x.ListingID == tmp.ListingId);

                    var firstImageUrl = "";

                    if (!String.IsNullOrEmpty(vehicle.CarImageUrl))
                    {
                        string tmpString = vehicle.CarImageUrl.Replace(" ", "%20");

                        if (tmpString.Contains("|") || tmpString.Contains(","))
                        {

                            string[] totalImage = tmpString.Split(new String[] { "|", "," },
                                                                  StringSplitOptions.RemoveEmptyEntries);
                            firstImageUrl = totalImage.First();
                        }
                        else
                        {
                            firstImageUrl = tmpString;
                        }
                    }
                    else
                    {
                        firstImageUrl = vehicle.DefaultImageUrl;
                    }


                    var addCar = new WhitmanEntepriseMasterVehicleInfo()
                    {
                        AutoID =
                            Globalvar.BufferMasterVehicleList.
                                Count +
                            1,
                        TrackingId = tmp.TrackingId,
                        ListingId = vehicle.ListingID,
                        StockNumber =
                            String.IsNullOrEmpty(vehicle.StockNumber)
                                ? ""
                                : vehicle.StockNumber,
                        Vin =
                            String.IsNullOrEmpty(vehicle.VINNumber)
                                ? ""
                                : vehicle.VINNumber,
                        ModelYear =
                            String.IsNullOrEmpty(vehicle.ModelYear)
                                ? ""
                                : vehicle.ModelYear,
                        Make =
                            String.IsNullOrEmpty(vehicle.Make)
                                ? ""
                                : vehicle.Make,
                        Model =
                            String.IsNullOrEmpty(vehicle.Model)
                                ? ""
                                : vehicle.Model,
                        Trim =
                            String.IsNullOrEmpty(vehicle.Trim)
                                ? ""
                                : vehicle.Trim,
                        Cylinder =
                            String.IsNullOrEmpty(vehicle.Cylinders)
                                ? ""
                                : vehicle.Cylinders,
                        BodyType =
                            String.IsNullOrEmpty(vehicle.BodyType)
                                ? ""
                                : vehicle.BodyType,
                        SalePrice =
                            String.IsNullOrEmpty(vehicle.SalePrice)
                                ? ""
                                : vehicle.SalePrice,
                        ExteriorColor =
                            String.IsNullOrEmpty(vehicle.ExteriorColor)
                                ? ""
                                : vehicle.ExteriorColor,
                        InteriorColor =
                            String.IsNullOrEmpty(vehicle.InteriorColor)
                                ? ""
                                : vehicle.InteriorColor,
                        Mileage =
                            String.IsNullOrEmpty(vehicle.Mileage)
                                ? ""
                                : vehicle.Mileage,
                        Description =
                            String.IsNullOrEmpty(vehicle.Descriptions)
                                ? ""
                                : vehicle.Descriptions,
                        CarImageUrl =
                            String.IsNullOrEmpty(vehicle.CarImageUrl)
                                ? ""
                                : vehicle.CarImageUrl.Replace("|", ","),
                        Door =
                            String.IsNullOrEmpty(vehicle.Doors)
                                ? ""
                                : vehicle.Doors,
                        Fuel =
                            String.IsNullOrEmpty(vehicle.FuelType)
                                ? ""
                                : vehicle.FuelType,
                        Litters =
                            String.IsNullOrEmpty(vehicle.Liters)
                                ? ""
                                : vehicle.Liters,
                        Tranmission =
                            String.IsNullOrEmpty(vehicle.Tranmission)
                                ? ""
                                : vehicle.Tranmission,
                        WheelDrive =
                            String.IsNullOrEmpty(vehicle.DriveTrain)
                                ? ""
                                : vehicle.DriveTrain,
                        Engine =
                            String.IsNullOrEmpty(vehicle.EngineType)
                                ? ""
                                : vehicle.EngineType,
                        Options =
                            String.IsNullOrEmpty(vehicle.CarsOptions)
                                ? ""
                                : vehicle.CarsOptions,
                        DefaultImageURL =
                            String.IsNullOrEmpty(
                                vehicle.DefaultImageUrl)
                                ? ""
                                : vehicle.DefaultImageUrl,
                        VincontrolId =
                            String.IsNullOrEmpty(
                                vehicle.VincontrolId.ToString())
                                ? ""
                                : vehicle.VincontrolId.ToString(),
                        DealershipName =
                            String.IsNullOrEmpty(
                                vehicle.DealershipName)
                                ? ""
                                : vehicle.DealershipName,
                        PhoneNumber =
                            String.IsNullOrEmpty(vehicle.PhoneNumber)
                                ? ""
                                : vehicle.PhoneNumber,
                        StreetAddress =
                            String.IsNullOrEmpty(vehicle.StreetAddress)
                                ? ""
                                : vehicle.StreetAddress,
                        City =
                            String.IsNullOrEmpty(vehicle.City)
                                ? ""
                                : vehicle.City,
                        State =
                            String.IsNullOrEmpty(vehicle.State)
                                ? ""
                                : vehicle.State,
                        ZipCode =
                            String.IsNullOrEmpty(vehicle.ZipCode)
                                ? ""
                                : vehicle.ZipCode,
                        LogoURL =
                            String.IsNullOrEmpty(vehicle.LogoURL)
                                ? ""
                                : vehicle.LogoURL,
                        WebSiteURL =
                            String.IsNullOrEmpty(vehicle.WebSiteURL)
                                ? ""
                                : vehicle.WebSiteURL,
                        CreditURL =
                            String.IsNullOrEmpty(vehicle.CreditURL)
                                ? ""
                                : vehicle.CreditURL,
                        Email =
                            String.IsNullOrEmpty(vehicle.Email)
                                ? ""
                                : vehicle.Email,
                        CityOveride =
                            String.IsNullOrEmpty(vehicle.CityOveride)
                                ? ""
                                : vehicle.CityOveride,
                        Price = selectedDealer.Price.GetValueOrDefault(),
                        PostingCityId = selectedDealer.CityId,
                        EmailFormat =
                            vehicle.EmailFormat.GetValueOrDefault(),

                        DealerId =
                            vehicle.VincontrolId.GetValueOrDefault(),
                        FirstImageUrl = firstImageUrl,

                        TradeInBannerLink = vehicle.TradeInBannerLink,
                        CraigslistExist = false



                    };




                    Globalvar.BufferMasterVehicleList.Add(addCar);

                }
            }
        }

        private void BgReGenerateImageWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(!e.Cancelled)
            {
                loadPicBox.Visible = false;

                dgridTestingReportImage.DataSource = Globalvar.BufferMasterVehicleList;

                lblLeftPost.Visible = true;

                lblLeftPost.Text = "Please wait.............";

                this.Refresh();

                bgProcessChangeImage.WorkerReportsProgress = true;

                bgProcessChangeImage.RunWorkerAsync();
               
            }
        }
        private void ScheudeFormFormClosing(object sender, FormClosingEventArgs e)
        {
            if (_browser != null)
            {

                _browser.ClearCache();

                _browser.ClearCookies();

                _browser.Close();

                _browser = null;
            }
        }
       
       

        private void BgProcessChangeImageProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage > 100)
            {
                progressChangeImage.Value = 100;

            }
            else
            {
                progressChangeImage.Value = e.ProgressPercentage;
            }
            
        }

        private void btnFixImageByDate_Click(object sender, EventArgs e)
        {
            var dtCompare = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-8);

            var context = new whitmanenterprisecraigslistEntities()
                {
                    CommandTimeout = 18000
                };

            Globalvar.BufferMasterVehicleList=new List<WhitmanEntepriseMasterVehicleInfo>();

            var imageList = from a in context.vincontrolcraigslisttrackings
                            where  a.AddedDate > dtCompare
                            select new
                            {
                                a.TrackingId,
                                a.Computer,
                                a.ListingId,
                                a.CityId,
                                a.AddedDate
                            };


            var invertoryPerId = from a in context.whitmanenterprisecraigslistinventories
                                 from b in context.whitmanenterprisedealerlists
                                 where
                                    !String.IsNullOrEmpty(a.CarImageUrl)  && a.DealershipId==b.VincontrolId 
                                 select new
                                 {
                                     a.ListingID,
                                     a.StockNumber,
                                     a.VINNumber,
                                     a.ModelYear,
                                     a.Make,
                                     a.Model,
                                     a.Trim,
                                     a.Cylinders,
                                     a.BodyType,
                                     a.SalePrice,
                                     a.ExteriorColor,
                                     a.InteriorColor,
                                     a.Mileage,
                                     a.Descriptions,
                                     a.CarImageUrl,
                                     a.Doors,
                                     a.FuelType,
                                     a.Liters,
                                     a.Tranmission,
                                     a.DriveTrain,
                                     a.EngineType,
                                     a.CarsOptions,
                                     a.DefaultImageUrl,
                                     b.VincontrolId,
                                     b.DealershipName,
                                     b.PhoneNumber,
                                     b.StreetAddress,
                                     b.City,
                                     b.State,
                                     b.ZipCode,
                                     b.LogoURL,
                                     b.WebSiteURL,
                                     b.CreditURL,
                                     b.Email,
                                     b.CityOveride,
                                     b.EmailFormat,
                                     b.TradeInBannerLink



                                 };

            var tolistInventory = invertoryPerId.ToList();

            foreach (var tmp in imageList.OrderByDescending(x=>x.AddedDate).ToList())
            {
                if (tolistInventory.Any(x => x.ListingID == tmp.ListingId))
                {


                    var vehicle = tolistInventory.First(x => x.ListingID == tmp.ListingId);

                    var firstImageUrl = "";

                    if (!String.IsNullOrEmpty(vehicle.CarImageUrl))
                    {
                        string tmpString = vehicle.CarImageUrl.Replace(" ", "%20");

                        if (tmpString.Contains("|") || tmpString.Contains(","))
                        {

                            string[] totalImage = tmpString.Split(new String[] { "|", "," },
                                                                  StringSplitOptions.RemoveEmptyEntries);
                            firstImageUrl = totalImage.First();
                        }
                        else
                        {
                            firstImageUrl = tmpString;
                        }
                    }
                    else
                    {
                        firstImageUrl = vehicle.DefaultImageUrl;
                    }


                    var addCar = new WhitmanEntepriseMasterVehicleInfo()
                    {
                        AutoID =
                            Globalvar.BufferMasterVehicleList.
                                Count +
                            1,
                        TrackingId = tmp.TrackingId,
                        ListingId = vehicle.ListingID,
                        StockNumber =
                            String.IsNullOrEmpty(vehicle.StockNumber)
                                ? ""
                                : vehicle.StockNumber,
                        Vin =
                            String.IsNullOrEmpty(vehicle.VINNumber)
                                ? ""
                                : vehicle.VINNumber,
                        ModelYear =
                            String.IsNullOrEmpty(vehicle.ModelYear)
                                ? ""
                                : vehicle.ModelYear,
                        Make =
                            String.IsNullOrEmpty(vehicle.Make)
                                ? ""
                                : vehicle.Make,
                        Model =
                            String.IsNullOrEmpty(vehicle.Model)
                                ? ""
                                : vehicle.Model,
                        Trim =
                            String.IsNullOrEmpty(vehicle.Trim)
                                ? ""
                                : vehicle.Trim,
                        Cylinder =
                            String.IsNullOrEmpty(vehicle.Cylinders)
                                ? ""
                                : vehicle.Cylinders,
                        BodyType =
                            String.IsNullOrEmpty(vehicle.BodyType)
                                ? ""
                                : vehicle.BodyType,
                        SalePrice =
                            String.IsNullOrEmpty(vehicle.SalePrice)
                                ? ""
                                : vehicle.SalePrice,
                        ExteriorColor =
                            String.IsNullOrEmpty(vehicle.ExteriorColor)
                                ? ""
                                : vehicle.ExteriorColor,
                        InteriorColor =
                            String.IsNullOrEmpty(vehicle.InteriorColor)
                                ? ""
                                : vehicle.InteriorColor,
                        Mileage =
                            String.IsNullOrEmpty(vehicle.Mileage)
                                ? ""
                                : vehicle.Mileage,
                        Description =
                            String.IsNullOrEmpty(vehicle.Descriptions)
                                ? ""
                                : vehicle.Descriptions,
                        CarImageUrl =
                            String.IsNullOrEmpty(vehicle.CarImageUrl)
                                ? ""
                                : vehicle.CarImageUrl.Replace("|", ","),
                        Door =
                            String.IsNullOrEmpty(vehicle.Doors)
                                ? ""
                                : vehicle.Doors,
                        Fuel =
                            String.IsNullOrEmpty(vehicle.FuelType)
                                ? ""
                                : vehicle.FuelType,
                        Litters =
                            String.IsNullOrEmpty(vehicle.Liters)
                                ? ""
                                : vehicle.Liters,
                        Tranmission =
                            String.IsNullOrEmpty(vehicle.Tranmission)
                                ? ""
                                : vehicle.Tranmission,
                        WheelDrive =
                            String.IsNullOrEmpty(vehicle.DriveTrain)
                                ? ""
                                : vehicle.DriveTrain,
                        Engine =
                            String.IsNullOrEmpty(vehicle.EngineType)
                                ? ""
                                : vehicle.EngineType,
                        Options =
                            String.IsNullOrEmpty(vehicle.CarsOptions)
                                ? ""
                                : vehicle.CarsOptions,
                        DefaultImageURL =
                            String.IsNullOrEmpty(
                                vehicle.DefaultImageUrl)
                                ? ""
                                : vehicle.DefaultImageUrl,
                        VincontrolId =
                            String.IsNullOrEmpty(
                                vehicle.VincontrolId.ToString())
                                ? ""
                                : vehicle.VincontrolId.ToString(),
                        DealershipName =
                            String.IsNullOrEmpty(
                                vehicle.DealershipName)
                                ? ""
                                : vehicle.DealershipName,
                        PhoneNumber =
                            String.IsNullOrEmpty(vehicle.PhoneNumber)
                                ? ""
                                : vehicle.PhoneNumber,
                        StreetAddress =
                            String.IsNullOrEmpty(vehicle.StreetAddress)
                                ? ""
                                : vehicle.StreetAddress,
                        City =
                            String.IsNullOrEmpty(vehicle.City)
                                ? ""
                                : vehicle.City,
                        State =
                            String.IsNullOrEmpty(vehicle.State)
                                ? ""
                                : vehicle.State,
                        ZipCode =
                            String.IsNullOrEmpty(vehicle.ZipCode)
                                ? ""
                                : vehicle.ZipCode,
                        LogoURL =
                            String.IsNullOrEmpty(vehicle.LogoURL)
                                ? ""
                                : vehicle.LogoURL,
                        WebSiteURL =
                            String.IsNullOrEmpty(vehicle.WebSiteURL)
                                ? ""
                                : vehicle.WebSiteURL,
                        CreditURL =
                            String.IsNullOrEmpty(vehicle.CreditURL)
                                ? ""
                                : vehicle.CreditURL,
                        Email =
                            String.IsNullOrEmpty(vehicle.Email)
                                ? ""
                                : vehicle.Email,
                        CityOveride =
                            String.IsNullOrEmpty(vehicle.CityOveride)
                                ? ""
                                : vehicle.CityOveride,
                        Price = false,
                        PostingCityId = tmp.CityId.GetValueOrDefault(),
                        EmailFormat =
                            vehicle.EmailFormat.GetValueOrDefault(),

                        DealerId =
                            vehicle.VincontrolId.GetValueOrDefault(),
                        FirstImageUrl = firstImageUrl,

                        TradeInBannerLink = vehicle.TradeInBannerLink,
                        CraigslistExist = false



                    };




                    Globalvar.BufferMasterVehicleList.Add(addCar);

                }

             
            }



            const int numberofitem = 10;

            bool end = false;

            int skipCountOfItem = 0;

            int index = Globalvar.BufferMasterVehicleList.Count;

            lblLeftPost.Text = index.ToString(CultureInfo.InvariantCulture);
            
            while (!end)
            {
                end = true;

                var result =
                        Globalvar.BufferMasterVehicleList.OrderBy(x => x.AutoID).Skip(skipCountOfItem).Take(numberofitem).ToList();

                foreach (var addCar in result)
                {

                    try
                    {
                        var imageModel = ImageHelper.GenerateRunTimeImageBlobByComputerAccount(addCar);

                        var searchResult =
                            context.vincontrolcraigslisttrackings.First(x => x.TrackingId == addCar.TrackingId);

                        if (searchResult != null)
                        {

                            if (imageModel.BottomImage != null && imageModel.TopImage != null)
                            {

                                index--;

                                lblLeftPost.Text = index.ToString(CultureInfo.InvariantCulture);

                                Refresh();

                            }
                        }
                    }
                    catch (Exception)
                    {


                    }


                    end = false;

                }

                context.SaveChanges();

                skipCountOfItem = skipCountOfItem + numberofitem;

            }

        }


        private void BgProcessChangeImageDoWork(object sender, DoWorkEventArgs e)
        {
            var context = new whitmanenterprisecraigslistEntities();



            foreach (var addCar in Globalvar.BufferMasterVehicleList)
            {
                var imageModel = ImageHelper.GenerateRunTimeImageBlobByComputerAccount(addCar);

                var searchResult =
                    context.vincontrolcraigslisttrackings.FirstOrDefault(x => x.TrackingId == addCar.TrackingId);

                if (imageModel.BottomImage != null && imageModel.TopImage != null)
                {

                 
                }

                _progressPercentage = (_runAutoId * 100) / Globalvar.BufferMasterVehicleList.Count;

                bgProcessChangeImage.ReportProgress(_runAutoId);

            }


        }


        private void BgProcessChangeImageRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                lblLeftPost.Text = "FINISHED!!!!!!!!!!!!";

                //dgridTestingReportImage.DefaultCellStyle.BackColor = Color.DarkRed;
            }

        }

        private void cbReportComputer_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedComputer = (String)cbReportComputer.SelectedItem;

            if (selectedComputer.Equals("Select All"))
                _reportComputerId = 0;
            else
            {
                _reportComputerId = Convert.ToInt32(selectedComputer);
            }
        }

        private void btnInventoryReportPrint_Click(object sender, EventArgs e)
        {
            var csvWriterDealer = new CsvExport<GridScheuleModel>(Globalvar.GridScheduleList.OrderBy(x => x.Computer).ToList());



            string pathDownload = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            string fullFilePathForDealer = pathDownload + "/inventoryreport" + "_" + DateTime.Now.ToString("MMddyy") +
                                           DateTime.Now.Millisecond +
                                           ".csv";

            csvWriterDealer.ExportToFileWithHeader(fullFilePathForDealer);


            var xlApp = new Excel.ApplicationClass();

            object missing = System.Reflection.Missing.Value;

            xlApp.Workbooks.OpenText
                (
                    fullFilePathForDealer,
                    Excel.XlPlatform.xlWindows, //Origin
                    1, // Start Row
                    Excel.XlTextParsingType.xlDelimited, //Datatype
                    Excel.XlTextQualifier.xlTextQualifierNone, //TextQualifier
                    false, // Consecutive Deliminators
                    false, // tab
                    false, // semicolon
                    true, //COMMA
                    false, // space
                    false, // other
                    missing, // Other Char
                    missing, // FieldInfo
                    missing, //TextVisualLayout
                    missing, // DecimalSeparator
                    missing, // ThousandsSeparator
                    missing, // TrialingMionusNumbers
                    missing //Local
                );


            xlApp.Visible = true;

           
        }

        private void btnRunReport_Click(object sender, EventArgs e)
        {
            var context = new whitmanenterprisecraigslistEntities()
                {
                    CommandTimeout = 25000
                };

            DateTime dt = trackingCalendar.SelectionRange.Start;

            var dtCompare = new DateTime(dt.Year, dt.Month, dt.Day);

            var dtCompareNextDay = dtCompare.AddDays(1);



            var checkList =
                context.vincontrolcraigslisttrackings.Where(
                    a =>
                    a.CLPostingId > 0 && a.AddedDate >= dtCompare && a.AddedDate < dtCompareNextDay).Select(a => new
                        {
                            a.TrackingId,
                            a.CityId,
                            a.CLPostingId,
                            a.Computer,
                            a.ShowAd,
                            a.CheckDate,
                            a.AddedDate
                        });

            if (dt.Date < DateTime.Now.Date)
            {
                if (_reportComputerId == 0)
                {


                    foreach (var tmp in checkList.ToList())
                    {

                        try
                        {
                            if (_browser == null)
                            {


                                _browser =
                                    new IE(Globalvar.CityList.First(x => x.CityID == tmp.CityId).CraigsListCityURL +
                                           "search/ctd?zoomToPosting=&query=" + tmp.CLPostingId +
                                           "&srchType=A&minAsk=&maxAsk=");


                                _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);
                            }
                            else
                            {
                                _browser.GoToNoWait(
                                    Globalvar.CityList.First(x => x.CityID == tmp.CityId).CraigsListCityURL +
                                    "search/ctd?zoomToPosting=&query=" + tmp.CLPostingId + "&srchType=A&minAsk=&maxAsk=");

                            }

                            //_browser.TextField(Find.ById("query")).Value =
                            //    tmp.CLPostingId.ToString();

                            //_browser.RadioButton(Find.ByTitle("search the entire posting")).Checked = true;

                            //_browser.Button(Find.ByValue("Search")).Click();



                            System.Threading.Thread.Sleep(1000);

                            var searchResult =
                                context.vincontrolcraigslisttrackings.First(x => x.TrackingId == tmp.TrackingId);

                            if (searchResult != null)
                            {

                                if (_browser.Html.Contains("Found: "))
                                {

                                    searchResult.ShowAd = true;

                                    searchResult.CheckDate = DateTime.Now;
                                }
                                else
                                {
                                    searchResult.ShowAd = false;

                                    searchResult.CheckDate = DateTime.Now;
                                }

                                context.SaveChanges();
                            }

                            System.Threading.Thread.Sleep(200);
                        }
                        catch (Exception)
                        {

                        }



                    }

                    var fullList = context.vincontrolcraigslisttrackings.Where(
                        a =>
                        a.CLPostingId > 0 && a.AddedDate >= dtCompare && a.AddedDate < dtCompareNextDay).Select(a => new
                            {
                                a.TrackingId,

                                a.ShowAd,

                            });

                    lblTotalAdsForDate.Text = fullList.Count().ToString(CultureInfo.InvariantCulture);

                    lblNumberAds.Text = fullList.Count(x => x.ShowAd == true).ToString(CultureInfo.InvariantCulture);

                    var percentage =
                        Math.Ceiling(((double)fullList.Count(x => x.ShowAd == true) /
                                      fullList.Count()) * 100);

                    lblNumberPercent.Text = percentage.ToString(CultureInfo.InvariantCulture) + "%";


                    lblReportStatus.Visible = true;
                }
                else
                {


                    foreach (var tmp in checkList.Where(x => x.Computer == _reportComputerId).ToList())
                    {

                        try
                        {
                            if (_browser == null)
                            {


                                _browser =
                                    new IE(Globalvar.CityList.First(x => x.CityID == tmp.CityId).CraigsListCityURL +
                                           "search/ctd?zoomToPosting=&query=" + tmp.CLPostingId +
                                           "&srchType=A&minAsk=&maxAsk=");


                                _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);
                            }
                            else
                            {
                                _browser.GoToNoWait(
                                    Globalvar.CityList.First(x => x.CityID == tmp.CityId).CraigsListCityURL +
                                    "search/ctd?zoomToPosting=&query=" + tmp.CLPostingId + "&srchType=A&minAsk=&maxAsk=");

                            }

                            //_browser.TextField(Find.ById("query")).Value =
                            //    tmp.CLPostingId.ToString();

                            //_browser.RadioButton(Find.ByTitle("search the entire posting")).Checked = true;

                            //_browser.Button(Find.ByValue("Search")).Click();

                            System.Threading.Thread.Sleep(1000);

                            var searchResult =
                                 context.vincontrolcraigslisttrackings.First(x => x.TrackingId == tmp.TrackingId);

                            if (searchResult != null)
                            {

                                if (_browser.Html.Contains("Found: "))
                                {

                                    searchResult.ShowAd = true;

                                    searchResult.CheckDate = DateTime.Now;
                                }
                                else
                                {
                                    searchResult.ShowAd = false;

                                    searchResult.CheckDate = DateTime.Now;
                                }

                                context.SaveChanges();
                            }

                            System.Threading.Thread.Sleep(200);
                        }
                        catch (Exception)
                        {

                        }



                    }

                    var fullList = from a in context.vincontrolcraigslisttrackings
                                   where a.CLPostingId > 0 && a.AddedDate >= dtCompare && a.AddedDate < dtCompareNextDay && a.Computer == _reportComputerId
                                   select new
                                   {
                                       a.TrackingId,
                                       a.ShowAd
                                   };

                    lblTotalAdsForDate.Text = fullList.Count().ToString(CultureInfo.InvariantCulture);

                    lblNumberAds.Text = fullList.Count(x => x.ShowAd == true).ToString(CultureInfo.InvariantCulture);

                    var percentage =
                        Math.Ceiling(((double)fullList.Count(x => x.ShowAd == true) /
                                      fullList.Count()) * 100);

                    lblNumberPercent.Text = percentage.ToString(CultureInfo.InvariantCulture) + "%";


                    lblReportStatus.Visible = true;
                }

                if (_browser != null)

                    _browser.Close();


                MessageBox.Show("Report has been done.", "Message", MessageBoxButtons.OK,
                                              MessageBoxIcon.Warning);
            }
            else
            {

                var compareDate = DateTime.Now.AddMinutes(-20);

                if (_reportComputerId == 0)
                {


                    foreach (var tmp in checkList.Where(x => x.AddedDate <= compareDate).ToList())
                    {

                        try
                        {
                            if (_browser == null)
                            {


                                _browser =
                                    new IE(Globalvar.CityList.First(x => x.CityID == tmp.CityId).CraigsListCityURL +
                                           "search/ctd?zoomToPosting=&query=" + tmp.CLPostingId +
                                           "&srchType=A&minAsk=&maxAsk=");


                                _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);
                            }
                            else
                            {
                                _browser.GoToNoWait(
                                    Globalvar.CityList.First(x => x.CityID == tmp.CityId).CraigsListCityURL +
                                    "search/ctd?zoomToPosting=&query=" + tmp.CLPostingId + "&srchType=A&minAsk=&maxAsk=");

                            }

                            //_browser.TextField(Find.ById("query")).Value =
                            //    tmp.CLPostingId.ToString();

                            //_browser.RadioButton(Find.ByTitle("search the entire posting")).Checked = true;

                            //_browser.Button(Find.ByValue("Search")).Click();


                            System.Threading.Thread.Sleep(1000);

                            var searchResult =
                                context.vincontrolcraigslisttrackings.First(x => x.TrackingId == tmp.TrackingId);

                            if (searchResult != null)
                            {

                                if (_browser.Html.Contains("Found: "))
                                {

                                    searchResult.ShowAd = true;

                                    searchResult.CheckDate = DateTime.Now;
                                }
                                else
                                {
                                    searchResult.ShowAd = false;

                                    searchResult.CheckDate = DateTime.Now;
                                }

                                context.SaveChanges();
                            }

                            System.Threading.Thread.Sleep(200);
                        }
                        catch (Exception ex)
                        {

                        }

                    }
                    var fullList = from a in context.vincontrolcraigslisttrackings
                                   where a.CLPostingId > 0 && a.AddedDate >= dtCompare && a.AddedDate < dtCompareNextDay
                                   select new
                                       {
                                           a.TrackingId,
                                           a.ShowAd
                                       };

                    lblTotalAdsForDate.Text = fullList.Count().ToString(CultureInfo.InvariantCulture);

                    lblNumberAds.Text = fullList.Count(x => x.ShowAd == true).ToString(CultureInfo.InvariantCulture);

                    var percentage =
                        Math.Ceiling(((double)fullList.Count(x => x.ShowAd == true) /
                                      fullList.Count()) * 100);

                    lblNumberPercent.Text = percentage.ToString(CultureInfo.InvariantCulture) + "%";


                    lblReportStatus.Visible = true;
                }
                else
                {


                    foreach (var tmp in checkList.Where(x => x.Computer == _reportComputerId && x.AddedDate <= compareDate).ToList())
                    {

                        try
                        {
                            if (_browser == null)
                            {


                                _browser =
                                    new IE(Globalvar.CityList.First(x => x.CityID == tmp.CityId).CraigsListCityURL +
                                           "search/ctd?zoomToPosting=&query=" + tmp.CLPostingId +
                                           "&srchType=A&minAsk=&maxAsk=");


                                _browser.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);
                            }
                            else
                            {
                                _browser.GoToNoWait(
                                    Globalvar.CityList.First(x => x.CityID == tmp.CityId).CraigsListCityURL +
                                    "search/ctd?zoomToPosting=&query=" + tmp.CLPostingId + "&srchType=A&minAsk=&maxAsk=");

                            }

                            //_browser.TextField(Find.ById("query")).Value =
                            //    tmp.CLPostingId.ToString();

                            //_browser.RadioButton(Find.ByTitle("search the entire posting")).Checked = true;

                            //_browser.Button(Find.ByValue("Search")).Click();



                            System.Threading.Thread.Sleep(1000);

                            var searchResult =
                                 context.vincontrolcraigslisttrackings.First(x => x.TrackingId == tmp.TrackingId);

                            if (searchResult != null)
                            {

                                if (_browser.Html.Contains("Found: "))
                                {

                                    searchResult.ShowAd = true;

                                    searchResult.CheckDate = DateTime.Now;
                                }
                                else
                                {
                                    searchResult.ShowAd = false;

                                    searchResult.CheckDate = DateTime.Now;
                                }

                                context.SaveChanges();
                            }

                            System.Threading.Thread.Sleep(200);
                        }
                        catch (Exception)
                        {

                        }

                    }
                    var fullList = from a in context.vincontrolcraigslisttrackings
                                   where a.CLPostingId > 0 && a.AddedDate >= dtCompare && a.AddedDate < dtCompareNextDay && a.Computer == _reportComputerId
                                   select new
                                   {
                                       a.TrackingId,
                                       a.ShowAd
                                   };

                    lblTotalAdsForDate.Text = fullList.Count().ToString(CultureInfo.InvariantCulture);

                    lblNumberAds.Text = fullList.Count(x => x.ShowAd == true).ToString(CultureInfo.InvariantCulture);

                    var percentage =
                        Math.Ceiling(((double)fullList.Count(x => x.ShowAd == true) /
                                      fullList.Count()) * 100);

                    lblNumberPercent.Text = percentage.ToString(CultureInfo.InvariantCulture) + "%";


                    lblReportStatus.Visible = true;
                }

                if (_browser != null)

                    _browser.Close();

                MessageBox.Show("Report has been done.", "Message", MessageBoxButtons.OK,
                                              MessageBoxIcon.Warning);

            }
        }

        private void btnDailySchedule_Click(object sender, EventArgs e)
        {
            picLoadSchedule.Visible = true;

            lblscheduleSuccess.Visible = false;

            this.Refresh();

            int numberOfSchedule = 0;Int32.TryParse(nuNumberSchedules.Value.ToString(), out numberOfSchedule);

            if (numberOfSchedule <= 0)
            {
                errorProvider1.SetError(nuNumberSchedules,"Please enter valid number of schedules to split");
                return;
            }

            var context = new whitmanenterprisecraigslistEntities();

            foreach (var tmp in context.vinclappdealerschedules.ToList())
            {
                context.Attach(tmp);
                context.DeleteObject(tmp);
            }

            foreach (DataGridViewRow tmp in dGridDailyView.Rows)
            {
                var chkPrice = tmp.Cells["Price"] as DataGridViewCheckBoxCell;

                var chkOc = tmp.Cells["OC"] as DataGridViewCheckBoxCell;

                var chkLB = tmp.Cells["LB"] as DataGridViewCheckBoxCell;

                var chkIe = tmp.Cells["IE"] as DataGridViewCheckBoxCell;

                var chkNsd = tmp.Cells["NSD"] as DataGridViewCheckBoxCell;

                var chkCla = tmp.Cells["CLA"] as DataGridViewCheckBoxCell;

                var chkSbv = tmp.Cells["SBV"] as DataGridViewCheckBoxCell;

                var chkPS = tmp.Cells["PS"] as DataGridViewCheckBoxCell;

                var chkHalfSplit = tmp.Cells["HalfSplit"] as DataGridViewCheckBoxCell;

                bool priceChecked = (null != chkPrice && null != chkPrice.Value && Convert.ToInt32(chkPrice.Value)==1);
                bool ocChecked = (null != chkOc && null != chkOc.Value && Convert.ToInt32(chkOc.Value) == 1);
                bool lbChecked = (null != chkLB && null != chkLB.Value && Convert.ToInt32(chkLB.Value) == 1);
                bool ieChecked = (null != chkIe && null != chkIe.Value && Convert.ToInt32(chkIe.Value) == 1);
                bool nsdChecked = (null != chkNsd && null != chkNsd.Value && Convert.ToInt32(chkNsd.Value) == 1);
                bool claChecked = (null != chkCla && null != chkCla.Value && Convert.ToInt32(chkCla.Value) == 1);
                bool sbvChecked= (null != chkSbv && null != chkSbv.Value && Convert.ToInt32(chkSbv.Value) == 1);
                bool psChecked = (null != chkPS && null != chkPS.Value && Convert.ToInt32(chkPS.Value) == 1);
                bool halfChecked = (null != chkHalfSplit && null != chkHalfSplit.Value && Convert.ToInt32(chkHalfSplit.Value) == 1);

                if (ocChecked)
                {
                    var dealerSchedule = new vinclappdealerschedule()
                        {

                            DealerId = Convert.ToInt32(tmp.Cells["DealerId"].Value),
                            Price = priceChecked,
                            CityId = 1,
                            Split = halfChecked,
                            LastUpdated = DateTime.Now,
                            Schedules = numberOfSchedule
                            


                        };


                    context.AddTovinclappdealerschedules(dealerSchedule);
                }

                if (nsdChecked)
                {
                    var dealerSchedule = new vinclappdealerschedule()
                    {

                        DealerId = Convert.ToInt32(tmp.Cells["DealerId"].Value),
                        Price = priceChecked,
                        CityId = 3,
                        Split = halfChecked,
                        LastUpdated = DateTime.Now,
                        Schedules = numberOfSchedule



                    };


                    context.AddTovinclappdealerschedules(dealerSchedule);
                }

                if (lbChecked)
                {
                    var dealerSchedule = new vinclappdealerschedule()
                    {

                        DealerId = Convert.ToInt32(tmp.Cells["DealerId"].Value),
                        Price = priceChecked,
                        CityId = 10,
                        Split = halfChecked,
                        LastUpdated = DateTime.Now,
                        Schedules = numberOfSchedule



                    };


                    context.AddTovinclappdealerschedules(dealerSchedule);
                }

                if (ieChecked)
                {
                    var dealerSchedule = new vinclappdealerschedule()
                    {

                        DealerId = Convert.ToInt32(tmp.Cells["DealerId"].Value),
                        Price = priceChecked,
                        CityId = 12,
                        Split = halfChecked,
                        LastUpdated = DateTime.Now,
                        Schedules = numberOfSchedule



                    };


                    context.AddTovinclappdealerschedules(dealerSchedule);
                }

                if (claChecked)
                {
                    var dealerSchedule = new vinclappdealerschedule()
                    {

                        DealerId = Convert.ToInt32(tmp.Cells["DealerId"].Value),
                        Price = priceChecked,
                        CityId = 8,
                        Split = halfChecked,
                        LastUpdated = DateTime.Now,
                        Schedules = numberOfSchedule



                    };


                    context.AddTovinclappdealerschedules(dealerSchedule);
                }

                if (sbvChecked)
                {
                    var dealerSchedule = new vinclappdealerschedule()
                    {

                        DealerId = Convert.ToInt32(tmp.Cells["DealerId"].Value),
                        Price = priceChecked,
                        CityId = 9,
                        Split = halfChecked,
                        LastUpdated = DateTime.Now,
                        Schedules = numberOfSchedule



                    };


                    context.AddTovinclappdealerschedules(dealerSchedule);
                }

                if (psChecked)
                {
                    var dealerSchedule = new vinclappdealerschedule()
                    {

                        DealerId = Convert.ToInt32(tmp.Cells["DealerId"].Value),
                        Price = priceChecked,
                        CityId = 13,
                        Split = halfChecked,
                        LastUpdated = DateTime.Now,
                        Schedules = numberOfSchedule



                    };


                    context.AddTovinclappdealerschedules(dealerSchedule);
                }

            }

            context.SaveChanges();

            var dealerScheduleList = context.vinclappdealerschedules.ToList();

            foreach (var tmp in Globalvar.DealerList)
            {
                tmp.ScheduleCityList=new List<ScheduleCity>();

                foreach (var scheduleTmp in dealerScheduleList.Where(x => x.DealerId == tmp.DealerId))
                {
                    tmp.ScheduleCityList.Add(new ScheduleCity()
                    {
                        City = scheduleTmp.CityId.GetValueOrDefault(),
                        Price = scheduleTmp.Price.GetValueOrDefault(),
                        Split = scheduleTmp.Split.GetValueOrDefault(),
                        Schedules = scheduleTmp.Schedules.GetValueOrDefault(),
                    });
                }
            }

            picLoadSchedule.Visible = false;

            if(dealerScheduleList.Any())

                Globalvar.SplitSchedules = dealerScheduleList.First().Schedules.GetValueOrDefault();

            else
            {
                Globalvar.SplitSchedules = 1;
            }

            lblscheduleSuccess.Visible = true;
          

        }

        private void btnPCSchedule_Click(object sender, EventArgs e)
        {
              var context = new whitmanenterprisecraigslistEntities();
            if(context.vinclappdealerschedules.Any())
            {

                var filterForm = PCSchedule.Instance(this);

                filterForm.Show();
            }
            else
            {

                MessageBox.Show("You have to create a schedule for dealerships first.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnNewClearSchedule_Click(object sender, EventArgs e)
        {
             DialogResult result= MessageBox.Show("Are you sure to delete schedule?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
            {

                var context = new whitmanenterprisecraigslistEntities();

                foreach (var tmp in context.vinclappdealerschedules.ToList())
                {
                    context.Attach(tmp);
                    context.DeleteObject(tmp);
                }

                foreach (var tmp in context.vinclapppcschedules.ToList())
                {
                    context.Attach(tmp);
                    context.DeleteObject(tmp);
                }

                context.SaveChanges();

                foreach (var tmp in Globalvar.DealerList)
                {
                    tmp.ScheduleCityList=new List<ScheduleCity>();
                }

                dGridDailyView.Rows.Clear();
               
                foreach (var tmp in Globalvar.DealerList.OrderBy(x => x.DealerId))
                {

                    var newRow = new DataGridViewRow();

                    newRow.CreateCells(dGridDailyView);
                    newRow.Cells[0].Value = tmp.DealerId;
                    newRow.Cells[1].Value = tmp.DealershipName;
                    newRow.Cells[2].Value = tmp.NumberOfCars;
                    newRow.Cells[3].Value = 0;
                    newRow.Cells[4].Value = 0;
                    newRow.Cells[5].Value = 0;

                    if (tmp.ScheduleCityList.Any(x => x.City == 1))
                        newRow.Cells[6].Value = 1;
                    if (tmp.ScheduleCityList.Any(x => x.City == 10))
                        newRow.Cells[7].Value = 1;
                    if (tmp.ScheduleCityList.Any(x => x.City == 12))
                        newRow.Cells[8].Value = 1;
                    if (tmp.ScheduleCityList.Any(x => x.City == 3))
                        newRow.Cells[9].Value = 1;
                    if (tmp.ScheduleCityList.Any(x => x.City == 8))
                        newRow.Cells[10].Value = 1;
                    if (tmp.ScheduleCityList.Any(x => x.City == 13))
                        newRow.Cells[11].Value = 1;
                    if (tmp.ScheduleCityList.Any(x => x.City == 9))
                        newRow.Cells[12].Value = 1;

                    if (tmp.ScheduleCityList.Any(x => x.Split == true))
                        newRow.Cells[13].Value = 1;
                    if (tmp.ScheduleCityList.Any(x => x.Price == true))
                        newRow.Cells[14].Value = 1;

                    dGridDailyView.Rows.Add(newRow);
                }

                nuNumberSchedules.Value = 1;

                Globalvar.SplitSchedules = 1;

                MessageBox.Show("Schedule was cleared ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void dGridDailyView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dGridDailyView.EndEdit();

            int number = Convert.ToInt32(lblNewPostTotal.Text);

            int splitSchedule =Convert.ToInt32(nuNumberSchedules.Value);

            if (e.ColumnIndex == dGridDailyView.Columns["OC"].Index && e.RowIndex >= 0)
            {
                var chkCell = dGridDailyView.Rows[e.RowIndex].Cells["OC"];

                bool cityChecked = (null != chkCell && null != chkCell.Value && Convert.ToInt32(chkCell.Value) == 1);

                if (cityChecked)
                {

                    number = number + Convert.ToInt32(dGridDailyView.Rows[e.RowIndex].Cells["Inventory"].Value);

                    lblNewPostTotal.Text = number.ToString(CultureInfo.InvariantCulture);

                    lblAverage.Text =Math.Ceiling((double)number/splitSchedule).ToString();
                }
                else
                {
                    number = number - Convert.ToInt32(dGridDailyView.Rows[e.RowIndex].Cells["Inventory"].Value);

                    lblNewPostTotal.Text = number.ToString(CultureInfo.InvariantCulture);

                    lblAverage.Text = Math.Ceiling((double)number / splitSchedule).ToString();
                }

            }
            else if (e.ColumnIndex == dGridDailyView.Columns["LB"].Index && e.RowIndex >= 0)
            {
                var chkCell = dGridDailyView.Rows[e.RowIndex].Cells["LB"];

                bool cityChecked = (null != chkCell && null != chkCell.Value && Convert.ToInt32(chkCell.Value) == 1);

                if (cityChecked)
                {

                    number = number + Convert.ToInt32(dGridDailyView.Rows[e.RowIndex].Cells["Inventory"].Value);

                    lblNewPostTotal.Text = number.ToString(CultureInfo.InvariantCulture);

                    lblAverage.Text = Math.Ceiling((double)number / splitSchedule).ToString();
                }
                else
                {
                    number = number - Convert.ToInt32(dGridDailyView.Rows[e.RowIndex].Cells["Inventory"].Value);

                    lblNewPostTotal.Text = number.ToString(CultureInfo.InvariantCulture);

                    lblAverage.Text = Math.Ceiling((double)number / splitSchedule).ToString();
                }

            }
            else if (e.ColumnIndex == dGridDailyView.Columns["IE"].Index && e.RowIndex >= 0)
            {
                var chkCell = dGridDailyView.Rows[e.RowIndex].Cells["IE"];

                bool cityChecked = (null != chkCell && null != chkCell.Value && Convert.ToInt32(chkCell.Value) == 1);

                if (cityChecked)
                {

                    number = number + Convert.ToInt32(dGridDailyView.Rows[e.RowIndex].Cells["Inventory"].Value);

                    lblNewPostTotal.Text = number.ToString(CultureInfo.InvariantCulture);

                    lblAverage.Text = Math.Ceiling((double)number / splitSchedule).ToString();
                }
                else
                {
                    number = number - Convert.ToInt32(dGridDailyView.Rows[e.RowIndex].Cells["Inventory"].Value);

                    lblNewPostTotal.Text = number.ToString(CultureInfo.InvariantCulture);

                    lblAverage.Text = Math.Ceiling((double)number / splitSchedule).ToString();
                }

            }

            else if (e.ColumnIndex == dGridDailyView.Columns["NSD"].Index && e.RowIndex >= 0)
            {
                var chkCell = dGridDailyView.Rows[e.RowIndex].Cells["NSD"];

                bool cityChecked = (null != chkCell && null != chkCell.Value && Convert.ToInt32(chkCell.Value) == 1);

                if (cityChecked)
                {

                    number = number + Convert.ToInt32(dGridDailyView.Rows[e.RowIndex].Cells["Inventory"].Value);

                    lblNewPostTotal.Text = number.ToString(CultureInfo.InvariantCulture);

                    lblAverage.Text = Math.Ceiling((double)number / splitSchedule).ToString();
                }
                else
                {
                    number = number - Convert.ToInt32(dGridDailyView.Rows[e.RowIndex].Cells["Inventory"].Value);

                    lblNewPostTotal.Text = number.ToString(CultureInfo.InvariantCulture);

                    lblAverage.Text = Math.Ceiling((double)number / splitSchedule).ToString();
                }
            }

            else if (e.ColumnIndex == dGridDailyView.Columns["CLA"].Index && e.RowIndex >= 0)
            {
                var chkCell = dGridDailyView.Rows[e.RowIndex].Cells["CLA"];

                bool cityChecked = (null != chkCell && null != chkCell.Value && Convert.ToInt32(chkCell.Value) == 1);

                if (cityChecked)
                {

                    number = number + Convert.ToInt32(dGridDailyView.Rows[e.RowIndex].Cells["Inventory"].Value);

                    lblNewPostTotal.Text = number.ToString(CultureInfo.InvariantCulture);

                    lblAverage.Text = Math.Ceiling((double)number / splitSchedule).ToString();
                }
                else
                {
                    number = number - Convert.ToInt32(dGridDailyView.Rows[e.RowIndex].Cells["Inventory"].Value);

                    lblNewPostTotal.Text = number.ToString(CultureInfo.InvariantCulture);

                    lblAverage.Text = Math.Ceiling((double)number / splitSchedule).ToString();
                }

            }
            else if (e.ColumnIndex == dGridDailyView.Columns["PS"].Index && e.RowIndex >= 0)
            {
                var chkCell = dGridDailyView.Rows[e.RowIndex].Cells["PS"];

                bool cityChecked = (null != chkCell && null != chkCell.Value && Convert.ToInt32(chkCell.Value) == 1);

                if (cityChecked)
                {

                    number = number + Convert.ToInt32(dGridDailyView.Rows[e.RowIndex].Cells["Inventory"].Value);

                    lblNewPostTotal.Text = number.ToString(CultureInfo.InvariantCulture);

                    lblAverage.Text = Math.Ceiling((double)number / splitSchedule).ToString();
                }
                else
                {
                    number = number - Convert.ToInt32(dGridDailyView.Rows[e.RowIndex].Cells["Inventory"].Value);

                    lblNewPostTotal.Text = number.ToString(CultureInfo.InvariantCulture);

                    lblAverage.Text = Math.Ceiling((double)number / splitSchedule).ToString();
                }
            }
            else if (e.ColumnIndex == dGridDailyView.Columns["SBV"].Index && e.RowIndex >= 0)
            {
                var chkCell = dGridDailyView.Rows[e.RowIndex].Cells["SBV"];

                bool cityChecked = (null != chkCell && null != chkCell.Value && Convert.ToInt32(chkCell.Value) == 1);

                if (cityChecked)
                {

                    number = number + Convert.ToInt32(dGridDailyView.Rows[e.RowIndex].Cells["Inventory"].Value);

                    lblNewPostTotal.Text = number.ToString(CultureInfo.InvariantCulture);

                    lblAverage.Text = Math.Ceiling((double)number / splitSchedule).ToString();
                }
                else
                {
                    number = number - Convert.ToInt32(dGridDailyView.Rows[e.RowIndex].Cells["Inventory"].Value);

                    lblNewPostTotal.Text = number.ToString(CultureInfo.InvariantCulture);

                    lblAverage.Text = Math.Ceiling((double)number / splitSchedule).ToString();
                }

            }
            else if (e.ColumnIndex == dGridDailyView.Columns["HalfSplit"].Index && e.RowIndex >= 0)
            {
                int numberofNewpostsinARow = 0;

                var chkCell = dGridDailyView.Rows[e.RowIndex].Cells["HalfSplit"];

                var chkOc = dGridDailyView.Rows[e.RowIndex].Cells["OC"] as DataGridViewCheckBoxCell;

                var chkLB = dGridDailyView.Rows[e.RowIndex].Cells["LB"] as DataGridViewCheckBoxCell;

                var chkIe = dGridDailyView.Rows[e.RowIndex].Cells["IE"] as DataGridViewCheckBoxCell;

                var chkNsd = dGridDailyView.Rows[e.RowIndex].Cells["NSD"] as DataGridViewCheckBoxCell;

                var chkCla = dGridDailyView.Rows[e.RowIndex].Cells["CLA"] as DataGridViewCheckBoxCell;

                var chkSbv = dGridDailyView.Rows[e.RowIndex].Cells["SBV"] as DataGridViewCheckBoxCell;

                var chkPS = dGridDailyView.Rows[e.RowIndex].Cells["PS"] as DataGridViewCheckBoxCell;

                
                bool ocChecked = (null != chkOc && null != chkOc.Value && Convert.ToInt32(chkOc.Value) == 1);
                bool lbChecked = (null != chkLB && null != chkLB.Value && Convert.ToInt32(chkLB.Value) == 1);
                bool ieChecked = (null != chkIe && null != chkIe.Value && Convert.ToInt32(chkIe.Value) == 1);
                bool nsdChecked = (null != chkNsd && null != chkNsd.Value && Convert.ToInt32(chkNsd.Value) == 1);
                bool claChecked = (null != chkCla && null != chkCla.Value && Convert.ToInt32(chkCla.Value) == 1);
                bool sbvChecked = (null != chkSbv && null != chkSbv.Value && Convert.ToInt32(chkSbv.Value) == 1);
                bool psChecked = (null != chkPS && null != chkPS.Value && Convert.ToInt32(chkPS.Value) == 1);
                bool halfChecked = (null != chkCell && null != chkCell.Value && Convert.ToInt32(chkCell.Value) == 1);

                if (ocChecked)
                    numberofNewpostsinARow += Convert.ToInt32(dGridDailyView.Rows[e.RowIndex].Cells["Inventory"].Value);
                if (lbChecked)
                    numberofNewpostsinARow += Convert.ToInt32(dGridDailyView.Rows[e.RowIndex].Cells["Inventory"].Value);
                if (ieChecked)
                    numberofNewpostsinARow += Convert.ToInt32(dGridDailyView.Rows[e.RowIndex].Cells["Inventory"].Value);
                if (nsdChecked)
                    numberofNewpostsinARow += Convert.ToInt32(dGridDailyView.Rows[e.RowIndex].Cells["Inventory"].Value);
                if (claChecked)
                    numberofNewpostsinARow += Convert.ToInt32(dGridDailyView.Rows[e.RowIndex].Cells["Inventory"].Value);
                if (sbvChecked)
                    numberofNewpostsinARow += Convert.ToInt32(dGridDailyView.Rows[e.RowIndex].Cells["Inventory"].Value);
                if (psChecked)
                    numberofNewpostsinARow += Convert.ToInt32(dGridDailyView.Rows[e.RowIndex].Cells["Inventory"].Value);
               
                if (halfChecked)
                {

                    number = number - numberofNewpostsinARow + (numberofNewpostsinARow/2);

                    lblNewPostTotal.Text = number.ToString(CultureInfo.InvariantCulture);

                    lblAverage.Text = Math.Ceiling((double)number / splitSchedule).ToString(CultureInfo.InvariantCulture);
                }

                else
                {

                    number = number + numberofNewpostsinARow - (numberofNewpostsinARow / 2);

                    lblNewPostTotal.Text = number.ToString(CultureInfo.InvariantCulture);

                    lblAverage.Text = Math.Ceiling((double)number / splitSchedule).ToString(CultureInfo.InvariantCulture);
                }
            }

        }

        private void nuNumberSchedules_ValueChanged(object sender, EventArgs e)
        {
            int number = Convert.ToInt32(lblNewPostTotal.Text);

            int splitSchedule = Convert.ToInt32(nuNumberSchedules.Value);

            lblAverage.Text = Math.Ceiling((double)number / splitSchedule).ToString(CultureInfo.InvariantCulture);
        }




    }
}
