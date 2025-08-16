using Microsoft.Extensions.DependencyInjection;
using Tudormobile.Wpf.Services;

namespace Tudormobile.Wpf;

/// <summary>
/// Extensions methods for WpfApplication.
/// <para>
/// These methods provide convienience methods for configuring the WpfApplication instance
/// in your OnConfigureServices override method as well as other common tasks.
/// </para>
/// </summary>
public static class WpfApplicationExtensions
{
    /// <summary>
    /// Adds the default implementation of <see cref="IHelpService"/> to the service collection.
    /// </summary>
    /// <remarks>
    /// This method registers <see cref="HelpService"/> as a singleton implementation of <see cref="IHelpService"/>.
    /// </remarks>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the service will be added.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection UseHelpService(this IServiceCollection services)
        => services.AddSingleton<IHelpService, HelpService>();

    /// <summary>
    /// Adds the default implementation of <see cref="IDialogService"/> to the service collection.
    /// </summary>
    /// <remarks>
    /// This method registers <see cref="DialogService"/> as a singleton implementation of <see cref="IHelpService"/>. 
    /// </remarks>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the service will be added.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection UseDialogService(this IServiceCollection services)
        => services.AddSingleton<IDialogService, DialogService>();

    /// <summary>
    /// Adds the default implementation of <see cref="IPrintService"/> to the service collection.
    /// </summary>
    /// <remarks>
    /// This method registers <see cref="PrintService"/> as a singleton implementation of <see cref="IPrintService"/>.
    /// </remarks>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the service will be added.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection UsePrintService(this IServiceCollection services)
        => services.AddSingleton<IPrintService, PrintService>();

    /// <summary>
    /// Adds the default implementation of <see cref="IWindowService"/> to the service collection.
    /// </summary>
    /// <remarks>
    /// This method registers <see cref="WindowService"/> as a singleton implementation of <see cref="IWindowService"/>.
    /// </remarks>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the service will be added.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection UseWindowService(this IServiceCollection services)
        => services.AddSingleton<IWindowService, WindowService>();
}
