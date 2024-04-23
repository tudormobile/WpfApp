using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    internal class MainWindowViewModel
    {
        public MainMenuViewModel MenuViewModel { get; }
        public MainWindowViewModel(MainMenuViewModel menuViewModel)
        {
            this.MenuViewModel = menuViewModel;
        }
    }
}
