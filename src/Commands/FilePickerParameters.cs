using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tudormobile.Wpf.Commands
{
    public class FilePickerParameters
    {
        // make these dependency properties
        public string? Title { get; set; }
        public string Filter { get; set; }
        public ICommand? Command { get; set; }
        public string? Filename { get; set; }

    }
}
