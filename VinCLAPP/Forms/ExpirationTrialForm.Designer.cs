namespace VinCLAPP.Forms
{
    partial class ExpirationTrialForm
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
        private static ExpirationTrialForm sForm = null;
        public static ExpirationTrialForm Instance()
        {
            if (sForm == null) { sForm = new ExpirationTrialForm(); }

            else
            {
                sForm.Close();
                sForm = new ExpirationTrialForm();
            }

            return sForm;
        }
        public static ExpirationTrialForm Instance(MainForm mainForm)
        {
            if (sForm == null) { sForm = new ExpirationTrialForm(mainForm); }

            else
            {
                sForm.Close();
                sForm = new ExpirationTrialForm(mainForm);
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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(391, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "                                          Your trial has been expired. \r\nPlease m" +
    "ake a payment on tools/billing to continue using our services. ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(159, 49);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 27);
            this.button1.TabIndex = 1;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ExpirationTrialForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 78);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ExpirationTrialForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Expiration Trial";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}