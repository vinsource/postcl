namespace CraigslistManagerApp
{
    partial class Mainform
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
            this.btnLoadPc = new System.Windows.Forms.Button();
            this.cbComputer = new System.Windows.Forms.ComboBox();
            this.lvInventory = new System.Windows.Forms.ListView();
            this.chNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cbCity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDealer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStock = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chYear = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPostingId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMake = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chModel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSalePrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chListingId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chVin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPictures = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAddtionalTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblCount = new System.Windows.Forms.Label();
            this.lblLastPost = new System.Windows.Forms.Label();
            this.btnPostAcct = new System.Windows.Forms.Button();
            this.timerPostAccount = new System.Windows.Forms.Timer(this.components);
            this.timerPause = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.btnPause = new System.Windows.Forms.Button();
            this.lblVersion = new System.Windows.Forms.Label();
            this.bgWorkerAccount = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRenew = new System.Windows.Forms.Button();
            this.btnChangeIp = new System.Windows.Forms.Button();
            this.btnAdvanced = new System.Windows.Forms.Button();
            this.btnReloadRenew = new System.Windows.Forms.Button();
            this.btnSkip = new System.Windows.Forms.Button();
            this.btnBrandNewPost = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReload = new System.Windows.Forms.Button();
            this.lblRenewAds = new System.Windows.Forms.Label();
            this.lblPostingAds = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCuPass = new System.Windows.Forms.TextBox();
            this.txtCuEmail = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCuPhone = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.bgFilter = new System.ComponentModel.BackgroundWorker();
            this.bgSkip = new System.ComponentModel.BackgroundWorker();
            this.pbPicLoad = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPicLoad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadPc
            // 
            this.btnLoadPc.Enabled = false;
            this.btnLoadPc.Location = new System.Drawing.Point(160, 21);
            this.btnLoadPc.Name = "btnLoadPc";
            this.btnLoadPc.Size = new System.Drawing.Size(75, 23);
            this.btnLoadPc.TabIndex = 48;
            this.btnLoadPc.Text = "Load PC";
            this.btnLoadPc.UseVisualStyleBackColor = true;
            this.btnLoadPc.Click += new System.EventHandler(this.BtnLoadPcClick);
            // 
            // cbComputer
            // 
            this.cbComputer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbComputer.FormattingEnabled = true;
            this.cbComputer.Location = new System.Drawing.Point(61, 20);
            this.cbComputer.Name = "cbComputer";
            this.cbComputer.Size = new System.Drawing.Size(93, 26);
            this.cbComputer.TabIndex = 46;
            // 
            // lvInventory
            // 
            this.lvInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvInventory.BackColor = System.Drawing.SystemColors.Window;
            this.lvInventory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chNum,
            this.cbCity,
            this.chDealer,
            this.chStock,
            this.chYear,
            this.chPostingId,
            this.chMake,
            this.chModel,
            this.chSalePrice,
            this.chListingId,
            this.chVin,
            this.chPictures,
            this.chAddtionalTitle});
            this.lvInventory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lvInventory.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvInventory.ForeColor = System.Drawing.Color.Blue;
            this.lvInventory.FullRowSelect = true;
            this.lvInventory.GridLines = true;
            this.lvInventory.HideSelection = false;
            this.lvInventory.Location = new System.Drawing.Point(9, 411);
            this.lvInventory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lvInventory.MultiSelect = false;
            this.lvInventory.Name = "lvInventory";
            this.lvInventory.Size = new System.Drawing.Size(830, 552);
            this.lvInventory.TabIndex = 34;
            this.lvInventory.TileSize = new System.Drawing.Size(300, 300);
            this.lvInventory.UseCompatibleStateImageBehavior = false;
            this.lvInventory.View = System.Windows.Forms.View.Details;
            // 
            // chNum
            // 
            this.chNum.Text = "Number";
            this.chNum.Width = 50;
            // 
            // cbCity
            // 
            this.cbCity.Text = "City";
            this.cbCity.Width = 107;
            // 
            // chDealer
            // 
            this.chDealer.Text = "Dealer";
            this.chDealer.Width = 121;
            // 
            // chStock
            // 
            this.chStock.Text = "Stock No";
            this.chStock.Width = 80;
            // 
            // chYear
            // 
            this.chYear.Text = "Title";
            this.chYear.Width = 180;
            // 
            // chPostingId
            // 
            this.chPostingId.Text = "PostingId";
            this.chPostingId.Width = 90;
            // 
            // chMake
            // 
            this.chMake.Text = "Email";
            this.chMake.Width = 140;
            // 
            // chModel
            // 
            this.chModel.Text = "Password";
            this.chModel.Width = 100;
            // 
            // chSalePrice
            // 
            this.chSalePrice.Text = "Sale Price";
            this.chSalePrice.Width = 100;
            // 
            // chListingId
            // 
            this.chListingId.Text = "Listing Id";
            // 
            // chVin
            // 
            this.chVin.Text = "VIN";
            this.chVin.Width = 100;
            // 
            // chPictures
            // 
            this.chPictures.Text = "Pictures";
            // 
            // chAddtionalTitle
            // 
            this.chAddtionalTitle.Text = "Additional Tittle";
            this.chAddtionalTitle.Width = 200;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.ForeColor = System.Drawing.Color.Red;
            this.lblCount.Location = new System.Drawing.Point(9, 351);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(195, 55);
            this.lblCount.TabIndex = 39;
            this.lblCount.Text = "LEFT = ";
            // 
            // lblLastPost
            // 
            this.lblLastPost.AutoSize = true;
            this.lblLastPost.Font = new System.Drawing.Font("Microsoft Sans Serif", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastPost.ForeColor = System.Drawing.Color.Red;
            this.lblLastPost.Location = new System.Drawing.Point(0, 75);
            this.lblLastPost.Name = "lblLastPost";
            this.lblLastPost.Size = new System.Drawing.Size(178, 64);
            this.lblLastPost.TabIndex = 48;
            this.lblLastPost.Text = "label3";
            this.lblLastPost.Visible = false;
            // 
            // btnPostAcct
            // 
            this.btnPostAcct.Enabled = false;
            this.btnPostAcct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPostAcct.Location = new System.Drawing.Point(197, 161);
            this.btnPostAcct.Name = "btnPostAcct";
            this.btnPostAcct.Size = new System.Drawing.Size(86, 23);
            this.btnPostAcct.TabIndex = 50;
            this.btnPostAcct.Text = "Daily Post";
            this.btnPostAcct.UseVisualStyleBackColor = true;
            this.btnPostAcct.Visible = false;
            this.btnPostAcct.Click += new System.EventHandler(this.BtnPostAcctClick);
            // 
            // timerPostAccount
            // 
            this.timerPostAccount.Interval = 280000;
            this.timerPostAccount.Tick += new System.EventHandler(this.TimerPostAccountTick);
            // 
            // timerPause
            // 
            this.timerPause.Interval = 500;
            this.timerPause.Tick += new System.EventHandler(this.TimerPauseTick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label3.Location = new System.Drawing.Point(7, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 24);
            this.label3.TabIndex = 47;
            this.label3.Text = "PC";
            // 
            // btnPause
            // 
            this.btnPause.Enabled = false;
            this.btnPause.Location = new System.Drawing.Point(197, 134);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 38;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.BtnPauseClick);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.Red;
            this.lblVersion.Location = new System.Drawing.Point(451, 15);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(80, 20);
            this.lblVersion.TabIndex = 55;
            this.lblVersion.Text = "Version = ";
            // 
            // bgWorkerAccount
            // 
            this.bgWorkerAccount.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgWorkerAccountDoWork);
            this.bgWorkerAccount.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgWorkerAccountRunWorkerCompleted);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRenew);
            this.groupBox1.Controls.Add(this.btnChangeIp);
            this.groupBox1.Controls.Add(this.btnAdvanced);
            this.groupBox1.Controls.Add(this.btnReloadRenew);
            this.groupBox1.Controls.Add(this.btnSkip);
            this.groupBox1.Controls.Add(this.btnBrandNewPost);
            this.groupBox1.Controls.Add(this.btnFilter);
            this.groupBox1.Controls.Add(this.cbFilter);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnReload);
            this.groupBox1.Controls.Add(this.lblRenewAds);
            this.groupBox1.Controls.Add(this.lblPostingAds);
            this.groupBox1.Controls.Add(this.btnPostAcct);
            this.groupBox1.Controls.Add(this.cbComputer);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnLoadPc);
            this.groupBox1.Controls.Add(this.lblLastPost);
            this.groupBox1.Controls.Add(this.btnPause);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(9, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(431, 190);
            this.groupBox1.TabIndex = 57;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Info";
            // 
            // btnRenew
            // 
            this.btnRenew.Location = new System.Drawing.Point(289, 132);
            this.btnRenew.Name = "btnRenew";
            this.btnRenew.Size = new System.Drawing.Size(75, 23);
            this.btnRenew.TabIndex = 62;
            this.btnRenew.Text = "Renew";
            this.btnRenew.UseVisualStyleBackColor = true;
            this.btnRenew.Click += new System.EventHandler(this.btnRenew_Click);
            // 
            // btnChangeIp
            // 
            this.btnChangeIp.Location = new System.Drawing.Point(197, 105);
            this.btnChangeIp.Name = "btnChangeIp";
            this.btnChangeIp.Size = new System.Drawing.Size(92, 23);
            this.btnChangeIp.TabIndex = 61;
            this.btnChangeIp.Text = "Change Ip";
            this.btnChangeIp.UseVisualStyleBackColor = true;
            this.btnChangeIp.Visible = false;
            this.btnChangeIp.Click += new System.EventHandler(this.btnChangeIp_Click);
            // 
            // btnAdvanced
            // 
            this.btnAdvanced.Location = new System.Drawing.Point(350, 52);
            this.btnAdvanced.Name = "btnAdvanced";
            this.btnAdvanced.Size = new System.Drawing.Size(81, 23);
            this.btnAdvanced.TabIndex = 60;
            this.btnAdvanced.Text = "Advanced";
            this.btnAdvanced.UseVisualStyleBackColor = true;
            this.btnAdvanced.Click += new System.EventHandler(this.btnAdvanced_Click);
            // 
            // btnReloadRenew
            // 
            this.btnReloadRenew.Location = new System.Drawing.Point(322, 20);
            this.btnReloadRenew.Name = "btnReloadRenew";
            this.btnReloadRenew.Size = new System.Drawing.Size(109, 23);
            this.btnReloadRenew.TabIndex = 59;
            this.btnReloadRenew.Text = "Reload Renew";
            this.btnReloadRenew.UseVisualStyleBackColor = true;
            this.btnReloadRenew.Visible = false;
            this.btnReloadRenew.Click += new System.EventHandler(this.btnReloadRenew_Click);
            // 
            // btnSkip
            // 
            this.btnSkip.Location = new System.Drawing.Point(295, 81);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(75, 23);
            this.btnSkip.TabIndex = 58;
            this.btnSkip.Text = "Skip";
            this.btnSkip.UseVisualStyleBackColor = true;
            this.btnSkip.Click += new System.EventHandler(this.BtnSkipClick);
            // 
            // btnBrandNewPost
            // 
            this.btnBrandNewPost.Enabled = false;
            this.btnBrandNewPost.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrandNewPost.Location = new System.Drawing.Point(289, 161);
            this.btnBrandNewPost.Name = "btnBrandNewPost";
            this.btnBrandNewPost.Size = new System.Drawing.Size(92, 23);
            this.btnBrandNewPost.TabIndex = 57;
            this.btnBrandNewPost.Text = "New Post";
            this.btnBrandNewPost.UseVisualStyleBackColor = true;
            this.btnBrandNewPost.Click += new System.EventHandler(this.BtnBrandNewPostClick);
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(295, 52);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(51, 23);
            this.btnFilter.TabIndex = 56;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.BtnFilterClick);
            // 
            // cbFilter
            // 
            this.cbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilter.FormattingEnabled = true;
            this.cbFilter.Location = new System.Drawing.Point(121, 52);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(168, 26);
            this.cbFilter.TabIndex = 54;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Firebrick;
            this.label1.Location = new System.Drawing.Point(7, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 18);
            this.label1.TabIndex = 55;
            this.label1.Text = "Filter By Dealer";
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(241, 20);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(75, 23);
            this.btnReload.TabIndex = 53;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.BtnReloadClick);
            // 
            // lblRenewAds
            // 
            this.lblRenewAds.AutoSize = true;
            this.lblRenewAds.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRenewAds.ForeColor = System.Drawing.Color.Blue;
            this.lblRenewAds.Location = new System.Drawing.Point(7, 169);
            this.lblRenewAds.Name = "lblRenewAds";
            this.lblRenewAds.Size = new System.Drawing.Size(147, 18);
            this.lblRenewAds.TabIndex = 52;
            this.lblRenewAds.Text = "Renewing Ads = 0 / 0";
            // 
            // lblPostingAds
            // 
            this.lblPostingAds.AutoSize = true;
            this.lblPostingAds.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPostingAds.ForeColor = System.Drawing.Color.Blue;
            this.lblPostingAds.Location = new System.Drawing.Point(8, 139);
            this.lblPostingAds.Name = "lblPostingAds";
            this.lblPostingAds.Size = new System.Drawing.Size(132, 18);
            this.lblPostingAds.TabIndex = 51;
            this.lblPostingAds.Text = "Posting Ads = 0 / 0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(9, 213);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(289, 144);
            this.groupBox2.TabIndex = 58;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Current Email Account";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.50847F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.49152F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtCuPass, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtCuEmail, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtCuPhone, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtIP, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.22222F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.77778F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(283, 123);
            this.tableLayoutPanel1.TabIndex = 55;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(3, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 20);
            this.label2.TabIndex = 55;
            this.label2.Text = "IP = ";
            // 
            // txtCuPass
            // 
            this.txtCuPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCuPass.Location = new System.Drawing.Point(89, 32);
            this.txtCuPass.Name = "txtCuPass";
            this.txtCuPass.ReadOnly = true;
            this.txtCuPass.Size = new System.Drawing.Size(191, 21);
            this.txtCuPass.TabIndex = 1;
            // 
            // txtCuEmail
            // 
            this.txtCuEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCuEmail.Location = new System.Drawing.Point(89, 3);
            this.txtCuEmail.Name = "txtCuEmail";
            this.txtCuEmail.ReadOnly = true;
            this.txtCuEmail.Size = new System.Drawing.Size(191, 21);
            this.txtCuEmail.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 20);
            this.label5.TabIndex = 51;
            this.label5.Text = "Email = ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(3, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 33);
            this.label7.TabIndex = 52;
            this.label7.Text = "Password = ";
            // 
            // txtCuPhone
            // 
            this.txtCuPhone.Location = new System.Drawing.Point(89, 65);
            this.txtCuPhone.Name = "txtCuPhone";
            this.txtCuPhone.ReadOnly = true;
            this.txtCuPhone.Size = new System.Drawing.Size(191, 22);
            this.txtCuPhone.TabIndex = 54;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(3, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 20);
            this.label8.TabIndex = 53;
            this.label8.Text = "Phone = ";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(89, 91);
            this.txtIP.Name = "txtIP";
            this.txtIP.ReadOnly = true;
            this.txtIP.Size = new System.Drawing.Size(191, 22);
            this.txtIP.TabIndex = 56;
            // 
            // bgFilter
            // 
            this.bgFilter.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgFilterDoWork);
            this.bgFilter.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgFilterRunWorkerCompleted);
            // 
            // bgSkip
            // 
            this.bgSkip.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgSkipDoWork);
            this.bgSkip.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgSkipRunWorkerCompleted);
            // 
            // pbPicLoad
            // 
            this.pbPicLoad.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pbPicLoad.Image = global::CraigslistManagerApp.Properties.Resources.ajax_loader_mainform;
            this.pbPicLoad.Location = new System.Drawing.Point(86, 433);
            this.pbPicLoad.Name = "pbPicLoad";
            this.pbPicLoad.Size = new System.Drawing.Size(101, 99);
            this.pbPicLoad.TabIndex = 46;
            this.pbPicLoad.TabStop = false;
            this.pbPicLoad.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CraigslistManagerApp.Properties.Resources.vinclapplogo;
            this.pictureBox1.Location = new System.Drawing.Point(446, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(404, 181);
            this.pictureBox1.TabIndex = 56;
            this.pictureBox1.TabStop = false;
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 964);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pbPicLoad);
            this.Controls.Add(this.lvInventory);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Mainform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Vin Clapp Full Service";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainformFormClosing);
            this.Load += new System.EventHandler(this.Form1Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPicLoad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListView lvInventory;
        private System.Windows.Forms.ColumnHeader chNum;
        private System.Windows.Forms.ColumnHeader chStock;
        private System.Windows.Forms.ColumnHeader chYear;
        private System.Windows.Forms.ColumnHeader chMake;
        private System.Windows.Forms.ColumnHeader chModel;
        private System.Windows.Forms.ColumnHeader chSalePrice;
        private System.Windows.Forms.ColumnHeader chVin;
        public System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.PictureBox pbPicLoad;
        private System.Windows.Forms.Label lblLastPost;
        private System.Windows.Forms.ComboBox cbComputer;
        private System.Windows.Forms.Button btnLoadPc;
        private System.Windows.Forms.Button btnPostAcct;
        private System.Windows.Forms.ColumnHeader chDealer;
        private System.Windows.Forms.Timer timerPostAccount;
        private System.Windows.Forms.Timer timerPause;
        private System.Windows.Forms.ColumnHeader cbCity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Label lblVersion;
        private System.ComponentModel.BackgroundWorker bgWorkerAccount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ColumnHeader chPostingId;
        private System.Windows.Forms.ColumnHeader chListingId;
        private System.Windows.Forms.ColumnHeader chPictures;
        public System.Windows.Forms.Label lblRenewAds;
        public System.Windows.Forms.Label lblPostingAds;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtCuPass;
        private System.Windows.Forms.TextBox txtCuEmail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCuPhone;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.ComboBox cbFilter;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker bgFilter;
        private System.Windows.Forms.Button btnBrandNewPost;
        private System.Windows.Forms.Button btnSkip;
        private System.ComponentModel.BackgroundWorker bgSkip;
        private System.Windows.Forms.ColumnHeader chAddtionalTitle;
        private System.Windows.Forms.Button btnReloadRenew;
        private System.Windows.Forms.Button btnAdvanced;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button btnChangeIp;
        private System.Windows.Forms.Button btnRenew;
    }
}

