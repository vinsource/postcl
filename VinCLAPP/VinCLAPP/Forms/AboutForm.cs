using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VinCLAPP.Forms
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                Version myVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion;

                lblVersion.Text = string.Format("VERSION: v{0}.{1}.{2}.{3}", myVersion.Major, myVersion.Minor,
                                                myVersion.Build, myVersion.Revision);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
