using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace DataFeedsVinControlConversion.Helper
{
    public class VinClappFTPClient
    {
        private string Username;

        private string Password;

        private string Host;

        private int Port;

        public VinClappFTPClient()
        {

            Host = System.Configuration.ConfigurationManager.AppSettings["CLAPPFTPHost"].ToString();

            Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CLAPPFTPPort"].ToString());

            Username = System.Configuration.ConfigurationManager.AppSettings["CLAPPFTPUsername"].ToString();

            Password = System.Configuration.ConfigurationManager.AppSettings["CLAPPFTPPassword"].ToString();
        }

        public VinClappFTPClient(string theHost)
        {
            Host = theHost;

            Port = 21;
        }



        public byte[] DownloadData(string path, NetworkCredential ClappCredential)
        {
            // Get the object used to communicate with the server.
            WebClient request = new WebClient();



            // Logon to the server using username + password
            request.Credentials = ClappCredential;

            byte[] data = request.DownloadData(BuildServerUri(path));

            request.Dispose();

            return data;
        }

        public byte[] DownloadDataNoCredential(string path)
        {
            // Get the object used to communicate with the server.
            var request = new WebClient();

            // Logon to the server using username + password

            byte[] data = request.DownloadData(BuildServerUri(path));

            request.Dispose();

            return data;
        }


        private Uri BuildServerUri(string Path)
        {
            return new Uri(String.Format("ftp://{0}:{1}/{2}", Host, Port, Path));
        }
    }
}
