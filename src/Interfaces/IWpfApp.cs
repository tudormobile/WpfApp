using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tudormobile.Wpf.Services;

namespace Tudormobile.Wpf;

/// <summary>
/// Defines an interface to manage a Wpf Application.
/// </summary>
/// <remarks>
/// The IWpfApp interface includes mechanisms to configure a Wpf application,
/// manage the application lifecycle, application level events, application services, 
/// and serves as a dependency injection container for a Wpf application.
/// </remarks>
public interface IWpfApp
{
    // Startup and Shutdown

    /// <summary>
    /// Starts the application host.
    /// </summary>
    /// <typeparam name="T">Main Window type.</typeparam>
    /// <returns>A Task that will be completed when the Host starts.</returns>
    public Task Start<T>() where T : Window;

    /// <summary>
    /// Starts the application host.
    /// </summary>
    /// <returns>A Task that will be completed when the Host starts.</returns>
    public Task Start();

    // Window Management

    /// <summary>
    /// Collection of application windows managed by the host.
    /// </summary>
    public ObservableCollection<Window> Windows { get; }

    /// <summary>
    /// Creates a new application window to be managed by the host.
    /// </summary>
    /// <typeparam name="TView">The application window type.</typeparam>
    /// <typeparam name="TViewModel">The type of view model to use as the Data Context for the window.</typeparam>
    /// <returns>A reference to the created window.</returns>
    public Window CreateWindow<TView, TViewModel>() where TViewModel : class where TView : Window;

    /// <summary>
    /// Represents the command line and its arguments.
    /// </summary>
    public ICommandLine CommandLine { get; }

    /// <summary>
    /// Returns an instance of a dialog service.
    /// </summary>
    public IDialogService DialogService { get; }

    /// <summary>
    /// Creates a view, view model, and sets the data context of the view to the view model.
    /// </summary>
    /// <typeparam name="TView">Type of view.</typeparam>
    /// <typeparam name="TViewModel">Type of view model.</typeparam>
    /// <returns>A framework element of type TView.</returns>
    public FrameworkElement CreateView<TView, TViewModel>() where TViewModel : class where TView : FrameworkElement;

    /// <summary>
    /// Creates a view, view model, and sets the data context of the view to the view model.
    /// </summary>
    /// <typeparam name="TView">Type of view.</typeparam>
    /// <returns>A framework element of type TView.</returns>
    /// <remarks>
    /// The view model type is inferred by convention or registration.
    /// </remarks>
    public FrameworkElement CreateView<TView>() where TView : FrameworkElement;

    /// <summary>
    /// Creates a view, view model, and sets the data context of the view to the view model.
    /// </summary>
    /// <param name="viewType">Type of view.</param>
    /// <param name="viewModelType">Type of view model.</param>
    /// <returns>A framework element of type TView.</returns>
    /// <remarks>
    /// The view model type is inferred by convention or registration.
    /// </remarks>
    public FrameworkElement CreateView(Type viewType, Type? viewModelType = null);
}
