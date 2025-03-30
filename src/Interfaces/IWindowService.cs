using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tudormobile.Wpf.Services;

/// <summary>
/// Window management service.
/// </summary>
public interface IWindowService : IWpfAppService
{
    /// <summary>
    /// Opens a window and returns without waiting for the newly opened window to close.
    /// </summary>
    /// <param name="window">Window to open.</param>
    [ExcludeFromCodeCoverage]
    public void ShowWindow(Window window) => window.Show();

    /// <summary>
    /// Opens a window and returns only when the newly opened window is closed.
    /// </summary>
    /// <param name="window">Window to show.</param>
    /// <returns>A bool? value that specifies whether the activity was accepted (true) or canceled (false)</returns>
    [ExcludeFromCodeCoverage]
    public bool? ShowDialog(Window window) => window.ShowDialog();
}
