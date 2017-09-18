using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Clapp.WpfControlLibrary.UploadImageControl.Commands;
using Clapp.WpfControlLibrary.UploadImageControl.Helpers;
using Clapp.WpfControlLibrary.UploadImageControl.Models;
using Clapp.WpfControlLibrary.UploadImageControl.ViewModels;

namespace Clapp.WpfControlLibrary.UploadImageControl.ImageViewer
{
    /// <summary>
    /// Interaction logic for PhotoStack.xaml
    /// </summary>
    public partial class PhotoStack : UserControl
    {
        private const int _maximumPhotoInStack = 5;
        private List<Photo> _photoCollection;

        private int _rear;
        private int Rear
        {
            set
            {
                _rear = value;
                LeftButton.IsEnabled = _rear != 0;
            }
            get { return _rear; }
        }

        private int _front;
        private int Front
        {
            set
            {
                _front = value;
                RightButton.IsEnabled = _front != _photoCollection.Count;
            }
            get { return _front; }
        }
        public PhotoStack(int dealerId, string imageServiceURL, int inventoryStatus, int listingId, string Vin)
        {
            App.DealerId = dealerId;
            App.ImageServiceURL = imageServiceURL;
            App.InventoryStatus = inventoryStatus;
            App.ListingId = listingId;
            App.Vin = Vin;
            App.Dispatcher = Dispatcher;
            InitializeComponent();
        }

        private double ThumbnailWidth
        {
            get
            {
                return (grd.ActualWidth - 72) / 5.0;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var uriBuilder = new UriBuilder(App.ImageServiceURL);
            var r = new Random();
            uriBuilder.Query = string.Format("Function=GetImageUrlList&ListingId={0}&r={1}", App.ListingId, r.Next());

            var client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
            client.DownloadStringAsync(uriBuilder.Uri);
        }

        void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(e.Result)))
                {

                    var serializer = new DataContractJsonSerializer(typeof(List<ServerImage>));
                    
                        var result = (List<ServerImage>)serializer.ReadObject(ms);

                        //foreach (var url in result)
                        //{
                        //    var file = new FileViewModel(this, 0, String.Empty, new BitmapImage(new Uri(url.FileUrl)))
                        //    {
                        //        ImageUrl = url.FileUrl,
                        //        ThumbnailImageUrl = url.ThumbnailUrl,
                        //        IsComplete = true,
                        //        IsOnServer = true,
                        //        Status = UploadStatus.Existed
                        //    };
                        //    Files.Add(file);
                        //}
                        double thumbWidth = ThumbnailWidth;
                        bool setFirstItemChecked = true;
                        int count = 0;

                    _photoCollection = new List<Photo>();
                        foreach (var url in result)
                        {
                            _photoCollection.Add(new Photo(url.FileUrl));
                        }

                        foreach (var photo in _photoCollection)
                        {
                            AddPhotoToStack(photo, thumbWidth, setFirstItemChecked);
                            setFirstItemChecked = false;
                            count++;
                            if (count == _maximumPhotoInStack)
                            {
                                break;
                            }
                        }

                        //_photoCollection = new PhotoCollection();
                        //_photoCollection.Path = @"C:\Users\Public\Pictures\Sample Pictures";

                        //foreach (var photo in _photoCollection)
                        //{
                        //    AddPhotoToStack(photo, thumbWidth, setFirstItemChecked);
                        //    setFirstItemChecked = false;
                        //    count++;
                        //    if (count == _maximumPhotoInStack)
                        //    {
                        //        break;
                        //    }
                        //}
                        Rear = 0;
                        Front = count;

                    }
                   

                    //IsBusy = false;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void AddPhotoToStack(Photo photo, double thumbWidth, bool setFirstItemChecked, bool isInsert = false)
        {
            ThumbnailPhoto tp = new ThumbnailPhoto();
            tp.ThumbnailClick += new EventHandler<ThumbnailEventArgs>(tp_ThumbnailClick);
            tp.ImageSource = photo.Image;
            tp.ImageHeight = photos.ActualHeight - 30;
            tp.ImageWidth = thumbWidth;
            if (isInsert)
            {
                photos.Children.Insert(0, tp);
            }
            else
            {
                photos.Children.Add(tp);
            }
            tp.IsSelected = setFirstItemChecked;
        }

        void tp_ThumbnailClick(object sender, ThumbnailEventArgs e)
        {
            mainPhoto.Source = e.ImageSource;
            mainPhoto.Width = this.ActualWidth ;
        }

        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            //sclPhoto.ScrollToHorizontalOffset(sclPhoto.HorizontalOffset + ThumbnailWidth);
            bool isSelected = ((ThumbnailPhoto)photos.Children[0]).IsSelected;
            photos.Children.RemoveAt(0);
            AddPhotoToStack(_photoCollection[Front], ThumbnailWidth, isSelected);
            Front++;
            Rear++;

        }

        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            //sclPhoto.ScrollToHorizontalOffset(sclPhoto.HorizontalOffset - ThumbnailWidth);
            bool isSelected = ((ThumbnailPhoto)photos.Children[_maximumPhotoInStack - 1]).IsSelected;
            photos.Children.RemoveAt(_maximumPhotoInStack - 1);
            AddPhotoToStack(_photoCollection[--Rear], ThumbnailWidth, isSelected, true);
            Front--;
        }
    }
}
