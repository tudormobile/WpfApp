# Getting Started

### Install the package
```
dotnet add package Tudormobile.WpfApp
```
### Prerequisites
**NONE**

### Dependencies
When using the application host, the following dependencies are required:
- Microsoft.Extensions.Hosting
- Microsoft.Extensions.DependencyInjection

### Configure and Run the application

#### Minimal Configuration
```
using Tudormobile.Wpf;

var builder = WpfApp.CreateBuilder();
var app = builder.Build();

app.Run();
```

#### Inlcuding library provided services
```
using Tudormobile.Wpf;

var builder = WpfApp.CreateBuilder()
                .AddMainWindow<MainWindow>();

var app = builder.Build();
app.Start<MainWindow>();
```

#### Using the Application Host and DI container
```
using Tudormobile.Wpf;

var builder = WpfApp.CreateBuilder()
                .AddHosting()
                .AddMainWindow<MainWindow>();

builder.HostBuilder.ConfigureServices((context, services) =>
            {
                services.AddTransient<MainMenuViewModel>()
                        .AddTransient<MainWindowViewModel>()
                        .AddTransient<TestWindow>()
                        .AddTransient<TestWindowViewModel>();
            });

var app = builder.Build();
app.Start();
```

#### Deriving from WpfApplication
You can derive from *WpfApplication*, which is a subclass of *System.Windows.Application*, and have the application container auto-configured, which works well for most implementations.

In App.xaml:
```
<wpf:WpfApplication x:Class="WpfApp3.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:wpf="clr-namespace:Tudormobile.Wpf;assembly=Tudormobile.WpfApp"
             xmlns:local="clr-namespace:WpfApp3"
             AutoConfigure="true"
             StartupUri="MainWindow.xaml">
    <Application.Resources>
         ...
    </Application.Resources>
</wpf:WpfApplication>
```
The key lines, here, being:
```
<wpf:WpfApplication ...
  xmlns:wpf="clr-namespace:Tudormobile.Wpf;assembly=Tudormobile.WpfApp"
  AutoConfigure="true"
 ...
 ```
 In App.xaml.cs code:
 ```
    public partial class App : WpfApplication
    {
        protected override void OnMainWindowCreated()
        {
            // ... add code here, for example:
            Help.Register(MainWindow, "https://www.google.com");
        }
   }
```
