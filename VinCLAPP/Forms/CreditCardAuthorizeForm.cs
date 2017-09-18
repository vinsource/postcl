using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Windows.Forms;
using VinCLAPP.Custom;
using VinCLAPP.DatabaseModel;
using VinCLAPP.Helper;
using VinCLAPP.Model;

namespace VinCLAPP
{
    public partial class CreditCardAuthorizeForm : Form
    {
        private readonly MainForm _frmMain;

        private IEnumerable<UsState> _allStatesList;

        private BillingCustomer _billingCustomer;

        private bool _success;

        public CreditCardAuthorizeForm()
        {
            InitializeComponent();
        }

        public CreditCardAuthorizeForm(MainForm frmMain)
        {
            InitializeComponent();
            this._frmMain = frmMain;
        }


        private bool ValidateForm()
        {
            bool finalFlag = false;

            bool flag1 = String.IsNullOrEmpty(txtCardNumber.Text.Trim());

            errorProvider1.SetError(txtCardNumber, flag1 ? "Please enter a valid card number" : "");
            
            bool flag17 = false;

            if (!flag1)
            {
                string cardNumber = CreditCardHelper.NormalizeCardNumber(txtCardNumber.Text.Trim());
                flag17 = !CreditCardHelper.IsCardNumberValid(cardNumber);
                errorProvider8.SetError(txtCardNumber, flag17 ? "Please enter a valid card number" : "");

            }

            bool flag2 = String.IsNullOrEmpty(txtNameOnCard.Text.Trim());

            errorProvider2.SetError(txtNameOnCard, flag2 ? "Please enter a valid name on the card" : "");

            bool flag3 = String.IsNullOrEmpty(txtSecurityCode.Text.Trim());

            errorProvider3.SetError(txtSecurityCode, flag3 ? "Please enter a valid security code" : "");


            bool flag9 = String.IsNullOrEmpty(txtStreetAddress.Text.Trim());

            errorProvider5.SetError(txtStreetAddress, flag9 ? "Please enter dealership street address" : "");

            bool flag10 = String.IsNullOrEmpty(txtZipcode.Text.Trim());

            errorProvider6.SetError(txtZipcode, flag10 ? "Please enter zip code" : "");

            bool flag11 = String.IsNullOrEmpty(txtCity.Text.Trim());

            errorProvider7.SetError(txtCity, flag11 ? "Please enter city" : "");

            bool flag12 = cbState.SelectedItem == null;

            finalFlag = !flag1 && !flag2 && !flag3 && !flag9 && !flag10 && !flag11 && !flag12 && !flag17;


            return finalFlag;

        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {

                btnOK.Enabled = false;

                panelIndicator.Visible = true;

                Refresh();

                string cardNumber = CreditCardHelper.NormalizeCardNumber(txtCardNumber.Text.Trim());

                var expirationMonth = (ExpirationMonth)cbExpireMonths.SelectedItem;

                var expirationYear = (int)cbExpireYear.SelectedItem;

                var selectPackage = (PostClPackgae)cbPackages.SelectedItem;

                var cardType = CreditCardHelper.GetCardType(cardNumber);

                _billingCustomer = new BillingCustomer()
                {
                    NameOnCard = txtNameOnCard.Text.Trim(),
                    CardNumber = txtCardNumber.Text.Trim(),
                    ExpirationMonth = expirationMonth.Month,
                    ExpirationYear = expirationYear,
                    SelectedPackage = selectPackage,
                    SecurityCode = txtSecurityCode.Text.Trim(),
                    DealerCity = GlobalVar.CurrentDealer.City,
                    DealerName = GlobalVar.CurrentDealer.DealershipName,
                    DealerPhone = GlobalVar.CurrentDealer.PhoneNumber,
                    DealerState = GlobalVar.CurrentDealer.State,
                    DealerStreetAddress = GlobalVar.CurrentDealer.StreetAddress,
                    DealerZipCode = GlobalVar.CurrentDealer.ZipCode,
                    //DifferentBillingAddress = cbDiffBilling.Checked,
                    BillingStreetAddress = txtStreetAddress.Text.Trim(),
                    BillingCity = txtCity.Text,
                    BillingState = cbState.SelectedText,
                    BillingZipCode = txtZipcode.Text,
                    QuickBookAccountName = GlobalVar.CurrentAccount.QuickBookAccountName,
                    FirstName = GlobalVar.CurrentAccount.FirstName,
                    LastName = GlobalVar.CurrentAccount.LastName,
                    CustomerEmail = GlobalVar.CurrentAccount.AccountName,
                    CreditCardType = cardType,
                    OneTimeSetUpFeed=cbkOnetimeSetupFeed.Checked

                };

              bgCreditCardProcessing.RunWorkerAsync();
            }
            else
            {


                MessageBox.Show(
                   "Please fill correct info in required fields. ",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        private void bgCreditCardProcessing_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            _success = QuickBookHelper.CreateSalesReceiptAndMakePayment(_billingCustomer);

            if (_success)
            {
                DataHelper.RemoveTrialPeriod(_billingCustomer);

                EmailHelper.SendConfirmationEmailForPayment(_billingCustomer);

                EmailHelper.SendConfirmationEmailForPaymentToCustomer(new MailAddress(GlobalVar.CurrentAccount.AccountName), _billingCustomer);

            }
           
        }

        private void bgCreditCardProcessing_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (_success)
            {
                panelIndicator.Visible = false;

                MessageBox.Show("Thanks for using PostCL. The transaction has been processed successfully.", "Message",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                _frmMain.AfterLogging();

                this.Close();
            }
            else
            {
                panelIndicator.Visible = false;

                MessageBox.Show(
                    "We apologize for inconvenience. We can not process your credit card at this time. Please try later or different credit card.",
                    "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
      

        //private void cbDiffBilling_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (cbDiffBilling.Checked)
        //    {
        //        grbBilling.Enabled = true;

        //        txtStreetAddress.Text ="";

        //        txtCity.Text ="";

        //        txtZipcode.Text ="";

        //        cbState.Enabled = true;

        //    }
        //    else
        //    {


        //        txtStreetAddress.Text = GlobalVar.CurrentDealer.StreetAddress;

        //        txtCity.Text = GlobalVar.CurrentDealer.City;

        //        txtZipcode.Text = GlobalVar.CurrentDealer.ZipCode;

        //        int zipcode = 0;
        //        Int32.TryParse(GlobalVar.CurrentDealer.ZipCode, out zipcode);

        //        ZipCodeCity findResult = ZipCodeHelper.LookUpZipCode(zipcode);

        //        if (!String.IsNullOrEmpty(findResult.City))
        //        {
        //            txtCity.Text = findResult.City;

        //            cbState.SelectedIndex = _allStatesList.ToList().FindIndex(x => x.State == findResult.State);

        //        }

        //        grbBilling.Enabled = false;
        //    }
        //}

        private void CreditCardAuthorizeForm_Load(object sender, EventArgs e)
        {
            cbExpireMonths.DataSource = QuickBookHelper.GetExpirationMonths();

            cbExpireYear.DataSource = QuickBookHelper.GetExpirationYears();

            cbPackages.DataSource = QuickBookHelper.GetAllPackages();

            _allStatesList = ZipCodeHelper.GetAllStates();

            cbState.DataSource = _allStatesList;

            txtNameOnCard.Text = GlobalVar.CurrentAccount.FirstName + " " + GlobalVar.CurrentAccount.LastName;

            txtStreetAddress.Text = GlobalVar.CurrentDealer.StreetAddress;

            txtCity.Text = GlobalVar.CurrentDealer.City;

            txtZipcode.Text = GlobalVar.CurrentDealer.ZipCode;

            int zipcode = 0;
            Int32.TryParse(GlobalVar.CurrentDealer.ZipCode, out zipcode);

            ZipCodeCity findResult = ZipCodeHelper.LookUpZipCode(zipcode);

            if (!String.IsNullOrEmpty(findResult.City))
            {
                txtCity.Text = findResult.City;

                cbState.SelectedIndex = _allStatesList.ToList().FindIndex(x => x.State == findResult.State);

            }
        }

    

        //private void linkWhatis_Click(object sender, EventArgs e)
        //{
        //    linkWhatis.Tag = global::VinCLAPP.Properties.Resources.securitycode;

        //    var myToolTipTemplate = new CustomizedToolTip { AutoSize = true };

        //    myToolTipTemplate.SetToolTip(linkWhatis, " ");
        //}

        private void txtCardNumber_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtCardNumber.Text.Trim()))
            {
                errorProvider1.SetError(txtCardNumber, "Please enter the valid card number.");
            }
            else
            {
                string cardNumber = CreditCardHelper.NormalizeCardNumber(txtCardNumber.Text.Trim());

                if (!CreditCardHelper.IsCardNumberValid(cardNumber))
                {
                    errorProvider1.SetError(txtCardNumber,"Please enter the valid card number.");
                    
                }
                else
                {
                    var cardType = CreditCardHelper.GetCardType(cardNumber);

                    if (cardType.Equals(CreditCardHelper.CardType.Amex))
                    {
                         errorProvider1.SetError(txtCardNumber,"");

                        pcBoxCreditCardType.Visible = true;

                        pcBoxCreditCardType.Image = global::VinCLAPP.Properties.Resources.amex_48;
                    }
                    else if (cardType.Equals(CreditCardHelper.CardType.Discover))
                    {
                        errorProvider1.SetError(txtCardNumber, "");

                        pcBoxCreditCardType.Visible = true;

                        pcBoxCreditCardType.Image = global::VinCLAPP.Properties.Resources.discover_48;
                    }
                    else if (cardType.Equals(CreditCardHelper.CardType.MasterCard))
                    {
                        errorProvider1.SetError(txtCardNumber, "");

                        pcBoxCreditCardType.Visible = true;

                        pcBoxCreditCardType.Image = global::VinCLAPP.Properties.Resources.mastercard_48;
                    }
                    else if (cardType.Equals(CreditCardHelper.CardType.VISA))
                    {
                        errorProvider1.SetError(txtCardNumber, "");

                        pcBoxCreditCardType.Visible = true;

                        pcBoxCreditCardType.Image = global::VinCLAPP.Properties.Resources.visa_48;
                    }
                }
            }
        }

        private void txtZipcode_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtZipcode.Text))
            {
                int zipcode = 0;
                Int32.TryParse(txtZipcode.Text, out zipcode);

                ZipCodeCity findResult = ZipCodeHelper.LookUpZipCode(zipcode);

                if (!String.IsNullOrEmpty(findResult.City))
                {
                    txtCity.Text = findResult.City;

                    cbState.SelectedIndex = _allStatesList.ToList().FindIndex(x => x.State == findResult.State);

                    cbState.Enabled = false;
                }
            }
        }

