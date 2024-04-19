using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tudormobile.Wpf
{
    /// <summary>
    /// Wpf Application Container.
    /// </summary>
    public class WpfApp : IWpfApp
    {
        /// <summary>
        /// Creates an instance of an IWpfAppBuilder.
        /// </summary>
        /// <returns></returns>
        public static IWpfAppBuilder CreateBuilder() => new WpfAppBuilder();
        internal WpfApp()
        {
            // construct...
            var app = System.Windows.Application.Current;
            app.Startup += App_Startup;
            app.Activated += App_Activated;
            app.LoadCompleted += App_LoadCompleted;
            app.FragmentNavigation += App_FragmentNavigation;
        }

        private void App_FragmentNavigation(object sender, System.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void App_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void App_Activated(object? sender, EventArgs e)
        {
            //throw new NotImplementedException();
            var app = System.Windows.Application.Current;
            if (app.Windows.Count == 1 && app.Windows[0].DataContext == null)
            {
                var name = app.Windows[0].GetType().FullName;
                var t = app.Windows[0].GetType().Assembly.GetType($"{name}ViewModel");
                if (t != null)
                {
                    app.Windows[0].DataContext = Activator.CreateInstance(t!);
                    app.Activated -= App_Activated;
                }
            }
        }

        private void App_Startup(object sender, System.Windows.StartupEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
