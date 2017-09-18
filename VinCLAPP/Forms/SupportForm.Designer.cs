namespace VinCLAPP.Forms
{
    partial class SupportForm
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
        private static SupportForm sForm = null;
        public static SupportForm Instance()
        {
            if (sForm == null) { sForm = new SupportForm(); }

            else
            {
                sForm.Close();
                sForm = null;
                sForm = new SupportForm();
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSubject = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rtbCustomMessage = new System.Windows.Forms.RichTextBox();
            this.btnSubmitRequest = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.linkVideo = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.linkVideo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Controls.Add(this.btnSubmitRequest);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(438, 319);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Please fill in all required fields for data feed ";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.cbSubject);
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Controls.Add(this.rtbCustomMessage);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 21);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(422, 217);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Subject";
            // 
            // cbSubject
            // 
            this.cbSubject.FormattingEnabled = true;
            this.cbSubject.Location = new System.Drawing.Point(62, 3);
            this.cbSubject.Name = "cbSubject";
            this.cbSubject.Size = new System.Drawing.Size(297, 24);
            this.cbSubject.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Description";
            // 
            // rtbCustomMessage
            // 
            this.rtbCustomMessage.Location = new System.Drawing.Point(85, 33);
            this.rtbCustomMessage.Name = "rtbCustomMessage";
            this.rtbCustomMessage.Size = new System.Drawing.Size(333, 170);
            this.rtbCustomMessage.TabIndex = 4;
            this.rtbCustomMessage.Text = "";
            // 
            // btnSubmitRequest
            // 
            this.btnSubmitRequest.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSubmitRequest.Location = new System.Drawing.Point(314, 244);
            this.btnSubmitRequest.Name = "btnSubmitRequest";
            this.btnSubmitRequest.Size = new System.Drawing.Size(116, 23);
            this.btnSubmitRequest.TabIndex = 7;
            this.btnSubmitRequest.Text = "Submit Request";
            this.btnSubmitRequest.UseVisualStyleBackColor = true;
            this.btnSubmitRequest.Click += new System.EventHandler(this.btnSubmitRequest_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 275);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "* For tutorial, please click";
            // 
            // linkVideo
            // 
            this.linkVideo.AutoSize = true;
            this.linkVideo.Location = new System.Drawing.Point(168, 275);
            this.linkVideo.Name = "linkVideo";
            this.linkVideo.Size = new System.Drawing.Size(139, 16);
            this.linkVideo.TabIndex = 9;
            this.linkVideo.TabStop = true;
            this.linkVideo.Text = "here to see tthe video.";
            this.linkVideo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkVideo_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 291);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Or go to help/video";
            // 
            // SupportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.ClientSize = new System.Drawing.Size(458, 337);
            this.Controls.Add(this.groupBox1);
            this.Name = "SupportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SupportForm";
            this.Load += new System.EventHandler(this.SupportForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSubject;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox rtbCustomMessage;
        private System.Windows.Forms.Button btnSubmitRequest;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkVideo;
        private System.Windows.Forms.Label label2;
    }
}