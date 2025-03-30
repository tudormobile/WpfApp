using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tudormobile.Wpf;

/// <summary>
/// A WpfApp service.
/// </summary>
public interface IWpfAppService
{
    /// <summary>
    /// Service name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// True if only one instance of this service should be used.
    /// </summary>
    public bool IsSingleton { get; }
}
