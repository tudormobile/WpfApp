using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Tudormobile.Wpf
{
    public static class WpfAppBuilderExtensions
    {
        public static IWpfAppBuilder AddViews(this IWpfAppBuilder builder, Assembly ass)
        {
            foreach (var t in ass.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("View")))
            {
                builder.HostBuilder.ConfigureServices((context, services) =>
                {
                    services.AddSingleton(t);
                    Debug.WriteLine($"Added VIEW: {t.Name}");
                });
            }
            return builder;
        }
        public static IWpfAppBuilder AddModels(this IWpfAppBuilder builder, Assembly ass)
        {
            foreach (var t in ass.GetTypes().Where(
                t => t.IsClass
                && !t.IsAbstract
                && (t.Name.EndsWith("Model") || t.IsAssignableTo(typeof(INotifyPropertyChanged)))
                ))
            {
                builder.HostBuilder.ConfigureServices((context, services) =>
                {
                    services.AddTransient(t);
                    Debug.WriteLine($"Added MODEL: {t.Name}");
                });
            }
            return builder;
        }
        public static IWpfAppBuilder AddDispatcher(this IWpfAppBuilder builder, Dispatcher d)
        {
            builder.HostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton(d);
                Debug.WriteLine("Added DISPATCHER");
            });
            return builder;
        }

    }
}
