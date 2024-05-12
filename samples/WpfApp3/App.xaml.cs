using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;
using Tudormobile.Wpf;
using Tudormobile.Wpf.Commands;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : WpfApplication
    {
        protected override void OnMainWindowCreated()
        {
            Help.Register(MainWindow, "https://www.google.com");
        }

        [Execute(nameof(MainWindowModel), nameof(MainWindowModel.SomeCommand))]
        public void SomeMethod(/*string filename*/)
        {
            App?.DialogService.ShowMessageBox("SomeMethod() was invoked.");
        }
        [Execute(nameof(MainWindowModel.AnotherCommand))]
        public void AnotherMethod(string filename)
        {
            App?.DialogService.ShowMessageBox("AnotherMethod(string filename) was invoked.");
        }
        [CanExecute(nameof(MainWindowModel.AnotherCommand))]
        public bool SomeMethod(string filename)
        {
            Debug.WriteLine("SomeMethod(string filename) was check via 'CanExecute'");
            return true;
        }
    }

}
