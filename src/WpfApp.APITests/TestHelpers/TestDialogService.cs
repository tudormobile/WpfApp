using Microsoft.Win32;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using Tudormobile.Wpf;
using Tudormobile.Wpf.Services;

namespace WpfAppAPITests
{
    [ExcludeFromCodeCoverage]
    public class TestDialogService : IDialogService
    {
        public MessageBoxResult Result { get; set; }
        public bool? FileDialogResult { get; set; }
        public string? FileDialogFileName { get; set; }
        public string Name { get; } = nameof(TestDialogService);
        public bool IsSingleton { get; } = false;

        public MessageBoxResult ShowMessageBox(
                    string? text,
                    string? caption = "",
                    MessageBoxButton button = MessageBoxButton.OK,
                    MessageBoxImage icon = MessageBoxImage.None,
                    MessageBoxResult result = MessageBoxResult.OK) => Result;

        public (bool?, FileDialog) ShowFileDialog(string? title, string? filter, string? filename, bool isSaveDialog = false)
        {
            FileDialog fd = isSaveDialog ? new SaveFileDialog() : new OpenFileDialog();
            fd.Title = title;
            fd.Filter = filter;
            fd.FileName = FileDialogFileName;
            return (FileDialogResult, fd);
        }

    }

}
