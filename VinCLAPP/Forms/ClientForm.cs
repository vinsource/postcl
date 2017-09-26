using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Windows.Forms;
using VinCLAPP.Helper;
using VinCLAPP.Model;

namespace VinCLAPP.Forms
{
    public partial class ClientForm : Form
    {
        private IEnumerable<UsState> _allStatesList;

        private readonly string _mode;

        private PostClCustomer _newCustomer;

        private int _dailyLimit=0;

        public ClientForm()
        {
            InitializeComponent();
        }

        public ClientForm(string mode)
        {
            this._mode = mode;
            if (mode.Equals("Edit"))
            {
                InitializeComponent();

                btnOK.Text = "Save and close";

                txtFirstName.Enabled = false;

                txtLastName.Enabled = false;

                txtCustomerEmail.Enabled = false;

                txtDealerName.Enabled = false;

                txtStreetAddress.Enabled = false;

                txtZipcode.Enabled = false;

                txtCity.Enabled = false;
            }
        }

       

  

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            _allStatesList = ZipCodeHelper.GetAllStates();

            cbState.DataSource = _allStatesList;

            cbFormat.DataSource = QuickBookHelper.GetLeadFormats();

            txtFirstName.Focus();

            if (GlobalVar.CurrentAccount != null)
            {
                txtFirstName.Text = GlobalVar.CurrentAccount.FirstName;

                txtLastName.Text = GlobalVar.CurrentAccount.LastName;

                txtCustomerEmail.Text = GlobalVar.CurrentAccount.AccountName;

                txtCustomerPhone.Text = GlobalVar.CurrentAccount.PersonalPhone;

                txtDealerName.Text = GlobalVar.CurrentDealer.DealershipName;

                txtWebSiteAddress.Text = GlobalVar.CurrentDealer.WebSiteUrl;

                txtStreetAddress.Text = GlobalVar.CurrentDealer.StreetAddress;

                txtZipcode.Text = GlobalVar.CurrentDealer.ZipCode;

                txtCity.Text = GlobalVar.CurrentDealer.City;

                cbState.SelectedIndex = _allStatesList.ToList().FindIndex(x => x.StateAbbr == GlobalVar.CurrentDealer.State);

                txtDealerPhone.Text = GlobalVar.CurrentDealer.PhoneNumber;

                txtLeadEmail.Text = GlobalVar.CurrentDealer.LeadEmail;

                cbFormat.SelectedIndex = QuickBookHelper.GetLeadFormats().ToList().FindIndex(x => x.LeadId == GlobalVar.CurrentDealer.EmailFormat);

                numericLimit.Value = GlobalVar.CurrentAccount.DailyLimit;

            }
        
      
        }

        private bool ValidateForm()
        {
            bool flag1 = String.IsNullOrEmpty(txtFirstName.Text.Trim());

            if(flag1)
                errorProvider1.SetError(txtFirstName, "Please enter your first name");
            else
            {
                errorProvider1.SetError(txtFirstName, "");
            }

            bool flag2 = String.IsNullOrEmpty(txtLastName.Text.Trim());

            if (flag2)
                errorProvider2.SetError(txtLastName, "Please enter your last name");
            else
            {
                errorProvider2.SetError(txtLastName, "");
            }

            bool flag3 = String.IsNullOrEmpty(txtCustomerEmail.Text.Trim());

            if (flag3)
                errorProvider3.SetError(txtCustomerEmail, "Please enter your email");
            else
            {
                errorProvider3.SetError(txtCustomerEmail, "");
            }


          
            bool flag6 = String.IsNullOrEmpty(txtCustomerPhone.Text.Trim());

            if (flag6)
                errorProvider4.SetError(txtCustomerPhone, "Please enter valid phone number");
            else
            {
                errorProvider4.SetError(txtCustomerPhone, "");
            }

            bool flag7 = String.IsNullOrEmpty(txtDealerName.Text.Trim());

            if (flag7)
                errorProvider5.SetError(txtDealerName, "Please enter dealership name");
            else
            {
                errorProvider5.SetError(txtDealerName, "");
            }

            bool flag8 = String.IsNullOrEmpty(txtWebSiteAddress.Text.Trim());

            if (flag8)
                errorProvider6.SetError(txtWebSiteAddress, "Please enter website url");
            else
            {
                errorProvider6.SetError(txtWebSiteAddress, "");
            }

            bool flag9 = String.IsNullOrEmpty(txtStreetAddress.Text.Trim());

            if (flag9)
                errorProvider7.SetError(txtStreetAddress, "Please enter dealership street address");
            else
            {
                errorProvider7.SetError(txtStreetAddress, "");
            }

            bool flag10 = String.IsNullOrEmpty(txtZipcode.Text.Trim());

            if (flag10)
                errorProvider8.SetError(txtZipcode, "Please enter zip code");
            else
            {
                errorProvider8.SetError(txtZipcode, "");
            }

            bool flag11= String.IsNullOrEmpty(txtCity.Text.Trim());

            if (flag11)
                errorProvider9.SetError(txtCity, "Please enter city");
            else
            {
                errorProvider9.SetError(txtCity, "");
            }

            bool flag12 = cbState.SelectedItem == null;

           

            bool flag15 = String.IsNullOrEmpty(txtDealerPhone.Text.Trim());

            errorProvider10.SetError(txtDealerPhone, flag15 ? "Please enter valid phone number" : "");

            bool flag16 = String.IsNullOrEmpty(txtLeadEmail.Text.Trim());

            errorProvider11.SetError(txtLeadEmail, flag16 ? "Please enter email to send leads out" : "");

            bool flag18 = !Int32.TryParse(numericLimit.Value.ToString(), out _dailyLimit);

            errorProvider12.SetError(numericLimit, flag18 ? "Please enter valid integer daily limit" : "");

            
            bool flag17 = !flag1 && !flag2 && !flag3  && !flag6 && !flag7 && !flag8 &&
                          !flag9 && !flag10 && !flag11 && !flag12 && !flag15 && !flag16 && !flag18;

            return flag17 != false && ValidateLogicForm();
            
        
        }

