using System.Windows;
using System.Windows.Input;
using Tudormobile.Wpf.Converters;
using Tudormobile.Wpf.Services;

namespace Tudormobile.Wpf.Commands;

/// <summary>
/// Provides a UI to display a message box to the user.
/// </summary>
public class MessageBoxCommand : ProxyCommand
{
    private readonly Action<MessageBoxResult> _resultAction;
    private readonly IDialogService _dialogService;

    /// <summary>
    /// Creates and initializes a new instance.
    /// </summary>
    /// <param name="resultAction">Action to take after message box is dismissed.</param>
    /// <param name="dialogService">Alternative dialog service to use.</param>
    public MessageBoxCommand(Action<MessageBoxResult>? resultAction = null, IDialogService? dialogService = null)
    {
        _resultAction = resultAction ?? (r => { });
        _dialogService = dialogService ?? new DialogService();  // TODO: Refactor this
    }

    /// <summary>
    /// Creates and initializes a new instance.
    /// </summary>
    /// <remarks>
    /// A parameterless constructor is needed to allow XAML instantiation.
    /// </remarks>
    public MessageBoxCommand() : this(null, null) { }

    /// <inheritdoc/>
    protected override void OnExecute(object? parameter)
    {
        if (parameter is MessageBoxParameters p)
        {
            showMessageBox(p);
        }
        else if (parameter is ICommand cmd)
        {
            showMessageBox(new MessageBoxParameters() { Command = cmd });
        }
        else if (parameter is String s)
        {
            showMessageBox((MessageBoxParameters)new MessageBoxParametersConverter().ConvertFrom(s)!);
        }
        else
        {
            base.OnExecute(parameter);
        }
    }
    private void showMessageBox(MessageBoxParameters p)
    {
        MessageBoxResult result;
        if (p.Icon == MessageBoxImage.None)
        {
            result = _dialogService.ShowMessageBox(p.Text, p.Caption ?? String.Empty, p.Button);
        }
        else
        {
            result = _dialogService.ShowMessageBox(p.Text, p.Caption, p.Button, p.Icon, p.Result);
        }
        //MessageBox.Show(p.Text);
        //MessageBox.Show(p.Text, p.Caption);
        //MessageBox.Show(p.Text, p.Caption, p.Button);
        //MessageBox.Show(p.Text, p.Caption, p.Button, p.Icon);
        //MessageBox.Show(p.Text, p.Caption, p.Button, p.Icon, p.Result);

        if (p.Command == null)
        {
            _resultAction.Invoke(result);
        }
        else
        {
            p.Command.Execute(result);
        }
        p.Result = result; // Update the bound property
    }
}

/// <summary>
/// MessageBoxCommandProxy is a Freezable that wraps a MessageBoxCommand to allow binding in XAML.
/// </summary>
public class MessageBoxCommandProxy : Freezable, ICommand
{
    private MessageBoxCommand? _messageBoxCommand;

    /// <inheritdoc/>
    public event EventHandler? CanExecuteChanged;

    /// <inheritdoc/>
    public bool CanExecute(object? parameter) => Command?.CanExecute(parameter) ?? false;

    /// <inheritdoc/>
    public void Execute(object? parameter)
    {
        var cmd = _messageBoxCommand ??= new MessageBoxCommand();
        if (parameter is MessageBoxParameters p)
        {
            p.Command = this.Command;
            cmd.Execute(p);
            return;
        }
        Command?.Execute(parameter);
    }

    /// <inheritdoc/>
    protected virtual void OnCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <inheritdoc/>
    protected override Freezable CreateInstanceCore()
    {
        return new MessageBoxCommandProxy();
    }

    /// <summary>
    /// Command to execute when the message box is dismissed.
    /// </summary>
    public ICommand Command
    {
        get { return (ICommand)GetValue(CommandProperty); }
        set { SetValue(CommandProperty, value); }
    }

    /// <inheritdoc/>
    public static readonly DependencyProperty CommandProperty = DependencyProperty
        .Register("Command",
        typeof(ICommand),
        typeof(MessageBoxCommandProxy),
        new PropertyMetadata(null));

}

