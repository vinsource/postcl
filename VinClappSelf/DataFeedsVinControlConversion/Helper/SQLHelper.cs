using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Configuration;
using System.Globalization;
using System.Xml;
using System.Reflection;
using System.IO;
using DataFeedsVinControlConversion.DatabaseModelVinControlScrappingData;
using DataFeedsVinControlConversion.Helper;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Linq;
using LumenWorks.Framework.IO.Csv;
using System.Net.FtpClient;
using System.Net;
using DataFeedsVinControlConversion.DatabaseModel;
using System.Data.Common;
using System.Data.Objects;

namespace DataFeedsVinControlConversion
{
    public sealed class SQLHelper
    {
        public SQLHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static  DataTable _dt;
        private static int _columnCount;

        public static void SaveStreamToFile(string fileFullPath, Stream stream)
        {
            if (stream.Length == 0) return;

            // Create a FileStream object to write a stream to a file
            using (FileStream fileStream = System.IO.File.Create(fileFullPath, (int)stream.Length))
            {
                // Fill the bytes[] array with the stream data
                byte[] bytesInStream = new byte[stream.Length];
                stream.Read(bytesInStream, 0, (int)bytesInStream.Length);

                // Use FileStream object to write to the specified file
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            }
        }



        public static void PopulateDataTableFromTextFile(StreamReader srdr)
        {
            StringBuilder builder = new StringBuilder();
            String strLine = String.Empty;
            //Int32 iLineCount = 0;
            do
            {
                strLine = srdr.ReadLine();
                if (strLine == null)
                { break; }
                
                    strLine = strLine.Replace("\"http://www.theautolocators.com\"", "");
                    
                    builder.AppendLine(strLine);
                    //_dt = CreateDataTableForTabbedData(strLine);
                
                //AddDataRowToTable(strLine, _dt);
            } while (true);
            File.WriteAllText(@"C:\CRAIGSLISTDATAFEED\23680.txt", builder.ToString());

          
            


            //_dt= changeColumName(_dt);
           
        }

        private static DataTable changeColumName(DataTable dt)
        {
            dt.Columns["Column-0"].ColumnName = "DealerID";
            dt.Columns["Column-1"].ColumnName = "VIN";
            dt.Columns["Column-2"].ColumnName = "StockNumber";
            dt.Columns["Column-3"].ColumnName = "Type";
            dt.Columns["Column-4"].ColumnName = "Year";
            dt.Columns["Column-5"].ColumnName = "Make";
            dt.Columns["Column-6"].ColumnName = "Model";
            dt.Columns["Column-7"].ColumnName = "TrimLevel";
            dt.Columns["Column-8"].ColumnName = "Price";
            dt.Columns["Column-9"].ColumnName = "ExteriorColor";
            dt.Columns["Column-10"].ColumnName = "InteriorColor";
            dt.Columns["Column-11"].ColumnName = "Cylinders";
            dt.Columns["Column-12"].ColumnName = "Liters";
            dt.Columns["Column-13"].ColumnName = "Odometer";
            dt.Columns["Column-14"].ColumnName = "FuelType";
            dt.Columns["Column-15"].ColumnName = "TransmissionType";
            dt.Columns["Column-16"].ColumnName = "DriveType";
            dt.Columns["Column-17"].ColumnName = "Options";
            dt.Columns["Column-18"].ColumnName = "Description";
            dt.Columns["Column-19"].ColumnName = "WebSpecial";
            dt.Columns["Column-20"].ColumnName = "MSRP";
            dt.Columns["Column-21"].ColumnName = "Web1";
            dt.Columns["Column-22"].ColumnName = "Web2";
            dt.Columns["Column-23"].ColumnName = "Web3";
            dt.Columns["Column-24"].ColumnName = "NewUsed";
            dt.Columns["Column-25"].ColumnName = "OnHoldDate";
            dt.Columns["Column-26"].ColumnName = "PicURL";
            dt.Rows.RemoveAt(0);
            return dt;
        }

        private static DataTable CreateDataTableForTabbedData(String strLine)
        {
            DataTable dt = new DataTable("TabbedTable");
            String[] strVals = strLine.Split(new char[] { ',' });
            _columnCount = strVals.Length;
            int idx = 0;
            foreach (String strVal in strVals)
            {
                String strColumnName = String.Format("Column-{0}", idx++);
                dt.Columns.Add(strColumnName, Type.GetType("System.String"));
            }
            return dt;
        }

        private static DataRow AddDataRowToTable(String strCSVLine, DataTable dt)
        {
            String[] strVals = strCSVLine.Split(new char[] { ',' });
            Int32 iTotalNumberOfValues = strVals.Length;
            if (iTotalNumberOfValues > _columnCount)
            {
                Int32 iDiff = iTotalNumberOfValues - _columnCount;
                for (Int32 i = 0; i < iDiff; i++)
                {
                    String strColumnName =
                     String.Format("Column-{0}", (_columnCount + i));
                    dt.Columns.Add(strColumnName, Type.GetType("System.String"));
                }
                _columnCount = iTotalNumberOfValues;
            }
            int idx = 0;
            DataRow drow = dt.NewRow();
            foreach (String strVal in strVals)
            {
                String strColumnName = String.Format("Column-{0}", idx++);
                drow[strColumnName] = strVal.Trim();
            }
            dt.Rows.Add(drow);
            return drow;
        }


