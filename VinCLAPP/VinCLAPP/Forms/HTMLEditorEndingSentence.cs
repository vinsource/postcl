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
    public partial class HTMLEditorEndingSentence : Form
    {
            private readonly MainForm _frmMain;
        public HTMLEditorEndingSentence()
        {
            InitializeComponent();
        }
        public HTMLEditorEndingSentence(MainForm frmMain)
        {
            InitializeComponent();
            
            this._frmMain = frmMain;
         

        }

        private void btnEditHTML_Click(object sender, EventArgs e)
        {
            this.htmlEditorControl.HtmlContentsEdit();
            this.htmlEditorControl.Focus();
        }

        private void btnViewHTML_Click(object sender, EventArgs e)
        {
            this.htmlEditorControl.HtmlContentsView();
            this.htmlEditorControl.Focus();
        }

        private void HTMLEditorEndingSentence_Load(object sender, EventArgs e)
        {
            this.htmlEditorControl.InnerHtml = GlobalVar.CurrentDealer.EndingSentence;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this._frmMain.txtEndingSentence.Text=  this.htmlEditorControl.InnerHtml;

            var context = new CLDMSEntities();

            var dealer = context.Dealers.First(o => o.DealerId == GlobalVar.CurrentDealer.DealerId);
            
            dealer.EndingSentence = String.IsNullOrEmpty(this.htmlEditorControl.InnerHtml) ? "" : this.htmlEditorControl.InnerHtml.Trim();

            context.SaveChanges();

            GlobalVar.CurrentDealer.EndingSentence = dealer.EndingSentence;

            this.Close();
        }
    }
}
