# Sample Applications
- WpfApp0
	- Demostrates *minimal* use of the library. The application integrates its own local ***MessageBoxService*** (using *AddSingleton()*) and utilizes the DI container (*AddTransient()*) to create the DataContext for the MainWindow in its constructor.
```
    protected override void OnConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IMessageBoxService, MessageBoxService>();
        services.AddTransient<MainWindowViewModel>();
    }
```
  
```
    public MainWindow()
    {
        InitializeComponent();
        DataContext = App.Current.Services.GetService<MainWindowViewModel>();
    }

```
- WpfApp1
    - Demonstrates the use of the *AppBuilder* to create and initialize a *WpfApp* object that can be used alongside *System.Windows.Application*. 
```
protected override void OnStartup(StartupEventArgs e)
{
    var app = WpfApp.CreateBuilder().Build();
    app.Start<MainWindow>();
    base.OnStartup(e);
}
```
- WpfApp3
    - Uses both the application object and a number of services from the library.
- WpfApp4
    - Similar to WpfApp3 but with some experimental features using reflection and attributes to automatically configure some ICommand instances. 

- WpfApp5
    - Demonstratoes the use of the IDialogService, FilePickers, and MessageBox commands.
    