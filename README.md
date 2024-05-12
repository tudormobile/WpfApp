# WpfApp
WPF Application container

[![Build and Deploy](https://github.com/tudormobile/WpfApp/actions/workflows/dotnet.yml/badge.svg)](https://github.com/tudormobile/WpfApp/actions/workflows/dotnet.yml)  [![Publish Docs](https://github.com/tudormobile/WpfApp/actions/workflows/docs.yml/badge.svg)](https://github.com/tudormobile/WpfApp/actions/workflows/docs.yml)  [![Create Package Release](https://github.com/tudormobile/WpfApp/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/tudormobile/WpfApp/actions/workflows/dotnet.yml)

Copyright (C) 2024 Tudormobile LLC
### Quick Start

```
using Tudormobile.Wpf;

var builder = WpfApp.CreateBuilder();
var app = builder.Build();

app.Run();
```
- This code creates an extensible IWpfAppBuilder and a IWpfApp with preconfigured defaults that closely match the '*System.Windows.Application*' implementation bundled with the WPF framework.
- Runs the application using the framework application lifecycle defaults.

[NuGET Package README](docs/README.md) | [Source Code README](src/README.md) | [API Documentation](https://tudormobile.github.io/WpfApp/)
