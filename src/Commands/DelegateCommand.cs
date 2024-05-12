using System.Windows.Input;

namespace Tudormobile.Wpf.Commands;

// ICommand implementation that invokes a delegate.
//
// Note: | This implementation is internal-only used for matching methods to ICommands via
//       | attributes and reflection. Not written for public exposure. Other libraries provide
//       | 'RelayCommand' and 'DelegateCommand' implementations.
internal class DelegateCommand(Delegate execute, Delegate? canExecute = null) : ICommand
{
    protected Delegate executeDelegate => execute;
    protected Delegate? canExecuteDelegate => canExecute;

    // ICommand Interface
    public event EventHandler? CanExecuteChanged;
    public bool CanExecute(object? parameter) => OnCanExecute(parameter);
    public void Execute(object? parameter) => OnExecute(parameter);

    // Extension points
    protected virtual bool OnCanExecute(object? parameter) => canExecuteDelegate?.DynamicInvoke() as bool? ?? true;
    protected virtual void OnExecute(object? parameter) => executeDelegate.DynamicInvoke();
    protected virtual void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}

// ICommand implementation that invokes a type-safe delegate.
//
// Note: | This implementation is internal-only used for matching methods to ICommands via
//       | attributes and reflection. Not written for public exposure. Other libraries provide
//       | 'RelayCommand' and 'DelegateCommand' implementations.
internal class DelegateCommand<T>(Delegate execute, Delegate? canExecute = null) : DelegateCommand(execute, canExecute)
{
    protected override void OnExecute(object? parameter) => executeDelegate.DynamicInvoke((T?)parameter);
    protected override bool OnCanExecute(object? parameter) => canExecuteDelegate?.DynamicInvoke((T?)parameter) as bool? ?? true;
}

