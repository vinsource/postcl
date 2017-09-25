using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using CraigslistManagerApp.Model;

namespace CraigslistManagerApp
{
    public partial class LoginWarningForm : Form
    {
        private System.Timers.Timer aTimer;
        private int flags;
        private int timeLeft = 70;
        private WhitmanEntepriseMasterVehicleInfo vehicle;

        public LoginWarningForm()
        {
            InitializeComponent();
            lblCheck.Text = "Login Warning";
        }
        public LoginWarningForm(WhitmanEntepriseMasterVehicleInfo vehicle)
        {
            InitializeComponent();
            lblCheck.Text = "Login Warning";
            this.vehicle = vehicle;
        }

       
        private void timerPhoneVerify_Tick(object sender, EventArgs e)
        {
            if (lblCheck.Visible == false)
                lblCheck.Visible = true;
            else
                lblCheck.Visible = false;
        }

        private void PhoneVerificationForm_MouseMove(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void PhoneVerificationForm_MouseHover(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PhoneVerificationForm_Leave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PhoneVerificationForm_Load(object sender, EventArgs e)
        {
            if (vehicle.CLPostingId > 0)
                lblAccount.Text = vehicle.CraigslistAccountName;
            else
            {
                lblAccount.Text = clsVariables.computeremailaccountList.First(t => t.isCurrentlyUsed).CraigslistAccount;
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

        private void PhoneVerificationForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PhoneVerificationForm_MouseEnter(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PhoneVerificationForm_MouseLeave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PhoneVerificationForm_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void PhoneVerificationForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.Close();
        }

        private void PhoneVerificationForm_KeyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }
    }
}
