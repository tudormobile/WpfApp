using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Security.AccessControl;
using System.Windows;
using System.Windows.Data;
using Tudormobile.Wpf.Commands;

namespace Tudormobile.Wpf.Converters
{
    /// <summary>
    /// A 'Type Converter' and 'Value Converter' that converts a string in XAML to MessageBoxParameters.
    /// </summary>
    public class MessageBoxParametersConverter : TypeConverter, IValueConverter
    {
        /// <inheritdoc/>
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
            => sourceType == typeof(string);

        /// <inheritdoc/>
        public override bool CanConvertTo(ITypeDescriptorContext? context, [NotNullWhen(true)] Type? destinationType)
            => destinationType == typeof(MessageBoxParameters);

        /// <inheritdoc/>
        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            var parts = ((string)value).Split('|');
            var result = new MessageBoxParameters()
            {
                Text = parts[0],
                Caption = parts.Length > 1 ? parts[1] : null,
                Button = parts.Length > 2 ? Enum.Parse<MessageBoxButton>(parts[2]) : (MessageBoxButton)0,
                Icon = parts.Length > 3 ? Enum.Parse<MessageBoxImage>(parts[3]) : (MessageBoxImage)0,
                Result = parts.Length > 4 ? Enum.Parse<MessageBoxResult>(parts[4]) : (MessageBoxResult)0,
            };
            return result;
        }
        /// <inheritdoc/>
        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (value is MessageBoxParameters p)
            {
                return string.Join("|",
                [
                    p.Text ?? String.Empty,
                    p.Caption ?? String.Empty,
                    p.Button.ToString(),
                    p.Icon.ToString(),
                    p.Result.ToString(),
                ]);
            }
            throw new NotSupportedException($"Cannot convert to type '{destinationType.Name}'");
        }

        /// <summary>
        /// Convert from a string value to MessageBoxParameters.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <param name="targetType">Target type (must be MessageBoxParameters)</param>
        /// <param name="parameter">Conversion parameter (ignored).</param>
        /// <param name="culture">Conversion culture (ignored).</param>
        /// <returns>MessageBoxParameters configured by the string.</returns>
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => ConvertFrom(value);

        /// <summary>
        /// Convert from MessageBoxParameters back to a String.
        /// </summary>
        /// <param name="value">Value to convert (Must be MessageBoxParameters).</param>
        /// <param name="targetType">Target type (must be string).</param>
        /// <param name="parameter">Conversion parameter (ignored).</param>
        /// <param name="culture">Conversion culture (ignored).</param>
        /// <returns>String representation of the MessageBoxParameters.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => ConvertTo(value, targetType);
    }
}
