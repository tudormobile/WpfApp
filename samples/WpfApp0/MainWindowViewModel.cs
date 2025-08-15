using System.Windows.Input;

namespace WpfApp0;

public class MainWindowViewModel
{
    public string Message => "Hello from the MainWindowViewModel!";
    public ICommand ShowMessageCommand { get; init; }
    public MainWindowViewModel(IMessageBoxService messageBoxService /*parameter will be provided by DI*/)
    {
        ShowMessageCommand = new MessageBoxCommand(messageBoxService);
    }

}



