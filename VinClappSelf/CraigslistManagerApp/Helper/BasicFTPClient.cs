namespace CraigslistManagerApp
{
    public class BasicFTPClient
    {
       // public  string Username;

       // public string Password;

       // public string Host;

       // public int Port;

    
       // public BasicFTPClient()
       // {
         
           
       //     Username = System.Configuration.ConfigurationManager.AppSettings["Username"].ToString();

       //     Password = System.Configuration.ConfigurationManager.AppSettings["Password"].ToString();

       //     Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Port"].ToString());

       //     Host = System.Configuration.ConfigurationManager.AppSettings["Host"].ToString();

         
       // }

       // public BasicFTPClient(WhitmanEnterpriseFTPAccount ftpAccount)
       // {

       //     Username = ftpAccount.FTPUserName;

       //     Password = ftpAccount.FTPPassword;

       //     Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PortGoDaddy"].ToString());

       //     Host = System.Configuration.ConfigurationManager.AppSettings["HostGoDaddy"].ToString();


       // }

       //private Uri BuildServerUri(string Path)
       // {
       //     return new Uri(String.Format("ftp://{0}:{1}/{2}", Host, Port, Path));
       // }

       // /// <summary>
       // /// This method downloads the given file name from the FTP server
       // /// and returns a byte array containing its contents.
       // /// Throws a WebException on encountering a network error.
       // /// </summary>

       // public byte[] DownloadData(string path)
       // {
       //     // Get the object used to communicate with the server.
       //     WebClient request = new WebClient();

       //     // Logon to the server using username + password
       //     request.Credentials = new NetworkCredential(Username, Password);
            
       //     return request.DownloadData(BuildServerUri(path));
       // }

       // /// <summary>
       // /// This method downloads the FTP file specified by "ftppath" and saves
       // /// it to "destfile".
       // /// Throws a WebException on encountering a network error.
       // /// </summary>
       // public void DownloadFile(string ftppath, string destfile)
       // {
       //     // Download the data
       //     byte[] Data = DownloadData(ftppath);

       //     // Save the data to disk
       //     FileStream fs = new FileStream(destfile, FileMode.Create);
       //     fs.Write(Data, 0, Data.Length);
       //     fs.Close();
       // }

       // /// <summary>
       // /// Upload a byte[] to the FTP server
       // /// </summary>
       // /// <param name="path">Path on the FTP server (upload/myfile.txt)</param>
       // /// <param name="Data">A byte[] containing the data to upload</param>
       // /// <returns>The server response in a byte[]</returns>
         
       // public bool UploadData(string hostingPath,string ImageURL,string ImageFileName, byte[] Data)
       // {
       //     var ftpConnect = new FTPConnection();
       //     try
       //     {
                
       //         ftpConnect.ServerAddress = Host;
       //         ftpConnect.ServerPort = Port;
       //         ftpConnect.UserName = Username;
       //         ftpConnect.Password = Password;
       //         ftpConnect.Connect();
       //         //ftpConnect.Login();

       //         string[] pathSub = ImageURL.Split(new String[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

       //         ftpConnect.ChangeWorkingDirectory(hostingPath);

       //         foreach (string tmp in pathSub)
       //         {
       //             hostingPath += "/" + tmp;
       //             if (!ftpConnect.DirectoryExists(tmp))
       //                 ftpConnect.CreateDirectory(tmp);

       //             ftpConnect.ChangeWorkingDirectory(hostingPath);
       //         }

       //         ftpConnect.UploadByteArray(Data, ImageFileName);

       //         return true;

       //     }
       //     catch (Exception ex)
       //     {
       //         return false;

       //     }
       //     finally
       //     {
       //         ftpConnect.Close();
       //         //ftpConnect.Dispose();
       //     }


       // }


       // public void UploadData(string path, byte[] Data)
       // {
          
       //     WebClient request = new WebClient();

       //     // Logon to the server using username + password
       //     try
       //     {
       //         request.Credentials = new NetworkCredential(Username, Password);

       //         request.UploadData(BuildServerUri(path), Data);
       //     }
       //     catch (Exception ex)
       //     {
       //         //ServiceLog.ErrorLog("Exception is " + ex.Message);

       //     }
       //     finally
       //     {
       //         request.Dispose();
       //     }


       // }

       // /// <summary>
       // /// Load a file from disk and upload it to the FTP server
       // /// </summary>
       // /// <param name="ftppath">Path on the FTP server (/upload/myfile.txt)</param>
       // /// <param name="srcfile">File on the local harddisk to upload</param>
       // /// <returns>The server response in a byte[]</returns>

     

       // public void CreateDirectoryOnFTP(String inFTPServerAndPath,String inNewDirectory)
       // {
       //     // Step 1 - Open a request using the full URI, ftp://ftp.server.tld/path/file.ext
       //     FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(inFTPServerAndPath + "/" + inNewDirectory);

       //     // Step 2 - Configure the connection request
       //     request.Credentials = new NetworkCredential(Username, Password);
       //     request.UsePassive = true;
       //     request.UseBinary = true;
       //     request.KeepAlive = false;

       //     request.Method = WebRequestMethods.Ftp.MakeDirectory;

       //     // Step 3 - Call GetResponse() method to actually attempt to create the directory
       //     FtpWebResponse makeDirectoryResponse = (FtpWebResponse)request.GetResponse();
       // }

      

     


       
    }
}
