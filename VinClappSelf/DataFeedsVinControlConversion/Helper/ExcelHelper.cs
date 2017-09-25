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
using System.Linq;
using System.Net;

namespace DataFeedsVinControlConversion
{
    public class ExcelHelper
    {

        private static int _columnCount;

        private static DataTable changeColumName(DataTable dt)
        {
            dt.Columns["Column-0"].ColumnName = "DealerID";
            dt.Columns["Column-1"].ColumnName = "StockNumber";
            dt.Columns["Column-2"].ColumnName = "Year";
            dt.Columns["Column-3"].ColumnName = "Make";
            dt.Columns["Column-4"].ColumnName = "Model";
            dt.Columns["Column-5"].ColumnName = "TrimLevel";
            dt.Columns["Column-6"].ColumnName = "VIN";
            dt.Columns["Column-7"].ColumnName = "Odometer";
            dt.Columns["Column-8"].ColumnName = "MSRP";
            dt.Columns["Column-9"].ColumnName = "Price";
            dt.Columns["Column-10"].ColumnName = "ExteriorColor";
            dt.Columns["Column-11"].ColumnName = "InteriorColor";
            dt.Columns["Column-12"].ColumnName = "TransmissionType";
            dt.Columns["Column-15"].ColumnName = "BodyType";
            dt.Columns["Column-18"].ColumnName = "FuelType";
            dt.Columns["Column-19"].ColumnName = "Options";

            dt.Columns["Column-20"].ColumnName = "PicURL";
            dt.Rows.RemoveAt(0);
            return dt;
        }

        public static DataTable PopulateDataTableFromTextFile(string path, NetworkCredential ClappCredential)
        {
            _columnCount = 0;

            BasicFTPClient MyClient = new BasicFTPClient();

            DataTable dt = new DataTable();

            try
            {
                byte[] data = MyClient.DownloadData(path, ClappCredential);
                MemoryStream memoryStream = new MemoryStream(data);
                StreamReader reader = new StreamReader(memoryStream);

                String strLine = String.Empty;
                Int32 iLineCount = 0;
                do
                {
                    strLine = reader.ReadLine();
                    if (strLine == null)
                    { break; }
                    if (0 == iLineCount++)
                    {
                        dt = CreateDataTableForTabbedData(strLine);
                    }
                    AddDataRowToTable(strLine, dt);
                } while (true);




                if (dt.Columns.Count > 20)
                {
                  
                    foreach (DataRow drRow in dt.Rows)
                    {
                        if (!String.IsNullOrEmpty(drRow["Column-20"].ToString()) && !drRow["Column-20"].ToString().Contains("http"))
                            drRow["Column-20"] = "";
                        for (int i = 20; i < dt.Columns.Count; i++)
                        {
                            if (!String.IsNullOrEmpty(drRow[i].ToString()))
                              drRow["Column-20"] += "," + drRow[i].ToString();
                            
                        }

                        if (!String.IsNullOrEmpty(drRow["Column-20"].ToString()) && drRow["Column-20"].ToString().Substring(0, 1).Equals(","))
                            drRow["Column-20"] = drRow["Column-20"].ToString().Substring(1);
                    }
                }

                dt.Columns.Remove("Column-13");
                dt.Columns.Remove("Column-14");
                dt.Columns.Remove("Column-16");
                dt.Columns.Remove("Column-17");
            

               return changeColumName(dt);

               // return dt;
            }


            catch (Exception ex)
            {
                throw new Exception("Exception is " + ex.Message);
            }


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
    }
}
