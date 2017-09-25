using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CraigslistManagerApp.Model;
using Microsoft.Win32;

namespace CraigslistManagerApp
{
    public partial class CraigsListAccountForm : Form
    {
        public CraigsListAccountForm()
        {
            InitializeComponent();
        }

        public CraigsListAccountForm(Mainform frmMain)
        {
            InitializeComponent();
            this.frmMain = frmMain;
        }


        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            dialog.Filter ="xml files (*.xml)|*.xml";

            dialog.InitialDirectory = path;
            
            dialog.Title = "Select craiglist account file";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtSource.Text = dialog.FileName;

                var xmlDoc = new XmlDocument();

                xmlDoc.Load(txtSource.Text);

                XmlNodeList accountNodes = xmlDoc.SelectNodes("//Account");

                if (accountNodes.Count <= 4)
                {
                    
                    txtAcc1.Text = accountNodes[0].Attributes["CraigslistAccount"].Value;

                    txtPass1.Text = accountNodes[0].Attributes["CraigsListPassword"].Value;

                    txtPhone1.Text = accountNodes[0].Attributes["PhoneNumber"].Value;

                    txtProxy1.Text = accountNodes[0].Attributes["Proxy"].Value;

                    txtAcc2.Text = accountNodes[1].Attributes["CraigslistAccount"].Value;

                    txtPass2.Text = accountNodes[1].Attributes["CraigsListPassword"].Value;

                    txtPhone2.Text = accountNodes[1].Attributes["PhoneNumber"].Value;

                    txtProxy2.Text = accountNodes[1].Attributes["Proxy"].Value;

                    txtAcc3.Text = accountNodes[2].Attributes["CraigslistAccount"].Value;

                    txtPass3.Text = accountNodes[2].Attributes["CraigsListPassword"].Value;

                    txtPhone3.Text = accountNodes[2].Attributes["PhoneNumber"].Value;

                    txtProxy3.Text = accountNodes[2].Attributes["Proxy"].Value;

                    txtAcc4.Text = accountNodes[3].Attributes["CraigslistAccount"].Value;

                    txtPass4.Text = accountNodes[3].Attributes["CraigsListPassword"].Value;

                    txtPhone4.Text = accountNodes[3].Attributes["PhoneNumber"].Value;

                    txtProxy4.Text = accountNodes[3].Attributes["Proxy"].Value;

                }
                else if (accountNodes.Count == 5)
                {

                    txtAcc1.Text = accountNodes[0].Attributes["CraigslistAccount"].Value;

                    txtPass1.Text = accountNodes[0].Attributes["CraigsListPassword"].Value;

                    txtPhone1.Text = accountNodes[0].Attributes["PhoneNumber"].Value;

                    txtProxy1.Text = accountNodes[0].Attributes["Proxy"].Value;

                    txtAcc2.Text = accountNodes[1].Attributes["CraigslistAccount"].Value;

                    txtPass2.Text = accountNodes[1].Attributes["CraigsListPassword"].Value;

                    txtPhone2.Text = accountNodes[1].Attributes["PhoneNumber"].Value;

                    txtProxy2.Text = accountNodes[1].Attributes["Proxy"].Value;

                    txtAcc3.Text = accountNodes[2].Attributes["CraigslistAccount"].Value;

                    txtPass3.Text = accountNodes[2].Attributes["CraigsListPassword"].Value;

                    txtPhone3.Text = accountNodes[2].Attributes["PhoneNumber"].Value;

                    txtProxy3.Text = accountNodes[2].Attributes["Proxy"].Value;

                    txtAcc4.Text = accountNodes[3].Attributes["CraigslistAccount"].Value;

                    txtPass4.Text = accountNodes[3].Attributes["CraigsListPassword"].Value;

                    txtPhone4.Text = accountNodes[3].Attributes["PhoneNumber"].Value;

                    txtProxy4.Text = accountNodes[3].Attributes["Proxy"].Value;

                    txtAcc5.Text = accountNodes[4].Attributes["CraigslistAccount"].Value;

                    txtPass5.Text = accountNodes[4].Attributes["CraigsListPassword"].Value;

                    txtPhone5.Text = accountNodes[4].Attributes["PhoneNumber"].Value;

                    txtProxy5.Text = accountNodes[4].Attributes["Proxy"].Value;

                }
                else if (accountNodes.Count == 6)
                {
                    txtAcc1.Text = accountNodes[0].Attributes["CraigslistAccount"].Value;

                    txtPass1.Text = accountNodes[0].Attributes["CraigsListPassword"].Value;

                    txtPhone1.Text = accountNodes[0].Attributes["PhoneNumber"].Value;

                    txtProxy1.Text = accountNodes[0].Attributes["Proxy"].Value;

                    txtAcc2.Text = accountNodes[1].Attributes["CraigslistAccount"].Value;

                    txtPass2.Text = accountNodes[1].Attributes["CraigsListPassword"].Value;

                    txtPhone2.Text = accountNodes[1].Attributes["PhoneNumber"].Value;

                    txtProxy2.Text = accountNodes[1].Attributes["Proxy"].Value;

                    txtAcc3.Text = accountNodes[2].Attributes["CraigslistAccount"].Value;

                    txtPass3.Text = accountNodes[2].Attributes["CraigsListPassword"].Value;

                    txtPhone3.Text = accountNodes[2].Attributes["PhoneNumber"].Value;

                    txtProxy3.Text = accountNodes[2].Attributes["Proxy"].Value;

                    txtAcc4.Text = accountNodes[3].Attributes["CraigslistAccount"].Value;

                    txtPass4.Text = accountNodes[3].Attributes["CraigsListPassword"].Value;

                    txtPhone4.Text = accountNodes[3].Attributes["PhoneNumber"].Value;

                    txtProxy4.Text = accountNodes[3].Attributes["Proxy"].Value;

                    txtAcc5.Text = accountNodes[4].Attributes["CraigslistAccount"].Value;

                    txtPass5.Text = accountNodes[4].Attributes["CraigsListPassword"].Value;

                    txtPhone5.Text = accountNodes[4].Attributes["PhoneNumber"].Value;

                    txtProxy5.Text = accountNodes[4].Attributes["Proxy"].Value;

                    txtAcc6.Text = accountNodes[5].Attributes["CraigslistAccount"].Value;

                    txtPass6.Text = accountNodes[5].Attributes["CraigsListPassword"].Value;

                    txtPhone6.Text = accountNodes[5].Attributes["PhoneNumber"].Value;

                    txtProxy6.Text = accountNodes[5].Attributes["Proxy"].Value;
                }
                else if (accountNodes.Count == 7)
                {
                    txtAcc1.Text = accountNodes[0].Attributes["CraigslistAccount"].Value;

                    txtPass1.Text = accountNodes[0].Attributes["CraigsListPassword"].Value;

                    txtPhone1.Text = accountNodes[0].Attributes["PhoneNumber"].Value;

                    txtProxy1.Text = accountNodes[0].Attributes["Proxy"].Value;

                    txtAcc2.Text = accountNodes[1].Attributes["CraigslistAccount"].Value;

                    txtPass2.Text = accountNodes[1].Attributes["CraigsListPassword"].Value;

                    txtPhone2.Text = accountNodes[1].Attributes["PhoneNumber"].Value;

                    txtProxy2.Text = accountNodes[1].Attributes["Proxy"].Value;

                    txtAcc3.Text = accountNodes[2].Attributes["CraigslistAccount"].Value;

                    txtPass3.Text = accountNodes[2].Attributes["CraigsListPassword"].Value;

                    txtPhone3.Text = accountNodes[2].Attributes["PhoneNumber"].Value;

                    txtProxy3.Text = accountNodes[2].Attributes["Proxy"].Value;

                    txtAcc4.Text = accountNodes[3].Attributes["CraigslistAccount"].Value;

                    txtPass4.Text = accountNodes[3].Attributes["CraigsListPassword"].Value;

                    txtPhone4.Text = accountNodes[3].Attributes["PhoneNumber"].Value;

                    txtProxy4.Text = accountNodes[3].Attributes["Proxy"].Value;

                    txtAcc5.Text = accountNodes[4].Attributes["CraigslistAccount"].Value;

                    txtPass5.Text = accountNodes[4].Attributes["CraigsListPassword"].Value;

                    txtPhone5.Text = accountNodes[4].Attributes["PhoneNumber"].Value;

                    txtProxy5.Text = accountNodes[4].Attributes["Proxy"].Value;

                    txtAcc6.Text = accountNodes[5].Attributes["CraigslistAccount"].Value;

                    txtPass6.Text = accountNodes[5].Attributes["CraigsListPassword"].Value;

                    txtPhone6.Text = accountNodes[5].Attributes["PhoneNumber"].Value;

                    txtProxy6.Text = accountNodes[5].Attributes["Proxy"].Value;

                    txtAcc7.Text = accountNodes[6].Attributes["CraigslistAccount"].Value;

                    txtPass7.Text = accountNodes[6].Attributes["CraigsListPassword"].Value;

                    txtPhone7.Text = accountNodes[6].Attributes["PhoneNumber"].Value;

                    txtProxy7.Text = accountNodes[6].Attributes["Proxy"].Value;
                }
                else if (accountNodes.Count == 8)
                {
                    txtAcc1.Text = accountNodes[0].Attributes["CraigslistAccount"].Value;

                    txtPass1.Text = accountNodes[0].Attributes["CraigsListPassword"].Value;

                    txtPhone1.Text = accountNodes[0].Attributes["PhoneNumber"].Value;

                    txtProxy1.Text = accountNodes[0].Attributes["Proxy"].Value;

                    txtAcc2.Text = accountNodes[1].Attributes["CraigslistAccount"].Value;

                    txtPass2.Text = accountNodes[1].Attributes["CraigsListPassword"].Value;

                    txtPhone2.Text = accountNodes[1].Attributes["PhoneNumber"].Value;

                    txtProxy2.Text = accountNodes[1].Attributes["Proxy"].Value;

                    txtAcc3.Text = accountNodes[2].Attributes["CraigslistAccount"].Value;

                    txtPass3.Text = accountNodes[2].Attributes["CraigsListPassword"].Value;

                    txtPhone3.Text = accountNodes[2].Attributes["PhoneNumber"].Value;

                    txtProxy3.Text = accountNodes[2].Attributes["Proxy"].Value;

                    txtAcc4.Text = accountNodes[3].Attributes["CraigslistAccount"].Value;

                    txtPass4.Text = accountNodes[3].Attributes["CraigsListPassword"].Value;

                    txtPhone4.Text = accountNodes[3].Attributes["PhoneNumber"].Value;

                    txtProxy4.Text = accountNodes[3].Attributes["Proxy"].Value;

                    txtAcc5.Text = accountNodes[4].Attributes["CraigslistAccount"].Value;

                    txtPass5.Text = accountNodes[4].Attributes["CraigsListPassword"].Value;

                    txtPhone5.Text = accountNodes[4].Attributes["PhoneNumber"].Value;

                    txtProxy5.Text = accountNodes[4].Attributes["Proxy"].Value;

                    txtAcc6.Text = accountNodes[5].Attributes["CraigslistAccount"].Value;

                    txtPass6.Text = accountNodes[5].Attributes["CraigsListPassword"].Value;

                    txtPhone6.Text = accountNodes[5].Attributes["PhoneNumber"].Value;

                    txtProxy6.Text = accountNodes[5].Attributes["Proxy"].Value;

                    txtAcc7.Text = accountNodes[6].Attributes["CraigslistAccount"].Value;

                    txtPass7.Text = accountNodes[6].Attributes["CraigsListPassword"].Value;

                    txtPhone7.Text = accountNodes[6].Attributes["PhoneNumber"].Value;

                    txtProxy7.Text = accountNodes[6].Attributes["Proxy"].Value;

                    txtAcct8.Text = accountNodes[7].Attributes["CraigslistAccount"].Value;

                    txtPass8.Text = accountNodes[7].Attributes["CraigsListPassword"].Value;

                    txtPhone8.Text = accountNodes[7].Attributes["PhoneNumber"].Value;

                    txtProxy8.Text = accountNodes[7].Attributes["Proxy"].Value;
                }
                else if (accountNodes.Count == 9)
                {
                    txtAcc1.Text = accountNodes[0].Attributes["CraigslistAccount"].Value;

                    txtPass1.Text = accountNodes[0].Attributes["CraigsListPassword"].Value;

                    txtPhone1.Text = accountNodes[0].Attributes["PhoneNumber"].Value;

                    txtProxy1.Text = accountNodes[0].Attributes["Proxy"].Value;

                    txtAcc2.Text = accountNodes[1].Attributes["CraigslistAccount"].Value;

                    txtPass2.Text = accountNodes[1].Attributes["CraigsListPassword"].Value;

                    txtPhone2.Text = accountNodes[1].Attributes["PhoneNumber"].Value;

                    txtProxy2.Text = accountNodes[1].Attributes["Proxy"].Value;

                    txtAcc3.Text = accountNodes[2].Attributes["CraigslistAccount"].Value;

                    txtPass3.Text = accountNodes[2].Attributes["CraigsListPassword"].Value;

                    txtPhone3.Text = accountNodes[2].Attributes["PhoneNumber"].Value;

                    txtProxy3.Text = accountNodes[2].Attributes["Proxy"].Value;

                    txtAcc4.Text = accountNodes[3].Attributes["CraigslistAccount"].Value;

                    txtPass4.Text = accountNodes[3].Attributes["CraigsListPassword"].Value;

                    txtPhone4.Text = accountNodes[3].Attributes["PhoneNumber"].Value;

                    txtProxy4.Text = accountNodes[3].Attributes["Proxy"].Value;

                    txtAcc5.Text = accountNodes[4].Attributes["CraigslistAccount"].Value;

                    txtPass5.Text = accountNodes[4].Attributes["CraigsListPassword"].Value;

                    txtPhone5.Text = accountNodes[4].Attributes["PhoneNumber"].Value;

                    txtProxy5.Text = accountNodes[4].Attributes["Proxy"].Value;

                    txtAcc6.Text = accountNodes[5].Attributes["CraigslistAccount"].Value;

                    txtPass6.Text = accountNodes[5].Attributes["CraigsListPassword"].Value;

                    txtPhone6.Text = accountNodes[5].Attributes["PhoneNumber"].Value;

                    txtProxy6.Text = accountNodes[5].Attributes["Proxy"].Value;

                    txtAcc7.Text = accountNodes[6].Attributes["CraigslistAccount"].Value;

                    txtPass7.Text = accountNodes[6].Attributes["CraigsListPassword"].Value;

                    txtPhone7.Text = accountNodes[6].Attributes["PhoneNumber"].Value;

                    txtProxy7.Text = accountNodes[6].Attributes["Proxy"].Value;

                    txtAcct8.Text = accountNodes[7].Attributes["CraigslistAccount"].Value;

                    txtPass8.Text = accountNodes[7].Attributes["CraigsListPassword"].Value;

                    txtPhone8.Text = accountNodes[7].Attributes["PhoneNumber"].Value;

                    txtProxy8.Text = accountNodes[7].Attributes["Proxy"].Value;

                    txtAcct9.Text = accountNodes[8].Attributes["CraigslistAccount"].Value;

                    txtPass9.Text = accountNodes[8].Attributes["CraigsListPassword"].Value;

                    txtPhone9.Text = accountNodes[8].Attributes["PhoneNumber"].Value;

                    txtProxy9.Text = accountNodes[8].Attributes["Proxy"].Value;
                }
                else
                {
                    txtAcc1.Text = accountNodes[0].Attributes["CraigslistAccount"].Value;

                    txtPass1.Text = accountNodes[0].Attributes["CraigsListPassword"].Value;

                    txtPhone1.Text = accountNodes[0].Attributes["PhoneNumber"].Value;

                    txtProxy1.Text = accountNodes[0].Attributes["Proxy"].Value;

                    txtAcc2.Text = accountNodes[1].Attributes["CraigslistAccount"].Value;

                    txtPass2.Text = accountNodes[1].Attributes["CraigsListPassword"].Value;

                    txtPhone2.Text = accountNodes[1].Attributes["PhoneNumber"].Value;

                    txtProxy2.Text = accountNodes[1].Attributes["Proxy"].Value;

                    txtAcc3.Text = accountNodes[2].Attributes["CraigslistAccount"].Value;

                    txtPass3.Text = accountNodes[2].Attributes["CraigsListPassword"].Value;

                    txtPhone3.Text = accountNodes[2].Attributes["PhoneNumber"].Value;

                    txtProxy3.Text = accountNodes[2].Attributes["Proxy"].Value;

                    txtAcc4.Text = accountNodes[3].Attributes["CraigslistAccount"].Value;

                    txtPass4.Text = accountNodes[3].Attributes["CraigsListPassword"].Value;

                    txtPhone4.Text = accountNodes[3].Attributes["PhoneNumber"].Value;

                    txtProxy4.Text = accountNodes[3].Attributes["Proxy"].Value;

                    txtAcc5.Text = accountNodes[4].Attributes["CraigslistAccount"].Value;

                    txtPass5.Text = accountNodes[4].Attributes["CraigsListPassword"].Value;

                    txtPhone5.Text = accountNodes[4].Attributes["PhoneNumber"].Value;

                    txtProxy5.Text = accountNodes[4].Attributes["Proxy"].Value;

                    txtAcc6.Text = accountNodes[5].Attributes["CraigslistAccount"].Value;

                    txtPass6.Text = accountNodes[5].Attributes["CraigsListPassword"].Value;

                    txtPhone6.Text = accountNodes[5].Attributes["PhoneNumber"].Value;

                    txtProxy6.Text = accountNodes[5].Attributes["Proxy"].Value;

                    txtAcc7.Text = accountNodes[6].Attributes["CraigslistAccount"].Value;

                    txtPass7.Text = accountNodes[6].Attributes["CraigsListPassword"].Value;

                    txtPhone7.Text = accountNodes[6].Attributes["PhoneNumber"].Value;

                    txtProxy7.Text = accountNodes[6].Attributes["Proxy"].Value;

                    txtAcct8.Text = accountNodes[7].Attributes["CraigslistAccount"].Value;

                    txtPass8.Text = accountNodes[7].Attributes["CraigsListPassword"].Value;

                    txtPhone8.Text = accountNodes[7].Attributes["PhoneNumber"].Value;

                    txtProxy8.Text = accountNodes[7].Attributes["Proxy"].Value;

                    txtAcct9.Text = accountNodes[8].Attributes["CraigslistAccount"].Value;

                    txtPass9.Text = accountNodes[8].Attributes["CraigsListPassword"].Value;

                    txtPhone9.Text = accountNodes[8].Attributes["PhoneNumber"].Value;

                    txtProxy9.Text = accountNodes[8].Attributes["Proxy"].Value;

                    txtAcct10.Text = accountNodes[9].Attributes["CraigslistAccount"].Value;

                    txtPass10.Text = accountNodes[9].Attributes["CraigsListPassword"].Value;

                    txtPhone10.Text = accountNodes[9].Attributes["PhoneNumber"].Value;

                    txtProxy10.Text = accountNodes[9].Attributes["Proxy"].Value;
                }
                btnOk.Enabled = true;

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool flag = String.IsNullOrEmpty(txtAcc1.Text.Trim()) || String.IsNullOrEmpty(txtPass1.Text.Trim()) || String.IsNullOrEmpty(txtAcc2.Text.Trim())
                || String.IsNullOrEmpty(txtPass2.Text.Trim()) ;
                        
