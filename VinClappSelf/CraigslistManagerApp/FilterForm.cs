using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CraigslistManagerApp.Helper;
using CraigslistManagerApp.Model;

namespace CraigslistManagerApp
{
    public partial class FilterForm : Form
    {
        public FilterForm()
        {
            InitializeComponent();
        }

        public FilterForm(Mainform frmMain)
        {
            

            InitializeComponent();
            this.frmMain = frmMain;

            int topPosition = 10;


            foreach (var tmp in clsVariables.fullMasterVehicleList.Select(x => x.DealerId).Distinct())
            {
                var checkBox = new CheckBox
                    {
                        Top = topPosition,
                        Left = 0,
                        Text = clsVariables.fullMasterVehicleList.First(x => x.DealerId == tmp).DealershipName,
                        Name = tmp.ToString(CultureInfo.InvariantCulture),
                        Size = new Size(148,20)
                    };

                topPosition += 30;

                PanelCheckbox.Controls.Add(checkBox);
                
            }

            nuSplit.Minimum = 1;

            nuSplit.Maximum = 30;

            this.ResumeLayout();
          
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (chkNoSplit.Checked)
            {
              
                var bufferList = new List<WhitmanEntepriseMasterVehicleInfo>();

                foreach (CheckBox c in PanelCheckbox.Controls)
                {
                    if (c.Checked)
                    {
                        int dealerId = Convert.ToInt32(c.Name);


                        bufferList.AddRange(
                            clsVariables.currentMasterVehicleList.Where(x => x.DealerId == dealerId));

                    }
                }

                clsVariables.currentMasterVehicleList = new List<WhitmanEntepriseMasterVehicleInfo>();

                foreach (
                    var tmp in bufferList.Where(x => x.CLPostingId > 0).OrderBy(x => x.CraigslistAccountName))
                {
                    tmp.AutoID = clsVariables.currentMasterVehicleList.Count + 1;

                    clsVariables.currentMasterVehicleList.Add(tmp);
                }


                foreach (var tmp in bufferList.Where(x => x.CLPostingId == 0))
                {
                    tmp.AutoID = clsVariables.currentMasterVehicleList.Count + 1;

                    clsVariables.currentMasterVehicleList.Add(tmp);
                }


                frmMain.UpdateListInventoryByComputerId();

                frmMain.lblPostingAds.Text = "Posting Ads = 0 / " +
                                             clsVariables.currentMasterVehicleList.Count(x => x.CLPostingId == 0);

                frmMain.lblRenewAds.Text = "Renewing Ads = 0 / " +
                                           clsVariables.currentMasterVehicleList.Count(x => x.CLPostingId > 0);

                frmMain.lblCount.Text = "LEFT = " + clsVariables.currentMasterVehicleList.Count;
            }
            else
            {
                int splitPart = Convert.ToInt32(nuSplit.Value);

                var bufferList = new List<WhitmanEntepriseMasterVehicleInfo>();

                foreach (CheckBox c in PanelCheckbox.Controls)
                {
                    if (c.Checked)
                    {
                        int dealerId = Convert.ToInt32(c.Name);


                        bufferList.AddRange(
                            clsVariables.currentMasterVehicleList.Where(x => x.DealerId == dealerId)
                                        .Split(30)
                                        .ElementAt(splitPart - 1));
                    }
                }

                clsVariables.currentMasterVehicleList = new List<WhitmanEntepriseMasterVehicleInfo>();

                foreach (
                    var tmp in bufferList.Where(x => x.CLPostingId > 0).OrderBy(x => x.CraigslistAccountName))
                {
                    tmp.AutoID = clsVariables.currentMasterVehicleList.Count + 1;

                    clsVariables.currentMasterVehicleList.Add(tmp);
                }


                foreach (var tmp in bufferList.Where(x => x.CLPostingId == 0))
                {
                    tmp.AutoID = clsVariables.currentMasterVehicleList.Count + 1;

                    clsVariables.currentMasterVehicleList.Add(tmp);
                }


                frmMain.UpdateListInventoryByComputerId();

                frmMain.lblPostingAds.Text = "Posting Ads = 0 / " +
                                             clsVariables.currentMasterVehicleList.Count(x => x.CLPostingId == 0);

                frmMain.lblRenewAds.Text = "Renewing Ads = 0 / " +
                                           clsVariables.currentMasterVehicleList.Count(x => x.CLPostingId > 0);

                frmMain.lblCount.Text = "LEFT = " + clsVariables.currentMasterVehicleList.Count;
            }



            Close();


        }

        private void chkNoSplit_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNoSplit.Checked)
            {
                nuSplit.Value = 1;

                nuSplit.Enabled = false;
            }
            else
            {
                nuSplit.Enabled = true;
            }

        }

        



    }
}
