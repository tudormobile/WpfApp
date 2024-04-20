using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tudormobile.Wpf;

/// <summary>
/// Wpf Application Container.
/// </summary>
public class WpfApp : IWpfApp
{
    private IHost? _host;
    private IHostBuilder? _hostBuilder;
    /// <summary>
    /// Creates an instance of an IWpfAppBuilder.
    /// </summary>
    /// <returns></returns>
    public static IWpfAppBuilder CreateBuilder() => new WpfAppBuilder();
    internal WpfApp(bool useHosting)
    {
        _hostBuilder = useHosting ? Host.CreateDefaultBuilder() : null;
        // construct...
        var app = System.Windows.Application.Current;
        if (app != null)
        {
            app.Startup += App_Startup;
            app.Activated += App_Activated;
            app.LoadCompleted += App_LoadCompleted;
            app.FragmentNavigation += App_FragmentNavigation;
            app.Exit += App_Exit;
        }
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
                app.Windows[0].DataContext = Activator.CreateInstance(t!);
                app.Activated -= App_Activated;
            }
        }
    }

    private void App_Startup(object sender, StartupEventArgs e)
    {
        //throw new NotImplementedException();
    }

    public async Task Start<T>() where T: Window
    {
        //if (_host != null)
       // {
            if (_hostBuilder != null)
            {
                _host = _hostBuilder.ConfigureServices((context, services) =>
                {
                    services.AddSingleton<T>();
                }).Build();
            }
            await _host.StartAsync();
            if (Application.Current?.StartupUri == null)
            {
                var mainWindow = _host.Services.GetRequiredService<T>();
                mainWindow?.Show();
            }
        //}
    }
}
