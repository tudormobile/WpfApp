using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Tudormobile.Wpf;

/// <inheritdoc/>
internal class WpfAppBuilder : IWpfAppBuilder
{
    private Lazy<IHostBuilder> _hostBuilder;
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
}
