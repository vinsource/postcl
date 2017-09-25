using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EnterpriseDT.Net.Ftp;
using Starksoft.Net.Ftp;

namespace ClappManagementSystem.Helper
{
    public class FTPHelper
    {
        public static FtpClient FtpClient;

        public static void ConnectToFTPCrailigstImageServer()
        {


            string FtpHost = System.Configuration.ConfigurationManager.AppSettings["HostGoDaddy"].ToString();

            int FtpPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PortGoDaddy"].ToString());

            string FtpUsername = System.Configuration.ConfigurationManager.AppSettings["UsernameGoDaddy"].ToString();

            string FtpPassword = System.Configuration.ConfigurationManager.AppSettings["PasswordGoDaddy"].ToString();


            FtpClient = new FtpClient(FtpHost, FtpPort);

            FtpClient.Open(FtpUsername, FtpPassword);

        }


        public static IEnumerable<FtpItem> GetFoldersFromFTP()
        {
            try
            {
                var FolderList = new string[]
                                     {
                                         "picsonweb", "vehicledetail", "imageleads", "carslisting", "imgpost",
                                         "motolisting", "imagedetail",
                                         "dealerimage", "sellsellcars2", "photosonweb", "sellsellsellcars1",
                                         "vinutility1", "imagehostingcenter", "picturedetail", "vinanalysis1",
                                         "imagesonweb", "customimages", "buybuybuycars1", "buybuybuycars2", "vingauge",
                                         "vinsell2", "vinanalyst1", "sellsellsellcars2", "sellsellcars1", "vinworld",
                                         "imagedetail2", "hostedimg2","hostedimg", "vinspecialist1", "test"
                                     };


                ConnectToFTPCrailigstImageServer();

                FtpClient.ChangeDirectory("/public_html");

                return FtpClient.GetDirList().Where(x => FolderList.Contains(x.Name)).ToList();
            }
            catch (Exception ex)
            {
                 System.Windows.Forms.MessageBox.Show(ex.Message);
                return null;
            }

            finally
            {
                FtpClient.Close();
                FtpClient = null;
            }
        }

        public static void CreateDirectory(IEnumerable<string> folderList)
        {
            var ftpConnect = new FTPConnection();
            try
            {

                ftpConnect.ServerAddress =
                    System.Configuration.ConfigurationManager.AppSettings["HostGoDaddy"].ToString();
                ftpConnect.ServerPort =
                    Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PortGoDaddy"].ToString());
                ftpConnect.UserName =
                    System.Configuration.ConfigurationManager.AppSettings["UsernameGoDaddy"].ToString();
                ftpConnect.Password =
                    System.Configuration.ConfigurationManager.AppSettings["PasswordGoDaddy"].ToString();
                
                ftpConnect.Connect();
             
                ftpConnect.ChangeWorkingDirectory("/public_html");

                foreach (var tmp in folderList)
                {
                    if (!ftpConnect.DirectoryExists(tmp))
                    ftpConnect.CreateDirectory(tmp);

                }

               
               
            

            }
            catch (Exception ex)
            {
                throw new Exception("Exception is " + ex.Message);

            }
            finally
            {
                ftpConnect.Close();
                //ftpConnect.Dispose();
            }
        }

        public static void DeleteDirectory(string remotePath)
        {
            var ftpConnect = new FTPConnection();
            try
            {

                ftpConnect.ServerAddress = System.Configuration.ConfigurationManager.AppSettings["HostGoDaddy"].ToString();
                ftpConnect.ServerPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PortGoDaddy"].ToString());
                ftpConnect.UserName = System.Configuration.ConfigurationManager.AppSettings["UsernameGoDaddy"].ToString();
                ftpConnect.Password = System.Configuration.ConfigurationManager.AppSettings["PasswordGoDaddy"].ToString();
                ftpConnect.Connect();
                //ftpConnect.Login();
                //ftpConnect.Timeout = 1200000;

                ftpConnect.ChangeWorkingDirectory("/public_html/" + remotePath);

                var files = ftpConnect.GetFileInfos().Where(x => !x.Name.Contains(".")).OrderBy(x=>x.LastModified);

                int index = 0;

                foreach (var f in files)
                {
                    var dtLastModified = f.LastModified;

                    int numberofDays = DateTime.Now.Subtract(dtLastModified).Days;

                    if (numberofDays > 15)
                    {
                        index++;
                    }
                }
                System.Windows.Forms.MessageBox.Show(index.ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //foreach (var tmp in files)
                //{
                //    //var dtLastModified = tmp.LastModified;

                //    //int numberofDays = DateTime.Now.Subtract(dtLastModified).Days;

                //    //if (numberofDays > 15)
                //    DeleteDirectoryRecursively(ftpConnect, tmp.Name);

                //    //System.Threading.Thread.Sleep(2000);
                //    break;
                //    ;

                //}

                //files = ftpConnect.GetFileInfos().Where(x => !x.Name.Contains(".")).OrderBy(x => x.LastModified);

                //System.Windows.Forms.MessageBox.Show(files.Count().ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception is " + ex.Message);

            }
            finally
            {
                ftpConnect.Close();
                //ftpConnect.Dispose();
            }
        }


        private static void DeleteDirectoryRecursively(FTPConnection ftpConnect, string remotePath)
        {
            ftpConnect.ChangeWorkingDirectory(remotePath);

            var files = ftpConnect.GetFileInfos().Where(x => !x.Name.Contains(".") || x.Name.Contains("jpg"));
           
            foreach(var tmp in files)
            {
                if (!tmp.Dir)
                {
                    ftpConnect.DeleteFile(tmp.Name);
                }
            }

            // delete all subdirectories in the remotePath directory
            foreach (var tmp in files)
            {
                if (tmp.Dir)
                    DeleteDirectoryRecursively(ftpConnect, tmp.Name);
            }

            // delete this directory
            ftpConnect.ChangeWorkingDirectoryUp();
            ftpConnect.DeleteDirectory(remotePath);
        }


       
    }
}
