using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using ClappManagementSystem.DatabaseModel;
using ClappManagementSystem.Helper;
using ClappManagementSystem.Model;

namespace ClappManagementSystem.Forms
{
  
    public partial class PCSchedule : Form
    {
        public PCSchedule()
        {
            InitializeComponent();
        }

        public void InitializeSettingAfterSave()
        {
         
            foreach (DataGridViewRow tmp in dGridPCSchedule.Rows)
            {
                var scheduleValue = tmp.Cells[3].Value;

                tmp.Cells[1].Value = scheduleValue;

            

            }
        }

        public PCSchedule(ScheudeForm frmMain)
        {
            
            InitializeComponent();

            dGridPCSchedule.Rows.Clear();

            var context = new whitmanenterprisecraigslistEntities();

            var pcScheduleList = context.vinclapppcschedules.ToList();

            var totalSchedule = context.vinclappdealerschedules.First().Schedules.GetValueOrDefault();

       
            for (var i = 1; i <= Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MaximumComputer"].ToString(CultureInfo.InvariantCulture)); i++)
            {

                var newRow = new DataGridViewRow();

                newRow.CreateCells(dGridPCSchedule);

                newRow.Cells[0].Value = i;

                if (pcScheduleList.Any(x => x.PC == i))
                {

                    newRow.Cells[1].Value = pcScheduleList.First(x => x.PC == i).Schedule;


                }

                dGridPCSchedule.Rows.Add(newRow);

            }

            var tmpCom = new List<ComplicatedValueComboBox>();
            for (var y = 1; y <= totalSchedule; y++)
            {
                var newComp = new ComplicatedValueComboBox()
                {
                    Text = y,
                    Value = y
                };

                tmpCom.Add(newComp);
            }

            var comboCol = new DataGridViewComboBoxColumn
            {
                Name = "cmbSchedule",
                HeaderText = "Select Schedule",
                DataSource = tmpCom,
                DisplayMember = "Text",
                ValueMember = "Value",
            };

            dGridPCSchedule.Columns.Add(comboCol);


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var context = new whitmanenterprisecraigslistEntities();


            foreach (var tmp in context.vinclapppcschedules.ToList())
            {
                context.Attach(tmp);
                context.DeleteObject(tmp);
            }
           
            foreach (DataGridViewRow tmp in dGridPCSchedule.Rows)
            {
                var scheduleValue = tmp.Cells[3].Value;

                if(scheduleValue!=null)
                {


                    var pcSchedule = new vinclapppcschedule()
                        {

                            PC = Convert.ToInt32(tmp.Cells[0].Value),
                            Schedule = Convert.ToInt32(scheduleValue),
                            LastUpdated = DateTime.Now
                        };

                    context.AddTovinclapppcschedules(pcSchedule);
                }
            }

            context.SaveChanges();

            InitializeSettingAfterSave();

            this.Refresh();

            MessageBox.Show("PC - Schedule was associated successfully. ");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
              DialogResult result= MessageBox.Show("Are you sure to delete pc-schedule association?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
            {
                var context = new whitmanenterprisecraigslistEntities();

                foreach (var tmp in context.vinclapppcschedules.ToList())
                {
                    context.Attach(tmp);
                    context.DeleteObject(tmp);
                }

                InitializeSettingAfterSave();
            }
        }

        private void dGridPCSchedule_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dGridPCSchedule.Columns["ClearButton"].Index && e.RowIndex >= 0)
            {
                var computerId = Convert.ToInt32(dGridPCSchedule.Rows[e.RowIndex].Cells[0].Value.ToString());

                var context = new whitmanenterprisecraigslistEntities();



                if (context.vinclapppcschedules.Any(x => x.PC == computerId))
                {
                    var searchResult = context.vinclapppcschedules.First(x => x.PC == computerId);

                    context.Attach(searchResult);

                    context.DeleteObject(searchResult);

                    context.SaveChanges();
                }



                dGridPCSchedule.Rows[e.RowIndex].Cells[1].Value = "";

            }
        }

    }
}
