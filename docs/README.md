# WPF Application Container
**Tudormobile.WpfApp** provides an extensible application container for WPF applications providing dependency injection and application lifecycle management.  

[Source Code](https://github.com/tudormobile/WpfApp) | [Documentation](https://tudormobile.github.io/WpfApp/) | [API documentation](https://tudormobile.github.io/WpfApp/api/Tudormobile.html)
## Getting Started
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
```
using Tudormobile.Wpf;

var builder = WpfApp.CreateBuilder();
var app = builder.Build();

app.Run();
```
### Key Features
- Application host, including dependency injection container (optional)
- Application lifecycle management
- Application window management
- Built-in support for MVVM patterns with services to create and display windows, dialogs, file pickers, and more.
- Extensible framework.

### Feedback
**Tudormobile.WpfApp** is released as open source under the MIT license. Bug reports and contributions are welcome at [the GitHub repository](https://github.com/tudormobile/WpfApp).