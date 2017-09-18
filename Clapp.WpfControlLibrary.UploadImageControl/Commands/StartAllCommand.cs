﻿using System;
using System.Windows.Input;
using Clapp.WpfControlLibrary.UploadImageControl.ViewModels;

namespace Clapp.WpfControlLibrary.UploadImageControl.Commands
{
    public class StartAllCommand : ICommand
    {
        private readonly UploadViewModel _vm;
        private const int MaxNumberOfFile = 5;
        public int DownloadingNumber { get; set; }

        public StartAllCommand(UploadViewModel vm)
        {
            _vm = vm;
        }

        public void Execute(object parameter)
        {
            foreach (var file in _vm.Files)
            {
                if (!file.IsComplete && DownloadingNumber <= MaxNumberOfFile && !file.IsRunning)
                {
                    file.IsRunning = true;
                    file.Upload.Execute(null);
                    DownloadingNumber++;
                    if (DownloadingNumber >= MaxNumberOfFile)
                    {
                        break;
                    }
                }
            }
        }
      

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
        public event EventHandler StartAllCommandComplete;
    }
}
