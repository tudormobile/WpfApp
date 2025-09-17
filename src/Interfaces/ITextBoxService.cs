namespace Tudormobile.Wpf;

/// <summary>
/// Service for managing TextBox related functionality. Currently supports
/// selecting all text in a TextBox when it receives focus.
/// <para>
/// You must register an implementation of this interface with the service collection,
/// and then call the Register() method in your application's OnStartup override. This
/// is done automatically if you use the UseTextBoxServices extension method in your
/// initializtion code.
/// </para>
/// </summary>
internal interface ITextBoxService
{
    /// <summary>
    /// Registers and activates the service.
    /// </summary>
    void Register();
}
