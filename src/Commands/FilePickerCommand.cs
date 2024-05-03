using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tudormobile.Wpf.Commands
{
    /// <summary>
    /// Provides a UI to choose a file in the file system.
    /// </summary>
    public class FilePickerCommand : ProxyCommand
    {
        protected override void OnExecute(object? parameter)
        {
            switch (parameter)
            {
                case FilePickerParameters pickerParameters:
                    onExecute(pickerParameters.Title, pickerParameters.Filter, pickerParameters.Filename, pickerParameters.Command);
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
            var ofd = new OpenFileDialog()
            {
                Title = title,
                Filter = filter,
                FileName = filename,
            };
            if (ofd.ShowDialog() == true)
            {
                command?.Execute(ofd.FileName);
            }

        }
    }
}
