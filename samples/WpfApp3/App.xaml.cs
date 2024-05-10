using System.Configuration;
using System.Data;
using System.Windows;
using Tudormobile.Wpf;

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
    }

}
