using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using VinCLAPP.DatabaseModel;
using VinCLAPP.Helper;
using VinCLAPP.Model;

namespace VinCLAPP
{
    public partial class CraigsListAccountForm : Form
    {
        public CraigsListAccountForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CraigsListAccountForm_Load(object sender, EventArgs e)
        {
            DataHelper.InitializeGlobalEmailAccountVariable(GlobalVar.CurrentAccount.AccountId);
            EmailGridView.CellValidating += new DataGridViewCellValidatingEventHandler(EmailGridView_CellValidating);
            EmailGridView.AutoGenerateColumns = false;
            EmailGridView.CellClick += new DataGridViewCellEventHandler(EmailGridView_CellClick);
            EmailGridView.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(EmailGridView_DataBindingComplete);
            EmailGridView.DataSource = GlobalVar.CurrentDealer.EmailAccountList.Count > 0 ? new BindingList<EmailAccount>(GlobalVar.CurrentDealer.EmailAccountList.ToList()) : new BindingList<EmailAccount>();
        }

        void EmailGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if ( EmailGridView.IsCurrentCellInEditMode && e.ColumnIndex == EmailGridView.Columns["EmailAccount"].Index && e.RowIndex == EmailGridView.CurrentCell.RowIndex )
            {
                var email = EmailGridView[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToString();
                if (!CommonHelper.EmailTests(email))
                {
                    MessageBox.Show(string.Format("{0} is not a valid email", email));
                }
            }        
        }

       

        void EmailGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (EmailGridView.Columns["Delete"] != null)
            {
                var indexDeleteColumn = EmailGridView.Columns["Delete"].Index;
                if (EmailGridView.Rows.Count >= 1)
                    ((DataGridViewImageCell)EmailGridView.Rows[0].Cells[indexDeleteColumn]).Value = new System.Drawing.Bitmap(1, 1);
                if (EmailGridView.Rows.Count >= 2)
                    ((DataGridViewImageCell)EmailGridView.Rows[1].Cells[indexDeleteColumn]).Value = new System.Drawing.Bitmap(1, 1);
            }
        }

        void EmailGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (EmailGridView.Columns["Delete"] == null || (e.ColumnIndex != EmailGridView.Columns["Delete"].Index) || e.RowIndex == 0 || e.RowIndex == 1) return;
            var list = (BindingList<EmailAccount>)EmailGridView.DataSource;
            list.RemoveAt(e.RowIndex);
            // Ignore clicks that are not on button cells.  
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var list = (BindingList<EmailAccount>)EmailGridView.DataSource;
            if (ValidateRequiredFields(list.ToList()))
            {
                DataHelper.DeleteAllEmailAccount(GlobalVar.CurrentAccount.AccountId);
                List<Email> finalEmailList =
                    list.Where(
                        item =>
                        !CheckEmtyString(item.CraigslistAccount) || !CheckEmtyString(item.CraigsAccountPhoneNumber) ||
                        !CheckEmtyString(item.CraigsListPassword)).Select(i => new Email()
                            {
                                EmailAddress = i.CraigslistAccount,
                                EmailPassword = i.CraigsListPassword,
                                PhoneNumber = i.CraigsAccountPhoneNumber,
                                AccountAutoId = GlobalVar.CurrentAccount.AccountId,
                                DateAdded = DateTime.Now,
                                DealerId = GlobalVar.CurrentDealer.DealerId
                            }).ToList();
                DataHelper.AddListEmailAccount(finalEmailList);
                DataHelper.InitializeGlobalEmailAccountVariable(GlobalVar.CurrentAccount.AccountId);
                Close();
            }
        }

        private bool ValidateRequiredFields(List<EmailAccount> list)
        {
            bool emptyFieldExisted = list == null || list.Count < 1 || CheckEmtyString(list[0].CraigslistAccount) ||
                                     CheckEmtyString(list[0].CraigsAccountPhoneNumber) ||
                                     CheckEmtyString(list[0].CraigsListPassword) ;
            if (emptyFieldExisted)
            {
                MessageBox.Show("You must fill all the blank fields for at least one account.", "Critical Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                errorProvider4.SetError(lblFillUp,
                                        "Please fill all blank fields for at least one account.");
                return false;
            }

            return true;

            //bool returnValue = true;
            //string listAccount = String.Empty;
            //for (int i = 2; i < list.Count; i++)
            //{
            //    bool allFieldFilledIn = CheckEmtyString(list[i].CraigslistAccount) &&
            //                            CheckEmtyString(list[i].CraigsAccountPhoneNumber) &&
            //                            CheckEmtyString(list[i].CraigsListPassword);
            //    bool noFieldFilledIn = !CheckEmtyString(list[i].CraigslistAccount) &&
            //                           !CheckEmtyString(list[i].CraigsAccountPhoneNumber) &&
            //                           !CheckEmtyString(list[i].CraigsListPassword);
            //    if (!allFieldFilledIn && !noFieldFilledIn)
            //    {
            //        returnValue = false;
            //        listAccount += (i + 1) + " ";
            //    }
            //}

            //if (!returnValue)
            //{
            //    MessageBox.Show("You must fill all the blank fields for account(s) " + listAccount, "Critical Error",
            //                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //return returnValue;
        }

        private static bool CheckEmtyString(string item)
        {
            if (String.IsNullOrEmpty(item)) return true;
            return item.Trim().Equals(String.Empty);
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            var list = (BindingList<EmailAccount>)EmailGridView.DataSource;
            list.Add(new EmailAccount());
        }

      
    }
}