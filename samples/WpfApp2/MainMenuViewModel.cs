using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using Tudormobile.Wpf;
using Tudormobile.Wpf.Commands;

namespace WpfApp2
{
    public class MainMenuViewModel
    {
        public ObservableCollection<Window>? Windows { get; }
        public ICommand OpenCommand { get; } = new Command(filename => MessageBox.Show($"Open '{filename}'"));
        public ICommand SaveAsCommand { get; } = new Command(filename => MessageBox.Show($"Save As '{filename}'"));
        public ICommand SaveAsPicker { get; } = new SaveFilePickerCommand();
        public ICommand SayHelloCommand { get; } = new MessageBoxCommand(resut => MessageBox.Show($"You chose {resut}"));
        public string Test { get; } = "This is the test property";
        public MessageBoxParameters MessageBoxParameters { get; } = new MessageBoxParameters()
        {
            Text = "This is from the bound paramters",
        };
        public FilePickerParameters SaveAsParameters { get; } = new FilePickerParameters()
        {
            Title = "Save the File as Title Goes Here",
        };
        public MainMenuViewModel()
        {
            Windows = WpfApp.Current?.Windows;
        }

        internal class Command : ICommand
        {
            private readonly Action<object?> _execute;
            private readonly Func<object?, bool>? _canExecute;
            public event EventHandler? CanExecuteChanged;

            public Command(Action<object?> execute, Func<object?, bool>? canExecute = null)
            {
                _execute = execute;
                _canExecute = canExecute;
            }

            public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

            public void Execute(object? parameter) => _execute?.Invoke(parameter);
            protected virtual void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        }
    }
}