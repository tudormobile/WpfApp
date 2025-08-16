using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
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

    /// <summary>
    /// Collection of application windows created by the service.
    /// </summary>
    /// <remarks>
    /// The service will attempt to maintain the application MainWindow as the first item in this collection. Windows
    /// created by the service will be added automatically to this collection, and removed when they are closed.
    /// </remarks>
    public ObservableCollection<Window> Windows { get; }

    /// <summary>
    /// Creates a new application window to be managed by the service. The DataContext of the window will be set
    /// to the specified view model type.
    /// </summary>
    /// <typeparam name="TView">The application window type.</typeparam>
    /// <typeparam name="TViewModel">The type of view model to use as the Data Context for the window.</typeparam>
    /// <returns>A reference to the created window.</returns>
    public Window CreateWindow<TView, TViewModel>() where TViewModel : class where TView : Window;

    /// <summary>
    /// Creates a new application window to be managed by the service. The DataContext of the window will be
    /// inferred from the view type if set via the DI container for the Window.
    /// </summary>
    /// <typeparam name="TView">The application window type.</typeparam>
    /// <returns>A reference to the created window.</returns>
    public Window CreateWindow<TView>() where TView : Window;

    /// <summary>
    /// Creates a new application window to be managed by the service. The DataContext of the window will be
    /// inferred from the view type if set via the DI container for the Window.
    /// </summary>
    /// <param name="windowType">The application window type.</param>
    /// <returns>A reference to the created window.</returns>
    public Window CreateWindow(Type windowType);

    /// <summary>
    /// Creates a new application window to be managed by the service. The DataContext of the window will be
    /// inferred from the view type if set via the DI container for the Window.
    /// </summary>
    /// <param name="windowType">The application window type.</param>
    /// <param name="modelType">The type of view model to use as the Data Context for the window.</param>
    /// <returns></returns>
    public Window CreateWindow(Type windowType, Type modelType);


}
