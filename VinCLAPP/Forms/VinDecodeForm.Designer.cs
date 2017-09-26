namespace VinCLAPP.Forms
{
    partial class VinDecodeForm
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
            this.lblMessage = new System.Windows.Forms.Label();
            this.pnDecode = new System.Windows.Forms.Panel();
            this.rbnCertified = new System.Windows.Forms.CheckBox();
            this.pnLabel = new System.Windows.Forms.Panel();
            this.lblVin = new System.Windows.Forms.Label();
            this.lblStock = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.lblMake = new System.Windows.Forms.Label();
            this.lblModel = new System.Windows.Forms.Label();
            this.lblTrim = new System.Windows.Forms.Label();
            this.lblExteriorColor = new System.Windows.Forms.Label();
            this.lblInteriorColor = new System.Windows.Forms.Label();
            this.lblCylinders = new System.Windows.Forms.Label();
            this.lblOdometer = new System.Windows.Forms.Label();
            this.lblDoors = new System.Windows.Forms.Label();
            this.lblMsrp = new System.Windows.Forms.Label();
            this.lblLiters = new System.Windows.Forms.Label();
            this.lblTransmission = new System.Windows.Forms.Label();
            this.lblStyle = new System.Windows.Forms.Label();
            this.lblFuel = new System.Windows.Forms.Label();
            this.lblDrive = new System.Windows.Forms.Label();
            this.pnVinDecodeType = new System.Windows.Forms.Panel();
            this.rbVindecodeYear = new System.Windows.Forms.RadioButton();
            this.rbDecodeVin = new System.Windows.Forms.RadioButton();
            this.imgPicLoad = new System.Windows.Forms.PictureBox();
            this.lblEmptyError = new System.Windows.Forms.Label();
            this.dtDate = new System.Windows.Forms.DateTimePicker();
            this.cbxModel = new System.Windows.Forms.ComboBox();
            this.cbxMake = new System.Windows.Forms.ComboBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDexcription = new System.Windows.Forms.Label();
            this.txtSalePrice = new System.Windows.Forms.TextBox();
            this.lblSalePrice = new System.Windows.Forms.Label();
            this.btnDecode = new System.Windows.Forms.Button();
            this.btnComplete = new System.Windows.Forms.Button();
            this.cbxDrive = new System.Windows.Forms.ComboBox();
            this.cbxFuel = new System.Windows.Forms.ComboBox();
            this.cbxStyle = new System.Windows.Forms.ComboBox();
            this.cbxTransmission = new System.Windows.Forms.ComboBox();
            this.cbxLiters = new System.Windows.Forms.ComboBox();
            this.cbxCylinders = new System.Windows.Forms.ComboBox();
            this.cbxInteriorColor = new System.Windows.Forms.ComboBox();
            this.cbxExteriorColor = new System.Windows.Forms.ComboBox();
            this.cbxTrim = new System.Windows.Forms.ComboBox();
            this.txtCustomInteriorColor = new System.Windows.Forms.TextBox();
            this.txtCustomExteriorColor = new System.Windows.Forms.TextBox();
            this.lblCustomInteriorColor = new System.Windows.Forms.Label();
            this.txtCustomTrim = new System.Windows.Forms.TextBox();
            this.lblCustomExteriorColor = new System.Windows.Forms.Label();
            this.lblCustomTrim = new System.Windows.Forms.Label();
            this.txtMsrp = new System.Windows.Forms.TextBox();
            this.txtDoors = new System.Windows.Forms.TextBox();
            this.txtOdometer = new System.Windows.Forms.TextBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.txtMake = new System.Windows.Forms.TextBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.txtVin = new System.Windows.Forms.TextBox();
            this.pnDecode.SuspendLayout();
            this.pnLabel.SuspendLayout();
            this.pnVinDecodeType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgPicLoad)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.LavenderBlush;
            this.lblMessage.Location = new System.Drawing.Point(10, 9);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 25);
            this.lblMessage.TabIndex = 57;
            // 
            // pnDecode
            // 
            this.pnDecode.BackColor = System.Drawing.Color.Maroon;
            this.pnDecode.Controls.Add(this.rbnCertified);
            this.pnDecode.Controls.Add(this.pnLabel);
            this.pnDecode.Controls.Add(this.pnVinDecodeType);
            this.pnDecode.Controls.Add(this.imgPicLoad);
            this.pnDecode.Controls.Add(this.lblEmptyError);
            this.pnDecode.Controls.Add(this.dtDate);
            this.pnDecode.Controls.Add(this.cbxModel);
            this.pnDecode.Controls.Add(this.cbxMake);
            this.pnDecode.Controls.Add(this.txtDescription);
            this.pnDecode.Controls.Add(this.lblDexcription);
            this.pnDecode.Controls.Add(this.txtSalePrice);
            this.pnDecode.Controls.Add(this.lblSalePrice);
            this.pnDecode.Controls.Add(this.lblMessage);
            this.pnDecode.Controls.Add(this.btnDecode);
            this.pnDecode.Controls.Add(this.btnComplete);
            this.pnDecode.Controls.Add(this.cbxDrive);
            this.pnDecode.Controls.Add(this.cbxFuel);
            this.pnDecode.Controls.Add(this.cbxStyle);
            this.pnDecode.Controls.Add(this.cbxTransmission);
            this.pnDecode.Controls.Add(this.cbxLiters);
            this.pnDecode.Controls.Add(this.cbxCylinders);
            this.pnDecode.Controls.Add(this.cbxInteriorColor);
            this.pnDecode.Controls.Add(this.cbxExteriorColor);
            this.pnDecode.Controls.Add(this.cbxTrim);
            this.pnDecode.Controls.Add(this.txtCustomInteriorColor);
            this.pnDecode.Controls.Add(this.txtCustomExteriorColor);
            this.pnDecode.Controls.Add(this.lblCustomInteriorColor);
            this.pnDecode.Controls.Add(this.txtCustomTrim);
            this.pnDecode.Controls.Add(this.lblCustomExteriorColor);
            this.pnDecode.Controls.Add(this.lblCustomTrim);
            this.pnDecode.Controls.Add(this.txtMsrp);
            this.pnDecode.Controls.Add(this.txtDoors);
            this.pnDecode.Controls.Add(this.txtOdometer);
            this.pnDecode.Controls.Add(this.txtModel);
            this.pnDecode.Controls.Add(this.txtMake);
            this.pnDecode.Controls.Add(this.txtYear);
            this.pnDecode.Controls.Add(this.txtStock);
            this.pnDecode.Controls.Add(this.txtVin);
            this.pnDecode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnDecode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnDecode.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnDecode.Location = new System.Drawing.Point(0, 0);
            this.pnDecode.Name = "pnDecode";
            this.pnDecode.Size = new System.Drawing.Size(583, 695);
            this.pnDecode.TabIndex = 73;
            // 
            // rbnCertified
            // 
            this.rbnCertified.AutoSize = true;
            this.rbnCertified.Location = new System.Drawing.Point(347, 422);
            this.rbnCertified.Name = "rbnCertified";
            this.rbnCertified.Size = new System.Drawing.Size(84, 22);
            this.rbnCertified.TabIndex = 78;
            this.rbnCertified.Text = "Certified";
            this.rbnCertified.UseVisualStyleBackColor = true;
            // 
            // pnLabel
            // 
            this.pnLabel.Controls.Add(this.lblVin);
            this.pnLabel.Controls.Add(this.lblStock);
            this.pnLabel.Controls.Add(this.lblDate);
            this.pnLabel.Controls.Add(this.lblYear);
            this.pnLabel.Controls.Add(this.lblMake);
            this.pnLabel.Controls.Add(this.lblModel);
            this.pnLabel.Controls.Add(this.lblTrim);
            this.pnLabel.Controls.Add(this.lblExteriorColor);
            this.pnLabel.Controls.Add(this.lblInteriorColor);
            this.pnLabel.Controls.Add(this.lblCylinders);
            this.pnLabel.Controls.Add(this.lblOdometer);
            this.pnLabel.Controls.Add(this.lblDoors);
            this.pnLabel.Controls.Add(this.lblMsrp);
            this.pnLabel.Controls.Add(this.lblLiters);
            this.pnLabel.Controls.Add(this.lblTransmission);
            this.pnLabel.Controls.Add(this.lblStyle);
            this.pnLabel.Controls.Add(this.lblFuel);
            this.pnLabel.Controls.Add(this.lblDrive);
            this.pnLabel.Location = new System.Drawing.Point(13, 78);
            this.pnLabel.Name = "pnLabel";
            this.pnLabel.Size = new System.Drawing.Size(106, 561);
            this.pnLabel.TabIndex = 77;
            // 
            // lblVin
            // 
            this.lblVin.AutoSize = true;
            this.lblVin.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblVin.Location = new System.Drawing.Point(15, 6);
            this.lblVin.Name = "lblVin";
            this.lblVin.Size = new System.Drawing.Size(37, 18);
            this.lblVin.TabIndex = 14;
            this.lblVin.Text = "VIN*";
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Location = new System.Drawing.Point(15, 32);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(47, 18);
            this.lblStock.TabIndex = 24;
            this.lblStock.Text = "Stock";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblDate.Location = new System.Drawing.Point(15, 58);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(39, 18);
            this.lblDate.TabIndex = 25;
            this.lblDate.Text = "Date";
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblYear.Location = new System.Drawing.Point(15, 84);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(44, 18);
            this.lblYear.TabIndex = 22;
            this.lblYear.Text = "Year*";
            // 
            // lblMake
            // 
            this.lblMake.AutoSize = true;
            this.lblMake.Location = new System.Drawing.Point(15, 110);
            this.lblMake.Name = "lblMake";
            this.lblMake.Size = new System.Drawing.Size(51, 18);
            this.lblMake.TabIndex = 23;
            this.lblMake.Text = "Make*";
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Location = new System.Drawing.Point(15, 136);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(55, 18);
            this.lblModel.TabIndex = 26;
            this.lblModel.Text = "Model*";
            // 
            // lblTrim
            // 
            this.lblTrim.AutoSize = true;
            this.lblTrim.Location = new System.Drawing.Point(15, 162);
            this.lblTrim.Name = "lblTrim";
            this.lblTrim.Size = new System.Drawing.Size(44, 18);
            this.lblTrim.TabIndex = 28;
            this.lblTrim.Text = "Trim*";
            // 
            // lblExteriorColor
            // 
            this.lblExteriorColor.AutoSize = true;
            this.lblExteriorColor.Location = new System.Drawing.Point(15, 216);
            this.lblExteriorColor.Name = "lblExteriorColor";
            this.lblExteriorColor.Size = new System.Drawing.Size(100, 18);
            this.lblExteriorColor.TabIndex = 15;
            this.lblExteriorColor.Text = "Exterior Color";
            // 
            // lblInteriorColor
            // 
            this.lblInteriorColor.AutoSize = true;
            this.lblInteriorColor.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblInteriorColor.Location = new System.Drawing.Point(15, 270);
            this.lblInteriorColor.Name = "lblInteriorColor";
            this.lblInteriorColor.Size = new System.Drawing.Size(94, 18);
            this.lblInteriorColor.TabIndex = 16;
            this.lblInteriorColor.Text = "Interior Color";
            // 
            // lblCylinders
            // 
            this.lblCylinders.AutoSize = true;
            this.lblCylinders.Location = new System.Drawing.Point(15, 348);
            this.lblCylinders.Name = "lblCylinders";
            this.lblCylinders.Size = new System.Drawing.Size(69, 18);
            this.lblCylinders.TabIndex = 13;
            this.lblCylinders.Text = "Cylinders";
            // 
            // lblOdometer
            // 
            this.lblOdometer.AutoSize = true;
            this.lblOdometer.Location = new System.Drawing.Point(15, 323);
            this.lblOdometer.Name = "lblOdometer";
            this.lblOdometer.Size = new System.Drawing.Size(81, 18);
            this.lblOdometer.TabIndex = 32;
            this.lblOdometer.Text = "Odometer*";
            // 
            // lblDoors
            // 
            this.lblDoors.AutoSize = true;
            this.lblDoors.Location = new System.Drawing.Point(15, 430);
            this.lblDoors.Name = "lblDoors";
            this.lblDoors.Size = new System.Drawing.Size(50, 18);
            this.lblDoors.TabIndex = 17;
            this.lblDoors.Text = "Doors";
            // 
            // lblMsrp
            // 
            this.lblMsrp.AutoSize = true;
            this.lblMsrp.Location = new System.Drawing.Point(16, 539);
            this.lblMsrp.Name = "lblMsrp";
            this.lblMsrp.Size = new System.Drawing.Size(106, 18);
            this.lblMsrp.TabIndex = 20;
            this.lblMsrp.Text = "Original MSRP";
            // 
            // lblLiters
            // 
            this.lblLiters.AutoSize = true;
            this.lblLiters.Location = new System.Drawing.Point(15, 375);
            this.lblLiters.Name = "lblLiters";
            this.lblLiters.Size = new System.Drawing.Size(44, 18);
            this.lblLiters.TabIndex = 21;
            this.lblLiters.Text = "Liters";
            // 
            // lblTransmission
            // 
            this.lblTransmission.AutoSize = true;
            this.lblTransmission.Location = new System.Drawing.Point(15, 402);
            this.lblTransmission.Name = "lblTransmission";
            this.lblTransmission.Size = new System.Drawing.Size(104, 18);
            this.lblTransmission.TabIndex = 18;
            this.lblTransmission.Text = "Transmission*";
            // 
            // lblStyle
            // 
            this.lblStyle.AutoSize = true;
            this.lblStyle.Location = new System.Drawing.Point(15, 455);
            this.lblStyle.Name = "lblStyle";
            this.lblStyle.Size = new System.Drawing.Size(40, 18);
            this.lblStyle.TabIndex = 19;
            this.lblStyle.Text = "Style";
            // 
            // lblFuel
            // 
            this.lblFuel.AutoSize = true;
            this.lblFuel.Location = new System.Drawing.Point(15, 482);
            this.lblFuel.Name = "lblFuel";
            this.lblFuel.Size = new System.Drawing.Size(36, 18);
            this.lblFuel.TabIndex = 33;
            this.lblFuel.Text = "Fuel";
            // 
            // lblDrive
            // 
            this.lblDrive.AutoSize = true;
            this.lblDrive.Location = new System.Drawing.Point(15, 509);
            this.lblDrive.Name = "lblDrive";
            this.lblDrive.Size = new System.Drawing.Size(42, 18);
            this.lblDrive.TabIndex = 31;
            this.lblDrive.Text = "Drive";
            // 
            // pnVinDecodeType
            // 
            this.pnVinDecodeType.Controls.Add(this.rbVindecodeYear);
            this.pnVinDecodeType.Controls.Add(this.rbDecodeVin);
            this.pnVinDecodeType.Location = new System.Drawing.Point(13, 35);
            this.pnVinDecodeType.Name = "pnVinDecodeType";
            this.pnVinDecodeType.Size = new System.Drawing.Size(179, 34);
            this.pnVinDecodeType.TabIndex = 76;
            // 
            // rbVindecodeYear
            // 
            this.rbVindecodeYear.AutoSize = true;
            this.rbVindecodeYear.Location = new System.Drawing.Point(82, 9);
            this.rbVindecodeYear.Name = "rbVindecodeYear";
            this.rbVindecodeYear.Size = new System.Drawing.Size(80, 22);
            this.rbVindecodeYear.TabIndex = 75;
            this.rbVindecodeYear.Text = "By Year";
            this.rbVindecodeYear.UseVisualStyleBackColor = true;
            this.rbVindecodeYear.Click += new System.EventHandler(this.rbVindecodeYear_Click);
            // 
            // rbDecodeVin
            // 
            this.rbDecodeVin.AutoSize = true;
            this.rbDecodeVin.Location = new System.Drawing.Point(3, 9);
            this.rbDecodeVin.Name = "rbDecodeVin";
            this.rbDecodeVin.Size = new System.Drawing.Size(70, 22);
            this.rbDecodeVin.TabIndex = 74;
            this.rbDecodeVin.Text = "By Vin";
            this.rbDecodeVin.UseVisualStyleBackColor = true;
            this.rbDecodeVin.Click += new System.EventHandler(this.rbDecodeVin_Click);
            // 
            // imgPicLoad
            // 
            this.imgPicLoad.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.imgPicLoad.Image = global::VinCLAPP.Properties.Resources.ajax_loader_mainform;
            this.imgPicLoad.Location = new System.Drawing.Point(258, 237);
            this.imgPicLoad.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.imgPicLoad.Name = "imgPicLoad";
            this.imgPicLoad.Size = new System.Drawing.Size(101, 122);
            this.imgPicLoad.TabIndex = 72;
            this.imgPicLoad.TabStop = false;
            this.imgPicLoad.Visible = false;
            // 
            // lblEmptyError
            // 
            this.lblEmptyError.AutoSize = true;
            this.lblEmptyError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmptyError.Location = new System.Drawing.Point(10, 663);
            this.lblEmptyError.Name = "lblEmptyError";
            this.lblEmptyError.Size = new System.Drawing.Size(149, 17);
            this.lblEmptyError.TabIndex = 73;
            this.lblEmptyError.Text = "Please fill all the field *";
            // 
            // dtDate
            // 
            this.dtDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDate.Location = new System.Drawing.Point(128, 130);
            this.dtDate.Name = "dtDate";
            this.dtDate.Size = new System.Drawing.Size(197, 24);
            this.dtDate.TabIndex = 65;
            // 
            // cbxModel
            // 
            this.cbxModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxModel.FormattingEnabled = true;
            this.cbxModel.Location = new System.Drawing.Point(128, 207);
            this.cbxModel.Name = "cbxModel";
            this.cbxModel.Size = new System.Drawing.Size(197, 26);
            this.cbxModel.TabIndex = 64;
            // 
            // cbxMake
            // 
            this.cbxMake.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMake.FormattingEnabled = true;
            this.cbxMake.Location = new System.Drawing.Point(128, 181);
            this.cbxMake.Name = "cbxMake";
            this.cbxMake.Size = new System.Drawing.Size(197, 26);
            this.cbxMake.TabIndex = 63;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(346, 185);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(225, 231);
            this.txtDescription.TabIndex = 62;
            // 
            // lblDexcription
            // 
            this.lblDexcription.AutoSize = true;
            this.lblDexcription.Location = new System.Drawing.Point(343, 162);
            this.lblDexcription.Name = "lblDexcription";
            this.lblDexcription.Size = new System.Drawing.Size(83, 18);
            this.lblDexcription.TabIndex = 61;
            this.lblDexcription.Text = "Description";
            // 
            // txtSalePrice
            // 
            this.txtSalePrice.Location = new System.Drawing.Point(421, 126);
            this.txtSalePrice.Name = "txtSalePrice";
            this.txtSalePrice.Size = new System.Drawing.Size(150, 24);
            this.txtSalePrice.TabIndex = 59;
            // 
            // lblSalePrice
            // 
            this.lblSalePrice.AutoSize = true;
            this.lblSalePrice.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblSalePrice.Location = new System.Drawing.Point(344, 129);
            this.lblSalePrice.Name = "lblSalePrice";
            this.lblSalePrice.Size = new System.Drawing.Size(75, 18);
            this.lblSalePrice.TabIndex = 58;
            this.lblSalePrice.Text = "Sale Price";
            // 
            // btnDecode
            // 
            this.btnDecode.BackColor = System.Drawing.SystemColors.GrayText;
            this.btnDecode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDecode.ForeColor = System.Drawing.Color.White;
            this.btnDecode.Location = new System.Drawing.Point(346, 74);
            this.btnDecode.Name = "btnDecode";
            this.btnDecode.Size = new System.Drawing.Size(75, 32);
            this.btnDecode.TabIndex = 56;
            this.btnDecode.Text = "Decode";
            this.btnDecode.UseVisualStyleBackColor = false;
            // 
            // btnComplete
            // 
            this.btnComplete.BackColor = System.Drawing.SystemColors.InfoText;
            this.btnComplete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnComplete.ForeColor = System.Drawing.Color.White;
            this.btnComplete.Location = new System.Drawing.Point(458, 650);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(113, 42);
            this.btnComplete.TabIndex = 55;
            this.btnComplete.Text = "Complete";
            this.btnComplete.UseVisualStyleBackColor = false;
            this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
            // 
            // cbxDrive
            // 
            this.cbxDrive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDrive.FormattingEnabled = true;
            this.cbxDrive.Location = new System.Drawing.Point(128, 582);
            this.cbxDrive.Name = "cbxDrive";
            this.cbxDrive.Size = new System.Drawing.Size(197, 26);
            this.cbxDrive.TabIndex = 48;
            // 
            // cbxFuel
            // 
            this.cbxFuel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFuel.FormattingEnabled = true;
            this.cbxFuel.Location = new System.Drawing.Point(128, 555);
            this.cbxFuel.Name = "cbxFuel";
            this.cbxFuel.Size = new System.Drawing.Size(197, 26);
            this.cbxFuel.TabIndex = 52;
            // 
            // cbxStyle
            // 
            this.cbxStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStyle.FormattingEnabled = true;
            this.cbxStyle.Location = new System.Drawing.Point(128, 528);
            this.cbxStyle.Name = "cbxStyle";
            this.cbxStyle.Size = new System.Drawing.Size(197, 26);
            this.cbxStyle.TabIndex = 51;
            // 
            // cbxTransmission
            // 
            this.cbxTransmission.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTransmission.FormattingEnabled = true;
            this.cbxTransmission.Location = new System.Drawing.Point(128, 475);
            this.cbxTransmission.Name = "cbxTransmission";
            this.cbxTransmission.Size = new System.Drawing.Size(197, 26);
            this.cbxTransmission.TabIndex = 49;
            // 
            // cbxLiters
            // 
            this.cbxLiters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLiters.FormattingEnabled = true;
            this.cbxLiters.Location = new System.Drawing.Point(128, 448);
            this.cbxLiters.Name = "cbxLiters";
            this.cbxLiters.Size = new System.Drawing.Size(197, 26);
            this.cbxLiters.TabIndex = 50;
            // 
            // cbxCylinders
            // 
            this.cbxCylinders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCylinders.FormattingEnabled = true;
            this.cbxCylinders.Location = new System.Drawing.Point(128, 421);
            this.cbxCylinders.Name = "cbxCylinders";
            this.cbxCylinders.Size = new System.Drawing.Size(197, 26);
            this.cbxCylinders.TabIndex = 46;
            // 
            // cbxInteriorColor
            // 
            this.cbxInteriorColor.FormattingEnabled = true;
            this.cbxInteriorColor.Location = new System.Drawing.Point(128, 343);
            this.cbxInteriorColor.Name = "cbxInteriorColor";
            this.cbxInteriorColor.Size = new System.Drawing.Size(197, 26);
            this.cbxInteriorColor.TabIndex = 54;
            // 
            // cbxExteriorColor
            // 
            this.cbxExteriorColor.FormattingEnabled = true;
            this.cbxExteriorColor.Location = new System.Drawing.Point(128, 289);
            this.cbxExteriorColor.Name = "cbxExteriorColor";
            this.cbxExteriorColor.Size = new System.Drawing.Size(197, 26);
            this.cbxExteriorColor.TabIndex = 53;
            // 
            // cbxTrim
            // 
            this.cbxTrim.FormattingEnabled = true;
            this.cbxTrim.Location = new System.Drawing.Point(128, 235);
            this.cbxTrim.Name = "cbxTrim";
            this.cbxTrim.Size = new System.Drawing.Size(197, 26);
            this.cbxTrim.TabIndex = 47;
            // 
            // txtCustomInteriorColor
            // 
            this.txtCustomInteriorColor.Location = new System.Drawing.Point(167, 370);
            this.txtCustomInteriorColor.Name = "txtCustomInteriorColor";
            this.txtCustomInteriorColor.Size = new System.Drawing.Size(158, 24);
            this.txtCustomInteriorColor.TabIndex = 38;
            // 
            // txtCustomExteriorColor
            // 
            this.txtCustomExteriorColor.Location = new System.Drawing.Point(167, 316);
            this.txtCustomExteriorColor.Name = "txtCustomExteriorColor";
            this.txtCustomExteriorColor.Size = new System.Drawing.Size(158, 24);
            this.txtCustomExteriorColor.TabIndex = 37;
            // 
            // lblCustomInteriorColor
            // 
            this.lblCustomInteriorColor.AutoSize = true;
            this.lblCustomInteriorColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomInteriorColor.Location = new System.Drawing.Point(125, 377);
            this.lblCustomInteriorColor.Name = "lblCustomInteriorColor";
            this.lblCustomInteriorColor.Size = new System.Drawing.Size(48, 17);
            this.lblCustomInteriorColor.TabIndex = 27;
            this.lblCustomInteriorColor.Text = "Other:";
            // 
            // txtCustomTrim
            // 
            this.txtCustomTrim.Location = new System.Drawing.Point(167, 262);
            this.txtCustomTrim.Name = "txtCustomTrim";
            this.txtCustomTrim.Size = new System.Drawing.Size(158, 24);
            this.txtCustomTrim.TabIndex = 42;
            // 
            // lblCustomExteriorColor
            // 
            this.lblCustomExteriorColor.AutoSize = true;
            this.lblCustomExteriorColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomExteriorColor.Location = new System.Drawing.Point(125, 323);
            this.lblCustomExteriorColor.Name = "lblCustomExteriorColor";
            this.lblCustomExteriorColor.Size = new System.Drawing.Size(48, 17);
            this.lblCustomExteriorColor.TabIndex = 30;
            this.lblCustomExteriorColor.Text = "Other:";
            // 
            // lblCustomTrim
            // 
            this.lblCustomTrim.AutoSize = true;
            this.lblCustomTrim.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomTrim.Location = new System.Drawing.Point(125, 269);
            this.lblCustomTrim.Name = "lblCustomTrim";
            this.lblCustomTrim.Size = new System.Drawing.Size(48, 17);
            this.lblCustomTrim.TabIndex = 29;
            this.lblCustomTrim.Text = "Other:";
            // 
            // txtMsrp
            // 
            this.txtMsrp.Location = new System.Drawing.Point(128, 609);
            this.txtMsrp.Name = "txtMsrp";
            this.txtMsrp.Size = new System.Drawing.Size(197, 24);
            this.txtMsrp.TabIndex = 39;
            // 
            // txtDoors
            // 
            this.txtDoors.Location = new System.Drawing.Point(128, 502);
            this.txtDoors.Name = "txtDoors";
            this.txtDoors.Size = new System.Drawing.Size(109, 24);
            this.txtDoors.TabIndex = 35;
            // 
            // txtOdometer
            // 
            this.txtOdometer.Location = new System.Drawing.Point(128, 395);
            this.txtOdometer.Name = "txtOdometer";
            this.txtOdometer.Size = new System.Drawing.Size(89, 24);
            this.txtOdometer.TabIndex = 36;
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(128, 208);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(150, 24);
            this.txtModel.TabIndex = 34;
            // 
            // txtMake
            // 
            this.txtMake.Location = new System.Drawing.Point(128, 182);
            this.txtMake.Name = "txtMake";
            this.txtMake.Size = new System.Drawing.Size(150, 24);
            this.txtMake.TabIndex = 43;
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(128, 156);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(197, 24);
            this.txtYear.TabIndex = 44;
            // 
            // txtStock
            // 
            this.txtStock.Location = new System.Drawing.Point(128, 104);
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(197, 24);
            this.txtStock.TabIndex = 40;
            // 
            // txtVin
            // 
            this.txtVin.Location = new System.Drawing.Point(128, 78);
            this.txtVin.Name = "txtVin";
            this.txtVin.Size = new System.Drawing.Size(197, 24);
            this.txtVin.TabIndex = 41;
            // 
            // VinDecodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 695);
            this.Controls.Add(this.pnDecode);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "VinDecodeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Decode";
            this.pnDecode.ResumeLayout(false);
            this.pnDecode.PerformLayout();
            this.pnLabel.ResumeLayout(false);
            this.pnLabel.PerformLayout();
            this.pnVinDecodeType.ResumeLayout(false);
            this.pnVinDecodeType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgPicLoad)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Panel pnDecode;
        private System.Windows.Forms.Panel pnVinDecodeType;
        private System.Windows.Forms.RadioButton rbVindecodeYear;
        private System.Windows.Forms.RadioButton rbDecodeVin;
        private System.Windows.Forms.PictureBox imgPicLoad;
        private System.Windows.Forms.Label lblEmptyError;
        private System.Windows.Forms.DateTimePicker dtDate;
        private System.Windows.Forms.ComboBox cbxModel;
        private System.Windows.Forms.ComboBox cbxMake;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDexcription;
        private System.Windows.Forms.TextBox txtSalePrice;
        private System.Windows.Forms.Label lblSalePrice;
        private System.Windows.Forms.Button btnDecode;
        private System.Windows.Forms.Button btnComplete;
        private System.Windows.Forms.ComboBox cbxDrive;
        private System.Windows.Forms.ComboBox cbxFuel;
        private System.Windows.Forms.ComboBox cbxStyle;
        private System.Windows.Forms.ComboBox cbxTransmission;
        private System.Windows.Forms.Label lblDrive;
        private System.Windows.Forms.ComboBox cbxLiters;
        private System.Windows.Forms.Label lblFuel;
        private System.Windows.Forms.ComboBox cbxCylinders;
        private System.Windows.Forms.Label lblStyle;
        private System.Windows.Forms.ComboBox cbxInteriorColor;
        private System.Windows.Forms.Label lblTransmission;
        private System.Windows.Forms.ComboBox cbxExteriorColor;
        private System.Windows.Forms.Label lblLiters;
        private System.Windows.Forms.Label lblMsrp;
        private System.Windows.Forms.Label lblDoors;
        private System.Windows.Forms.Label lblOdometer;
        private System.Windows.Forms.Label lblCylinders;
        private System.Windows.Forms.ComboBox cbxTrim;
        private System.Windows.Forms.Label lblInteriorColor;
        private System.Windows.Forms.Label lblExteriorColor;
        private System.Windows.Forms.Label lblTrim;
        private System.Windows.Forms.TextBox txtCustomInteriorColor;
        private System.Windows.Forms.TextBox txtCustomExteriorColor;
        private System.Windows.Forms.Label lblCustomInteriorColor;
        private System.Windows.Forms.TextBox txtCustomTrim;
        private System.Windows.Forms.Label lblCustomExteriorColor;
        private System.Windows.Forms.Label lblCustomTrim;
        private System.Windows.Forms.TextBox txtMsrp;
        private System.Windows.Forms.TextBox txtDoors;
        private System.Windows.Forms.TextBox txtOdometer;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Label lblModel;
        private System.Windows.Forms.TextBox txtMake;
        private System.Windows.Forms.Label lblMake;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.TextBox txtVin;
        private System.Windows.Forms.Label lblVin;
        private System.Windows.Forms.Panel pnLabel;
        private System.Windows.Forms.CheckBox rbnCertified;



    }
}