namespace VinCLAPP
{
    partial class PhoneVerificationForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblCheck = new System.Windows.Forms.Label();
            this.timerPhoneVerify = new System.Windows.Forms.Timer(this.components);
            this.lblAccount = new System.Windows.Forms.Label();
            this.txtTimer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCheck
            // 
            this.lblCheck.AutoSize = true;
            this.lblCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblCheck.Location = new System.Drawing.Point(22, 35);
            this.lblCheck.Name = "lblCheck";
            this.lblCheck.Size = new System.Drawing.Size(376, 52);
            this.lblCheck.TabIndex = 4;
            this.lblCheck.Text = "Phone Verification";
            // 
            // timerPhoneVerify
            // 
            this.timerPhoneVerify.Interval = 500;
            this.timerPhoneVerify.Tick += new System.EventHandler(this.timerPhoneVerify_Tick);
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccount.Location = new System.Drawing.Point(31, 76);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(64, 25);
            this.lblAccount.TabIndex = 5;
            this.lblAccount.Text = "label1";
            // 
            // txtTimer
            // 
            this.txtTimer.Enabled = false;
            this.txtTimer.Location = new System.Drawing.Point(129, 8);
            this.txtTimer.Name = "txtTimer";
            this.txtTimer.Size = new System.Drawing.Size(125, 20);
            this.txtTimer.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(38, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Time Left";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhone.Location = new System.Drawing.Point(33, 110);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(60, 24);
            this.lblPhone.TabIndex = 8;
            this.lblPhone.Text = "label2";
            // 
            // PhoneVerificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.ClientSize = new System.Drawing.Size(406, 144);
            this.ControlBox = false;
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.txtTimer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblAccount);
            this.Controls.Add(this.lblCheck);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PhoneVerificationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.PhoneVerificationForm_Load);
            this.Click += new System.EventHandler(this.PhoneVerificationForm_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PhoneVerificationForm_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PhoneVerificationForm_KeyPress);
            this.Leave += new System.EventHandler(this.PhoneVerificationForm_Leave);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PhoneVerificationForm_MouseClick);
            this.MouseEnter += new System.EventHandler(this.PhoneVerificationForm_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.PhoneVerificationForm_MouseLeave);
            this.MouseHover += new System.EventHandler(this.PhoneVerificationForm_MouseHover);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PhoneVerificationForm_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCheck;
        private System.Windows.Forms.Timer timerPhoneVerify;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.TextBox txtTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPhone;
    }
}