using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tudormobile.Wpf
{
    /// <summary>
    /// Defines an interface to manage a Wpf Application.
    /// </summary>
    /// <remarks>
    /// The IWpfApp interface includes mechanisms to configure a Wpf application,
    /// manage the application lifecycle, application level events, application services, 
    /// and serves as a dependency injection container for a Wpf application.
    /// </remarks>
    public interface IWpfApp
    {
        public Task Start<T>() where T: Window;
    }
}
