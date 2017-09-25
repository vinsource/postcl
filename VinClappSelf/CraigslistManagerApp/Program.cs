using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CraigslistManagerApp
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
            Application.Run(new Mainform());

            //var clForm = new CraigsListAccountForm();


            //if (clForm.ShowDialog() == DialogResult.OK)
            //{
            //    clForm.Close();
            //    Application.Run(new MAINFORM());

            //}
            

            
            
        }
    }
}
