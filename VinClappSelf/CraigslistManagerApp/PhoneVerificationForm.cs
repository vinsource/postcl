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
    public partial class PhoneVerificationForm : Form
    {
        private System.Timers.Timer aTimer;
        private int flags;
        private int timeLeft = 70;
        private string mode = "";
        public PhoneVerificationForm(int status)
        {
            InitializeComponent();
            if (status == 3)
                lblCheck.Text = "Email Verification";
        }
        public PhoneVerificationForm(int status,string mode)
        {
            this.mode = mode;
            InitializeComponent();
            if (status == 3)
                lblCheck.Text = "Email Verification";
        }
       
        private void TimerPhoneVerifyTick(object sender, EventArgs e)
        {
            if (lblCheck.Visible == false)
                lblCheck.Visible = true;
            else
                lblCheck.Visible = false;
        }

        private void PhoneVerificationFormMouseMove(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void PhoneVerificationFormMouseHover(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PhoneVerificationFormLeave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PhoneVerificationFormLoad(object sender, EventArgs e)
        {
            if (mode.Equals("ScheduleMode"))
                lblAccount.Text = clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).CraigslistAccount;
            
            else
            {
                lblAccount.Text = clsVariables.accountList.First(t => t.isCurrentlyUsed).CraigslistAccount;
            }
            
            flags = WinAPI.AW_ACTIVATE | WinAPI.AW_HOR_POSITIVE | WinAPI.AW_SLIDE;
            WinAPI.AnimateWindow(this.Handle, 1500, flags);
            timerPhoneVerify.Start();
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

        private void PhoneVerificationFormClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PhoneVerificationFormMouseEnter(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PhoneVerificationFormMouseLeave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PhoneVerificationFormMouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void PhoneVerificationFormKeyPress(object sender, KeyPressEventArgs e)
        {
            this.Close();
        }

        private void PhoneVerificationFormKeyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }
    }
}
