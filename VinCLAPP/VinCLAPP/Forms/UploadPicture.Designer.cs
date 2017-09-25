namespace VinCLAPP.Forms
{
    partial class UploadPicture
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
            this.elementHost = new System.Windows.Forms.Integration.ElementHost();
            this.SuspendLayout();
            // 
            // elementHost
            // 
            this.elementHost.Location = new System.Drawing.Point(12, 12);
            this.elementHost.Name = "elementHost";
            this.elementHost.Size = new System.Drawing.Size(831, 655);
            this.elementHost.TabIndex = 4;
            this.elementHost.Text = "elementHost";
            this.elementHost.Child = null;
            // 
            // UploadPicture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 679);
            this.Controls.Add(this.elementHost);
            this.Name = "UploadPicture";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Upload Images";
            this.ResumeLayout(false);

        }

        #endregion

        private Clapp.WpfControlLibrary.UploadImageControl.WPFUploadImage wpfUploadImage1;
        private System.Windows.Forms.Integration.ElementHost elementHost;


    }
}