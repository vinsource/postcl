using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using VinCLAPP.Helper;
using VinCLAPP.Model;

namespace VinCLAPP.Forms
{
    public partial class DataFeedForm : Form
    {
        private PostClDataFeed _postClDataFeed;
        public DataFeedForm()
        {
            InitializeComponent();
        }

        private bool ValidateLogicForm()
        {
            bool flag = false;

            try
            {
                var customerEmail = new MailAddress(txtVendorEmail.Text).Address;

                flag = true;
            }
            catch (FormatException)
            {

                errorProvider3.SetError(txtVendorEmail, "Please enter a valid email");


            }


            return flag;
        }

        private void btnSubmitRequest_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                if (cbAuthorize.Checked)
                {

                    btnSubmitRequest.Enabled = false;

                    panelIndicator.Visible = true;

                    Refresh();
                    _postClDataFeed = new PostClDataFeed()
                        {
                            CustomMessage = rtbCustomMessage.Text.Trim(),
                            VendorEmail = txtVendorEmail.Text.Trim(),
                            VendorName = cbVendorList.SelectedText,
                            VendorPhone = txtVendorPhone.Text.Trim(),
                            YourName = txtYourName.Text.Trim(),
                        };

                    if (cbVendorList.SelectedText.Equals("Other"))
                        _postClDataFeed.VendorName = txtOtherVendor.Text.Trim();
                    bgDataFeed.RunWorkerAsync();
                   
                }
                else
                {
                    MessageBox.Show(
                        "You have to authorize us to process the feed on your behalf.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            else
            {
                MessageBox.Show(
                    "Please fill correct info in required fields. ",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void bgDataFeed_DoWork(object sender, DoWorkEventArgs e)
        {
         
            DataHelper.UpdateDataFeedSetUp(_postClDataFeed);

            EmailHelper.SendConfirmationEmailForDataFeedSetup(_postClDataFeed);

            EmailHelper.SendConfirmationEmailForPaymentToVendor(_postClDataFeed);

            this.Close();
        }

        private void bgDataFeed_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            panelIndicator.Visible = false;
            this.Close();
        }


        private void DataFeedForm_Load(object sender, EventArgs e)
        {
            cbVendorList.DataSource = DataHelper.GetDataFeedPackage();

        }

        private bool ValidateForm()
        {
            bool flag1 = txtOtherVendor.ReadOnly == false && String.IsNullOrEmpty(txtOtherVendor.Text.Trim());

            errorProvider1.SetError(txtOtherVendor, flag1 ? "Please enter the vendor for us to contact" : "");

            bool flag2 = String.IsNullOrEmpty(txtVendorEmail.Text.Trim());

            errorProvider2.SetError(txtVendorEmail, flag2 ? "Please enter the vendor email" : "");

            bool flag3 = String.IsNullOrEmpty(txtVendorPhone.Text.Trim());

            errorProvider3.SetError(txtVendorPhone, flag3 ? "Please enter the vendor phone" : "");

            bool flag4 = String.IsNullOrEmpty(txtYourName.Text.Trim());

            errorProvider4.SetError(txtYourName, flag4 ? "Please enter your name" : "");


            bool finalFlag = !flag1 && !flag2 && !flag3 && !flag4;

            if (finalFlag)
            {
                finalFlag = ValidateLogicForm();
            }
            return finalFlag;

        }

        private void cbVendorList_SelectedIndexChanged(object sender, EventArgs e)
        {


            var selectedVendor = (string) cbVendorList.SelectedItem;

            txtOtherVendor.ReadOnly = !selectedVendor.Equals("Other");

            txtOtherVendor.Text = "";
        }

      


    }
}
