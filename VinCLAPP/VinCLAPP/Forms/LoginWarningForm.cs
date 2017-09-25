using System;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using VinCLAPP.Helper;
using Timer = System.Timers.Timer;

namespace VinCLAPP
{
    public partial class LoginWarningForm : Form
    {
        private Timer aTimer;
        private int flags;
        private int timeLeft = 70;

        public LoginWarningForm()
        {
            InitializeComponent();
            lblCheck.Text = "Login Warning";
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
            Close();
        }

        private void PhoneVerificationForm_MouseHover(object sender, EventArgs e)
        {
            Close();
        }

        private void PhoneVerificationForm_Leave(object sender, EventArgs e)
        {
            Close();
        }

        private void PhoneVerificationForm_Load(object sender, EventArgs e)
        {
            lblAccount.Text =
                GlobalVar.CurrentDealer.EmailAccountList.FirstOrDefault(t => t.IsCurrentlyUsed).CraigslistAccount;
            flags = WinAPI.AW_ACTIVATE | WinAPI.AW_HOR_POSITIVE | WinAPI.AW_SLIDE;
            WinAPI.AnimateWindow(Handle, 1500, flags);
            timerPhoneVerify.Start();
            aTimer = new Timer();

            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += OnTimedEvent;

            aTimer.Interval = 1000;

            aTimer.Enabled = true;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;

                txtTimer.Text = timeLeft + " seconds";

                Refresh();
            }
            else
            {
                Close();
            }
        }

        private void PhoneVerificationForm_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PhoneVerificationForm_MouseEnter(object sender, EventArgs e)
        {
            Close();
        }

        private void PhoneVerificationForm_MouseLeave(object sender, EventArgs e)
        {
            Close();
        }

        private void PhoneVerificationForm_MouseClick(object sender, MouseEventArgs e)
        {
            Close();
        }

        private void PhoneVerificationForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            Close();
        }

        private void PhoneVerificationForm_KeyDown(object sender, KeyEventArgs e)
        {
            Close();
        }
    }
}