using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;

namespace Clapp.WpfControlLibrary.UploadImageControl.ImageViewer
{
    public class Photo
    {
        public Photo(string path)
        {
            _path = path;
            _source = new Uri(path);
            _image = BitmapFrame.Create(_source);
        }

        public override string ToString()
        {
            return _source.ToString();
        }

        private string _path;

        private Uri _source;
        public string Source { get { return _path; } }

        private BitmapFrame _image;
        public BitmapFrame Image { get { return _image; } set { _image = value; } }
    }

}
