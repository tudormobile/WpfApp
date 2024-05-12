using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Tudormobile.Wpf.Services;

/// <summary>
/// Help service.
/// </summary>
public interface IHelpService : IWpfAppService
{
    /// <summary>
    /// Register help Uri for a UI Element.
    /// </summary>
    /// <param name="element">Element to register.</param>
    /// <param name="uriString">String URI value of the help content.</param>
    public void Register(UIElement element, string uriString)
        => Register(element, new Uri(uriString));

    /// <summary>
    /// Register help Uri for a UI Element.
    /// </summary>
    /// <param name="element">Element to register.</param>
    /// <param name="uri">URI to help content.</param>
    public void Register(UIElement element, Uri uri);

    /// <summary>
    /// Show help content.
    /// </summary>
    /// <param name="uri">URI of the help content.</param>
    public void ShowHelp(Uri uri);

}
