namespace VinCLAPP
{
    partial class CityChoiceForm
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
        private static CityChoiceForm sForm = null;
        public static CityChoiceForm Instance()
        {
            if (sForm == null) { sForm = new CityChoiceForm(); }

            else
            {
                sForm.Close();
                sForm = new CityChoiceForm();
            }

            return sForm;
        }

        public static CityChoiceForm Instance(MainForm mainForm)
        {
            if (sForm == null) { sForm = new CityChoiceForm(mainForm); }

            else
            {
                sForm.Close();
                sForm = new CityChoiceForm(mainForm);
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.StateTreeview = new System.Windows.Forms.TreeView();
            this.cbCityList = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.StateTreeview);
            this.groupBox1.Controls.Add(this.cbCityList);
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Location = new System.Drawing.Point(11, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 452);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Which city do you want to post first?";
            // 
            // btnReset
            // 
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(151, 412);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(67, 34);
            this.btnReset.TabIndex = 14;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // StateTreeview
            // 
            this.StateTreeview.CheckBoxes = true;
            this.StateTreeview.Location = new System.Drawing.Point(6, 51);
            this.StateTreeview.Name = "StateTreeview";
            this.StateTreeview.Size = new System.Drawing.Size(321, 355);
            this.StateTreeview.TabIndex = 13;
            this.StateTreeview.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.StateTreeview_AfterCheck);
            // 
            // cbCityList
            // 
            this.cbCityList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCityList.FormattingEnabled = true;
            this.cbCityList.Location = new System.Drawing.Point(6, 21);
            this.cbCityList.Name = "cbCityList";
            this.cbCityList.Size = new System.Drawing.Size(321, 28);
            this.cbCityList.TabIndex = 12;
            // 
            // btnOK
            // 
            this.btnOK.BackgroundImage = global::VinCLAPP.Properties.Resources.btnBGblack;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(221, 412);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(106, 34);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "Save And Close";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // CityChoiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.ClientSize = new System.Drawing.Size(356, 466);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CityChoiceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "City";
            this.Load += new System.EventHandler(this.CityChoiceForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbCityList;
        private System.Windows.Forms.TreeView StateTreeview;
        private System.Windows.Forms.Button btnReset;
    }
}