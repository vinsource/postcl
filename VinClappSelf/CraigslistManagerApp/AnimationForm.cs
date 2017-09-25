using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CraigslistManagerApp
{
    public partial class AnimationForm : Form
    {
        private int animationTime;
        private int flags;
        public AnimationForm()
        {
            InitializeComponent();
        }

        public AnimationForm(int AnimationTime, int Flags)
		{
			animationTime = AnimationTime;
			flags = Flags;
			InitializeComponent();
		}

       

        private void AnimationForm_Load(object sender, EventArgs e)
        {
           WinAPI.AnimateWindow(this.Handle, animationTime, flags);
        }

        private void AnimationForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AnimationForm_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void AnimationForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            this.Close();
        }

        private void AnimationForm_KeyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }

        private void AnimationForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
