namespace VinCLAPP.Forms
{
    partial class DataFeedForm
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
        private static DataFeedForm sForm = null;
        public static DataFeedForm Instance()
        {
            if (sForm == null) { sForm = new DataFeedForm(); }

            else
            {
                sForm.Close();
                sForm = new DataFeedForm();
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbVendorList = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOtherVendor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVendorEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtVendorPhone = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rtbCustomMessage = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtYourName = new System.Windows.Forms.TextBox();
            this.cbAuthorize = new System.Windows.Forms.CheckBox();
            this.btnSubmitRequest = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider4 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider5 = new System.Windows.Forms.ErrorProvider(this.components);
            this.bgDataFeed = new System.ComponentModel.BackgroundWorker();
            this.panelIndicator = new System.Windows.Forms.FlowLayoutPanel();
            this.imgPicLoad = new System.Windows.Forms.PictureBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).BeginInit();
            this.panelIndicator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgPicLoad)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Controls.Add(this.btnSubmitRequest);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(0, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(455, 337);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Please fill in all required fields for data feed ";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.cbVendorList);
            this.flowLayoutPanel1.Controls.Add(this.label5);
            this.flowLayoutPanel1.Controls.Add(this.txtOtherVendor);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.txtVendorEmail);
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.txtVendorPhone);
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Controls.Add(this.rtbCustomMessage);
            this.flowLayoutPanel1.Controls.Add(this.label6);
            this.flowLayoutPanel1.Controls.Add(this.txtYourName);
            this.flowLayoutPanel1.Controls.Add(this.cbAuthorize);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 21);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(422, 275);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data feed Vendor";
            // 
            // cbVendorList
            // 
            this.cbVendorList.FormattingEnabled = true;
            this.cbVendorList.Location = new System.Drawing.Point(123, 3);
            this.cbVendorList.Name = "cbVendorList";
            this.cbVendorList.Size = new System.Drawing.Size(236, 24);
            this.cbVendorList.TabIndex = 0;
            this.cbVendorList.SelectedIndexChanged += new System.EventHandler(this.cbVendorList_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Other vendor";
            // 
            // txtOtherVendor
            // 
            this.txtOtherVendor.Location = new System.Drawing.Point(94, 33);
            this.txtOtherVendor.Name = "txtOtherVendor";
            this.txtOtherVendor.ReadOnly = true;
            this.txtOtherVendor.Size = new System.Drawing.Size(324, 22);
            this.txtOtherVendor.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Contact’s Email at Vendor";
            // 
            // txtVendorEmail
            // 
            this.txtVendorEmail.Location = new System.Drawing.Point(170, 61);
            this.txtVendorEmail.Name = "txtVendorEmail";
            this.txtVendorEmail.Size = new System.Drawing.Size(248, 22);
            this.txtVendorEmail.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Contact’s Phone Number";
            // 
            // txtVendorPhone
            // 
            this.txtVendorPhone.Location = new System.Drawing.Point(165, 89);
            this.txtVendorPhone.Mask = "(999) 000-0000";
            this.txtVendorPhone.Name = "txtVendorPhone";
            this.txtVendorPhone.Size = new System.Drawing.Size(144, 22);
            this.txtVendorPhone.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Custom Message: ";
            // 
            // rtbCustomMessage
            // 
            this.rtbCustomMessage.Location = new System.Drawing.Point(128, 117);
            this.rtbCustomMessage.Name = "rtbCustomMessage";
            this.rtbCustomMessage.Size = new System.Drawing.Size(285, 96);
            this.rtbCustomMessage.TabIndex = 4;
            this.rtbCustomMessage.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 216);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Your Name";
            // 
            // txtYourName
            // 
            this.txtYourName.Location = new System.Drawing.Point(85, 219);
            this.txtYourName.Name = "txtYourName";
            this.txtYourName.Size = new System.Drawing.Size(328, 22);
            this.txtYourName.TabIndex = 5;
            // 
            // cbAuthorize
            // 
            this.cbAuthorize.AutoSize = true;
            this.cbAuthorize.Location = new System.Drawing.Point(3, 247);
            this.cbAuthorize.Name = "cbAuthorize";
            this.cbAuthorize.Size = new System.Drawing.Size(377, 20);
            this.cbAuthorize.TabIndex = 6;
            this.cbAuthorize.Text = "I authorize POSTCL to contact vendor for setting up the feed";
            this.cbAuthorize.UseVisualStyleBackColor = true;
            // 
            // btnSubmitRequest
            // 
            this.btnSubmitRequest.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSubmitRequest.Location = new System.Drawing.Point(314, 302);
            this.btnSubmitRequest.Name = "btnSubmitRequest";
            this.btnSubmitRequest.Size = new System.Drawing.Size(116, 23);
            this.btnSubmitRequest.TabIndex = 7;
            this.btnSubmitRequest.Text = "Submit Request";
            this.btnSubmitRequest.UseVisualStyleBackColor = true;
            this.btnSubmitRequest.Click += new System.EventHandler(this.btnSubmitRequest_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // errorProvider3
            // 
            this.errorProvider3.ContainerControl = this;
            // 
            // errorProvider4
            // 
            this.errorProvider4.ContainerControl = this;
            // 
            // errorProvider5
            // 
            this.errorProvider5.ContainerControl = this;
            // 
            // bgDataFeed
            // 
            this.bgDataFeed.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgDataFeed_DoWork);
            this.bgDataFeed.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgDataFeed_RunWorkerCompleted);
            // 
            // panelIndicator
            // 
            this.panelIndicator.BackColor = System.Drawing.Color.MidnightBlue;
            this.panelIndicator.Controls.Add(this.imgPicLoad);
            this.panelIndicator.Controls.Add(this.label15);
            this.panelIndicator.Location = new System.Drawing.Point(120, 149);
            this.panelIndicator.Name = "panelIndicator";
            this.panelIndicator.Size = new System.Drawing.Size(225, 55);
            this.panelIndicator.TabIndex = 39;
            this.panelIndicator.Visible = false;
            // 
            // imgPicLoad
            // 
            this.imgPicLoad.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.imgPicLoad.Image = global::VinCLAPP.Properties.Resources.CustomerSignUpLoadingIndicator;
            this.imgPicLoad.Location = new System.Drawing.Point(3, 4);
            this.imgPicLoad.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.imgPicLoad.Name = "imgPicLoad";
            this.imgPicLoad.Size = new System.Drawing.Size(218, 21);
            this.imgPicLoad.TabIndex = 75;
            this.imgPicLoad.TabStop = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label15.Location = new System.Drawing.Point(3, 29);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(145, 20);
            this.label15.TabIndex = 38;
            this.label15.Text = "Please wait..............";
            // 
            // DataFeedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.ClientSize = new System.Drawing.Size(457, 353);
            this.Controls.Add(this.panelIndicator);
            this.Controls.Add(this.groupBox1);
            this.Name = "DataFeedForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Feed";
            this.Load += new System.EventHandler(this.DataFeedForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).EndInit();
            this.panelIndicator.ResumeLayout(false);
            this.panelIndicator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgPicLoad)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbVendorList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOtherVendor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtVendorEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox rtbCustomMessage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtYourName;
        private System.Windows.Forms.CheckBox cbAuthorize;
        private System.Windows.Forms.Button btnSubmitRequest;
        private System.Windows.Forms.MaskedTextBox txtVendorPhone;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.ErrorProvider errorProvider4;
        private System.Windows.Forms.ErrorProvider errorProvider5;
        private System.ComponentModel.BackgroundWorker bgDataFeed;
        private System.Windows.Forms.FlowLayoutPanel panelIndicator;
        private System.Windows.Forms.PictureBox imgPicLoad;
        private System.Windows.Forms.Label label15;
    }
}