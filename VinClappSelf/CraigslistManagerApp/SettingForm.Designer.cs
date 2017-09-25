namespace CraigslistManagerApp
{
    partial class SettingForm
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

        private static SettingForm sForm = null;
        public static SettingForm Instance(Mainform frmMain)
        {
            if (sForm == null) { sForm = new SettingForm(frmMain); }

            else
            {
                sForm.Close();
                sForm = null;
                sForm = new SettingForm(frmMain);
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
            this.nudInterval = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb5 = new System.Windows.Forms.RadioButton();
            this.rb3 = new System.Windows.Forms.RadioButton();
            this.rb25 = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.cbAll = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudInterval)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // nudInterval
            // 
            this.nudInterval.Location = new System.Drawing.Point(118, 13);
            this.nudInterval.Margin = new System.Windows.Forms.Padding(4);
            this.nudInterval.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudInterval.Name = "nudInterval";
            this.nudInterval.Size = new System.Drawing.Size(94, 22);
            this.nudInterval.TabIndex = 0;
            this.nudInterval.ValueChanged += new System.EventHandler(this.nudInterval_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb5);
            this.groupBox1.Controls.Add(this.rb3);
            this.groupBox1.Controls.Add(this.rb25);
            this.groupBox1.Location = new System.Drawing.Point(3, 42);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(287, 52);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Timer";
            // 
            // rb5
            // 
            this.rb5.AutoSize = true;
            this.rb5.Location = new System.Drawing.Point(204, 25);
            this.rb5.Margin = new System.Windows.Forms.Padding(4);
            this.rb5.Name = "rb5";
            this.rb5.Size = new System.Drawing.Size(82, 20);
            this.rb5.TabIndex = 2;
            this.rb5.Text = "5 Minutes";
            this.rb5.UseVisualStyleBackColor = true;
            // 
            // rb3
            // 
            this.rb3.AutoSize = true;
            this.rb3.Location = new System.Drawing.Point(116, 25);
            this.rb3.Margin = new System.Windows.Forms.Padding(4);
            this.rb3.Name = "rb3";
            this.rb3.Size = new System.Drawing.Size(82, 20);
            this.rb3.TabIndex = 1;
            this.rb3.Text = "3 Minutes";
            this.rb3.UseVisualStyleBackColor = true;
            // 
            // rb25
            // 
            this.rb25.AutoSize = true;
            this.rb25.Location = new System.Drawing.Point(9, 25);
            this.rb25.Margin = new System.Windows.Forms.Padding(4);
            this.rb25.Name = "rb25";
            this.rb25.Size = new System.Drawing.Size(92, 20);
            this.rb25.TabIndex = 0;
            this.rb25.Text = "2.5 Minutes";
            this.rb25.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(157, 101);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(67, 21);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(37, 101);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(114, 21);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "Save And Close";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Interval of Posts";
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.errorProvider1.ContainerControl = this.nudInterval;
            // 
            // cbAll
            // 
            this.cbAll.AutoSize = true;
            this.cbAll.Location = new System.Drawing.Point(219, 16);
            this.cbAll.Name = "cbAll";
            this.cbAll.Size = new System.Drawing.Size(72, 20);
            this.cbAll.TabIndex = 10;
            this.cbAll.Text = "Post All";
            this.cbAll.UseVisualStyleBackColor = true;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 134);
            this.Controls.Add(this.cbAll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.nudInterval);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SettingForm";
            ((System.ComponentModel.ISupportInitialize)(this.nudInterval)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudInterval;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rb5;
        private System.Windows.Forms.RadioButton rb3;
        private System.Windows.Forms.RadioButton rb25;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private Mainform frmMain;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.CheckBox cbAll;

    }
}