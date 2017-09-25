namespace CraigslistManagerApp
{
    partial class UploadForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.clBoxCityList = new System.Windows.Forms.CheckedListBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listFinishedCities = new System.Windows.Forms.ListBox();
            this.lblDealerName = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.lblUpload = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // clBoxCityList
            // 
            this.clBoxCityList.FormattingEnabled = true;
            this.clBoxCityList.Location = new System.Drawing.Point(13, 38);
            this.clBoxCityList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clBoxCityList.Name = "clBoxCityList";
            this.clBoxCityList.Size = new System.Drawing.Size(240, 140);
            this.clBoxCityList.TabIndex = 0;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(16, 185);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(90, 23);
            this.btnUpload.TabIndex = 1;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(113, 185);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listFinishedCities);
            this.groupBox1.Location = new System.Drawing.Point(260, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 140);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Finished";
            // 
            // listFinishedCities
            // 
            this.listFinishedCities.FormattingEnabled = true;
            this.listFinishedCities.ItemHeight = 16;
            this.listFinishedCities.Location = new System.Drawing.Point(6, 18);
            this.listFinishedCities.Name = "listFinishedCities";
            this.listFinishedCities.Size = new System.Drawing.Size(212, 116);
            this.listFinishedCities.TabIndex = 0;
            // 
            // lblDealerName
            // 
            this.lblDealerName.AutoSize = true;
            this.lblDealerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDealerName.Location = new System.Drawing.Point(99, 9);
            this.lblDealerName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDealerName.Name = "lblDealerName";
            this.lblDealerName.Size = new System.Drawing.Size(89, 16);
            this.lblDealerName.TabIndex = 9;
            this.lblDealerName.Text = "Dealer Name";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(13, 9);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(63, 16);
            this.lblID.TabIndex = 10;
            this.lblID.Text = "Dealer Id";
            // 
            // lblUpload
            // 
            this.lblUpload.AutoSize = true;
            this.lblUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpload.Location = new System.Drawing.Point(36, 82);
            this.lblUpload.Name = "lblUpload";
            this.lblUpload.Size = new System.Drawing.Size(184, 25);
            this.lblUpload.TabIndex = 11;
            this.lblUpload.Text = "Uploading Images";
            this.lblUpload.Visible = false;
            // 
            // UploadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 210);
            this.Controls.Add(this.lblUpload);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.lblDealerName);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.clBoxCityList);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "UploadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UploadForm";
            this.Load += new System.EventHandler(this.UploadForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private static UploadForm sForm = null;
        public static UploadForm Instance(MAINFORM frmMain)
        {
            if (sForm == null) { sForm = new UploadForm(frmMain); }

            else
            {
                sForm.Close();
                sForm = null;
                sForm = new UploadForm(frmMain);
            }

            return sForm;
        }
        private System.Windows.Forms.CheckedListBox clBoxCityList;
        private System.Windows.Forms.Button btnUpload;

        private MAINFORM frmMain;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listFinishedCities;
        private System.Windows.Forms.Label lblDealerName;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblUpload;
    }
}