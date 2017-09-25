using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Forms;

namespace CraigslistManagerApp
{
    public partial class TimerForm : Form
    {
        private int timeLeft = 75;

        private System.Timers.Timer aTimer;

        public TimerForm(Mainform frmMain)
        {
            InitializeComponent();
            this.frmMain = frmMain;
        }

        private void TimerForm_Load(object sender, EventArgs e)
        {
            CountDownTimer.Start();

            aTimer = new System.Timers.Timer();

            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            aTimer.Interval = 1000;

            aTimer.Enabled = true;

        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (timeLeft > 0)
            {

                timeLeft = timeLeft - 1;

                txtTimer.Text = timeLeft + " seconds";

                this.Refresh();

            }
            else
            {
                this.Close();

            }
        }

        private void CountDownTimer_Tick(object sender, EventArgs e)
        {
            if (lblCheck.Visible == false)
                lblCheck.Visible = true;
            else
                lblCheck.Visible = false;
        }


    }
}
