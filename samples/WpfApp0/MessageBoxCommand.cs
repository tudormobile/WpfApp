using System.Windows.Input;

namespace WpfApp0;

public class MessageBoxCommand(IMessageBoxService messageBoxService) : ICommand
{
    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => true;
    public void Execute(object? parameter) => messageBoxService.ShowMessage(parameter as string ?? "No message provided");
    protected virtual void OnCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}



