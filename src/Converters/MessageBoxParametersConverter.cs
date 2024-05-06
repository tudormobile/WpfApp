using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Security.AccessControl;
using System.Windows;
using Tudormobile.Wpf.Commands;

namespace Tudormobile.Wpf.Converters
{
    /// <summary>
    /// Converts a string in XAML to MessageBoxparameters.
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
            var parts = value?.ToString().Split('|');
            var result = new MessageBoxParameters()
            {
                Text = parts.Length > 0 ? parts[0] : string.Empty,
                Caption = parts.Length > 1 ? parts[1] : null,
                Button = parts.Length > 2 ? Enum.Parse<MessageBoxButton>(parts[2]) : null,
                Icon = parts.Length > 1 ? Enum.Parse<MessageBoxImage>(parts[3]) : null,
                Result = parts.Length > 4 ? Enum.Parse<MessageBoxResult>(parts[4]) : null,
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
                    p.Text,
                    p.Caption ?? String.Empty,
                    p.Button?.ToString()?? String.Empty,
                    p.Icon?.ToString()?? String.Empty,
                    p.Result?.ToString()?? String.Empty,
                });
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
