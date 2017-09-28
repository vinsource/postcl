namespace VinCLAPP.Forms
{
    partial class TrackingAdsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private static TrackingAdsForm sForm = null;
        public static TrackingAdsForm Instance()
        {
            if (sForm == null) { sForm = new TrackingAdsForm(); }

            else
            {
                sForm.Close();
                sForm = new TrackingAdsForm();
            }

            return sForm;
        }

        public static TrackingAdsForm Instance(int listingId)
        {
            if (sForm == null)
            {
                sForm = new TrackingAdsForm(listingId);
            }

            else
            {
                sForm.Close();
                sForm = new TrackingAdsForm(listingId);
            }

            return sForm;
        }

        public static TrackingAdsForm Instance(int listingId, int cityId)
        {
            if (sForm == null) { sForm = new TrackingAdsForm(listingId, cityId); }

            else
            {
                sForm.Close();
                sForm = new TrackingAdsForm(listingId, cityId);
            }

            return sForm;
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dGridViewSameCity = new System.Windows.Forms.DataGridView();
            this.ListingId = new System.Windows.Forms.DataGridViewLinkColumn();
            this.ClpostingId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CityId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HtmlCraigslistUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockNumber = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Title = new System.Windows.Forms.DataGridViewLinkColumn();
            this.CityName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewLinkColumn1 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.dataGridViewLinkColumn2 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.dataGridViewLinkColumn3 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Clbrowser = new System.Windows.Forms.WebBrowser();
            this.dGridViewOtherCities = new System.Windows.Forms.DataGridView();
            this.ListingIdOtherCities = new System.Windows.Forms.DataGridViewLinkColumn();
            this.ClpostingIdOther = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewLinkColumn5 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.dataGridViewLinkColumn6 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dGridViewSameCity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGridViewOtherCities)).BeginInit();
            this.SuspendLayout();
            // 
            // dGridViewSameCity
            // 
            this.dGridViewSameCity.AllowUserToAddRows = false;
            this.dGridViewSameCity.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dGridViewSameCity.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dGridViewSameCity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGridViewSameCity.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ListingId,
            this.ClpostingId,
            this.CityId,
            this.HtmlCraigslistUrl,
            this.StockNumber,
            this.Title,
            this.CityName});
            this.dGridViewSameCity.Location = new System.Drawing.Point(781, 2);
            this.dGridViewSameCity.Name = "dGridViewSameCity";
            this.dGridViewSameCity.Size = new System.Drawing.Size(490, 261);
            this.dGridViewSameCity.TabIndex = 3;
            this.dGridViewSameCity.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGridViewToday_CellClick_1);
            // 
            // ListingId
            // 
            this.ListingId.DataPropertyName = "ListingId";
            this.ListingId.HeaderText = "Id";
            this.ListingId.Name = "ListingId";
            this.ListingId.Width = 30;
            // 
            // ClpostingId
            // 
            this.ClpostingId.DataPropertyName = "TrackingId";
            this.ClpostingId.HeaderText = "ClpostingId";
            this.ClpostingId.Name = "ClpostingId";
            this.ClpostingId.Visible = false;
            // 
            // CityId
            // 
            this.CityId.DataPropertyName = "CityId";
            this.CityId.HeaderText = "CityId";
            this.CityId.Name = "CityId";
            this.CityId.Visible = false;
            // 
            // HtmlCraigslistUrl
            // 
            this.HtmlCraigslistUrl.DataPropertyName = "HtmlCraigslistUrl";
            this.HtmlCraigslistUrl.HeaderText = "HtmlCraigslistUrl";
            this.HtmlCraigslistUrl.Name = "HtmlCraigslistUrl";
            this.HtmlCraigslistUrl.Visible = false;
            // 
            // StockNumber
            // 
            this.StockNumber.DataPropertyName = "StockNumber";
            this.StockNumber.HeaderText = "Stock";
            this.StockNumber.Name = "StockNumber";
            this.StockNumber.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.StockNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.StockNumber.Width = 70;
            // 
            // Title
            // 
            this.Title.DataPropertyName = "Title";
            this.Title.HeaderText = "Posted";
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            this.Title.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Title.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Title.Width = 170;
            // 
            // CityName
            // 
            this.CityName.DataPropertyName = "CityName";
            this.CityName.HeaderText = "City";
            this.CityName.Name = "CityName";
            this.CityName.Width = 120;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(782, 264);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(266, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Same posting in other cities : ";
            // 
            // dataGridViewLinkColumn1
            // 
            this.dataGridViewLinkColumn1.DataPropertyName = "AutoId";
            this.dataGridViewLinkColumn1.HeaderText = "Id";
            this.dataGridViewLinkColumn1.Name = "dataGridViewLinkColumn1";
            this.dataGridViewLinkColumn1.Width = 30;
            // 
            // dataGridViewLinkColumn2
            // 
            this.dataGridViewLinkColumn2.DataPropertyName = "StockNumber";
            this.dataGridViewLinkColumn2.HeaderText = "Stock";
            this.dataGridViewLinkColumn2.Name = "dataGridViewLinkColumn2";
            this.dataGridViewLinkColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewLinkColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewLinkColumn2.Width = 70;
            // 
            // dataGridViewLinkColumn3
            // 
            this.dataGridViewLinkColumn3.DataPropertyName = "Title";
            this.dataGridViewLinkColumn3.HeaderText = "Posted/Renew Cars";
            this.dataGridViewLinkColumn3.Name = "dataGridViewLinkColumn3";
            this.dataGridViewLinkColumn3.ReadOnly = true;
            this.dataGridViewLinkColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewLinkColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewLinkColumn3.Width = 200;
            // 
            // Clbrowser
            // 
            this.Clbrowser.Location = new System.Drawing.Point(-1, 2);
            this.Clbrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.Clbrowser.Name = "Clbrowser";
            this.Clbrowser.Size = new System.Drawing.Size(776, 804);
            this.Clbrowser.TabIndex = 7;
            this.Clbrowser.Url = new System.Uri("http://www.craigslist.org/about/sites", System.UriKind.Absolute);
            // 
            // dGridViewOtherCities
            // 
            this.dGridViewOtherCities.AllowUserToAddRows = false;
            this.dGridViewOtherCities.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dGridViewOtherCities.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dGridViewOtherCities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGridViewOtherCities.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ListingIdOtherCities,
            this.ClpostingIdOther,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewLinkColumn5,
            this.dataGridViewLinkColumn6,
            this.dataGridViewTextBoxColumn9});
            this.dGridViewOtherCities.Location = new System.Drawing.Point(781, 289);
            this.dGridViewOtherCities.Name = "dGridViewOtherCities";
            this.dGridViewOtherCities.Size = new System.Drawing.Size(490, 517);
            this.dGridViewOtherCities.TabIndex = 8;
            this.dGridViewOtherCities.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGridViewOtherCities_CellClick_1);
            // 
            // ListingIdOtherCities
            // 
            this.ListingIdOtherCities.DataPropertyName = "ListingId";
            this.ListingIdOtherCities.HeaderText = "Id";
            this.ListingIdOtherCities.Name = "ListingIdOtherCities";
            this.ListingIdOtherCities.Width = 30;
            // 
            // ClpostingIdOther
            // 
            this.ClpostingIdOther.DataPropertyName = "TrackingId";
            this.ClpostingIdOther.HeaderText = "ClpostingId";
            this.ClpostingIdOther.Name = "ClpostingIdOther";
            this.ClpostingIdOther.Visible = false;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "CityId";
            this.dataGridViewTextBoxColumn7.HeaderText = "CityId";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Visible = false;
            this.dataGridViewTextBoxColumn7.Width = 120;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "HtmlCraigslistUrl";
            this.dataGridViewTextBoxColumn8.HeaderText = "HtmlCraigslistUrl";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Visible = false;
            this.dataGridViewTextBoxColumn8.Width = 120;
            // 
            // dataGridViewLinkColumn5
            // 
            this.dataGridViewLinkColumn5.DataPropertyName = "StockNumber";
            this.dataGridViewLinkColumn5.HeaderText = "Stock";
            this.dataGridViewLinkColumn5.Name = "dataGridViewLinkColumn5";
            this.dataGridViewLinkColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewLinkColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewLinkColumn5.Width = 70;
            // 
            // dataGridViewLinkColumn6
            // 
            this.dataGridViewLinkColumn6.DataPropertyName = "Title";
            this.dataGridViewLinkColumn6.HeaderText = "Posted";
            this.dataGridViewLinkColumn6.Name = "dataGridViewLinkColumn6";
            this.dataGridViewLinkColumn6.ReadOnly = true;
            this.dataGridViewLinkColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewLinkColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewLinkColumn6.Width = 170;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "CityName";
            this.dataGridViewTextBoxColumn9.HeaderText = "City";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Width = 120;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "CityName";
            this.dataGridViewTextBoxColumn1.HeaderText = "Posted/Renew Cars";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            this.dataGridViewTextBoxColumn1.Width = 250;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "CityId";
            this.dataGridViewTextBoxColumn2.HeaderText = "CityId";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "HtmlCraigslistUrl";
            this.dataGridViewTextBoxColumn3.HeaderText = "HtmlCraigslistUrl";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Visible = false;
            this.dataGridViewTextBoxColumn3.Width = 120;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "CityName";
            this.dataGridViewTextBoxColumn4.HeaderText = "City";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Visible = false;
            this.dataGridViewTextBoxColumn4.Width = 120;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "HtmlCraigslistUrl";
            this.dataGridViewTextBoxColumn5.HeaderText = "HtmlCraigslistUrl";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Visible = false;
            this.dataGridViewTextBoxColumn5.Width = 90;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "CityName";
            this.dataGridViewTextBoxColumn6.HeaderText = "City";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Visible = false;
            this.dataGridViewTextBoxColumn6.Width = 120;
            // 
            // TrackingAdsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.ClientSize = new System.Drawing.Size(1274, 819);
            this.Controls.Add(this.dGridViewOtherCities);
            this.Controls.Add(this.Clbrowser);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dGridViewSameCity);
            this.MaximizeBox = false;
            this.Name = "TrackingAdsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tracking Ads";
            this.Load += new System.EventHandler(this.TrackingAdsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGridViewSameCity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGridViewOtherCities)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dGridViewSameCity;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewLinkColumn dataGridViewLinkColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewLinkColumn dataGridViewLinkColumn2;
        private System.Windows.Forms.DataGridViewLinkColumn dataGridViewLinkColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.WebBrowser Clbrowser;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridView dGridViewOtherCities;
        private System.Windows.Forms.DataGridViewLinkColumn ListingId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClpostingId;
        private System.Windows.Forms.DataGridViewTextBoxColumn CityId;
        private System.Windows.Forms.DataGridViewTextBoxColumn HtmlCraigslistUrl;
        private System.Windows.Forms.DataGridViewLinkColumn StockNumber;
        private System.Windows.Forms.DataGridViewLinkColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn CityName;
        private System.Windows.Forms.DataGridViewLinkColumn ListingIdOtherCities;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClpostingIdOther;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewLinkColumn dataGridViewLinkColumn5;
        private System.Windows.Forms.DataGridViewLinkColumn dataGridViewLinkColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
    }
}