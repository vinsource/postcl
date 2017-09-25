namespace CraigslistManagerApp
{
    partial class TimerForm
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

        private static TimerForm sForm = null;
        public static TimerForm Instance(Mainform frmMain)
        {
            if (sForm == null) { sForm = new TimerForm(frmMain); }

            else
            {
                sForm.Close();
                sForm = new TimerForm(frmMain);
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtTimer = new System.Windows.Forms.TextBox();
            this.lblCheck = new System.Windows.Forms.Label();
            this.CountDownTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Time Left";
            // 
            // txtTimer
            // 
            this.txtTimer.Enabled = false;
            this.txtTimer.Location = new System.Drawing.Point(82, 12);
            this.txtTimer.Name = "txtTimer";
            this.txtTimer.Size = new System.Drawing.Size(125, 20);
            this.txtTimer.TabIndex = 2;
            // 
            // lblCheck
            // 
            this.lblCheck.AutoSize = true;
            this.lblCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblCheck.Location = new System.Drawing.Point(12, 47);
            this.lblCheck.Name = "lblCheck";
            this.lblCheck.Size = new System.Drawing.Size(202, 25);
            this.lblCheck.TabIndex = 3;
            this.lblCheck.Text = "Check The Captcha";
            // 
            // CountDownTimer
            // 
            this.CountDownTimer.Interval = 500;
            this.CountDownTimer.Tick += new System.EventHandler(this.CountDownTimer_Tick);
            // 
            // TimerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 90);
            this.Controls.Add(this.lblCheck);
            this.Controls.Add(this.txtTimer);
            this.Controls.Add(this.label1);
            this.Name = "TimerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TimerForm";
            this.Load += new System.EventHandler(this.TimerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Mainform frmMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTimer;
        private System.Windows.Forms.Label lblCheck;
        private System.Windows.Forms.Timer CountDownTimer;
    }
}