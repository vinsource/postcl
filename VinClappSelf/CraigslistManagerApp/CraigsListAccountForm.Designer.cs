namespace CraigslistManagerApp
{
    partial class CraigsListAccountForm
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

        private static CraigsListAccountForm sForm = null;
        public static CraigsListAccountForm Instance(Mainform frmMain)
        {
            if (sForm == null) { sForm = new CraigsListAccountForm(frmMain); }

            else
            {
                sForm.Close();
                sForm = new CraigsListAccountForm(frmMain);
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
            this.txtSource = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.txtAcc1 = new System.Windows.Forms.TextBox();
            this.txtAcc2 = new System.Windows.Forms.TextBox();
            this.txtAcc3 = new System.Windows.Forms.TextBox();
            this.txtAcc4 = new System.Windows.Forms.TextBox();
            this.txtPass1 = new System.Windows.Forms.TextBox();
            this.txtPass2 = new System.Windows.Forms.TextBox();
            this.txtPass3 = new System.Windows.Forms.TextBox();
            this.txtPass4 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAcc5 = new System.Windows.Forms.TextBox();
            this.txtPass5 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAcc6 = new System.Windows.Forms.TextBox();
            this.txtPass6 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pbLoabPic = new System.Windows.Forms.PictureBox();
            this.txtAcc7 = new System.Windows.Forms.TextBox();
            this.txtPass7 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAcct8 = new System.Windows.Forms.TextBox();
            this.txtPass8 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAcct9 = new System.Windows.Forms.TextBox();
            this.txtPass9 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAcct10 = new System.Windows.Forms.TextBox();
            this.txtPass10 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtPhone1 = new System.Windows.Forms.TextBox();
            this.txtPhone2 = new System.Windows.Forms.TextBox();
            this.txtPhone3 = new System.Windows.Forms.TextBox();
            this.txtPhone4 = new System.Windows.Forms.TextBox();
            this.txtPhone5 = new System.Windows.Forms.TextBox();
            this.txtPhone6 = new System.Windows.Forms.TextBox();
            this.txtPhone7 = new System.Windows.Forms.TextBox();
            this.txtPhone8 = new System.Windows.Forms.TextBox();
            this.txtPhone9 = new System.Windows.Forms.TextBox();
            this.txtPhone10 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtProxy10 = new System.Windows.Forms.TextBox();
            this.txtProxy9 = new System.Windows.Forms.TextBox();
            this.txtProxy8 = new System.Windows.Forms.TextBox();
            this.txtProxy7 = new System.Windows.Forms.TextBox();
            this.txtProxy6 = new System.Windows.Forms.TextBox();
            this.txtProxy5 = new System.Windows.Forms.TextBox();
            this.txtProxy4 = new System.Windows.Forms.TextBox();
            this.txtProxy3 = new System.Windows.Forms.TextBox();
            this.txtProxy2 = new System.Windows.Forms.TextBox();
            this.txtProxy1 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoabPic)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSource
            // 
            this.txtSource.Enabled = false;
            this.txtSource.Location = new System.Drawing.Point(6, 19);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(309, 20);
            this.txtSource.TabIndex = 3;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(321, 19);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(98, 22);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.SelectedPath = "C:\\";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSource);
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 53);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Import From File";
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.WorkerSupportsCancellation = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // txtAcc1
            // 
            this.txtAcc1.Location = new System.Drawing.Point(72, 20);
            this.txtAcc1.Name = "txtAcc1";
            this.txtAcc1.Size = new System.Drawing.Size(183, 20);
            this.txtAcc1.TabIndex = 0;
            // 
            // txtAcc2
            // 
            this.txtAcc2.Location = new System.Drawing.Point(72, 45);
            this.txtAcc2.Name = "txtAcc2";
            this.txtAcc2.Size = new System.Drawing.Size(183, 20);
            this.txtAcc2.TabIndex = 3;
            // 
            // txtAcc3
            // 
            this.txtAcc3.Location = new System.Drawing.Point(72, 72);
            this.txtAcc3.Name = "txtAcc3";
            this.txtAcc3.Size = new System.Drawing.Size(183, 20);
            this.txtAcc3.TabIndex = 6;
            // 
            // txtAcc4
            // 
            this.txtAcc4.Location = new System.Drawing.Point(72, 98);
            this.txtAcc4.Name = "txtAcc4";
            this.txtAcc4.Size = new System.Drawing.Size(183, 20);
            this.txtAcc4.TabIndex = 9;
            // 
            // txtPass1
            // 
            this.txtPass1.Location = new System.Drawing.Point(261, 19);
            this.txtPass1.Name = "txtPass1";
            this.txtPass1.Size = new System.Drawing.Size(167, 20);
            this.txtPass1.TabIndex = 1;
            // 
            // txtPass2
            // 
            this.txtPass2.Location = new System.Drawing.Point(261, 45);
            this.txtPass2.Name = "txtPass2";
            this.txtPass2.Size = new System.Drawing.Size(167, 20);
            this.txtPass2.TabIndex = 4;
            // 
            // txtPass3
            // 
            this.txtPass3.Location = new System.Drawing.Point(261, 72);
            this.txtPass3.Name = "txtPass3";
            this.txtPass3.Size = new System.Drawing.Size(167, 20);
            this.txtPass3.TabIndex = 7;
            // 
            // txtPass4
            // 
            this.txtPass4.Location = new System.Drawing.Point(261, 98);
            this.txtPass4.Name = "txtPass4";
            this.txtPass4.Size = new System.Drawing.Size(167, 20);
            this.txtPass4.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Account 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Account 2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Account 3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Account 4";
            // 
            // txtAcc5
            // 
            this.txtAcc5.Location = new System.Drawing.Point(72, 126);
            this.txtAcc5.Name = "txtAcc5";
            this.txtAcc5.Size = new System.Drawing.Size(183, 20);
            this.txtAcc5.TabIndex = 12;
            // 
            // txtPass5
            // 
            this.txtPass5.Location = new System.Drawing.Point(261, 126);
            this.txtPass5.Name = "txtPass5";
            this.txtPass5.Size = new System.Drawing.Size(167, 20);
            this.txtPass5.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Account 5";
            // 
            // txtAcc6
            // 
            this.txtAcc6.Location = new System.Drawing.Point(72, 159);
            this.txtAcc6.Name = "txtAcc6";
            this.txtAcc6.Size = new System.Drawing.Size(183, 20);
            this.txtAcc6.TabIndex = 15;
            // 
            // txtPass6
            // 
            this.txtPass6.Location = new System.Drawing.Point(261, 159);
            this.txtPass6.Name = "txtPass6";
            this.txtPass6.Size = new System.Drawing.Size(167, 20);
            this.txtPass6.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 159);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Account 6";
            // 
            // pbLoabPic
            // 
            this.pbLoabPic.Image = global::CraigslistManagerApp.Properties.Resources.ajax_loader;
            this.pbLoabPic.Location = new System.Drawing.Point(230, 122);
            this.pbLoabPic.Name = "pbLoabPic";
            this.pbLoabPic.Size = new System.Drawing.Size(46, 50);
            this.pbLoabPic.TabIndex = 18;
            this.pbLoabPic.TabStop = false;
            this.pbLoabPic.Visible = false;
            // 
            // txtAcc7
            // 
            this.txtAcc7.Location = new System.Drawing.Point(72, 185);
            this.txtAcc7.Name = "txtAcc7";
            this.txtAcc7.Size = new System.Drawing.Size(183, 20);
            this.txtAcc7.TabIndex = 18;
            // 
            // txtPass7
            // 
            this.txtPass7.Location = new System.Drawing.Point(261, 185);
            this.txtPass7.Name = "txtPass7";
            this.txtPass7.Size = new System.Drawing.Size(167, 20);
            this.txtPass7.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 185);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Account 7";
            // 
            // txtAcct8
            // 
            this.txtAcct8.Location = new System.Drawing.Point(72, 211);
            this.txtAcct8.Name = "txtAcct8";
            this.txtAcct8.Size = new System.Drawing.Size(183, 20);
            this.txtAcct8.TabIndex = 21;
            // 
            // txtPass8
            // 
            this.txtPass8.Location = new System.Drawing.Point(261, 211);
            this.txtPass8.Name = "txtPass8";
            this.txtPass8.Size = new System.Drawing.Size(167, 20);
            this.txtPass8.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 211);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Account 8";
            // 
            // txtAcct9
            // 
            this.txtAcct9.Location = new System.Drawing.Point(72, 237);
            this.txtAcct9.Name = "txtAcct9";
            this.txtAcct9.Size = new System.Drawing.Size(183, 20);
            this.txtAcct9.TabIndex = 24;
            // 
            // txtPass9
            // 
            this.txtPass9.Location = new System.Drawing.Point(261, 237);
            this.txtPass9.Name = "txtPass9";
            this.txtPass9.Size = new System.Drawing.Size(167, 20);
            this.txtPass9.TabIndex = 25;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 237);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Account 9";
            // 
            // txtAcct10
            // 
            this.txtAcct10.Location = new System.Drawing.Point(72, 263);
            this.txtAcct10.Name = "txtAcct10";
            this.txtAcct10.Size = new System.Drawing.Size(183, 20);
            this.txtAcct10.TabIndex = 27;
            // 
            // txtPass10
            // 
            this.txtPass10.Location = new System.Drawing.Point(261, 263);
            this.txtPass10.Name = "txtPass10";
            this.txtPass10.Size = new System.Drawing.Size(167, 20);
            this.txtPass10.TabIndex = 28;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 263);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "Account 10";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(525, 292);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 22);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(629, 292);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(145, 23);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "Save And Close";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtPhone1
            // 
            this.txtPhone1.Location = new System.Drawing.Point(434, 19);
            this.txtPhone1.Name = "txtPhone1";
            this.txtPhone1.Size = new System.Drawing.Size(167, 20);
            this.txtPhone1.TabIndex = 2;
            // 
            // txtPhone2
            // 
            this.txtPhone2.Location = new System.Drawing.Point(434, 45);
            this.txtPhone2.Name = "txtPhone2";
            this.txtPhone2.Size = new System.Drawing.Size(167, 20);
            this.txtPhone2.TabIndex = 5;
            // 
            // txtPhone3
            // 
            this.txtPhone3.Location = new System.Drawing.Point(434, 72);
            this.txtPhone3.Name = "txtPhone3";
            this.txtPhone3.Size = new System.Drawing.Size(167, 20);
            this.txtPhone3.TabIndex = 8;
            // 
            // txtPhone4
            // 
            this.txtPhone4.Location = new System.Drawing.Point(434, 98);
            this.txtPhone4.Name = "txtPhone4";
            this.txtPhone4.Size = new System.Drawing.Size(167, 20);
            this.txtPhone4.TabIndex = 11;
            // 
            // txtPhone5
            // 
            this.txtPhone5.Location = new System.Drawing.Point(434, 126);
            this.txtPhone5.Name = "txtPhone5";
            this.txtPhone5.Size = new System.Drawing.Size(167, 20);
            this.txtPhone5.TabIndex = 14;
            // 
            // txtPhone6
            // 
            this.txtPhone6.Location = new System.Drawing.Point(434, 159);
            this.txtPhone6.Name = "txtPhone6";
            this.txtPhone6.Size = new System.Drawing.Size(167, 20);
            this.txtPhone6.TabIndex = 17;
            // 
            // txtPhone7
            // 
            this.txtPhone7.Location = new System.Drawing.Point(434, 185);
            this.txtPhone7.Name = "txtPhone7";
            this.txtPhone7.Size = new System.Drawing.Size(167, 20);
            this.txtPhone7.TabIndex = 20;
            // 
            // txtPhone8
            // 
            this.txtPhone8.Location = new System.Drawing.Point(434, 211);
            this.txtPhone8.Name = "txtPhone8";
            this.txtPhone8.Size = new System.Drawing.Size(167, 20);
            this.txtPhone8.TabIndex = 23;
            // 
            // txtPhone9
            // 
            this.txtPhone9.Location = new System.Drawing.Point(434, 237);
            this.txtPhone9.Name = "txtPhone9";
            this.txtPhone9.Size = new System.Drawing.Size(167, 20);
            this.txtPhone9.TabIndex = 26;
            // 
            // txtPhone10
            // 
            this.txtPhone10.Location = new System.Drawing.Point(434, 263);
            this.txtPhone10.Name = "txtPhone10";
            this.txtPhone10.Size = new System.Drawing.Size(167, 20);
            this.txtPhone10.TabIndex = 29;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtProxy10);
            this.groupBox2.Controls.Add(this.txtProxy9);
            this.groupBox2.Controls.Add(this.txtProxy8);
            this.groupBox2.Controls.Add(this.txtProxy7);
            this.groupBox2.Controls.Add(this.txtProxy6);
            this.groupBox2.Controls.Add(this.txtProxy5);
            this.groupBox2.Controls.Add(this.txtProxy4);
            this.groupBox2.Controls.Add(this.txtProxy3);
            this.groupBox2.Controls.Add(this.txtProxy2);
            this.groupBox2.Controls.Add(this.txtProxy1);
            this.groupBox2.Controls.Add(this.txtPhone10);
            this.groupBox2.Controls.Add(this.txtPhone9);
            this.groupBox2.Controls.Add(this.txtPhone8);
            this.groupBox2.Controls.Add(this.txtPhone7);
            this.groupBox2.Controls.Add(this.txtPhone6);
            this.groupBox2.Controls.Add(this.txtPhone5);
            this.groupBox2.Controls.Add(this.txtPhone4);
            this.groupBox2.Controls.Add(this.txtPhone3);
            this.groupBox2.Controls.Add(this.txtPhone2);
            this.groupBox2.Controls.Add(this.txtPhone1);
            this.groupBox2.Controls.Add(this.btnOk);
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtPass10);
            this.groupBox2.Controls.Add(this.txtAcct10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtPass9);
            this.groupBox2.Controls.Add(this.txtAcct9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtPass8);
            this.groupBox2.Controls.Add(this.txtAcct8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtPass7);
            this.groupBox2.Controls.Add(this.txtAcc7);
            this.groupBox2.Controls.Add(this.pbLoabPic);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtPass6);
            this.groupBox2.Controls.Add(this.txtAcc6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtPass5);
            this.groupBox2.Controls.Add(this.txtAcc5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtPass4);
            this.groupBox2.Controls.Add(this.txtPass3);
            this.groupBox2.Controls.Add(this.txtPass2);
            this.groupBox2.Controls.Add(this.txtPass1);
            this.groupBox2.Controls.Add(this.txtAcc4);
            this.groupBox2.Controls.Add(this.txtAcc3);
            this.groupBox2.Controls.Add(this.txtAcc2);
            this.groupBox2.Controls.Add(this.txtAcc1);
            this.groupBox2.Location = new System.Drawing.Point(13, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(795, 320);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Enter manually";
            // 
            // txtProxy10
            // 
            this.txtProxy10.Location = new System.Drawing.Point(607, 266);
            this.txtProxy10.Name = "txtProxy10";
            this.txtProxy10.Size = new System.Drawing.Size(167, 20);
            this.txtProxy10.TabIndex = 40;
            // 
            // txtProxy9
            // 
            this.txtProxy9.Location = new System.Drawing.Point(607, 238);
            this.txtProxy9.Name = "txtProxy9";
            this.txtProxy9.Size = new System.Drawing.Size(167, 20);
            this.txtProxy9.TabIndex = 39;
            // 
            // txtProxy8
            // 
            this.txtProxy8.Location = new System.Drawing.Point(607, 212);
            this.txtProxy8.Name = "txtProxy8";
            this.txtProxy8.Size = new System.Drawing.Size(167, 20);
            this.txtProxy8.TabIndex = 38;
            // 
            // txtProxy7
            // 
            this.txtProxy7.Location = new System.Drawing.Point(607, 185);
            this.txtProxy7.Name = "txtProxy7";
            this.txtProxy7.Size = new System.Drawing.Size(167, 20);
            this.txtProxy7.TabIndex = 37;
            // 
            // txtProxy6
            // 
            this.txtProxy6.Location = new System.Drawing.Point(607, 159);
            this.txtProxy6.Name = "txtProxy6";
            this.txtProxy6.Size = new System.Drawing.Size(167, 20);
            this.txtProxy6.TabIndex = 36;
            // 
            // txtProxy5
            // 
            this.txtProxy5.Location = new System.Drawing.Point(607, 127);
            this.txtProxy5.Name = "txtProxy5";
            this.txtProxy5.Size = new System.Drawing.Size(167, 20);
            this.txtProxy5.TabIndex = 35;
            // 
            // txtProxy4
            // 
            this.txtProxy4.Location = new System.Drawing.Point(607, 99);
            this.txtProxy4.Name = "txtProxy4";
            this.txtProxy4.Size = new System.Drawing.Size(167, 20);
            this.txtProxy4.TabIndex = 34;
            // 
            // txtProxy3
            // 
            this.txtProxy3.Location = new System.Drawing.Point(607, 73);
            this.txtProxy3.Name = "txtProxy3";
            this.txtProxy3.Size = new System.Drawing.Size(167, 20);
            this.txtProxy3.TabIndex = 33;
            // 
            // txtProxy2
            // 
            this.txtProxy2.Location = new System.Drawing.Point(607, 46);
            this.txtProxy2.Name = "txtProxy2";
            this.txtProxy2.Size = new System.Drawing.Size(167, 20);
            this.txtProxy2.TabIndex = 32;
            // 
            // txtProxy1
            // 
            this.txtProxy1.Location = new System.Drawing.Point(607, 20);
            this.txtProxy1.Name = "txtProxy1";
            this.txtProxy1.Size = new System.Drawing.Size(167, 20);
            this.txtProxy1.TabIndex = 31;
            // 
            // CraigsListAccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 395);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "CraigsListAccountForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CraigsListAccountForm";
            
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoabPic)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Mainform frmMain;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.TextBox txtAcc1;
        private System.Windows.Forms.TextBox txtAcc2;
        private System.Windows.Forms.TextBox txtAcc3;
        private System.Windows.Forms.TextBox txtAcc4;
        private System.Windows.Forms.TextBox txtPass1;
        private System.Windows.Forms.TextBox txtPass2;
        private System.Windows.Forms.TextBox txtPass3;
        private System.Windows.Forms.TextBox txtPass4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAcc5;
        private System.Windows.Forms.TextBox txtPass5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAcc6;
        private System.Windows.Forms.TextBox txtPass6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pbLoabPic;
        private System.Windows.Forms.TextBox txtAcc7;
        private System.Windows.Forms.TextBox txtPass7;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtAcct8;
        private System.Windows.Forms.TextBox txtPass8;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAcct9;
        private System.Windows.Forms.TextBox txtPass9;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtAcct10;
        private System.Windows.Forms.TextBox txtPass10;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtPhone1;
        private System.Windows.Forms.TextBox txtPhone2;
        private System.Windows.Forms.TextBox txtPhone3;
        private System.Windows.Forms.TextBox txtPhone4;
        private System.Windows.Forms.TextBox txtPhone5;
        private System.Windows.Forms.TextBox txtPhone6;
        private System.Windows.Forms.TextBox txtPhone7;
        private System.Windows.Forms.TextBox txtPhone8;
        private System.Windows.Forms.TextBox txtPhone9;
        private System.Windows.Forms.TextBox txtPhone10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtProxy10;
        private System.Windows.Forms.TextBox txtProxy9;
        private System.Windows.Forms.TextBox txtProxy8;
        private System.Windows.Forms.TextBox txtProxy7;
        private System.Windows.Forms.TextBox txtProxy6;
        private System.Windows.Forms.TextBox txtProxy5;
        private System.Windows.Forms.TextBox txtProxy4;
        private System.Windows.Forms.TextBox txtProxy3;
        private System.Windows.Forms.TextBox txtProxy2;
        private System.Windows.Forms.TextBox txtProxy1;

    }
}