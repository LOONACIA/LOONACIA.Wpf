# LOONACIA.Wpf

## How to use
**Define Application class**
```csharp
class MyApp : LoonaciaApp
{
	public MyApp(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override Window CreateWindow() => new MyWindow();
}
```

**In entry point**
```csharp
class Entry
{
	[STAThread]
	public static void Main()
	{
		LoonaciaAppBuilder<MyApp>.Create()
			.Build()
			.Run();
	}
}
```

### Add Bootstrapper
**Define Bootstrapper**
```csharp
class MyBootstrapper : IBootstrapper
{
	public void ConfigureServices(IServiceCollection services)
	{
		// Register services here.
	}
}
```

**In entry point**
```csharp
LoonaciaAppBuilder<MyApp>.Create()
	.BootstrapWith<MyBootstrapper>()
	.Build()
	.Run();
```

### Merge resource dictionaries
1. Using pack uri

```csharp
string packuri = "My ResourceDictionary uri to merge here";

LoonaciaAppBuilder<MyApp>.Create()
	.BootstrapWith<MyBootstrapper>()
	.AddResource(packuri)
	.Build()
	.Run();
```

2. Using ResourceDictionary class

**Define ResourceDictionary class**
```csharp
public partial class MyResource : ResourceDictionary
{
	public MyResource()
	{
		InitializeComponent();
	}
}
```
**In entry point**
```csharp
LoonaciaAppBuilder<MyApp>.Create()
	.BootstrapWith<MyBootstrapper>()
	.AddResource<MyResource>()
	.Build()
	.Run();
```

### Resolve the dependencies
```csharp
T? service = LoonaciaApp.GetService<T>();
T service = LoonaciaApp.GetRequiredService<T>();
```
or
```csharp
T? service = (Application.Current as LoonaciaApp).ServiceProvider.GetService<T>();
T service = (Application.Current as LoonaciaApp).ServiceProvider.GetRequiredService<T>();
```