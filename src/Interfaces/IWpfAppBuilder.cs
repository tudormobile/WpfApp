using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Tudormobile.Wpf;

/// <summary>
/// Defines a class that provides the mechanisms to configure and manage a Wpf application.
/// </summary>
public interface IWpfAppBuilder : IBuilder<IWpfApp>
{
    /// <summary>
    /// Host builder
    /// </summary>
    IHostBuilder HostBuilder { get; }

    /// <summary>
    /// Use Host container.
    /// </summary>
    /// <param name="useHosting">True to add hosting; use false to remove hosting.</param>
    /// <returns>Fluent reference to the builder.</returns>
    public IWpfAppBuilder AddHosting(bool useHosting = true);

    /// <summary>
    /// Adds a class to represent the Main Window of the application.
    /// </summary>
    /// <typeparam name="T">Type of main window.</typeparam>
    /// <returns>Fluent reference to the builder.</returns>
    public IWpfAppBuilder AddMainWindow<T>() where T : Window;

    /// <summary>
    /// Adds a Window and associated View Model to the application container.
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    /// <typeparam name="TViewModel"></typeparam>
    /// <returns>Fluent reference to the builder.</returns>
    public IWpfAppBuilder AddWindow<TView, TViewModel>() where TView : Control where TViewModel : class;

    /// <summary>
    /// Adds a View and associated ViewModel to the application container.
    /// </summary>
    /// <typeparam name="TView">Type of the view object.</typeparam>
    /// <typeparam name="TViewModel">Type of the View Model object.</typeparam>
    /// <returns>Fluent reference to the builder.</returns>
    public IWpfAppBuilder AddView<TView, TViewModel>() where TView : FrameworkElement where TViewModel : class;
}
