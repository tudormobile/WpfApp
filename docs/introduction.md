# Introduction
**Tudormobile.WpfApp** provides an extensible application container for WPF applications providing dependency injection and application lifecycle management.

Use the '***Tudormobile.WpfApp***' as a supplment to the the *'System.Windows.Application'* object provided by the WPF framework, or use the '***Tudormobile.WpfApplication***' object, which is derived from *System.Windows.Application*, as a direct replacement with added functionality. 

```
using Tudormobile.Wpf;

var app = WpfApp.Current;   // the current WPF application

// Create and show an application window
var w = app.CreateWindow<MyWindow>();
w.Show();

// Modal dialog
var result = app.CreateWindow<MyDialog, MyDialogViewModel>();
result.ShowDialog();
```
The ***DataContext*** is located, created, and set via naming convention or configuration. Depenency injection can be used to compose complex view models. 

> The WpfApp framework does not include support for complex view creation, which is considered to be an anti-pattern in the WPF framework. Views are created using the default, parameterless constructor.

An ***ObservableCollection&lt;Window&gt;*** is provided as a property on the WpfApp instance that can be used, for example, to data-bind menu items or another list of application windows.

Several ***ICommand*** objects are available via WpfAppCommands that can invoke window creation, open/save dialogs (File Pickers), Printing/Print Preview, and further delegate actions to your custom command delegates. Commands are also available for showing 'Help', and ... [add content here ...]

Application Lifecycle management is accomplished ... add content...

### Using WpfApplication
A more light-weight approach that contains no services by default, allowing both library services and your own services to be configured using a simple override.

App.xaml:
```
<wpf:Application x:Class="SampleApp.App"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="clr-namespace:SampleApp"
    xmlns:wpf="clr-namespace:Tudormobile.Wpf;assembly=Tudormobile.WpfApp"
    StartupUri="MainWindow.xaml">
</wpf:Application>
```
App.xaml.cs:
```
using Tudormobile.Wpf;
using System.Windows;
namespace SampleApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Tudormobile.Wpf.Application
    {
        protected override void OnConfigureServices(IServiceCollection services)
        {
            // additional override to configure services
            // ...

            // several extensions to utilize built-in services
            services.UseDialogService()
                     // ...
                    .UseMessageBoxService();
        }
    }
}
```
This code replaces the default System.Windows.Application with the Tudormobile.Wpf.Application implementation. In addition to a number of *optional*
services configured via extensions (UseXXXService), it also provides an additional override *OnConfigureServices* where
you can configure the dependency injection container. This approach is more lightweight than using the full IWpfAppBuilder and IWpfApp, but it is still 
extensible and allows you to add custom services as needed, as well as easily include and built-in services desired.