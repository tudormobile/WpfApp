using Microsoft.Extensions.DependencyInjection;
using Tudormobile.Wpf;

namespace WpfApp0
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : WpfApplication
    {
        protected override void OnConfigureServices(IServiceCollection services)
        {
            // this sample only uses services from this application. Provides a simple
            // example of using the WpfApplication infrastructure.

            // services
            services.AddSingleton<IMessageBoxService, MessageBoxService>();

            // view models
            services.AddTransient<MainWindowViewModel>();

        }
    }

}
