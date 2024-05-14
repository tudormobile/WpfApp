using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Tudormobile.Wpf.Commands;
using Tudormobile.Wpf.Services;

namespace Tudormobile.Wpf;

/// <summary>
/// Wpf Application Container.
/// </summary>
public partial class WpfApp : IWpfApp
{
    private IHost? _host;
    private readonly IHostBuilder? _builder;
    private readonly Type? _mainWindowType;
    private readonly Lazy<CommandLine> _commandLine = new(() => new CommandLine());
    private readonly Lazy<IDialogService> _dialogService = new(() => new DialogService());
    private readonly Lazy<DelegateCommandLocator> _commandLocator = new(() => new DelegateCommandLocator());

    /// <inheritdoc/>
    public IDialogService DialogService => _dialogService.Value;

    /// <inheritdoc/>
    public ICommandLine CommandLine => _commandLine.Value;

    /// <inheritdoc/>
    public ObservableCollection<Window> Windows { get; } = [];

    /// <summary>
    /// Creates an instance of an IWpfAppBuilder.
    /// </summary>
    /// <returns></returns>
    public static IWpfAppBuilder CreateBuilder() => new WpfAppBuilder();

    /// <summary>
    /// The current WpfApp Application Host object.
    /// </summary>
    public static IWpfApp? Current { get; set; } = null;

    /// <summary>
    /// The command locator for registering and resolving ICommand delegates.
    /// </summary>
    internal DelegateCommandLocator CommandLocator => _commandLocator.Value;

    internal WpfApp(IHostBuilder? builder = null, Type? mainWindowType = null)
    {
        _builder = builder;
        _mainWindowType = mainWindowType;
        var app = Application.Current;
        if (app != null)
        {
            app.Startup += app_Startup;
            app.Activated += app_Activated;
            app.LoadCompleted += app_LoadCompleted;
            app.FragmentNavigation += app_FragmentNavigation;
            app.Exit += app_Exit;
        }

        Current = this;
    }

    private void app_Exit(object sender, ExitEventArgs e)
    {
        var t = Task.Run(async () =>
        {
            if (_host != null)
            {
                await _host.StopAsync();
            }
        });
        t.Wait();
    }

    private void app_FragmentNavigation(object sender, System.Windows.Navigation.FragmentNavigationEventArgs e)
    {
        //throw new NotImplementedException();
    }

    private void app_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
    {
        //throw new NotImplementedException();
    }

    private void app_Activated(object? sender, EventArgs e)
    {
        var app = Application.Current;
        if (app.Windows.Count == 1)
        {
            app.Activated -= app_Activated;
            if (app.Windows[0].DataContext == null)
            {
                // Try and set the data context
                var name = app.Windows[0].GetType().FullName;
                var ass = app.Windows[0].GetType().Assembly;
                var t = ass.GetType($"{name}ViewModel") ?? ass.GetType($"{name}Model");
                if (t != null)
                {
                    var model = _host?.Services.GetRequiredService(t) ?? Activator.CreateInstance(t!)!;
                    _commandLocator.Value.ResolveHandlers(model);
                    app.Windows[0].DataContext = model;
                }
            }
            if (Windows.Count == 0)
            {
                Windows.Add(app.Windows[0]);
            }
            if (app.MainWindow != null)
            {
                app.MainWindow.CommandBindings.Add(new CommandBinding(WpfApplicationCommands.Exit, closeMainWindow));
                app.MainWindow.CommandBindings.Add(new CommandBinding(WpfApplicationCommands.CloseAll, closeAllWindows, canCloseAllWindows));
                app.MainWindow.CommandBindings.Add(new CommandBinding(WpfApplicationCommands.SelectWindow, selectWindow));
                app.MainWindow.CommandBindings.Add(new CommandBinding(WpfApplicationCommands.Open, openFile));
            }
        }
    }

    private void openFile(object? sender, ExecutedRoutedEventArgs e)
    {
        var cmd = new FilePickerCommand();
        cmd.Execute(e.Parameter);
    }

    private void selectWindow(object? sender, ExecutedRoutedEventArgs e)
    {
        var w = e.Parameter as Window;
        w?.Activate();
    }


    private void closeMainWindow(object? sender, EventArgs e)
    {
        if (sender == Application.Current.MainWindow)
        {
            Application.Current.Shutdown();
            //Application.Current.MainWindow.Close();//?
        }
    }

    private void closeAllWindows(object? sender, EventArgs e)
    {
        if (sender == Application.Current.MainWindow)
        {
            foreach (var w in this.Windows.Where(w => w != Application.Current.MainWindow))
            {
                w.Close();
            }
        }
    }

    private void canCloseAllWindows(object? sender, CanExecuteRoutedEventArgs e)
    {
        if (sender == Application.Current.MainWindow)
        {
            e.CanExecute = Windows.Any(w => w != Application.Current.MainWindow);
        }
    }

