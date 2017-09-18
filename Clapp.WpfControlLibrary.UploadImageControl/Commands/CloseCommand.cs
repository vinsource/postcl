using System;
using System.Windows.Input;
using Clapp.WpfControlLibrary.UploadImageControl.Helpers;
using Clapp.WpfControlLibrary.UploadImageControl.ViewModels;

namespace Clapp.WpfControlLibrary.UploadImageControl.Commands
{
    public class CloseCommand : ICommand
    {
        public CloseCommand(UploadViewModel vm)
        {
        }

        public void Execute(object parameter)
        {
            if(CloseCommandComplete!=null)
            {
                CloseCommandComplete(null, null);
            }
            //HtmlHelper.CloseForm();
        }

     
        public event EventHandler CanExecuteChanged;
        public event EventHandler CloseCommandComplete;


        public bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
