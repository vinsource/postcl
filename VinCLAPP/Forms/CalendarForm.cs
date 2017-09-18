using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using VinCLAPP.DatabaseModel;
using VinCLAPP.Helper;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms.DataVisualization.Charting;

namespace VinCLAPP.Forms
{
    public partial class CalendarForm : Form
    {
        public CalendarForm()
        {
            InitializeComponent();
        }

        private void CalendarForm_Load(object sender, EventArgs e)
        {
            dateTimePickerFrom.MaxDate = DateTime.Now;
            dateTimePickerEnd.MaxDate = DateTime.Now;

            cbCity.DataSource = GetCityList();
        }

        public static IEnumerable<ShortCity> GetCityList()
        {
            var returnList = new List<ShortCity>();

            returnList.Add(new ShortCity()
            {
                CityName = "All",
                CityId = 0
            });

            foreach (var tmp in GlobalVar.CurrentDealer.CityList)
            {
                var record = new ShortCity()
                {
                    CityName = tmp.CityName,
                    CityId = tmp.CityID,
                };

                returnList.Add(record);
            }
           
            return returnList;
        }

        private void btnView_Click_1(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value > dateTimePickerEnd.Value)
            {

                MessageBox.Show(
                    "Date Range is not valid",
                    "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                try
                {
                    var context = new CLDMSEntities();

                    var dateTo = new DateTime(dateTimePickerFrom.Value.Year, dateTimePickerFrom.Value.Month, dateTimePickerFrom.Value.Day);

                    var dateEnd =
                        new DateTime(dateTimePickerEnd.Value.Year, dateTimePickerEnd.Value.Month,
                            dateTimePickerEnd.Value.Day).AddDays(1);


                    var fullCityList = context.Cities.ToList();

                    var fullAdsList = new List<TrackingAds>();

                    var selectedCity = (ShortCity)cbCity.SelectedItem;

                    if (selectedCity.CityId == 0)
                    {

                        foreach (
                            var tmp in
                                context.Trackings.Where(
                                    a =>a.AccountId==GlobalVar.CurrentAccount.AccountId&& a.AddedDate >= dateTo && a.AddedDate <= dateEnd))
                        {
                            var index = 1;
                            var record = new TrackingAds()
                            {
                                Index = index++,
                                CityName = fullCityList.FirstOrDefault(x => x.CityID == tmp.CityId).CityName,
                                CraigslistUrl = tmp.AdsUrl,
                                PostingId = tmp.CLPostingId.GetValueOrDefault(),
                                CreatedDate = tmp.AddedDate.GetValueOrDefault(),
                                ExpirationDate = tmp.AddedDate.GetValueOrDefault().AddDays(30)
                            };
                            fullAdsList.Add(record);

                        }
                    }
                    else
                    {
                        foreach (
                           var tmp in
                               context.Trackings.Where(
                                   a => a.AccountId == GlobalVar.CurrentAccount.AccountId && a.CityId == selectedCity.CityId && a.AddedDate >= dateTo && a.AddedDate <= dateEnd))
                        {
                            var index = 1;
                            var record = new TrackingAds()
                            {
                                Index = index++,
                                CityName = fullCityList.FirstOrDefault(x => x.CityID == tmp.CityId).CityName,
                                CraigslistUrl = tmp.AdsUrl,
                                PostingId = tmp.CLPostingId.GetValueOrDefault(),
                                CreatedDate = tmp.AddedDate.GetValueOrDefault(),
                                ExpirationDate = tmp.AddedDate.GetValueOrDefault().AddDays(30)
                            };
                            fullAdsList.Add(record);

                        }
                    }


                    var csvWriterDealer = new CsvExport<TrackingAds>(fullAdsList.OrderBy(x => x.CreatedDate).ToList());


                    string pathDownload = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                    string fullFilePathForDealer = pathDownload + "/trackingads" + "_" + DateTime.Now.ToString("MMddyy") +
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
                catch (Exception)
                {

                    MessageBox.Show(
                        "There is an error when opening a report. Please send the report to your email instead.");
                }
         
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value > dateTimePickerEnd.Value)
            {

                MessageBox.Show(
                    "Date Range is not valid",
                    "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                try
                {
                    var context = new CLDMSEntities();

                    var dateTo = new DateTime(dateTimePickerFrom.Value.Year, dateTimePickerFrom.Value.Month, dateTimePickerFrom.Value.Day);

                    var dateEnd =
                      new DateTime(dateTimePickerEnd.Value.Year, dateTimePickerEnd.Value.Month,
                          dateTimePickerEnd.Value.Day).AddDays(1);

                    var fullCityList = context.Cities.ToList();

                    var fullAdsList = new List<TrackingAds>();

                    var selectedCity = (ShortCity)cbCity.SelectedItem;

                    if (selectedCity.CityId == 0)
                    {

                        foreach (
                            var tmp in
                                context.Trackings.Where(
                                    a => a.AccountId == GlobalVar.CurrentAccount.AccountId && a.AddedDate >= dateTo && a.AddedDate <= dateEnd))
                        {
                            var index = 1;
                            var record = new TrackingAds()
                            {
                                Index = index++,
                                CityName = fullCityList.FirstOrDefault(x => x.CityID == tmp.CityId).CityName,
                                CraigslistUrl = tmp.AdsUrl,
                                PostingId = tmp.CLPostingId.GetValueOrDefault(),
                                CreatedDate = tmp.AddedDate.GetValueOrDefault(),
                                ExpirationDate = tmp.AddedDate.GetValueOrDefault().AddDays(30)
                            };
                            fullAdsList.Add(record);

                        }
                    }
                    else
                    {
                        foreach (
                           var tmp in
                               context.Trackings.Where(
                                   a => a.AccountId == GlobalVar.CurrentAccount.AccountId && a.CityId == selectedCity.CityId && a.AddedDate >= dateTo && a.AddedDate <= dateEnd))
                        {
                            var index = 1;
                            var record = new TrackingAds()
                            {
                                Index = index++,
                                CityName = fullCityList.FirstOrDefault(x => x.CityID == tmp.CityId).CityName,
                                CraigslistUrl = tmp.AdsUrl,
                                PostingId = tmp.CLPostingId.GetValueOrDefault(),
                                CreatedDate = tmp.AddedDate.GetValueOrDefault(),
                                ExpirationDate = tmp.AddedDate.GetValueOrDefault().AddDays(30)
                            };
                            fullAdsList.Add(record);

                        }
                    }



                    var csvWriterDealer = new CsvExport<TrackingAds>(fullAdsList.OrderBy(x => x.CreatedDate).ToList());


                    string pathDownload = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                    string fileName = "trackingads" + "_" + DateTime.Now.ToString("MMddyy") +
                                                   DateTime.Now.Millisecond +
                                                   ".csv";

                    string fullFilePathForDealer = pathDownload + fileName;

                    csvWriterDealer.ExportToFileWithHeader(fullFilePathForDealer);

                    EmailHelper.SendExcelMail(fileName, csvWriterDealer.ExportToBytes());
                    MessageBox.Show("Hi " + GlobalVar.CurrentAccount.FirstName + " " + GlobalVar.CurrentAccount.LastName +
                                    ". You will receive the report in your inbox shortly.", "Info", MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                    

                }
                catch (Exception)
                {

                    MessageBox.Show(
                        "There is an error when opening a report. Please send the report to your email instead.");
                }

            }
           
        }
    }

    public class TrackingAds
    {
        public int Index { get; set; }

        public string CityName { get; set; }

        public long PostingId { get; set; }

        public string CraigslistUrl { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
    public class ShortTrackingAds
    {
        public int ListingId { get; set; }

        public int CityId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int TotalAds { get; set; }

        
    }
    public class ShortCity
    {

        public string CityName { get; set; }

        public int CityId { get; set; }

        public override string ToString()
        {
            return CityName;
        }
    }




}
