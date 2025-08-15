using System.Windows;
using Tudormobile.Wpf;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Simplest method to startup application
            var app = WpfApp.CreateBuilder().Build();
            app.Start<MainWindow>();
            base.OnStartup(e);
        }

    }

}
