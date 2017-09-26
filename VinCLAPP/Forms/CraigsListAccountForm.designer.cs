namespace VinCLAPP
{
    partial class CraigsListAccountForm
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

        private static CraigsListAccountForm sForm = null;
        public static CraigsListAccountForm Instance()
        {
            if (sForm == null) { sForm = new CraigsListAccountForm(); }

            else
            {
                sForm.Close();
                sForm = new CraigsListAccountForm();
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblFillUp = new System.Windows.Forms.Label();
            this.errorProvider4 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnAddAccount = new System.Windows.Forms.Button();
            this.EmailGridView = new System.Windows.Forms.DataGridView();
            this.EmailAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Phone = new VinCLAPP.Controls.DataGridViewMaskedTextColumn();
            this.Delete = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmailGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOk.Location = new System.Drawing.Point(466, 278);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(145, 23);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "Save And Close";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancel.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(362, 279);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 22);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // errorProvider3
            // 
            this.errorProvider3.ContainerControl = this;
            // 
            // lblFillUp
            // 
            this.lblFillUp.AutoSize = true;
            this.lblFillUp.Location = new System.Drawing.Point(12, 279);
            this.lblFillUp.Name = "lblFillUp";
            this.lblFillUp.Size = new System.Drawing.Size(132, 15);
            this.lblFillUp.TabIndex = 16;
            this.lblFillUp.Text = "* Fill up all blank fields ";
            // 
            // errorProvider4
            // 
            this.errorProvider4.ContainerControl = this;
            // 
            // btnAddAccount
            // 
            this.btnAddAccount.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAddAccount.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAccount.Location = new System.Drawing.Point(246, 279);
            this.btnAddAccount.Name = "btnAddAccount";
            this.btnAddAccount.Size = new System.Drawing.Size(98, 22);
            this.btnAddAccount.TabIndex = 17;
            this.btnAddAccount.Text = "Add Account";
            this.btnAddAccount.UseVisualStyleBackColor = true;
            this.btnAddAccount.Click += new System.EventHandler(this.btnAddAccount_Click);
            // 
            // EmailGridView
            // 
            this.EmailGridView.AllowUserToAddRows = false;
            this.EmailGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.EmailGridView.BackgroundColor = System.Drawing.Color.DarkRed;
            this.EmailGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EmailGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EmailAccount,
            this.Password,
            this.Phone,
            this.Delete});
            this.EmailGridView.Location = new System.Drawing.Point(12, 25);
            this.EmailGridView.Name = "EmailGridView";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.EmailGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.EmailGridView.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.EmailGridView.Size = new System.Drawing.Size(599, 247);
            this.EmailGridView.TabIndex = 18;
            // 
            // EmailAccount
            // 
            this.EmailAccount.DataPropertyName = "CraigslistAccount";
            this.EmailAccount.FillWeight = 113.0288F;
            this.EmailAccount.HeaderText = "Email Account";
            this.EmailAccount.Name = "EmailAccount";
            this.EmailAccount.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Password
            // 
            this.Password.DataPropertyName = "CraigsListPassword";
            this.Password.FillWeight = 113.0288F;
            this.Password.HeaderText = "Password";
            this.Password.Name = "Password";
            // 
            // Phone
            // 
            this.Phone.DataPropertyName = "CraigsAccountPhoneNumber";
            this.Phone.FillWeight = 113.0288F;
            this.Phone.HeaderText = "Phone";
            this.Phone.Mask = "(999) 000-0000";
            this.Phone.Name = "Phone";
            this.Phone.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Delete
            // 
            this.Delete.FillWeight = 60.9137F;
            this.Delete.HeaderText = "";
            this.Delete.Image = global::VinCLAPP.Properties.Resources.delete;
            this.Delete.Name = "Delete";
            // 
            // CraigsListAccountForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkRed;
            this.ClientSize = new System.Drawing.Size(626, 323);
            this.Controls.Add(this.EmailGridView);
            this.Controls.Add(this.btnAddAccount);
            this.Controls.Add(this.lblFillUp);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Name = "CraigsListAccountForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Email Account";
            this.Load += new System.EventHandler(this.CraigsListAccountForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EmailGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.Label lblFillUp;
        private System.Windows.Forms.ErrorProvider errorProvider4;
        private System.Windows.Forms.Button btnAddAccount;
        private System.Windows.Forms.DataGridView EmailGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmailAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Password;
        private Controls.DataGridViewMaskedTextColumn Phone;
        private System.Windows.Forms.DataGridViewImageColumn Delete;

    }
}