namespace VinCLAPP
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnPause = new System.Windows.Forms.Button();
            this.timerPost10 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbRemember = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.cbPrice = new System.Windows.Forms.CheckBox();
            this.lblDealerAddress = new System.Windows.Forms.Label();
            this.txtDCity = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.dGridInventory = new System.Windows.Forms.DataGridView();
            this.IsSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ListingID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PostingCityId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AutoID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PostingCity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalAds = new System.Windows.Forms.DataGridViewLinkColumn();
            this.LastPosted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Make = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Trim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pictures = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mileage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UploadImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.ViewImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.Edit = new System.Windows.Forms.DataGridViewImageColumn();
            this.AdsLink = new System.Windows.Forms.DataGridViewLinkColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.linkEditEnding = new System.Windows.Forms.LinkLabel();
            this.txtEndingSentence = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.lblCity = new System.Windows.Forms.Label();
            this.lblTotalPost = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnPost = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labelImageTip = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pbPicLoad = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editBillingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.craigslistBillingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.craigslistFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.craigslistAPIAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EmailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.postingCitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.datafeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtError = new System.Windows.Forms.TextBox();
            this.lblError = new System.Windows.Forms.Label();
            this.lblProcessing = new System.Windows.Forms.Label();
            this.ckAllSelect = new System.Windows.Forms.CheckBox();
            this.progressPostingBar = new System.Windows.Forms.ProgressBar();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblDealerName = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbCarToCity = new System.Windows.Forms.RadioButton();
            this.rbCityToCar = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.timerPause = new System.Windows.Forms.Timer(this.components);
            this.btnDelete = new System.Windows.Forms.Button();
            this.linkSignup = new System.Windows.Forms.LinkLabel();
            this.timerCheckSingleLogon = new System.Windows.Forms.Timer(this.components);
            this.btnAddInventory = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblSupposedPosts = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblEstimatedTimeLeft = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnView = new System.Windows.Forms.Button();
            this.bgWorkerChangeCity = new System.ComponentModel.BackgroundWorker();
            this.lnlfogotPass = new System.Windows.Forms.LinkLabel();
            this.TimerForceClose = new System.Windows.Forms.Timer(this.components);
            this.btnPostAPI = new System.Windows.Forms.Button();
            this.bgAPICall = new System.ComponentModel.BackgroundWorker();
            this.btnDailyReport = new System.Windows.Forms.Button();
            this.btnSupport = new System.Windows.Forms.Button();
            this.bgNewVersion = new System.ComponentModel.BackgroundWorker();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGridInventory)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPicLoad)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.Red;
            this.lblVersion.Location = new System.Drawing.Point(130, 269);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(0, 23);
            this.lblVersion.TabIndex = 70;
            // 
            // btnPause
            // 
            this.btnPause.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPause.Enabled = false;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPause.ForeColor = System.Drawing.Color.Firebrick;
            this.btnPause.Location = new System.Drawing.Point(1001, 268);
            this.btnPause.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(71, 30);
            this.btnPause.TabIndex = 65;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // timerPost10
            // 
            this.timerPost10.Interval = 200000;
            this.timerPost10.Tick += new System.EventHandler(this.timerPost10_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.cbRemember);
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Controls.Add(this.btnLogin);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(318, 96);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox1.Size = new System.Drawing.Size(304, 135);
            this.groupBox1.TabIndex = 55;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Login";
            // 
            // cbRemember
            // 
            this.cbRemember.AutoSize = true;
            this.cbRemember.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRemember.Location = new System.Drawing.Point(96, 100);
            this.cbRemember.Margin = new System.Windows.Forms.Padding(4);
            this.cbRemember.Name = "cbRemember";
            this.cbRemember.Size = new System.Drawing.Size(127, 21);
            this.cbRemember.TabIndex = 76;
            this.cbRemember.Text = "Remember Me";
            this.cbRemember.UseVisualStyleBackColor = true;
            this.cbRemember.CheckedChanged += new System.EventHandler(this.cbRemember_CheckedChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.0757F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.9243F));
            this.tableLayoutPanel2.Controls.Add(this.txtPassword, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtUsername, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 24);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.61111F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.38889F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(292, 69);
            this.tableLayoutPanel2.TabIndex = 75;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(94, 37);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(193, 25);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.Validating += new System.ComponentModel.CancelEventHandler(this.txtPassword_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Username";
            // 
            // txtUsername
            // 
            this.txtUsername.AcceptsTab = true;
            this.txtUsername.Location = new System.Drawing.Point(95, 6);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(192, 25);
            this.txtUsername.TabIndex = 0;
            this.txtUsername.Validating += new System.ComponentModel.CancelEventHandler(this.txtUsername_Validating);
            // 
            // btnLogin
            // 
            this.btnLogin.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLogin.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.DarkRed;
            this.btnLogin.Location = new System.Drawing.Point(8, 95);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(82, 29);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // cbPrice
            // 
            this.cbPrice.AutoSize = true;
            this.cbPrice.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPrice.Location = new System.Drawing.Point(12, 139);
            this.cbPrice.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.cbPrice.Name = "cbPrice";
            this.cbPrice.Size = new System.Drawing.Size(119, 20);
            this.cbPrice.TabIndex = 54;
            this.cbPrice.Text = "No Price         ";
            this.cbPrice.UseVisualStyleBackColor = true;
            // 
            // lblDealerAddress
            // 
            this.lblDealerAddress.AutoSize = true;
            this.lblDealerAddress.BackColor = System.Drawing.Color.Transparent;
            this.lblDealerAddress.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDealerAddress.ForeColor = System.Drawing.Color.White;
            this.lblDealerAddress.Location = new System.Drawing.Point(5, 35);
            this.lblDealerAddress.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblDealerAddress.Name = "lblDealerAddress";
            this.lblDealerAddress.Size = new System.Drawing.Size(87, 18);
            this.lblDealerAddress.TabIndex = 50;
            this.lblDealerAddress.Text = "Dealer Name";
            this.lblDealerAddress.Visible = false;
            // 
            // txtDCity
            // 
            this.txtDCity.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDCity.Location = new System.Drawing.Point(150, 21);
            this.txtDCity.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtDCity.MaxLength = 40;
            this.txtDCity.Name = "txtDCity";
            this.txtDCity.Size = new System.Drawing.Size(282, 26);
            this.txtDCity.TabIndex = 49;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 26);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 18);
            this.label2.TabIndex = 48;
            this.label2.Text = "Dealer City Override";
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.WorkerSupportsCancellation = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgWorkerDoWork);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgWorkerRunWorkerCompleted);
            // 
            // dGridInventory
            // 
            this.dGridInventory.AllowUserToAddRows = false;
            this.dGridInventory.AllowUserToDeleteRows = false;
            this.dGridInventory.AllowUserToResizeColumns = false;
            this.dGridInventory.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.PaleGreen;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dGridInventory.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dGridInventory.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dGridInventory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dGridInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGridInventory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IsSelected,
            this.ListingID,
            this.PostingCityId,
            this.AutoID,
            this.PostingCity,
            this.Stock,
            this.TotalAds,
            this.LastPosted,
            this.Year,
            this.Make,
            this.Model,
            this.Trim,
            this.Vin,
            this.Pictures,
            this.Mileage,
            this.SalePrice,
            this.UploadImage,
            this.ViewImage,
            this.Edit,
            this.AdsLink});
            this.dGridInventory.Location = new System.Drawing.Point(0, 68);
            this.dGridInventory.Margin = new System.Windows.Forms.Padding(4);
            this.dGridInventory.Name = "dGridInventory";
            this.dGridInventory.RowHeadersWidth = 25;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 8.25F);
            this.dGridInventory.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dGridInventory.Size = new System.Drawing.Size(1450, 616);
            this.dGridInventory.TabIndex = 78;
            // 
            // IsSelected
            // 
            this.IsSelected.DataPropertyName = "IsSelected";
            this.IsSelected.HeaderText = "Select";
            this.IsSelected.Name = "IsSelected";
            this.IsSelected.Width = 44;
            // 
            // ListingID
            // 
            this.ListingID.DataPropertyName = "ListingID";
            this.ListingID.HeaderText = "ListingID";
            this.ListingID.Name = "ListingID";
            this.ListingID.Visible = false;
            this.ListingID.Width = 19;
            // 
            // PostingCityId
            // 
            this.PostingCityId.DataPropertyName = "PostingCityId";
            this.PostingCityId.HeaderText = "Posting City Id";
            this.PostingCityId.Name = "PostingCityId";
            this.PostingCityId.Visible = false;
            // 
            // AutoID
            // 
            this.AutoID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.AutoID.DataPropertyName = "AutoID";
            this.AutoID.HeaderText = "Id";
            this.AutoID.Name = "AutoID";
            this.AutoID.Width = 40;
            // 
            // PostingCity
            // 
            this.PostingCity.DataPropertyName = "PostingCity";
            this.PostingCity.HeaderText = "PostingCity";
            this.PostingCity.Name = "PostingCity";
            this.PostingCity.ReadOnly = true;
            this.PostingCity.Width = 110;
            // 
            // Stock
            // 
            this.Stock.DataPropertyName = "Stock";
            this.Stock.HeaderText = "Stock";
            this.Stock.Name = "Stock";
            this.Stock.Width = 61;
            // 
            // TotalAds
            // 
            this.TotalAds.DataPropertyName = "TotalAds";
            this.TotalAds.HeaderText = "Ads";
            this.TotalAds.Name = "TotalAds";
            this.TotalAds.Width = 30;
            // 
            // LastPosted
            // 
            this.LastPosted.DataPropertyName = "LastPosted";
            this.LastPosted.HeaderText = "Last Posted";
            this.LastPosted.Name = "LastPosted";
            // 
            // Year
            // 
            this.Year.DataPropertyName = "Year";
            this.Year.HeaderText = "Year";
            this.Year.Name = "Year";
            this.Year.Width = 57;
            // 
            // Make
            // 
            this.Make.DataPropertyName = "Make";
            this.Make.HeaderText = "Make";
            this.Make.Name = "Make";
            this.Make.Width = 80;
            // 
            // Model
            // 
            this.Model.DataPropertyName = "Model";
            this.Model.HeaderText = "Model";
            this.Model.Name = "Model";
            this.Model.Width = 90;
            // 
            // Trim
            // 
            this.Trim.DataPropertyName = "Trim";
            this.Trim.HeaderText = "Trim";
            this.Trim.Name = "Trim";
            this.Trim.Width = 80;
            // 
            // Vin
            // 
            this.Vin.DataPropertyName = "Vin";
            this.Vin.HeaderText = "Vin";
            this.Vin.Name = "Vin";
            this.Vin.Width = 101;
            // 
            // Pictures
            // 
            this.Pictures.DataPropertyName = "Pictures";
            this.Pictures.HeaderText = "Pics";
            this.Pictures.Name = "Pictures";
            this.Pictures.Width = 40;
            // 
            // Mileage
            // 
            this.Mileage.DataPropertyName = "Mileage";
            this.Mileage.HeaderText = "Mileage";
            this.Mileage.Name = "Mileage";
            this.Mileage.Width = 60;
            // 
            // SalePrice
            // 
            this.SalePrice.DataPropertyName = "SalePrice";
            this.SalePrice.HeaderText = "SalePrice";
            this.SalePrice.Name = "SalePrice";
            this.SalePrice.Width = 78;
            // 
            // UploadImage
            // 
            this.UploadImage.HeaderText = "Up";
            this.UploadImage.Image = global::VinCLAPP.Properties.Resources.upload;
            this.UploadImage.Name = "UploadImage";
            this.UploadImage.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UploadImage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.UploadImage.ToolTipText = "Upload Images";
            this.UploadImage.Width = 25;
            // 
            // ViewImage
            // 
            this.ViewImage.HeaderText = "View";
            this.ViewImage.Image = global::VinCLAPP.Properties.Resources.view;
            this.ViewImage.Name = "ViewImage";
            this.ViewImage.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ViewImage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ViewImage.ToolTipText = "View images";
            this.ViewImage.Width = 30;
            // 
            // Edit
            // 
            this.Edit.HeaderText = "Edit";
            this.Edit.Image = global::VinCLAPP.Properties.Resources.edit;
            this.Edit.Name = "Edit";
            this.Edit.ToolTipText = "Edit Info";
            this.Edit.Width = 30;
            // 
            // AdsLink
            // 
            this.AdsLink.DataPropertyName = "AdsLink";
            this.AdsLink.HeaderText = "View Ad";
            this.AdsLink.Name = "AdsLink";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.linkEditEnding);
            this.groupBox3.Controls.Add(this.txtEndingSentence);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtDCity);
            this.groupBox3.Controls.Add(this.cbPrice);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(630, 97);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(442, 166);
            this.groupBox3.TabIndex = 73;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Setting";
            // 
            // linkEditEnding
            // 
            this.linkEditEnding.AutoSize = true;
            this.linkEditEnding.BackColor = System.Drawing.Color.White;
            this.linkEditEnding.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkEditEnding.Location = new System.Drawing.Point(392, 136);
            this.linkEditEnding.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkEditEnding.Name = "linkEditEnding";
            this.linkEditEnding.Size = new System.Drawing.Size(40, 23);
            this.linkEditEnding.TabIndex = 63;
            this.linkEditEnding.TabStop = true;
            this.linkEditEnding.Text = "Edit";
            this.linkEditEnding.Visible = false;
            this.linkEditEnding.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkEditEnding_LinkClicked);
            // 
            // txtEndingSentence
            // 
            this.txtEndingSentence.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEndingSentence.Location = new System.Drawing.Point(150, 54);
            this.txtEndingSentence.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtEndingSentence.Multiline = true;
            this.txtEndingSentence.Name = "txtEndingSentence";
            this.txtEndingSentence.ReadOnly = true;
            this.txtEndingSentence.Size = new System.Drawing.Size(282, 74);
            this.txtEndingSentence.TabIndex = 62;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(9, 57);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(115, 18);
            this.label13.TabIndex = 61;
            this.label13.Text = "Ending Sentence";
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCity.Location = new System.Drawing.Point(164, 19);
            this.lblCity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(0, 18);
            this.lblCity.TabIndex = 61;
            // 
            // lblTotalPost
            // 
            this.lblTotalPost.AutoSize = true;
            this.lblTotalPost.Location = new System.Drawing.Point(164, 88);
            this.lblTotalPost.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalPost.Name = "lblTotalPost";
            this.lblTotalPost.Size = new System.Drawing.Size(15, 18);
            this.lblTotalPost.TabIndex = 63;
            this.lblTotalPost.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 90);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(126, 18);
            this.label9.TabIndex = 62;
            this.label9.Text = "Total Posts Today =";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 20);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 18);
            this.label3.TabIndex = 60;
            this.label3.Text = "Current City : ";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // btnPost
            // 
            this.btnPost.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPost.Enabled = false;
            this.btnPost.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPost.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPost.ForeColor = System.Drawing.Color.DarkRed;
            this.btnPost.Location = new System.Drawing.Point(1316, 62);
            this.btnPost.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(134, 36);
            this.btnPost.TabIndex = 52;
            this.btnPost.Text = "Post";
            this.btnPost.UseVisualStyleBackColor = true;
            this.btnPost.Click += new System.EventHandler(this.BtnPostClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(401, 60);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 38);
            this.label5.TabIndex = 75;
            this.label5.Text = "Step 1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(706, 60);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 38);
            this.label7.TabIndex = 76;
            this.label7.Text = "Step 2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(1042, 60);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 38);
            this.label8.TabIndex = 77;
            this.label8.Text = "Step 3";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::VinCLAPP.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(15, 46);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(286, 123);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pbPicLoad
            // 
            this.pbPicLoad.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pbPicLoad.Image = global::VinCLAPP.Properties.Resources.ajax_loader_mainform;
            this.pbPicLoad.Location = new System.Drawing.Point(677, 285);
            this.pbPicLoad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbPicLoad.Name = "pbPicLoad";
            this.pbPicLoad.Size = new System.Drawing.Size(101, 113);
            this.pbPicLoad.TabIndex = 71;
            this.pbPicLoad.TabStop = false;
            this.pbPicLoad.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackgroundImage = global::VinCLAPP.Properties.Resources.btnBGblack;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.accountToolStripMenuItem,
            this.craigslistBillingToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1500, 28);
            this.menuStrip1.TabIndex = 79;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(105, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(108, 26);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // accountToolStripMenuItem
            // 
            this.accountToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editBillingToolStripMenuItem,
            this.editToolStripMenuItem});
            this.accountToolStripMenuItem.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            this.accountToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.accountToolStripMenuItem.Text = "Account/Billing";
            // 
            // editBillingToolStripMenuItem
            // 
            this.editBillingToolStripMenuItem.Name = "editBillingToolStripMenuItem";
            this.editBillingToolStripMenuItem.Size = new System.Drawing.Size(138, 26);
            this.editBillingToolStripMenuItem.Text = "Billing";
            this.editBillingToolStripMenuItem.Click += new System.EventHandler(this.editBillingToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(138, 26);
            this.editToolStripMenuItem.Text = "Account";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // craigslistBillingToolStripMenuItem
            // 
            this.craigslistBillingToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.craigslistBillingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.craigslistFToolStripMenuItem,
            this.craigslistAPIAccountToolStripMenuItem});
            this.craigslistBillingToolStripMenuItem.Name = "craigslistBillingToolStripMenuItem";
            this.craigslistBillingToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.craigslistBillingToolStripMenuItem.Text = "Craigslist";
            // 
            // craigslistFToolStripMenuItem
            // 
            this.craigslistFToolStripMenuItem.Name = "craigslistFToolStripMenuItem";
            this.craigslistFToolStripMenuItem.Size = new System.Drawing.Size(249, 26);
            this.craigslistFToolStripMenuItem.Text = "Craigslist Credit Payment";
            this.craigslistFToolStripMenuItem.Click += new System.EventHandler(this.craigslistFToolStripMenuItem_Click);
            // 
            // craigslistAPIAccountToolStripMenuItem
            // 
            this.craigslistAPIAccountToolStripMenuItem.Name = "craigslistAPIAccountToolStripMenuItem";
            this.craigslistAPIAccountToolStripMenuItem.Size = new System.Drawing.Size(249, 26);
            this.craigslistAPIAccountToolStripMenuItem.Text = "Craigslist API Account";
            this.craigslistAPIAccountToolStripMenuItem.Click += new System.EventHandler(this.craigslistAPIAccountToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EmailToolStripMenuItem,
            this.postingCitiesToolStripMenuItem,
            this.datafeedToolStripMenuItem});
            this.toolsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // EmailToolStripMenuItem
            // 
            this.EmailToolStripMenuItem.Name = "EmailToolStripMenuItem";
            this.EmailToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.EmailToolStripMenuItem.Text = "&Email";
            this.EmailToolStripMenuItem.Click += new System.EventHandler(this.EmailToolStripMenuItemClick);
            // 
            // postingCitiesToolStripMenuItem
            // 
            this.postingCitiesToolStripMenuItem.Name = "postingCitiesToolStripMenuItem";
            this.postingCitiesToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.postingCitiesToolStripMenuItem.Text = "Posting Cities";
            this.postingCitiesToolStripMenuItem.Click += new System.EventHandler(this.postingCitiesToolStripMenuItem_Click);
            // 
            // datafeedToolStripMenuItem
            // 
            this.datafeedToolStripMenuItem.Name = "datafeedToolStripMenuItem";
            this.datafeedToolStripMenuItem.Size = new System.Drawing.Size(173, 26);
            this.datafeedToolStripMenuItem.Text = "Datafeed";
            this.datafeedToolStripMenuItem.Click += new System.EventHandler(this.datafeedToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.videoToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // videoToolStripMenuItem
            // 
            this.videoToolStripMenuItem.Name = "videoToolStripMenuItem";
            this.videoToolStripMenuItem.Size = new System.Drawing.Size(134, 26);
            this.videoToolStripMenuItem.Text = "Video";
            this.videoToolStripMenuItem.Click += new System.EventHandler(this.videoToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(134, 26);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::VinCLAPP.Properties.Resources.list_window_bg;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtError);
            this.panel1.Controls.Add(this.lblError);
            this.panel1.Controls.Add(this.lblProcessing);
            this.panel1.Controls.Add(this.pbPicLoad);
            this.panel1.Controls.Add(this.ckAllSelect);
            this.panel1.Controls.Add(this.dGridInventory);
            this.panel1.Controls.Add(this.progressPostingBar);
            this.panel1.Location = new System.Drawing.Point(19, 306);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1450, 672);
            this.panel1.TabIndex = 80;
            // 
            // txtError
            // 
            this.txtError.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtError.Location = new System.Drawing.Point(150, 71);
            this.txtError.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtError.MaxLength = 40;
            this.txtError.Name = "txtError";
            this.txtError.Size = new System.Drawing.Size(1298, 26);
            this.txtError.TabIndex = 94;
            this.txtError.Visible = false;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.BackColor = System.Drawing.Color.Transparent;
            this.lblError.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Maroon;
            this.lblError.Location = new System.Drawing.Point(68, 70);
            this.lblError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(80, 26);
            this.lblError.TabIndex = 93;
            this.lblError.Text = "Error = ";
            this.lblError.Visible = false;
            // 
            // lblProcessing
            // 
            this.lblProcessing.AutoSize = true;
            this.lblProcessing.BackColor = System.Drawing.Color.Transparent;
            this.lblProcessing.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcessing.ForeColor = System.Drawing.Color.Maroon;
            this.lblProcessing.Location = new System.Drawing.Point(31, 34);
            this.lblProcessing.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProcessing.Name = "lblProcessing";
            this.lblProcessing.Size = new System.Drawing.Size(428, 26);
            this.lblProcessing.TabIndex = 90;
            this.lblProcessing.Text = "Posting status of each ad here......                 ";
            // 
            // ckAllSelect
            // 
            this.ckAllSelect.AutoSize = true;
            this.ckAllSelect.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckAllSelect.Location = new System.Drawing.Point(9, 9);
            this.ckAllSelect.Margin = new System.Windows.Forms.Padding(4);
            this.ckAllSelect.Name = "ckAllSelect";
            this.ckAllSelect.Size = new System.Drawing.Size(18, 17);
            this.ckAllSelect.TabIndex = 89;
            this.ckAllSelect.UseVisualStyleBackColor = true;
            this.ckAllSelect.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // progressPostingBar
            // 
            this.progressPostingBar.Location = new System.Drawing.Point(36, 4);
            this.progressPostingBar.Margin = new System.Windows.Forms.Padding(4);
            this.progressPostingBar.Name = "progressPostingBar";
            this.progressPostingBar.Size = new System.Drawing.Size(1414, 29);
            this.progressPostingBar.TabIndex = 88;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.BackColor = System.Drawing.Color.Transparent;
            this.lblPhone.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhone.ForeColor = System.Drawing.Color.White;
            this.lblPhone.Location = new System.Drawing.Point(5, 65);
            this.lblPhone.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(129, 26);
            this.lblPhone.TabIndex = 81;
            this.lblPhone.Text = "Dealer Name";
            this.lblPhone.Visible = false;
            // 
            // lblDealerName
            // 
            this.lblDealerName.AutoSize = true;
            this.lblDealerName.BackColor = System.Drawing.Color.Transparent;
            this.lblDealerName.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDealerName.ForeColor = System.Drawing.Color.White;
            this.lblDealerName.Location = new System.Drawing.Point(5, 0);
            this.lblDealerName.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblDealerName.Name = "lblDealerName";
            this.lblDealerName.Size = new System.Drawing.Size(107, 23);
            this.lblDealerName.TabIndex = 82;
            this.lblDealerName.Text = "Dealer Name";
            this.lblDealerName.Visible = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.DimGray;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.lblDealerName, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblDealerAddress, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblPhone, 0, 2);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(15, 172);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.57143F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.42857F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(286, 114);
            this.tableLayoutPanel3.TabIndex = 82;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.rbCarToCity);
            this.groupBox4.Controls.Add(this.rbCityToCar);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(1079, 98);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(292, 51);
            this.groupBox4.TabIndex = 74;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Group By";
            // 
            // rbCarToCity
            // 
            this.rbCarToCity.AutoSize = true;
            this.rbCarToCity.Location = new System.Drawing.Point(155, 19);
            this.rbCarToCity.Margin = new System.Windows.Forms.Padding(4);
            this.rbCarToCity.Name = "rbCarToCity";
            this.rbCarToCity.Size = new System.Drawing.Size(96, 22);
            this.rbCarToCity.TabIndex = 66;
            this.rbCarToCity.TabStop = true;
            this.rbCarToCity.Text = "Car to City";
            this.rbCarToCity.UseVisualStyleBackColor = true;
            this.rbCarToCity.CheckedChanged += new System.EventHandler(this.rbCarToCity_CheckedChanged);
            // 
            // rbCityToCar
            // 
            this.rbCityToCar.AutoSize = true;
            this.rbCityToCar.Location = new System.Drawing.Point(12, 19);
            this.rbCityToCar.Margin = new System.Windows.Forms.Padding(4);
            this.rbCityToCar.Name = "rbCityToCar";
            this.rbCityToCar.Size = new System.Drawing.Size(96, 22);
            this.rbCityToCar.TabIndex = 65;
            this.rbCityToCar.TabStop = true;
            this.rbCityToCar.Text = "City to Car";
            this.rbCityToCar.UseVisualStyleBackColor = true;
            this.rbCityToCar.CheckedChanged += new System.EventHandler(this.rbCityToCar_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(440, 125);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 18);
            this.label10.TabIndex = 63;
            this.label10.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(119, 121);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(0, 18);
            this.label12.TabIndex = 61;
            // 
            // timerPause
            // 
            this.timerPause.Interval = 500;
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.Location = new System.Drawing.Point(717, 268);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(81, 29);
            this.btnDelete.TabIndex = 83;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // linkSignup
            // 
            this.linkSignup.AutoSize = true;
            this.linkSignup.BackColor = System.Drawing.Color.White;
            this.linkSignup.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkSignup.Location = new System.Drawing.Point(318, 276);
            this.linkSignup.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkSignup.Name = "linkSignup";
            this.linkSignup.Size = new System.Drawing.Size(245, 23);
            this.linkSignup.TabIndex = 90;
            this.linkSignup.TabStop = true;
            this.linkSignup.Text = "Need an account? Sign up here!";
            this.linkSignup.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSignup_LinkClicked);
            // 
            // timerCheckSingleLogon
            // 
            this.timerCheckSingleLogon.Interval = 15000;
            // 
            // btnAddInventory
            // 
            this.btnAddInventory.Enabled = false;
            this.btnAddInventory.ForeColor = System.Drawing.Color.Black;
            this.btnAddInventory.Location = new System.Drawing.Point(630, 268);
            this.btnAddInventory.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddInventory.Name = "btnAddInventory";
            this.btnAddInventory.Size = new System.Drawing.Size(79, 29);
            this.btnAddInventory.TabIndex = 91;
            this.btnAddInventory.Text = "Add Car";
            this.btnAddInventory.UseVisualStyleBackColor = true;
            this.btnAddInventory.Click += new System.EventHandler(this.btnAddInventory_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.lblSupposedPosts);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.lblEstimatedTimeLeft);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.lblCity);
            this.groupBox2.Controls.Add(this.lblTotalPost);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(1079, 152);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(292, 145);
            this.groupBox2.TabIndex = 92;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Posting Info";
            // 
            // lblSupposedPosts
            // 
            this.lblSupposedPosts.AutoSize = true;
            this.lblSupposedPosts.Location = new System.Drawing.Point(164, 41);
            this.lblSupposedPosts.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSupposedPosts.Name = "lblSupposedPosts";
            this.lblSupposedPosts.Size = new System.Drawing.Size(15, 18);
            this.lblSupposedPosts.TabIndex = 67;
            this.lblSupposedPosts.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 41);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(49, 18);
            this.label14.TabIndex = 66;
            this.label14.Text = "Left = ";
            // 
            // lblEstimatedTimeLeft
            // 
            this.lblEstimatedTimeLeft.AutoSize = true;
            this.lblEstimatedTimeLeft.Location = new System.Drawing.Point(164, 68);
            this.lblEstimatedTimeLeft.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEstimatedTimeLeft.Name = "lblEstimatedTimeLeft";
            this.lblEstimatedTimeLeft.Size = new System.Drawing.Size(0, 18);
            this.lblEstimatedTimeLeft.TabIndex = 65;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 68);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(149, 18);
            this.label11.TabIndex = 64;
            this.label11.Text = "Estimated Time Left : ";
            // 
            // btnView
            // 
            this.btnView.Enabled = false;
            this.btnView.ForeColor = System.Drawing.Color.Black;
            this.btnView.Location = new System.Drawing.Point(1374, 270);
            this.btnView.Margin = new System.Windows.Forms.Padding(4);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(96, 29);
            this.btnView.TabIndex = 93;
            this.btnView.Text = "Daily Ads";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // bgWorkerChangeCity
            // 
            this.bgWorkerChangeCity.WorkerReportsProgress = true;
            this.bgWorkerChangeCity.WorkerSupportsCancellation = true;
            this.bgWorkerChangeCity.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerChangeCity_DoWork);
            this.bgWorkerChangeCity.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerChangeCity_RunWorkerCompleted);
            // 
            // lnlfogotPass
            // 
            this.lnlfogotPass.AutoSize = true;
            this.lnlfogotPass.BackColor = System.Drawing.Color.White;
            this.lnlfogotPass.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnlfogotPass.Location = new System.Drawing.Point(318, 251);
            this.lnlfogotPass.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnlfogotPass.Name = "lnlfogotPass";
            this.lnlfogotPass.Size = new System.Drawing.Size(149, 18);
            this.lnlfogotPass.TabIndex = 96;
            this.lnlfogotPass.TabStop = true;
            this.lnlfogotPass.Text = "Forgot your password?";
            this.lnlfogotPass.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnlfogotPass_LinkClicked);
            // 
            // TimerForceClose
            // 
            this.TimerForceClose.Interval = 10000;
            // 
            // btnPostAPI
            // 
            this.btnPostAPI.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPostAPI.Enabled = false;
            this.btnPostAPI.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnPostAPI.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPostAPI.ForeColor = System.Drawing.Color.DarkRed;
            this.btnPostAPI.Location = new System.Drawing.Point(1161, 62);
            this.btnPostAPI.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnPostAPI.Name = "btnPostAPI";
            this.btnPostAPI.Size = new System.Drawing.Size(169, 36);
            this.btnPostAPI.TabIndex = 98;
            this.btnPostAPI.Text = "Post With API";
            this.btnPostAPI.UseVisualStyleBackColor = true;
            this.btnPostAPI.Visible = false;
            this.btnPostAPI.Click += new System.EventHandler(this.btnPostAPI_Click);
            // 
            // bgAPICall
            // 
            this.bgAPICall.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgAPICall_DoWork);
            this.bgAPICall.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgAPICall_RunWorkerCompleted);
            // 
            // btnDailyReport
            // 
            this.btnDailyReport.Location = new System.Drawing.Point(1374, 228);
            this.btnDailyReport.Margin = new System.Windows.Forms.Padding(4);
            this.btnDailyReport.Name = "btnDailyReport";
            this.btnDailyReport.Size = new System.Drawing.Size(96, 34);
            this.btnDailyReport.TabIndex = 99;
            this.btnDailyReport.Text = "Report";
            this.btnDailyReport.UseVisualStyleBackColor = true;
            this.btnDailyReport.Click += new System.EventHandler(this.btnDailyReport_Click);
            // 
            // btnSupport
            // 
            this.btnSupport.BackgroundImage = global::VinCLAPP.Properties.Resources.support_btn;
            this.btnSupport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSupport.Location = new System.Drawing.Point(1374, 157);
            this.btnSupport.Margin = new System.Windows.Forms.Padding(4);
            this.btnSupport.Name = "btnSupport";
            this.btnSupport.Size = new System.Drawing.Size(95, 64);
            this.btnSupport.TabIndex = 100;
            this.btnSupport.UseVisualStyleBackColor = true;
            this.btnSupport.Click += new System.EventHandler(this.btnSupport_Click);
            // 
            // bgNewVersion
            // 
            this.bgNewVersion.WorkerReportsProgress = true;
            this.bgNewVersion.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgNewVersion_DoWork);
            this.bgNewVersion.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgNewVersion_ProgressChanged);
            this.bgNewVersion.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgNewVersion_RunWorkerCompleted);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ListingID";
            this.dataGridViewTextBoxColumn1.HeaderText = "ListingID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            this.dataGridViewTextBoxColumn1.Width = 19;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "AutoID";
            this.dataGridViewTextBoxColumn2.HeaderText = "Id";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Visible = false;
            this.dataGridViewTextBoxColumn2.Width = 50;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "PostingCity";
            this.dataGridViewTextBoxColumn3.HeaderText = "PostingCity";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 110;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Stock";
            this.dataGridViewTextBoxColumn4.HeaderText = "Stock";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 61;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Vin";
            this.dataGridViewTextBoxColumn5.HeaderText = "Vin";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 130;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Year";
            this.dataGridViewTextBoxColumn6.HeaderText = "Year";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 57;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Make";
            this.dataGridViewTextBoxColumn7.HeaderText = "Make";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 80;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Model";
            this.dataGridViewTextBoxColumn8.HeaderText = "Model";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 90;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "Trim";
            this.dataGridViewTextBoxColumn9.HeaderText = "Trim";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Width = 80;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "ExteriorColor";
            this.dataGridViewTextBoxColumn10.HeaderText = "ExteriorColor";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Width = 101;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "Pictures";
            this.dataGridViewTextBoxColumn11.HeaderText = "Pictures";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.Width = 50;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "Mileage";
            this.dataGridViewTextBoxColumn12.HeaderText = "Mileage";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.Width = 60;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.DataPropertyName = "SalePrice";
            this.dataGridViewTextBoxColumn13.HeaderText = "SalePrice";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.Width = 78;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.DataPropertyName = "SalePrice";
            this.dataGridViewTextBoxColumn14.HeaderText = "SalePrice";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.Width = 78;
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Gray;
            this.BackgroundImage = global::VinCLAPP.Properties.Resources.bg_vinclapp1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1500, 994);
            this.Controls.Add(this.btnSupport);
            this.Controls.Add(this.btnDailyReport);
            this.Controls.Add(this.btnPostAPI);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.lnlfogotPass);
            this.Controls.Add(this.btnAddInventory);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.linkSignup);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnPost);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CLDMS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGridInventory)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPicLoad)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbPicLoad;
        private System.Windows.Forms.Label lblVersion;

        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Timer timerPost10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUsername;
        public System.Windows.Forms.CheckBox cbPrice;
        private System.Windows.Forms.Button btnPost;
        private System.Windows.Forms.Label lblDealerAddress;
        private System.Windows.Forms.TextBox txtDCity;
        private System.Windows.Forms.Label label2;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dGridInventory;
        private System.Windows.Forms.Label lblTotalPost;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolTip labelImageTip;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EmailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblDealerName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.ToolStripMenuItem postingCitiesToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbRemember;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Timer timerPause;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ToolStripMenuItem accountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editBillingToolStripMenuItem;
        private System.Windows.Forms.ProgressBar progressPostingBar;
        private System.Windows.Forms.LinkLabel linkSignup;
        private System.Windows.Forms.Timer timerCheckSingleLogon;
        private System.Windows.Forms.Button btnAddInventory;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox ckAllSelect;
        private System.Windows.Forms.Label lblEstimatedTimeLeft;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblSupposedPosts;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.RadioButton rbCarToCity;
        private System.Windows.Forms.RadioButton rbCityToCar;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.ComponentModel.BackgroundWorker bgWorkerChangeCity;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.LinkLabel lnlfogotPass;
        private System.Windows.Forms.Timer TimerForceClose;
        private System.Windows.Forms.ToolStripMenuItem datafeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem craigslistBillingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem craigslistFToolStripMenuItem;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.TextBox txtEndingSentence;
        private System.Windows.Forms.Button btnPostAPI;
        private System.Windows.Forms.ToolStripMenuItem craigslistAPIAccountToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker bgAPICall;
        private System.Windows.Forms.LinkLabel linkEditEnding;
        private System.Windows.Forms.Button btnDailyReport;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn ListingID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PostingCityId;
        private System.Windows.Forms.DataGridViewTextBoxColumn AutoID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PostingCity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stock;
        private System.Windows.Forms.DataGridViewLinkColumn TotalAds;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastPosted;
        private System.Windows.Forms.DataGridViewTextBoxColumn Year;
        private System.Windows.Forms.DataGridViewTextBoxColumn Make;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn Trim;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vin;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pictures;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mileage;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalePrice;
        private System.Windows.Forms.DataGridViewImageColumn UploadImage;
        private System.Windows.Forms.DataGridViewImageColumn ViewImage;
        private System.Windows.Forms.DataGridViewImageColumn Edit;
        private System.Windows.Forms.DataGridViewLinkColumn AdsLink;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.Button btnSupport;
        private System.Windows.Forms.ToolStripMenuItem videoToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker bgNewVersion;
        private System.Windows.Forms.Label lblProcessing;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.TextBox txtError;

    }
}

