namespace VinCLAPP.Forms
{
    partial class WebForm
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

        private static WebForm sForm = null;
        public static WebForm Instance(string url)
        {
            if (sForm == null) { sForm = new WebForm(url); }

            else
            {
                sForm.Close();
                sForm = new WebForm(url);
            }

            return sForm;
        }
        public static WebForm Instance()
        {
            if (sForm == null) { sForm = new WebForm(); }

            else
            {
                sForm.Close();
                sForm = new WebForm();
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
            this.dGridViewToday = new System.Windows.Forms.DataGridView();
            this.ClWebBrowser = new System.Windows.Forms.WebBrowser();
            this.ListingId = new System.Windows.Forms.DataGridViewLinkColumn();
            this.StockNumber = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Title = new System.Windows.Forms.DataGridViewLinkColumn();
            this.HtmlCraigslistUrl = new System.Windows.Forms.DataGridViewLinkColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CityName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dGridViewToday)).BeginInit();
            this.SuspendLayout();
            // 
            // dGridViewToday
            // 
            this.dGridViewToday.AllowUserToAddRows = false;
            this.dGridViewToday.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dGridViewToday.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dGridViewToday.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGridViewToday.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ListingId,
            this.StockNumber,
            this.Title,
            this.CityName,
            this.HtmlCraigslistUrl});
            this.dGridViewToday.Location = new System.Drawing.Point(776, 2);
            this.dGridViewToday.Name = "dGridViewToday";
            this.dGridViewToday.Size = new System.Drawing.Size(445, 796);
            this.dGridViewToday.TabIndex = 1;
            this.dGridViewToday.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGridViewToday_CellClick);
            // 
            // ClWebBrowser
            // 
            this.ClWebBrowser.Location = new System.Drawing.Point(3, 2);
            this.ClWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.ClWebBrowser.Name = "ClWebBrowser";
            this.ClWebBrowser.Size = new System.Drawing.Size(767, 804);
            this.ClWebBrowser.TabIndex = 2;
            this.ClWebBrowser.Url = new System.Uri("http://www.craigslist.org/about/sites", System.UriKind.Absolute);
            // 
            // ListingId
            // 
            this.ListingId.DataPropertyName = "ListingId";
            this.ListingId.HeaderText = "Id";
            this.ListingId.Name = "ListingId";
            this.ListingId.Width = 30;
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
            this.Title.HeaderText = "Posted/Renew Cars";
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            this.Title.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Title.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Title.Width = 200;
            // 
            // HtmlCraigslistUrl
            // 
            this.HtmlCraigslistUrl.DataPropertyName = "HtmlCraigslistUrl";
            this.HtmlCraigslistUrl.HeaderText = "URL";
            this.HtmlCraigslistUrl.Name = "HtmlCraigslistUrl";
            this.HtmlCraigslistUrl.ReadOnly = true;
            this.HtmlCraigslistUrl.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "CityName";
            this.dataGridViewTextBoxColumn1.HeaderText = "Posted/Renew Cars";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 250;
            // 
            // CityName
            // 
            this.CityName.DataPropertyName = "CityName";
            this.CityName.HeaderText = "City";
            this.CityName.Name = "CityName";
            // 
            // WebForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.ClientSize = new System.Drawing.Size(1224, 810);
            this.Controls.Add(this.ClWebBrowser);
            this.Controls.Add(this.dGridViewToday);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WebForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Ads On Web";
            this.Load += new System.EventHandler(this.WebForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGridViewToday)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dGridViewToday;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.WebBrowser ClWebBrowser;
        private System.Windows.Forms.DataGridViewLinkColumn ListingId;
        private System.Windows.Forms.DataGridViewLinkColumn StockNumber;
        private System.Windows.Forms.DataGridViewLinkColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn CityName;
        private System.Windows.Forms.DataGridViewLinkColumn HtmlCraigslistUrl;
    }
}