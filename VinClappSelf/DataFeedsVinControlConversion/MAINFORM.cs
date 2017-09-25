using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Text.RegularExpressions;
using System.Xml.XPath;
using DataFeedsVinControlConversion.DBCL;
using DataFeedsVinControlConversion.DatabaseModelVinControlScrappingData;
using DataFeedsVinControlConversion.Helper;
using DataFeedsVinControlConversion.OldDatabaseModel;
using HiQPdf;
using HtmlAgilityPack;
using LumenWorks.Framework.IO.Csv;
using DataFeedsVinControlConversion.com.chrome.platform.AutomotiveDescriptionService6;
using System.Net;
using System.IO;
using DataFeedsVinControlConversion.DatabaseModel;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using HtmlDocument = System.Windows.Forms.HtmlDocument;

namespace DataFeedsVinControlConversion
{
    public partial class MAINFORM : Form
    {
        private DataTable dtCraigInventory = null;

        private DataTable dtVinControlInventory = null;

        private XmlNode dealerShip = null;

        private string dealerXMLPath = null;

        private DataTable dtInventory = null;


        public MAINFORM()
        {
            InitializeComponent();

            dtInventory = new DataTable();

        }

        private void MAINFORM_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'zipcodesDataSet.ZIP_Codes' table. You can move, or remove it, as needed.

            dealerXMLPath = AppDomain.CurrentDomain.BaseDirectory + "/PreloadData/DealerList.xml";

            //string rootPath = AppDomain.CurrentDomain.BaseDirectory;

            //rootPath = rootPath.Remove(rootPath.Length - 1);

            //DirectoryInfo di = new DirectoryInfo(rootPath.Substring(0, rootPath.LastIndexOf(@"\")));

            //txtDealerID.Text = di.Parent.FullName + "/DealerList.xml";

        }




        private void btnDealer_Click(object sender, EventArgs e)
        {

            //StreamReader reader = new StreamReader(@"C:\CRAIGSLISTDATAFEED\AutoSoft\23680.txt");
            //SQLHelper.PopulateDataTableFromTextFile(reader);
            //dtCraigInventory = new DataTable();
            //dtCraigInventory = SQLHelper.getInventoryForCraiglistFromFTPServer("/23680.txt");
            ////dtCraigInventory.Load(csv);
            //dRgDealer.DataSource = dtCraigInventory;
            //string Path = "/" + txtDealerID.Text + ".txt";

            string Path = "/19527.txt";

            //dealerShip = XMLHelper.selectOneElement("Dealer", dealerXMLPath, "Id=" + txtDealerID.Text);



            CachedCsvReader csv = SQLHelper.getInventoryAutoTraderFormatWithHeader(Path);


            var list = new List<string>();

            //dRgDealer.DataSource = csv;

            //System.Windows.Forms.MessageBox.Show(csv.FieldCount.ToString(), "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (csv != null)
            {
                dtCraigInventory = new DataTable();
                dtCraigInventory.Load(csv);

                foreach (var tmp in dtCraigInventory.Rows)
                {
                    //var splitArray = tmp.ToString().Split(new string[] {"|"}, StringSplitOptions.None);


                    //foreach (var s in splitArray)
                    //{
                    //    list.Add(s);
                    //    break;
                    //}
                }
            }

            drgVinControl.DataSource = list;
            //txtDealerID.Text = (char)(((Int64)'A') + 1) + "";

        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            dtVinControlInventory = new DataTable();

            dtVinControlInventory = SQLHelper.initialVinControlDataTable();

            AutoService autoService = new AutoService();


            foreach (DataRow drRow in dtCraigInventory.Rows)
            {
                StringBuilder imageBuilder = new StringBuilder();
                VehicleInformation vehicleInfo = autoService.getVehicleInformationFromVin(drRow.Field<string>("vin"));


                DataRow dr = dtVinControlInventory.NewRow();

                dr["ModelYear"] = drRow.Field<string>("Year");

                dr["Make"] = drRow.Field<string>("Make");

                dr["Model"] = drRow.Field<string>("Model");

                dr["Trim"] = drRow.Field<string>("trimlevel");

                dr["VINNumber"] = drRow.Field<string>("vin");

                dr["StockNumber"] = drRow.Field<string>("stocknumber");

                dr["SalePrice"] = drRow.Field<string>("price");

                dr["MSRP"] = drRow.Field<string>("msrp");

                dr["Mileage"] = drRow.Field<string>("odometer");

                dr["ExteriorColor"] = drRow.Field<string>("exteriorcolor");

                dr["InteriorColor"] = drRow.Field<string>("interiorcolor");


                dr["InteriorSurface"] = null;

                try
                {
                    foreach (BodyType bd in vehicleInfo.vehicleSpecification.bodyTypes)
                    {
                        if (!String.IsNullOrEmpty(bd.bodyTypeName))
                        {
                            dr["BodyType"] = bd.bodyTypeName;
                            break;
                        }

                    }
                }
                catch (Exception)
                {
                    dr["BodyType"] = null;
                }
                try
                {
                    foreach (Engine er in vehicleInfo.vehicleSpecification.engines)
                    {
                        string fuelType = er.fuelType;
                        int index = fuelType.LastIndexOf(" ");

                        dr["Cylinders"] = er.cylinders.ToString();
                        dr["FuelType"] = fuelType.Substring(0, index);
                        dr["Liters"] = er.displacementL.ToString();
                        dr["EngineType"] = er.engineType;
                        break;
                    }
                }
                catch (Exception)
                {
                    dr["Cylinders"] = null;
                    dr["FuelType"] = null;
                    dr["Liters"] = null;
                    dr["EngineType"] = null;
                }




                try
                {

                    dr["DriveTrain"] = getDriveXMLFileFromVehicleInfo(vehicleInfo);
                }
                catch (Exception)
                {
                    dr["DriveTrain"] = null;

                }

                dr["Tranmission"] = drRow.Field<string>("transmissiontype");

                try
                {

                    dr["Doors"] = vehicleInfo.vehicleSpecification.numberOfPassengerDoors;
                }
                catch (Exception)
                {
                    dr["Doors"] = null;
                }



                dr["Certified"] = true;

                StringBuilder builderOption = new StringBuilder();

                if (vehicleInfo != null && vehicleInfo.standards != null)
                {

                    foreach (Standard sd in vehicleInfo.standards)
                    {
                        if (!String.IsNullOrEmpty(sd.description))
                        {
                            builderOption.Append(sd.description + ",");
                        }
                    }
                }
                if (String.IsNullOrEmpty(builderOption.ToString()))
                    dr["CarsOptions"] = "";
                else
                {
                    builderOption.Remove(builderOption.Length - 1, 1);
                    dr["CarsOptions"] = builderOption.ToString().Replace("\'", "\\'");
                }
                //if (String.IsNullOrEmpty(drRow.Field<string>("options")))
                //    dr["CarsOptions"] = drRow.Field<string>("options");
                //else
                //dr["CarsOptions"] = drRow.Field<string>("options").Replace("\'", "\\'"); 




                dr["Descriptions"] = drRow.Field<string>("description").Replace("\'", "\\'");

                //if (hash.Contains(drRow.Field<string>("stocknumber")))

                //    dr["CarImageUrl"] = hash[drRow.Field<string>("stocknumber")];
                //else
                //    dr["CarImageUrl"] = "";

                //http://whitmanentimages.com/vinanalysis/NewportCoastAuto/ZAMEC38A360022669/3434T-1.jpg

                //for (int i = 1; i < 28; i++)
                //{

                //    imageBuilder.Append("http://whitmanentimages.com/vinanalysis/NewportCoastAuto/" + drRow.Field<string>("vin") + "/" + drRow.Field<string>("stocknumber") + "-" + i + ".jpg");
                //    imageBuilder.Append(",");

                //}

                dr["CarImageUrl"] = drRow.Field<string>("picurl").Replace("|", ",");

                dr["DateInStock"] = DateTime.Now.ToShortDateString();

                dr["DealershipName"] = dealerShip.Attributes["Name"].Value;

                dr["DealershipAddress"] = dealerShip.Attributes["StreetAddress"].Value;

                dr["DealershipCity"] = dealerShip.Attributes["City"].Value;

                dr["DealershipState"] = dealerShip.Attributes["ZipCode"].Value;

                dr["DealershipPhone"] = dealerShip.Attributes["PhoneNumber"].Value;

                dr["DealershipId"] = "1541";

                dr["DealerCost"] = null;

                dr["ACV"] = null;

                try
                {
                    if (vehicleInfo.stockPhotos != null && vehicleInfo.stockPhotos.Count() > 0)
                        dr["DefaultImageUrl"] = vehicleInfo.stockPhotos.First().url;
                    else
                        dr["DefaultImageUrl"] =
                            drRow.Field<string>("picurl").Split(new String[] {","},
                                                                StringSplitOptions.RemoveEmptyEntries).First<string>();
                }
                catch (Exception)
                {
                    dr["DefaultImageUrl"] =
                        drRow.Field<string>("picurl").Split(new String[] {","}, StringSplitOptions.RemoveEmptyEntries).
                            First<string>();
                }
                dr["AddToInventoryBy"] = "1541";

                dr["Newused"] = "Used";

                dr["AppraisalId"] = "";

                dtVinControlInventory.Rows.Add(dr);
            }

            drgVinControl.DataSource = dtVinControlInventory;
        }

        public static string getDriveXMLFileFromVehicleInfo(VehicleInformation vehicleInformation)
        {
            GenericEquipment[] list_genericEquip = vehicleInformation.genericEquipment;

            string drive = "Any Drive";

            foreach (GenericEquipment ge in list_genericEquip)
            {
                if (ge.categoryId == 1040 || ge.categoryId == 1041 || ge.categoryId == 1042 || ge.categoryId == 1043)
                {
                    drive = ge.categoryId.ToString();
                    break;
                }

            }
            XmlNode driveNode = XMLHelper.selectOneElement("Drive",
                                                           AppDomain.CurrentDomain.BaseDirectory +
                                                           "/PreLoadData/WheelDrive.xml", "Name=" + drive);

            if (driveNode != null)

                return driveNode.Attributes["Value"].Value;

            return "";

        }



