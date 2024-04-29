using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3
{
    internal class MainWindowModel
    {
        public string Message => "Hello World, from MainWindowModel";
        public ClockData Clock { get; }
        public MainWindowModel(ClockData clockData)
        {
            Clock = clockData;
        }
    }
}
