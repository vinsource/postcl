using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Clapp.WpfControlLibrary.UploadImageControl.Commands;
using Clapp.WpfControlLibrary.UploadImageControl.Interfaces;
using Clapp.WpfControlLibrary.UploadImageControl.ViewModels;

namespace Clapp.WpfControlLibrary.UploadImageControl
{
    /// <summary>
    /// Interaction logic for WPFUploadImage.xaml
    /// </summary>
    public partial class WPFUploadImage : UserControl, IView
    {
        public WPFUploadImage(int dealerId, string imageServiceURL, int inventoryStatus, int listingId, string Vin)
        {
            IntApp(dealerId, imageServiceURL, inventoryStatus, listingId, Vin);
            InitializeComponent();
            Loaded += new RoutedEventHandler(WPFUploadImage_Loaded);
        }

        private void IntApp(int dealerId, string imageServiceURL, int inventoryStatus, int listingId, string Vin)
        {
            App.DealerId = dealerId;
            App.ImageServiceURL = imageServiceURL;
            App.InventoryStatus = inventoryStatus;
            App.ListingId = listingId;
            App.Vin = Vin;
            App.Dispatcher = Dispatcher;
        }

        void WPFUploadImage_Loaded(object sender, RoutedEventArgs e)
        {
            var _vm = new UploadViewModel(this);
            _vm.Close.CloseCommandComplete += new EventHandler(Close_CloseCommandComplete);
        }

        void Close_CloseCommandComplete(object sender, EventArgs e)
        {
            if (this.Close != null)
            {
                this.Close(null, null);
            }
        }

        public event EventHandler Close;

        public void SetDataContext(object context)
        {
            DataContext = context;
        }
    }
}
