using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Tudormobile.Wpf.Services
{
    /// <inheritdoc/>
    internal class TextBoxService : ITextBoxService
    {
        private bool _registered = false;
        /// <inheritdoc/>
        public void Register()
        {
            if (!_registered)
            {
                _registered = true;
                EventManager.RegisterClassHandler(typeof(TextBox), TextBox.PreviewMouseLeftButtonDownEvent,
                    new MouseButtonEventHandler(ignoreMouseButton));
                EventManager.RegisterClassHandler(typeof(TextBox), TextBox.GotKeyboardFocusEvent,
                    new RoutedEventHandler(selectAll));
                EventManager.RegisterClassHandler(typeof(TextBox), TextBox.MouseDoubleClickEvent,
                    new RoutedEventHandler(selectAll));
            }
        }

        private void selectAll(object sender, RoutedEventArgs e) => (e.OriginalSource as TextBox)?.SelectAll();
        private void ignoreMouseButton(object sender, MouseButtonEventArgs e)
        {
            // Find the TextBox
            var parent = e.OriginalSource as DependencyObject;
            while (parent != null && parent is not TextBox)
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            if (parent is TextBox textBox)
            {
                if (!textBox.IsKeyboardFocusWithin)
                {
                    textBox.Focus();
                    // stop future processing of this event
                    e.Handled = true;
                }
                else
                {
                    if (e.ClickCount > 2)
                    {
                        // On triple-click, select all text
                        textBox.SelectAll();
                    }
                }
            }
        }
    }
}
