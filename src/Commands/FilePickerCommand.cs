using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tudormobile.Wpf.Commands
{
    public class SaveFilePickerCommand : FilePickerCommand
    {
        public SaveFilePickerCommand() : base(true) { }
    }
    public class OpenFilePickerCommand : FilePickerCommand { }
    /// <summary>
    /// Provides a UI to choose a file in the file system.
    /// </summary>
    public class FilePickerCommand(bool isSaveCommand = false) : ProxyCommand
    {
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

            FileDialog fd = isSaveCommand ? new SaveFileDialog() : new OpenFileDialog();
            fd.Title = title;
            fd.Filter = filter;
            fd.FileName = filename;
            fd.Filter = filter;
            if (fd.ShowDialog() == true)
            {
                command?.Execute(fd.FileName);
            }

        }
    }
}
