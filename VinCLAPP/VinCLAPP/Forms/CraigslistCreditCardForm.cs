using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Forms;
using VinCLAPP.Helper;
using VinCLAPP.Model;

namespace VinCLAPP.Forms
{
    public partial class CraigslistCreditCardForm : Form
    {
        private IEnumerable<UsState> _allStatesList;
        private readonly MainForm _frmMain;
        public CraigslistCreditCardForm()
        {
            InitializeComponent();
        }
        public CraigslistCreditCardForm(MainForm frmMain)
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
            
            if (!flag1 )
            {
                string cardNumber = CreditCardHelper.NormalizeCardNumber(txtCardNumber.Text.Trim());
                flag17 = !CreditCardHelper.IsCardNumberValid(cardNumber);
                errorProvider12.SetError(txtCardNumber, flag17 ? "Please enter a valid card number" : "");
                
            }


            bool flag2 = String.IsNullOrEmpty(txtFirstName.Text.Trim());

            errorProvider2.SetError(txtFirstName, flag2 ? "Please enter a valid first name on the card" : "");

            bool flag13 = String.IsNullOrEmpty(txtLastName.Text.Trim());

            errorProvider2.SetError(txtLastName, flag13 ? "Please enter a valid last name on the card" : "");


            bool flag3 = String.IsNullOrEmpty(txtSecurityCode.Text.Trim());

            errorProvider3.SetError(txtSecurityCode, flag3 ? "Please enter a valid security code" : "");


            bool flag9 = String.IsNullOrEmpty(txtStreetAddress.Text.Trim());

            errorProvider5.SetError(txtStreetAddress, flag9 ? "Please enter dealership street address" : "");

            bool flag10 = String.IsNullOrEmpty(txtZipcode.Text.Trim());

            errorProvider6.SetError(txtZipcode, flag10 ? "Please enter zip code" : "");

            bool flag11 = String.IsNullOrEmpty(txtCity.Text.Trim());

            errorProvider7.SetError(txtCity, flag11 ? "Please enter city" : "");

            bool flag12 = cbState.SelectedItem == null;

            bool flag14 = String.IsNullOrEmpty(txtContactName.Text.Trim());

            errorProvider9.SetError(txtContactName, flag14 ? "Please enter Contact Name" : "");

            bool flag15 = String.IsNullOrEmpty(txtPhoneNumber.Text.Trim());

            errorProvider10.SetError(txtPhoneNumber, flag15 ? "Please enter Contact Phone" : "");

            bool flag16 = String.IsNullOrEmpty(txtContactEmailAddress.Text.Trim());

            errorProvider11.SetError(txtContactEmailAddress, flag16 ? "Please enter Contact Email Address" : "");

            finalFlag = !flag1 && !flag2 && !flag3 && !flag9 && !flag10 && !flag11 && !flag12 && !flag14 && !flag15 && !flag16&&!flag17;
            


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

                var selectedState = (UsState)cbState.SelectedItem;

                //if(cardNumber.Length>=15)
                    Properties.Settings.Default.cardnumber = cardNumber;
                Properties.Settings.Default.securitycode = txtSecurityCode.Text;
                Properties.Settings.Default.expirationmonth = expirationMonth.Month;
             
                Properties.Settings.Default.expirationyear = expirationYear;
                Properties.Settings.Default.cardfirstname = txtFirstName.Text;
                Properties.Settings.Default.cardlastname = txtLastName.Text;
                Properties.Settings.Default.cardaddress = txtStreetAddress.Text;
                Properties.Settings.Default.cardcity = txtCity.Text;
                Properties.Settings.Default.cardstate = selectedState.StateAbbr;
                Properties.Settings.Default.cardzipcode = txtZipcode.Text;
                Properties.Settings.Default.cardcontactname = txtContactName.Text;
                Properties.Settings.Default.cardcontactphone = txtPhoneNumber.Text;
                Properties.Settings.Default.cardcontactemail = txtContactEmailAddress.Text;


                Properties.Settings.Default.Save();

                this.Close();

                if(_frmMain!=null)
                    _frmMain.RunFirstExecuteNewVersion();
            }
        }

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
                    errorProvider1.SetError(txtCardNumber, "Please enter the valid card number.");

                }
                else
                {
                    var cardType = CreditCardHelper.GetCardType(cardNumber);

                    if (cardType.Equals(CreditCardHelper.CardType.Amex))
                    {
                        errorProvider1.SetError(txtCardNumber, "");

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

        private void CraigslistCreditCardForm_Load(object sender, EventArgs e)
        {
            cbExpireMonths.DataSource = QuickBookHelper.GetExpirationMonths();

            cbExpireYear.DataSource = QuickBookHelper.GetExpirationYears();
            
            _allStatesList = ZipCodeHelper.GetAllStates();

            cbState.DataSource = _allStatesList;


            
            this.txtCardNumber.Text = Properties.Settings.Default.cardnumber;

            this.txtSecurityCode.Text = Properties.Settings.Default.securitycode;

            this.txtFirstName.Text = Properties.Settings.Default.cardfirstname;

            this.txtLastName.Text = Properties.Settings.Default.cardlastname;

            this.txtStreetAddress.Text = Properties.Settings.Default.cardaddress;

            this.txtCity.Text = Properties.Settings.Default.cardcity;

            this.txtZipcode.Text = Properties.Settings.Default.cardzipcode;

            this.txtContactName.Text = Properties.Settings.Default.cardcontactname;

            this.txtPhoneNumber.Text = Properties.Settings.Default.cardcontactphone;

            this.txtContactEmailAddress.Text = Properties.Settings.Default.cardcontactemail;

            cbState.SelectedIndex = _allStatesList.ToList().FindIndex(x => x.StateAbbr == Properties.Settings.Default.cardstate);

            foreach (ExpirationMonth cbi in cbExpireMonths.Items)
            {
                if (cbi.Month == Properties.Settings.Default.expirationmonth)
                {
                    cbExpireMonths.SelectedItem = cbi;
                    break;
                }
            }

          
            cbExpireYear.SelectedIndex = QuickBookHelper.GetExpirationYears().ToList().FindIndex(x => x == Properties.Settings.Default.expirationyear);

            
        }

        private string FilterCc(string source)
        {
            if (!String.IsNullOrEmpty(source))
            {
                var x = new Regex(@"^\d+(?=\d{4}$)");
                return x.Replace(source, match => new String('*', match.Value.Length));
            }
            return String.Empty;
        }
    }
}
