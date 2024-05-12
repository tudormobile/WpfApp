using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tudormobile.Wpf.Interfaces;

namespace Tudormobile.Wpf.Services;

/// <summary>
/// Base implementation of IWpfAppService
/// </summary>
internal abstract class WpfAppServiceBase : IWpfAppService
{
    /// <inheritdoc/>
    public string Name { get; }

    /// <inheritdoc/>
    public bool IsSingleton { get; }

    /// <summary>
    /// Creates and initializes the service.
    /// </summary>
    /// <param name="name">Name of the service.</param>
    /// <param name="isSingleton">True if singleton, otherwise (false). Default = true.</param>
    public WpfAppServiceBase(string? name = null, bool isSingleton = true)
    {
        Name = name ?? this.GetType().Name;
        IsSingleton = isSingleton;
    }
}
