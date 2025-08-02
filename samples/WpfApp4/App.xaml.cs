using System.Configuration;
using System.Data;
using System.Windows;
using Tudormobile.Wpf;
using Tudormobile.Wpf.Commands;
using WpfApp4.Views;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : WpfApplication
    {
        protected override void OnMainWindowCreated()
        {
            if (MainWindow.DataContext is MainWindowModel model)
            {
                model.Controls.Add(typeof(HelloWorldControl));
            }
        }

        [Execute(nameof(MainWindowModel.SelectControlCommand))]
        public void SelectUIItem(Type t)
        {
            if (MainWindow.DataContext is MainWindowModel model)
            {
                model.SelectedUI = App!.CreateView(t);
            }
        }
    }

}
