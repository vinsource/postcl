using System;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Clapp.WpfControlLibrary.UploadImageControl.Helpers;
using Clapp.WpfControlLibrary.UploadImageControl.Models;
using Clapp.WpfControlLibrary.UploadImageControl.ViewModels;
using System.IO;

namespace Clapp.WpfControlLibrary.UploadImageControl.Commands
{
    public class UploadCommand : ICommand
    {
        private readonly FileViewModel _vm;

        public UploadCommand(FileViewModel vm)
        {
            _vm = vm;
        }

        private void StartNextUpload()
        {
            _vm.Parent.StartAll.DownloadingNumber--;
            _vm.Parent.StartAll.Execute(null);
        }

        public void Execute(object parameter)
        {
            var uriBuilder = new UriBuilder(App.ImageServiceURL);
            var r = new Random();
            uriBuilder.Query = string.Format("uploadedfile={0}&DealerId={1}&ListingId={2}&Vin={3}&Overlay={4}&r={5}", _vm.FileName, App.DealerId, App.ListingId, App.Vin, _vm.Parent.IsOverlay ? 1 : 0, r.Next());

            var webrequest = (HttpWebRequest)WebRequest.Create(uriBuilder.Uri);
            webrequest.Method = "POST";
            webrequest.BeginGetRequestStream(new AsyncCallback(WriteCallback), webrequest);
        }

        private void WriteCallback(IAsyncResult asynchronousResult)
        {
            var webrequest = (HttpWebRequest)asynchronousResult.AsyncState;
            // End the operation.
            var requestStream = webrequest.EndGetRequestStream(asynchronousResult);
            var buffer = new Byte[4096];
            var bytesRead = 0;
            Stream fileStream = _vm.FileInfo;
            fileStream.Position = 0;
            while ((bytesRead =
            fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                requestStream.Write(buffer, 0, bytesRead);
                requestStream.Flush();
            }

            fileStream.Close();
            requestStream.Close();
            webrequest.BeginGetResponse(new AsyncCallback(ReadCallback), webrequest);
        }

        private void ReadCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                var webrequest = (HttpWebRequest)asynchronousResult.AsyncState;
                var response = (HttpWebResponse)webrequest.EndGetResponse(asynchronousResult);
                var serializer =
                              new DataContractJsonSerializer(typeof(ServerImage));

                var result = (ServerImage)serializer.ReadObject(response.GetResponseStream());
                _vm.ImageUrl = result.FileUrl;
                _vm.ThumbnailImageUrl = result.ThumbnailUrl;
               
               App.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => UpdateInterface(result.FileUrl)));

            }
            catch (Exception e)
            {

                ErrorHandler.ShowWarning(e.InnerException + e.Message);
            }
        }

        void UpdateInterface(string FileUrl)
        {
            var image = new BitmapImage(new Uri(FileUrl));
            this._vm.ImageSource = image;
            MarkComplete();
        }

        private void MarkComplete()
        {
            _vm.IsComplete = true;
            _vm.IsFinish = true;
            _vm.Parent.NumberOfUploadedFiles++;
            _vm.Status = UploadStatus.Finish;
            StartNextUpload();
            //_vm.Parent.StartAll.DownloadingNumber--;
            //_vm.Parent.StartAll.Execute(null);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
        public event EventHandler UploadCommandComplete;
    }
}
