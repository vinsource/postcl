namespace CraigslistManagerApp
{
    partial class FilterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private static FilterForm sForm = null;
        public static FilterForm Instance(Mainform frmMain)
        {
            if (sForm == null) { sForm = new FilterForm(frmMain); }

            else
            {
                sForm.Close();
                sForm = null;
                sForm = new FilterForm(frmMain);
            }

            return sForm;
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.grbDealers = new System.Windows.Forms.GroupBox();
            this.PanelCheckbox = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nuSplit = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.chkNoSplit = new System.Windows.Forms.CheckBox();
            this.grbDealers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuSplit)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(431, 200);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(67, 21);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(431, 147);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(114, 21);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "Save And Close";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // grbDealers
            // 
            this.grbDealers.Controls.Add(this.PanelCheckbox);
            this.grbDealers.Location = new System.Drawing.Point(12, 12);
            this.grbDealers.Name = "grbDealers";
            this.grbDealers.Size = new System.Drawing.Size(413, 388);
            this.grbDealers.TabIndex = 9;
            this.grbDealers.TabStop = false;
            this.grbDealers.Text = "Dealers";
            // 
            // PanelCheckbox
            // 
            this.PanelCheckbox.AutoSize = true;
            this.PanelCheckbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelCheckbox.Location = new System.Drawing.Point(3, 18);
            this.PanelCheckbox.Name = "PanelCheckbox";
            this.PanelCheckbox.Size = new System.Drawing.Size(407, 367);
            this.PanelCheckbox.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(434, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Split Interval";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(437, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Split Part";
            // 
            // nuSplit
            // 
            this.nuSplit.Location = new System.Drawing.Point(520, 66);
            this.nuSplit.Name = "nuSplit";
            this.nuSplit.Size = new System.Drawing.Size(65, 22);
            this.nuSplit.TabIndex = 13;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(520, 27);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(83, 22);
            this.textBox1.TabIndex = 14;
            this.textBox1.Text = "30";
            // 
            // chkNoSplit
            // 
            this.chkNoSplit.AutoSize = true;
            this.chkNoSplit.Location = new System.Drawing.Point(440, 104);
            this.chkNoSplit.Name = "chkNoSplit";
            this.chkNoSplit.Size = new System.Drawing.Size(74, 20);
            this.chkNoSplit.TabIndex = 15;
            this.chkNoSplit.Text = "No Split";
            this.chkNoSplit.UseVisualStyleBackColor = true;
            this.chkNoSplit.CheckedChanged += new System.EventHandler(this.chkNoSplit_CheckedChanged);
            // 
            // FilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 412);
            this.Controls.Add(this.chkNoSplit);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.nuSplit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grbDealers);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FilterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filter";
            this.grbDealers.ResumeLayout(false);
            this.grbDealers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nuSplit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private Mainform frmMain;
        private System.Windows.Forms.GroupBox grbDealers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nuSplit;
        private System.Windows.Forms.Panel PanelCheckbox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox chkNoSplit;

    }
}