using System;
using System.Linq;
using System.Windows.Forms;
using VinCLAPP.DatabaseModel;

namespace VinCLAPP
{
    public partial class ChangePassForm : Form
    {
        private readonly MainForm frmMain;

        public ChangePassForm()
        {
            InitializeComponent();
        }

        public ChangePassForm(MainForm frmMain)
        {
            InitializeComponent();
            this.frmMain = frmMain;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateEmpty() && ValidateSamePassword())
            {
                var context = new CLDMSEntities();

                var account =
                    context.Accounts.FirstOrDefault(x => x.AccountId == GlobalVar.CurrentAccount.AccountId);

                account.AccountPassword = txtNewPass.Text;

                account.LastUpdated = DateTime.Now;

                account.IsFirstLogOn = false;

                account.Active = true;

                context.SaveChanges();

                frmMain.txtPassword.Text = txtNewPass.Text;

                Close();

                var cForm = CreditCardAuthorizeForm.Instance(frmMain);

                cForm.Show();

                cForm.Activate();

              
            }
        }

        private bool ValidateEmpty()
        {
            bool case1 = String.IsNullOrEmpty(txtNewPass.Text.Trim());

            bool case2 = String.IsNullOrEmpty(txtRetypePass.Text.Trim());

            bool finalFlag = true;

            if (case1 && case2)
            {
                lblEmptyError.Visible = true;
                errorProvider1.SetError(lblEmptyError,
                                        "Required field.");
                finalFlag = false;
            }
            else
            {
                lblEmptyError.Visible = false;
                errorProvider1.SetError(lblEmptyError,
                                        "");
            }

            return finalFlag;
        }


        private bool ValidateSamePassword()
        {
            bool finalFlag = true;

            if (!txtNewPass.Text.Equals(txtRetypePass.Text))
            {
                lblEmptyError.Text = "Passwords are not match";
                lblEmptyError.Visible = true;
                errorProvider2.SetError(lblEmptyError,
                                        "Required field.");
                finalFlag = false;
            }
            else
            {
                lblEmptyError.Visible = false;
                errorProvider2.SetError(lblEmptyError,
                                        "");
            }
            return finalFlag;
        }
    }
}