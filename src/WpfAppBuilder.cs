using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Tudormobile.Wpf.Interfaces;

namespace Tudormobile.Wpf;

/// <inheritdoc/>
internal class WpfAppBuilder : IWpfAppBuilder
{
    private readonly List<(Type, Type)> _services = [];
    private readonly List<(Type, Type)> _singleServices = [];
    private readonly Lazy<IHostBuilder> _hostBuilder;
    private Type? _mainWindow;
    private bool _useHosting;
    internal WpfAppBuilder()
    {
        _hostBuilder = new Lazy<IHostBuilder>(createHostBuilder);
        _useHosting = true;
    }
    private IHostBuilder createHostBuilder() => Host.CreateDefaultBuilder();

    /// <inheritdoc/>
    public IWpfAppBuilder AddHosting(bool useHosting = true)
    {
        _useHosting = useHosting;
        return this;
    }

    /// <inheritdoc/>
    public IWpfAppBuilder AddMainWindow<T>() where T : Window
    {
        _mainWindow = typeof(T);
        return this;
    }

    /// <inheritdoc/>
    public IHostBuilder HostBuilder => _hostBuilder.Value;

    /// <inheritdoc/>
    public IWpfApp Build()
    {
        if (_mainWindow != null)
        {
            _hostBuilder.Value.ConfigureServices((context, services) =>
            {
                services.AddSingleton(_mainWindow);
            });
        }
        _hostBuilder.Value.ConfigureServices((context, services) =>
            {
                foreach (var service in _singleServices)
                {
                    services.AddSingleton(service.Item1, service.Item2);
                }
                foreach (var service in _singleServices)
                {
                    services.AddTransient(service.Item1, service.Item2);
                }
            });

        return _hostBuilder.IsValueCreated ? new WpfApp(_hostBuilder.Value, _mainWindow) : new WpfApp(null, _mainWindow);
    }

    /// <inheritdoc/>
    public IWpfAppBuilder AddWindow<TView, TViewModel>()
        where TView : Control
        where TViewModel : class
    {
        _hostBuilder.Value.ConfigureServices((context, services) =>
        {
            services.AddTransient<TView>().AddTransient<TViewModel>();
        });
        return this;
    }

    /// <inheritdoc/>
    public IWpfAppBuilder AddView<TView, TViewModel>()
        where TView : FrameworkElement
        where TViewModel : class
    {
        _hostBuilder.Value.ConfigureServices((context, services) =>
        {
            services.AddTransient<TView>().AddTransient<TViewModel>();
        });
        return this;
    }

    /// <inheritdoc/>
    public IWpfAppBuilder AddService<TServiceInterface, TServiceClass>(bool isSingleton = false)
        where TServiceInterface : IWpfAppService
        where TServiceClass : class
    {
        if (isSingleton)
        {
            _singleServices.Add((typeof(TServiceInterface), typeof(TServiceClass)));
        }
        else
        {
            _services.Add((typeof(TServiceInterface), typeof(TServiceClass)));
        }
        return this;
    }
}
