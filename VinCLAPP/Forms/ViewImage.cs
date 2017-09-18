using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Clapp.WpfControlLibrary.UploadImageControl;
using Clapp.WpfControlLibrary.UploadImageControl.ImageViewer;

namespace VinCLAPP.Forms
{
    public partial class ViewImage : Form
    {
        private readonly int dealerId;
        private readonly string imageServiceURL;
        private readonly int inventoryStatus;
        private readonly int listingId;
        private readonly string VIN;

        public ViewImage(int dealerId, string imageServiceURL, int inventoryStatus, int listingId, string Vin)
        {
            this.dealerId = dealerId;
            this.imageServiceURL = imageServiceURL;
            this.inventoryStatus = inventoryStatus;
            this.listingId = listingId;
            VIN = Vin;
            Load += new EventHandler(ViewImage_Load);
            InitializeComponent();
        }

        void ViewImage_Load(object sender, EventArgs e)
        {
            PhotoStack wpfctl = new PhotoStack(dealerId, imageServiceURL, inventoryStatus, listingId, VIN);
            //wpfctl.Close += new EventHandler(wpfctl_Close);
            elementHost1.Child = wpfctl;
        }
    }
}
