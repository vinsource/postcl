namespace VinCLAPP.Forms
{
    partial class CraigslistCreditCardForm
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
        private static CraigslistCreditCardForm sForm = null;
        public static CraigslistCreditCardForm Instance()
        {
            if (sForm == null) { sForm = new CraigslistCreditCardForm(); }

            else
            {
                sForm.Close();
                sForm = new CraigslistCreditCardForm();
            }

            return sForm;
        }

        public static CraigslistCreditCardForm Instance(MainForm maniform)
        {
            if (sForm == null) { sForm = new CraigslistCreditCardForm(maniform); }

            else
            {
                sForm.Close();
                sForm = new CraigslistCreditCardForm(maniform);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CraigslistCreditCardForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtContactEmailAddress = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPhoneNumber = new System.Windows.Forms.MaskedTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtContactName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panelIndicator = new System.Windows.Forms.FlowLayoutPanel();
            this.imgPicLoad = new System.Windows.Forms.PictureBox();
            this.label15 = new System.Windows.Forms.Label();
            this.pcBoxCreditCardType = new System.Windows.Forms.PictureBox();
            this.grbBilling = new System.Windows.Forms.GroupBox();
            this.txtZipcode = new System.Windows.Forms.MaskedTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbState = new System.Windows.Forms.ComboBox();
            this.txtStreetAddress = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbExpireYear = new System.Windows.Forms.ComboBox();
            this.cbExpireMonths = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCardNumber = new System.Windows.Forms.TextBox();
            this.txtSecurityCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider4 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider5 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider6 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider7 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider8 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider9 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider10 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider11 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider12 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panelIndicator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgPicLoad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBoxCreditCardType)).BeginInit();
            this.grbBilling.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider12)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Maroon;
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.txtLastName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.panelIndicator);
            this.groupBox1.Controls.Add(this.pcBoxCreditCardType);
            this.groupBox1.Controls.Add(this.grbBilling);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.txtFirstName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbExpireYear);
            this.groupBox1.Controls.Add(this.cbExpireMonths);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCardNumber);
            this.groupBox1.Controls.Add(this.txtSecurityCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(5, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(779, 529);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Please enter your credit card information";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 480);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(737, 32);
            this.label6.TabIndex = 3;
            this.label6.Text = resources.GetString("label6.Text");
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtContactEmailAddress);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtPhoneNumber);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtContactName);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox3.Location = new System.Drawing.Point(16, 325);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(447, 125);
            this.groupBox3.TabIndex = 49;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Who should we contact if we have questions about your posting?";
            // 
            // txtContactEmailAddress
            // 
            this.txtContactEmailAddress.Location = new System.Drawing.Point(182, 91);
            this.txtContactEmailAddress.Name = "txtContactEmailAddress";
            this.txtContactEmailAddress.Size = new System.Drawing.Size(175, 22);
            this.txtContactEmailAddress.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(163, 16);
            this.label8.TabIndex = 41;
            this.label8.Text = "Contact Email Address  (*)";
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(197, 49);
            this.txtPhoneNumber.Mask = "(999) 000-0000";
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(100, 22);
            this.txtPhoneNumber.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 16);
            this.label7.TabIndex = 36;
            this.label7.Text = "Contact Name  (*)";
            // 
            // txtContactName
            // 
            this.txtContactName.Location = new System.Drawing.Point(173, 16);
            this.txtContactName.Name = "txtContactName";
            this.txtContactName.Size = new System.Drawing.Size(175, 22);
            this.txtContactName.TabIndex = 11;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 55);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(165, 16);
            this.label14.TabIndex = 39;
            this.label14.Text = "Contact Phone Number  (*)";
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(159, 194);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(195, 22);
            this.txtLastName.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 194);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 16);
            this.label5.TabIndex = 47;
            this.label5.Text = "Last Name  (*)";
            // 
            // panelIndicator
            // 
            this.panelIndicator.BackColor = System.Drawing.Color.MidnightBlue;
            this.panelIndicator.Controls.Add(this.imgPicLoad);
            this.panelIndicator.Controls.Add(this.label15);
            this.panelIndicator.Location = new System.Drawing.Point(469, 416);
            this.panelIndicator.Name = "panelIndicator";
            this.panelIndicator.Size = new System.Drawing.Size(225, 55);
            this.panelIndicator.TabIndex = 38;
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
            // pcBoxCreditCardType
            // 
            this.pcBoxCreditCardType.BackColor = System.Drawing.Color.Transparent;
            this.pcBoxCreditCardType.Image = global::VinCLAPP.Properties.Resources.visa_48;
            this.pcBoxCreditCardType.Location = new System.Drawing.Point(392, 58);
            this.pcBoxCreditCardType.Name = "pcBoxCreditCardType";
            this.pcBoxCreditCardType.Size = new System.Drawing.Size(49, 45);
            this.pcBoxCreditCardType.TabIndex = 46;
            this.pcBoxCreditCardType.TabStop = false;
            this.pcBoxCreditCardType.Visible = false;
            // 
            // grbBilling
            // 
            this.grbBilling.Controls.Add(this.txtZipcode);
            this.grbBilling.Controls.Add(this.label9);
            this.grbBilling.Controls.Add(this.cbState);
            this.grbBilling.Controls.Add(this.txtStreetAddress);
            this.grbBilling.Controls.Add(this.label12);
            this.grbBilling.Controls.Add(this.label10);
            this.grbBilling.Controls.Add(this.txtCity);
            this.grbBilling.Controls.Add(this.label11);
            this.grbBilling.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.grbBilling.Location = new System.Drawing.Point(16, 222);
            this.grbBilling.Name = "grbBilling";
            this.grbBilling.Size = new System.Drawing.Size(477, 93);
            this.grbBilling.TabIndex = 44;
            this.grbBilling.TabStop = false;
            this.grbBilling.Text = "Billing Address";
            // 
            // txtZipcode
            // 
            this.txtZipcode.Location = new System.Drawing.Point(91, 52);
            this.txtZipcode.Mask = "00000";
            this.txtZipcode.Name = "txtZipcode";
            this.txtZipcode.Size = new System.Drawing.Size(64, 22);
            this.txtZipcode.TabIndex = 8;
            this.txtZipcode.Leave += new System.EventHandler(this.txtZipcode_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 16);
            this.label9.TabIndex = 36;
            this.label9.Text = "Street Address  (*)";
            // 
            // cbState
            // 
            this.cbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbState.FormattingEnabled = true;
            this.cbState.Location = new System.Drawing.Point(343, 50);
            this.cbState.Name = "cbState";
            this.cbState.Size = new System.Drawing.Size(118, 24);
            this.cbState.TabIndex = 10;
            // 
            // txtStreetAddress
            // 
            this.txtStreetAddress.Location = new System.Drawing.Point(122, 16);
            this.txtStreetAddress.Name = "txtStreetAddress";
            this.txtStreetAddress.Size = new System.Drawing.Size(319, 22);
            this.txtStreetAddress.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(298, 55);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(39, 16);
            this.label12.TabIndex = 42;
            this.label12.Text = "State";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(161, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 16);
            this.label10.TabIndex = 38;
            this.label10.Text = "City";
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(202, 50);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(90, 22);
            this.txtCity.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 55);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 16);
            this.label11.TabIndex = 39;
            this.label11.Text = "ZipCode  (*)";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(11, 21);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(124, 20);
            this.radioButton1.TabIndex = 50;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Accept Payment";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::VinCLAPP.Properties.Resources.visamasteramericanexpress;
            this.pictureBox1.Location = new System.Drawing.Point(158, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(195, 37);
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(158, 163);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(195, 22);
            this.txtFirstName.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 16);
            this.label4.TabIndex = 17;
            this.label4.Text = "Security Code  (*)";
            // 
            // cbExpireYear
            // 
            this.cbExpireYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExpireYear.FormattingEnabled = true;
            this.cbExpireYear.Location = new System.Drawing.Point(284, 129);
            this.cbExpireYear.Name = "cbExpireYear";
            this.cbExpireYear.Size = new System.Drawing.Size(102, 24);
            this.cbExpireYear.TabIndex = 4;
            // 
            // cbExpireMonths
            // 
            this.cbExpireMonths.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExpireMonths.FormattingEnabled = true;
            this.cbExpireMonths.Location = new System.Drawing.Point(159, 129);
            this.cbExpireMonths.Name = "cbExpireMonths";
            this.cbExpireMonths.Size = new System.Drawing.Size(119, 24);
            this.cbExpireMonths.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "Expires  (*)";
            // 
            // btnOK
            // 
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOK.Location = new System.Drawing.Point(349, 456);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(114, 21);
            this.btnOK.TabIndex = 14;
            this.btnOK.Text = "Process";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Card Number (*)";
            // 
            // txtCardNumber
            // 
            this.txtCardNumber.Location = new System.Drawing.Point(159, 70);
            this.txtCardNumber.Name = "txtCardNumber";
            this.txtCardNumber.Size = new System.Drawing.Size(227, 22);
            this.txtCardNumber.TabIndex = 1;
            this.txtCardNumber.Leave += new System.EventHandler(this.txtCardNumber_Leave);
            // 
            // txtSecurityCode
            // 
            this.txtSecurityCode.Location = new System.Drawing.Point(159, 100);
            this.txtSecurityCode.Name = "txtSecurityCode";
            this.txtSecurityCode.Size = new System.Drawing.Size(119, 22);
            this.txtSecurityCode.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 163);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "First Name  (*)";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Menu;
            this.groupBox2.Controls.Add(this.richTextBox1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(5, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(779, 83);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Info;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(3, 20);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(773, 60);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "Required fields are in green.\nThe address entered in this form must EXACTLY match" +
    " the billing address on your monthly credit card statement.";
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
            // errorProvider6
            // 
            this.errorProvider6.ContainerControl = this;
            // 
            // errorProvider7
            // 
            this.errorProvider7.ContainerControl = this;
            // 
            // errorProvider8
            // 
            this.errorProvider8.ContainerControl = this;
            // 
            // errorProvider9
            // 
            this.errorProvider9.ContainerControl = this;
            // 
            // errorProvider10
            // 
            this.errorProvider10.ContainerControl = this;
            // 
            // errorProvider11
            // 
            this.errorProvider11.ContainerControl = this;
            // 
            // errorProvider12
            // 
            this.errorProvider12.ContainerControl = this;
            // 
            // CraigslistCreditCardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 615);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "CraigslistCreditCardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Craigslist Payment Form";
            this.Load += new System.EventHandler(this.CraigslistCreditCardForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panelIndicator.ResumeLayout(false);
            this.panelIndicator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgPicLoad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBoxCreditCardType)).EndInit();
            this.grbBilling.ResumeLayout(false);
            this.grbBilling.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider12)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel panelIndicator;
        private System.Windows.Forms.PictureBox imgPicLoad;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.PictureBox pcBoxCreditCardType;
        private System.Windows.Forms.GroupBox grbBilling;
        private System.Windows.Forms.MaskedTextBox txtZipcode;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbState;
        private System.Windows.Forms.TextBox txtStreetAddress;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbExpireYear;
        private System.Windows.Forms.ComboBox cbExpireMonths;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCardNumber;
        private System.Windows.Forms.TextBox txtSecurityCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtContactName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtContactEmailAddress;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MaskedTextBox txtPhoneNumber;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.ErrorProvider errorProvider4;
        private System.Windows.Forms.ErrorProvider errorProvider5;
        private System.Windows.Forms.ErrorProvider errorProvider6;
        private System.Windows.Forms.ErrorProvider errorProvider7;
        private System.Windows.Forms.ErrorProvider errorProvider8;
        private System.Windows.Forms.ErrorProvider errorProvider9;
        private System.Windows.Forms.ErrorProvider errorProvider10;
        private System.Windows.Forms.ErrorProvider errorProvider11;
        private System.Windows.Forms.ErrorProvider errorProvider12;

    }
}