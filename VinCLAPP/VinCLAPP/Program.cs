using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using VinCLAPP.Forms;

namespace VinCLAPP
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            //var form = new LoadingForm();
            //if (form.ShowDialog() == DialogResult.OK)
            //{
            //    form.Close();
            //    string executeFile =
            //        ConfigurationManager.AppSettings["baseDirectory"].ToString(CultureInfo.InvariantCulture) +
            //        System.Configuration.ConfigurationManager.AppSettings["runFilePath"].ToString(
            //            CultureInfo.InvariantCulture);
            //    Process.Start(executeFile);


            //}
        }
    }
}