using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Tudormobile.Wpf.Commands
{
    public class FilePickerParameters : DependencyObject
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty
            .Register("Title", typeof(string), typeof(FilePickerParameters), new PropertyMetadata(String.Empty));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty
            .Register(nameof(Command), typeof(string), typeof(FilePickerParameters), new PropertyMetadata(null));
        public string Filter
        {
            get { return (string)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        public static readonly DependencyProperty FilterProperty = DependencyProperty
            .Register(nameof(Filter), typeof(string), typeof(FilePickerParameters), new PropertyMetadata(String.Empty));
        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        public static readonly DependencyProperty FileNameProperty = DependencyProperty
            .Register(nameof(FileName), typeof(string), typeof(FilePickerParameters), new PropertyMetadata(String.Empty));


    }
}
