using System.Windows;

namespace Clapp.WpfControlLibrary.UploadImageControl.Helpers
{
    public partial class WarningWindow 
    {
        public WarningWindow(string message)
        {
            InitializeComponent();

            try
            {
                fldMessage.Text = message;
            }
            catch { }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DialogResult = true;
            }
            catch { }
        }
    }
}
