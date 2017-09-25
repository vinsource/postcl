using System;
using System.ComponentModel;
using System.Windows.Forms;
using ClappManagementSystem.Helper;

namespace ClappManagementSystem.Forms
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            MySqlHelper.InitializeSettings();
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            bgWorker.RunWorkerAsync();
        }
    }
}
