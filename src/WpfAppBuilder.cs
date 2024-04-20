using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tudormobile.Wpf
{
    internal class WpfAppBuilder : IWpfAppBuilder
    {
        private bool _useHosting = true;    // defaults to use hosting DI APIs
        public IWpfApp Build()
        {
            return new WpfApp(_useHosting);
        }
    }
}
