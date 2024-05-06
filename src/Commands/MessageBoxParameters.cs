using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tudormobile.Wpf.Converters;

namespace Tudormobile.Wpf.Commands
{
    [TypeConverter(typeof(MessageBoxParametersConverter))]
    public class MessageBoxParameters : DependencyObject
    {
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty
            .Register("Text", typeof(string), typeof(MessageBoxParameters), new PropertyMetadata(null));

        public string? Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public static readonly DependencyProperty CaptionProperty = DependencyProperty
            .Register(nameof(Caption), typeof(string), typeof(MessageBoxParameters), new PropertyMetadata(null));

        public MessageBoxImage? Icon
        {
            get { return (MessageBoxImage?)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty
            .Register(nameof(Icon), typeof(MessageBoxImage), typeof(MessageBoxParameters), new PropertyMetadata(null));
        public MessageBoxButton? Button
        {
            get { return (MessageBoxButton?)GetValue(ButtonProperty); }
            set { SetValue(ButtonProperty, value); }
        }

        public static readonly DependencyProperty ButtonProperty = DependencyProperty
            .Register(nameof(Button), typeof(MessageBoxButton), typeof(MessageBoxParameters), new PropertyMetadata(null));
        public MessageBoxResult? Result
        {
            get { return (MessageBoxResult?)GetValue(ResultProperty); }
            set { SetValue(ResultProperty, value); }
        }

        public static readonly DependencyProperty ResultProperty = DependencyProperty
            .Register(nameof(Result), typeof(MessageBoxResult), typeof(MessageBoxParameters), new PropertyMetadata(null));
        public ICommand? Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty
            .Register(nameof(Command), typeof(ICommand), typeof(MessageBoxParameters), new PropertyMetadata(null));

    }
}