        private void btnCategory_Click(object sender, EventArgs e)
        {
            DataTable dtCategory = SQLHelper.GetDataTableFromMySQL("GetWhitmanVehicleCategory");

            dRgDealer.DataSource = dtCategory;

            this.Refresh();

            DataTable dtCarsInWareHouse = SQLHelper.GetDataTableFromMySQL("SelectCarsNoDistance");




            foreach (DataRow dr in dtCarsInWareHouse.Rows)
            {
                //EnumerableRowCollection<DataRow> list = dtCategory.AsEnumerable().Where(t => t.Field<int>("Year") == dr.Field<int>("ModelYear") && t.Field<string>("Make").Equals(dr["Make"].ToString()) && t.Field<string>("Model").Equals(dr["Model"].ToString()));

                EnumerableRowCollection<DataRow> list =
                    dtCategory.AsEnumerable().Where(
                        t => t.Field<string>("NameCompare").Equals(dr["NameCompare"].ToString()));

                if (list.Count() > 0)
                {

                    dr["CategoryID"] = list.First().Field<int>("CategoryID");

                }
                else
                    dr["CategoryID"] = 0;
            }
            drgVinControl.DataSource = dtCarsInWareHouse;
            this.Refresh();

            SQLHelper.updateCategoryID(dtCarsInWareHouse);

            System.Windows.Forms.MessageBox.Show("DONE", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            DataTable dtDealership = SQLHelper.GetDataTableFromMySQL("GetDealershipFinal");

            SQLHelper.InsertDefaultSetting(dtDealership);

            System.Windows.Forms.MessageBox.Show("DONE", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnAddress_Click(object sender, EventArgs e)
        {
            DataTable dtDealership = SQLHelper.GetDataTableFromMySQL("GetDealershipFinal");

            var regex = new Regex(@"(?<=\w)\w", RegexOptions.Compiled);

            DataTable dtZipCodes = SQLHelper.GetDataTableFromMySQL("GetSimpleZipCodes");

            dRgDealer.DataSource = dtZipCodes;

            Hashtable hash = new Hashtable();

            foreach (DataRow dr in dtZipCodes.Rows)
            {

                int zipCode = dr.Field<int>("zipcode");

                string City = regex.Replace(dr.Field<string>("City"),
                                            new MatchEvaluator(m => m.Value.ToLowerInvariant()));

                ZipCode zip = new ZipCode()
                                  {
                                      State = dr.Field<string>("state"),
                                      StateName = dr.Field<string>("StateName"),
                                      City = City
                                  };

                hash.Add(zipCode, zip);
            }

            SQLHelper.updateDealerAddress(dtDealership, hash);

            System.Windows.Forms.MessageBox.Show("DONE", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnFinalStreet_Click(object sender, EventArgs e)
        {
            DataTable dtDealership = SQLHelper.GetDataTableFromMySQL("GetDealershipFinal");

            drgVinControl.DataSource = dtDealership;

            this.Refresh();

            //SQLHelper.updateStreetAddress(dtDealership);
            //System.Windows.Forms.MessageBox.Show("DONE", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private DataTable dtAutoTrader = null;

        private void btnAutoTrader_Click(object sender, EventArgs e)
        {

            dtAutoTrader = SQLHelper.initialAutoTraderTable();


            foreach (DataRow drRow in dtInventory.Rows)
            {

                DataRow dr = dtAutoTrader.NewRow();

                dr["DealerId"] = 55862436;

                //dr["DealerName"] = drRow.Field<string>("DealershipName");

                //dr["Address"] = drRow.Field<string>("DealershipAddress");

                //dr["City"] = drRow.Field<string>("DealershipCity");

                //dr["State"] = drRow.Field<string>("DealershipState");

                //dr["ZipCode"] = drRow.Field<string>("DealershipZipCode");

                //dr["PhoneNumber"] = drRow.Field<string>("DealershipPhone");

                dr["StockNumber"] = drRow.Field<string>("StockNumber");

                dr["Year"] = drRow.Field<int>("ModelYear").ToString();

                dr["Make"] = drRow.Field<string>("Make");

                dr["Model"] = drRow.Field<string>("Model");


                dr["Trim"] = drRow.Field<string>("Trim");


                dr["VIN"] = drRow.Field<string>("VINNumber");

                dr["Mileage"] = drRow.Field<string>("Mileage");

                dr["Price"] = drRow.Field<string>("SalePrice");

                dr["ExteriorColor"] = drRow.Field<string>("ExteriorColor");

                dr["InteriorColor"] = drRow.Field<string>("InteriorColor");

                dr["Tranmission"] = drRow.Field<string>("Tranmission");

                dr["PhysicalImages"] = "";

                dr["Description"] = drRow.Field<string>("Descriptions").Trim();

                dr["BodyType"] = drRow.Field<string>("BodyType");

                dr["EngineType"] = drRow.Field<string>("EngineType");

                dr["DriveType"] = drRow.Field<string>("DriveTrain");

                dr["FuelType"] = drRow.Field<string>("FuelType");


                if (!String.IsNullOrEmpty(drRow.Field<string>("CarsOptions")) &&
                    drRow.Field<string>("CarsOptions").Contains("|"))
                    dr["Options"] = drRow.Field<string>("CarsOptions").Replace('|', ',');
                else
                    dr["Options"] = drRow.Field<string>("CarsOptions");


                if (!String.IsNullOrEmpty(drRow.Field<string>("CarImageUrl")) &&
                    drRow.Field<string>("CarImageUrl").Contains("|"))
                    dr["ImageURL"] = drRow.Field<string>("CarImageUrl").Replace('|', ',');
                else
                    dr["ImageURL"] = drRow.Field<string>("CarImageUrl");








                dr["VideoURL"] = "";

                //dr["VideoDuration"] = "";

                dr["VideoSource"] = "";



                dtAutoTrader.Rows.Add(dr);
            }

            drgAutoTrader.DataSource = dtAutoTrader;


        }

        private DataTable dtCarsCom = null;

        private DataTable dtCarsImage = null;

        private void btnCarsCom_Click(object sender, EventArgs e)
        {
            dtCarsCom = SQLHelper.initialCarsComTable();

            dtCarsImage = SQLHelper.initialCarsComImageTable();

            foreach (DataRow drRow in dtInventory.Rows)
            {

                DataRow dr = dtCarsCom.NewRow();

                dr["DealerId"] = drRow.Field<int>("DealershipID").ToString();

                dr["Type"] = drRow.Field<string>("NewUsed");

                dr["StockNumber"] = drRow.Field<string>("StockNumber");

                dr["VIN"] = drRow.Field<string>("VINNumber");

                dr["Year"] = drRow.Field<int>("ModelYear").ToString();

                dr["Make"] = drRow.Field<string>("Make");

                dr["Model"] = drRow.Field<string>("Model");

                dr["Body"] = drRow.Field<string>("BodyType");

                dr["Trim-Style"] = drRow.Field<string>("Trim");

                dr["ExtColor"] = drRow.Field<string>("ExteriorColor");

                dr["IntColor"] = drRow.Field<string>("InteriorColor");

                dr["EngineCylinders"] = drRow.Field<string>("Cylinders");

                dr["EngineDisplacement"] = drRow.Field<string>("Liters");

                dr["EngineDescription"] = drRow.Field<string>("Cylinders") + " " + drRow.Field<string>("Liters");

                dr["Tranmission"] = drRow.Field<string>("Tranmission");

                dr["Miles"] = drRow.Field<string>("Mileage");

                dr["SellingPrice"] = drRow.Field<string>("SalePrice");

                dr["Null-1"] = "";

                dr["Null-2"] = "";

                dr["Null-3"] = "";

                dr["DealerTagLine"] = "";

                if (!String.IsNullOrEmpty(drRow.Field<string>("CarsOptions")) &&
                    drRow.Field<string>("CarsOptions").Contains("|"))
                    dr["AddedOptions"] = drRow.Field<string>("CarsOptions").Replace('|', ',');
                else
                    dr["AddedOptions"] = drRow.Field<string>("CarsOptions");





                dtCarsCom.Rows.Add(dr);
            }


            foreach (DataRow drRow in dtInventory.Rows)
            {
                var imageList =
                    drRow.Field<string>("CarImageUrl").ToString().Split(new String[] {"|", ","},
                                                                        StringSplitOptions.RemoveEmptyEntries).ToList();

                foreach (string tmp in imageList)
                {
                    // int index = 1;

                    DataRow dr = dtCarsImage.NewRow();

                    dr["DealerId"] = drRow.Field<int>("DealershipID").ToString();
                    dr["VehicleId"] = drRow.Field<string>("StockNumber");

                    dr["VersionDate"] = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
                    dr["URL"] = tmp;
                    dr["ImageSequence"] = imageList.IndexOf(tmp) + 1;


                    dtCarsImage.Rows.Add(dr);
                }



            }

            drgCarsCom.DataSource = dtCarsCom;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dtInventory = SQLHelper.GetInventoryForDealership(Convert.ToInt32(txtDealerID.Text)).Tables[0];

            drgVinControl.DataSource = dtInventory;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string AutoTraderNamePipeDelimited = "VinControl" + DateTime.Now.ToString("MMddyy") + "-Pipe Delimited.txt";
            string AutoTraderNameCommaDelimited = "VinControl" + DateTime.Now.ToString("MMddyy") +
                                                  "-Comma Delimited.txt";


            AutoTraderCsvWriter autotrader = new AutoTraderCsvWriter();

            autotrader.WriteCsv(dtAutoTrader, @"C:\DataConversion\" + AutoTraderNameCommaDelimited);


            //using (CsvWriter csvWriter = new CsvWriter())
            //{
            //    csvWriter.WriteCsv(dtAutoTrader, @"C:\DataConversion\" + AutoTraderNamePipeDelimited);
            //}

            //using (CsvWriter csvWriter = new CsvWriter())
            //{
            //    //MODIFY IN WRITERECORD FUNCTION TO GENERATE TEXT TAB DELIMITED FILE

            //    csvWriter.WriteCsv(dtCarsCom, @"C:\DataConversion\CarsCom.txt");

            //}
            //using (CSVImageWriter csvImageWriter = new CSVImageWriter())
            //{
            //    csvImageWriter.WriteCsv(dtCarsImage, @"C:\DataConversion\CarsComImage.txt");
            //}
        }

        private Hashtable hash = new Hashtable();

        private void btnVin_Click(object sender, EventArgs e)
        {
            //dtVinControlInventory = SQLHelper.GetInventoryForDealership(1541).Tables[0];


            //var context = new whitmanenterprisewarehouseEntities();

            //int DID = Convert.ToInt32(txtDealerID.Text);

            // AutoService autoService = new AutoService();


            // foreach (var tmp in context.whitmanenterprisedealershipinventory.Where(x => x.DealershipId == DID).ToList())
            // {

            //     var vehicleInformation = autoService.getVehicleInformationFromVin(tmp.VINNumber);

            //     if (vehicleInformation != null)
            //     {

            //         try
            //         {

            //             if (vehicleInformation.vehicleSpecification.engines != null &&
            //                 vehicleInformation.vehicleSpecification.engines.Count() > 0)
            //             {
            //                 foreach (Engine er in vehicleInformation.vehicleSpecification.engines)
            //                 {
            //                     string fuelType = er.fuelType;
            //                     int index = fuelType.LastIndexOf(" ");

            //                     tmp.Cylinders = er.cylinders.ToString();
            //                     tmp.FuelType = fuelType.Substring(0, index);
            //                     tmp.Liters = er.displacementL.ToString();
            //                     tmp.EngineType = er.engineType;
            //                     break;
            //                 }
            //             }
            //         }
            //         catch (Exception)
            //         {
            //             tmp.Cylinders = "";
            //             tmp.FuelType = "";
            //             tmp.Liters = "";
            //             tmp.EngineType = "";
            //         }


            //         if (String.IsNullOrEmpty(tmp.Tranmission))
            //         {

            //             if (vehicleInformation.genericEquipment != null)
            //             {

            //                 foreach (GenericEquipment ge in vehicleInformation.genericEquipment)
            //                 {
            //                     if (ge.categoryId == 1130 || ge.categoryId == 1101 || ge.categoryId == 1102 ||
            //                         ge.categoryId == 1103 || ge.categoryId == 1104 || ge.categoryId == 1210 ||
            //                         ge.categoryId == 1220)
            //                     {

            //                         tmp.Tranmission = "Automatic";
            //                         break;
            //                     }
            //                     else if (ge.categoryId == 1131 || ge.categoryId == 1105 || ge.categoryId == 1106 ||
            //                              ge.categoryId == 1107 || ge.categoryId == 1108 || ge.categoryId == 1146 ||
            //                              ge.categoryId == 1147 || ge.categoryId == 1148)
            //                     {
            //                         tmp.Tranmission = "Manual";
            //                         break;
            //                     }

            //                 }
            //             }

            //         }
            //         else
            //         {
            //             tmp.Tranmission = "";
            //         }

            //         if (vehicleInformation.vehicleSpecification.engines != null &&
            //             vehicleInformation.vehicleSpecification.engines.Count() > 0)
            //         {

            //             foreach (Engine er in vehicleInformation.vehicleSpecification.engines)
            //             {
            //                 tmp.FuelEconomyCity = er.fuelEconomyCityValue.lowValue.ToString();
            //                 tmp.FuelEconomyHighWay = er.fuelEconomyHwyValue.lowValue.ToString();
            //                 break;

            //             }
            //         }

            //         StringBuilder builderOption = new StringBuilder();

            //         if (vehicleInformation != null && vehicleInformation.standards != null)
            //         {

            //             foreach (Standard sd in vehicleInformation.standards)
            //             {
            //                 if (!String.IsNullOrEmpty(sd.description) && sd.installed)
            //                 {
            //                     builderOption.Append(sd.description + ",");
            //                 }
            //             }
            //         }
            //         if (String.IsNullOrEmpty(builderOption.ToString()))
            //             tmp.StandardOptions = "";
            //         else
            //         {
            //             builderOption.Remove(builderOption.Length - 1, 1);
            //             tmp.StandardOptions = builderOption.ToString().Replace("\'", "\\'");
            //         }


            //         try
            //         {

            //             tmp.Doors = vehicleInformation.vehicleSpecification.numberOfPassengerDoors.ToString();
            //         }
            //         catch (Exception)
            //         {
            //             tmp.Doors = "";
            //         }

            //         try
            //         {

            //             tmp.DriveTrain = CommonHelper.getDriveXMLFileFromVehicleInfo(vehicleInformation);
            //         }
            //         catch (Exception)
            //         {
            //             tmp.DriveTrain = "";

            //         }



            //         context.SaveChanges();
            //     }

            // }

            //DataTable dtEmer = SQLHelper.GetInventoryForDealership(37695).Tables[0];

            //Hashtable hash = new Hashtable();

            //foreach (DataRow dr in dtEmer.Rows)
            //{
            //    hash.Add(dr.Field<string>("StockNumber"), dr.Field<string>("Vinnumber"));
            //}
            //foreach (DataRow dr in dtVinControlInventory.Rows)
            //{

            //    if (hash.Contains(dr.Field<string>("StockNumber")))
            //    {
            //        string Vinnumber = hash[dr.Field<string>("StockNumber")].ToString();
            //        SQLHelper.UpdateSalePrice(Vinnumber, 12293, dr.Field<string>("SalePrice"));
            //    }

            //}

            SQLHelper.InsertToInvetory(dtVinControlInventory);

            System.Windows.Forms.MessageBox.Show("DONE", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        private void btnPic_Click(object sender, EventArgs e)
        {

            int DID = Convert.ToInt32(txtDealerID.Text);

            dtVinControlInventory = SQLHelper.GetInventoryForDealership(DID).Tables[0];


            drgVinControl.DataSource = dtVinControlInventory;

            this.Refresh();

            WebClient request = new WebClient();

            int process = 1;

            var arrayList = dtVinControlInventory.AsEnumerable().ToList();

            int count = 0;


            foreach (DataRow dr in arrayList)
            {
                if (!String.IsNullOrEmpty(dr["CarImageUrl"].ToString()) &&
                    !dr["CarImageUrl"].ToString().Contains("vincontrol"))
                {

                    string vin = dr["Vinnumber"].ToString();

                    string ListingId = dr["ListingId"].ToString();

                    string physicalImagePath = "C://DealerImages" + "/" + DID + "/" + vin + "/NormalSizeImages";

                    string physicalImageThumbNailPath = "C://DealerImages" + "/" + DID + "/" + vin +
                                                        "/ThumbnailSizeImages";
                    ;

                    string carURL = "";
                    string thumnailURL = "";


                    string[] carImage = dr["CarImageUrl"].ToString().Split(new string[] {",", "|"},
                                                                           StringSplitOptions.RemoveEmptyEntries);

                    var dirNormal = new DirectoryInfo(physicalImagePath);

                    var dirThumbnail = new DirectoryInfo(physicalImageThumbNailPath);

                    if (!dirNormal.Exists)
                        dirNormal.Create();

                    if (!dirThumbnail.Exists)
                        dirThumbnail.Create();


                    foreach (string tmp in carImage)
                    {
                        string imageFileName = tmp.Substring(tmp.LastIndexOf("/") + 1);

                        try
                        {

                            byte[] byteStream = request.DownloadData(tmp);

                            using (FileStream fs = new FileStream(dirNormal + "/" + imageFileName, FileMode.Create))
                            {
                                fs.Write(byteStream, 0, byteStream.Length);

                            }

                            ImageResizer.ImageBuilder.Current.Build(dirNormal.FullName + "/" + imageFileName,
                                                                    dirThumbnail.FullName + "/" + imageFileName,
                                                                    new ImageResizer.ResizeSettings(
                                                                        "maxwidth=260&maxheight=260&format=jpg"));


                            string relativePath = "http://www.vincontrol.com/DealerImages/" + DID + "/" + vin +
                                                  "/NormalSizeImages/" + imageFileName + ",";

                            string relativeThumbnailPath = "http://www.vincontrol.com/DealerImages/" + DID + "/" + vin +
                                                           "/ThumbnailSizeImages/" + imageFileName + ",";

                            carURL += relativePath;

                            thumnailURL += relativeThumbnailPath;
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    lblCars.Text = process.ToString();

                    this.Refresh();

                    process++;


                    if (!String.IsNullOrEmpty(carURL) && carURL.Substring(carURL.Length - 1).Equals(","))
                        carURL = carURL.Substring(0, carURL.Length - 1);

                    if (!String.IsNullOrEmpty(thumnailURL) && thumnailURL.Substring(thumnailURL.Length - 1).Equals(","))
                        thumnailURL = thumnailURL.Substring(0, thumnailURL.Length - 1);

                    SQLHelper.UpdateCarImageURL(ListingId, carURL);

                    SQLHelper.UpdateCarImageThubmnailURL(ListingId, thumnailURL);


                }

            }
            System.Windows.Forms.MessageBox.Show("UploadDone", "Critical Error", MessageBoxButtons.OK,
                                                 MessageBoxIcon.Warning);
        }


        public static string UppercaseWords(string value)
        {
            value = value.ToLower();
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }

        private void button29_Click(object sender, EventArgs e)
        {
            var context = new whitmanenterprisewarehouseEntities();

            var list = context.whitmanenterprisedealershipinventory.Where(x => x.DealershipId == 1660).ToList();


            foreach (var tmp in list)
            {
                tmp.Make = UppercaseWords(tmp.Make);
                tmp.Model = UppercaseWords(tmp.Model);
            }
            context.SaveChanges();
            drgVinControl.DataSource = list;
        }

        private void button28_Click(object sender, EventArgs e)
        {

            var context = new whitmanenterprisewarehouseEntities();

            var list = context.whitmanenterprisedealershipinventory.Where(x => x.DealershipId == 37695).ToList();

            var listFeed =
                context.whitmanenterprisedealershipinventorysoldout.Where(x => x.DealershipId == 37695).ToList();
            foreach (var tmp in list)
            {
                if (listFeed.Any(x => x.VINNumber == tmp.VINNumber))
                {
                    var co = listFeed.First(x => x.VINNumber == tmp.VINNumber);
                    tmp.Descriptions = co.Descriptions;
                    //tmp.ThumbnailImageURL = co.ThumbnailImageURL;

                }
            }
            context.SaveChanges();
            drgVinControl.DataSource = list;
        }


        private void button36_Click(object sender, EventArgs e)
        {
            DataTable dt = AccountList();
            DataTable dtProxy = ProxyList();
            for (int y = 1; y <= 31; y++)
            {
                XmlWriter writer = writer = XmlWriter.Create(@"C:\XMLFile\" + y + ".xml");

                writer.Flush();

                writer.WriteStartDocument();

                writer.WriteStartElement("CraiglistAccounts");

                int indexMin = (y - 1)*5;
                int indexMax = (y*5);

                int tmp = 0;



                foreach (DataRow drRow in dt.Rows)
                {
                    if (!String.IsNullOrEmpty(drRow[2].ToString()))
                    {
                        if (tmp >= indexMin && tmp < indexMax)
                        {

                            writer.WriteStartElement("Account");
                            writer.WriteAttributeString("CraigslistAccount", drRow[0].ToString());
                            writer.WriteAttributeString("CraigsListPassword", drRow[1].ToString());
                            writer.WriteAttributeString("PhoneNumber",
                                                        "949-" + drRow[2].ToString().Substring(0, 3) + "-" +
                                                        drRow[2].ToString().Substring(3));
                            writer.WriteAttributeString("Proxy", dtProxy.Rows[y - 1][0].ToString());
                            writer.WriteEndElement();





                        }
                    }
                    tmp++;
                }

                for (int h = 0; h < 5; h++)
                {
                    writer.WriteStartElement("Account");
                    writer.WriteAttributeString("CraigslistAccount", "");
                    writer.WriteAttributeString("CraigsListPassword", "");
                    writer.WriteAttributeString("PhoneNumber", "");
                    writer.WriteAttributeString("Proxy", "");
                    writer.WriteEndElement();
                }
                writer.WriteEndElement(); //end of source tag
                writer.WriteEndDocument(); //end of source tag
                writer.Close();



                //txtDealerID.Text = (Convert.ToInt32(txtDealerID.Text) + 1).ToString();
            }
        }

        private DataTable AccountList()
        {
            string Path = "/Accounts.csv"; //10278,2074,23099,3636

            var ClappCredential =
                new NetworkCredential(
                    System.Configuration.ConfigurationManager.AppSettings["CLAPPFTPUsername"].ToString(),
                    System.Configuration.ConfigurationManager.AppSettings["CLAPPFTPPassword"].ToString());


            var csv = SQLHelper.getInventoryAutoTraderFormatWithoutHeader222(Path);

            var dtTemporaryNoheader = new DataTable();

            dtTemporaryNoheader.Load(csv);

            return dtTemporaryNoheader;
        }


        private DataTable ProxyList()
        {
            string Path = "/3738.txt"; //10278,2074,23099,3636

            var ClappCredential =
                new NetworkCredential(
                    System.Configuration.ConfigurationManager.AppSettings["CLAPPFTPUsername"].ToString(),
                    System.Configuration.ConfigurationManager.AppSettings["CLAPPFTPPassword"].ToString());


            var csv = SQLHelper.getInventoryAutoTraderFormatWithoutHeader222(Path);

            var dtTemporaryNoheader = new DataTable();

            dtTemporaryNoheader.Load(csv);

            return dtTemporaryNoheader;
        }



        private void Button4Click(object sender, EventArgs e)
        {
            DataTable dtTemporaryNoheader = null;

            DataTable dtDNS = null;

            int DID = Convert.ToInt32(txtDealerID.Text);

            var path = "/" + DID + ".txt";

            if (DID == 113738)
                path = "/3738" + ".txt";

            if (DID == 15896 || DID == 15986 || DID == 2650)
                path = "/15896" + ".txt";

            var clappCredential = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["CLAPPFTPUsername"].ToString(CultureInfo.InvariantCulture), System.Configuration.ConfigurationManager.AppSettings["CLAPPFTPPassword"].ToString(CultureInfo.InvariantCulture));

            CachedCsvReader csv = FTPHelper.GetInventoryAutoTraderFormatWithoutHeader(path, clappCredential);


            if (csv != null)
            {

                dtTemporaryNoheader = new DataTable();

                dtDNS = SQLHelper.InitialAutoTraderTable();

                dtTemporaryNoheader.Load(csv);
                //Condition

                foreach (DataRow drRow in dtTemporaryNoheader.Rows)
                {
                    if (DID == 44670)
                    {

                        DataRow dr = dtDNS.NewRow();

                        dr["DealerId"] = DID;

                        dr["StockNumber"] = drRow.Field<string>(1);

                        dr["Year"] = drRow.Field<string>(2);

                        dr["Make"] = drRow.Field<string>(3);

                        dr["Model"] = drRow.Field<string>(4);

                        dr["Trim"] = drRow.Field<string>(5);


                        dr["VIN"] = drRow.Field<string>(6);

                        dr["Mileage"] = drRow.Field<string>(7);


                        dr["Price"] = drRow.Field<string>(9);

                        dr["ExteriorColor"] = drRow.Field<string>(10);

                        dr["InteriorColor"] = drRow.Field<string>(11);

                        dr["Tranmission"] = drRow.Field<string>(12);

                        dr["PhysicalImages"] = drRow.Field<string>(20);


                        dr["Descriptions"] = drRow.Field<string>(14);

                        dr["BodyType"] = drRow.Field<string>(15);

                        dr["EngineType"] = drRow.Field<string>(16);

                        dr["DriveType"] = drRow.Field<string>(17);

                        dr["FuelType"] = drRow.Field<string>(18);

                        dr["Options"] = drRow.Field<string>(19);



                        if (dtTemporaryNoheader.Columns.Count > 20)
                        {

                            dr["ImageURL"] = drRow.Field<string>(20);

                            dr["VideoURL"] = drRow.Field<string>(21);

                            dr["VideoSource"] = drRow.Field<string>(22);
                        }
                        else
                        {
                            dr["ImageURL"] = "";

                            dr["VideoURL"] = "";
                            dr["VideoSource"] = "";
                        }

                        dtDNS.Rows.Add(dr);


                    }
                    else if (DID == 113738 || DID==15896)
                    {
                        int mileageReturn = 0;
                        Int32.TryParse(drRow[7].ToString(), out mileageReturn);
                        if (mileageReturn < 500)
                        {
                            DataRow dr = dtDNS.NewRow();

                            dr["DealerId"] = DID;

                            dr["StockNumber"] = drRow.Field<string>(1);

                            dr["Year"] = drRow.Field<string>(2);

                            dr["Make"] = drRow.Field<string>(3);

                            dr["Model"] = drRow.Field<string>(4);

                            dr["Trim"] = drRow.Field<string>(5);


                            dr["VIN"] = drRow.Field<string>(6);

                            dr["Mileage"] = drRow.Field<string>(7);


                            dr["Price"] = drRow.Field<string>(9);

                            dr["ExteriorColor"] = drRow.Field<string>(10);

                            dr["InteriorColor"] = drRow.Field<string>(11);

                            dr["Tranmission"] = drRow.Field<string>(12);

                            dr["PhysicalImages"] = drRow.Field<string>(20);


                            dr["Descriptions"] = drRow.Field<string>(14);

                            dr["BodyType"] = drRow.Field<string>(15);

                            dr["EngineType"] = drRow.Field<string>(16);

                            dr["DriveType"] = drRow.Field<string>(17);

                            dr["FuelType"] = drRow.Field<string>(18);

                            dr["Options"] = drRow.Field<string>(19);



                            if (dtTemporaryNoheader.Columns.Count > 20)
                            {

                                dr["ImageURL"] = drRow.Field<string>(20);

                                dr["VideoURL"] = drRow.Field<string>(21);

                                dr["VideoSource"] = drRow.Field<string>(22);
                            }
                            else
                            {
                                dr["ImageURL"] = "";

                                dr["VideoURL"] = "";
                                dr["VideoSource"] = "";
                            }

                            dtDNS.Rows.Add(dr);
                        }

                    }
                  
                  

                }
            }


            dataGridView1.DataSource = dtDNS;

            //var dtDNS = SQLHelper.InitialAutoTraderTable();

            //foreach (DataRow drRow in dtTemporaryNoheader.Rows)
            //{
            //    int mileageReturn = 0;
            //    Int32.TryParse(drRow[7].ToString(), out mileageReturn);
            //    if (mileageReturn < 200)
            //    {

            //        DataRow dr = dtDNS.NewRow();

            //        dr["DealerId"] = "3738";

            //        dr["StockNumber"] = drRow.Field<string>(1);

            //        dr["Year"] = drRow.Field<string>(2);

            //        dr["Make"] = drRow.Field<string>(3);

            //        dr["Model"] = drRow.Field<string>(4);

            //        dr["Trim"] = drRow.Field<string>(5);


            //        dr["VIN"] = drRow.Field<string>(6);

            //        dr["Mileage"] = drRow.Field<string>(7);


            //        dr["Price"] = drRow.Field<string>(9);

            //        dr["ExteriorColor"] = drRow.Field<string>(10);

            //        dr["InteriorColor"] = drRow.Field<string>(11);

            //        dr["Tranmission"] = drRow.Field<string>(12);

            //        dr["PhysicalImages"] = drRow.Field<string>(20);


            //        dr["Descriptions"] = drRow.Field<string>(14);

            //        dr["BodyType"] = drRow.Field<string>(15);

            //        dr["EngineType"] = drRow.Field<string>(16);

            //        dr["DriveType"] = drRow.Field<string>(17);

            //        dr["FuelType"] = drRow.Field<string>(18);

            //        dr["Options"] = drRow.Field<string>(19);



            //        if (dtTemporaryNoheader.Columns.Count > 20)
            //        {

            //            dr["ImageURL"] = drRow.Field<string>(20);

            //            dr["VideoURL"] = drRow.Field<string>(21);

            //            dr["VideoSource"] = drRow.Field<string>(22);
            //        }
            //        else
            //        {
            //            dr["ImageURL"] = "";

            //            dr["VideoURL"] = "";
            //            dr["VideoSource"] = "";
            //        }

            //        dtDNS.Rows.Add(dr);
            //    }
            //}

            //dataGridView1.DataSource = dtDNS;


            //MessageBox.Show(dtTemporaryNoheader.Rows[0].Field<string>(7));
            //var context = new vincontrolscrappingEntities();

            //foreach (DataRow drRow in dtTemporaryNoheader.Rows)
            //{
            //    var rowEntry = new proxylist()
            //                       {
            //                           ProxyIP = drRow[0].ToString()
            //                       };

            //    context.AddToproxylist(rowEntry);
            //}
            //context.SaveChanges();
            //var autoList = new List<AutoTraderVehicle>();

            //foreach (DataRow drRow in dtTemporaryNoheader.Rows)
            //{
            //    var vehicle = new AutoTraderVehicle()
            //                      {
            //                          VIN = drRow[6].ToString(),
            //                          PhysicalImages = drRow[20].ToString(),
            //                      };

            //    autoList.Add(vehicle);
            //}
            ////drgVinControl.DataSource = autoList;
            //var context = new whitmanenterprisewarehouseEntities();



            //var list = context.whitmanenterprisedealershipinventory.Where(x => x.DealershipId == 3636 && x.NewUsed == "New").ToList();



            //foreach (var tmp in list)
            //{
            //    if (String.IsNullOrEmpty(tmp.CarImageUrl))
            //    {
            //        if (autoList.Any(x => x.VIN == tmp.VINNumber))
            //        {
            //            tmp.CarImageUrl = autoList.First(x => x.VIN == tmp.VINNumber).PhysicalImages.Replace(" ", ",");

            //            tmp.ThumbnailImageURL = autoList.First(x => x.VIN == tmp.VINNumber).PhysicalImages.Replace(" ",
            //                                                                                                       ",");
            //        }
            //    }

            //}

            //context.SaveChanges();
            //drgVinControl.DataSource = list;


            //foreach (var tmp in list)
            //{
            //    if (listFeed.Any(x => x.VIN == tmp.VINNumber))
            //    {
            //        var co = listFeed.First(x => x.VIN == tmp.VINNumber);
            //        tmp.SalePrice = co.SalePrice;
            //        //tmp.TruckClass = co.TruckClass;
            //        //tmp.TruckType = co.TruckType;
            //        //tmp.TruckCategory = co.TruckCategory;
            //    }
            //    else
            //    {
            //        context.Attach(tmp);
            //        context.DeleteObject(tmp);
            //    }
            //}



            //context.SaveChanges();
            // drgVinControl.DataSource = dtTemporaryNoheader;


            //foreach (var tmp in list)
            //{
            //    if(dtTemporaryNoheader.Any(x=>x.V))
            //}
            //DataTable dtTemporaryNoheader = new DataTable();

            ////dtTemporaryNoheader = ExcelHelper.PopulateDataTableFromTextFile(Path,ClappCredential);

            //if (csv != null)
            //{
            //    dtTemporaryNoheader = new DataTable();

            //    dtTemporaryNoheader.Load(csv);


            //}

            ////txtDealerID.Text = dtTemporaryNoheader.Columns.Count.ToString();

            //dRgDealer.DataSource = dtTemporaryNoheader;

            //dtAutoTrader = SQLHelper.initialAutoTraderTable();


            //foreach (DataRow drRow in dtTemporaryNoheader.Rows)
            //{

            //    DataRow dr = dtAutoTrader.NewRow();


            //    dr["DealerId"] =drRow.Field<string>(0);

            //    dr["StockNumber"] = drRow.Field<string>(1);

            //    dr["Year"] = drRow.Field<string>(2);

            //    dr["Make"] = drRow.Field<string>(3);

            //    dr["Model"] = drRow.Field<string>(4);

            //    dr["Trim"] = drRow.Field<string>(5);


            //    dr["VIN"] = drRow.Field<string>(6);

            //    dr["Mileage"] = drRow.Field<string>(7);

            //    dr["Price"] = drRow.Field<string>(8);

            //    dr["ExteriorColor"] = drRow.Field<string>(9);

            //    dr["InteriorColor"] = drRow.Field<string>(10);

            //    dr["Tranmission"] = drRow.Field<string>(11);

            //    dr["PhysicalImages"] = drRow.Field<string>(12);

            //    dr["Description"] = drRow.Field<string>(13);

            //    dr["BodyType"] = drRow.Field<string>(14);

            //    dr["EngineType"] = drRow.Field<string>(15);

            //    dr["DriveType"] = drRow.Field<string>(16);

            //    dr["FuelType"] = drRow.Field<string>(17);

            //    dr["Options"] = drRow.Field<string>(18);

            //    if (dtTemporaryNoheader.Columns.Count > 19)
            //    {

            //        dr["ImageURL"] = drRow.Field<string>(19);

            //        dr["VideoURL"] = drRow.Field<string>(20);

            //        dr["VideoSource"] = drRow.Field<string>(21);
            //    }
            //    else
            //    {
            //         dr["ImageURL"] ="";

            //        dr["VideoURL"] = "";
            //        dr["VideoSource"] = "";
            //    }

            //    dtAutoTrader.Rows.Add(dr);
            //}

            //drgAutoTrader.DataSource = dtAutoTrader;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            DataSet ds = SQLHelper.GetInventoryForDealership(37695);

            dtVinControlInventory = ds.Tables[0];

            drgVinControl.DataSource = dtVinControlInventory;


        }

        private void button9_Click(object sender, EventArgs e)
        {

            dtVinControlInventory = SQLHelper.GetInventoryForDealership(2183).Tables[0];

            AutoService autoService = new AutoService();

            var regex = new Regex(@"(?<=\w)\w", RegexOptions.Compiled);

            foreach (DataRow dr in dtVinControlInventory.Rows)
            {

                if (String.IsNullOrEmpty(dr["StandardOptions"].ToString()))
                {

                    string ListingId = dr["ListingId"].ToString();

                    string vin = dr["Vinnumber"].ToString();

                    VehicleInformation vehicleInformation = autoService.getVehicleInformationFromVin(vin);
                    if (vehicleInformation != null)
                    {

                        if (vehicleInformation.standards != null && vehicleInformation.standards.Count() > 0)
                        {

                            Hashtable hash = new Hashtable();

                            string standardOption = "";

                            foreach (Standard fo in vehicleInformation.standards)
                            {

                                string name = fo.description;

                                if (!hash.Contains(name) && fo.installed)
                                {
                                    string newstring = regex.Replace(name,
                                                                     new MatchEvaluator(m => m.Value.ToLowerInvariant()));

                                    standardOption += newstring + ",";

                                    hash.Add(name, name);

                                }
                            }

                            if (!String.IsNullOrEmpty(standardOption))
                            {
                                standardOption = standardOption.Substring(0, standardOption.Length - 1);
                                SQLHelper.UpdateStandardOption(Convert.ToInt32(ListingId), standardOption);
                            }
                        }




                    }
                }

            }

            System.Windows.Forms.MessageBox.Show("UpdateDone", "Critical Error", MessageBoxButtons.OK,
                                                 MessageBoxIcon.Warning);
        }



        private void button6_Click(object sender, EventArgs e)
        {
            var arrayList = dtVinControlInventory.AsEnumerable().ToList();

            foreach (DataRow dr in arrayList)
            {


                string vin = dr["Vinnumber"].ToString();

                string ListingId = dr["ListingId"].ToString();

                if (dr["CarImageUrl"] != null && !String.IsNullOrEmpty(dr["CarImageUrl"].ToString()))
                {

                    string[] carImage = dr["CarImageUrl"].ToString().Split(new string[] {",", "|"},
                                                                           StringSplitOptions.RemoveEmptyEntries);

                    //                        string thumnailURL = dr["ThumbnailImageURL"].ToString().Replace("NormalSizeImages", "ThumbnailSizeImages");


                    string carURL = "";
                    string thumnailURL = "";



                    if (carImage != null)
                    {

                        foreach (string tmp in carImage)
                        {

                            string imageFileName = tmp.Substring(tmp.LastIndexOf("/") + 1);

                            string relativePath = "http://www.vincontrol.com/DealerImages/1541/" + vin +
                                                  "/NormalSizeImages/" + imageFileName + ",";

                            string relativeThumbnailPath = "http://www.vincontrol.com/DealerImages/1541/" + vin +
                                                           "/ThumbnailSizeImages/" + imageFileName + ",";

                            carURL += relativePath;

                            thumnailURL += relativeThumbnailPath;


                        }
                    }

                    if (carURL.Substring(carURL.Length - 1).Equals(","))
                        carURL = carURL.Substring(0, carURL.Length - 1);

                    if (thumnailURL.Substring(thumnailURL.Length - 1).Equals(","))
                        thumnailURL = thumnailURL.Substring(0, thumnailURL.Length - 1);

                    SQLHelper.UpdateCarImageURL(ListingId, carURL);

                    SQLHelper.UpdateCarImageThubmnailURL(ListingId, thumnailURL);
                }



                //foreach (string tmp in carImage)
                //{


                //    byte[] byteStream = request.DownloadData(tmp);

                //    Stream imageStream = new MemoryStream(byteStream);

                //    string path = "NewportCoastAuto" + "/" + dr["vin"].ToString();

                //    string imageFileName = dr["stocknumber"].ToString() + "-" + count.ToString() + ".jpg";

                //    BasicFTPClient MyClient = new BasicFTPClient("iPage");

                //    MyClient.connect();

                //    MyClient.Upload(imageStream, path, imageFileName);

                //    MyClient.closeConnect();

                //    transferImage = "http://vinanalysis.com/" + path + "/" + imageFileName + ",";

                //    count++;
                //}

                //transferImage = transferImage.Substring(0, transferImage.Length - 1);

                //hash.Add(dr["stocknumber"].ToString(), transferImage);

                //lblCars.Text = process.ToString();

                //this.Refresh();

                //process++;

            }

            System.Windows.Forms.MessageBox.Show("UpdateDone", "Critical Error", MessageBoxButtons.OK,
                                                 MessageBoxIcon.Warning);
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            DataTable dt = SQLHelper.GetDataTableFromMySQL("MercedesBenzStoredProcedure");

            foreach (DataRow dr in dt.Rows)
            {
                int ListingId = dr.Field<int>("ListingId");

                string VinNumber = dr.Field<string>("VINNumber");

                AutoService autoService = new AutoService();

                VehicleInformation vehicleInfo = autoService.getVehicleInformationFromVin(VinNumber);

                if (vehicleInfo != null)
                {

                    if (vehicleInfo.styles != null && vehicleInfo.styles.Count() > 0)
                    {
                        string Trim = String.IsNullOrEmpty(vehicleInfo.styles.First().consumerFriendlyModelName)
                                          ? vehicleInfo.vinTrimName
                                          : vehicleInfo.styles.First().consumerFriendlyModelName;

                        SQLHelper.updateTrim(ListingId, Trim);

                    }


                }



            }

            System.Windows.Forms.MessageBox.Show("UpDateDone", "Critical Error", MessageBoxButtons.OK,
                                                 MessageBoxIcon.Warning);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SQLHelper.getInventoryFor23680("23680.txt");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            using (var context = new whitmanenterprisewarehouseEntities())
            {
                var firstTmp = context.whitmanenteprisecarfax.FirstOrDefault(o => o.Vin.Equals("1G6YV34A145603883"));

                DateTime dt = DateTime.Parse(firstTmp.ExpiredDate.ToString());
                //DateTime dt = DateTime.Parse("2012-04-09 14:54:05");

                if (dt.Date >= DateTime.Now.Date)
                    MessageBox.Show("Greater");
                else
                    MessageBox.Show("Samller");
            }
        }

        //private void button11_Click(object sender, EventArgs e)
        //{
        //    FTPConnection ftpConnection1 = new FTPConnection();

        //    ftpConnection1.ServerAddress = "74.124.200.50";
        //    ftpConnection1.ServerPort = 21;
        //    ftpConnection1.UserName = "37695ie@vehicleinventorynetwork.com";
        //    ftpConnection1.Password = "jeff1451";


        //    ftpConnection1.Connect();


        //    listBox1.Items.AddRange(ftpConnection1.GetFiles());
        //}

        private void button12_Click(object sender, EventArgs e)
        {
            SQLHelper.InsertToInvetory(dtVinControlInventory);

            //SQLHelper.InsertToInvetoryFprHornBurg(dtVinControlInventory);

            System.Windows.Forms.MessageBox.Show("DONE", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int DID = Convert.ToInt32(txtDealerID.Text);

            dtVinControlInventory = SQLHelper.GetInventoryForDealershipCL(DID).Tables[0];


            drgVinControl.DataSource = dtVinControlInventory;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            // Get a list of all network interfaces (usually one per network card, dialup, and VPN connection) 
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface network in networkInterfaces)
            {

                // Read the IP configuration for each network 
                IPInterfaceProperties properties = network.GetIPProperties();

                // Each network interface may have multiple IP addresses 
                foreach (IPAddressInformation address in properties.UnicastAddresses)
                {
                    // We're only interested in IPv4 addresses for now 

                    if (address.Address.AddressFamily != AddressFamily.InterNetwork)
                        continue;

                    // Ignore loopback addresses (e.g., 127.0.0.1) 
                    if (IPAddress.IsLoopback(address.Address))
                        continue;


                    sb.AppendLine(address.Address.ToString() + " (" + network.GetPhysicalAddress() + ")");
                }
            }

            MessageBox.Show(sb.ToString());
        }

        private void button15_Click(object sender, EventArgs e)
        {
            using (var context = new whitmanenterprisewarehouseEntities())
            {
                var result =
                    context.whitmanenterprisedealershipinventorysoldout.Where(
                        x => String.IsNullOrEmpty(x.FuelEconomyCity)).ToList();


                AutoService autoService = new AutoService();

                foreach (var tmp in result)
                {
                    VehicleInformation vehicleInfo = autoService.getVehicleInformationFromVin(tmp.VINNumber);

                    if (vehicleInfo != null)
                    {
                        if (vehicleInfo.vehicleSpecification != null)
                        {
                            if (vehicleInfo.vehicleSpecification.engines != null &&
                                vehicleInfo.vehicleSpecification.engines.Count() > 0)
                            {

                                foreach (Engine er in vehicleInfo.vehicleSpecification.engines)
                                {
                                    tmp.FuelEconomyCity = er.fuelEconomyCityValue.lowValue.ToString();
                                    tmp.FuelEconomyHighWay = er.fuelEconomyHwyValue.lowValue.ToString();
                                    break;

                                }
                            }
                            else
                            {
                                tmp.FuelEconomyCity = "";
                                tmp.FuelEconomyHighWay = "";
                            }


                        }
                        else
                        {
                            tmp.FuelEconomyCity = "";
                            tmp.FuelEconomyHighWay = "";
                        }

                    }
                    else
                    {
                        tmp.FuelEconomyCity = "";
                        tmp.FuelEconomyHighWay = "";
                    }

                    context.SaveChanges();
                }

            }
            System.Windows.Forms.MessageBox.Show("DONE", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void button16_Click(object sender, EventArgs e)
        {
            //var context = new whitmanenterprisewarehouseEntities();

            //var result = context.whitmanenterprisezipcodes.ToList();

            //var contextGoDaddy = new GoDaddy.whitmanenterprisewarehouseEntities1();

            //foreach (var tmp in result)
            //{
            //    GoDaddy.whitmanenterprisezipcodes zip = new DataFeedsVinControlConversion.GoDaddy.whitmanenterprisezipcodes()
            //    {
            //        zipcode=tmp.zipcode,
            //        City=tmp.City,
            //        Class=tmp.Class,
            //        latitude=tmp.latitude,
            //        longitude=tmp.longitude,
            //        StateCode=tmp.StateCode,
            //        ZipCodesInDistance100=tmp.ZipCodesInDistance100
            //    };


            //    contextGoDaddy.AddTowhitmanenterprisezipcodes(zip);
            //    contextGoDaddy.SaveChanges();
            //}


            //System.Windows.Forms.MessageBox.Show("DONE", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button17_Click(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {
            //var context = new whitmanenterprisewarehouseEntities();

            //var result = context.whitmanenterpriseusazipcode.Where(x => x.StateAbbr == "CA").ToList();

            //var builderCity = new StringBuilder();

            //var builderZipCode = new StringBuilder();

            //var hashset = new HashSet<string>();

            //foreach (var tmp in result)
            //{
            //    if (CommonHelper.getDistance("33.525540", "-117.714360", tmp.Latitude, tmp.Longitude) <= 25)
            //    {
            //        if (!hashset.Contains(tmp.CityName))

            //            builderCity.Append(tmp.CityName + ",");

            //        builderZipCode.Append(tmp.ZIPCode + ",");

            //        hashset.Add(tmp.CityName);
            //    }
            //}
            //richTextBox1.Text = builderCity.ToString();
            //richTextBox2.Text = builderZipCode.ToString();

        }


        private IEnumerable<whitmanenterprisedealershipinventory> vincontrolist = null;



        private void button19_Click(object sender, EventArgs e)
        {
            //var contextscarpping = new vincontrolscrappingEntities();

            //var context = new whitmanenterprisewarehouseEntities();

            //int DID = Convert.ToInt32(txtDealerID.Text);

            //foreach (
            //    var tmp in context.whitmanenterprisedealershipinventory.Where(x => x.DealershipId == DID).AsEnumerable()
            //    )
            //{
            //    //if (string.IsNullOrEmpty(tmp.MarketRange.ToString()))
            //    //{

            //    var hash = new Hashtable();

            //    string uniqueString = tmp.ModelYear + tmp.Make + tmp.Model + tmp.Trim;


            //    if (!hash.Contains(uniqueString))
            //    {

            //        string ModelTrim = tmp.Model + tmp.Trim;

            //        var markerResult = contextscarpping.californiazone1.Where(x => x.Year == tmp.ModelYear &&
            //                                                                       x.Make.Equals(tmp.Make,
            //                                                                                     StringComparison.
            //                                                                                         InvariantCultureIgnoreCase) &&
            //                                                                       (ModelTrim.ToLower().Contains(
            //                                                                           x.Model)) &&
            //                                                                       !x.CurrentPrice.Equals("0")).
            //            AsEnumerable();


            //        var listPrice = new List<decimal>();

            //        foreach (var market in markerResult)
            //        {
            //            decimal price = 0;

            //            //CHECK LATER
            //            bool flagtmp = Decimal.TryParse(market.CurrentPrice, out price);

            //            if (flagtmp)
            //                listPrice.Add(price);


            //        }

            //        decimal SalePrice = 0;

            //        bool flagSalePrice = Decimal.TryParse(tmp.SalePrice, out SalePrice);

            //        if (listPrice.Any() && flagSalePrice)

            //            tmp.MarketRange = marketRange(SalePrice, Math.Round(listPrice.Average()));
            //        else
            //            tmp.MarketRange = 0;

            //        hash.Add(uniqueString, tmp.MarketRange);



            //    }
            //    else
            //    {

            //        tmp.MarketRange = (int) hash[uniqueString];
            //    }



            //    //}
            //}

            //context.SaveChanges();
            System.Windows.Forms.MessageBox.Show("DONE", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            var context = new whitmanenterprisewarehouseEntities();

            int DID = Convert.ToInt32(txtDealerID.Text);

            vincontrolist =
                context.whitmanenterprisedealershipinventory.Where(x => x.DealershipId == DID).AsEnumerable();

            dRgDealer.DataSource = vincontrolist;


        }



        public int marketRange(decimal price, decimal averagePrice)
        {
            decimal aboveMarketLowestPoint = averagePrice*Convert.ToDecimal(1.1);

            decimal belowMarketHighestPoint = averagePrice*Convert.ToDecimal(0.9);

            if (price >= aboveMarketLowestPoint)
                return 1;
            else if (price < aboveMarketLowestPoint && price > belowMarketHighestPoint)
                return 0;
            else
                return -1;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            FileInfo newFile = new FileInfo(@"C:\DataConversion\Template.xlsx");
            if (newFile.Exists)
            {
                newFile.Delete(); // ensures we create a new workbook
                newFile = new FileInfo(@"C:\DataConversion\Template.xlsx");
            }
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("KPI Report");
                //Add the headers
                worksheet.Cells[1, 1].Value = "Year";
                worksheet.Cells[1, 2].Value = "Make";
                worksheet.Cells[1, 3].Value = "Model";
                worksheet.Cells[1, 4].Value = "Stock#";
                worksheet.Cells[1, 5].Value = "Vin";
                worksheet.Cells[1, 6].Value = "Mileage";
                worksheet.Cells[1, 7].Value = "Color";
                worksheet.Cells[1, 8].Value = "Price";
                worksheet.Cells[1, 9].Value = "Avg(Days)";
                worksheet.Cells[1, 10].Value = "Pics";


                //Set column width
                worksheet.Column(1).Width = 10;
                worksheet.Column(2).Width = 10;
                worksheet.Column(3).Width = 10;
                worksheet.Column(4).Width = 15;
                worksheet.Column(5).Width = 25;
                worksheet.Column(6).Width = 25;
                worksheet.Column(7).Width = 25;
                worksheet.Column(8).Width = 15;
                worksheet.Column(9).Width = 15;
                worksheet.Column(10).Width = 15;


                //Set NumberFormat
                //worksheet.Column(6).Style.Numberformat.Format = "$#,##0.00";



                using (ExcelRange row = worksheet.Cells["A1:J1"])
                {
                    row.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    row.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(23, 55, 93));
                    row.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    row.Style.Font.Size = 12;
                    row.Style.Font.Bold = true;
                }

                //Switch the PageLayoutView back to normal
                //worksheet.View.PageLayoutView = true;

                // lets set the header text 
                worksheet.HeaderFooter.oddHeader.CenteredText = "&24&U&\"Arial,Regular Bold\" KPI Report";
                // add the page number to the footer plus the total number of pages
                worksheet.HeaderFooter.oddFooter.RightAlignedText =
                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);
                // add the sheet name to the footer
                worksheet.HeaderFooter.oddFooter.CenteredText = ExcelHeaderFooter.SheetName;
                // add the file path to the footer
                worksheet.HeaderFooter.oddFooter.LeftAlignedText = ExcelHeaderFooter.FilePath +
                                                                   ExcelHeaderFooter.FileName;

                worksheet.PrinterSettings.RepeatRows = worksheet.Cells["1:2"];
                worksheet.PrinterSettings.RepeatColumns = worksheet.Cells["A:G"];

                package.Workbook.Properties.Title = "KPI Report";

                package.Save();

            }

        }

        private void button22_Click(object sender, EventArgs e)
        {
            //var context = new whitmanenterprisewarehouseEntities();

            var contextCL = new whitmanenterprisecraigslistEntities();

            int DID = Convert.ToInt32(txtDealerID.Text);

            var tttt =
                contextCL.whitmanenterprisecraigslistinventory.Where(
                    x => x.DealershipId == DID ).ToList();

            //vincontrolist = context.whitmanenterprisedealershipinventory.ToList();

            // var tttt = contextCL.whitmanenterprisecraigslistinventory.ToList();

            drgVinControl.DataSource = tttt;

            this.Refresh();

            var autoService = new AutoService();
            foreach (var tmp in tttt)
            {

                var vehicleInformation = autoService.getVehicleInformationFromVin(tmp.VINNumber);

                if (vehicleInformation != null)
                {
                    tmp.Model = vehicleInformation.vinModelName;
                    //if (vehicleInformation.vehicleSpecification.bodyTypes != null && vehicleInformation.vehicleSpecification.bodyTypes.Any())
                    //{


                    //    tmp.BodyType = vehicleInformation.vehicleSpecification.bodyTypes.First(x => x.primary).bodyTypeName;
                    //    if (vehicleInformation.vehicleSpecification.bodyTypes.Any(x => !x.primary))
                    //    {
                    //        var btmp =
                    //            vehicleInformation.vehicleSpecification.bodyTypes.First(x => !x.primary);
                    //        if (tmp.BodyType.Equals("2dr Car"))
                    //        {
                    //            if (btmp.bodyTypeName.Contains("Convertible"))
                    //                tmp.BodyType = btmp.bodyTypeName;
                    //            else
                    //            {
                    //                tmp.BodyType = "Coupe";
                    //            }
                    //        }


                    //    }
                    //    else
                    //    {
                    //        if (tmp.BodyType.Equals("2dr Car"))
                    //        {

                    //                tmp.BodyType = "Coupe";

                    //        }

                    //    }

                    //}
                    //else
                    //{
                    //    tmp.BodyType = "";
                    //}

                    //try
                    //{

                    //    if (vehicleInformation.vehicleSpecification.engines != null &&
                    //        vehicleInformation.vehicleSpecification.engines.Count() > 0)
                    //    {
                    //        foreach (Engine er in vehicleInformation.vehicleSpecification.engines)
                    //        {
                    //            string fuelType = er.fuelType;
                    //            int index = fuelType.LastIndexOf(" ");

                    //            tmp.Cylinders = er.cylinders.ToString();
                    //            tmp.FuelType = fuelType.Substring(0, index);
                    //            tmp.Liters = er.displacementL.ToString();
                    //            tmp.EngineType = er.engineType;
                    //            break;
                    //        }
                    //    }
                    //}
                    //catch (Exception)
                    //{
                    //    tmp.Cylinders = "";
                    //    tmp.FuelType = "";
                    //    tmp.Liters = "";
                    //    tmp.EngineType = "";
                    //}


                    //if (String.IsNullOrEmpty(tmp.Tranmission))
                    //{

                    //    if (vehicleInformation.genericEquipment != null)
                    //    {

                    //        foreach (GenericEquipment ge in vehicleInformation.genericEquipment)
                    //        {
                    //            if (ge.categoryId == 1130 || ge.categoryId == 1101 || ge.categoryId == 1102 ||
                    //                ge.categoryId == 1103 || ge.categoryId == 1104 || ge.categoryId == 1210 ||
                    //                ge.categoryId == 1220)
                    //            {

                    //                tmp.Tranmission = "Automatic";
                    //                break;
                    //            }
                    //            else if (ge.categoryId == 1131 || ge.categoryId == 1105 || ge.categoryId == 1106 ||
                    //                     ge.categoryId == 1107 || ge.categoryId == 1108 || ge.categoryId == 1146 ||
                    //                     ge.categoryId == 1147 || ge.categoryId == 1148)
                    //            {
                    //                tmp.Tranmission = "Manual";
                    //                break;
                    //            }

                    //        }
                    //    }

                    //}
                    //else
                    //{
                    //    tmp.Tranmission = "";
                    //}

                    //if (vehicleInformation.vehicleSpecification.engines != null &&
                    //    vehicleInformation.vehicleSpecification.engines.Any())
                    //{

                    //    foreach (Engine er in vehicleInformation.vehicleSpecification.engines)
                    //    {
                    //        tmp.FuelEconomyCity = er.fuelEconomyCityValue.lowValue.ToString(CultureInfo.InvariantCulture);
                    //        tmp.FuelEconomyHighWay = er.fuelEconomyHwyValue.lowValue.ToString(CultureInfo.InvariantCulture);
                    //        break;

                    //    }
                    //}

                    //var builderOption = new StringBuilder();

                    //if (vehicleInformation.genericEquipment != null && vehicleInformation.genericEquipment.Any())
                    //{

                    //    foreach (var fo in vehicleInformation.genericEquipment.Where(x => x.installed))
                    //    {

                    //        builderOption.Append(fo.description + ",");
                    //    }


                    //    if (!String.IsNullOrEmpty(builderOption.ToString()))

                    //        builderOption.Remove(builderOption.Length - 1, 1);

                    //    tmp.StandardOptions = builderOption.ToString();

                    //}




                    //try
                    //{

                    //    tmp.Doors = vehicleInformation.vehicleSpecification.numberOfPassengerDoors.ToString();
                    //}
                    //catch (Exception)
                    //{
                    //    tmp.Doors = "";
                    //}

                    //try
                    //{

                    //    tmp.DriveTrain = CommonHelper.getDriveXMLFileFromVehicleInfo(vehicleInformation);
                    //}
                    //catch (Exception)
                    //{
                    //    tmp.DriveTrain = "";

                    //}


                    //if (vehicleInformation.stockPhotos != null && vehicleInformation.stockPhotos.Any())
                    //{
                    //    tmp.DefaultImageUrl = vehicleInformation.stockPhotos.First().url;
                    //    //tmp.ThumbnailImageURL = vehicleInformation.stockPhotos.First().url;
                    //    //tmp.CarImageUrl = vehicleInformation.stockPhotos.First().url;
                    //}




                }

                contextCL.SaveChanges();
                // contextCL.SaveChanges();

            }


           
        }

        private void BtnCarafxClick(object sender, EventArgs e)
        {
            int numberofowner = CarFaxHelper.RunCarFaxAndSaveOwner("3D4PG4FBXAT126688");

            //var context = new whitmanenterprisewarehouseEntities();

            //int DID = Convert.ToInt32(txtDealerID.Text);

            //var result =
            //    context.whitmanenterprisedealershipinventory.Where(x => x.DealershipId == DID && x.NewUsed == "Used").
            //        ToList();

            //drgVinControl.DataSource = vincontrolist;

            //this.Refresh();

            //foreach (var tmp in result)
            //{
            //    if (tmp.CarFaxOwner < 1)
            //    {
            //        int numberofowner = CarFaxHelper.RunCarFaxAndSaveOwner(tmp.VINNumber);

            //        if (numberofowner == 0)
            //            tmp.CarFaxOwner = -1;
            //        else

            //            tmp.CarFaxOwner = numberofowner;
            //    }



            //}

            //context.SaveChanges();

            System.Windows.Forms.MessageBox.Show(numberofowner.ToString(), "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void button23_Click(object sender, EventArgs e)
        {
            var context = new whitmanenterprisewarehouseEntities();

            var contextCl = new whitmanenterprisecraigslistEntities();

            foreach (var tmp in context.vincontrolexportlist.ToList())
            {
                string DID = tmp.DealerId.ToString();

                var search = contextCl.whitmanenterprisedealerlist.FirstOrDefault(x => x.VincontrolId.Equals(DID));

                tmp.Name = search.DealershipName;

                tmp.Address = search.StreetAddress;

                tmp.City = search.City;

                tmp.State = search.State;

                tmp.ZipCode = search.ZipCode;

                tmp.Email = search.Email;

                tmp.InventoryURL = search.WebSiteURL;

                tmp.Phone = search.PhoneNumber;
            }

            context.SaveChanges();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            string link =
                "tracktype=usedcc&amp;csDlId=&amp;csDgId=&amp;listingId=91613627&amp;listingRecNum=8&amp;criteria=sf1Dir%3DDESC%26stkTyp%3DU%26crSrtFlds%3DstkTypId-feedSegId%26rn%3D0%26PMmt%3D0-0-0%26stkTypId%3D28881%26sf1Nm%3Dprice%26isDealerGrouping%3Dfalse%26rpp%3D250%26feedSegId%3D28705%26dlId%3D417987&amp;aff=national&amp;listType=1";

            if (!String.IsNullOrEmpty(link) && link.Contains("listingId"))
            {
                string tmp = link.Substring(link.IndexOf("listingId"));

                var index2 = tmp.IndexOf("&");

                tmp = tmp.Substring(0, index2);

                txtDealerID.Text = "http://www.cars.com/go/search/detail.jsp?" + tmp;


            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            var context = new whitmanenterprisecraigslistEntities();

            var hashSet = new HashSet<string>();

           var result = context.whitmanenterprisecraigslistinventory.ToList();



            foreach (var tmp in result)
            {
                if (!hashSet.Contains(tmp.VINNumber))
                    hashSet.Add(tmp.VINNumber);
                else
                {
                    //var result = context.whitmanenterprisedealershipinventory.FirstOrDefault(x => x.ListingID == vehicle.ListingId);
                    context.Attach(tmp);
                    context.DeleteObject(tmp);
                }

            }

            context.SaveChanges();
            System.Windows.Forms.MessageBox.Show("DONE", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);



        }

        private void button26_Click(object sender, EventArgs e)
        {

        }

        private void button27_Click(object sender, EventArgs e)
        {
            var context = new whitmanenterprisewarehouseEntities();

            vincontrolist = context.whitmanenterprisedealershipinventory.Where(x => x.DealershipId == 29713).ToList();



            var autoService = new AutoService();
            foreach (var tmp in vincontrolist)
            {


                var vehicleInformation = autoService.getVehicleInformationFromVin(tmp.VINNumber);

                if (vehicleInformation != null)
                {
                    tmp.Model = vehicleInformation.vinModelName;



                    tmp.MSRP =
                        CommonHelper.RemoveSpecialCharactersForMSRP(vehicleInformation.baseMsrp.highValue.ToString());


                    //tmp.Make = vehicleInformation.vinMakeName;

                    //tmp.Trim = vehicleInformation.vinTrimName;

                    //if (String.IsNullOrEmpty(vehicleInformation.vinTrimName))
                    //{
                    //    foreach (Style s in vehicleInformation.styles)
                    //    {
                    //        tmp.Trim = s.consumerFriendlyModelName;
                    //        break;
                    //    }
                    //}


                    //tmp.ModelYear = vehicleInformation.modelYear;


                    if (vehicleInformation.vehicleSpecification.engines != null &&
                        vehicleInformation.vehicleSpecification.engines.Any())
                    {
                        foreach (Engine er in vehicleInformation.vehicleSpecification.engines)
                        {
                            //string fuelType = er.fuelType;
                            //int index = fuelType.LastIndexOf(" ");

                            //tmp.Cylinders = er.cylinders.ToString();
                            //tmp.FuelType = fuelType.Substring(0, index);
                            //tmp.Liters = er.displacementL.ToString();

                            //tmp.FuelEconomyCity = er.fuelEconomyCityValue.lowValue.ToString();
                            //tmp.FuelEconomyHighWay = er.fuelEconomyHwyValue.lowValue.ToString();

                            tmp.EngineType = er.engineType;
                            break;
                        }
                    }


                    //if (String.IsNullOrEmpty(tmp.Tranmission))
                    //{

                    //    if (vehicleInformation.genericEquipment != null)
                    //    {

                    //        foreach (GenericEquipment ge in vehicleInformation.genericEquipment)
                    //        {
                    //            if (ge.categoryId == 1130 || ge.categoryId == 1101 || ge.categoryId == 1102 ||
                    //                ge.categoryId == 1103 || ge.categoryId == 1104 || ge.categoryId == 1210 ||
                    //                ge.categoryId == 1220)
                    //            {

                    //                tmp.Tranmission = "Automatic";
                    //                break;
                    //            }
                    //            else if (ge.categoryId == 1131 || ge.categoryId == 1105 || ge.categoryId == 1106 ||
                    //                     ge.categoryId == 1107 || ge.categoryId == 1108 || ge.categoryId == 1146 ||
                    //                     ge.categoryId == 1147 || ge.categoryId == 1148)
                    //            {
                    //                tmp.Tranmission = "Manual";
                    //                break;
                    //            }

                    //        }
                    //    }

                    //}
                    //else
                    //{
                    //    tmp.Tranmission = "";
                    //}

                    //if (vehicleInformation.vehicleSpecification.engines != null &&
                    //    vehicleInformation.vehicleSpecification.engines.Any())
                    //{

                    //    foreach (Engine er in vehicleInformation.vehicleSpecification.engines)
                    //    {
                    //        tmp.FuelEconomyCity = er.fuelEconomyCityValue.lowValue.ToString();
                    //        tmp.FuelEconomyHighWay = er.fuelEconomyHwyValue.lowValue.ToString();
                    //        break;

                    //    }
                    //}

                    //var builderOption = new StringBuilder();

                    //if (vehicleInformation != null && vehicleInformation.standards != null)
                    //{

                    //    foreach (Standard sd in vehicleInformation.standards)
                    //    {
                    //        if (!String.IsNullOrEmpty(sd.description) && sd.installed)
                    //        {
                    //            builderOption.Append(sd.description + ",");
                    //        }
                    //    }
                    //}
                    //if (String.IsNullOrEmpty(builderOption.ToString()))
                    //    tmp.StandardOptions = "";
                    //else
                    //{
                    //    builderOption.Remove(builderOption.Length - 1, 1);
                    //    tmp.StandardOptions = builderOption.ToString().Replace("\'", "\\'");
                    //}

                    //if (vehicleInformation.vehicleSpecification.bodyTypes != null && vehicleInformation.vehicleSpecification.bodyTypes.Any())
                    //{
                    //    foreach (BodyType bd in vehicleInformation.vehicleSpecification.bodyTypes)
                    //    {
                    //        tmp.BodyType = bd.bodyTypeName;
                    //        break;
                    //    }



                    //}

                    //try
                    //{

                    //    tmp.Doors = vehicleInformation.vehicleSpecification.numberOfPassengerDoors.ToString();
                    //}
                    //catch (Exception)
                    //{
                    //    tmp.Doors = "";
                    //}

                    //try
                    //{

                    //    tmp.DriveTrain = CommonHelper.getDriveXMLFileFromVehicleInfo(vehicleInformation);
                    //}
                    //catch (Exception)
                    //{
                    //    tmp.DriveTrain = "";

                    //}


                    //if (vehicleInformation.stockPhotos != null && vehicleInformation.stockPhotos.Any())
                    //{
                    //    tmp.DefaultImageUrl = vehicleInformation.stockPhotos.First().url;
                    //    //tmp.ThumbnailImageURL = vehicleInformation.stockPhotos.First().url;
                    //    //tmp.CarImageUrl = vehicleInformation.stockPhotos.First().url;
                    //}


                    //if (vehicleInformation.vehicleSpecification.marketClasses != null && vehicleInformation.vehicleSpecification.marketClasses.Any())
                    //{
                    //    foreach (var a in vehicleInformation.vehicleSpecification.marketClasses)
                    //    {
                    //        if (a.marketClassName.Contains("Truck") || a.marketClassName.Contains("Cargo Vans") || a.marketClassName.Contains("Large Passenger"))
                    //        {
                    //            tmp.VehicleType = "Truck";
                    //            tmp.TruckClass = "Class1";

                    //            tmp.TruckType = "Truck";

                    //            tmp.TruckCategory = "Pickup Truck";
                    //            break;
                    //        }
                    //    }
                    //}






                }
                context.SaveChanges();

            }


            drgVinControl.DataSource = vincontrolist;
        }

        private void button30_Click(object sender, EventArgs e)
        {
            var contextCl = new whitmanenterprisecraigslistEntities();

            var dtCityList = contextCl.whitmanenterprisecraigslistcity.ToList();

            var hostingserver = contextCl.whitmanenterprisehostingserver.ToList();

            foreach (var tmp in hostingserver)
            {
                if (dtCityList.Any(x => x.CityName == tmp.County))
                    tmp.CityId = dtCityList.First(x => x.CityName == tmp.County).CityID;


            }

            contextCl.SaveChanges();

        }

        private void button31_Click(object sender, EventArgs e)
        {
            var onlineList = CommonHelper.getOnlineVehicleList();

            var doc = CommonHelper.loadXMLDocument();

            var XPathExpression = "//Vehicle";

            var nodelist = doc.SelectNodes(XPathExpression);

            DataTable dtAutoTrader = SQLHelper.initialAutoTraderTable();

            if (nodelist.Count > 0)
            {
                foreach (XmlNode node in nodelist)
                {
                    DataRow dr = dtAutoTrader.NewRow();

                    string stockNumber = node.SelectSingleNode(".//StockNum").InnerText;

                    if (onlineList.Any(x => x.StockNumber == stockNumber))
                    {

                        var car = onlineList.First(x => x.StockNumber == stockNumber);

                        dr["DealerId"] = "10125";

                        if (node.SelectSingleNode(".//StockNum") == null)
                            dr["StockNumber"] = "";
                        else
                            dr["StockNumber"] = stockNumber;


                        if (node.SelectSingleNode(".//Year") == null)
                            dr["Year"] = "";
                        else
                            dr["Year"] = node.SelectSingleNode(".//Year").InnerText;

                        if (node.SelectSingleNode(".//Make") == null)
                            dr["Make"] = "";
                        else
                            dr["Make"] = node.SelectSingleNode(".//Make").InnerText;

                        if (node.SelectSingleNode(".//Model") == null)
                            dr["Model"] = "";
                        else
                            dr["Model"] = node.SelectSingleNode(".//Model").InnerText;


                        dr["Trim"] = "";

                        if (node.SelectSingleNode(".//VIN") == null)
                            dr["VIN"] = "";
                        else
                            dr["VIN"] = node.SelectSingleNode(".//VIN").InnerText;


                        if (node.SelectSingleNode(".//Mileage") == null)
                            dr["Mileage"] = "";
                        else
                            dr["Mileage"] = node.SelectSingleNode(".//Mileage").InnerText;

                        if (node.SelectSingleNode(".//ExtColor") == null)
                            dr["ExteriorColor"] = "";
                        else
                            dr["ExteriorColor"] = node.SelectSingleNode(".//ExtColor").InnerText;

                        if (node.SelectSingleNode(".//IntColor") == null)
                            dr["InteriorColor"] = "";
                        else
                            dr["InteriorColor"] = node.SelectSingleNode(".//IntColor").InnerText;

                        if (node.SelectSingleNode(".//Transmission") == null)
                            dr["Tranmission"] = "";
                        else
                            dr["Tranmission"] = node.SelectSingleNode(".//Transmission").InnerText;

                        dr["PhysicalImages"] = "";

                        if (node.SelectSingleNode(".//Description") == null)
                            dr["Descriptions"] = "";
                        else
                            dr["Descriptions"] = node.SelectSingleNode(".//Description").InnerText;


                        if (node.SelectSingleNode(".//VehicleType") == null)
                            dr["BodyType"] = "";
                        else
                            dr["BodyType"] = node.SelectSingleNode(".//VehicleType").InnerText;


                        if (node.SelectSingleNode(".//Fuel") == null)
                            dr["FuelType"] = "";
                        else
                            dr["FuelType"] = node.SelectSingleNode(".//Fuel").InnerText;

                        if (node.SelectSingleNode(".//Drivetrain") == null)
                            dr["DriveType"] = "";
                        else
                            dr["DriveType"] = node.SelectSingleNode(".//Drivetrain").InnerText;


                        dr["EngineType"] = "";

                        dr["VideoURL"] = "";

                        dr["VideoSource"] = "";

                        dr["Options"] = "";

                        dr["NewUsed"] = "Used";

                        dr["Age"] = 0;

                        if (car != null)
                        {
                            dr["ImageURL"] = car.CarImageUrl;

                            dr["PhysicalImages"] = car.CarImageUrl;
                            dr["Price"] = car.SalePrice;
                            dtAutoTrader.Rows.Add(dr);
                        }


                    }
                }

            }

            drgVinControl.DataSource = ConvertAutoTraderFormatToVinControl(dtAutoTrader);

        }


        public static List<VinControlVehicle> ConvertAutoTraderFormatToVinControl(DataTable dtAutoTrader)
        {
            var list = new List<VinControlVehicle>();


            foreach (DataRow dr in dtAutoTrader.Rows)
            {
                var vehicle = new VinControlVehicle()
                                  {
                                      Year = 0,
                                      Make = dr["Make"].ToString(),
                                      Model = dr["Model"].ToString(),
                                      Trim = dr["Trim"].ToString(),
                                      VIN = dr["VIN"].ToString(),
                                      StockNumber = dr["StockNumber"].ToString(),
                                      SalePrice = dr["Price"].ToString(),
                                      Mileage = dr["Mileage"].ToString(),
                                      ExteriorColor = dr["ExteriorColor"].ToString(),
                                      BodyType = dr["BodyType"].ToString(),
                                      EngineType = dr["EngineType"].ToString(),
                                      DriveTrain = dr["DriveType"].ToString(),
                                      FuelType = dr["FuelType"].ToString(),
                                      Tranmission = dr["Tranmission"].ToString(),
                                      CarsOptions = dr["Options"].ToString(),
                                      Descriptions = dr["Descriptions"].ToString(),
                                      CarImageUrl = dr["PhysicalImages"].ToString(),
                                      DealerId = 10215,



                                  };

                if (!String.IsNullOrEmpty(dr["NewUsed"].ToString()))
                {
                    if (dr["NewUsed"].ToString().ToLower().Equals("used"))
                        vehicle.NewUsed = "Used";
                    else if (dr["NewUsed"].ToString().ToLower().Equals("new"))
                        vehicle.NewUsed = "New";
                }
                else
                {
                    vehicle.NewUsed = "Used";
                }


                if (!String.IsNullOrEmpty(dr["Age"].ToString()))
                {
                    vehicle.Age = Convert.ToInt32(dr["Age"].ToString());
                }
                else
                {
                    vehicle.Age = 0;
                }


                int Year = 0;

                bool yearFlag = Int32.TryParse(dr["Year"].ToString(), out Year);

                vehicle.Year = Year;
                if (String.IsNullOrEmpty(dr.Field<string>("PhysicalImages")) &&
                    String.IsNullOrEmpty(dr.Field<string>("ImageURL")))
                    vehicle.CarImageUrl = "";
                else if (!String.IsNullOrEmpty(dr.Field<string>("PhysicalImages")) &&
                         String.IsNullOrEmpty(dr.Field<string>("ImageURL")))
                    vehicle.CarImageUrl = dr.Field<string>("PhysicalImages").Replace("|", ",");
                else if (String.IsNullOrEmpty(dr.Field<string>("PhysicalImages")) &&
                         !String.IsNullOrEmpty(dr.Field<string>("ImageURL")))
                    vehicle.CarImageUrl = dr.Field<string>("ImageURL").Replace("|", ",");
                else
                    vehicle.CarImageUrl = dr.Field<string>("PhysicalImages").Replace("|", ",");

                if (vehicle.SalePrice.Contains("."))
                    vehicle.SalePrice = vehicle.SalePrice.Substring(0, vehicle.SalePrice.IndexOf("."));

                list.Add(vehicle);
            }
            return list;
        }

        private void button33_Click(object sender, EventArgs e)
        {
            RegistryKey registry =
                Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            registry.SetValue("ProxyEnable", 1);
            registry.SetValue("ProxyServer", "69.5.45.1:3333");
            //registry.SetValue("Port", "3128");
        }

        private void button34_Click(object sender, EventArgs e)
        {


            var trackingid =
                Convert.ToUInt64(Convert.ToInt64(txtDealerID.Text)*DateTime.Now.Year*DateTime.Now.Month*DateTime.Now.Day);


            txtDealerID.Text = trackingid.ToString();



        }



        private void button37_Click(object sender, EventArgs e)
        {
            var context = new whitmanenterprisecraigslistEntities();

            var cityAz = context.whitmanenterprisecraigslistcity.Where(x => x.CityID > 29).ToList();

            foreach (var tmp in cityAz)
            {
                for (int i = 0; i < 7; i++)
                {
                    var hosting = new whitmanenterprisehostingserver()
                                      {
                                          CityId = tmp.CityID,
                                          County = tmp.CityName,
                                          DaysOfWeek = DateTime.Now.AddDays(i).DayOfWeek.ToString(),
                                          HostName = "http://kissingpic.us/",
                                          WorkingDir = "20120829OtherPics",

                                      };

                    context.AddTowhitmanenterprisehostingserver(hosting);
                }

            }

            context.SaveChanges();

            System.Windows.Forms.MessageBox.Show("DONE", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);



        }

        private void button38_Click(object sender, EventArgs e)
        {
            txtDealerID.Text = Calculate(33.707390, -117.705377, 33.648280, -117.915538).ToString();
        }


        public double DistanceBetweenPlaces(string lat1, string lon1, string lat2, string lon2)
        {
            return Distance(Convert.ToDouble(lat1), Convert.ToDouble(lon1), Convert.ToDouble(lat2),
                            Convert.ToDouble(lon2), 'M');
        }


        private double Distance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1))*Math.Sin(deg2rad(lat2)) +
                          Math.Cos(deg2rad(lat1))*Math.Cos(deg2rad(lat2))*Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist*60*1.1515;
            if (unit == 'K')
            {
                dist = dist*1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist*0.8684;
            }
            return (dist);
        }

        private double deg2rad(double deg)
        {
            return (deg*Math.PI/180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts radians to decimal degrees             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private double rad2deg(double rad)
        {
            return (rad/Math.PI*180.0);
        }

        public double Calculate(double sLatitude, double sLongitude, double eLatitude,
                                double eLongitude)
        {
            var sLatitudeRadians = sLatitude*(Math.PI/180.0);
            var sLongitudeRadians = sLongitude*(Math.PI/180.0);
            var eLatitudeRadians = eLatitude*(Math.PI/180.0);
            var eLongitudeRadians = eLongitude*(Math.PI/180.0);

            var dLongitude = eLongitudeRadians - sLongitudeRadians;
            var dLatitude = eLatitudeRadians - sLatitudeRadians;

            var result1 = Math.Pow(Math.Sin(dLatitude/2.0), 2.0) +
                          Math.Cos(sLatitudeRadians)*Math.Cos(eLatitudeRadians)*
                          Math.Pow(Math.Sin(dLongitude/2.0), 2.0);

            // Using 3956 as the number of miles around the earth
            var result2 = 3956.0*2.0*
                          Math.Atan2(Math.Sqrt(result1), Math.Sqrt(1.0 - result1));

            return result2;
        }

        private void button39_Click(object sender, EventArgs e)
        {
           var sourcelist= GetOnlineVehicleList();
          
            drgAutoTrader.DataSource = sourcelist;

           richTextBox1.Text=sourcelist.Count.ToString();
        }
        public static XmlDocument LoadXMLDocumentForExtreme()
        {
            try
            {
                var reader = new XmlTextReader("http://www.extremecarstrucks.com/inventory.xml?ID=12655");

                var urlDoc = new XmlDocument();

                urlDoc.Load(reader);

                return urlDoc;


            }
            catch (System.Exception ex)
            {
                throw ex;

            }

        }

        public static List<VinControlVehicle> GetOnlineVehicleList()
        {
            string basicURL = "http://www.extremecarstrucks.com/view-inventory/";

            var htmlWeb = new HtmlWeb();

            var htmlDoc = new HtmlAgilityPack.HtmlDocument();

            htmlWeb.AutoDetectEncoding = true;

            var list = new List<VinControlVehicle>();

            try
            {
                htmlDoc = htmlWeb.Load(basicURL);

                var selectNode_TotalResult = htmlDoc.DocumentNode.SelectSingleNode("//span[@class='accent-color1']");

                int totalResultReturn = 0;

                int maxPage = GetNumberOfPageToRun(selectNode_TotalResult.InnerText, out totalResultReturn);

                for (int index = 1; index <= maxPage; index++)
                {
                    if (index > 1)
                        htmlDoc = htmlWeb.Load(basicURL + "/?page=" + index);

                    var selectImageNode = htmlDoc.DocumentNode.SelectNodes("//td[@class='inventory-photo']");

                    var selectInfoNode = htmlDoc.DocumentNode.SelectNodes("//td[@class='inventory-details']");

                    if (selectImageNode != null && selectInfoNode != null)
                    {

                        for (int i = 0; i < selectImageNode.Count; i++)
                        {

                            var car = new VinControlVehicle();

                            HtmlNode linkNode = selectImageNode[i].SelectSingleNode(".//a");

                            HtmlNode stockNode = selectInfoNode[i].SelectSingleNode(".//span[@class='stocknumber']");

                            HtmlNode vinNode = selectInfoNode[i].SelectSingleNode(".//span[@class='vin']");

                            HtmlNode exteriorNode = selectInfoNode[i].SelectSingleNode(".//span[@class='Extcolor']");
                            HtmlNode interiorNode = selectInfoNode[i].SelectSingleNode(".//span[@class='Intcolor']");

                            HtmlNode tranmissionNode = selectInfoNode[i].SelectSingleNode(".//div[@class='transmission']");

                            //HtmlNode fuelNode = selectInfoNode[i].SelectNodes(".//span[@class='fuel']").Last();

                            HtmlNode engineNode = selectInfoNode[i].SelectSingleNode(".//div[@class='engine']");

                            HtmlNode mileageNode = selectInfoNode[i].SelectSingleNode(".//span[@class='mileage']");

                            if (linkNode != null && stockNode != null && vinNode!=null)
                            {

                                var htmlSubDoc = new HtmlAgilityPack.HtmlDocument();

                                htmlSubDoc = htmlWeb.Load(linkNode.Attributes["href"].Value);

                                var selectSubImageNode =
                                    htmlSubDoc.DocumentNode.SelectSingleNode("//div[@class='slider-viewport']");

                                var selectMonthlyPaymentNode =
                                    htmlSubDoc.DocumentNode.SelectSingleNode("//div[@class='accent-color1']");



                                if (selectSubImageNode != null && selectMonthlyPaymentNode != null)
                                {

                                    var subimageNode = selectSubImageNode.SelectNodes(".//a");

                                    if (subimageNode != null)
                                    {

                                        string imageURL = "";

                                        foreach (var thnode in subimageNode)
                                        {
                                            if (!String.IsNullOrEmpty(thnode.Attributes["href"].Value))

                                                imageURL += thnode.Attributes["href"].Value + ",";
                                        }

                                        if (!String.IsNullOrEmpty(imageURL))
                                            imageURL = imageURL.Substring(0, imageURL.Length - 1);

                                        car.SalePrice =
                                            selectMonthlyPaymentNode.InnerText.Substring(
                                                selectMonthlyPaymentNode.InnerText.LastIndexOf('.') + 1);
                                     

                                        car.CarImageUrl = imageURL;
                                        car.StockNumber = stockNode.InnerText;
                                        car.VIN =vinNode.InnerText;
                                        car.InteriorColor = interiorNode == null ? "" : interiorNode.InnerText;
                                        car.ExteriorColor = exteriorNode == null ? "" : exteriorNode.InnerText;
                                        car.Tranmission = tranmissionNode == null ? "" : tranmissionNode.InnerText;
                                     
                                        car.EngineType = engineNode == null ? "" : engineNode.InnerText;
                                        car.Mileage = mileageNode == null ? "" :CommonHelper.RemoveSpecialCharactersForMSRP(mileageNode.InnerText.Replace("miles","").Trim());

                                        if(!list.Any(x=>x.VIN==car.VIN))
                                            list.Add(car);
                                    }

                                }


                            }





                        }
                    }



                }

                return list;



            }
            catch (Exception ex)
            {
                //return null;
                throw new Exception("*******EXCEPTION IN GETLISTVEHICLEINFORMATIONONLINE IN WESTCOAST FUNCTION AT LINK IS********************" + basicURL + "**************DETAILS" + ex.InnerException + "AND MESSAGE IS " + ex.Message);

            }

        }
        public static int GetNumberOfPageToRun(string stringTotalResult, out int totalresultReturn)
        {

            string pureNumber = "";

            int totalResult = 0;

            int resultReturn = 0;

            totalresultReturn = 0;

            if (stringTotalResult.Contains(","))
            {
                string[] splitResult = stringTotalResult.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string tmp in splitResult)
                {
                    if (!tmp.Equals(","))
                        pureNumber += tmp;
                }

            }
            else
                pureNumber = stringTotalResult;


            bool flag = int.TryParse(pureNumber, out totalResult);

            if (flag)
            {
                if (totalResult == 0)
                    resultReturn = totalResult;
                else
                {
                    resultReturn = totalResult / 25 + 1;
                }
            }

            return resultReturn;
        }

        //private void button40_Click(object sender, EventArgs e)
        //{

        //    var context = new whitmanenterprisecraigslistEntities();

        //    var dealerList = context.whitmanenterprisedealerlist.ToList();

        //    foreach (var tmp in dealerList)
        //    {
        //        tmp.TradeInBannerLink = EncryptionHelper.EncryptString(tmp.VincontrolId);

        //    }

        //    context.SaveChanges();
        //}

        private void button41_Click(object sender, EventArgs e)
        {
            var contextCL = new whitmanenterprisecraigslistEntities();
            var contextVin = new whitmanenterprisewarehouseEntities();

            var dealerList = contextVin.whitmanenterprisedealership.ToList();

            var dealerCLList = contextCL.whitmanenterprisedealerlist.ToList();

            foreach (var tmp in dealerList)
            {
                string did = tmp.idWhitmanenterpriseDealership.ToString();
                if (dealerCLList.Any(x => x.VincontrolId.Equals(did)))
                {
                    tmp.EmailFormat = dealerCLList.First(x => x.VincontrolId.Equals(did)).EmailFormat;
                }

            }

            contextVin.SaveChanges();

        }

        private void button42_Click(object sender, EventArgs e)
        {
            var contextCL = new whitmanenterprisecraigslistEntities();

            string Path = "/newaccounts.csv";

            var ClappCredential =
                new NetworkCredential(
                    System.Configuration.ConfigurationManager.AppSettings["CLAPPFTPUsername"].ToString(),
                    System.Configuration.ConfigurationManager.AppSettings["CLAPPFTPPassword"].ToString());


            var csv = SQLHelper.getInventoryAutoTraderFormatWithoutHeader222(Path);

            var dtTemporaryNoheader = new DataTable();

            dtTemporaryNoheader.Load(csv);

            drgVinControl.DataSource = dtTemporaryNoheader;

            for (int y = 1; y <= 58; y++)
            {
                int tmp = 0;


                int indexMin = (y - 1)*5;
                int indexMax = (y*5);



                foreach (DataRow drRow in dtTemporaryNoheader.Rows)
                {
                    if (tmp >= indexMin && tmp < indexMax)
                    {
                        var emailAccount = new vinclappemailaccount()
                                               {
                                                   AccountName = drRow[0].ToString(),
                                                   AccountPassword = drRow[1].ToString(),
                                                   Phone =
                                                       "949-" + drRow[2].ToString().Substring(0, 3) + "-" +
                                                       drRow[2].ToString().Substring(3),
                                                   Proxy = true,
                                                   ProxyIP = drRow[3].ToString(),
                                                   DateAdded = DateTime.Now,
                                                   LastUpdated = DateTime.Now,
                                                   Active = true,
                                                   Computer = y
                                               };

                        contextCL.AddTovinclappemailaccount(emailAccount);


                    }
                    tmp++;
                }




            }

            contextCL.SaveChanges();


        }

        private void button43_Click(object sender, EventArgs e)
        {
            string image1 = @"C:\DealerLogo\18803-DSCN0094.jpg";
            string image2 = @"C:\DealerLogo\BPHead.jpg";

            System.Drawing.Image canvas = Bitmap.FromFile(image1);

            Graphics gra = Graphics.FromImage(canvas);
            Bitmap smallImg = new Bitmap(image2);

            gra.DrawImage(smallImg, new Point(0, -20));
            canvas.Save(@"C:\DealerLogo\newimage.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private void button44_Click(object sender, EventArgs e)
        {
            var htmlToImageConverter = new HtmlToImage();

            htmlToImageConverter.SerialNumber = ConfigurationManager.AppSettings["PDFSerialNumber"];
            // set browser width
            htmlToImageConverter.BrowserWidth = 300;

            // set HTML Load timeout
            htmlToImageConverter.HtmlLoadedTimeout = 2;

            htmlToImageConverter.TransparentImage = false;

            System.Drawing.Image imageObject = null;

            string htmlCode =
                CommonHelper.GenerateHTMLImageCode();

            imageObject = htmlToImageConverter.ConvertHtmlToImage(htmlCode, null)[0];

            //var stream = new MemoryStream();

            imageObject.Save(@"C:\DealerLogo\WTEST.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);



        }

        private void button45_Click(object sender, EventArgs e)
        {
            var context = new whitmanenterprisewarehouseEntities();


            var newFirstMasterUSer = new vincontrolusers()
                                         {
                                             username = "37695",
                                             expirationdate = DateTime.Now.AddYears(2),
                                             accountstatus = true,
                                             password = "jeff1451",
                                             rolename = "Admin"


                                         };


            context.AddTovincontrolusers(newFirstMasterUSer);


            context.SaveChanges();

            System.Windows.Forms.MessageBox.Show("DONE", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void button35_Click(object sender, EventArgs e)
        {
            int DID = Convert.ToInt32(txtDealerID.Text);

            var context = new whitmanenterprisewarehouseEntities();

            var newFirstUSer = new whitmanenterpriseusers()
                                   {
                                       Active = true,
                                       Cellphone = "949-910-7292",
                                       DealershipID = DID,
                                       DefaultLogin = DID,
                                       Email = "travis@vincontrol.com",
                                       Name = "Freeway Isuzu",
                                       Password = "jeff1451",
                                       RoleName = "Admin",
                                       UserName = DID.ToString(CultureInfo.InvariantCulture),


                                   };

            var newFirstUsernotification = new whitmanenterpriseusersnotification()
                                               {
                                                   AgingNotification = true,
                                                   AppraisalNotification = true,
                                                   C24Hnotification = true,
                                                   DealershipId = DID,
                                                   InventoryNotification = true,
                                                   NoteNotification = true,
                                                   PriceChangeNotification = true,
                                                   UserName = DID.ToString(CultureInfo.InvariantCulture),
                                                   WholeNotification = true,


                                               };

            context.AddTowhitmanenterpriseusers(newFirstUSer);

            context.AddTowhitmanenterpriseusersnotification(newFirstUsernotification);

            context.SaveChanges();

            System.Windows.Forms.MessageBox.Show("DONE", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        //private void Button46Click(object sender, EventArgs e)
        //{
        //    var context = new vincontrolscrappingEntities();

        //    var dtAutoTraderList = context.autotraderdealer.ToList();

        //    var dtCarsComList = context.carscomdealer.ToList();

        //    //foreach (var carscomdealer in dtCarsComList)
        //    //{
        //    //    if (dtAutoTraderList.Any(x => x.AutoTraderDealerName == carscomdealer.CarsComDealerName))
        //    //    {
        //    //        var searchresult =
        //    //            dtAutoTraderList.First(x => x.AutoTraderDealerName == carscomdealer.CarsComDealerName);
        //    //        carscomdealer.AutoTraderDealerName = carscomdealer.CarsComDealerName;

        //    //        carscomdealer.AutoTraderId = searchresult.AutoTraderId;
        //    //    }
        //    //}


        //    foreach (var autotraderdealer in dtAutoTraderList)
        //    {
        //        if (dtCarsComList.Any(x => x.CarsComDealerName == autotraderdealer.AutoTraderDealerName))
        //        {
        //            var searchresult =
        //                dtCarsComList.First(x => x.CarsComDealerName == autotraderdealer.AutoTraderDealerName);

        //            autotraderdealer.CarsComDealerName = searchresult.CarsComDealerName;

        //            autotraderdealer.CarsComId = searchresult.CarsComInverntoryId;
        //        }
        //    }
        //    context.SaveChanges();

        //    System.Windows.Forms.MessageBox.Show("DONE", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        //}

        //private void button47_Click(object sender, EventArgs e)
        //{

        //}

        //private void button48_Click(object sender, EventArgs e)
        //{
        //    var context = new vincontrolscrappingEntities();

        //    var autoMakeList = context.automake.ToList().Select(x => x.Make.ToLower()).Distinct();

        //    foreach (var tmp in context.autotraderdealer.Where(x => x.Franchise == null).ToList())
        //    {
        //        tmp.Franchise = existName(tmp.AutoTraderDealerName.ToLower(), autoMakeList);

        //        context.SaveChanges();
        //    }

        //    System.Windows.Forms.MessageBox.Show("DONE", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //}

        //private bool existName(string dealerName, IEnumerable<string> makeList)
        //{
        //    foreach (var tmp in makeList)
        //    {
        //        if (dealerName.Contains(tmp))
        //            return true;

        //    }

        //    return false;
        //}

        //private void button49_Click(object sender, EventArgs e)
        //{
        //    var context = new vincontrolscrappingEntities();

        //    var zipcodeList = context.usazipcode.ToList();

        //    foreach (var tmp in context.autotraderdealer.Where(x => String.IsNullOrEmpty(x.City)).ToList())
        //    {

        //        if (zipcodeList.Any(x => x.ZIPCode == tmp.ZipCode))
        //        {
        //            var searchResult = zipcodeList.First(x => x.ZIPCode == tmp.ZipCode);

        //            tmp.Latitude = searchResult.Latitude;

        //            tmp.Longitude = searchResult.Longitude;

        //            tmp.CountyName = searchResult.CountyName;

        //            tmp.State = searchResult.StateAbbr;

        //            tmp.City = searchResult.CityName;

        //            context.SaveChanges();
        //        }
        //    }

        //    System.Windows.Forms.MessageBox.Show("DONE", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        //}

        //private void Button50Click(object sender, EventArgs e)
        //{
        //    var context = new vincontrolscrappingEntities();

        //    foreach (var tmp in context.autotraderdealer.Where(x => x.ZipCode == null).ToList())
        //    {
        //        string address = tmp.Address;

        //        string zipCodePart = address.Substring(address.LastIndexOf(" ", System.StringComparison.Ordinal) + 1);

        //        if (zipCodePart.Contains("-"))
        //            tmp.ZipCode =
        //                Convert.ToInt32(
        //                    zipCodePart.Substring(0, zipCodePart.IndexOf("-", System.StringComparison.Ordinal)));
        //        else
        //        {
        //            tmp.ZipCode = Convert.ToInt32(zipCodePart);
        //        }

        //        context.SaveChanges();
        //    }

        //}

        //private void button51_Click(object sender, EventArgs e)
        //{
        //    var context = new vincontrolscrappingEntities();

        //    foreach (var tmp in context.carscomdealer.ToList())
        //    {
        //        string urlLInk = tmp.CarsComInventoryLink;

        //        if (!String.IsNullOrEmpty(urlLInk))
        //        {
        //            var dlLink = urlLInk.Substring(urlLInk.LastIndexOf("dlId=", System.StringComparison.Ordinal));

        //            dlLink = dlLink.Replace("dlId=", "");

        //            tmp.CarsComInverntoryId = Convert.ToInt32(dlLink);

        //            context.SaveChanges();
        //        }
        //    }

        //    System.Windows.Forms.MessageBox.Show("DONE", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        //    ///dealers/search/inventory?stkTyp=U&dlId=8805641
        //}

        //private void button52_Click(object sender, EventArgs e)
        //{
        //    var context = new vincontrolscrappingEntities();
        //    foreach (
        //        var tmp in
        //            context.pacifictimezone1.Where(
        //                x => !String.IsNullOrEmpty(x.CarFaxURL) && String.IsNullOrEmpty(x.Vin)).ToList())
        //    {
        //        tmp.Vin = CarFaxHelper.GetVinNumberFromCarFaxLink(tmp.CarFaxURL);

        //        context.SaveChanges();
        //    }



        //}

        //private void button53_Click(object sender, EventArgs e)
        //{
        //    var chartGraph = new ChartGraph();
        //    var list = new List<ChartModel>();
        //    var contextVinControl = new whitmanenterprisewarehouseEntities();
        //    var targetCar =
        //        contextVinControl.whitmanenterprisedealershipinventory.FirstOrDefault(
        //            (x => x.ListingID == 19391));
        //    string modelWord = targetCar.Model.Trim().Replace(" ", "");

        //    modelWord = modelWord.Replace("Sedan", "");

        //    modelWord = modelWord.Replace("Coupe", "");

        //    modelWord = modelWord.Replace("Convertible", "");

        //    modelWord = modelWord.Replace("4WDTruck", "");

        //    var context = new vincontrolscrappingEntities();


        //    var resultNorth = from i in context.pacifictimezone1

        //                      where
        //                          targetCar.DealershipState == i.State && i.Year == targetCar.ModelYear &&
        //                          i.Make == targetCar.Make &&
        //                          ((i.Model.TrimEnd().Replace(" ", "") + i.Trim.Replace(" ", "")).Contains(modelWord)) &&
        //                          !i.CurrentPrice.Equals("0")
        //                      select new
        //                                 {
        //                                     i.RegionalListingId,
        //                                     i.Make,
        //                                     i.Model,
        //                                     i.Trim,
        //                                     i.Year,
        //                                     i.ExteriorColor,
        //                                     i.InteriorColor,
        //                                     i.MoonRoof,
        //                                     i.SunRoof,
        //                                     i.Certified,
        //                                     i.Vin,
        //                                     i.Mileage,
        //                                     i.CurrentPrice,
        //                                     i.DateAdded,
        //                                     i.CarsComThumbnailURL,
        //                                     i.CarsComListingURL,
        //                                     i.Dealershipname,
        //                                     i.Address,
        //                                     i.City,
        //                                     i.State,
        //                                     i.Franchise,
        //                                     i.CountyName,
        //                                     i.Latitude,
        //                                     i.Longitude,
        //                                     i.ZipCode,
        //                                     i.BodyStyle,
        //                                     i.CarsCom,
        //                                     i.AutoTrader,
        //                                     i.AutoTraderListingURL

        //                                 };


        //    if (resultNorth.Any())
        //    {
        //        var table = new HashSet<string>();

        //        var hashSet = new HashSet<string>();

        //        foreach (var row in resultNorth)
        //        {
        //            if (!String.IsNullOrEmpty(row.CurrentPrice) && !String.IsNullOrEmpty(row.Mileage))
        //            {
        //                string filtertrim = string.IsNullOrEmpty(row.Trim) ? "other" : row.Trim;
        //                if (!hashSet.Contains(filtertrim))
        //                    table.Add(filtertrim);
        //                hashSet.Add(filtertrim);
        //            }

        //        }

        //        var trimsList = table.ToList();


        //        //if (targetCar.BodyType.ToLower().Contains("coupe"))
        //        //{
        //        //    resultNorth = resultNorth.Where(x => x.BodyStyle.Contains("coupe"));

        //        //}
        //        //else if (targetCar.BodyType.ToLower().Contains("convertible"))
        //        //{
        //        //    resultNorth = resultNorth.Where(x => x.BodyStyle.Contains("convertible"));

        //        //}

        //        foreach (var item in resultNorth)
        //        {
        //            if (!String.IsNullOrEmpty(item.CurrentPrice) && !String.IsNullOrEmpty(item.Mileage))
        //            {

        //                var chart = new ChartModel
        //                                {
        //                                    ListingId = item.RegionalListingId,
        //                                    Title =
        //                                        new ChartModel.TitleInfo
        //                                            {
        //                                                Make = item.Make,
        //                                                Model = item.Model,
        //                                                Trim = string.IsNullOrEmpty(item.Trim) ? "other" : item.Trim,
        //                                                Year = item.Year.GetValueOrDefault()
        //                                            },
        //                                    Color =
        //                                        new ChartModel.ColorInfo
        //                                            {Exterior = item.ExteriorColor, Interior = item.InteriorColor},
        //                                    Option = new ChartModel.OptionInfo
        //                                                 {
        //                                                     Moonroof = item.MoonRoof.GetValueOrDefault(),
        //                                                     Navigation = item.MoonRoof.GetValueOrDefault(),
        //                                                     Sunroof = item.SunRoof.GetValueOrDefault()
        //                                                 },
        //                                    Certified = item.Certified.GetValueOrDefault(),
        //                                    VIN = item.Vin,
        //                                    Franchise = item.Franchise.GetValueOrDefault() ? "franchise" : "independant",

        //                                    Uptime = DateTime.Now.Subtract(item.DateAdded.GetValueOrDefault()).Days,
        //                                    Trims = trimsList,
        //                                    ThumbnailURL =
        //                                        String.IsNullOrEmpty(item.CarsComThumbnailURL)
        //                                            ? ""
        //                                            : item.CarsComThumbnailURL,

        //                                    CarsCom = item.CarsCom.GetValueOrDefault(),
        //                                    CarsComListingURL =
        //                                        String.IsNullOrEmpty(item.CarsComListingURL)
        //                                            ? ""
        //                                            : item.CarsComListingURL,
        //                                    AutoTrader = item.AutoTrader != null && item.AutoTrader.GetValueOrDefault(),
        //                                    AutoTraderListingURL =
        //                                        String.IsNullOrEmpty(item.AutoTraderListingURL)
        //                                            ? ""
        //                                            : item.AutoTraderListingURL,
        //                                    Seller =
        //                                        new ChartModel.SellerInfo()
        //                                            {
        //                                                SellerName = item.Dealershipname,
        //                                                SellerAddress = item.Address
        //                                            },



        //                                };


        //                int validMileage = 0;

        //                bool flagMile = Int32.TryParse(item.Mileage, out validMileage);


        //                int validPrice = 0;

        //                bool flagPrice = Int32.TryParse(item.CurrentPrice, out validPrice);

        //                chart.Miles = validMileage;

        //                chart.Price = validPrice;

        //                list.Add(chart);
        //            }
        //        }



        //        int MileageNumber = 0;
        //        Int32.TryParse(targetCar.Mileage, out MileageNumber);

        //        int SalePriceNumber = 0;
        //        Int32.TryParse(targetCar.SalePrice, out SalePriceNumber);

        //        chartGraph.Target = new ChartGraph.TargetCar()
        //                                {
        //                                    ListingId = 19391,
        //                                    Mileage = MileageNumber,
        //                                    SalePrice = SalePriceNumber,
        //                                };

        //        chartGraph.ChartModels = list.OrderBy(x => x.Distance).ToList();

        //        drgAutoTrader.DataSource = chartGraph.ChartModels;


        //    }

        //}

        private void button54_Click(object sender, EventArgs e)
        {
            int dealerId = Convert.ToInt32(txtDealerID.Text);

            var context = new whitmanenterprisecraigslistEntities();

            var removeList = from a in context.vincontrolcraigslisttracking
                             where a.CLPostingId != null && a.CLPostingId > 0 && a.DealerId == dealerId
                             select new
                                        {
                                            a.TrackingId,
                                            a.AddedDate
                                        };

            dRgDealer.DataSource = removeList;

            foreach (var tmp in removeList.ToList())
            {
                var dateForDeletion = DateTime.Now.AddDays(-1);

                if (tmp.AddedDate.Value.Date <= dateForDeletion.Date)
                {
                    var searchResult = context.vincontrolcraigslisttracking.First(x => x.TrackingId == tmp.TrackingId);
                    searchResult.CLPostingId = 0;
                    context.SaveChanges();
                }


            }


        }

        private void button55_Click(object sender, EventArgs e)
        {
            //var context = new vincontrolscrappingEntities();

            
            //foreach (var tmp in context.pacifictimezone1.Where(x=>!x.Make.Equals("Land Rover")).ToList())
            //{
            //        var text = Regex.Replace(tmp.Trim, @"\s+", " ");
            //        tmp.Trim = text;
            //        context.SaveChanges();

            
            //}

          
            ////          var context = new whitmanenterprisewarehouseEntities();

            
            ////foreach (var tmp in context.vincontrolchartselection.Wher.ToList())
            ////{
            ////        var text = Regex.Replace(tmp.Trim, @"\s+", " ");
            ////        tmp.Trim = text;

            
            ////}

            ////context.SaveChanges();

            //MessageBox.Show("Done");
        }

        private void button56_Click(object sender, EventArgs e)
        {
            //var context = new vincontrolscrappingEntities();
            //context.CommandTimeout = 120000;

            ////drgAutoTrader.DataSource =
            ////    context.centraltimezone2.Where(x => String.IsNullOrEmpty(x.Dealershipname)).ToList();
            //var dealerList = context.carscomdealer.Where(x => x.Active == true && x.CarsComInverntoryId > 0).ToList();

            //foreach (var tmp in context.easterntimezone2.Where(x => String.IsNullOrEmpty(x.Dealershipname)).ToList())
            //{
            //    var dealershipName = dealerList.FirstOrDefault(x => x.CarsComInverntoryId == tmp.CarsComDealerId).CarsComDealerName;


            //    tmp.Dealershipname = dealershipName;

            //    context.SaveChanges();

            //}
        }
        //private whitmanenterpriseappraisal GetAppraisalTargetCar(int appraisalId)
        //{
        //    whitmanenterpriseappraisal targetCar;
        //    using (var contextVinControl = new whitmanenterprisewarehouseEntities())
        //    {
        //        targetCar =
        //            contextVinControl.whitmanenterpriseappraisal.FirstOrDefault((x => x.idAppraisal == appraisalId));


        //    }

        //    return targetCar;

        //}

        private ChartSelection GetChartSelectionAutoTraderForAppraisal(int appraisalId)
        {
            var contextVinControl = new whitmanenterprisewarehouseEntities();


            var existingChartSelection =
                contextVinControl.vincontrolchartselection.FirstOrDefault(s => s.listingId == appraisalId && s.screen == "Appraisal" && s.sourceType == "AutoTrader");
            var savedSelection = new ChartSelection();
            if (existingChartSelection != null)
            {
                savedSelection.IsAll = existingChartSelection.isAll != null && Convert.ToBoolean(existingChartSelection.isAll);
                savedSelection.IsCarsCom = existingChartSelection.isCarsCom != null && Convert.ToBoolean(existingChartSelection.isCarsCom);
                savedSelection.IsCertified = existingChartSelection.isCertified != null && Convert.ToBoolean(existingChartSelection.isCertified);
                savedSelection.IsFranchise = existingChartSelection.isFranchise != null && Convert.ToBoolean(existingChartSelection.isFranchise);
                savedSelection.IsIndependant = existingChartSelection.isIndependant != null && Convert.ToBoolean(existingChartSelection.isIndependant);
                savedSelection.Options = existingChartSelection.options != null && existingChartSelection.options != "0"
                                             ? existingChartSelection.options.ToLower()
                                             : "";
                savedSelection.Trims = existingChartSelection.trims != null && existingChartSelection.trims != "0"
                                           ? existingChartSelection.trims.ToLower()
                                           : "";
            }

            return savedSelection;
        }

        private void Button57Click(object sender, EventArgs e)
        {
//            var context = new vincontrolscrappingEntities();

//            var targetCar = GetAppraisalTargetCar(2497);

//            var savedSelection = GetChartSelectionAutoTraderForAppraisal(2497);

//            var chartGraph = new ChartGraph();

//            var list = new List<ChartModel>();

//            var listPrice = new List<decimal>();

//            var modelWord = "";

//            if (!String.IsNullOrEmpty(targetCar.Model))
//            {

//                modelWord = targetCar.Model.Trim();

//                if (targetCar.Make.ToLower().Equals("lexus") && modelWord.Contains(" "))
//                {
//                    modelWord = modelWord.Substring(0, modelWord.IndexOf(" ", System.StringComparison.Ordinal));
//                }

//                modelWord = modelWord.Replace("Sdn", "");

//                modelWord = modelWord.Replace("XL", "");

//                modelWord = modelWord.Replace("Sedan", "");

//                modelWord = modelWord.Replace("Coupe", "");

//                modelWord = modelWord.Replace("Convertible", "");

//                modelWord = modelWord.Replace("4WDTruck", "");
//                modelWord = modelWord.Replace("Hybrid", "");
//                if (modelWord.Contains("MX-5"))
//                    modelWord = "Miata";
//                if (modelWord.Contains("3.5L"))
//                    modelWord = "E350";
//                if (modelWord.Contains("Silverado"))
//                    modelWord = "Silverado";

//                modelWord = modelWord.Replace(" ", "");
//            }
           
//            var query = MapperFactory.GetAutoTraderMarketCarQuery(context, targetCar.ModelYear);

           
//            query = query.Where(i => targetCar.DealershipState == i.State && i.Year == targetCar.ModelYear &&
//                                     i.Make == "Lexus" && i.Model=="RX"
//                                     &&
//                                      !String.IsNullOrEmpty(i.CurrentPrice) && !i.CurrentPrice.Equals("0") &&
//                                     !String.IsNullOrEmpty(i.Mileage));

//            MessageBox.Show(query.Count().ToString());

//            if (savedSelection.IsCertified)
//                query = query.Where(i => i.Certified.HasValue && i.Certified.Value);

//            if (savedSelection.IsFranchise)
//                query = query.Where(i => i.Franchise.HasValue && i.Franchise.Value);
//            else if (savedSelection.IsIndependant)
//                query = query.Where(i => i.Franchise.HasValue && !i.Franchise.Value);

//            if (!string.IsNullOrEmpty(savedSelection.Options))
//            {
//                if (savedSelection.Options.ToLower().Contains("moonroof"))
//                    query = query.Where(i => i.MoonRoof.HasValue && i.MoonRoof.Value);

//                if (savedSelection.Options.ToLower().Contains("sunroof"))
//                    query = query.Where(i => i.SunRoof.HasValue && i.SunRoof.Value);
//            }

//            if (!string.IsNullOrEmpty(savedSelection.Trims))
//            {
//                var arrayTrim = String.Format(",{0},", savedSelection.Trims.Replace(" ", "").ToLower());
//                var trimEqualOther = savedSelection.Trims.ToLower().Equals("other");
//                var trimContainOther = savedSelection.Trims.ToLower().Contains("other");
//                query = trimEqualOther
//                            ? query.Where(i => string.IsNullOrEmpty(i.Trim.Trim()))
//                            : trimContainOther
//                                  ? query.Where(
//                                      i =>
//                                      string.IsNullOrEmpty(i.Trim.Trim()) ||
//                                      arrayTrim.Contains("," + i.Trim.Replace(" ", "").ToLower() + ","))
//                                  : query.Where(
//                                      i =>
//                                      !string.IsNullOrEmpty(i.Trim.Trim()) &&
//                                      arrayTrim.Contains("," + i.Trim.Replace(" ", "").ToLower() + ","));
//            }

//            if (modelWord.Equals("Silverado"))
//            {
//                if (targetCar.Model.Contains("1500"))
//                    query = query.Where(x => x.Trim.Contains("1500"));
//                if (targetCar.Model.Contains("2500"))
//                    query = query.Where(x => x.Trim.Contains("1500"));
//                if (targetCar.Model.Contains("3500"))
//                    query = query.Where(x => x.Trim.Contains("3500"));
//            }

//            if (query.Any())
//            {

//                var table = new HashSet<string>();



//                foreach (var row in query)
//                {
//                    string filtertrim = string.IsNullOrEmpty(row.Trim)
//                                            ? "other"
//                                            : Regex.Replace(row.Trim, @"\s+", " ").ToLower();
//                    table.Add(filtertrim);
//                }



//                foreach (var item in query)
//                {
//                    int distance =
//                        CommonHelper.DistanceBetweenPlaces(Convert.ToDouble(item.Latitude),
//                                                           Convert.ToDouble(item.Longitude),
//                                                           33.680139,
//                                                           -117.908452
//);
//                    int validMileage = 0;
//                    Int32.TryParse(item.Mileage, out validMileage);

//                    int validPrice = 0;
//                    Int32.TryParse(item.CurrentPrice, out validPrice);

//                    if (validMileage > 0 && validPrice > 0 && distance <= 100)
//                    {
//                        var chart = new ChartModel
//                        {
//                            ListingId = item.RegionalListingId,
//                            Title =
//                                new ChartModel.TitleInfo
//                                {
//                                    Make = item.Make,
//                                    Model = item.Model.TrimEnd(),
//                                    Trim = string.IsNullOrEmpty(item.Trim) ? "other" : item.Trim,
//                                    Year = item.Year.GetValueOrDefault()
//                                },
//                            Color =
//                                new ChartModel.ColorInfo { Exterior = item.ExteriorColor, Interior = item.InteriorColor },
//                            Option = new ChartModel.OptionInfo
//                            {
//                                Moonroof = item.MoonRoof.GetValueOrDefault(),
//                                Navigation = item.MoonRoof.GetValueOrDefault(),
//                                Sunroof = item.SunRoof.GetValueOrDefault()
//                            },
//                            Certified = item.Certified.GetValueOrDefault(),
//                            VIN = item.Vin,
//                            Franchise =
//                                item.Franchise.GetValueOrDefault() ? "franchise" : "independant",
//                            Distance = distance,
//                            Uptime = DateTime.Now.Subtract(item.DateAdded.GetValueOrDefault()).Days,
//                            Trims = table.ToList(),
//                            Miles = validMileage,
//                            Price = validPrice,
//                            ThumbnailURL =
//                                String.IsNullOrEmpty(item.AutoTraderThumbnailURL)
//                                    ? ""
//                                    : item.AutoTraderThumbnailURL,

//                            CarsCom = item.CarsCom != null && item.CarsCom.GetValueOrDefault(),
//                            CarsComListingURL =
//                                String.IsNullOrEmpty(item.CarsComListingURL)
//                                    ? ""
//                                    : item.CarsComListingURL,
//                            AutoTrader =
//                                item.AutoTrader != null && item.AutoTrader.GetValueOrDefault(),
//                            AutoTraderListingURL =
//                                String.IsNullOrEmpty(item.AutoTraderListingURL)
//                                    ? ""
//                                    : item.AutoTraderListingURL,
//                            Seller =
//                                new ChartModel.SellerInfo()
//                                {
//                                    SellerName = item.Dealershipname,
//                                    SellerAddress = item.Address
//                                },
//                        };
//                        listPrice.Add(validPrice);
//                        list.Add(chart);
//                    }
//                }
//            }



//            int mileageNumber = 0;
//            Int32.TryParse(targetCar.Mileage, out mileageNumber);

//            int salePriceNumber = 0;
//            Int32.TryParse(targetCar.SalePrice, out salePriceNumber);

//            chartGraph.Target = new ChartGraph.TargetCar()
//            {
//                ListingId = targetCar.idAppraisal,
//                Mileage = mileageNumber,
//                SalePrice = salePriceNumber,

//                ThumbnailImageURL = String.IsNullOrEmpty(targetCar.DefaultImageUrl)
//                       ? "" : targetCar.DefaultImageUrl
//                                                     ,
//                Distance = 0,
//                Title =
//                    new ChartModel.TitleInfo
//                    {
//                        Make = targetCar.Make,
//                        Model = modelWord,
//                        Trim =
//                            string.IsNullOrEmpty(targetCar.Trim)
//                                ? "other"
//                                : targetCar.Trim,
//                        Year = targetCar.ModelYear ?? 2012
//                    },
//                Certified = targetCar.Certified ?? false,
//                Trim = string.IsNullOrEmpty(targetCar.Trim) ? "other" : targetCar.Trim,
//                Seller =;
//                    new ChartModel.SellerInfo()
//                    {
//                        SellerName = targetCar.DealershipName,
//                        SellerAddress =
//                            targetCar.DealershipAddress + " " +
//                            targetCar.DealershipCity +
//                            "," + targetCar.DealershipState +
//                            " " +
//                            targetCar.ZipCode
//                    },
//            };

//            if (listPrice.Any())
//            {
//                // set ranking
//                var ranking = 1;
//                foreach (var price in listPrice)
//                {
//                    if (chartGraph.Target.SalePrice >= price)
//                        ranking++;
//                }
//                chartGraph.Target.Ranking = ranking;

//                chartGraph.Market = new ChartGraph.MarketInfo
//                {
//                    CarsOnMarket = listPrice.Count() + 1,
//                    MinimumPrice = listPrice.Min().ToString("c0"),
//                    AveragePrice = Math.Round(listPrice.Average()).ToString("c0"),
//                    MaximumPrice = listPrice.Max().ToString("c0"),
//                    AverageDays =
//                        Math.Round(list.Select(x => x.Uptime).Average(), 0).ToString(CultureInfo.InvariantCulture)

//                };


//                chartGraph.ChartModels = list.OrderBy(x => x.Distance).ToList();
//            }

//            dataGridView1.DataSource = chartGraph.ChartModels;
        }

        private void Button58Click(object sender, EventArgs e)
        {
            var contextOld = new vincontrolscrappingEntities();

            var contextNew = new vincontrolscrappingNewEntitiy();

            int id = Convert.ToInt32(txtDealerID.Text);

            if (id == 1)
            {

                var regionList = contextOld.region1_carscom.ToList();

                foreach (var tmp in regionList)
                {
                    var regionRecord = new DatabaseModel.region1_carscom()
                                           {
                                               Address = tmp.Address,
                                               DateAdded = tmp.DateAdded,
                                               LastUpdated = tmp.LastUpdated,
                                               Year = tmp.Year,
                                               AutoCheckURL = tmp.AutoCheckURL,
                                               AutoTrader = tmp.AutoTrader,
                                               AutoTraderDealerId = tmp.AutoTraderDealerId,
                                               AutoTraderDescription = tmp.AutoTraderDescription,
                                               AutoTraderInstalledFeatures = tmp.AutoTraderInstalledFeatures,
                                               AutoTraderListingId = tmp.AutoTraderListingId,
                                               AutoTraderListingName = tmp.AutoTraderListingName,
                                               AutoTraderListingURL = tmp.AutoTraderListingURL,
                                               AutoTraderStockNo = tmp.AutoTraderStockNo,
                                               AutoTraderThumbnailURL = tmp.AutoTraderThumbnailURL,
                                               BodyStyle = tmp.BodyStyle,
                                               CarFaxType = tmp.CarFaxType,
                                               CarFaxURL = tmp.CarFaxURL,
                                               CarsCom = tmp.CarsCom,
                                               CarsComDealerId = tmp.CarsComDealerId,
                                               CarsComDescription = tmp.CarsComDescription,
                                               CarsComInstalledFeatures = tmp.CarsComInstalledFeatures,
                                               CarsComListingId = tmp.CarsComListingId,
                                               CarsComListingName = tmp.CarsComListingName,
                                               CarsComListingURL = tmp.CarsComListingURL,
                                               CarsComStockNo = tmp.CarsComStockNo,
                                               CarsComThumbnailURL = tmp.CarsComThumbnailURL,
                                               Certified = tmp.Certified,
                                               City = tmp.City,
                                               CountyName = tmp.CountyName,
                                               CurrentPrice = tmp.CurrentPrice,
                                               Dealershipname = tmp.Dealershipname,
                                               Doors = tmp.Doors,
                                               DriveType = tmp.DriveType,
                                               Ebay = tmp.Ebay,
                                               EbayDescription = tmp.EbayDescription,
                                               EbayInstalledFeatures = tmp.EbayInstalledFeatures,
                                               EbayListingId = tmp.EbayListingId,
                                               EbayListingName = tmp.EbayListingName,
                                               EbayThumbnailURL = tmp.EbayThumbnailURL,
                                               EbayURL = tmp.EbayURL,
                                               Engine = tmp.Engine,
                                               ExteriorColor = tmp.ExteriorColor,
                                               Franchise = tmp.Franchise,
                                               FuelType = tmp.FuelType,
                                               InteriorColor = tmp.InteriorColor,
                                               LastUpdatedPrice = tmp.LastUpdatedPrice,
                                               Latitude = tmp.Latitude,
                                               Longitude = tmp.Longitude,
                                               MSRP = tmp.MSRP,
                                               Make = tmp.Make,
                                               Mileage = tmp.Mileage,
                                               Model = tmp.Model,
                                               MoonRoof = tmp.MoonRoof,
                                               Navigation = tmp.Navigation,
                                               StartingPrice = tmp.StartingPrice,
                                               State = tmp.State,
                                               SunRoof = tmp.SunRoof,
                                               Tranmission = tmp.Tranmission,
                                               Trim = tmp.Trim,
                                               UsedNew = tmp.UsedNew,
                                               Vin = tmp.Vin,
                                               VinControlDealerId = tmp.VinControlDealerId,
                                               ZipCode = tmp.ZipCode

                                           };

                    contextNew.AddToregion1_carscom(regionRecord);
                    contextNew.SaveChanges();
                }
            }
            else if (id == 2)
            {

                var regionList = contextOld.region2_carscom.ToList();

                foreach (var tmp in regionList)
                {
                    var regionRecord = new DatabaseModel.region2_carscom()
                    {
                        Address = tmp.Address,
                        DateAdded = tmp.DateAdded,
                        LastUpdated = tmp.LastUpdated,
                        Year = tmp.Year,
                        AutoCheckURL = tmp.AutoCheckURL,
                        AutoTrader = tmp.AutoTrader,
                        AutoTraderDealerId = tmp.AutoTraderDealerId,
                        AutoTraderDescription = tmp.AutoTraderDescription,
                        AutoTraderInstalledFeatures = tmp.AutoTraderInstalledFeatures,
                        AutoTraderListingId = tmp.AutoTraderListingId,
                        AutoTraderListingName = tmp.AutoTraderListingName,
                        AutoTraderListingURL = tmp.AutoTraderListingURL,
                        AutoTraderStockNo = tmp.AutoTraderStockNo,
                        AutoTraderThumbnailURL = tmp.AutoTraderThumbnailURL,
                        BodyStyle = tmp.BodyStyle,
                        CarFaxType = tmp.CarFaxType,
                        CarFaxURL = tmp.CarFaxURL,
                        CarsCom = tmp.CarsCom,
                        CarsComDealerId = tmp.CarsComDealerId,
                        CarsComDescription = tmp.CarsComDescription,
                        CarsComInstalledFeatures = tmp.CarsComInstalledFeatures,
                        CarsComListingId = tmp.CarsComListingId,
                        CarsComListingName = tmp.CarsComListingName,
                        CarsComListingURL = tmp.CarsComListingURL,
                        CarsComStockNo = tmp.CarsComStockNo,
                        CarsComThumbnailURL = tmp.CarsComThumbnailURL,
                        Certified = tmp.Certified,
                        City = tmp.City,
                        CountyName = tmp.CountyName,
                        CurrentPrice = tmp.CurrentPrice,
                        Dealershipname = tmp.Dealershipname,
                        Doors = tmp.Doors,
                        DriveType = tmp.DriveType,
                        Ebay = tmp.Ebay,
                        EbayDescription = tmp.EbayDescription,
                        EbayInstalledFeatures = tmp.EbayInstalledFeatures,
                        EbayListingId = tmp.EbayListingId,
                        EbayListingName = tmp.EbayListingName,
                        EbayThumbnailURL = tmp.EbayThumbnailURL,
                        EbayURL = tmp.EbayURL,
                        Engine = tmp.Engine,
                        ExteriorColor = tmp.ExteriorColor,
                        Franchise = tmp.Franchise,
                        FuelType = tmp.FuelType,
                        InteriorColor = tmp.InteriorColor,
                        LastUpdatedPrice = tmp.LastUpdatedPrice,
                        Latitude = tmp.Latitude,
                        Longitude = tmp.Longitude,
                        MSRP = tmp.MSRP,
                        Make = tmp.Make,
                        Mileage = tmp.Mileage,
                        Model = tmp.Model,
                        MoonRoof = tmp.MoonRoof,
                        Navigation = tmp.Navigation,
                        StartingPrice = tmp.StartingPrice,
                        State = tmp.State,
                        SunRoof = tmp.SunRoof,
                        Tranmission = tmp.Tranmission,
                        Trim = tmp.Trim,
                        UsedNew = tmp.UsedNew,
                        Vin = tmp.Vin,
                        VinControlDealerId = tmp.VinControlDealerId,
                        ZipCode = tmp.ZipCode

                    };

                    contextNew.AddToregion2_carscom(regionRecord);
                    contextNew.SaveChanges();
                }
            }
            else if (id == 3)
            {

                var regionList = contextOld.region3_carscom.ToList();

                foreach (var tmp in regionList)
                {
                    var regionRecord = new DatabaseModel.region3_carscom()
                    {
                        Address = tmp.Address,
                        DateAdded = tmp.DateAdded,
                        LastUpdated = tmp.LastUpdated,
                        Year = tmp.Year,
                        AutoCheckURL = tmp.AutoCheckURL,
                        AutoTrader = tmp.AutoTrader,
                        AutoTraderDealerId = tmp.AutoTraderDealerId,
                        AutoTraderDescription = tmp.AutoTraderDescription,
                        AutoTraderInstalledFeatures = tmp.AutoTraderInstalledFeatures,
                        AutoTraderListingId = tmp.AutoTraderListingId,
                        AutoTraderListingName = tmp.AutoTraderListingName,
                        AutoTraderListingURL = tmp.AutoTraderListingURL,
                        AutoTraderStockNo = tmp.AutoTraderStockNo,
                        AutoTraderThumbnailURL = tmp.AutoTraderThumbnailURL,
                        BodyStyle = tmp.BodyStyle,
                        CarFaxType = tmp.CarFaxType,
                        CarFaxURL = tmp.CarFaxURL,
                        CarsCom = tmp.CarsCom,
                        CarsComDealerId = tmp.CarsComDealerId,
                        CarsComDescription = tmp.CarsComDescription,
                        CarsComInstalledFeatures = tmp.CarsComInstalledFeatures,
                        CarsComListingId = tmp.CarsComListingId,
                        CarsComListingName = tmp.CarsComListingName,
                        CarsComListingURL = tmp.CarsComListingURL,
                        CarsComStockNo = tmp.CarsComStockNo,
                        CarsComThumbnailURL = tmp.CarsComThumbnailURL,
                        Certified = tmp.Certified,
                        City = tmp.City,
                        CountyName = tmp.CountyName,
                        CurrentPrice = tmp.CurrentPrice,
                        Dealershipname = tmp.Dealershipname,
                        Doors = tmp.Doors,
                        DriveType = tmp.DriveType,
                        Ebay = tmp.Ebay,
                        EbayDescription = tmp.EbayDescription,
                        EbayInstalledFeatures = tmp.EbayInstalledFeatures,
                        EbayListingId = tmp.EbayListingId,
                        EbayListingName = tmp.EbayListingName,
                        EbayThumbnailURL = tmp.EbayThumbnailURL,
                        EbayURL = tmp.EbayURL,
                        Engine = tmp.Engine,
                        ExteriorColor = tmp.ExteriorColor,
                        Franchise = tmp.Franchise,
                        FuelType = tmp.FuelType,
                        InteriorColor = tmp.InteriorColor,
                        LastUpdatedPrice = tmp.LastUpdatedPrice,
                        Latitude = tmp.Latitude,
                        Longitude = tmp.Longitude,
                        MSRP = tmp.MSRP,
                        Make = tmp.Make,
                        Mileage = tmp.Mileage,
                        Model = tmp.Model,
                        MoonRoof = tmp.MoonRoof,
                        Navigation = tmp.Navigation,
                        StartingPrice = tmp.StartingPrice,
                        State = tmp.State,
                        SunRoof = tmp.SunRoof,
                        Tranmission = tmp.Tranmission,
                        Trim = tmp.Trim,
                        UsedNew = tmp.UsedNew,
                        Vin = tmp.Vin,
                        VinControlDealerId = tmp.VinControlDealerId,
                        ZipCode = tmp.ZipCode

                    };

                    contextNew.AddToregion3_carscom(regionRecord);
                    contextNew.SaveChanges();
                }
            }
            else if (id == 41)
            {

                var regionList = contextOld.region4_p1_carscom.ToList();

                foreach (var tmp in regionList)
                {
                    var regionRecord = new DatabaseModel.region4_p1_carscom()
                    {
                        Address = tmp.Address,
                        DateAdded = tmp.DateAdded,
                        LastUpdated = tmp.LastUpdated,
                        Year = tmp.Year,
                        AutoCheckURL = tmp.AutoCheckURL,
                        AutoTrader = tmp.AutoTrader,
                        AutoTraderDealerId = tmp.AutoTraderDealerId,
                        AutoTraderDescription = tmp.AutoTraderDescription,
                        AutoTraderInstalledFeatures = tmp.AutoTraderInstalledFeatures,
                        AutoTraderListingId = tmp.AutoTraderListingId,
                        AutoTraderListingName = tmp.AutoTraderListingName,
                        AutoTraderListingURL = tmp.AutoTraderListingURL,
                        AutoTraderStockNo = tmp.AutoTraderStockNo,
                        AutoTraderThumbnailURL = tmp.AutoTraderThumbnailURL,
                        BodyStyle = tmp.BodyStyle,
                        CarFaxType = tmp.CarFaxType,
                        CarFaxURL = tmp.CarFaxURL,
                        CarsCom = tmp.CarsCom,
                        CarsComDealerId = tmp.CarsComDealerId,
                        CarsComDescription = tmp.CarsComDescription,
                        CarsComInstalledFeatures = tmp.CarsComInstalledFeatures,
                        CarsComListingId = tmp.CarsComListingId,
                        CarsComListingName = tmp.CarsComListingName,
                        CarsComListingURL = tmp.CarsComListingURL,
                        CarsComStockNo = tmp.CarsComStockNo,
                        CarsComThumbnailURL = tmp.CarsComThumbnailURL,
                        Certified = tmp.Certified,
                        City = tmp.City,
                        CountyName = tmp.CountyName,
                        CurrentPrice = tmp.CurrentPrice,
                        Dealershipname = tmp.Dealershipname,
                        Doors = tmp.Doors,
                        DriveType = tmp.DriveType,
                        Ebay = tmp.Ebay,
                        EbayDescription = tmp.EbayDescription,
                        EbayInstalledFeatures = tmp.EbayInstalledFeatures,
                        EbayListingId = tmp.EbayListingId,
                        EbayListingName = tmp.EbayListingName,
                        EbayThumbnailURL = tmp.EbayThumbnailURL,
                        EbayURL = tmp.EbayURL,
                        Engine = tmp.Engine,
                        ExteriorColor = tmp.ExteriorColor,
                        Franchise = tmp.Franchise,
                        FuelType = tmp.FuelType,
                        InteriorColor = tmp.InteriorColor,
                        LastUpdatedPrice = tmp.LastUpdatedPrice,
                        Latitude = tmp.Latitude,
                        Longitude = tmp.Longitude,
                        MSRP = tmp.MSRP,
                        Make = tmp.Make,
                        Mileage = tmp.Mileage,
                        Model = tmp.Model,
                        MoonRoof = tmp.MoonRoof,
                        Navigation = tmp.Navigation,
                        StartingPrice = tmp.StartingPrice,
                        State = tmp.State,
                        SunRoof = tmp.SunRoof,
                        Tranmission = tmp.Tranmission,
                        Trim = tmp.Trim,
                        UsedNew = tmp.UsedNew,
                        Vin = tmp.Vin,
                        VinControlDealerId = tmp.VinControlDealerId,
                        ZipCode = tmp.ZipCode

                    };

                    contextNew.AddToregion4_p1_carscom(regionRecord);
                    contextNew.SaveChanges();
                }
            }
            else if (id == 42)
            {

                var regionList = contextOld.region4_p2_carscom.ToList();

                foreach (var tmp in regionList)
                {
                    var regionRecord = new DatabaseModel.region4_p2_carscom()
                    {
                        Address = tmp.Address,
                        DateAdded = tmp.DateAdded,
                        LastUpdated = tmp.LastUpdated,
                        Year = tmp.Year,
                        AutoCheckURL = tmp.AutoCheckURL,
                        AutoTrader = tmp.AutoTrader,
                        AutoTraderDealerId = tmp.AutoTraderDealerId,
                        AutoTraderDescription = tmp.AutoTraderDescription,
                        AutoTraderInstalledFeatures = tmp.AutoTraderInstalledFeatures,
                        AutoTraderListingId = tmp.AutoTraderListingId,
                        AutoTraderListingName = tmp.AutoTraderListingName,
                        AutoTraderListingURL = tmp.AutoTraderListingURL,
                        AutoTraderStockNo = tmp.AutoTraderStockNo,
                        AutoTraderThumbnailURL = tmp.AutoTraderThumbnailURL,
                        BodyStyle = tmp.BodyStyle,
                        CarFaxType = tmp.CarFaxType,
                        CarFaxURL = tmp.CarFaxURL,
                        CarsCom = tmp.CarsCom,
                        CarsComDealerId = tmp.CarsComDealerId,
                        CarsComDescription = tmp.CarsComDescription,
                        CarsComInstalledFeatures = tmp.CarsComInstalledFeatures,
                        CarsComListingId = tmp.CarsComListingId,
                        CarsComListingName = tmp.CarsComListingName,
                        CarsComListingURL = tmp.CarsComListingURL,
                        CarsComStockNo = tmp.CarsComStockNo,
                        CarsComThumbnailURL = tmp.CarsComThumbnailURL,
                        Certified = tmp.Certified,
                        City = tmp.City,
                        CountyName = tmp.CountyName,
                        CurrentPrice = tmp.CurrentPrice,
                        Dealershipname = tmp.Dealershipname,
                        Doors = tmp.Doors,
                        DriveType = tmp.DriveType,
                        Ebay = tmp.Ebay,
                        EbayDescription = tmp.EbayDescription,
                        EbayInstalledFeatures = tmp.EbayInstalledFeatures,
                        EbayListingId = tmp.EbayListingId,
                        EbayListingName = tmp.EbayListingName,
                        EbayThumbnailURL = tmp.EbayThumbnailURL,
                        EbayURL = tmp.EbayURL,
                        Engine = tmp.Engine,
                        ExteriorColor = tmp.ExteriorColor,
                        Franchise = tmp.Franchise,
                        FuelType = tmp.FuelType,
                        InteriorColor = tmp.InteriorColor,
                        LastUpdatedPrice = tmp.LastUpdatedPrice,
                        Latitude = tmp.Latitude,
                        Longitude = tmp.Longitude,
                        MSRP = tmp.MSRP,
                        Make = tmp.Make,
                        Mileage = tmp.Mileage,
                        Model = tmp.Model,
                        MoonRoof = tmp.MoonRoof,
                        Navigation = tmp.Navigation,
                        StartingPrice = tmp.StartingPrice,
                        State = tmp.State,
                        SunRoof = tmp.SunRoof,
                        Tranmission = tmp.Tranmission,
                        Trim = tmp.Trim,
                        UsedNew = tmp.UsedNew,
                        Vin = tmp.Vin,
                        VinControlDealerId = tmp.VinControlDealerId,
                        ZipCode = tmp.ZipCode

                    };

                    contextNew.AddToregion4_p2_carscom(regionRecord);
                    contextNew.SaveChanges();
                }
            }
            else if (id == 51)
            {

                var regionList = contextOld.region5_p1_carscom;

                foreach (var tmp in regionList)
                {
                    var regionRecord = new DatabaseModel.region5_p1_carscom()
                    {
                        Address = tmp.Address,
                        DateAdded = tmp.DateAdded,
                        LastUpdated = tmp.LastUpdated,
                        Year = tmp.Year,
                        AutoCheckURL = tmp.AutoCheckURL,
                        AutoTrader = tmp.AutoTrader,
                        AutoTraderDealerId = tmp.AutoTraderDealerId,
                        AutoTraderDescription = tmp.AutoTraderDescription,
                        AutoTraderInstalledFeatures = tmp.AutoTraderInstalledFeatures,
                        AutoTraderListingId = tmp.AutoTraderListingId,
                        AutoTraderListingName = tmp.AutoTraderListingName,
                        AutoTraderListingURL = tmp.AutoTraderListingURL,
                        AutoTraderStockNo = tmp.AutoTraderStockNo,
                        AutoTraderThumbnailURL = tmp.AutoTraderThumbnailURL,
                        BodyStyle = tmp.BodyStyle,
                        CarFaxType = tmp.CarFaxType,
                        CarFaxURL = tmp.CarFaxURL,
                        CarsCom = tmp.CarsCom,
                        CarsComDealerId = tmp.CarsComDealerId,
                        CarsComDescription = tmp.CarsComDescription,
                        CarsComInstalledFeatures = tmp.CarsComInstalledFeatures,
                        CarsComListingId = tmp.CarsComListingId,
                        CarsComListingName = tmp.CarsComListingName,
                        CarsComListingURL = tmp.CarsComListingURL,
                        CarsComStockNo = tmp.CarsComStockNo,
                        CarsComThumbnailURL = tmp.CarsComThumbnailURL,
                        Certified = tmp.Certified,
                        City = tmp.City,
                        CountyName = tmp.CountyName,
                        CurrentPrice = tmp.CurrentPrice,
                        Dealershipname = tmp.Dealershipname,
                        Doors = tmp.Doors,
                        DriveType = tmp.DriveType,
                        Ebay = tmp.Ebay,
                        EbayDescription = tmp.EbayDescription,
                        EbayInstalledFeatures = tmp.EbayInstalledFeatures,
                        EbayListingId = tmp.EbayListingId,
                        EbayListingName = tmp.EbayListingName,
                        EbayThumbnailURL = tmp.EbayThumbnailURL,
                        EbayURL = tmp.EbayURL,
                        Engine = tmp.Engine,
                        ExteriorColor = tmp.ExteriorColor,
                        Franchise = tmp.Franchise,
                        FuelType = tmp.FuelType,
                        InteriorColor = tmp.InteriorColor,
                        LastUpdatedPrice = tmp.LastUpdatedPrice,
                        Latitude = tmp.Latitude,
                        Longitude = tmp.Longitude,
                        MSRP = tmp.MSRP,
                        Make = tmp.Make,
                        Mileage = tmp.Mileage,
                        Model = tmp.Model,
                        MoonRoof = tmp.MoonRoof,
                        Navigation = tmp.Navigation,
                        StartingPrice = tmp.StartingPrice,
                        State = tmp.State,
                        SunRoof = tmp.SunRoof,
                        Tranmission = tmp.Tranmission,
                        Trim = tmp.Trim,
                        UsedNew = tmp.UsedNew,
                        Vin = tmp.Vin,
                        VinControlDealerId = tmp.VinControlDealerId,
                        ZipCode = tmp.ZipCode

                    };

                    contextNew.AddToregion5_p1_carscom(regionRecord);
                    contextNew.SaveChanges();
                }
            }
            else if (id == 52)
            {

                var regionList = contextOld.region5_p2_carscom;

                foreach (var tmp in regionList)
                {
                    var regionRecord = new DatabaseModel.region5_p2_carscom()
                    {
                        Address = tmp.Address,
                        DateAdded = tmp.DateAdded,
                        LastUpdated = tmp.LastUpdated,
                        Year = tmp.Year,
                        AutoCheckURL = tmp.AutoCheckURL,
                        AutoTrader = tmp.AutoTrader,
                        AutoTraderDealerId = tmp.AutoTraderDealerId,
                        AutoTraderDescription = tmp.AutoTraderDescription,
                        AutoTraderInstalledFeatures = tmp.AutoTraderInstalledFeatures,
                        AutoTraderListingId = tmp.AutoTraderListingId,
                        AutoTraderListingName = tmp.AutoTraderListingName,
                        AutoTraderListingURL = tmp.AutoTraderListingURL,
                        AutoTraderStockNo = tmp.AutoTraderStockNo,
                        AutoTraderThumbnailURL = tmp.AutoTraderThumbnailURL,
                        BodyStyle = tmp.BodyStyle,
                        CarFaxType = tmp.CarFaxType,
                        CarFaxURL = tmp.CarFaxURL,
                        CarsCom = tmp.CarsCom,
                        CarsComDealerId = tmp.CarsComDealerId,
                        CarsComDescription = tmp.CarsComDescription,
                        CarsComInstalledFeatures = tmp.CarsComInstalledFeatures,
                        CarsComListingId = tmp.CarsComListingId,
                        CarsComListingName = tmp.CarsComListingName,
                        CarsComListingURL = tmp.CarsComListingURL,
                        CarsComStockNo = tmp.CarsComStockNo,
                        CarsComThumbnailURL = tmp.CarsComThumbnailURL,
                        Certified = tmp.Certified,
                        City = tmp.City,
                        CountyName = tmp.CountyName,
                        CurrentPrice = tmp.CurrentPrice,
                        Dealershipname = tmp.Dealershipname,
                        Doors = tmp.Doors,
                        DriveType = tmp.DriveType,
                        Ebay = tmp.Ebay,
                        EbayDescription = tmp.EbayDescription,
                        EbayInstalledFeatures = tmp.EbayInstalledFeatures,
                        EbayListingId = tmp.EbayListingId,
                        EbayListingName = tmp.EbayListingName,
                        EbayThumbnailURL = tmp.EbayThumbnailURL,
                        EbayURL = tmp.EbayURL,
                        Engine = tmp.Engine,
                        ExteriorColor = tmp.ExteriorColor,
                        Franchise = tmp.Franchise,
                        FuelType = tmp.FuelType,
                        InteriorColor = tmp.InteriorColor,
                        LastUpdatedPrice = tmp.LastUpdatedPrice,
                        Latitude = tmp.Latitude,
                        Longitude = tmp.Longitude,
                        MSRP = tmp.MSRP,
                        Make = tmp.Make,
                        Mileage = tmp.Mileage,
                        Model = tmp.Model,
                        MoonRoof = tmp.MoonRoof,
                        Navigation = tmp.Navigation,
                        StartingPrice = tmp.StartingPrice,
                        State = tmp.State,
                        SunRoof = tmp.SunRoof,
                        Tranmission = tmp.Tranmission,
                        Trim = tmp.Trim,
                        UsedNew = tmp.UsedNew,
                        Vin = tmp.Vin,
                        VinControlDealerId = tmp.VinControlDealerId,
                        ZipCode = tmp.ZipCode

                    };

                    contextNew.AddToregion5_p2_carscom(regionRecord);
                    contextNew.SaveChanges();
                }
            }
            else if (id == 6)
            {

                var regionList = contextOld.region6_carscom;

                foreach (var tmp in regionList)
                {
                    var regionRecord = new DatabaseModel.region6_carscom()
                    {
                        Address = tmp.Address,
                        DateAdded = tmp.DateAdded,
                        LastUpdated = tmp.LastUpdated,
                        Year = tmp.Year,
                        AutoCheckURL = tmp.AutoCheckURL,
                        AutoTrader = tmp.AutoTrader,
                        AutoTraderDealerId = tmp.AutoTraderDealerId,
                        AutoTraderDescription = tmp.AutoTraderDescription,
                        AutoTraderInstalledFeatures = tmp.AutoTraderInstalledFeatures,
                        AutoTraderListingId = tmp.AutoTraderListingId,
                        AutoTraderListingName = tmp.AutoTraderListingName,
                        AutoTraderListingURL = tmp.AutoTraderListingURL,
                        AutoTraderStockNo = tmp.AutoTraderStockNo,
                        AutoTraderThumbnailURL = tmp.AutoTraderThumbnailURL,
                        BodyStyle = tmp.BodyStyle,
                        CarFaxType = tmp.CarFaxType,
                        CarFaxURL = tmp.CarFaxURL,
                        CarsCom = tmp.CarsCom,
                        CarsComDealerId = tmp.CarsComDealerId,
                        CarsComDescription = tmp.CarsComDescription,
                        CarsComInstalledFeatures = tmp.CarsComInstalledFeatures,
                        CarsComListingId = tmp.CarsComListingId,
                        CarsComListingName = tmp.CarsComListingName,
                        CarsComListingURL = tmp.CarsComListingURL,
                        CarsComStockNo = tmp.CarsComStockNo,
                        CarsComThumbnailURL = tmp.CarsComThumbnailURL,
                        Certified = tmp.Certified,
                        City = tmp.City,
                        CountyName = tmp.CountyName,
                        CurrentPrice = tmp.CurrentPrice,
                        Dealershipname = tmp.Dealershipname,
                        Doors = tmp.Doors,
                        DriveType = tmp.DriveType,
                        Ebay = tmp.Ebay,
                        EbayDescription = tmp.EbayDescription,
                        EbayInstalledFeatures = tmp.EbayInstalledFeatures,
                        EbayListingId = tmp.EbayListingId,
                        EbayListingName = tmp.EbayListingName,
                        EbayThumbnailURL = tmp.EbayThumbnailURL,
                        EbayURL = tmp.EbayURL,
                        Engine = tmp.Engine,
                        ExteriorColor = tmp.ExteriorColor,
                        Franchise = tmp.Franchise,
                        FuelType = tmp.FuelType,
                        InteriorColor = tmp.InteriorColor,
                        LastUpdatedPrice = tmp.LastUpdatedPrice,
                        Latitude = tmp.Latitude,
                        Longitude = tmp.Longitude,
                        MSRP = tmp.MSRP,
                        Make = tmp.Make,
                        Mileage = tmp.Mileage,
                        Model = tmp.Model,
                        MoonRoof = tmp.MoonRoof,
                        Navigation = tmp.Navigation,
                        StartingPrice = tmp.StartingPrice,
                        State = tmp.State,
                        SunRoof = tmp.SunRoof,
                        Tranmission = tmp.Tranmission,
                        Trim = tmp.Trim,
                        UsedNew = tmp.UsedNew,
                        Vin = tmp.Vin,
                        VinControlDealerId = tmp.VinControlDealerId,
                        ZipCode = tmp.ZipCode

                    };

                    contextNew.AddToregion6_carscom(regionRecord);
                    contextNew.SaveChanges();
                }
            }
            else if (id == 7)
            {

                var regionList = contextOld.region7_carscom;

                foreach (var tmp in regionList)
                {
                    var regionRecord = new DatabaseModel.region7_carscom()
                    {
                        Address = tmp.Address,
                        DateAdded = tmp.DateAdded,
                        LastUpdated = tmp.LastUpdated,
                        Year = tmp.Year,
                        AutoCheckURL = tmp.AutoCheckURL,
                        AutoTrader = tmp.AutoTrader,
                        AutoTraderDealerId = tmp.AutoTraderDealerId,
                        AutoTraderDescription = tmp.AutoTraderDescription,
                        AutoTraderInstalledFeatures = tmp.AutoTraderInstalledFeatures,
                        AutoTraderListingId = tmp.AutoTraderListingId,
                        AutoTraderListingName = tmp.AutoTraderListingName,
                        AutoTraderListingURL = tmp.AutoTraderListingURL,
                        AutoTraderStockNo = tmp.AutoTraderStockNo,
                        AutoTraderThumbnailURL = tmp.AutoTraderThumbnailURL,
                        BodyStyle = tmp.BodyStyle,
                        CarFaxType = tmp.CarFaxType,
                        CarFaxURL = tmp.CarFaxURL,
                        CarsCom = tmp.CarsCom,
                        CarsComDealerId = tmp.CarsComDealerId,
                        CarsComDescription = tmp.CarsComDescription,
                        CarsComInstalledFeatures = tmp.CarsComInstalledFeatures,
                        CarsComListingId = tmp.CarsComListingId,
                        CarsComListingName = tmp.CarsComListingName,
                        CarsComListingURL = tmp.CarsComListingURL,
                        CarsComStockNo = tmp.CarsComStockNo,
                        CarsComThumbnailURL = tmp.CarsComThumbnailURL,
                        Certified = tmp.Certified,
                        City = tmp.City,
                        CountyName = tmp.CountyName,
                        CurrentPrice = tmp.CurrentPrice,
                        Dealershipname = tmp.Dealershipname,
                        Doors = tmp.Doors,
                        DriveType = tmp.DriveType,
                        Ebay = tmp.Ebay,
                        EbayDescription = tmp.EbayDescription,
                        EbayInstalledFeatures = tmp.EbayInstalledFeatures,
                        EbayListingId = tmp.EbayListingId,
                        EbayListingName = tmp.EbayListingName,
                        EbayThumbnailURL = tmp.EbayThumbnailURL,
                        EbayURL = tmp.EbayURL,
                        Engine = tmp.Engine,
                        ExteriorColor = tmp.ExteriorColor,
                        Franchise = tmp.Franchise,
                        FuelType = tmp.FuelType,
                        InteriorColor = tmp.InteriorColor,
                        LastUpdatedPrice = tmp.LastUpdatedPrice,
                        Latitude = tmp.Latitude,
                        Longitude = tmp.Longitude,
                        MSRP = tmp.MSRP,
                        Make = tmp.Make,
                        Mileage = tmp.Mileage,
                        Model = tmp.Model,
                        MoonRoof = tmp.MoonRoof,
                        Navigation = tmp.Navigation,
                        StartingPrice = tmp.StartingPrice,
                        State = tmp.State,
                        SunRoof = tmp.SunRoof,
                        Tranmission = tmp.Tranmission,
                        Trim = tmp.Trim,
                        UsedNew = tmp.UsedNew,
                        Vin = tmp.Vin,
                        VinControlDealerId = tmp.VinControlDealerId,
                        ZipCode = tmp.ZipCode

                    };

                    contextNew.AddToregion7_carscom(regionRecord);
                    contextNew.SaveChanges();
                }
            }
            else if (id == 8)
            {

                var regionList = contextOld.region8_carscom;

                foreach (var tmp in regionList)
                {
                    var regionRecord = new DatabaseModel.region8_carscom()
                    {
                        Address = tmp.Address,
                        DateAdded = tmp.DateAdded,
                        LastUpdated = tmp.LastUpdated,
                        Year = tmp.Year,
                        AutoCheckURL = tmp.AutoCheckURL,
                        AutoTrader = tmp.AutoTrader,
                        AutoTraderDealerId = tmp.AutoTraderDealerId,
                        AutoTraderDescription = tmp.AutoTraderDescription,
                        AutoTraderInstalledFeatures = tmp.AutoTraderInstalledFeatures,
                        AutoTraderListingId = tmp.AutoTraderListingId,
                        AutoTraderListingName = tmp.AutoTraderListingName,
                        AutoTraderListingURL = tmp.AutoTraderListingURL,
                        AutoTraderStockNo = tmp.AutoTraderStockNo,
                        AutoTraderThumbnailURL = tmp.AutoTraderThumbnailURL,
                        BodyStyle = tmp.BodyStyle,
                        CarFaxType = tmp.CarFaxType,
                        CarFaxURL = tmp.CarFaxURL,
                        CarsCom = tmp.CarsCom,
                        CarsComDealerId = tmp.CarsComDealerId,
                        CarsComDescription = tmp.CarsComDescription,
                        CarsComInstalledFeatures = tmp.CarsComInstalledFeatures,
                        CarsComListingId = tmp.CarsComListingId,
                        CarsComListingName = tmp.CarsComListingName,
                        CarsComListingURL = tmp.CarsComListingURL,
                        CarsComStockNo = tmp.CarsComStockNo,
                        CarsComThumbnailURL = tmp.CarsComThumbnailURL,
                        Certified = tmp.Certified,
                        City = tmp.City,
                        CountyName = tmp.CountyName,
                        CurrentPrice = tmp.CurrentPrice,
                        Dealershipname = tmp.Dealershipname,
                        Doors = tmp.Doors,
                        DriveType = tmp.DriveType,
                        Ebay = tmp.Ebay,
                        EbayDescription = tmp.EbayDescription,
                        EbayInstalledFeatures = tmp.EbayInstalledFeatures,
                        EbayListingId = tmp.EbayListingId,
                        EbayListingName = tmp.EbayListingName,
                        EbayThumbnailURL = tmp.EbayThumbnailURL,
                        EbayURL = tmp.EbayURL,
                        Engine = tmp.Engine,
                        ExteriorColor = tmp.ExteriorColor,
                        Franchise = tmp.Franchise,
                        FuelType = tmp.FuelType,
                        InteriorColor = tmp.InteriorColor,
                        LastUpdatedPrice = tmp.LastUpdatedPrice,
                        Latitude = tmp.Latitude,
                        Longitude = tmp.Longitude,
                        MSRP = tmp.MSRP,
                        Make = tmp.Make,
                        Mileage = tmp.Mileage,
                        Model = tmp.Model,
                        MoonRoof = tmp.MoonRoof,
                        Navigation = tmp.Navigation,
                        StartingPrice = tmp.StartingPrice,
                        State = tmp.State,
                        SunRoof = tmp.SunRoof,
                        Tranmission = tmp.Tranmission,
                        Trim = tmp.Trim,
                        UsedNew = tmp.UsedNew,
                        Vin = tmp.Vin,
                        VinControlDealerId = tmp.VinControlDealerId,
                        ZipCode = tmp.ZipCode

                    };

                    contextNew.AddToregion8_carscom(regionRecord);
                    contextNew.SaveChanges();
                }
            }
            else if (id == 9)
            {

                var regionList = contextOld.region9_carscom;

                foreach (var tmp in regionList)
                {
                    var regionRecord = new DatabaseModel.region9_carscom()
                    {
                        Address = tmp.Address,
                        DateAdded = tmp.DateAdded,
                        LastUpdated = tmp.LastUpdated,
                        Year = tmp.Year,
                        AutoCheckURL = tmp.AutoCheckURL,
                        AutoTrader = tmp.AutoTrader,
                        AutoTraderDealerId = tmp.AutoTraderDealerId,
                        AutoTraderDescription = tmp.AutoTraderDescription,
                        AutoTraderInstalledFeatures = tmp.AutoTraderInstalledFeatures,
                        AutoTraderListingId = tmp.AutoTraderListingId,
                        AutoTraderListingName = tmp.AutoTraderListingName,
                        AutoTraderListingURL = tmp.AutoTraderListingURL,
                        AutoTraderStockNo = tmp.AutoTraderStockNo,
                        AutoTraderThumbnailURL = tmp.AutoTraderThumbnailURL,
                        BodyStyle = tmp.BodyStyle,
                        CarFaxType = tmp.CarFaxType,
                        CarFaxURL = tmp.CarFaxURL,
                        CarsCom = tmp.CarsCom,
                        CarsComDealerId = tmp.CarsComDealerId,
                        CarsComDescription = tmp.CarsComDescription,
                        CarsComInstalledFeatures = tmp.CarsComInstalledFeatures,
                        CarsComListingId = tmp.CarsComListingId,
                        CarsComListingName = tmp.CarsComListingName,
                        CarsComListingURL = tmp.CarsComListingURL,
                        CarsComStockNo = tmp.CarsComStockNo,
                        CarsComThumbnailURL = tmp.CarsComThumbnailURL,
                        Certified = tmp.Certified,
                        City = tmp.City,
                        CountyName = tmp.CountyName,
                        CurrentPrice = tmp.CurrentPrice,
                        Dealershipname = tmp.Dealershipname,
                        Doors = tmp.Doors,
                        DriveType = tmp.DriveType,
                        Ebay = tmp.Ebay,
                        EbayDescription = tmp.EbayDescription,
                        EbayInstalledFeatures = tmp.EbayInstalledFeatures,
                        EbayListingId = tmp.EbayListingId,
                        EbayListingName = tmp.EbayListingName,
                        EbayThumbnailURL = tmp.EbayThumbnailURL,
                        EbayURL = tmp.EbayURL,
                        Engine = tmp.Engine,
                        ExteriorColor = tmp.ExteriorColor,
                        Franchise = tmp.Franchise,
                        FuelType = tmp.FuelType,
                        InteriorColor = tmp.InteriorColor,
                        LastUpdatedPrice = tmp.LastUpdatedPrice,
                        Latitude = tmp.Latitude,
                        Longitude = tmp.Longitude,
                        MSRP = tmp.MSRP,
                        Make = tmp.Make,
                        Mileage = tmp.Mileage,
                        Model = tmp.Model,
                        MoonRoof = tmp.MoonRoof,
                        Navigation = tmp.Navigation,
                        StartingPrice = tmp.StartingPrice,
                        State = tmp.State,
                        SunRoof = tmp.SunRoof,
                        Tranmission = tmp.Tranmission,
                        Trim = tmp.Trim,
                        UsedNew = tmp.UsedNew,
                        Vin = tmp.Vin,
                        VinControlDealerId = tmp.VinControlDealerId,
                        ZipCode = tmp.ZipCode

                    };

                    contextNew.AddToregion9_carscom(regionRecord);
                    contextNew.SaveChanges();
                }
            }
            else if (id == 10)
            {

                var regionList = contextOld.region10_carscom;

                foreach (var tmp in regionList)
                {
                    var regionRecord = new DatabaseModel.region10_carscom()
                    {
                        Address = tmp.Address,
                        DateAdded = tmp.DateAdded,
                        LastUpdated = tmp.LastUpdated,
                        Year = tmp.Year,
                        AutoCheckURL = tmp.AutoCheckURL,
                        AutoTrader = tmp.AutoTrader,
                        AutoTraderDealerId = tmp.AutoTraderDealerId,
                        AutoTraderDescription = tmp.AutoTraderDescription,
                        AutoTraderInstalledFeatures = tmp.AutoTraderInstalledFeatures,
                        AutoTraderListingId = tmp.AutoTraderListingId,
                        AutoTraderListingName = tmp.AutoTraderListingName,
                        AutoTraderListingURL = tmp.AutoTraderListingURL,
                        AutoTraderStockNo = tmp.AutoTraderStockNo,
                        AutoTraderThumbnailURL = tmp.AutoTraderThumbnailURL,
                        BodyStyle = tmp.BodyStyle,
                        CarFaxType = tmp.CarFaxType,
                        CarFaxURL = tmp.CarFaxURL,
                        CarsCom = tmp.CarsCom,
                        CarsComDealerId = tmp.CarsComDealerId,
                        CarsComDescription = tmp.CarsComDescription,
                        CarsComInstalledFeatures = tmp.CarsComInstalledFeatures,
                        CarsComListingId = tmp.CarsComListingId,
                        CarsComListingName = tmp.CarsComListingName,
                        CarsComListingURL = tmp.CarsComListingURL,
                        CarsComStockNo = tmp.CarsComStockNo,
                        CarsComThumbnailURL = tmp.CarsComThumbnailURL,
                        Certified = tmp.Certified,
                        City = tmp.City,
                        CountyName = tmp.CountyName,
                        CurrentPrice = tmp.CurrentPrice,
                        Dealershipname = tmp.Dealershipname,
                        Doors = tmp.Doors,
                        DriveType = tmp.DriveType,
                        Ebay = tmp.Ebay,
                        EbayDescription = tmp.EbayDescription,
                        EbayInstalledFeatures = tmp.EbayInstalledFeatures,
                        EbayListingId = tmp.EbayListingId,
                        EbayListingName = tmp.EbayListingName,
                        EbayThumbnailURL = tmp.EbayThumbnailURL,
                        EbayURL = tmp.EbayURL,
                        Engine = tmp.Engine,
                        ExteriorColor = tmp.ExteriorColor,
                        Franchise = tmp.Franchise,
                        FuelType = tmp.FuelType,
                        InteriorColor = tmp.InteriorColor,
                        LastUpdatedPrice = tmp.LastUpdatedPrice,
                        Latitude = tmp.Latitude,
                        Longitude = tmp.Longitude,
                        MSRP = tmp.MSRP,
                        Make = tmp.Make,
                        Mileage = tmp.Mileage,
                        Model = tmp.Model,
                        MoonRoof = tmp.MoonRoof,
                        Navigation = tmp.Navigation,
                        StartingPrice = tmp.StartingPrice,
                        State = tmp.State,
                        SunRoof = tmp.SunRoof,
                        Tranmission = tmp.Tranmission,
                        Trim = tmp.Trim,
                        UsedNew = tmp.UsedNew,
                        Vin = tmp.Vin,
                        VinControlDealerId = tmp.VinControlDealerId,
                        ZipCode = tmp.ZipCode

                    };

                    contextNew.AddToregion10_carscom(regionRecord);
                    contextNew.SaveChanges();
                }
            }
          
            MessageBox.Show("Done");
        }

        private void button59_Click(object sender, EventArgs e)
        {
            var context = new whitmanenterprisewarehouseEntities();

            var appraisal = context.whitmanenterpriseappraisal.First(x => x.idAppraisal == 2677);

            pictureBox1.Image=new Bitmap(new MemoryStream(appraisal.Photo));


        }

        private void button60_Click(object sender, EventArgs e)
        {
              var contextNew = new whitmanenterprisewarehouseEntities();

            var context = new whitmanenterprisewarehouseEntities1();

            var appraisalList = context.whitmanenterpriseappraisal.Where(x => x.Photo != null);

            drgAutoTrader.DataSource = appraisalList;

            Refresh();

            foreach (var tmp in appraisalList)
            {
                if(contextNew.whitmanenterpriseappraisal.Any(x => x.idAppraisal == tmp.idAppraisal))
                {
                    var searchResult =
                        contextNew.whitmanenterpriseappraisal.First(x => x.idAppraisal == tmp.idAppraisal);



                    searchResult.Photo = tmp.Photo;

                    searchResult.Signature = tmp.Signature;


                    contextNew.SaveChanges();
                }
            }

           

            drgAutoTrader.DataSource = appraisalList;

        }

        private void button61_Click(object sender, EventArgs e)
        {
            DataTable dtTemporaryNoheader = null;

            DataTable dtDNS = null;

            int DID = Convert.ToInt32(txtDealerID.Text);

            var path = "/" + DID + ".txt";

           var clappCredential = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["CLAPPFTPUsername"].ToString(CultureInfo.InvariantCulture), System.Configuration.ConfigurationManager.AppSettings["CLAPPFTPPassword"].ToString(CultureInfo.InvariantCulture));

           CachedCsvReader csv = FTPHelper.GetInventoryAutoTraderFormatWithoutHeade222r(path, clappCredential);


            dtTemporaryNoheader = new DataTable();

            dtTemporaryNoheader.Load(csv);

            dataGridView1.DataSource = dtTemporaryNoheader;

        }




    }
}
