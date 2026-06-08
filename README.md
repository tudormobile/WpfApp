# WpfApp
WPF Application container

[![Build and Deploy](https://github.com/tudormobile/WpfApp/actions/workflows/dotnet.yml/badge.svg)](https://github.com/tudormobile/WpfApp/actions/workflows/dotnet.yml)  [![Publish Docs](https://github.com/tudormobile/WpfApp/actions/workflows/docs.yml/badge.svg)](https://github.com/tudormobile/WpfApp/actions/workflows/docs.yml)  

Copyright (C) 2024-2025 Bill Tudor
### Quick Start
Use **Tudormobile.Wpf.*WpfApplication*** in place of **System.Windows.*Application***  

App.xaml:
```
<wpf:WpfApplication x:Class="SampleApp.App"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="clr-namespace:SampleApp"
    xmlns:wpf="clr-namespace:Tudormobile.Wpf;assembly=Tudormobile.WpfApp"
    StartupUri="MainWindow.xaml">
</wpf:WpfApplication>
```
In App.xaml.cs, override *OnConfigureServices()*  

```
using Tudormobile.Wpf;
using System.Windows;
namespace SampleApp;

public partial class App : Tudormobile.Wpf.WpfApplication
{
    // additional override to configure services
    protected override void OnConfigureServices(IServiceCollection services)
    {
        // ...

        // several extensions exist to utilize built-in services
        services.UseDialogService()
                    // ...
                .UsePrintService();
    }
}
```
### Alternative
You can also utilize the application builder to create an app instance containing all of the internal services available in the library. This instance does not replace the standard *System.Windows.Application*. Remove the StartupUri designation from the App.xaml, and place the following code in the *OnStartup()* override:
```
using Tudormobile.Wpf;

var builder = WpfApp.CreateBuilder();
var app = builder.Build();

app.Run();
```
- Creates an extensible IWpfAppBuilder and a IWpfApp with preconfigured defaults that closely match the '*System.Windows.Application*' implementation bundled with the WPF framework.
- Runs the application using the framework application lifecycle defaults.

> [!TIP]
> Checkout each example project for additional information.

[NuGET Package README](docs/README.md) | [Source Code README](src/README.md) | [API Documentation](https://tudormobile.github.io/WpfApp/)
