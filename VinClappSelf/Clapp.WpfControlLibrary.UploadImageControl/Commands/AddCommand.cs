using System;
using System.Globalization;
using System.IO;
using System.Windows.Input;
using Clapp.WpfControlLibrary.UploadImageControl.Helpers;
using Clapp.WpfControlLibrary.UploadImageControl.ViewModels;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Clapp.WpfControlLibrary.UploadImageControl.Commands
{
    public class AddCommand : ICommand
    {
        private const long Maxfilesize = 1024 * 1024;
        private const long Maxnumberoffile = 75;
        private readonly UploadViewModel _vm;

        public AddCommand(UploadViewModel vm)
        {
            _vm = vm;
        }

        public void Execute(object parameter)
        {
            _vm.IsBusy = true;
            var dlg = new OpenFileDialog {Filter = "All Files (*.*)|*.*", FilterIndex = 1, Multiselect = true};
            bool? result = dlg.ShowDialog();

            if (result.HasValue && result.Value)
            {
                ValidateFiles(dlg);
                if (AddCommandComplete != null)
                    AddCommandComplete.Invoke(this, new EventArgs());

                _vm.StartAll.Execute(null);
            }

            _vm.IsBusy = false;

        }

        private void ValidateFiles(OpenFileDialog dlg)
        {
            bool exist = false;
            int totalNumberOfFile = _vm.Files.Count + dlg.FileNames.Length;
            if (totalNumberOfFile > Maxnumberoffile)
            {
                ErrorHandler.ShowWarning((Maxnumberoffile - _vm.Files.Count) > 0
                                             ? string.Format("You can only upload {0} images more.",
                                                             (Maxnumberoffile - _vm.Files.Count).ToString(CultureInfo.InvariantCulture))
                                             : "The image library is full.");
                return;
            }

            int i = 0;
            foreach (var file in dlg.OpenFiles())
            {
                if (file.Length > Maxfilesize)
                {
                    if (!exist)
                    {
                        ErrorHandler.ShowWarning("There are at least one file exceed 1 MB.");
                        exist = true;
                    }
                }
                else
                {
                    _vm.TotalNumberOfFiles += 1;
                    var image = new BitmapImage();
                    //TODOHERE
                    image.StreamSource = file;
                    var fileViewModel = new FileViewModel(_vm, file.Length, dlg.SafeFileNames[i], image, file) { IsFinish = false };
                    _vm.Files.Add(fileViewModel);
                }
                i++;
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
        public event EventHandler AddCommandComplete;
    }
}
