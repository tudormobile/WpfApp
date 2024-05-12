using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Tudormobile.Wpf.Commands
{
    /// <summary>
    /// File Picker (dialog) parameters.
    /// </summary>
    public class FilePickerParameters : DependencyObject
    {
        /// <summary>
        /// Gets or sets the file dialog box title.
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the file dialog box title.
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty
            .Register(nameof(Title), typeof(string), typeof(FilePickerParameters), new PropertyMetadata(String.Empty));

        /// <summary>
        /// Gets or sets the file dialog results command.
        /// </summary>
        public ICommand? Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the file dialog results command.
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty
            .Register(nameof(Command), typeof(ICommand), typeof(FilePickerParameters), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the current file name filter string, which determines the choices that appear in the "Save as file type" or "Files of type" box in the dialog box.
        /// </summary>
        public string Filter
        {
            get { return (string)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        /// <summary>
        /// Gets or sets the current file name filter string, which determines the choices that appear in the "Save as file type" or "Files of type" box in the dialog box.
        /// </summary>
        public static readonly DependencyProperty FilterProperty = DependencyProperty
            .Register(nameof(Filter), typeof(string), typeof(FilePickerParameters), new PropertyMetadata(String.Empty));

        /// <summary>
        /// Gets or sets a string containing the file name selected in the file dialog box.
        /// </summary>
        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets a string containing the file name selected in the file dialog box.
        /// </summary>
        public static readonly DependencyProperty FileNameProperty = DependencyProperty
            .Register(nameof(FileName), typeof(string), typeof(FilePickerParameters), new PropertyMetadata(String.Empty));

    }
}
