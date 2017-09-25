namespace ClappManagementSystem.Forms
{
    partial class PCSchedule
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

        private static PCSchedule sForm = null;
        public static PCSchedule Instance(ScheudeForm frmMain)
        {
            if (sForm == null) { sForm = new PCSchedule(frmMain); }

            else
            {
                sForm.Close();
                sForm = null;
                sForm = new PCSchedule(frmMain);
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
            this.dGridPCSchedule = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Computer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Schedule = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClearButton = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dGridPCSchedule)).BeginInit();
            this.SuspendLayout();
            // 
            // dGridPCSchedule
            // 
            this.dGridPCSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGridPCSchedule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Computer,
            this.Schedule,
            this.ClearButton});
            this.dGridPCSchedule.Dock = System.Windows.Forms.DockStyle.Top;
            this.dGridPCSchedule.Location = new System.Drawing.Point(0, 0);
            this.dGridPCSchedule.Name = "dGridPCSchedule";
            this.dGridPCSchedule.Size = new System.Drawing.Size(528, 675);
            this.dGridPCSchedule.TabIndex = 0;
            this.dGridPCSchedule.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGridPCSchedule_CellClick);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(428, 681);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 33);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(220, 681);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 33);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(324, 681);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(98, 33);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "Delete";
            this.dataGridViewImageColumn1.Image = global::ClappManagementSystem.Properties.Resources.delete;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            // 
            // Computer
            // 
            this.Computer.DataPropertyName = "Computer";
            this.Computer.HeaderText = "Computer";
            this.Computer.Name = "Computer";
            this.Computer.ReadOnly = true;
            // 
            // Schedule
            // 
            this.Schedule.DataPropertyName = "Schedule";
            this.Schedule.HeaderText = "Current Schedule";
            this.Schedule.Name = "Schedule";
            this.Schedule.Width = 150;
            // 
            // ClearButton
            // 
            this.ClearButton.HeaderText = "Delete";
            this.ClearButton.Image = global::ClappManagementSystem.Properties.Resources.Delete_icon_small;
            this.ClearButton.Name = "ClearButton";
            // 
            // PCSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 726);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dGridPCSchedule);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "PCSchedule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PC - Schedule";
            ((System.ComponentModel.ISupportInitialize)(this.dGridPCSchedule)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dGridPCSchedule;
        private ScheudeForm frmMain;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Computer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Schedule;
        private System.Windows.Forms.DataGridViewImageColumn ClearButton;
    }
}