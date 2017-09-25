using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CraigslistManagerApp.Helper;
using System.IO;

namespace CraigslistManagerApp
{
    public partial class UploadForm : Form
    {
        public UploadForm()
        {
            InitializeComponent();
        }

        public UploadForm(MAINFORM frmMain)
        {
            InitializeComponent();
            this.frmMain = frmMain;
        }

        private void UploadForm_Load(object sender, EventArgs e)
        {

            XmlNodeList cityList = getListCityOfDays();
            
            foreach (XmlNode node in cityList)
            {
                string cityName = node.Attributes["County"].Value;

                clBoxCityList.Items.Add(cityName);
            }

            lblDealerName.Text = clsVariables.currentDealer.DealershipName;

            lblID.Text = clsVariables.currentDealer.DealerId.ToString();

            
        }

        private XmlNodeList getListCityOfDays()
        {
            string hostingXMLPath = MAINFORM.dataFolderPath + "/PreloadData/HostingServer.xml";
            
            XmlNodeList dateNode = null;

            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    dateNode = XMLHelper.selectElements("Server", hostingXMLPath, "DaysOfWeek=" + DayOfWeek.Monday.ToString());
                    break;
                case DayOfWeek.Tuesday:
                    dateNode = XMLHelper.selectElements("Server", hostingXMLPath, "DaysOfWeek=" + DayOfWeek.Tuesday.ToString());
                    break;
                case DayOfWeek.Wednesday:
                    dateNode = XMLHelper.selectElements("Server", hostingXMLPath, "DaysOfWeek=" + DayOfWeek.Wednesday.ToString());
                    break;
                case DayOfWeek.Thursday:
                    dateNode = XMLHelper.selectElements("Server", hostingXMLPath, "DaysOfWeek=" + DayOfWeek.Thursday.ToString());
                    break;
                case DayOfWeek.Friday:
                    dateNode = XMLHelper.selectElements("Server", hostingXMLPath, "DaysOfWeek=" + DayOfWeek.Friday.ToString());
                    break;
                case DayOfWeek.Saturday:
                    dateNode = XMLHelper.selectElements("Server", hostingXMLPath, "DaysOfWeek=" + DayOfWeek.Saturday.ToString());
                    break;
                case DayOfWeek.Sunday:
                    dateNode = XMLHelper.selectElements("Server", hostingXMLPath, "DaysOfWeek=" + DayOfWeek.Sunday.ToString());
                    break;
                default:
                    dateNode = XMLHelper.selectElements("Server", hostingXMLPath, "DaysOfWeek=" + DayOfWeek.Monday.ToString());
                    break;
            }

            return dateNode;
        }

        private List<CarStockURL> generateImages()
        {
            List<CarStockURL> list = new List<CarStockURL>();
            
            WebsitesScreenshot.WebsitesScreenshot _Obj;

            _Obj = new WebsitesScreenshot.WebsitesScreenshot(System.Configuration.ConfigurationManager.AppSettings["WebScreenShotSerialKey"].ToString());

            try
            {
                foreach (WhitmanEntepriseVehicleInfo vehicle in clsVariables.currentDealer.Inventory)
                {

                    WebsitesScreenshot.WebsitesScreenshot.Result _Result;
                    _Result = _Obj.CaptureHTML("<html><body>" + CommonHelper.generateHTMLImageCode(vehicle, frmMain.getRadioButtonChecked(), frmMain.getRadioButtonInsertChecked(), frmMain.cbPrice.Checked,  frmMain.txtDealerTitle.Text) + "</body></html>");

                    if (_Result == WebsitesScreenshot.WebsitesScreenshot.Result.Captured)
                    {
                        _Obj.ImageFormat = WebsitesScreenshot.
                            WebsitesScreenshot.ImageFormats.JPG;

                        System.Drawing.Bitmap _Image; _Image = _Obj.GetImage();

                        MemoryStream stream = new MemoryStream();

                        _Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);

                        Byte[] bytes = stream.ToArray();
                                              
                        var car = new CarStockURL()
                        {
                            Stock=vehicle.StockNumber,
                            ImageRow=vehicle,
                            ImageBytes = bytes
                        };

                        list.Add(car);

                        stream.Close();

                    }


                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Generate Images Failed " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return list;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            lblUpload.Visible = true;
            if (this.clBoxCityList.CheckedItems.Count > 0)
            {
                var list = generateImages();

                BasicFTPClient MyClient = new BasicFTPClient("");

                if (list.Count > 0)
                {
                    try
                    {
                        foreach (string city in this.clBoxCityList.CheckedItems)
                        {
                          

                            XmlNode hostingNode = CommonHelper.randomizeHost(city);

                            foreach (CarStockURL carImage in list)
                            {
                                MyClient.connect();

                                Stream s = new MemoryStream(carImage.ImageBytes);

                                string imageURL = CommonHelper.generateImageURL(carImage.ImageRow);

                                string imageFileName = CommonHelper.generateImageFileName(carImage.ImageRow);

                                carImage.ImageURL = hostingNode.Attributes["HostName"].Value + imageURL + "/" + imageFileName;

                                MyClient.Upload(hostingNode, s, imageURL, imageFileName);

                                MyClient.closeConnect();

                                s.Close();

                            }

                            //CommonHelper.generateXMLFile(list, city);
                            
                            this.listFinishedCities.Items.Add(city);

                            this.Refresh();

                            System.Threading.Thread.Sleep(1000);

                        }
                        lblUpload.Visible = false;

                        System.Windows.Forms.MessageBox.Show("Uploading images for " + clsVariables.currentDealer.DealershipName + " Is Done", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Uploading Failed " + ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }

            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Please choose at least one city to upload images" , "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
          
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
         
        }
    }
}
