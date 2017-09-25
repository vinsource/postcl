using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ClappManagementSystem.Forms;

namespace ClappManagementSystem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var form = new Login();
            if (form.ShowDialog() == DialogResult.OK)
            {
                form.Close();
                Application.Run(new ScheudeForm());

            }
            //Application.Run(new ScheudeForm());

        }
    }
}
