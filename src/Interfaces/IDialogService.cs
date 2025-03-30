using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tudormobile.Wpf.Services;

/// <summary>
/// Dialog management service.
/// </summary>
public interface IDialogService : IWpfAppService
{
    /// <summary>
    /// Displays a message box to the user.
    /// </summary>
    /// <param name="text">A String that specifies the text to display.</param>
    /// <param name="caption">A String that specifies the title bar caption to display.</param>
    /// <param name="button">A MessageBoxButton value that specifies which button or buttons to display.</param>
    /// <param name="icon">A MessageBoxImage value that specifies the icon to display.</param>
    /// <param name="result">A MessageBoxResult value that specifies the default result of the message box.</param>
    /// <returns>A MessageBoxResult value that specifies which message box button is clicked by the user.</returns>
    [ExcludeFromCodeCoverage]
    public MessageBoxResult ShowMessageBox(string? text,
        string? caption = "",
        MessageBoxButton button = MessageBoxButton.OK,
        MessageBoxImage icon = MessageBoxImage.None,
        MessageBoxResult result = MessageBoxResult.OK) => MessageBox.Show(text, caption, button, icon, result);

    /// <summary>
    /// Display and Open or Save file dialog to the user.
    /// </summary>
    /// <param name="title">The text shown in the title bar of the file dialog, or null if a localized default from the operating system is used (typically something like "Save As" or "Open").</param>
    /// <param name="filter">A String that contains the filter. The default is Empty, which means that no filter is applied and all file types are displayed.</param>
    /// <param name="filename">A String that is the full path of the file selected in the file dialog. The default is Empty.</param>
    /// <param name="isSaveDialog">True if Save file dialog; otherwise (false). The default value is (false).</param>
    /// <returns></returns>
    [ExcludeFromCodeCoverage]
    public (bool?, FileDialog) ShowFileDialog(string? title, string? filter, string? filename, bool isSaveDialog = false)
    {
        FileDialog fd = isSaveDialog ? new SaveFileDialog() : new OpenFileDialog();
        fd.Title = title;
        fd.Filter = filter;
        fd.FileName = filename;
        return (fd.ShowDialog(), fd);
    }
}
