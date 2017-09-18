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

namespace VinCLAPP.Forms
{
    public partial class EmailForgetPassowrd : Form
    {
        public EmailForgetPassowrd()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();

            var flag1 = String.IsNullOrEmpty(email);
            
            if (flag1)
                errorProvider1.SetError(txtEmail, "Please enter the valid email you signed up");
            else
            {
                try
                {
                    var customerEmail = new MailAddress(email).Address;

                    errorProvider1.SetError(txtEmail, "");
                    
                    if (DataHelper.CheckAccountNameExist(email))
                    {
                        var postClCustomer = DataHelper.ChangeTemporayPassword(email);

                        EmailHelper.SendChangePasswordEmail(new MailAddress(postClCustomer.CustomerEmail), postClCustomer);

                        MessageBox.Show(
                            " A password recovery will be sent to your email shortly. Please add our email system address to your inbox to prevent spam.",
                            "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }

                    else
                    {
                        MessageBox.Show(
                              "Sorry this email doesn't exist in our system. Please enter the email you signed up for POSTCL program. ",
                              "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                catch (Exception)
                {
                    errorProvider1.SetError(txtEmail, "Please enter the valid email you signed up");
                }

            

              
            }
            
            
        }

        private void EmailForgetPassowrd_Load(object sender, EventArgs e)
        {
            txtEmail.Focus();
        }
    }
}
