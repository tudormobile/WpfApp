using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tudormobile.Wpf
{
    public class WpfApplication : Application
    {
        private Lazy<IWpfAppBuilder> _builder = new(() => WpfApp.CreateBuilder());
        public bool AutoConfigure { get; set; } = true;
        public IWpfAppBuilder Builder => _builder.Value;
        public IWpfApp? App { get; private set; }

        protected override async void OnStartup(StartupEventArgs e)
        {
            if (AutoConfigure)
            {
                var a = this.GetType().Assembly;
                App = Builder.AddViews(a)
                             .AddModels(a)
                             .AddDispatcher(Application.Current.Dispatcher)
                             .Build();
                base.OnStartup(e);
                await App.Start();
                return;
            }
            base.OnStartup(e);
        }
    }
}
