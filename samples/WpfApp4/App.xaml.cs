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
            base.OnMainWindowCreated(); // Allow auto-creation of DataContext via base class.
            if (MainWindow.DataContext is MainWindowModel model)
            {
                model.Controls.Add(typeof(HelloWorldControl));
            }
        }

        [Execute(nameof(MainWindowModel.SelectControlCommand))]
        public void SelectUIItem(Type t)
        {
            // TODO: Re-work this example with Commands and View creation, or remove it.
            //if (MainWindow.DataContext is MainWindowModel model)
            //{
            //    model.SelectedUI = App!.CreateView(t);
            //}
        }
    }

}
