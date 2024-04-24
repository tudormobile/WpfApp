# Introduction
**Tudormobile.WpfApp** provides an extensible application container for WPF applications providing dependency injection and application lifecycle management.

Use the '***Tudormobile.WpfApp***' as a replacement for the *'System.Windows.Application'* object provided by the WPF framework. 

```
using Tudormobile.Wpf;

var app = WpfApp.Current;   // the current WPF application

// Create and show an application window
var w = app.CreateWindow<MyWindow>();
w.Show();

// Modal dialog
var result = app.ShowDialog<MyDialog, MyDialogViewModel>();
```
The ***DataContext*** is located, created, and set via naming convention or configuration. Depenency injection can be used to compose complex view models. 

> The WpfApp framework does not include support for complex view creation, which is considered to be an anti-pattern in the WPF framework. Views are created using the default, parameterless constructor.

An ***ObservableCollection&lt;Window&gt;*** is provided as a property on the WpfApp instance that can be used, for example, to data-bind menu items or another list of application windows.

Several ***ICommand*** objects are available via WpfAppCommands that can invoke window creation, open/save dialogs (File Pickers), Printing/Print Preview, and further delegate actions to your custom command delegates. Commands are also available for showing 'Help', and ... [add content here ...]

Application Lifecycle management is accomplished ... add content...
