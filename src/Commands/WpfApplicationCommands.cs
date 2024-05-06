using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tudormobile.Wpf.Commands;

/// <summary>
/// Defines a standard set of WpfApp Commands.
/// </summary>
public static class WpfApplicationCommands
{
    private static Lazy<RoutedUICommand> _exitCommand = new(() => new RoutedUICommand(nameof(Exit), nameof(Exit), typeof(WpfApplicationCommands)));
    private static Lazy<RoutedUICommand> _closeAllCommand = new(() => new RoutedUICommand(nameof(CloseAll), nameof(CloseAll), typeof(WpfApplicationCommands)));
    private static Lazy<RoutedUICommand> _selectWindowCommand = new(() => new RoutedUICommand(nameof(SelectWindow), nameof(SelectWindow), typeof(WpfApplicationCommands)));
    private static Lazy<RoutedUICommand> _openCommand = new(() => new RoutedUICommand(nameof(Open), nameof(Open), typeof(WpfApplicationCommands)));
 
    /// <summary>
    /// Exit the Application.
    /// </summary>
    public static RoutedUICommand Exit => _exitCommand.Value;

    /// <summary>
    /// Close all Windows.
    /// </summary>
    public static RoutedUICommand CloseAll => _closeAllCommand.Value;

    /// <summary>
    /// Select a window.
    /// </summary>
    public static RoutedUICommand SelectWindow => _selectWindowCommand.Value;

    /// <summary>
    /// Open a file dialog and choose a file.
    /// </summary>
    public static RoutedUICommand Open => _openCommand.Value;
}
