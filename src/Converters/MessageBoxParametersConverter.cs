using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Security.AccessControl;
using System.Windows;
using Tudormobile.Wpf.Commands;

namespace Tudormobile.Wpf.Converters
{
    /// <summary>
    /// Converts a string in XAML to MessageBoxParameters.
    /// </summary>
    public class MessageBoxParametersConverter : TypeConverter
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
                return string.Join("|", new string[]
                {
                    p.Text ?? String.Empty,
                    p.Caption ?? String.Empty,
                    p.Button.ToString(),
                    p.Icon.ToString(),
                    p.Result.ToString(),
                });
            }
            throw new NotSupportedException($"Cannot convert to type '{destinationType.Name}'");
        }
    }
}