            if (flag)
                System.Windows.Forms.MessageBox.Show("Fill all the blank fields for the first two accounts", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            else
            {
                pbLoabPic.Visible = true;

                btnOk.Enabled = false;

                this.Refresh();

                bgWorker.RunWorkerAsync();

             

            }

        }

     
     
        private void updateCraigsList()
        {
           
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(txtSource.Text);

            XmlNodeList accountNodes = xmlDoc.SelectNodes("//Account");

            
            if (accountNodes.Count == 10)
            {

                accountNodes[0].Attributes["CraigslistAccount"].Value = txtAcc1.Text.Trim();

                accountNodes[0].Attributes["CraigsListPassword"].Value = txtPass1.Text.Trim();

                accountNodes[0].Attributes["PhoneNumber"].Value = txtPhone1.Text.Trim();

                accountNodes[0].Attributes["Proxy"].Value = txtProxy1.Text.Trim();

                accountNodes[1].Attributes["CraigslistAccount"].Value = txtAcc2.Text.Trim();

                accountNodes[1].Attributes["CraigsListPassword"].Value = txtPass2.Text.Trim();

                accountNodes[1].Attributes["PhoneNumber"].Value = txtPhone2.Text.Trim();

                accountNodes[1].Attributes["Proxy"].Value = txtProxy2.Text.Trim();

                accountNodes[2].Attributes["CraigslistAccount"].Value = txtAcc3.Text.Trim();

                accountNodes[2].Attributes["CraigsListPassword"].Value = txtPass3.Text.Trim();

                accountNodes[2].Attributes["PhoneNumber"].Value = txtPhone3.Text.Trim();

                accountNodes[2].Attributes["Proxy"].Value = txtProxy3.Text.Trim();

                accountNodes[3].Attributes["CraigslistAccount"].Value = txtAcc4.Text.Trim();

                accountNodes[3].Attributes["CraigsListPassword"].Value = txtPass4.Text.Trim();

                accountNodes[3].Attributes["PhoneNumber"].Value = txtPhone4.Text.Trim();

                accountNodes[3].Attributes["Proxy"].Value = txtProxy4.Text.Trim();
                
                accountNodes[4].Attributes["CraigslistAccount"].Value = txtAcc5.Text.Trim();

                accountNodes[4].Attributes["CraigsListPassword"].Value = txtPass5.Text.Trim();

                accountNodes[4].Attributes["PhoneNumber"].Value = txtPhone5.Text.Trim();

                accountNodes[4].Attributes["Proxy"].Value = txtProxy5.Text.Trim();

                accountNodes[5].Attributes["CraigslistAccount"].Value = txtAcc6.Text.Trim();

                accountNodes[5].Attributes["CraigsListPassword"].Value = txtPass6.Text.Trim();

                accountNodes[5].Attributes["PhoneNumber"].Value = txtPhone6.Text.Trim();

                accountNodes[5].Attributes["Proxy"].Value = txtProxy6.Text.Trim();

                accountNodes[6].Attributes["CraigslistAccount"].Value = txtAcc7.Text.Trim();

                accountNodes[6].Attributes["CraigsListPassword"].Value = txtPass7.Text.Trim();

                accountNodes[6].Attributes["PhoneNumber"].Value = txtPhone7.Text.Trim();

                accountNodes[6].Attributes["Proxy"].Value = txtProxy7.Text.Trim();

                accountNodes[7].Attributes["CraigslistAccount"].Value = txtAcct8.Text.Trim();

                accountNodes[7].Attributes["CraigsListPassword"].Value = txtPass8.Text.Trim();

                accountNodes[7].Attributes["PhoneNumber"].Value = txtPhone8.Text.Trim();

                accountNodes[7].Attributes["Proxy"].Value = txtProxy8.Text.Trim();

                accountNodes[8].Attributes["CraigslistAccount"].Value = txtAcct9.Text.Trim();

                accountNodes[8].Attributes["CraigsListPassword"].Value = txtPass9.Text.Trim();

                accountNodes[8].Attributes["PhoneNumber"].Value = txtPhone9.Text.Trim();

                accountNodes[8].Attributes["Proxy"].Value = txtProxy9.Text.Trim();

                accountNodes[9].Attributes["CraigslistAccount"].Value = txtAcct10.Text.Trim();

                accountNodes[9].Attributes["CraigsListPassword"].Value = txtPass10.Text.Trim();

                accountNodes[9].Attributes["PhoneNumber"].Value = txtPhone10.Text.Trim();

                accountNodes[9].Attributes["Proxy"].Value = txtProxy10.Text.Trim();

             
            }
            else
            {
                var root = xmlDoc.SelectSingleNode("CraiglistAccounts");

                for (int i = 0; i < 10 - accountNodes.Count;i++ )

                    root.AppendChild(accountNodes[0].Clone());


                accountNodes[0].Attributes["CraigslistAccount"].Value = txtAcc1.Text.Trim();

                accountNodes[0].Attributes["CraigsListPassword"].Value = txtPass1.Text.Trim();

                accountNodes[0].Attributes["PhoneNumber"].Value = txtPhone1.Text.Trim();

                accountNodes[0].Attributes["Proxy"].Value = txtProxy1.Text.Trim();

                accountNodes[1].Attributes["CraigslistAccount"].Value = txtAcc2.Text.Trim();

                accountNodes[1].Attributes["CraigsListPassword"].Value = txtPass2.Text.Trim();

                accountNodes[1].Attributes["PhoneNumber"].Value = txtPhone2.Text.Trim();

                accountNodes[1].Attributes["Proxy"].Value = txtProxy2.Text.Trim();

                accountNodes[2].Attributes["CraigslistAccount"].Value = txtAcc3.Text.Trim();

                accountNodes[2].Attributes["CraigsListPassword"].Value = txtPass3.Text.Trim();

                accountNodes[2].Attributes["PhoneNumber"].Value = txtPhone3.Text.Trim();

                accountNodes[2].Attributes["Proxy"].Value = txtProxy3.Text.Trim();

                accountNodes[3].Attributes["CraigslistAccount"].Value = txtAcc4.Text.Trim();

                accountNodes[3].Attributes["CraigsListPassword"].Value = txtPass4.Text.Trim();

                accountNodes[3].Attributes["PhoneNumber"].Value = txtPhone4.Text.Trim();

                accountNodes[3].Attributes["Proxy"].Value = txtProxy4.Text.Trim();

                accountNodes[4].Attributes["CraigslistAccount"].Value = txtAcc5.Text.Trim();

                accountNodes[4].Attributes["CraigsListPassword"].Value = txtPass5.Text.Trim();

                accountNodes[4].Attributes["PhoneNumber"].Value = txtPhone5.Text.Trim();

                accountNodes[4].Attributes["Proxy"].Value = txtProxy5.Text.Trim();

                accountNodes[5].Attributes["CraigslistAccount"].Value = txtAcc6.Text.Trim();

                accountNodes[5].Attributes["CraigsListPassword"].Value = txtPass6.Text.Trim();

                accountNodes[5].Attributes["PhoneNumber"].Value = txtPhone6.Text.Trim();

                accountNodes[5].Attributes["Proxy"].Value = txtProxy6.Text.Trim();

                accountNodes[6].Attributes["CraigslistAccount"].Value = txtAcc7.Text.Trim();

                accountNodes[6].Attributes["CraigsListPassword"].Value = txtPass7.Text.Trim();

                accountNodes[6].Attributes["PhoneNumber"].Value = txtPhone7.Text.Trim();

                accountNodes[6].Attributes["Proxy"].Value = txtProxy7.Text.Trim();

                accountNodes[7].Attributes["CraigslistAccount"].Value = txtAcct8.Text.Trim();

                accountNodes[7].Attributes["CraigsListPassword"].Value = txtPass8.Text.Trim();

                accountNodes[7].Attributes["PhoneNumber"].Value = txtPhone8.Text.Trim();

                accountNodes[7].Attributes["Proxy"].Value = txtProxy8.Text.Trim();

                accountNodes[8].Attributes["CraigslistAccount"].Value = txtAcct9.Text.Trim();

                accountNodes[8].Attributes["CraigsListPassword"].Value = txtPass9.Text.Trim();

                accountNodes[8].Attributes["PhoneNumber"].Value = txtPhone9.Text.Trim();

                accountNodes[8].Attributes["Proxy"].Value = txtProxy9.Text.Trim();

                accountNodes[9].Attributes["CraigslistAccount"].Value = txtAcct10.Text.Trim();

                accountNodes[9].Attributes["CraigsListPassword"].Value = txtPass10.Text.Trim();

                accountNodes[9].Attributes["PhoneNumber"].Value = txtPhone10.Text.Trim();

                accountNodes[9].Attributes["Proxy"].Value = txtProxy10.Text.Trim();

                
            }


            xmlDoc.Save(txtSource.Text);
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            updateCraigsList();

            clsVariables.accountList = new List<CraigsListEmailAccount>();

            var accountNodes = XMLHelper.selectElements("Account", txtSource.Text);

            int Position = 1;

            foreach (XmlNode xmlAccount in accountNodes)
            {
                if (!xmlAccount.Attributes["CraigslistAccount"].Value.Equals("") && !xmlAccount.Attributes["CraigsListPassword"].Value.Equals(""))
                {
                    var clAccount = new CraigsListEmailAccount()
                    {
                        CraigslistAccount = xmlAccount.Attributes["CraigslistAccount"].Value,
                        CraigsListPassword = xmlAccount.Attributes["CraigsListPassword"].Value,
                        CraigsAccountPhoneNumber = xmlAccount.Attributes["PhoneNumber"].Value,
                        Proxy = xmlAccount.Attributes["Proxy"].Value,
                        isCurrentlyUsed = false,
                        IntervalofAds = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["IntervalOfAds"].ToString(CultureInfo.InvariantCulture)),
                        Position = Position++,

                    };

                    clsVariables.accountList.Add(clAccount);
                }
            }
            clsVariables.accountList.First().isCurrentlyUsed = true;

            clsVariables.chunkList = SQLHelper.GetChunkString();

            //clsVariables.hostingServerList = SQLHelper.GetHostingServer(DateTime.Now.DayOfWeek.ToString());

            clsVariables.computerList = SQLHelper.GetComputerList();

        }

      
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!String.IsNullOrEmpty(clsVariables.accountList.First(t => t.isCurrentlyUsed).Proxy))
            {
                var registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);

                registry.SetValue("ProxyEnable", 1);

                registry.SetValue("ProxyServer", clsVariables.accountList.First(t => t.isCurrentlyUsed).Proxy);
            }
            else
            {

                var registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);

                registry.SetValue("ProxyEnable", 0);

                //registry.SetValue("ProxyServer", clsVariables.accountList.First(t => t.isCurrentlyUsed).Proxy);
            }

         

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
     

       
    }
}


