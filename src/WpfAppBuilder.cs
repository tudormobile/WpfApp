﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tudormobile.Wpf
{
    internal class WpfAppBuilder : IWpfAppBuilder
    {
        public IWpfApp Build()
        {
            return new WpfApp();
        }
    }
}
