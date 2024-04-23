using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using Tudormobile.Wpf;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Build w/MainWindow and Host Container
            var builder = WpfApp.CreateBuilder()
                .AddHosting()
                .AddMainWindow<MainWindow>();
            builder.HostBuilder.ConfigureServices((context, services) =>
            {
                services.AddTransient<MainMenuViewModel>()
                        .AddTransient<MainWindowViewModel>()
                        .AddTransient<TestWindow>()
                        .AddTransient<TestWindowViewModel>();
            });
            var app = builder.Build();

            app.Start<MainWindow>();
            base.OnStartup(e);
        }
    }

}
