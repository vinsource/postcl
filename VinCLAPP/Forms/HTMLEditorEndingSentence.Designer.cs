namespace VinCLAPP.Forms
{
    partial class HTMLEditorEndingSentence
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
        private static HTMLEditorEndingSentence sForm = null;
        public static HTMLEditorEndingSentence Instance()
        {
            if (sForm == null) { sForm = new HTMLEditorEndingSentence(); }

            else
            {
                sForm.Close();
                sForm = new HTMLEditorEndingSentence();
            }

            return sForm;
        }
        public static HTMLEditorEndingSentence Instance(MainForm mainForm)
        {
            if (sForm == null) { sForm = new HTMLEditorEndingSentence(mainForm); }

            else
            {
                sForm.Close();
                sForm = new HTMLEditorEndingSentence(mainForm);
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
            this.htmlEditorControl = new MSDN.Html.Editor.HtmlEditorControl();
            this.btnEditHTML = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // htmlEditorControl
            // 
            this.htmlEditorControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.htmlEditorControl.InnerText = null;
            this.htmlEditorControl.Location = new System.Drawing.Point(98, 2);
            this.htmlEditorControl.Name = "htmlEditorControl";
            this.htmlEditorControl.Size = new System.Drawing.Size(597, 468);
            this.htmlEditorControl.TabIndex = 0;
            // 
            // btnEditHTML
            // 
            this.btnEditHTML.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnEditHTML.Location = new System.Drawing.Point(0, 173);
            this.btnEditHTML.Name = "btnEditHTML";
            this.btnEditHTML.Size = new System.Drawing.Size(92, 29);
            this.btnEditHTML.TabIndex = 8;
            this.btnEditHTML.Text = "Edit HTML";
            this.btnEditHTML.UseVisualStyleBackColor = true;
            this.btnEditHTML.Click += new System.EventHandler(this.btnEditHTML_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSave.Location = new System.Drawing.Point(0, 208);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(92, 29);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save ";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(95, 487);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(607, 40);
            this.label6.TabIndex = 11;
            this.label6.Text = "*Remember that Craigslist has some limitations on HTML postings\r\nPlease check at " +
    "http://www.craigslist.org/about/help/html_in_craigslist_postings/";
            // 
            // HTMLEditorEndingSentence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.ClientSize = new System.Drawing.Size(717, 528);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnEditHTML);
            this.Controls.Add(this.htmlEditorControl);
            this.MaximizeBox = false;
            this.Name = "HTMLEditorEndingSentence";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EndingSentence HTML Editor";
            this.Load += new System.EventHandler(this.HTMLEditorEndingSentence_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MSDN.Html.Editor.HtmlEditorControl htmlEditorControl;
        private System.Windows.Forms.Button btnEditHTML;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label6;
    }
}