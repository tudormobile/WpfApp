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