using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tudormobile.Wpf.Commands;

namespace Tudormobile.Wpf;

/// <summary>
/// Wpf Application Container.
/// </summary>
public class WpfApp : IWpfApp
{
    private IHost? _host;
    private readonly IHostBuilder? _builder;
    private readonly Type? _mainWindowType;
    private Lazy<CommandLine> _commandLine = new(() => new CommandLine());

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

    internal WpfApp(IHostBuilder? builder = null, Type? mainWindowType = null)
    {
        _builder = builder;
        _mainWindowType = mainWindowType;
        var app = Application.Current;
        if (app != null)
        {
            app.Startup += App_Startup;
            app.Activated += App_Activated;
            app.LoadCompleted += App_LoadCompleted;
            app.FragmentNavigation += App_FragmentNavigation;
            app.Exit += App_Exit;
        }


        Current = this;
    }

    private void App_Exit(object sender, ExitEventArgs e)
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

    private void App_FragmentNavigation(object sender, System.Windows.Navigation.FragmentNavigationEventArgs e)
    {
        //throw new NotImplementedException();
    }

    private void App_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
    {
        //throw new NotImplementedException();
    }

    private void App_Activated(object? sender, EventArgs e)
    {
        //throw new NotImplementedException();
        var app = Application.Current;
        if (app.Windows.Count == 1 && app.Windows[0].DataContext == null)
        {
            var name = app.Windows[0].GetType().FullName;
            var t = app.Windows[0].GetType().Assembly.GetType($"{name}ViewModel");
            if (t != null)
            {
                var model = _host?.Services.GetRequiredService(t) ?? Activator.CreateInstance(t!);
                app.Windows[0].DataContext = model;
                app.Activated -= App_Activated;
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

            }
        }
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
            e.CanExecute = this.Windows.Count(w => w != Application.Current.MainWindow) > 0;
        }
    }



    private void App_Startup(object sender, StartupEventArgs e)
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
            var mainWindow = Activator.CreateInstance<T>() as Window;
            if (mainWindow != null)
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
            var mainWindow = Activator.CreateInstance(_mainWindowType) as Window;
            if (mainWindow != null)
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
                    var mainWindow = _host.Services.GetRequiredService(_mainWindowType) as Window;
                    if (mainWindow != null)
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

}