    private void app_Startup(object sender, StartupEventArgs e)
    {
        //throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public async Task Start<T>() where T : Window
    {
        if (_builder != null)
        {
            _host = _builder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<T>();
            }).Build();
        }
        if (_host != null)
        {
            await _host.StartAsync();
            if (Application.Current?.StartupUri == null)
            {
                var mainWindow = _host.Services.GetRequiredService<T>();
                mainWindow?.Show();
            }
        }
        if (_builder == null && _host == null && Application.Current != null && Application.Current.StartupUri == null)
        {
            if (Activator.CreateInstance<T>() is Window mainWindow)
            {
                Windows.Add(mainWindow);
                mainWindow.Show();
            }
            return;
        }
    }

    /// <inheritdoc/>
    public async Task Start()
    {
        // TODO: refactor this w/the above
        if (_builder != null && _mainWindowType == null)
        {
            _host = _builder.Build();
            await _host.StartAsync();
        }

        if (_builder == null && _mainWindowType != null)
        {
            if (Activator.CreateInstance(_mainWindowType) is Window mainWindow)
            {
                Windows.Add(mainWindow);
                mainWindow.Show();
            }
            return;
        }

        if (_builder != null && _mainWindowType != null)
        {
            _host = _builder.ConfigureServices((context, services) =>
            {
                services.AddSingleton(_mainWindowType);
            }).Build();
            if (_host != null)
            {
                await _host.StartAsync();
                if (Application.Current?.StartupUri == null)
                {
                    if (_host.Services.GetRequiredService(_mainWindowType) is Window mainWindow)
                    {
                        Windows.Add(mainWindow);
                        mainWindow.Show();
                    }
                }
            }
        }
    }

    /// <inheritdoc/>
    public Window CreateWindow<TView, TViewModel>() where TViewModel : class where TView : Window
    {
        if (_host != null)
        {
            var w = _host.Services.GetRequiredService<TView>();
            var m = _host.Services.GetRequiredService(typeof(TViewModel));
            _commandLocator.Value.ResolveHandlers(m);
            w.DataContext = m;
            Windows.Add(w);
            return w;
        }
        var model = Activator.CreateInstance<TViewModel>();
        var result = Activator.CreateInstance<TView>();
        result.DataContext = model;
        Windows.Add(result);
        return result;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public FrameworkElement CreateView<TView, TViewModel>()
        where TView : FrameworkElement
        where TViewModel : class
    {
        if (_host != null)
        {
            var v = _host.Services.GetRequiredService<TView>();
            var m = _host.Services.GetRequiredService(typeof(TViewModel));
            return resolveDataContext(v, m);
        }
        var model = Activator.CreateInstance<TViewModel>();
        var view = Activator.CreateInstance<TView>();
        return resolveDataContext(view, model);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public FrameworkElement CreateView<TView>() where TView : FrameworkElement
    {
        if (_host != null)
        {
            var v = _host.Services.GetRequiredService<TView>();
            return resolveDataContext(v, null);
        }
        var view = Activator.CreateInstance<TView>();
        return resolveDataContext(view, null);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public FrameworkElement CreateView(Type viewType, Type? viewModelType = null)
    {
        if (_host != null)
        {
            var v = (_host.Services.GetRequiredService(viewType) as FrameworkElement)!;
            if (viewModelType != null)
            {
                var m = _host.Services.GetRequiredService(viewModelType);
                return resolveDataContext(v, m);
            }
            return resolveDataContext(v, null);
        }
        var view = (Activator.CreateInstance(viewType) as FrameworkElement)!;
        if (viewModelType != null)
        {
            var model = Activator.CreateInstance(viewModelType)!;
            return resolveDataContext(view, model);
        }
        return resolveDataContext(view, null);
    }

    private FrameworkElement resolveDataContext(FrameworkElement element, object? context)
    {
        if (context == null)
        {
            // try to 'find' data context
            var name = element.GetType().Name;
            var ass = element.GetType().Assembly;

            // TODO: Obviously needs refactoring
            if (_host != null)
            {
                foreach (var modelTypeName in possibleModelNames(name))
                {
                    var t = findType(ass, modelTypeName);
                    if ( t != null)
                    {
                        context = _host.Services.GetService(t);
                        break;
                    }
                }
            }
            else { /* later*/}

        }
        if (context != null)
        {
            _commandLocator.Value.ResolveHandlers(context);
            element.DataContext = context;
        }
        return element;
    }

    // TODO: Obvious refactoring
    private Type? findType(Assembly ass, string modelTypeName)
    {
        return ass.GetTypes().Where(t=>t.Name == modelTypeName).FirstOrDefault();
    }

    // TODO: Obvious refactoring
    private IEnumerable<string> possibleModelNames(string viewName)
    {
        // candidates: [name]Model, [name]ViewModel, [abbrName]Model, [abbrName]ViewModel
        yield return viewName + "Model";
        yield return viewName + "ViewModel";

        // strip off 'Control', 'Window'
        if (viewName.EndsWith("Control"))
        {
            var shortName = viewName.Substring(0, viewName.Length - 7);
            yield return shortName + "Model";
            yield return shortName + "ViewModel";
        }
        if (viewName.EndsWith("Window"))
        {
            var shortName = viewName.Substring(0, viewName.Length - 6);
            yield return shortName + "Model";
            yield return shortName + "ViewModel";
        }
    }
}
