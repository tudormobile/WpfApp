using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tudormobile.Wpf.Services;

namespace Tudormobile.Wpf.Commands
{
    /// <summary>
    /// Provides a UI to choose a file in the file system to save.
    /// </summary>
    public class SaveFilePickerCommand : FilePickerCommand
    {
        /// <summary>
        /// Creates and initializes a new instance.
        /// </summary>
        public SaveFilePickerCommand(IDialogService? dialogService = null) : base(true, dialogService) { }
    }

    /// <summary>
    /// Provides a UI to choose a file in the file system to open.
    /// </summary>
    public class OpenFilePickerCommand : FilePickerCommand 
    {
        /// <summary>
        /// Creates and initializes a new instance.
        /// </summary>
        public OpenFilePickerCommand(IDialogService? dialogService = null) : base(false, dialogService) { }
    }

    /// <summary>
    /// Provides a UI to choose a file in the file system.
    /// </summary>
    public class FilePickerCommand(bool isSaveCommand = false, IDialogService? dialogService = null) : ProxyCommand
    {
        private readonly IDialogService _dialogService = dialogService ?? new DialogService();
        /// <inheritdoc/>
        protected override void OnExecute(object? parameter)
        {
            switch (parameter)
            {
                case FilePickerParameters pickerParameters:
                    onExecute(pickerParameters.Title, pickerParameters.Filter, pickerParameters.FileName, pickerParameters.Command);
                    break;
                case ICommand command:
                    onExecute(null, null, null, command);
                    break;
                default:
                    base.OnExecute(parameter);
                    break;
            }
        }

        private void onExecute(string? title, string? filter, string? filename, ICommand? command)
        {
            var (result, fd) = _dialogService.ShowFileDialog(title, filter, filename, isSaveDialog: isSaveCommand);
            if (result == true)
            {
                command?.Execute(fd.FileName);
            }
        }
    }
}
