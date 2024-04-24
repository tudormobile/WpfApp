using System.Collections.ObjectModel;
using System.Windows;
using Tudormobile.Wpf;

namespace WpfApp2
{
    public class MainMenuViewModel
    {
        public ObservableCollection<Window>? Windows { get; }
        public MainMenuViewModel()
        {
            Windows = WpfApp.Current?.Windows;
        }
    }
}