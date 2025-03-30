## Tudormobile Namespaces
The namespaces available in the Tudormobile.WpfApp library are described below. In several cases, including the namespace is required to expose extension methods provided.

### Tudormobile.WpfApp.dll

```
using Tudormobile.Wpf;
```

The ***WpfApp library*** namespaces

- [Tudormobile.Wpf](Tudormobile.Wpf.yml)
    - Root namespace for the WpfApp library.  
This is where you will find the WpfApp, WpfApplication, builder objects, and command line processing.
- [Tudormobile.Wpf.Services](Tudormobile.Wpf.Services.yml)
    - Library services.  
The interface definitions for the various available services are found here.
- [Tudormobile.Wpf.Commands](Tudormobile.Wpf.Commands.yml)
    - Built-in library commands.  
    Commands are available for invoking open/save file dialogs, print dialogs, message boxes, etc. Includes support for automatically associating methods in your classes to ICommand properties on view models without having to write boilerplate code.
    
# Release [!include[version](../../src/bin/release/ver.txt)]
Latest unit testing results are shown below.
[!include[summary](../../output/SummaryGithub.md)]