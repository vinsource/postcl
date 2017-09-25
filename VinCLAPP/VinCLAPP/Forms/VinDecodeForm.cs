using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using VinCLAPP.DatabaseModel;
using VinCLAPP.Helper;
using VinCLAPP.Model;
using System.Linq;

namespace VinCLAPP.Forms
{
    public partial class VinDecodeForm : Form
    {
        private int listingID = 0;
        private bool _isbusy;
        public bool Isbusy
        {
            get { return _isbusy; }
            set
            {
                if (value != _isbusy)
                {
                    _isbusy = value;
                    //pnDecode.Enabled = !_isbusy;
                    imgPicLoad.Visible = _isbusy;
                    //if (imgPicLoad.Visible)
                    //{
                    //    imgPicLoad.Enabled = true;
                    //    imgPicLoad.BringToFront();
                    //}
                }

            }
        }
        private DecodeType _decodeType = DecodeType.VIN;
        private string _defaultImageUrl;
        private string _engineType;
        private MainForm _frmMain;

        private string Model
        {
            get
            {
                //listingID!=0 means edit case
                if (_decodeType.Equals(DecodeType.VIN)||listingID!=0)
                    return txtModel.Text;
                else
                    return cbxModel.Text;
            }
            set
            {
                if (_decodeType.Equals(DecodeType.VIN) || listingID != 0)
                    txtModel.Text = value;
                else
                    cbxModel.Text = value;

            }
        }
        private string Make
        {
            get
            {
                if (_decodeType.Equals(DecodeType.VIN) || listingID != 0)
                    return txtMake.Text;
                else
                    return cbxMake.Text;
            }
            set
            {
                if (_decodeType.Equals(DecodeType.VIN) || listingID != 0)
                    txtMake.Text = value;
                else
                    cbxMake.Text = value;

            }
        }

        public VinDecodeForm(DecodeType decodeType, MainForm frmMain)
        {
            _decodeType = decodeType;
        
            Load += VinDecodeForm_Load;
            InitializeComponent();

            if (decodeType.Equals(DecodeType.Manual))
                rbVindecodeYear.Checked = true;
            if (decodeType.Equals(DecodeType.VIN))
                rbDecodeVin.Checked = true;
            this._frmMain = frmMain;
        }

        public VinDecodeForm(DecodeType decodeType, int ListingID,MainForm frmMain)
            : this(decodeType, frmMain)
        {
            this._frmMain = frmMain;
            listingID = ListingID;
        }

        private void InitControl(DecodeType decodeType)
        {

            if (listingID != 0)
            {
                cbxMake.Visible = false;
                cbxModel.Visible = false;
                btnDecode.Visible = false;
            }
            else
            {
                DisableAllChildControls(pnDecode);
                switch (decodeType)
                {
                    case DecodeType.VIN:
                        txtVin.Enabled = true;
                        btnDecode.Enabled = true;
                        cbxMake.Visible = false;
                        cbxModel.Visible = false;
                        pnLabel.Enabled = true;
                        pnVinDecodeType.Enabled = true;
                        lblSalePrice.Enabled = true;
                        lblDexcription.Enabled = true;
                        rbnCertified.Enabled = true;
                        txtVin.KeyDown += new KeyEventHandler(txtVin_KeyDown);
                        break;
                    case DecodeType.Manual:
                        txtYear.Enabled = true;
                        txtYear.KeyDown += new KeyEventHandler(txtYear_KeyDown);
                        btnDecode.Enabled = true;
                        lblVin.Visible = false;
                        txtVin.Visible = false;
                        txtMake.Visible = false;
                        txtModel.Visible = false;
                        pnDecode.Enabled = true;
                        pnLabel.Enabled = true;
                        pnVinDecodeType.Enabled = true;
                          lblSalePrice.Enabled = true;
                        lblDexcription.Enabled = true;
                        rbnCertified.Enabled = true;
                        cbxMake.SelectedIndexChanged += new EventHandler(cbxMake_SelectedIndexChanged);
                        cbxModel.SelectedIndexChanged += new EventHandler(cbxModel_SelectedIndexChanged);
                        break;
                }
            }
        }

