using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp3
{
    internal class MainWindowModel
    {
        public ICommand? SomeCommand { get; set; }
        public ICommand? AnotherCommand { get; set; }
        public string Message => "Hello World, from MainWindowModel";
        public ClockData Clock { get; }
        public MainWindowModel(ClockData clockData)
        {
            Clock = clockData;
        }
    }
}