        public static void getInventoryFor23680(string path)
        {

            BasicFTPClient MyClient = new BasicFTPClient();

            try
            {
                byte[] data = MyClient.DownloadData(path);
                MemoryStream memoryStream = new MemoryStream(data);
                StreamReader reader = new StreamReader(memoryStream);
                PopulateDataTableFromTextFile(reader);

               


            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

     



        }


        public static DataTable getInventoryForCraiglistFromFTPServer(string path)
        {

            BasicFTPClient MyClient = new BasicFTPClient();
           
            try
            {
                byte[] data = MyClient.DownloadData(path);
                MemoryStream memoryStream = new MemoryStream(data);
                StreamReader reader = new StreamReader(memoryStream);
                PopulateDataTableFromTextFile(reader);

                return _dt;


            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
  
            return null;




        }


        public static CachedCsvReader getInventoryAutoTraderFormatWithoutHeader(string path)
        {

            BasicFTPClient MyClient = new BasicFTPClient();

            try
            {
                byte[] data = MyClient.DownloadData(path);
                MemoryStream memoryStream = new MemoryStream(data);
                StreamReader reader = new StreamReader(memoryStream);
                CachedCsvReader cr = new CachedCsvReader(reader, false, ',');

                return cr;


            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return null;




        }

        public static DataTable InitialAutoTraderTable()
        {
            DataTable workTable = new DataTable("AutoTrader");


            workTable.Columns.Add("DealerId", typeof(String));
            workTable.Columns.Add("StockNumber", typeof(String));
            workTable.Columns.Add("Year", typeof(String));
            workTable.Columns.Add("Make", typeof(String));
            workTable.Columns.Add("Model", typeof(String));
            workTable.Columns.Add("Trim", typeof(String));
            workTable.Columns.Add("VIN", typeof(String));
            workTable.Columns.Add("Mileage", typeof(String));

            workTable.Columns.Add("Price", typeof(String));
            workTable.Columns.Add("ExteriorColor", typeof(String));
            workTable.Columns.Add("InteriorColor", typeof(String));
            workTable.Columns.Add("Tranmission", typeof(String));
            workTable.Columns.Add("PhysicalImages", typeof(String));
            workTable.Columns.Add("Descriptions", typeof(String));
            workTable.Columns.Add("BodyType", typeof(String));
            workTable.Columns.Add("EngineType", typeof(String));
            workTable.Columns.Add("DriveType", typeof(String));
            workTable.Columns.Add("FuelType", typeof(String));
            workTable.Columns.Add("Options", typeof(String));
            workTable.Columns.Add("NewUsed", typeof(String));

            workTable.Columns.Add("ImageURL", typeof(String));


            workTable.Columns.Add("VideoURL", typeof(String));

            workTable.Columns.Add("VideoSource", typeof(String));
            workTable.Columns.Add("Age", typeof(String));

            return workTable;


        }
        public static CachedCsvReader getInventoryAutoTraderFormatWithoutHeader222(string path)
        {

           // BasicFTPClient MyClient = new BasicFTPClient("vinwindow");

            var MyClient = new BasicFTPClient();

            try
            {
                byte[] data = MyClient.DownloadData(path);
                var memoryStream = new MemoryStream(data);
                var reader = new StreamReader(memoryStream);
                var cr = new CachedCsvReader(reader, false, ',');

                return cr;


            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return null;




        }
        public static CachedCsvReader getInventoryAutoTraderFormatWithHeader(string path)
        {

            BasicFTPClient MyClient = new BasicFTPClient();

            try
            {
                byte[] data = MyClient.DownloadData(path);
                MemoryStream memoryStream = new MemoryStream(data);
                StreamReader reader = new StreamReader(memoryStream);
                CachedCsvReader cr = new CachedCsvReader(reader, true, '\t');
                return cr;


            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return null;




        }
        public static List<AutoTraderVehicle> getInventoryAutoTraderFormatWithHeaderAndTabDelimited2(string path, NetworkCredential ClappCredential)
        {
            var list = new List<AutoTraderVehicle>();
            BasicFTPClient MyClient = new BasicFTPClient();

            //try
            //{
            //    byte[] data = MyClient.DownloadData(path, ClappCredential);

            //    MemoryStream memoryStream = new MemoryStream(data);
            //    StreamReader reader = new StreamReader(memoryStream);

            //    DataTable theDataTable = new DataTable();



            //    while (reader.Peek() > -1)
            //    {

            //        object[] currentLine = reader.ReadLine().Split(new char[] { '|' });

            //        theDataTable.Rows.Add(currentLine);

            //    }

            //    MemoryStream memoryStream = new MemoryStream(data);
            //    StreamReader reader = new StreamReader(memoryStream);
            //    CachedCsvReader cr = new CachedCsvReader(reader, true, '|',' ','\n',' ',ValueTrimmingOptions.None);

            //    return theDataTable;

            try
            {

              byte[] data = MyClient.DownloadData(path, ClappCredential);

                var memoryStream = new MemoryStream(data);

                var reader = new StreamReader(memoryStream);

                var builder = new StringBuilder();
                
                var strLine = String.Empty;

                int i = 0;

                do
                {

                   
                    if (i > 0)
                    {

                        strLine = reader.ReadLine();
                        if (strLine == null)
                        {
                            break;
                        }


                        var currentLine = strLine.Split(new char[] {'|'},StringSplitOptions.None);

                        //System.Windows.Forms.MessageBox.Show(currentLine.Count().ToString(), "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (currentLine[13].ToString().Equals("U"))
                        {

                            var vehicle = new AutoTraderVehicle
                                              {
                                                  DealerId = 19527,
                                                  StockNumber = currentLine[0],
                                                  Year = currentLine[2],
                                                  Make = currentLine[3],
                                                  Model = currentLine[4],
                                                  Trim = currentLine[5],
                                                  VIN = currentLine[6],
                                                  Mileage = currentLine[7],
                                                  Price = currentLine[9],
                                                  ExteriorColor = currentLine[10],
                                                  InteriorColor = "",
                                                  Tranmission = "",
                                                  PhysicalImages = currentLine[20],
                                                  Descriptions = currentLine[19],
                                                  BodyType = "",
                                                  EngineType = "",
                                                  DriveType = "",
                                                  FuelType = "",
                                                  Options = currentLine[19],
                                                  ImageURL = "",
                                                  VideoURL = "",
                                                  VideoSource = "",

                                              };

                            list.Add(vehicle);
                        }
                        //builder.AppendLine(strLine);
                    }
                    if (i == 0)
                    {
                        strLine = reader.ReadLine();
                        i++;
                    }

                } while (true);

                //byte[] byteArray = Encoding.ASCII.GetBytes(builder.ToString());

                //var finalStream = new MemoryStream(byteArray);

                //var finalReader = new StreamReader(finalStream);

                //var cr = new CachedCsvReader(finalReader, true, '\t');

                return list;


            }
            catch (Exception ex)
            {

                throw new Exception("Exception is " + ex.Message);
            }



        }


        public static CachedCsvReader getInventoryAutoTraderFormatWithHeaderAndTabDelimited(string path)
        {

            BasicFTPClient MyClient = new BasicFTPClient();

            try
            {
                byte[] data = MyClient.DownloadData(path);
             
                MemoryStream memoryStream = new MemoryStream(data);

                StreamReader reader = new StreamReader(memoryStream);

                string fileContent = reader.ReadToEnd();
                fileContent = fileContent.Replace('|', ',');
                byte[] byteArray = Encoding.ASCII.GetBytes(fileContent);

                MemoryStream finalStream = new MemoryStream(byteArray);

                StreamReader finalReader = new StreamReader(finalStream);

                CachedCsvReader cr = new CachedCsvReader(finalReader, true, ',');

                return cr;

            }
            catch (Exception ex)
            {

                throw new Exception("Exception is " + ex.Message);
            }



        }

        //public static CachedCsvReader getInventoryAutoTraderFormatWithoutHeader(string path)
        //{

        //    FTPHelper.ConnectToFtpServer();


        //    try
        //    {
        //        StreamReader reader = new StreamReader(FTPHelper.getFilesPerDealerId(path));
        //        CachedCsvReader cr = new CachedCsvReader(reader, false, ',');

        //        return cr;


        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception("Exception is " + ex.Message);
        //    }

        //    return null;




        //}



        public static DataTable GetDataTableFromMySQL(string storedProcedure)
        {
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionStringMySQLCL"].ToString();

            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {

                MySqlCommand command = new MySqlCommand(storedProcedure, connection);

                command.CommandType = CommandType.StoredProcedure;

                command.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TimeOut"].ToString());

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                DataSet ds = new DataSet();

                adapter.Fill(ds);

                if (ds.Tables.Count > 0)

                    return ds.Tables[0];

                return null;



            }
            catch (Exception ex)
            {
                throw new Exception("Exception is " + ex.Message);

            }



        }
        public static bool checkVinExist(string vin, int dealerId)
        {
            bool vinExist = true;

            string connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionStringMySQL"].ToString();

            MySqlConnection connection = new MySqlConnection(connectionString);


            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("VinExistStoredProcedure", connection);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?v_DealershipId", MySql.Data.MySqlClient.MySqlDbType.Int32));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?v_Vin", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters["?v_DealershipId"].Value = dealerId;

                command.Parameters["?v_DealershipId"].Direction = ParameterDirection.Input;

                command.Parameters["?v_Vin"].Value = vin;

                command.Parameters["?v_Vin"].Direction = ParameterDirection.Input;

                MySqlDataReader rdr = command.ExecuteReader();

                if (!rdr.Read())
                {
                    vinExist = false;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Dispose();
                connection.Close();
            }

            return vinExist;
        }




       
        //public static void InsertToInvetory(DataTable dt)
        //{


        //    string connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionStringMySQL"].ToString();

        //    MySqlConnection connection = new MySqlConnection(connectionString);

        //    MySqlCommand command = null;
        //    try
        //    {

        //        connection.Open();

        //        command = connection.CreateCommand();

        //        command.Connection = connection;

        //        command.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TimeOut"].ToString());

        //        command.CommandType = CommandType.StoredProcedure;

        //        //***************************************INSERT NEW RECORD TO DATAWAREHOUSE TABLE***************************************

        //        command.CommandText = "InsertInventoryStoredProcedure";


        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?ModelYear", MySql.Data.MySqlClient.MySqlDbType.Int16));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?Make", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?Model", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?Trim", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?VINNumber", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?StockNumber", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?SalePrice", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?MSRP", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?Mileage", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?ExteriorColor", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?InteriorColor", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?InteriorSurface", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?BodyType", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?Cylinders", MySql.Data.MySqlClient.MySqlDbType.VarChar, 45));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?Liters", MySql.Data.MySqlClient.MySqlDbType.VarChar, 45));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?EngineType", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?DriveTrain", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?FuelType", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?Tranmission", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?Doors", MySql.Data.MySqlClient.MySqlDbType.VarChar, 45));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?Certified", MySql.Data.MySqlClient.MySqlDbType.Bit));


        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?StandardOptions", MySql.Data.MySqlClient.MySqlDbType.LongText));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?CarsOptions", MySql.Data.MySqlClient.MySqlDbType.LongText));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?CarsPackages", MySql.Data.MySqlClient.MySqlDbType.LongText));



        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?Descriptions", MySql.Data.MySqlClient.MySqlDbType.LongText));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?CarImageUrl", MySql.Data.MySqlClient.MySqlDbType.LongText));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?ThumbnailImageURL", MySql.Data.MySqlClient.MySqlDbType.LongText));


        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?DealershipName", MySql.Data.MySqlClient.MySqlDbType.VarChar, 200));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?DealershipAddress", MySql.Data.MySqlClient.MySqlDbType.VarChar, 250));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?DealershipCity", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?DealershipState", MySql.Data.MySqlClient.MySqlDbType.VarChar, 10));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?DealershipZipCode", MySql.Data.MySqlClient.MySqlDbType.VarChar, 10));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?DealershipPhone", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?DealershipId", MySql.Data.MySqlClient.MySqlDbType.Int32));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?DefaultImageUrl", MySql.Data.MySqlClient.MySqlDbType.LongText));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?NewUsed", MySql.Data.MySqlClient.MySqlDbType.VarChar, 10));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?AddToInventoryBy", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

        //        command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?AppraisalID", MySql.Data.MySqlClient.MySqlDbType.VarChar, 45));





        //        command.Parameters["?ModelYear"].Direction = ParameterDirection.Input;

        //        command.Parameters["?Make"].Direction = ParameterDirection.Input;

        //        command.Parameters["?Model"].Direction = ParameterDirection.Input;

        //        command.Parameters["?Trim"].Direction = ParameterDirection.Input;

        //        command.Parameters["?VINNumber"].Direction = ParameterDirection.Input;

        //        command.Parameters["?StockNumber"].Direction = ParameterDirection.Input;

        //        command.Parameters["?SalePrice"].Direction = ParameterDirection.Input;

        //        command.Parameters["?MSRP"].Direction = ParameterDirection.Input;

        //        command.Parameters["?Mileage"].Direction = ParameterDirection.Input;

        //        command.Parameters["?ExteriorColor"].Direction = ParameterDirection.Input;

        //        command.Parameters["?InteriorColor"].Direction = ParameterDirection.Input;

        //        command.Parameters["?InteriorSurface"].Direction = ParameterDirection.Input;

        //        command.Parameters["?BodyType"].Direction = ParameterDirection.Input;

        //        command.Parameters["?Cylinders"].Direction = ParameterDirection.Input;

        //        command.Parameters["?Liters"].Direction = ParameterDirection.Input;

        //        command.Parameters["?EngineType"].Direction = ParameterDirection.Input;

        //        command.Parameters["?DriveTrain"].Direction = ParameterDirection.Input;

        //        command.Parameters["?FuelType"].Direction = ParameterDirection.Input;

        //        command.Parameters["?Tranmission"].Direction = ParameterDirection.Input;

        //        command.Parameters["?Doors"].Direction = ParameterDirection.Input;

        //        command.Parameters["?Certified"].Direction = ParameterDirection.Input;


        //        command.Parameters["?StandardOptions"].Direction = ParameterDirection.Input;


        //        command.Parameters["?CarsOptions"].Direction = ParameterDirection.Input;

        //        command.Parameters["?CarsPackages"].Direction = ParameterDirection.Input;

        //        command.Parameters["?Descriptions"].Direction = ParameterDirection.Input;

        //        command.Parameters["?CarImageUrl"].Direction = ParameterDirection.Input;

        //        command.Parameters["?ThumbnailImageURL"].Direction = ParameterDirection.Input;


        //        command.Parameters["?DealershipName"].Direction = ParameterDirection.Input;

        //        command.Parameters["?DealershipAddress"].Direction = ParameterDirection.Input;

        //        command.Parameters["?DealershipCity"].Direction = ParameterDirection.Input;

        //        command.Parameters["?DealershipState"].Direction = ParameterDirection.Input;

        //        command.Parameters["?DealershipState"].Direction = ParameterDirection.Input;

        //        command.Parameters["?DealershipZipCode"].Direction = ParameterDirection.Input;

        //        command.Parameters["?DealershipId"].Direction = ParameterDirection.Input;

        //        command.Parameters["?DefaultImageUrl"].Direction = ParameterDirection.Input;

        //        command.Parameters["?NewUsed"].Direction = ParameterDirection.Input;

        //        command.Parameters["?AddToInventoryBy"].Direction = ParameterDirection.Input;

        //        command.Parameters["?AppraisalID"].Direction = ParameterDirection.Input;
                

        //        foreach (DataRow drRow in dt.Rows)
        //        {
                 
        //                command.Parameters["?ModelYear"].Value =Convert.ToInt32(drRow.Field<string>("ModelYear"));

        //                command.Parameters["?Make"].Value = String.IsNullOrEmpty(drRow.Field<string>("Make")) ? "" : drRow.Field<string>("Make");

        //                command.Parameters["?Model"].Value = String.IsNullOrEmpty(drRow.Field<string>("Model")) ? "" : drRow.Field<string>("Model");

        //                command.Parameters["?Trim"].Value = String.IsNullOrEmpty(drRow.Field<string>("Trim")) ? "" : drRow.Field<string>("Trim");

        //                command.Parameters["?VINNumber"].Value = String.IsNullOrEmpty(drRow.Field<string>("VINNumber")) ? "" : drRow.Field<string>("VINNumber");

        //                command.Parameters["?StockNumber"].Value = String.IsNullOrEmpty(drRow.Field<string>("StockNumber")) ? "" : drRow.Field<string>("StockNumber");

        //                command.Parameters["?SalePrice"].Value = String.IsNullOrEmpty(drRow.Field<string>("SalePrice")) ? "" : drRow.Field<string>("SalePrice");

        //                command.Parameters["?MSRP"].Value = String.IsNullOrEmpty(drRow.Field<string>("MSRP")) ? "" : drRow.Field<string>("MSRP");

        //                command.Parameters["?Mileage"].Value = String.IsNullOrEmpty(drRow.Field<string>("Mileage")) ? "" : drRow.Field<string>("Mileage");

        //                command.Parameters["?ExteriorColor"].Value = String.IsNullOrEmpty(drRow.Field<string>("ExteriorColor")) ? "" : drRow.Field<string>("ExteriorColor");

        //                command.Parameters["?InteriorColor"].Value = String.IsNullOrEmpty(drRow.Field<string>("InteriorColor")) ? "" : drRow.Field<string>("InteriorColor");

        //                command.Parameters["?InteriorSurface"].Value = "";

        //                command.Parameters["?BodyType"].Value = String.IsNullOrEmpty(drRow.Field<string>("BodyType")) ? "" : drRow.Field<string>("BodyType");

        //                command.Parameters["?Cylinders"].Value = String.IsNullOrEmpty(drRow.Field<string>("Cylinders")) ? "" : drRow.Field<string>("Cylinders");

        //                command.Parameters["?Liters"].Value = String.IsNullOrEmpty(drRow.Field<string>("Liters")) ? "" : drRow.Field<string>("Liters");

        //                command.Parameters["?EngineType"].Value = String.IsNullOrEmpty(drRow.Field<string>("EngineType")) ? "" : drRow.Field<string>("EngineType");

        //                command.Parameters["?DriveTrain"].Value = String.IsNullOrEmpty(drRow.Field<string>("DriveTrain")) ? "" : drRow.Field<string>("DriveTrain");

        //                command.Parameters["?FuelType"].Value = String.IsNullOrEmpty(drRow.Field<string>("FuelType")) ? "" : drRow.Field<string>("FuelType");

        //                command.Parameters["?Tranmission"].Value = String.IsNullOrEmpty(drRow.Field<string>("Tranmission")) ? "" : drRow.Field<string>("Tranmission");

        //                command.Parameters["?Doors"].Value = String.IsNullOrEmpty(drRow.Field<string>("Doors")) ? "" : drRow.Field<string>("Doors");

        //                command.Parameters["?Certified"].Value = false;

        //                command.Parameters["?StandardOptions"].Value = String.IsNullOrEmpty(drRow.Field<string>("CarsOptions")) ? "" : drRow.Field<string>("CarsOptions").Replace("\'", "\\'");

        //                command.Parameters["?CarsOptions"].Value = String.IsNullOrEmpty(drRow.Field<string>("CarsOptions")) ? "" : drRow.Field<string>("CarsOptions").Replace("\'", "\\'");

        //                command.Parameters["?CarsPackages"].Value = "";

        //                command.Parameters["?Descriptions"].Value = String.IsNullOrEmpty(drRow.Field<string>("Descriptions")) ? "" : drRow.Field<string>("Descriptions").Replace("\'", "\\'");

        //                command.Parameters["?CarImageUrl"].Value = String.IsNullOrEmpty(drRow.Field<string>("CarImageUrl")) ? "" : drRow.Field<string>("CarImageUrl").Replace("\'", "\\'");

        //                command.Parameters["?ThumbnailImageURL"].Value = String.IsNullOrEmpty(drRow.Field<string>("CarImageUrl")) ? "" : drRow.Field<string>("CarImageUrl").Replace("\'", "\\'");


        //                command.Parameters["?DealershipName"].Value = String.IsNullOrEmpty(drRow.Field<string>("DealershipName")) ? "" : drRow.Field<string>("DealershipName").Replace("\'", "\\'");

        //                command.Parameters["?DealershipAddress"].Value = String.IsNullOrEmpty(drRow.Field<string>("DealershipAddress")) ? "" : drRow.Field<string>("DealershipAddress").Replace("\'", "\\'");

        //                command.Parameters["?DealershipCity"].Value = String.IsNullOrEmpty(drRow.Field<string>("DealershipCity")) ? "" : drRow.Field<string>("DealershipCity").Replace("\'", "\\'");


        //                command.Parameters["?DealershipState"].Value = String.IsNullOrEmpty(drRow.Field<string>("DealershipState")) ? "" : drRow.Field<string>("DealershipState");


        //                command.Parameters["?DealershipPhone"].Value = String.IsNullOrEmpty(drRow.Field<string>("DealershipPhone")) ? "" : drRow.Field<string>("DealershipPhone");


        //                command.Parameters["?DealershipId"].Value = String.IsNullOrEmpty(drRow.Field<string>("DealershipId")) ? "" : drRow.Field<string>("DealershipId");

        //                command.Parameters["?DefaultImageUrl"].Value = String.IsNullOrEmpty(drRow.Field<string>("CarImageUrl")) ? "http://vincontrol.com/alpha/cListThemes/gloss/no-image-available.jpg" : drRow.Field<string>("CarImageUrl").ToString().Substring(0, drRow.Field<string>("CarImageUrl").ToString().IndexOf(","));


        //                command.Parameters["?NewUsed"].Value = String.IsNullOrEmpty(drRow.Field<string>("NewUsed")) ? "" : drRow.Field<string>("NewUsed");

        //                command.Parameters["?AddToInventoryBy"].Value = String.IsNullOrEmpty(drRow.Field<string>("AddToInventoryBy")) ? "" : drRow.Field<string>("AddToInventoryBy");


        //                command.Parameters["?AppraisalId"].Value = String.IsNullOrEmpty(drRow.Field<string>("AppraisalId")) ? "" : drRow.Field<string>("AppraisalId");


        //                command.ExecuteNonQuery();

                   
                  
                                      
        //        }



        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Exception is " + ex.Message);

        //    }
        //    finally
        //    {
        //        connection.Close();

        //        connection.Dispose();
        //    }


        //}
        public static void InsertToInvetory(DataTable dt)
        {


      
            try
            {


                using (var context = new whitmanenterprisewarehouseEntities())
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var e = new whitmanenterprisedealershipinventory()
                        {
                           ListingID=context.whitmanenterprisedealershipinventory.Max(x=>x.ListingID)+1,
                           ModelYear = Convert.ToInt32(dr.Field<string>("ModelYear")),
                           Make = String.IsNullOrEmpty(dr.Field<string>("Make")) ? "" : dr.Field<string>("Make"),
                           Model = String.IsNullOrEmpty(dr.Field<string>("Model")) ? "" : dr.Field<string>("Model"),
                           Trim = String.IsNullOrEmpty(dr.Field<string>("Trim")) ? "" : dr.Field<string>("Trim"),
                           VINNumber = String.IsNullOrEmpty(dr.Field<string>("VINNumber")) ? "" : dr.Field<string>("VINNumber"),
                           StockNumber = String.IsNullOrEmpty(dr.Field<string>("StockNumber")) ? "" : dr.Field<string>("StockNumber"),
                           SalePrice = String.IsNullOrEmpty(dr.Field<string>("SalePrice")) ? "" : dr.Field<string>("SalePrice"),
                           MSRP = String.IsNullOrEmpty(dr.Field<string>("MSRP")) ? "" : dr.Field<string>("MSRP"),
                           Mileage = String.IsNullOrEmpty(dr.Field<string>("Mileage")) ? "" : dr.Field<string>("Mileage"),
                           ExteriorColor = String.IsNullOrEmpty(dr.Field<string>("ExteriorColor")) ? "" : dr.Field<string>("ExteriorColor"),
                           InteriorColor = String.IsNullOrEmpty(dr.Field<string>("InteriorColor")) ? "" : dr.Field<string>("InteriorColor"),
                           InteriorSurface="",
                           BodyType = String.IsNullOrEmpty(dr.Field<string>("BodyType")) ? "" : dr.Field<string>("BodyType"),
                           Cylinders = String.IsNullOrEmpty(dr.Field<string>("Cylinders")) ? "" : dr.Field<string>("Cylinders"),
                           Liters = String.IsNullOrEmpty(dr.Field<string>("Liters")) ? "" : dr.Field<string>("Liters"),
                           EngineType = String.IsNullOrEmpty(dr.Field<string>("EngineType")) ? "" : dr.Field<string>("EngineType"),
                           DriveTrain = String.IsNullOrEmpty(dr.Field<string>("DriveTrain")) ? "" : dr.Field<string>("DriveTrain"),
                           FuelType = String.IsNullOrEmpty(dr.Field<string>("FuelType")) ? "" : dr.Field<string>("FuelType"),
                           Tranmission = String.IsNullOrEmpty(dr.Field<string>("Tranmission")) ? "" : dr.Field<string>("Tranmission"),
                           Doors = String.IsNullOrEmpty(dr.Field<string>("Doors")) ? "" : dr.Field<string>("Doors"),
                           Certified=false,
                           StandardOptions=String.IsNullOrEmpty(dr.Field<string>("CarsOptions")) ? "" : dr.Field<string>("CarsOptions").Replace("\'", "\\'"),
                           CarsOptions=String.IsNullOrEmpty(dr.Field<string>("CarsOptions")) ? "" : dr.Field<string>("CarsOptions").Replace("\'", "\\'"),
                           CarsPackages="",
                           Descriptions=String.IsNullOrEmpty(dr.Field<string>("Descriptions")) ? "" : dr.Field<string>("Descriptions").Replace("\'", "\\'"),
                           CarImageUrl = String.IsNullOrEmpty(dr.Field<string>("CarImageUrl")) ? "" : dr.Field<string>("CarImageUrl"),
                           ThumbnailImageURL = String.IsNullOrEmpty(dr.Field<string>("CarImageUrl")) ? "" : dr.Field<string>("CarImageUrl"),
                           DealershipName = String.IsNullOrEmpty(dr.Field<string>("DealershipName")) ? "" : dr.Field<string>("DealershipName"),
                           DealershipAddress = String.IsNullOrEmpty(dr.Field<string>("DealershipAddress")) ? "" : dr.Field<string>("DealershipAddress"),
                           DealershipCity = String.IsNullOrEmpty(dr.Field<string>("DealershipCity")) ? "" : dr.Field<string>("DealershipCity"),
                           DealershipZipCode = String.IsNullOrEmpty(dr.Field<string>("DealershipZipCode")) ? "" : dr.Field<string>("DealershipZipCode"),
                           DealershipPhone = String.IsNullOrEmpty(dr.Field<string>("DealershipPhone")) ? "" : dr.Field<string>("DealershipPhone"),
                           DealershipId =dr.Field<int>("DealershipId"),
                           DealershipState = dr.Field<string>("DealershipState"),
                           DefaultImageUrl = String.IsNullOrEmpty(dr.Field<string>("DefaultImageUrl")) ? "" : dr.Field<string>("DefaultImageUrl"),
                           NewUsed=dr.Field<string>("NewUsed"),
                           AddToInventoryBy="VinControlAdmin",
                           AppraisalID="999-999-999",
                           DateInStock=DateTime.Now,
                           LastUpdated=DateTime.Now,
                           VehicleType="Car",
                           TruckCategory="",
                           TruckClass="",
                           TruckType="",
                           Recon = false,
                           PriorRental = false
                          
                                                      
                           


                        };


                        //Add to memory
                        context.AddTowhitmanenterprisedealershipinventory(e);
                        context.SaveChanges();
                      
                    }

                   
                    
                   
                   
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Exception is " + ex.Message);

            }
      

        }
        //public static void InsertToInvetoryFprHornBurg(DataTable dt)
        //{
        //    try
        //    {
                

        //        using (var context = new whitmanenterprisewarehouseEntities())
        //        {
                    
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                if (!dr.Field<string>("Make").Equals("Jaguar") && !dr.Field<string>("Make").Equals("Land Rover") && !dr.Field<string>("Make").Equals("Aston Martin"))
        //                  {
        //                      var e = new hornburginventory()
        //                                  {

        //                                      ModelYear = Convert.ToInt32(dr.Field<string>("ModelYear")),
        //                                      Make =
        //                                          String.IsNullOrEmpty(dr.Field<string>("Make"))
        //                                              ? ""
        //                                              : dr.Field<string>("Make"),
        //                                      Model =
        //                                          String.IsNullOrEmpty(dr.Field<string>("Model"))
        //                                              ? ""
        //                                              : dr.Field<string>("Model"),
        //                                      Trim =
        //                                          String.IsNullOrEmpty(dr.Field<string>("Trim"))
        //                                              ? ""
        //                                              : dr.Field<string>("Trim"),
        //                                      VINNumber =
        //                                          String.IsNullOrEmpty(dr.Field<string>("VINNumber"))
        //                                              ? ""
        //                                              : dr.Field<string>("VINNumber"),
        //                                      StockNumber =
        //                                          String.IsNullOrEmpty(dr.Field<string>("StockNumber"))
        //                                              ? ""
        //                                              : dr.Field<string>("StockNumber"),
        //                                      SalePrice =
        //                                          String.IsNullOrEmpty(dr.Field<string>("SalePrice"))
        //                                              ? ""
        //                                              : dr.Field<string>("SalePrice"),
        //                                      MSRP =
        //                                          String.IsNullOrEmpty(dr.Field<string>("MSRP"))
        //                                              ? ""
        //                                              : dr.Field<string>("MSRP"),
        //                                      Mileage =
        //                                          String.IsNullOrEmpty(dr.Field<string>("Mileage"))
        //                                              ? ""
        //                                              : dr.Field<string>("Mileage"),
        //                                      ExteriorColor =
        //                                          String.IsNullOrEmpty(dr.Field<string>("ExteriorColor"))
        //                                              ? ""
        //                                              : dr.Field<string>("ExteriorColor"),
        //                                      InteriorColor =
        //                                          String.IsNullOrEmpty(dr.Field<string>("InteriorColor"))
        //                                              ? ""
        //                                              : dr.Field<string>("InteriorColor"),
        //                                      InteriorSurface = "",
        //                                      BodyType =
        //                                          String.IsNullOrEmpty(dr.Field<string>("BodyType"))
        //                                              ? ""
        //                                              : dr.Field<string>("BodyType"),
        //                                      Cylinders =
        //                                          String.IsNullOrEmpty(dr.Field<string>("Cylinders"))
        //                                              ? ""
        //                                              : dr.Field<string>("Cylinders"),
        //                                      Liters =
        //                                          String.IsNullOrEmpty(dr.Field<string>("Liters"))
        //                                              ? ""
        //                                              : dr.Field<string>("Liters"),
        //                                      EngineType =
        //                                          String.IsNullOrEmpty(dr.Field<string>("EngineType"))
        //                                              ? ""
        //                                              : dr.Field<string>("EngineType"),
        //                                      DriveTrain =
        //                                          String.IsNullOrEmpty(dr.Field<string>("DriveTrain"))
        //                                              ? ""
        //                                              : dr.Field<string>("DriveTrain"),
        //                                      FuelType =
        //                                          String.IsNullOrEmpty(dr.Field<string>("FuelType"))
        //                                              ? ""
        //                                              : dr.Field<string>("FuelType"),
        //                                      Tranmission =
        //                                          String.IsNullOrEmpty(dr.Field<string>("Tranmission"))
        //                                              ? ""
        //                                              : dr.Field<string>("Tranmission"),
        //                                      Doors =
        //                                          String.IsNullOrEmpty(dr.Field<string>("Doors"))
        //                                              ? ""
        //                                              : dr.Field<string>("Doors"),
        //                                      Certified = false,
        //                                      StandardOptions =
        //                                          String.IsNullOrEmpty(dr.Field<string>("CarsOptions"))
        //                                              ? ""
        //                                              : dr.Field<string>("CarsOptions").Replace("\'", "\\'"),
        //                                      CarsOptions =
        //                                          String.IsNullOrEmpty(dr.Field<string>("CarsOptions"))
        //                                              ? ""
        //                                              : dr.Field<string>("CarsOptions").Replace("\'", "\\'"),
        //                                      CarsPackages = "",
        //                                      Descriptions =
        //                                          String.IsNullOrEmpty(dr.Field<string>("Descriptions"))
        //                                              ? ""
        //                                              : dr.Field<string>("Descriptions").Replace("\'", "\\'"),
        //                                      CarImageUrl =
        //                                          String.IsNullOrEmpty(dr.Field<string>("CarImageUrl"))
        //                                              ? ""
        //                                              : dr.Field<string>("CarImageUrl"),
        //                                      ThumbnailImageURL =
        //                                          String.IsNullOrEmpty(dr.Field<string>("CarImageUrl"))
        //                                              ? ""
        //                                              : dr.Field<string>("CarImageUrl"),
        //                                      DealershipName =
        //                                          String.IsNullOrEmpty(dr.Field<string>("DealershipName"))
        //                                              ? ""
        //                                              : dr.Field<string>("DealershipName"),
        //                                      DealershipAddress =
        //                                          String.IsNullOrEmpty(dr.Field<string>("DealershipAddress"))
        //                                              ? ""
        //                                              : dr.Field<string>("DealershipAddress"),
        //                                      DealershipCity =
        //                                          String.IsNullOrEmpty(dr.Field<string>("DealershipCity"))
        //                                              ? ""
        //                                              : dr.Field<string>("DealershipCity"),
        //                                      DealershipZipCode =
        //                                          String.IsNullOrEmpty(dr.Field<string>("DealershipZipCode"))
        //                                              ? ""
        //                                              : dr.Field<string>("DealershipZipCode"),
        //                                      DealershipPhone =
        //                                          String.IsNullOrEmpty(dr.Field<string>("DealershipPhone"))
        //                                              ? ""
        //                                              : dr.Field<string>("DealershipPhone"),
        //                                      DealershipId = Convert.ToInt32(dr.Field<string>("DealershipId")),
        //                                      DealershipState = dr.Field<string>("DealershipState"),
        //                                      DefaultImageUrl =
        //                                          String.IsNullOrEmpty(dr.Field<string>("DefaultImageUrl"))
        //                                              ? ""
        //                                              : dr.Field<string>("DefaultImageUrl"),
        //                                      NewUsed = "Used",
        //                                      AddToInventoryBy = "VinControlAdmin",
        //                                      AppraisalID = "999-999-999",
        //                                      DateInStock = DateTime.Now,
        //                                      LastUpdated = DateTime.Now,
        //                                      VehicleType = "Car",
        //                                      TruckCategory = "",
        //                                      TruckClass = "",
        //                                      TruckType = "",
        //                                      DealerGroupID = "HB1000"




        //                                  };
        //                      context.AddTohornburginventory(e);
                             

        //                  }
        //                //Add to memory

                       
        //            }
        //            context.SaveChanges();
                   
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Exception is " + ex.Message);

        //    }
      

        //}


        


        public static void InsertToInvetoryRow(DataRow drRow)
        {


            string connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionStringMySQL"].ToString();

            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = null;
            try
            {

                connection.Open();

                command = connection.CreateCommand();

                command.Connection = connection;

                command.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TimeOut"].ToString());

                command.CommandType = CommandType.StoredProcedure;

                //***************************************INSERT NEW RECORD TO DATAWAREHOUSE TABLE***************************************

                command.CommandText = "InsertInventoryStoredProcedure";


                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?ModelYear", MySql.Data.MySqlClient.MySqlDbType.Int16));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?Make", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?Model", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?Trim", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?VINNumber", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?StockNumber", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?SalePrice", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?MSRP", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?Mileage", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?ExteriorColor", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?InteriorColor", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?InteriorSurface", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?BodyType", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?Cylinders", MySql.Data.MySqlClient.MySqlDbType.VarChar, 45));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?Liters", MySql.Data.MySqlClient.MySqlDbType.VarChar, 45));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?EngineType", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?DriveTrain", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?FuelType", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?Tranmission", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?Doors", MySql.Data.MySqlClient.MySqlDbType.VarChar, 45));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?Certified", MySql.Data.MySqlClient.MySqlDbType.Bit));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?CarsOptions", MySql.Data.MySqlClient.MySqlDbType.LongText));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?CarsPackages", MySql.Data.MySqlClient.MySqlDbType.LongText));



                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?Descriptions", MySql.Data.MySqlClient.MySqlDbType.LongText));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?CarImageUrl", MySql.Data.MySqlClient.MySqlDbType.LongText));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?ThumbnailImageURL", MySql.Data.MySqlClient.MySqlDbType.LongText));


                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?DealershipName", MySql.Data.MySqlClient.MySqlDbType.VarChar, 200));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?DealershipAddress", MySql.Data.MySqlClient.MySqlDbType.VarChar, 250));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?DealershipCity", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?DealershipState", MySql.Data.MySqlClient.MySqlDbType.VarChar, 10));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?DealershipZipCode", MySql.Data.MySqlClient.MySqlDbType.VarChar, 10));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?DealershipPhone", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?DealershipId", MySql.Data.MySqlClient.MySqlDbType.Int32));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?DefaultImageUrl", MySql.Data.MySqlClient.MySqlDbType.LongText));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?NewUsed", MySql.Data.MySqlClient.MySqlDbType.VarChar, 10));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?AddToInventoryBy", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?AppraisalID", MySql.Data.MySqlClient.MySqlDbType.VarChar, 45));





                command.Parameters["?ModelYear"].Direction = ParameterDirection.Input;

                command.Parameters["?Make"].Direction = ParameterDirection.Input;

                command.Parameters["?Model"].Direction = ParameterDirection.Input;

                command.Parameters["?Trim"].Direction = ParameterDirection.Input;

                command.Parameters["?VINNumber"].Direction = ParameterDirection.Input;

                command.Parameters["?StockNumber"].Direction = ParameterDirection.Input;

                command.Parameters["?SalePrice"].Direction = ParameterDirection.Input;

                command.Parameters["?MSRP"].Direction = ParameterDirection.Input;

                command.Parameters["?Mileage"].Direction = ParameterDirection.Input;

                command.Parameters["?ExteriorColor"].Direction = ParameterDirection.Input;

                command.Parameters["?InteriorColor"].Direction = ParameterDirection.Input;

                command.Parameters["?InteriorSurface"].Direction = ParameterDirection.Input;

                command.Parameters["?BodyType"].Direction = ParameterDirection.Input;

                command.Parameters["?Cylinders"].Direction = ParameterDirection.Input;

                command.Parameters["?Liters"].Direction = ParameterDirection.Input;

                command.Parameters["?EngineType"].Direction = ParameterDirection.Input;

                command.Parameters["?DriveTrain"].Direction = ParameterDirection.Input;

                command.Parameters["?FuelType"].Direction = ParameterDirection.Input;

                command.Parameters["?Tranmission"].Direction = ParameterDirection.Input;

                command.Parameters["?Doors"].Direction = ParameterDirection.Input;

                command.Parameters["?Certified"].Direction = ParameterDirection.Input;

                command.Parameters["?CarsOptions"].Direction = ParameterDirection.Input;

                command.Parameters["?CarsPackages"].Direction = ParameterDirection.Input;

                command.Parameters["?Descriptions"].Direction = ParameterDirection.Input;

                command.Parameters["?CarImageUrl"].Direction = ParameterDirection.Input;

                command.Parameters["?ThumbnailImageURL"].Direction = ParameterDirection.Input;


                command.Parameters["?DealershipName"].Direction = ParameterDirection.Input;

                command.Parameters["?DealershipAddress"].Direction = ParameterDirection.Input;

                command.Parameters["?DealershipCity"].Direction = ParameterDirection.Input;

                command.Parameters["?DealershipState"].Direction = ParameterDirection.Input;

                command.Parameters["?DealershipState"].Direction = ParameterDirection.Input;

                command.Parameters["?DealershipZipCode"].Direction = ParameterDirection.Input;

                command.Parameters["?DealershipId"].Direction = ParameterDirection.Input;

                command.Parameters["?DefaultImageUrl"].Direction = ParameterDirection.Input;

                command.Parameters["?NewUsed"].Direction = ParameterDirection.Input;

                command.Parameters["?AddToInventoryBy"].Direction = ParameterDirection.Input;

                command.Parameters["?AppraisalID"].Direction = ParameterDirection.Input;



                command.Parameters["?ModelYear"].Value = drRow.Field<string>("ModelYear");

                command.Parameters["?Make"].Value = String.IsNullOrEmpty(drRow.Field<string>("Make")) ? "" : drRow.Field<string>("Make");

                command.Parameters["?Model"].Value = String.IsNullOrEmpty(drRow.Field<string>("Model")) ? "" : drRow.Field<string>("Model");

                command.Parameters["?Trim"].Value = String.IsNullOrEmpty(drRow.Field<string>("Trim")) ? "" : drRow.Field<string>("Trim");

                command.Parameters["?VINNumber"].Value = String.IsNullOrEmpty(drRow.Field<string>("VINNumber")) ? "" : drRow.Field<string>("VINNumber");

                command.Parameters["?StockNumber"].Value = String.IsNullOrEmpty(drRow.Field<string>("StockNumber")) ? "" : drRow.Field<string>("StockNumber");

                command.Parameters["?SalePrice"].Value = String.IsNullOrEmpty(drRow.Field<string>("SalePrice")) ? "" : drRow.Field<string>("SalePrice");

                command.Parameters["?MSRP"].Value = String.IsNullOrEmpty(drRow.Field<string>("MSRP")) ? "" : drRow.Field<string>("MSRP");

                command.Parameters["?Mileage"].Value = String.IsNullOrEmpty(drRow.Field<string>("Mileage")) ? "" : drRow.Field<string>("Mileage");

                command.Parameters["?ExteriorColor"].Value = String.IsNullOrEmpty(drRow.Field<string>("ExteriorColor")) ? "" : drRow.Field<string>("ExteriorColor");

                command.Parameters["?InteriorColor"].Value = String.IsNullOrEmpty(drRow.Field<string>("InteriorColor")) ? "" : drRow.Field<string>("InteriorColor");

                command.Parameters["?InteriorSurface"].Value = "";

                command.Parameters["?BodyType"].Value = String.IsNullOrEmpty(drRow.Field<string>("BodyType")) ? "" : drRow.Field<string>("BodyType");

                command.Parameters["?Cylinders"].Value = String.IsNullOrEmpty(drRow.Field<string>("Cylinders")) ? "" : drRow.Field<string>("Cylinders");

                command.Parameters["?Liters"].Value = String.IsNullOrEmpty(drRow.Field<string>("Liters")) ? "" : drRow.Field<string>("Liters");

                command.Parameters["?EngineType"].Value = String.IsNullOrEmpty(drRow.Field<string>("EngineType")) ? "" : drRow.Field<string>("EngineType");

                command.Parameters["?DriveTrain"].Value = String.IsNullOrEmpty(drRow.Field<string>("DriveTrain")) ? "" : drRow.Field<string>("DriveTrain");

                command.Parameters["?FuelType"].Value = String.IsNullOrEmpty(drRow.Field<string>("FuelType")) ? "" : drRow.Field<string>("FuelType");

                command.Parameters["?Tranmission"].Value = String.IsNullOrEmpty(drRow.Field<string>("Tranmission")) ? "" : drRow.Field<string>("Tranmission");

                command.Parameters["?Doors"].Value = String.IsNullOrEmpty(drRow.Field<string>("Doors")) ? "" : drRow.Field<string>("Doors");

                command.Parameters["?Certified"].Value = false;

                command.Parameters["?CarsOptions"].Value = String.IsNullOrEmpty(drRow.Field<string>("CarsOptions")) ? "" : drRow.Field<string>("CarsOptions");

                command.Parameters["?CarsPackages"].Value = "";

                command.Parameters["?Descriptions"].Value = String.IsNullOrEmpty(drRow.Field<string>("Descriptions")) ? "" : drRow.Field<string>("Descriptions");

                command.Parameters["?CarImageUrl"].Value = String.IsNullOrEmpty(drRow.Field<string>("CarImageUrl")) ? "" : drRow.Field<string>("CarImageUrl");

                command.Parameters["?ThumbnailImageURL"].Value = String.IsNullOrEmpty(drRow.Field<string>("CarImageUrl")) ? "" : drRow.Field<string>("CarImageUrl");


                command.Parameters["?DealershipName"].Value = String.IsNullOrEmpty(drRow.Field<string>("DealershipName")) ? "" : drRow.Field<string>("DealershipName");


                command.Parameters["?DealershipAddress"].Value = String.IsNullOrEmpty(drRow.Field<string>("DealershipAddress")) ? "" : drRow.Field<string>("DealershipAddress");

                command.Parameters["?DealershipCity"].Value = String.IsNullOrEmpty(drRow.Field<string>("DealershipCity")) ? "" : drRow.Field<string>("DealershipCity");


                command.Parameters["?DealershipState"].Value = String.IsNullOrEmpty(drRow.Field<string>("DealershipState")) ? "" : drRow.Field<string>("DealershipState");


                command.Parameters["?DealershipPhone"].Value = String.IsNullOrEmpty(drRow.Field<string>("DealershipPhone")) ? "" : drRow.Field<string>("DealershipPhone");


                command.Parameters["?DealershipId"].Value = String.IsNullOrEmpty(drRow.Field<string>("DealershipId")) ? "" : drRow.Field<string>("DealershipId");

                command.Parameters["?DefaultImageUrl"].Value = String.IsNullOrEmpty(drRow.Field<string>("CarImageUrl")) ? "http://vincontrol.com/alpha/cListThemes/gloss/no-image-available.jpg" : drRow.Field<string>("CarImageUrl").ToString().Substring(0, drRow.Field<string>("CarImageUrl").ToString().IndexOf(","));


                command.Parameters["?NewUsed"].Value = String.IsNullOrEmpty(drRow.Field<string>("NewUsed")) ? "" : drRow.Field<string>("NewUsed");

                command.Parameters["?AddToInventoryBy"].Value = String.IsNullOrEmpty(drRow.Field<string>("AddToInventoryBy")) ? "" : drRow.Field<string>("AddToInventoryBy");


                command.Parameters["?AppraisalId"].Value = String.IsNullOrEmpty(drRow.Field<string>("AppraisalId")) ? "" : drRow.Field<string>("AppraisalId");


                command.ExecuteNonQuery();


                //else
                //{
                //    SQLHelper.UpdateSalePrice(drRow.Field<string>("VINNumber"), 7228, drRow.Field<string>("SalePrice"));
                //}





            }
            catch (Exception ex)
            {
                throw new Exception("Exception is " + ex.Message);

            }
            finally
            {
                connection.Close();

                connection.Dispose();
            }


        }


        public static void UpdateSalePrice(string VinNumber,int dealerId, string salePrice)
        {

            string connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionStringMySQL"].ToString();

            MySqlConnection connection = new MySqlConnection(connectionString);


            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("UpdateSalePriceByVinStoredProcedure", connection);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?v_DealerId", MySql.Data.MySqlClient.MySqlDbType.Int32));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?v_Vin", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?v_salePrice", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));


                command.Parameters["?v_DealerId"].Direction = ParameterDirection.Input;

                command.Parameters["?v_Vin"].Direction = ParameterDirection.Input;

                command.Parameters["?v_salePrice"].Direction = ParameterDirection.Input;


                command.Parameters["?v_DealerId"].Value = dealerId;

                command.Parameters["?v_Vin"].Value = VinNumber;

                command.Parameters["?v_salePrice"].Value = salePrice;


                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Dispose();
                connection.Close();
            }


        }

        public static void RemoveOld(int ListingId)
        {

            string connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionStringMySQL"].ToString();

            MySqlConnection connection = new MySqlConnection(connectionString);


            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("RemoveListingIdStoredProcedure", connection);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?v_ListingId", MySql.Data.MySqlClient.MySqlDbType.Int32));



                command.Parameters["?v_ListingId"].Direction = ParameterDirection.Input;



                command.Parameters["?v_ListingId"].Value = ListingId;



                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Dispose();
                connection.Close();
            }


        }

        public static void InsertDefaultSetting(DataTable dt)
        {


            string connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionStringMySQL"].ToString();

            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = null;
            try
            {

                connection.Open();

                command = connection.CreateCommand();

                command.Connection = connection;

                command.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TimeOut"].ToString());

                command.CommandType = CommandType.StoredProcedure;

                //***************************************INSERT NEW RECORD TO DATAWAREHOUSE TABLE***************************************

                command.CommandText = "InsertDefaultSettingStoredProcedure";

                

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?InventorySorting", MySql.Data.MySqlClient.MySqlDbType.VarChar,45));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?SoldOut", MySql.Data.MySqlClient.MySqlDbType.VarChar, 45));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?DealershipId", MySql.Data.MySqlClient.MySqlDbType.VarChar, 45));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?Cragislist", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?CraigslistPassword", MySql.Data.MySqlClient.MySqlDbType.VarChar, 45));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?Ebay", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?EbayPassword", MySql.Data.MySqlClient.MySqlDbType.VarChar, 45));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?CarFax", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?CarFaxPassword", MySql.Data.MySqlClient.MySqlDbType.VarChar, 45));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?Manheim", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?ManheimPassword", MySql.Data.MySqlClient.MySqlDbType.VarChar, 45));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?KellyBlueBook", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?KellyPassword", MySql.Data.MySqlClient.MySqlDbType.VarChar, 45));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?BlackBook", MySql.Data.MySqlClient.MySqlDbType.VarChar, 45));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?BlackBookPassword", MySql.Data.MySqlClient.MySqlDbType.VarChar, 45));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?AutoCheck", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?AutoCheckPassword", MySql.Data.MySqlClient.MySqlDbType.VarChar, 45));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?AppraisalNotification", MySql.Data.MySqlClient.MySqlDbType.Bit));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?WholeNotification", MySql.Data.MySqlClient.MySqlDbType.Bit));
                
                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?InventoryNotification", MySql.Data.MySqlClient.MySqlDbType.Bit));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?24Hnotification", MySql.Data.MySqlClient.MySqlDbType.Bit));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?NoteNotification", MySql.Data.MySqlClient.MySqlDbType.Bit));



                command.Parameters["?InventorySorting"].Direction = ParameterDirection.Input;

                command.Parameters["?SoldOut"].Direction = ParameterDirection.Input;

                command.Parameters["?DealershipId"].Direction = ParameterDirection.Input;

                command.Parameters["?Cragislist"].Direction = ParameterDirection.Input;

                command.Parameters["?CraigslistPassword"].Direction = ParameterDirection.Input;

                command.Parameters["?Ebay"].Direction = ParameterDirection.Input;

                command.Parameters["?EbayPassword"].Direction = ParameterDirection.Input;

                command.Parameters["?CarFax"].Direction = ParameterDirection.Input;

                command.Parameters["?CarFaxPassword"].Direction = ParameterDirection.Input;

                command.Parameters["?Manheim"].Direction = ParameterDirection.Input;

                command.Parameters["?ManheimPassword"].Direction = ParameterDirection.Input;

                command.Parameters["?KellyBlueBook"].Direction = ParameterDirection.Input;

                command.Parameters["?KellyPassword"].Direction = ParameterDirection.Input;

                command.Parameters["?BlackBook"].Direction = ParameterDirection.Input;

                command.Parameters["?BlackBookPassword"].Direction = ParameterDirection.Input;

                command.Parameters["?AutoCheck"].Direction = ParameterDirection.Input;

                command.Parameters["?AutoCheckPassword"].Direction = ParameterDirection.Input;

                command.Parameters["?AppraisalNotification"].Direction = ParameterDirection.Input;

                command.Parameters["?WholeNotification"].Direction = ParameterDirection.Input;

                command.Parameters["?InventoryNotification"].Direction = ParameterDirection.Input;

                command.Parameters["?24Hnotification"].Direction = ParameterDirection.Input;

                command.Parameters["?NoteNotification"].Direction = ParameterDirection.Input;

             
                foreach (DataRow drRow in dt.Rows)
                {

                    command.Parameters["?InventorySorting"].Value = "Make";

                    command.Parameters["?SoldOut"].Value = "Delete (Default)";

                    command.Parameters["?DealershipId"].Value = drRow["idWhitmanenterpriseDealership"].ToString();

                    command.Parameters["?Cragislist"].Value = "vincontrol";

                    command.Parameters["?CraigslistPassword"].Value = "password";

                    command.Parameters["?Ebay"].Value = "vincontrol";

                    command.Parameters["?EbayPassword"].Value = "password";

                    command.Parameters["?CarFax"].Value = "vincontrol";

                    command.Parameters["?CarFaxPassword"].Value = "password";

                    command.Parameters["?Manheim"].Value = "vincontrol";

                    command.Parameters["?ManheimPassword"].Value = "password";

                    command.Parameters["?KellyBlueBook"].Value = "vincontrol";

                    command.Parameters["?KellyPassword"].Value = "password";

                    command.Parameters["?BlackBook"].Value = "vincontrol";
                    
                    command.Parameters["?BlackBookPassword"].Value = "password";

                    command.Parameters["?AutoCheck"].Value = "vincontrol";

                    command.Parameters["?AutoCheckPassword"].Value = "password";

                    command.Parameters["?AppraisalNotification"].Value = false;

                    command.Parameters["?WholeNotification"].Value = false;

                    command.Parameters["?InventoryNotification"].Value = false;

                    command.Parameters["?24Hnotification"].Value = false;

                    command.Parameters["?NoteNotification"].Value = false;

                  


                    command.ExecuteNonQuery();


                }



            }
            catch (Exception ex)
            {
                throw new Exception("Exception is " + ex.Message);

            }
            finally
            {
                connection.Close();

                connection.Dispose();
            }


        }

        public static DataTable initialVinControlDataTable()
        {
            DataTable workTable = new DataTable("VinControl");
            

            workTable.Columns.Add("ModelYear", typeof(string));
            workTable.Columns.Add("Make", typeof(String));
            workTable.Columns.Add("Model", typeof(String));
            workTable.Columns.Add("Trim", typeof(String));
            workTable.Columns.Add("VINNumber", typeof(String));
            workTable.Columns.Add("StockNumber", typeof(String));
            workTable.Columns.Add("SalePrice", typeof(String));
            workTable.Columns.Add("MSRP", typeof(String));
            workTable.Columns.Add("Mileage", typeof(String));
            workTable.Columns.Add("ExteriorColor", typeof(String));
            workTable.Columns.Add("InteriorColor", typeof(String));
            workTable.Columns.Add("InteriorSurface", typeof(String));
            workTable.Columns.Add("BodyType", typeof(String));
            workTable.Columns.Add("EngineType", typeof(String));
            workTable.Columns.Add("DriveTrain", typeof(String));
            workTable.Columns.Add("Cylinders", typeof(String));
            workTable.Columns.Add("Liters", typeof(String));
            workTable.Columns.Add("FuelType", typeof(String));
            workTable.Columns.Add("Tranmission", typeof(String));
            workTable.Columns.Add("Doors", typeof(String));
            workTable.Columns.Add("Certified", typeof(Boolean));
            workTable.Columns.Add("CarsOptions", typeof(String));
            workTable.Columns.Add("Descriptions", typeof(String));
            workTable.Columns.Add("CarImageUrl", typeof(String));
            workTable.Columns.Add("DateInStock", typeof(String));
            workTable.Columns.Add("DealershipName", typeof(String));
            workTable.Columns.Add("DealershipAddress", typeof(String));
            workTable.Columns.Add("DealershipCity", typeof(String));
            workTable.Columns.Add("DealershipState", typeof(String));
            workTable.Columns.Add("DealershipPhone", typeof(String));
            workTable.Columns.Add("DealershipId", typeof(String));
            workTable.Columns.Add("DealerCost", typeof(String));
            workTable.Columns.Add("ACV", typeof(String));
            workTable.Columns.Add("DefaultImageUrl", typeof(String));
            workTable.Columns.Add("NewUsed", typeof(string));
            workTable.Columns.Add("AddToInventoryBy", typeof(String));
            workTable.Columns.Add("AppraisalId", typeof(String));
            
            return workTable;

        }


        public static DataTable initialAutoTraderTable()
        {
            DataTable workTable = new DataTable("AutoTrader");


            workTable.Columns.Add("DealerId", typeof(String));
            workTable.Columns.Add("StockNumber", typeof(String));
            workTable.Columns.Add("Year", typeof(String));
            workTable.Columns.Add("Make", typeof(String));
            workTable.Columns.Add("Model", typeof(String));
            workTable.Columns.Add("Trim", typeof(String));
            workTable.Columns.Add("VIN", typeof(String));
            workTable.Columns.Add("Mileage", typeof(String));

            workTable.Columns.Add("Price", typeof(String));
            workTable.Columns.Add("ExteriorColor", typeof(String));
            workTable.Columns.Add("InteriorColor", typeof(String));
            workTable.Columns.Add("Tranmission", typeof(String));
            workTable.Columns.Add("PhysicalImages", typeof(String));
            workTable.Columns.Add("Descriptions", typeof(String));
            workTable.Columns.Add("BodyType", typeof(String));
            workTable.Columns.Add("EngineType", typeof(String));
            workTable.Columns.Add("DriveType", typeof(String));
            workTable.Columns.Add("FuelType", typeof(String));
            workTable.Columns.Add("Options", typeof(String));
            workTable.Columns.Add("NewUsed", typeof(String));

            workTable.Columns.Add("ImageURL", typeof(String));


            workTable.Columns.Add("VideoURL", typeof(String));

            workTable.Columns.Add("VideoSource", typeof(String));
            workTable.Columns.Add("Age", typeof(String));

            return workTable;

        }

        public static DataTable initialCarsComTable()
        {
            DataTable workTable = new DataTable("CarsCom");


            workTable.Columns.Add("DealerId", typeof(String));
            workTable.Columns.Add("Type", typeof(String));
            workTable.Columns.Add("StockNumber", typeof(String));
            workTable.Columns.Add("Vin", typeof(String));
            workTable.Columns.Add("Year", typeof(String));
            workTable.Columns.Add("Make", typeof(String));
            workTable.Columns.Add("Model", typeof(String));
            workTable.Columns.Add("Body", typeof(String));
            workTable.Columns.Add("Trim-Style", typeof(String));
            workTable.Columns.Add("ExtColor", typeof(String));
            workTable.Columns.Add("IntColor", typeof(String));
            workTable.Columns.Add("EngineCylinders", typeof(String));
            workTable.Columns.Add("EngineDisplacement", typeof(String));
            workTable.Columns.Add("EngineDescription", typeof(String));
            workTable.Columns.Add("Tranmission", typeof(String));
            workTable.Columns.Add("Miles", typeof(String));
            workTable.Columns.Add("SellingPrice", typeof(String));
            workTable.Columns.Add("Null-1", typeof(String));
            workTable.Columns.Add("Null-2", typeof(String));
            workTable.Columns.Add("Null-3", typeof(String));
            workTable.Columns.Add("DealerTagLine", typeof(String));
            workTable.Columns.Add("AddedOptions", typeof(String));
            

            return workTable;

        }

        public static DataTable initialCarsComImageTable()
        {
            DataTable workTable = new DataTable("CarsComImages");


            workTable.Columns.Add("DealerId", typeof(String));
            workTable.Columns.Add("VehicleId", typeof(String));
            workTable.Columns.Add("VersionDate", typeof(String));
            workTable.Columns.Add("URL", typeof(String));
            workTable.Columns.Add("ImageSequence", typeof(String));
         


            return workTable;

        }



        public static void updateCategoryID(DataTable dt)
        {
            //UpdateCategoryInWareHouseStoredProcedure



            string connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionStringMySQL"].ToString();

            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = null;
            try
            {

                connection.Open();

                command = connection.CreateCommand();

                command.Connection = connection;

                command.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TimeOut"].ToString());

                command.CommandType = CommandType.StoredProcedure;

                //***************************************INSERT NEW RECORD TO DATAWAREHOUSE TABLE***************************************

                command.CommandText = "UpdateCategoryInWareHouseStoredProcedure";



                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?v_ListingId", MySql.Data.MySqlClient.MySqlDbType.Int32));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?v_CategoryID", MySql.Data.MySqlClient.MySqlDbType.Int32));





                command.Parameters["?v_ListingId"].Direction = ParameterDirection.Input;

                command.Parameters["?v_CategoryID"].Direction = ParameterDirection.Input;

          


                foreach (DataRow drRow in dt.Rows)
                {

                    command.Parameters["?v_ListingId"].Value = drRow.Field<int>("ListingID");

                    command.Parameters["?v_CategoryID"].Value = drRow.Field<int>("CategoryId");

         
                    command.ExecuteNonQuery();


                }



            }
            catch (Exception ex)
            {
                throw new Exception("Exception is " + ex.Message);

            }
            finally
            {
                connection.Close();

                connection.Dispose();
            }

        }


        public static void updateDealerAddress(DataTable dt,Hashtable hash)
        {
           

            string connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionStringMySQL"].ToString();

            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = null;

            string exceptionZipCode = "";
            try
            {

                connection.Open();

                command = connection.CreateCommand();

                command.Connection = connection;

                command.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TimeOut"].ToString());

                command.CommandType = CommandType.StoredProcedure;

                //***************************************INSERT NEW RECORD TO DATAWAREHOUSE TABLE***************************************

                command.CommandText = "UpdateDealerAddress";



                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?v_DealerID", MySql.Data.MySqlClient.MySqlDbType.Int32));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?v_State", MySql.Data.MySqlClient.MySqlDbType.VarChar,100));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?v_City", MySql.Data.MySqlClient.MySqlDbType.VarChar,100));



                command.Parameters["?v_DealerID"].Direction = ParameterDirection.Input;
                
                command.Parameters["?v_State"].Direction = ParameterDirection.Input;

                command.Parameters["?v_City"].Direction = ParameterDirection.Input;




                foreach (DataRow drRow in dt.Rows)
                {
                    int ZipCode =Convert.ToInt32(drRow.Field<string>("ZipCode").Trim());

                    exceptionZipCode = ZipCode.ToString() +"***"  + drRow.Field<int>("idWhitmanenterpriseDealership");

                    if (hash.Contains(ZipCode))
                    {
                        ZipCode zip = (ZipCode)hash[ZipCode];

                        command.Parameters["?v_DealerID"].Value = drRow.Field<int>("idWhitmanenterpriseDealership");

                        command.Parameters["?v_State"].Value = zip.State;

                        command.Parameters["?v_City"].Value = zip.City;
                    }
                    else if (hash.Contains(ZipCode - 1))
                    {
                        ZipCode zip = (ZipCode)hash[ZipCode-1];

                        command.Parameters["?v_DealerID"].Value = drRow.Field<int>("idWhitmanenterpriseDealership");

                        command.Parameters["?v_State"].Value = zip.State;

                        command.Parameters["?v_City"].Value = zip.City;
                    }


                    command.ExecuteNonQuery();


                }



            }
            catch (Exception ex)
            {
                throw new Exception("Exception is " + exceptionZipCode + ex.Message);

            }
            finally
            {
                connection.Close();

                connection.Dispose();
            }

        }



        public static void updateStreetAddress(DataTable dt)
        {


            string connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionStringMySQL"].ToString();

            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = null;

            string exceptionZipCode = "";
            try
            {

                connection.Open();

                command = connection.CreateCommand();

                command.Connection = connection;

                command.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TimeOut"].ToString());

                command.CommandType = CommandType.StoredProcedure;

                //***************************************INSERT NEW RECORD TO DATAWAREHOUSE TABLE***************************************

                command.CommandText = "UpdateDealerStreetAddress";



                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?v_DealerID", MySql.Data.MySqlClient.MySqlDbType.Int32));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?v_StreetAddress", MySql.Data.MySqlClient.MySqlDbType.VarChar, 100));

             


                command.Parameters["?v_DealerID"].Direction = ParameterDirection.Input;

                command.Parameters["?v_StreetAddress"].Direction = ParameterDirection.Input;

            


                foreach (DataRow drRow in dt.Rows)
                {
                    string city = drRow.Field<string>("city");

                    string fullAddress=drRow.Field<string>("DealershipAddress");

                    command.Parameters["?v_DealerID"].Value = drRow.Field<int>("idWhitmanenterpriseDealership");

                    if (fullAddress.IndexOf(city) != -1)
                        command.Parameters["?v_StreetAddress"].Value = fullAddress.Substring(0, fullAddress.IndexOf(city)).Trim();
                    else
                        command.Parameters["?v_StreetAddress"].Value = fullAddress;

                    
                    command.ExecuteNonQuery();


                }



            }
            catch (Exception ex)
            {
                throw new Exception("Exception is " + exceptionZipCode + ex.Message);

            }
            finally
            {
                connection.Close();

                connection.Dispose();
            }

        }


        public static DataSet GetInventoryForDealership(int dealershipId)
        {
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionStringMySQL"].ToString();

            MySqlConnection connection = new MySqlConnection(connectionString);



            try
            {

                MySqlCommand command = new MySqlCommand("SelectCarsWithDealershipId", connection);

                command.CommandType = CommandType.StoredProcedure;

                command.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TimeOut"].ToString());


                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?v_DealershipId", MySql.Data.MySqlClient.MySqlDbType.Int32));

                command.Parameters["?v_DealershipId"].Direction = ParameterDirection.Input;


                command.Parameters["?v_DealershipId"].Value = dealershipId;


                MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                DataSet ds = new DataSet();

                adapter.Fill(ds);

                return ds;



            }
            catch (Exception ex)
            {
                throw new Exception("Exception is " + ex.Message);

            }



        }

        public static DataSet GetInventoryForDealershipCL(int dealershipId)
        {
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionStringMySQLCL"].ToString();

            MySqlConnection connection = new MySqlConnection(connectionString);



            try
            {

                MySqlCommand command = new MySqlCommand("SelectCarsWithDealershipId", connection);

                command.CommandType = CommandType.StoredProcedure;

                command.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TimeOut"].ToString());


                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?v_DealershipId", MySql.Data.MySqlClient.MySqlDbType.Int32));

                command.Parameters["?v_DealershipId"].Direction = ParameterDirection.Input;


                command.Parameters["?v_DealershipId"].Value = dealershipId;


                MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                DataSet ds = new DataSet();

                adapter.Fill(ds);

                return ds;



            }
            catch (Exception ex)
            {
                throw new Exception("Exception is " + ex.Message);

            }



        }

        public static void UpdateCarImageURL(string ListingId, string ThumbnailImageURL)
        {

            string connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionStringMySQL"].ToString();

            MySqlConnection connection = new MySqlConnection(connectionString);


            try
            {
                connection.Open();

                //MySqlCommand command = new MySqlCommand("UpdateThumbnailURLStoredProcedure", connection);

                MySqlCommand command = new MySqlCommand("UpdateCarImageUrlStoredProcedure", connection);

                

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?v_ListingId", MySql.Data.MySqlClient.MySqlDbType.Int32));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?v_CarImageUrl", MySql.Data.MySqlClient.MySqlDbType.LongText));

                //command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?v_ThumbnailImageURL", MySql.Data.MySqlClient.MySqlDbType.LongText));



                command.Parameters["?v_ListingId"].Direction = ParameterDirection.Input;

                command.Parameters["?v_CarImageUrl"].Direction = ParameterDirection.Input;

                //command.Parameters["?v_ThumbnailImageURL"].Direction = ParameterDirection.Input;


                command.Parameters["?v_ListingId"].Value = ListingId;

                command.Parameters["?v_CarImageUrl"].Value = ThumbnailImageURL;

                //command.Parameters["?v_ThumbnailImageURL"].Value = ThumbnailImageURL;



                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Dispose();
                connection.Close();
            }


        }

        public static void UpdateCarImageThubmnailURL(string ListingId, string ThumbnailImageURL)
        {

            string connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionStringMySQL"].ToString();

            MySqlConnection connection = new MySqlConnection(connectionString);


            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("UpdateThumbnailURLStoredProcedure", connection);

            



                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?v_ListingId", MySql.Data.MySqlClient.MySqlDbType.Int32));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?v_ThumbnailImageURL", MySql.Data.MySqlClient.MySqlDbType.LongText));



                command.Parameters["?v_ListingId"].Direction = ParameterDirection.Input;

                

                command.Parameters["?v_ThumbnailImageURL"].Direction = ParameterDirection.Input;


                command.Parameters["?v_ListingId"].Value = ListingId;



                command.Parameters["?v_ThumbnailImageURL"].Value = ThumbnailImageURL;



                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Dispose();
                connection.Close();
            }


        }

        public static void updateTrim(int ListingId, string Trim)
        {

            string connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionStringMySQLCL"].ToString();

            MySqlConnection connection = new MySqlConnection(connectionString);


            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("UpdateTrimStoredProcedure", connection);





                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?v_ListingId", MySql.Data.MySqlClient.MySqlDbType.Int32));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?v_Trim", MySql.Data.MySqlClient.MySqlDbType.VarChar, 255));



                command.Parameters["?v_ListingId"].Direction = ParameterDirection.Input;



                command.Parameters["?v_Trim"].Direction = ParameterDirection.Input;


                command.Parameters["?v_ListingId"].Value = ListingId;



                command.Parameters["?v_Trim"].Value = Trim;



                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Dispose();
                connection.Close();
            }


        }

        public static void UpdateStandardOption(int ListingId, string standardOption)
        {

            string connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionStringMySQL"].ToString();

            MySqlConnection connection = new MySqlConnection(connectionString);


            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("UpdateStandardOptionStoredProcedure", connection);





                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?v_ListingId", MySql.Data.MySqlClient.MySqlDbType.Int32));

                command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("?v_standardOption", MySql.Data.MySqlClient.MySqlDbType.LongText));



                command.Parameters["?v_ListingId"].Direction = ParameterDirection.Input;



                command.Parameters["?v_standardOption"].Direction = ParameterDirection.Input;


                command.Parameters["?v_ListingId"].Value = ListingId;



                command.Parameters["?v_standardOption"].Value = standardOption;



                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Dispose();
                connection.Close();
            }


        }
        

    }
}
