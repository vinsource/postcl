﻿using System;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Input;
using Clapp.WpfControlLibrary.UploadImageControl.Helpers;
using Clapp.WpfControlLibrary.UploadImageControl.Models;
using System.IO;
using Clapp.WpfControlLibrary.UploadImageControl.ViewModels;

namespace Clapp.WpfControlLibrary.UploadImageControl.Commands
{
    public class SaveCommand : ICommand
    {
        private readonly UploadViewModel _vm;

        public SaveCommand(UploadViewModel vm)
        {
            _vm = vm;
        }

        public void Execute(object parameter)
        {
            if (_vm.NumberOfUploadedFiles == _vm.TotalNumberOfFiles)
            {
                _vm.IsBusy = true;
                var uriBuilder = new UriBuilder(App.ImageServiceURL);
                var r = new Random();
                uriBuilder.Query = string.Format("Function=SaveImageUrl&r={0}&InventoryStatus={1}&ListingId={2}",
                                                 r.Next(), App.InventoryStatus,
                                                 App.ListingId);
                var client = new WebClient();
                client.UploadStringCompleted += client_UploadStringCompleted;
                CarImage data = GetPostData();
                using (var ms = new MemoryStream())
                {
                    var serializer = new DataContractJsonSerializer(data.GetType());
                    serializer.WriteObject(ms, data);
                    ms.Position = 0;

                    using (var reader = new StreamReader(ms))
                    {
                        client.UploadStringAsync(uriBuilder.Uri, reader.ReadToEnd());
                    }
                }
            }
            else
            {
                ErrorHandler.ShowWarning("Wait until your upload is finished.");
            }
        }

        private CarImage GetPostData()
        {
            string thumbnailResult = _vm.Files.Aggregate(string.Empty, (current, file) => current + (file.ThumbnailImageUrl + ","));
            if (!String.IsNullOrEmpty(thumbnailResult))
            {
                thumbnailResult = thumbnailResult.Substring(0, thumbnailResult.Length - 1);
            }

            string fullResult = _vm.Files.Aggregate(string.Empty, (current, file) => current + (file.ImageUrl + ","));

            if (!String.IsNullOrEmpty(thumbnailResult))
            {
                fullResult = fullResult.Substring(0, fullResult.Length - 1);
            }

            return new CarImage
                {
                    FileUrLs = fullResult,
                    ThumbnailUrLs = thumbnailResult
                };
        }

        void client_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            var a = e.Result;
            _vm.IsBusy = false;
            //HtmlHelper.CloseForm();
            _vm.Close.Execute(null);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
        public event EventHandler SaveCommandComplete;

    }
}
