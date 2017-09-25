using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Clapp.WpfControlLibrary.UploadImageControl.ImageViewer
{
    /// <summary>
    /// Interaction logic for ThumbnailPhoto.xaml
    /// </summary>
    public partial class ThumbnailPhoto : UserControl
    {
        public ThumbnailPhoto()
        {
            InitializeComponent();
        }

        public ImageSource ImageSource
        {
            set
            {
                img.Source = value;
                this.Tag = value;
            }
        }
        public double ImageWidth
        {
            set
            {
                this.Width = value;
            }
        }

        public double ImageHeight
        {
            set
            {
                this.Height = value;
            }
        }

        public event EventHandler<ThumbnailEventArgs> ThumbnailClick;

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if(ThumbnailClick != null)
            {
                ThumbnailClick(sender, new ThumbnailEventArgs() { ImageSource = this.Tag as ImageSource });
            }
        }
        public bool IsSelected
        {
            set { rdo.IsChecked = value; }
            get { return rdo.IsChecked.HasValue && rdo.IsChecked.Value; }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
    public class ThumbnailEventArgs:EventArgs
    {
        public ImageSource ImageSource { get; set; }
    }
}
