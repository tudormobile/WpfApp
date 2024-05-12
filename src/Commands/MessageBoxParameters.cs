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
    /// <summary>
    /// Message box (dialog) parameters.
    /// </summary>
    [TypeConverter(typeof(MessageBoxParametersConverter))]
    public class MessageBoxParameters : DependencyObject
    {
        /// <summary>
        /// Gets or sets the text to display.
        /// </summary>
        public string? Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text to display.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty
            .Register("Text", typeof(string), typeof(MessageBoxParameters), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the title bar caption to display.
        /// </summary>
        public string? Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        /// <summary>
        /// Gets or sets the title bar caption to display.
        /// </summary>
        public static readonly DependencyProperty CaptionProperty = DependencyProperty
            .Register(nameof(Caption), typeof(string), typeof(MessageBoxParameters), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public MessageBoxImage Icon
        {
            get { return (MessageBoxImage)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// Gets or sets the icon to display.
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty
            .Register(nameof(Icon), typeof(MessageBoxImage), typeof(MessageBoxParameters), new PropertyMetadata((MessageBoxImage)0));

        /// <summary>
        /// Gets or sets the button or buttons to display.
        /// </summary>
        public MessageBoxButton Button
        {
            get { return (MessageBoxButton)GetValue(ButtonProperty); }
            set { SetValue(ButtonProperty, value); }
        }

        /// <summary>
        /// Gets or sets the button or buttons to display.
        /// </summary>
        public static readonly DependencyProperty ButtonProperty = DependencyProperty
            .Register(nameof(Button), typeof(MessageBoxButton), typeof(MessageBoxParameters), new PropertyMetadata(MessageBoxButton.OK));

        /// <summary>
        /// Gets or sets the default result of the message box.
        /// </summary>
        public MessageBoxResult Result
        {
            get { return (MessageBoxResult)GetValue(ResultProperty); }
            set { SetValue(ResultProperty, value); }
        }

        /// <summary>
        /// Gets or sets the default result of the message box.
        /// </summary>
        public static readonly DependencyProperty ResultProperty = DependencyProperty
            .Register(nameof(Result), typeof(MessageBoxResult), typeof(MessageBoxParameters), new PropertyMetadata(MessageBoxResult.None));


        /// <summary>
        /// Gets or sets the message box results command.
        /// </summary>
        public ICommand? Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the message box results command.
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty
            .Register(nameof(Command), typeof(ICommand), typeof(MessageBoxParameters), new PropertyMetadata(null));

    }
}
