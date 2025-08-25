using System.Windows;
using System.Windows.Input;
using Tudormobile.Wpf.Services;

namespace Tudormobile.Wpf.Commands;

/// <summary>
/// Provides a UI to choose a file in the file system to save.
/// </summary>
public class SaveFilePickerCommand : FilePickerCommand
{
    /// <summary>
    /// Creates and initializes a new instance.
    /// </summary>
    public SaveFilePickerCommand(IDialogService? dialogService = null) : base(true, dialogService) { }

    /// <summary>
    /// Creates and initializes a new instance.
    /// </summary>
    /// <remarks>
    /// A parameterless constructor is needed to allow XAML instantiation.
    /// </remarks>
    public SaveFilePickerCommand() : this(null) { }
}

/// <summary>
/// Provides a UI to choose a file in the file system to open.
/// </summary>
public class OpenFilePickerCommand : FilePickerCommand
{
    /// <summary>
    /// Creates and initializes a new instance.
    /// </summary>
    public OpenFilePickerCommand(IDialogService? dialogService = null) : base(false, dialogService) { }

    /// <summary>
    /// Creates and initializes a new instance.
    /// </summary>
    /// <remarks>
    /// A parameterless constructor is needed to allow XAML instantiation.
    /// </remarks>
    public OpenFilePickerCommand() : this(null) { }
}

/// <summary>
/// Provides a UI to choose a file in the file system.
/// </summary>
public class FilePickerCommand(bool isSaveCommand = false, IDialogService? dialogService = null) : ProxyCommand
{
    private readonly IDialogService _dialogService = dialogService ?? new DialogService();
    /// <inheritdoc/>
    protected override void OnExecute(object? parameter)
    {
        switch (parameter)
        {
            case FilePickerParameters pickerParameters:
                onExecute(pickerParameters.Title,
                    pickerParameters.Filter,
                    pickerParameters.FileName,
                    pickerParameters.Command,
                    (s) => pickerParameters.FileName = s
                    );
                break;
            case ICommand command:
                onExecute(null, null, null, command);
                break;
            default:
                base.OnExecute(parameter);
                break;
        }
    }

    private void onExecute(string? title, string? filter, string? filename, ICommand? command, Action<string>? action = null)
    {
        var (result, fd) = _dialogService.ShowFileDialog(title, filter, filename, isSaveDialog: isSaveCommand);
        if (result == true)
        {
            command?.Execute(fd.FileName);
            action?.Invoke(fd.FileName);
        }
    }
}

/// <summary>
/// Base class for file picker command proxies.
/// </summary>
public abstract class FilePickerCommandProxy : Freezable, ICommand
{
    private FilePickerCommand? _filePickerCommand;
    private FilePickerParameters? _filePickerParameters;

    /// <summary>
    /// Creates the file picker command.
    /// </summary>
    /// <returns>File picker command.</returns>
    public abstract FilePickerCommand CreateCommand();

    /// <inheritdoc/>
    public bool CanExecute(object? parameter)
    {
        var cmd = _filePickerCommand ??= CreateCommand();
        var p = _filePickerParameters ??= new FilePickerParameters();
        p.Command = this.Command;
        return cmd.CanExecute(parameter ?? p);
    }

    /// <inheritdoc/>
    public void Execute(object? parameter)
    {
        var cmd = _filePickerCommand ??= CreateCommand();
        var p = _filePickerParameters ??= new FilePickerParameters();
        p.Command = this.Command;
        cmd.Execute(parameter ?? p);
    }

    /// <summary>
    /// Command to execute when a file is selected.
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
        typeof(FilePickerCommandProxy),
        new UIPropertyMetadata(null));

    /// <inheritdoc/>
    public event EventHandler? CanExecuteChanged;

    /// <inheritdoc/>
    protected virtual void OnCanExecuteChanged()
        => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}

/// <summary>
/// OpenFilePicker command proxy.
/// </summary>
public class OpenFilePickerCommandProxy : FilePickerCommandProxy
{
    /// <inheritdoc/>
    public override FilePickerCommand CreateCommand() => new OpenFilePickerCommand();

    /// <inheritdoc/>
    protected override Freezable CreateInstanceCore() => new OpenFilePickerCommandProxy();
}

/// <summary>
/// SaveFilePicker command proxy.
/// </summary>
public class SaveFilePickerCommandProxy : FilePickerCommandProxy
{
    /// <inheritdoc/>
    public override FilePickerCommand CreateCommand() => new SaveFilePickerCommand();

    /// <inheritdoc/>
    protected override Freezable CreateInstanceCore() => new SaveFilePickerCommandProxy();
}

