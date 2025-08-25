using Microsoft.Extensions.DependencyInjection;
using Tudormobile.Wpf;

namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : WpfApplication
    {
        protected override void OnConfigureServices(IServiceCollection services)
        {
            services.UseDialogService();
            services.AddTransient<MainWindowModel>();
        }
    }

}