        private void txtNameOnCard_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNameOnCard.Text.Trim()))
            {
                errorProvider2.SetError(txtNameOnCard, "Please enter the valid name.");
            }
            else
            {
                errorProvider2.SetError(txtNameOnCard,"");
            }
        }

        private void txtSecurityCode_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtSecurityCode.Text.Trim()))
            {
                errorProvider3.SetError(txtSecurityCode, "Please enter the valid security code.");
            }
            else
            {
                errorProvider3.SetError(txtSecurityCode, "");
            }
        }

        private void cbPackages_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedPackage = (PostClPackgae) cbPackages.SelectedItem;
            if (selectedPackage.Period.Equals("1 month"))
            {
                cbkOnetimeSetupFeed.Checked = false;
                cbkOnetimeSetupFeed.Enabled = true;
                cbkOnetimeSetupFeed.Text = "One time datafeed setup - $99.00";
            }
            else if (selectedPackage.Period.Equals("6 months"))
            {
                cbkOnetimeSetupFeed.Checked = true;
                cbkOnetimeSetupFeed.Enabled = false;
                cbkOnetimeSetupFeed.Text = "One time datafeed setup - Included";
            }

            else if (selectedPackage.Period.Equals("1 year"))
            {
                cbkOnetimeSetupFeed.Checked = true;
                cbkOnetimeSetupFeed.Enabled = false;
                cbkOnetimeSetupFeed.Text = "One time datafeed setup - Included";
            }
        }

        //private void linkWhatis_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    linkWhatis.Tag = global::VinCLAPP.Properties.Resources.securitycode;

        //    var myToolTipTemplate = new CustomizedToolTip { AutoSize = true };

        //    myToolTipTemplate.SetToolTip(linkWhatis, " ");
        //}




    }
}