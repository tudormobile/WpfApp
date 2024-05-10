using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tudormobile.Wpf.Interfaces;

namespace Tudormobile.Wpf.Services;

/// <summary>
/// Application Help System service.
/// </summary>
internal class HelpService : WpfAppServiceBase, IHelpService
{
    private readonly Dictionary<UIElement, Uri> _registry = [];

    /// <inheritdoc/>
    public void Register(UIElement element, Uri uri)
    {
        _registry.Add(element, uri);
        element.CommandBindings.Add(new CommandBinding(ApplicationCommands.Help, showHelp));
    }

    /// <inheritdoc/>
    public void ShowHelp(Uri uri)
    {
        // TODO: Support lots of URIs
        Process.Start(new ProcessStartInfo() { FileName = uri.ToString(), UseShellExecute = true });
    }

    private void showHelp(object sender, ExecutedRoutedEventArgs e)
    {
        if (sender is UIElement uie && _registry.TryGetValue(uie, out var uri))
        {
            ShowHelp(uri);
        }
    }
}
