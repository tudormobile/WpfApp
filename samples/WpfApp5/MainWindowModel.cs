using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Tudormobile.Wpf.Commands;
using Tudormobile.Wpf.Services;

namespace WpfApp5
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        private string _saveFilename = "newname_from_data_model.txt";
        private IDialogService _dialogService;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Message => @"Demonstration of the use of 'Commands' (ICommand) using the IDialogService: MessageBox, Open/Save Dialogs, etc.";
        public string OpenFileTitle => "[ Custom Title = Select a file to Open ]";
        public string SaveFilename
        {
            get { return _saveFilename; }
            set { _saveFilename = value; OnPropertyChanged(nameof(SaveFilename)); }
        }
        public ICommand YesNoCancelCommand { get; set; }
        public ICommand OpenCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand ShowMessageBoxCommand => new MessageBoxCommand(r => _dialogService.ShowMessageBox(r.ToString()));
        public MessageBoxParameters ShowMessageBoxCommandParameter => new MessageBoxParameters()
        {
            Text = "This is a message box from the ViewModel.",
            Caption = "Message Box from ViewModel",
            Button = MessageBoxButton.OKCancel,
            Icon = MessageBoxImage.Information,
            Result = MessageBoxResult.OK,

        };

        public MainWindowModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            OpenCommand = new FileCommandImpl(dialogService);
            SaveCommand = new FileCommandImpl(dialogService);
            YesNoCancelCommand = new FileCommandImpl(dialogService);
        }


        internal class FileCommandImpl : ICommand
        {
            private IDialogService _dialogService;

            public FileCommandImpl(IDialogService dialogService)
            {
                _dialogService = dialogService;
            }

            public event EventHandler? CanExecuteChanged;
            public bool CanExecute(object? parameter) => true;
            public void Execute(object? parameter)
            {
                if (parameter is String filename)
                {
                    _dialogService.ShowMessageBox($"You selected the file: {filename}", "File Selected");
                }
                if (parameter is MessageBoxResult result)
                {
                    _dialogService.ShowMessageBox($"You selected the button: {result}", "Button Selected");
                }
            }
            protected virtual void OnCanExecuteChanged()
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    // Move these to the library project for reuse????
    public class BindingProxy : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty = DependencyProperty
            .Register("Data", typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));
    }

}
