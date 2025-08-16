using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;

namespace Tudormobile.Wpf.Services;

/// <summary>
/// Window management services.
/// </summary>
internal class WindowService : WpfAppServiceBase, IWindowService
{
    /// <inheritdoc/>
    public ObservableCollection<Window> Windows { get; } = [];

    /// <inheritdoc/>
    public Window CreateWindow<TView, TViewModel>() where TView : Window where TViewModel : class
    {
        var serviceProvider = WpfApplication.Current.Services;
        var w = serviceProvider.GetRequiredService<TView>();
        var m = serviceProvider.GetRequiredService(typeof(TViewModel));
        TrySetDataContext(w, m);
        return RegisterWindow(w);
    }

    /// <inheritdoc/>
    public Window CreateWindow<TView>() where TView : Window
    {
        var serviceProvider = WpfApplication.Current.Services;
        var w = serviceProvider.GetRequiredService<TView>();
        TrySetDataContext(w);
        return RegisterWindow(w);
    }

    /// <inheritdoc/>
    public Window CreateWindow(Type windowType) => CreateWindow(windowType, null);

    /// <inheritdoc/>
    public Window CreateWindow(Type windowType, Type? modelType)
    {
        var serviceProvider = WpfApplication.Current.Services;
        var w = (Window)serviceProvider.GetRequiredService(windowType);
        object? m = null;
        if (modelType != null)
        {
            m = serviceProvider.GetService(modelType);
        }
        TrySetDataContext(w, m);
        return RegisterWindow(w);
    }

    internal Window RegisterWindow(Window window)
    {
        window.Closed += (s, e) => Windows.Remove((Window)s!);
        Windows.Add(window);
        return window;
    }

    internal static void TrySetDataContext(Window window, object? dataContext = null)
    {
        if (window != null && window.DataContext == null)
        {
            if (dataContext == null)
            {

                var windowType = window.GetType();
                var name = windowType.FullName;
                var ass = windowType.Assembly;
                var modelType = ass.GetType($"{name}ViewModel") ?? ass.GetType($"{name}Model");
                if (modelType != null)
                {
                    dataContext = WpfApplication.Current.Services.GetService(modelType) ?? Activator.CreateInstance(modelType);
                }
            }
            if (dataContext != null)
            {
                //_commandLocator.Value.ResolveHandlers(model);
                window.DataContext = dataContext;
            }
        }
    }

}
