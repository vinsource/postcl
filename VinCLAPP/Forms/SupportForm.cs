using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VinCLAPP.Helper;

namespace VinCLAPP.Forms
{
    public partial class SupportForm : Form
    {
        public SupportForm()
        {
            InitializeComponent();
        }

        private bool ValidateForm()
        {
            bool flag1 = String.IsNullOrEmpty(rtbCustomMessage.Text.Trim());

            errorProvider1.SetError(rtbCustomMessage, flag1 ? "Please give us more information about what current issues you are having" : "");

            var selectedvalue = (string)cbSubject.SelectedItem;
            
            var flag12 = selectedvalue.Equals("Select a subject....");

            var finalFlag = !flag1 && !flag12;

            return finalFlag;
        }

        private void SupportForm_Load(object sender, EventArgs e)
        {
            cbSubject.DataSource = GetSubjectList();

            cbSubject.SelectedIndex = 0;

        }

        private IEnumerable<string> GetSubjectList()
        {
            var returnList = new List<string> {"Select a subject....","Billing Issues", "Technical Issues", "Tutorial", "Other"};

            return returnList;

        }

        private void btnSubmitRequest_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                var selectedSubject = (string)cbSubject.SelectedItem;

                EmailHelper.SendSupportMail(selectedSubject,rtbCustomMessage.Text.Trim());
                MessageBox.Show("Hi " + GlobalVar.CurrentAccount.FirstName + " " + GlobalVar.CurrentAccount.LastName +
                                ".Thanks for reporting issues. We will contact you shortly.", "Info", MessageBoxButtons.OK,
                          MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show(
                 "Please fill correct info in required fields. ",
                 "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void linkVideo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("http://youtu.be/p7c5mfPMMP4");

            }
            catch (Exception)
            {
                
                
            }
           
        }
    }
}