        void txtYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnDecode.PerformClick();
            }
        }

        void txtVin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnDecode.PerformClick();
            }
        }

        void DisableAllChildControls(Panel p)
        {
            foreach (Control c in p.Controls)
            {
                c.Enabled = false;
            }
        }

        void EnableAllChildControls(Panel p)
        {
            foreach (Control c in p.Controls)
            {
                c.Enabled = true;
            }
        }

        void cbxModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxModel.DataSource != null)
            {
                if (((SelectListItem)cbxModel.SelectedItem).Value.Equals(String.Empty))
                {
                    ClearValuesFromTrim();
                }
                else
                {
                    Isbusy = true;
                    cbxTrim.DataSource = VinDecodeHelper.GetTrimList(((SelectListItem)cbxModel.SelectedItem).Value);
                    cbxTrim.DisplayMember = "Text";
                    cbxTrim.ValueMember = "Value";
                    Isbusy = false;
                }
            }
        }

        private void ClearValuesFromTrim()
        {
            cbxTrim.DataSource = null;
            txtCustomTrim.Clear();
            cbxExteriorColor.DataSource = null;
            txtCustomExteriorColor.Clear();
            cbxInteriorColor.DataSource = null;
            txtCustomInteriorColor.Clear();
            txtOdometer.Clear();
            cbxCylinders.DataSource = null;
            cbxLiters.DataSource = null;
            cbxTransmission.DataSource = null;
            txtDoors.Clear();
            cbxStyle.DataSource = null;
            cbxFuel.DataSource = null;
            cbxDrive.DataSource = null;
            txtMsrp.Clear();
            txtSalePrice.Clear();
            txtDescription.Clear();
            rbnCertified.Checked = false;
        }

        void cbxMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((SelectListItem)cbxMake.SelectedItem).Value.Equals(String.Empty))
            {
                ClearValuesFromTrim();
                cbxModel.DataSource = null;
            }
            else
            {
                Isbusy = true;
                var source = VinDecodeHelper.GetModelList(txtYear.Text, ((SelectListItem)cbxMake.SelectedItem).Value);
                if (source != null)
                {
                    source.Insert(0, new SelectListItem() { Text = "Please select..", Value = String.Empty });
                }
                cbxModel.DataSource = source;
                cbxModel.DisplayMember = "Text";
                cbxModel.ValueMember = "Value";
                Isbusy = false;
            }
        }

        #region Events

        void VinDecodeForm_Load(object sender, EventArgs e)
        {
            InitControl(_decodeType);
            cbxTrim.SelectedIndexChanged += new EventHandler(cbxTrim_SelectedIndexChanged);
            btnDecode.Click += new EventHandler(btnDecode_Click);
            btnComplete.Click += new EventHandler(btnComplete_Click);

            //In case edit an item, load necessary thing for edit form
            if (listingID != 0)
            {
                Isbusy = true;
                var editWorker = new BackgroundWorker();
                editWorker.DoWork += new DoWorkEventHandler(editWorker_DoWork);
                editWorker.RunWorkerAsync();
            }

         
        }

        void editWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var context = new CLDMSEntities();
            var item = context.Inventories.FirstOrDefault(i => i.ListingID == listingID);

            var result = _decodeType.Equals(DecodeType.VIN) ? VinDecodeHelper.DecodeProcessingByVin(item.VINNumber) : VinDecodeHelper.GetVehicleInformationFromYearMakeModel(int.Parse(item.ModelYear), item.Make, item.Model);

            CombineWithCurrentData(result, listingID);
            BindingFormControl(result);
            if (result != null)
            {
                txtVin.Text = result.VinNumber;
            }
            lblMessage.Text = "Edit inventory";
            Isbusy = false;
        }

        void cbxTrim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxTrim.DataSource != null)
            {
                Isbusy = true;
                switch (_decodeType)
                {
                    case DecodeType.Manual:
                        var manualItem =
                            VinDecodeHelper.GetVehicleInformationFromStyleId(
                                ((SelectListItem)cbxTrim.SelectedItem).Value);
                        BindTrimRelatedItem(manualItem);
                        break;
                    case DecodeType.VIN:
                        var item = VinDecodeHelper.GetVehicleInformationFromStyleId(txtVin.Text,
                                                                                    ((SelectListItem)
                                                                                     cbxTrim.SelectedItem).Value);
                        BindTrimRelatedItem(item);
                        break;
                }
                Isbusy = false;
            }
        }
      

        private void btnDecode_Click(object sender, EventArgs e)
        {
            try
            {
                Isbusy = true;
                switch (_decodeType)
                {
                    case DecodeType.VIN:
                        var vin = txtVin.Text;
                        var result = (VinDecodeHelper.DecodeProcessingByVin(vin));
                        CombineWithCurrentData(result, txtVin.Text);
                        BindingFormControl(result);
                        break;
                    case DecodeType.Manual:
                        var source = VinDecodeHelper.GetMakeList(txtYear.Text);
                        if (source != null)
                        {
                            source.Insert(0, new SelectListItem() { Text = "Please select..", Value = String.Empty });
                        }
                        cbxMake.DataSource = source;
                        cbxMake.DisplayMember = "Text";
                        cbxMake.ValueMember = "Value";
                        break;
                }
                EnableAllChildControls(pnDecode);
                Isbusy = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please input correct data format. Error detail: " + ex.StackTrace);
                EnableAllChildControls(pnDecode);
                Isbusy = false;
            }
        }

     

        private bool ValidateForm()
        {
            switch (_decodeType)
            {
                case DecodeType.VIN:
                    if (String.IsNullOrEmpty(txtVin.Text) || String.IsNullOrEmpty(txtMake.Text) || String.IsNullOrEmpty(txtModel.Text))
                        return false;
                    break;
                case DecodeType.Manual:
                    if (cbxMake.SelectedItem == null || cbxModel.SelectedItem == null || String.IsNullOrEmpty(((SelectListItem)cbxMake.SelectedItem).Value) || String.IsNullOrEmpty(((SelectListItem)cbxModel.SelectedItem).Value) )
                        return false;
                    break;
            }

            return cbxTrim.SelectedItem != null && !String.IsNullOrEmpty(txtYear.Text) && !String.IsNullOrEmpty(((SelectListItem)cbxTrim.SelectedItem).Value) && !String.IsNullOrEmpty(txtOdometer.Text) && !String.IsNullOrEmpty(((SelectListItem)cbxTransmission.SelectedItem).Value);
        }

        #endregion

        #region Binding

        private void BindTrimRelatedItem(AppraisalViewFormModel result)
        {
            if (result != null)
            {
                BindToListByText(cbxExteriorColor, result.ExteriorColorList);
                BindToListByText(cbxInteriorColor, result.InteriorColorList);
                BindToListByText(cbxCylinders, result.CylinderList);
                BindToListByText(cbxLiters, result.LitersList);
                BindToList(cbxTransmission, result.TranmissionList);
                BindToListByText(cbxStyle, result.BodyTypeList);
                BindToListByText(cbxFuel, result.FuelList);
                BindToListByText(cbxDrive, result.DriveTrainList);
                txtCustomExteriorColor.Text = result.CusExteriorColor;
                txtCustomInteriorColor.Text = result.CusInteriorColor;
                txtDoors.Text = result.Door;
                txtMsrp.Text = result.MSRP;

               
            }
        }

        private void BindingFormControl(AppraisalViewFormModel result)
        {
            BindToListByText(cbxTrim, result.TrimList);
            txtStock.Text = result.StockNumber;
            dtDate.Text = result.DateInStock.HasValue ? result.DateInStock.Value.ToShortDateString() : String.Empty;
            txtYear.Text = result.ModelYear;
            Make = result.Make;
            Model = result.SelectedModel;
            txtCustomTrim.Text = result.CusTrim;
            rbnCertified.Checked = result.IsCertified;
            txtSalePrice.Text = result.SalePrice;
            txtDescription.Text = result.Descriptions;
            _defaultImageUrl = result.DefaultImageUrl;
            _engineType = result.EngineType;
            txtOdometer.Text = result.Mileage;
            lblMessage.Text = result.IsExisted ? "Edit an inventory" : "Create new inventory";
            txtYear.Enabled = false;
            txtModel.Enabled = false;
            txtMake.Enabled = false;

            BindTrimRelatedItem(result);

       
            if (!String.IsNullOrEmpty(result.SelectedTranmission))
            {
                cbxTransmission.SelectedIndex = result.SelectedTranmission.Equals("Automatic") ? 1 : 2;
            }
        }

        private static void BindToList(ComboBox control, IEnumerable<SelectListItem> result)
        {
            control.DataSource = result.ToList();
            control.DisplayMember = "Text";
            control.ValueMember = "Value";
        }

        private void BindToListByText(ComboBox control, IEnumerable<SelectListItem> result)
        {
            BindToList(control, result);
            var item = result.FirstOrDefault(i => i.Selected);
            //if (selectedText != null && result.Select(i => i.Text).Contains(selectedText))
            if (item != null)
            {
                control.Text = item.Text;
            }
        }



        #endregion

        #region DataHelpers

        private void CombineWithCurrentData(AppraisalViewFormModel result, string VIN)
        {
            var context = new CLDMSEntities();
            var item = context.Inventories.FirstOrDefault(i => i.VINNumber.ToLower().Equals(VIN.ToLower()));
            if (result != null)
                CheckInDatabase(result, item);
        }

        private void CombineWithCurrentData(AppraisalViewFormModel result, int listingID)
        {
            var context = new CLDMSEntities();
            var item = context.Inventories.FirstOrDefault(i => i.ListingID == listingID);
            if (result != null)
                CheckInDatabase(result, item);
        }

        private static void CheckInDatabase(AppraisalViewFormModel result, Inventory item)
        {
            if (item != null)
            {
                result.IsExisted = true;
                result.StockNumber = item.StockNumber;
                result.DateInStock = item.DateInStock;
                result.ModelYear = item.ModelYear;
                result.Make = item.Make;
                result.SelectedModel = item.Model;

                TransferTrimAndTrimList(result, item);
                TransferExteriorColorAndList(result, item);
                TransferInteriorColorAndList(result, item);

                result.SelectedTranmission = item.Tranmission;
                result.Mileage = item.Mileage;
                result.SelectedCylinder = item.Cylinders;
                result.SelectedLiters = item.Liters;
                result.SelectedTranmission = item.Tranmission;
                result.Door = item.Doors;
                result.MSRP = item.MSRP;
                result.SelectedBodyType = item.BodyType;
                result.SelectedFuel = item.FuelType;
                result.SelectedDriveTrain = item.DriveTrain;
                result.SalePrice = item.SalePrice;
                result.Descriptions = item.Descriptions;
                result.IsCertified = item.Certified ?? false;
            }
            else
            {
                result.IsExisted = false;
            }
        }

        private static void TransferInteriorColorAndList(AppraisalViewFormModel result, Inventory item)
        {
            result.SelectedInteriorColor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.InteriorColor.ToLower());
            result.InteriorColorList =
                result.InteriorColorList.Select(
                    i =>
                    new SelectListItem()
                    {
                        Selected = false,
                        Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(i.Text.ToLower()),
                        Value = i.Value
                    }).ToList();
            var interiorColorItem = result.InteriorColorList.FirstOrDefault(i => i.Text == result.SelectedInteriorColor);
            if (interiorColorItem != null)
            {
                result.CusInteriorColor = String.Empty;
                interiorColorItem.Selected = true;
            }
            else
            {
                result.CusInteriorColor = result.SelectedInteriorColor;
                var baseitem = result.InteriorColorList.FirstOrDefault(i => i.Text == Constants.OtherColors);
                if (baseitem != null)
                {
                    baseitem.Selected = true;
                }
            }
        }

        private static void TransferExteriorColorAndList(AppraisalViewFormModel result, Inventory item)
        {
            result.SelectedExteriorColorValue = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.ExteriorColor.ToLower());
            result.ExteriorColorList =
                result.ExteriorColorList.Select(
                    i =>
                    new SelectListItem()
                    {
                        Selected = false,
                        Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(i.Text.ToLower()),
                        Value = i.Value
                    }).ToList();

            var exteriorColorItem = result.ExteriorColorList.FirstOrDefault(i => i.Text == result.SelectedExteriorColorValue);
            if (exteriorColorItem != null)
            {
                result.CusExteriorColor = String.Empty;
                exteriorColorItem.Selected = true;
            }
            else
            {
                result.CusExteriorColor = result.SelectedExteriorColorValue;
                var baseitem = result.ExteriorColorList.FirstOrDefault(i => i.Text == Constants.OtherColors);
                if (baseitem != null)
                {
                    baseitem.Selected = true;
                }
            }
        }

        private static void TransferTrimAndTrimList(AppraisalViewFormModel result, Inventory item)
        {
            result.Trim = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.Trim.ToLower());
            result.TrimList =
                result.TrimList.Select(
                    i =>
                    new SelectListItem()
                    {
                        Selected = false,
                        Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(i.Text.ToLower()),
                        Value = i.Value
                    }).ToList();
            var trimItem = result.TrimList.FirstOrDefault(i => i.Text == result.Trim);
            if (trimItem != null)
            {
                result.CusTrim = String.Empty;
                trimItem.Selected = true;
            }
            else
            {
                result.CusTrim = result.Trim;
                var baseitem = result.TrimList.FirstOrDefault(i => i.Text == Constants.OtherTrims);
                if (baseitem != null)
                {
                    baseitem.Selected = true;
                }
            }
        }

        #endregion

        #region Manipulate Form Data
        private void SaveToDB()
        {
            var context = new CLDMSEntities();
            var item = context.Inventories.FirstOrDefault(i => i.ListingID == listingID);

            if (item != null)
            {
              
                LoadData(item);
                context.SaveChanges();
            }
            else
            {
                var inventory = new Inventory();
                LoadData(inventory);
                context.Inventories.Add(inventory);
                context.SaveChanges();
            }
        }

        private void LoadData(Inventory item)
        {
            item.AddToInventoryBy = GlobalVar.CurrentAccount.AccountId.ToString(CultureInfo.InvariantCulture);
            item.BodyType = cbxStyle.SelectedText;
            item.Certified = rbnCertified.Checked;
            item.Cylinders = cbxCylinders.SelectedText;
            DateTime date;
            if (DateTime.TryParse(dtDate.Text, out date))
            {
                item.DateInStock = date;
            }
            item.Make = Make;
            item.Mileage = txtOdometer.Text;
            item.Model = Model;
            item.SalePrice = txtSalePrice.Text;
            item.DealerId = GlobalVar.CurrentDealer.DealerId;
            item.DealershipAddress = GlobalVar.CurrentDealer.StreetAddress;
            item.DealershipCity = GlobalVar.CurrentDealer.City;
            item.DealershipName = GlobalVar.CurrentDealer.DealershipName;
            item.DealershipPhone = GlobalVar.CurrentDealer.PhoneNumber;
            item.DealershipState = GlobalVar.CurrentDealer.State;
            item.DealershipZipCode = GlobalVar.CurrentDealer.ZipCode;
            item.DefaultImageUrl = _defaultImageUrl;
            item.Descriptions = txtDescription.Text;
            item.Doors = txtDoors.Text;
            item.DriveTrain = cbxDrive.SelectedText;
            item.EngineType = _engineType;
            item.FuelType = cbxFuel.SelectedText;
            item.LastUpdated = DateTime.Now;
            item.Liters = cbxLiters.SelectedText;
            item.MSRP = CommonHelper.RemoveSpecialCharactersForMsrp(txtMsrp.Text);
            item.ModelYear = txtYear.Text;
            item.StockNumber = txtStock.Text;

            var selectedTranmission = (SelectListItem)cbxTransmission.SelectedItem;

            item.Tranmission = selectedTranmission.Value;
            item.VINNumber = txtVin.Text;

            item.Trim = cbxTrim.Text.Equals(Constants.OtherTrims) ? txtCustomTrim.Text : cbxTrim.Text;
            item.InteriorColor = cbxInteriorColor.Text.Equals(Constants.OtherColors) ? txtCustomInteriorColor.Text : cbxInteriorColor.Text;
            item.ExteriorColor = cbxExteriorColor.Text.Equals(Constants.OtherColors) ? txtCustomExteriorColor.Text : cbxExteriorColor.Text;
        }

        #endregion


        private void rbVindecodeYear_Click(object sender, EventArgs e)
        {
            if (rbVindecodeYear.Checked)
            {
                this.Close();
                var form = new VinDecodeForm(DecodeType.Manual,_frmMain);
                form.Show();

            }
        }

        private void rbDecodeVin_Click(object sender, EventArgs e)
        {
            if (rbDecodeVin.Checked)
            {
                this.Close();
                var form = new VinDecodeForm(DecodeType.VIN, _frmMain);
                form.Show();

            }
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
           
            try
            {
                if (!ValidateForm())
                {
                    lblMessage.Text = "Please fill in required fields.";
                }
                else
                {
                    Isbusy = true;
                    SaveToDB();
                    _frmMain.RefreshInventoryForChangesFromSubForm();
                    Isbusy = false;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please input correct data format. Error detail: " + ex.StackTrace);
                EnableAllChildControls(pnDecode);
                Isbusy = false;
            }
        }

     

       

      
        

      
    }
}