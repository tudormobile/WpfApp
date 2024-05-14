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
using Tudormobile.Wpf.Services;

namespace Tudormobile.Wpf
{
    /// <summary>
    /// WpfApp builder extensions.
    /// </summary>
    public static class WpfAppBuilderExtensions
    {
        /// <summary>
        /// Add 'Views' from an assembly.
        /// </summary>
        /// <param name="builder">WpfAppBuilder to extend.</param>
        /// <param name="ass">Assembly to scan for 'Views'.</param>
        /// <returns>Fluent-reference to the builder.</returns>
        /// <remarks>
        /// 'Views' are identified by convention as concrete classes whose name ends in 'View'.
        /// </remarks>
        public static IWpfAppBuilder AddViews(this IWpfAppBuilder builder, Assembly ass)
        {
            foreach (var t in ass.GetTypes().Where(t => t.IsClass && !t.IsAbstract && nameCouldBeView(t.Name)))
            {
                builder.HostBuilder.ConfigureServices((context, services) =>
                {
                    services.AddSingleton(t);
                    Debug.WriteLine($"Added VIEW: {t.Name}");
                });
            }
            return builder;
        }
        private static bool nameCouldBeView(string name)
            => name.EndsWith("View") || name.EndsWith("Control") || name.EndsWith("Window");

        /// <summary>
        /// Add 'Models' from an assembly.
        /// </summary>
        /// <param name="builder">WpfAppBuilder to extend.</param>
        /// <param name="ass">Assembly to scan for 'Views'.</param>
        /// <returns>Fluent-reference to the builder.</returns>
        /// <remarks>
        /// 'Models' are identified by convention as concrete classes whose name ends in 'Model' or any
        /// concrete class that implements INotifyPropertyChanged.
        /// </remarks>
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

        /// <summary>
        /// Add a 'Dispatcher' to the application model.
        /// </summary>
        /// <param name="builder">WpfAppBuilder to extend.</param>
        /// <param name="d">Dispatcher to add.</param>
        /// <returns>Fluent-reference to the builder.</returns>
        public static IWpfAppBuilder AddDispatcher(this IWpfAppBuilder builder, Dispatcher d)
        {
            builder.HostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton(d);
                Debug.WriteLine("Added DISPATCHER");
            });
            return builder;
        }

        /// <summary>
        /// Utilize basic services.
        /// </summary>
        /// <param name="builder">WpfAppBuilder to extend.</param>
        /// <returns>Fluent-reference to the builder.</returns>
        public static IWpfAppBuilder UseBasicServices(this IWpfAppBuilder builder)
        {
            builder.UseDialogService();
            builder.UseWindowService();
            return builder;
        }

        /// <summary>
        /// Utilize all available services.
        /// </summary>
        /// <param name="builder">WpfAppBuilder to extend.</param>
        /// <returns>Fluent-reference to the builder.</returns>
        public static IWpfAppBuilder UseServices(this IWpfAppBuilder builder)
        {
            UseBasicServices(builder);
            builder.UsePrintService();
            builder.UseHelpService();
            return builder;
        }

        /// <summary>
        /// Utilize the 'Print' service.
        /// </summary>
        /// <param name="builder">WpfAppBuilder to extend.</param>
        /// <returns>Fluent-reference to the builder.</returns>
        public static IWpfAppBuilder UsePrintService(this IWpfAppBuilder builder)
        {
            builder.AddService<IPrintService, PrintService>();
            return builder;
        }

        /// <summary>
        /// Utilize the 'Window' service.
        /// </summary>
        /// <param name="builder">WpfAppBuilder to extend.</param>
        /// <returns>Fluent-reference to the builder.</returns>
        public static IWpfAppBuilder UseWindowService(this IWpfAppBuilder builder)
        {
            builder.AddService<IWindowService, WindowService>();
            return builder;
        }

        /// <summary>
        /// Utilize the 'Help' service.
        /// </summary>
        /// <param name="builder">WpfAppBuilder to extend.</param>
        /// <returns>Fluent-reference to the builder.</returns>
        public static IWpfAppBuilder UseHelpService(this IWpfAppBuilder builder)
        {
            builder.AddService<IHelpService, HelpService>();
            return builder;
        }

        /// <summary>
        /// Utilize the 'Dialog' service.
        /// </summary>
        /// <param name="builder">WpfAppBuilder to extend.</param>
        /// <returns>Fluent-reference to the builder.</returns>
        public static IWpfAppBuilder UseDialogService(this IWpfAppBuilder builder)
        {
            builder.AddService<IDialogService, DialogService>();
            return builder;
        }
    }
}
