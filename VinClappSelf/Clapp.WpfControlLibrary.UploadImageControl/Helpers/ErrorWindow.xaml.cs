namespace Clapp.WpfControlLibrary.UploadImageControl.Helpers
{
    public partial class ErrorWindow 
    {
        public ErrorWindow(string message)
        {
            InitializeComponent();

            try
            {
                fldMessage.Text = message;
            }
            catch { }
        }
    }
}
