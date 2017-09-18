using System;
using System.IO;
using System.Windows.Forms;
using Clapp.WpfControlLibrary.UploadImageControl;

namespace VinCLAPP.Forms
{
    public partial class UploadPicture : Form
    {
        private readonly int dealerId;
        private readonly string imageServiceURL;
        private readonly int inventoryStatus;
        private readonly int listingId;
        private readonly string VIN;

        public UploadPicture(int dealerId, string imageServiceURL, int inventoryStatus, int listingId, string Vin)
        {
            this.dealerId = dealerId;
            this.imageServiceURL = imageServiceURL;
            this.inventoryStatus = inventoryStatus;
            this.listingId = listingId;
            VIN = Vin;
            Load += new EventHandler(UploadPicture_Load);
            InitializeComponent();
        }

        void UploadPicture_Load(object sender, EventArgs e)
        {
            WPFUploadImage wpfctl = new WPFUploadImage(dealerId, imageServiceURL, inventoryStatus, listingId, VIN);
            wpfctl.Close += new EventHandler(wpfctl_Close);
            elementHost.Child = wpfctl;

        }

        void wpfctl_Close(object sender, EventArgs e)
        {
            this.Close();
        }
      
    }
}