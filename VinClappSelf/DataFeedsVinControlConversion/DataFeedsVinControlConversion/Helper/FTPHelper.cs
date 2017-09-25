using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using DataFeedsVinControlConversion.Helper;
using LumenWorks.Framework.IO.Csv;
using Starksoft.Net.Ftp;
using System.IO;

namespace DataFeedsVinControlConversion
{

    public sealed class FTPHelper
    {
        public static FtpClient FtpClient;

        public static void ConnectToFtpServer()
        {


            string FtpHost = System.Configuration.ConfigurationManager.AppSettings["CLAPPFTPHost"].ToString();

            int FtpPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CLAPPFTPPort"].ToString());

            string FtpUsername = System.Configuration.ConfigurationManager.AppSettings["CLAPPFTPUsername"].ToString();

            string FtpPassword = System.Configuration.ConfigurationManager.AppSettings["CLAPPFTPPassword"].ToString();


            FtpClient = new FtpClient(FtpHost, FtpPort);

            FtpClient.Open(FtpUsername, FtpPassword);

        }

        public static void CloseConnectionToFtpSever()
        {
            FtpClient.Close();
        }

        public static void UploadToCarsComFtpServer(string localPath, string remotePath)
        {
            try
            {
                //subFolder = "/" + subFolder;

                //FtpClient.ChangeDirectory(subFolder);

                FtpClient.PutFile(localPath, remotePath, FileAction.Create);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                FtpClient.Close();
                FtpClient = null;
            }

        }


        public static Stream getFilesPerDealerId(string dealerId)
        {
            try
            {
                string FtpFilePath = "/" + dealerId + ".txt";
                Stream stream = null;
                FtpClient.GetFile(FtpFilePath, stream, true);

                return stream;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                FtpClient.Close();
                FtpClient = null;
            }
        }

        public static CachedCsvReader GetInventoryAutoTraderFormatWithoutHeader(string path, NetworkCredential ClappCredential)
        {

            var MyClient = new VinClappFTPClient();

            try
            {
                byte[] data = MyClient.DownloadData(path, ClappCredential);
                var memoryStream = new MemoryStream(data);
                var reader = new StreamReader(memoryStream);
                var cr = new CachedCsvReader(reader, false, ',');

                return cr;


            }
            catch (Exception ex)
            {

                throw new Exception("Exception is " + ex.Message);
            }



        }
        public static CachedCsvReader GetInventoryAutoTraderFormatWithoutHeade222r(string path, NetworkCredential ClappCredential)
        {

            var MyClient = new VinClappFTPClient();

            try
            {
                byte[] data = MyClient.DownloadData(path, ClappCredential);
                var memoryStream = new MemoryStream(data);
                var reader = new StreamReader(memoryStream);
                var cr = new CachedCsvReader(reader, true, '\t');

                return cr;


            }
            catch (Exception ex)
            {

                throw new Exception("Exception is " + ex.Message);
            }



        }

    }
}
