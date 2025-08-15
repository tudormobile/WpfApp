using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using Tudormobile.Wpf;
using Tudormobile.Wpf.Commands;
using Tudormobile.Wpf.Services;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : WpfApplication
    {
        protected override void OnConfigureServices(IServiceCollection services)
        {
            services.UseHelp().UseDialog();
        }
        protected override void OnMainWindowCreated()
        {
            var helpService = Services.GetRequiredService<IHelpService>();
            helpService.Register(MainWindow, "https://www.google.com");
        }

        [Execute(nameof(MainWindowModel), nameof(MainWindowModel.SomeCommand))]
        public void SomeMethod(/*string filename*/)
        {
            var dialogService = Services.GetRequiredService<IDialogService>();
            dialogService.ShowMessageBox("SomeMethod() was invoked.");
        }
        [Execute(nameof(MainWindowModel.AnotherCommand))]
        public void AnotherMethod(string filename)
        {
            var dialogService = Services.GetRequiredService<IDialogService>();
            dialogService.ShowMessageBox("AnotherMethod(string filename) was invoked.");
        }
        [CanExecute(nameof(MainWindowModel.AnotherCommand))]
        public bool SomeMethod(string filename)
        {
            Debug.WriteLine("SomeMethod(string filename) was check via 'CanExecute'");
            return true;
        }
    }

}
