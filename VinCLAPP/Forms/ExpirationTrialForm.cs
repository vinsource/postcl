using System;
using System.Windows.Forms;

namespace VinCLAPP.Forms
{
    public partial class ExpirationTrialForm : Form
    {
        private readonly MainForm _frmMain;
        public ExpirationTrialForm()
        {
            InitializeComponent();
        }

        public ExpirationTrialForm(MainForm frmMain)
        {
            InitializeComponent();
            this._frmMain = frmMain;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            var sForm = CreditCardAuthorizeForm.Instance(_frmMain);

            sForm.Show();

            sForm.Activate();
        }
    }
}
