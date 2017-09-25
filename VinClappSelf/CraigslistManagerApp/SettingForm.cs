using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CraigslistManagerApp
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
        }

        public SettingForm(Mainform frmMain)
        {
            InitializeComponent();
            this.frmMain = frmMain;
            nudInterval.Value = clsVariables.IntervalOfAds;

            if (clsVariables.DelayTimer == 150000)
                rb25.Checked = true;
            else if (clsVariables.DelayTimer == 360000)
                rb3.Checked = true;
            else if (clsVariables.DelayTimer == 300000)
                rb5.Checked = true;

            if (clsVariables.PostAll)
                cbAll.Checked = true;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            clsVariables.IntervalOfAds =(int)nudInterval.Value;
            if (cbAll.Checked)
            {
                clsVariables.PostAll = true;
                this.Close();
            }
            else
            {
                
                if (clsVariables.IntervalOfAds <= 0)
                {
                    errorProvider1.SetError(nudInterval, "Number must be positive");
                }
                else
                {
                    if (rb25.Checked)
                        clsVariables.DelayTimer = 150000;
                    else if (rb3.Checked)
                        clsVariables.DelayTimer = 360000;
                    else if (rb5.Checked)
                        clsVariables.DelayTimer = 300000;

                    clsVariables.PostAll = false;

                    this.Close();
                }
            }
            
         
        }

        private void nudInterval_ValueChanged(object sender, EventArgs e)
        {
            int currentValue = (int)nudInterval.Value;

            if (currentValue != 0)
                cbAll.Checked = false;
            else
                cbAll.Checked = true;
        }

      
    }
}