        private bool ValidateLogicForm()
        {
            bool flag = false;

            try
            {
                var customerEmail = new MailAddress(txtCustomerEmail.Text).Address;

                flag = true;
            }
            catch (FormatException)
            {

                errorProvider3.SetError(txtCustomerEmail, "Please enter a valid email");

                
            }

            
            return flag;
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                if (!String.IsNullOrEmpty(_mode) && _mode.Equals("Edit"))
                {
                    var selectedLeadFormat = (LeadFormat)cbFormat.SelectedItem;

                    var selectedState = (UsState)cbState.SelectedItem;

                    var newCustomer = new PostClCustomer
                    {
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        CustomerEmail = txtCustomerEmail.Text,
                        CustomerPhone = txtCustomerPhone.Text,
                        QuickBookAccountName = txtDealerName.Text + " - " + txtCustomerEmail.Text,
                        DealerName = txtDealerName.Text,
                        WebSiteAddress = txtWebSiteAddress.Text,
                        DealerStreetAddress = txtStreetAddress.Text,
                        DealerZipCode = txtZipcode.Text,
                        DealerCity = txtCity.Text,
                        DealerState = selectedState.StateAbbr,
                        DealerPhone = txtDealerPhone.Text,
                        LeadEmail = txtLeadEmail.Text,
                        LeadFormat = selectedLeadFormat.LeadId,
                        DailyLimit = _dailyLimit
                    };

                    DataHelper.UpdateDealerInfo(newCustomer);

                    DataHelper.InitializeGlobalDealerInfoVariable(GlobalVar.CurrentAccount.DealerId);

                    this.Close();
                }
                else
                {
                    if (!DataHelper.CheckAccountNameExist(txtCustomerEmail.Text))
                    {
                        btnOK.Enabled = false;

                        panelIndicator.Visible = true;

                        Refresh();

                        var selectedLeadFormat = (LeadFormat) cbFormat.SelectedItem;

                        var selectedState = (UsState) cbState.SelectedItem;

                        _newCustomer = new PostClCustomer
                            {
                                FirstName = txtFirstName.Text.Trim(),
                                LastName = txtLastName.Text.Trim(),
                                CustomerEmail = txtCustomerEmail.Text.Trim(),
                                CustomerPhone = txtCustomerPhone.Text,
                                QuickBookAccountName = txtDealerName.Text + " - " + txtCustomerEmail.Text,
                                DealerName = txtDealerName.Text.Trim(),
                                WebSiteAddress = txtWebSiteAddress.Text.Trim(),
                                DealerStreetAddress = txtStreetAddress.Text.Trim(),
                                DealerZipCode = txtZipcode.Text.Trim(),
                                DealerCity = txtCity.Text.Trim(),
                                DealerState = selectedState.StateAbbr,
                                DealerPhone = txtDealerPhone.Text,
                                LeadEmail = txtLeadEmail.Text.Trim(),
                                LeadFormat = selectedLeadFormat.LeadId,
                                DailyLimit = _dailyLimit
                            };




                        bgCustomerSignUp.RunWorkerAsync();


                    }
                    else
                    {
                        MessageBox.Show(
                            "This email is already in use. Please register with another email.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }

            }
            else
            {
                MessageBox.Show(
                   "Please fill correct info in required fields. ",
                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
        }

        private void bgCustomerSignUp_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            QuickBookHelper.AddCustomer(_newCustomer);

            EmailHelper.SendWelcomeEmail(new MailAddress(_newCustomer.CustomerEmail), _newCustomer);

            EmailHelper.SendConfirmationEmailForSignUp(_newCustomer);

        }

      
        private void bgCustomerSignUp_RunWorkerCompleted(object sender,
                                                         System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            panelIndicator.Visible = false;

            btnOK.Enabled = true;

            MessageBox.Show(
                "Thanks for signing up with POSTCL software. A confimation will be sent to your email shortly ",
                "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Refresh();

            this.Close();
        }


        private void txtZipcode_Leave_1(object sender, EventArgs e)
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

        private void txtCustomerEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
