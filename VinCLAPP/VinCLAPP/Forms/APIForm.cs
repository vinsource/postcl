using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VinCLAPP.DatabaseModel;

namespace VinCLAPP.Forms
{
    public partial class APIForm : Form
    {
            private readonly MainForm _frmMain;
        public APIForm()
        {
            InitializeComponent();
        }

        public APIForm(MainForm frmMain)
        {
            InitializeComponent();
            this._frmMain = frmMain;

        }

        private void btnSubmitRequest_Click(object sender, EventArgs e)
        {
            var context = new CLDMSEntities();

            var account = context.Accounts.First(o => o.AccountId == GlobalVar.CurrentAccount.AccountId);

            account.APIUsername = String.IsNullOrEmpty(txtUserName.Text) ? "" : txtUserName.Text.Trim();

            account.APIPassword = String.IsNullOrEmpty(txtPassword.Text) ? "" : txtPassword.Text.Trim();

            account.APIAccountId = String.IsNullOrEmpty(txtAccountId.Text) ? "" : txtAccountId.Text.Trim();
            
            account.LastUpdated = DateTime.Now;

            context.SaveChanges();

            GlobalVar.CurrentAccount.APIUsername = account.APIUsername;

            GlobalVar.CurrentAccount.APIPassword = account.APIPassword;

            GlobalVar.CurrentAccount.APIAccountId = account.APIAccountId;

            this.Close();
            
        }

        private void APIForm_Load(object sender, EventArgs e)
        {
            txtUserName.Text = GlobalVar.CurrentAccount.APIUsername;

            txtAccountId.Text = GlobalVar.CurrentAccount.APIAccountId;

            txtPassword.Text = GlobalVar.CurrentAccount.APIPassword;
        }
    }
}
