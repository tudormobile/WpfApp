using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tudormobile.Wpf.Commands;
using Tudormobile.Wpf.Services;

namespace Tudormobile.Wpf
{
    /// <summary>
    /// Application class supporting the WpfApp model.
    /// </summary>
    public class WpfApplication : Application
    {
        private readonly Lazy<IWpfAppBuilder> _builder = new(() => WpfApp.CreateBuilder());
        private readonly Lazy<IHelpService> _help = new(() => new HelpService());

        /// <summary>
        /// True if application should auto-configure itself.
        /// </summary>
        /// <remarks>
        /// Auto-configure will add a dispatcher, models, views, and services.
        /// </remarks>
        public bool AutoConfigure { get; set; } = true;

        /// <summary>
        /// Reference to the application builder object.
        /// </summary>
        /// <remarks>
        /// A builder object will be created if it does not already exist.
        /// </remarks>
        public IWpfAppBuilder Builder => _builder.Value;

        /// <summary>
        /// Reference to the application object.
        /// </summary>
        /// <remarks>
        /// This value will be (null) if AutoConfigure is not enabled.
        /// </remarks>
        public IWpfApp? App { get; private set; }

        /// <summary>
        /// Help service reference.
        /// </summary>
        public IHelpService Help => _help.Value;

        /// <inheritdoc/>
        protected override async void OnStartup(StartupEventArgs e)
        {
            var app = Application.Current;
            if (app != null)
            {
                app.Activated += app_Activated;
            }

            if (AutoConfigure)
            {
                var a = this.GetType().Assembly;
                App = Builder.AddViews(a)
                             .AddModels(a)
                             .AddDispatcher(Application.Current.Dispatcher)
                             .Build();
                base.OnStartup(e);
                await App.Start();
                // experimental
                addHandlers(this);
                return;
            }
            base.OnStartup(e);
        }

        private void addHandlers(Object o)
        {
            // experimental
            var t = o.GetType();
            var ms = t.GetMethods().Where(m => m.CustomAttributes.Any()).ToArray();

            foreach (var m in ms)
            {
                // Execute handlers
                var ea = m.GetCustomAttribute<ExecuteAttribute>();
                if (ea != null)
                {
                    // register
                    ((WpfApp?)App)?.CommandLocator.RegisterHandler(ea.ClassName ?? String.Empty, ea.CommandName, o, m);
                }
                // CanExecute handlers
                var cea = m.GetCustomAttribute<CanExecuteAttribute>();
                if (cea != null)
                {
                    // register
                    ((WpfApp?)App)?.CommandLocator.RegisterHandler(cea.ClassName ?? String.Empty, cea.CommandName, o, m, isCanExecute: true);
                }
            }
        }

        /// <summary>
        /// The main window was created.
        /// </summary>
        protected virtual void OnMainWindowCreated() { }

        private void app_Activated(object? sender, EventArgs e)
        {
            var app = Application.Current;
            if (app.Windows.Count > 0 && app.MainWindow != null)
            {
                OnMainWindowCreated();
                app.Activated -= app_Activated;
            }
        }
    }
}
