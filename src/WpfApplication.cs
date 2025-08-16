using Microsoft.Extensions.DependencyInjection;
using Tudormobile.Wpf.Services;

namespace Tudormobile.Wpf;

/// <summary>
/// Provides an enhanced implementation to encapsule a Windows Presentation Foundation (WPF) Application.
/// <para>
/// This implementation of the <see cref="System.Windows.Application"/> class adds a single additional override method, 
/// <see cref="OnConfigureServices(IServiceCollection)"/>, that alows you to configure the dependency injection container
/// for the application.
/// </para>
/// </summary>
public partial class WpfApplication : System.Windows.Application
{
    private static CommandLine _commandLine = new();
    internal ServiceCollection _serviceCollection;
    internal IServiceProvider? _services;

    /// <summary>
    /// Provides access to the command line arguments passed to the application.
    /// </summary>
    public CommandLine CommandLine => _commandLine;

    /// <summary>
    /// Gets the service provider that resolves service dependencies.
    /// </summary>
    /// <remarks>
    /// The service provider is built from the underlying service collection. Accessing this property
    /// will initialize the service provider if it has not already been created.
    /// </remarks>
    public IServiceProvider Services => _services ??= _serviceCollection.BuildServiceProvider();

    /// <summary>
    /// Gets the current application instance for the application domain, overrideing the base class implementation.
    /// </summary>
    /// <remarks>This property provides access to the singleton instance of the <see cref="WpfApplication"/>
    /// class  for the current application domain. Use this property to interact with application-wide resources  or
    /// state, such as managing global events or accessing shared data.</remarks>
    public new static WpfApplication Current => (WpfApplication)System.Windows.Application.Current;

    /// <summary>
    /// Gets the current service provider for resolving application services.
    /// </summary>
    /// <remarks>
    /// Use this property to retrieve services registered in the application's dependency injection
    /// container. This property is a convenient way to access the service provider without needing
    /// to run through the `Current`.
    /// </remarks>
    public static IServiceProvider ServiceProvider => Current.Services;

    /// <summary>
    /// Creates a new instance of the WpfApplication class and initializes the service collection.
    /// The constructor also calls the <see cref="OnConfigureServices(IServiceCollection)"/> virtual method that
    /// the application can override to configure services.
    /// </summary>
    public WpfApplication()
    {
        _serviceCollection = new ServiceCollection();
        OnConfigureServices(_serviceCollection);
        this.Activated += onActivated;
    }

    /// <summary>
    /// Allows derived classes to configure additional services for the application.
    /// </summary>
    /// <remarks>
    /// This method is intended to be overridden in derived classes to register custom services  or
    /// modify existing service configurations. It is called during the application's service  configuration
    /// phase.
    /// </remarks>
    /// <param name="services">The <see cref="IServiceCollection"/> to which services can be added.</param>
    protected virtual void OnConfigureServices(IServiceCollection services) { }

    /// <summary>
    /// Allows derived classes to be notified when the MainWindow is activated for the first time.
    /// </summary>
    /// <remarks>
    /// This method is intended to be overridden in derived classes to allow for custom logic to be executed
    /// when the main window of the application is created and activated for the first time.
    /// </remarks>
    protected virtual void OnMainWindowCreated() => Wpf.Services.WindowService.TrySetDataContext(MainWindow);

    private void onActivated(object? sender, EventArgs e)
    {
        this.Activated -= onActivated;
        if (Windows.Count > 0 && MainWindow != null)
        {
            // Invoke the OnMainWindowCreated method when the main window is activated for the first time.
            OnMainWindowCreated();
            Services.GetService<IWindowService>()?.Windows.Add(MainWindow);
        }
    }
}

//old code
#if false

using System.Reflection;
using System.Windows;
using System.Windows.Threading;
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
                // hack for now
                _ = ThreadPool.QueueUserWorkItem(d =>
                {
                    ((Dispatcher)d!).Invoke(OnMainWindowCreated, System.Windows.Threading.DispatcherPriority.ApplicationIdle);
                }, Dispatcher);
                app.Activated -= app_Activated;
            }
        }
    }
}
#endif