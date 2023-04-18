# LOONACIA.Wpf

## How to use
**Define Application class**
```csharp
class MyApp : Application
{
}
```

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
class Entry
{
	[STAThread]
	public static void Main()
	{
		string packuri = "My ResourceDictionary uri to merge here";

		var builder = new LoonaAppBuilder<MyApp>()
			.WithIoc(MyIocInstance) // If you use Ioc.Default, can skip this line.
			.BootstrapWith<MyBootstrapper>()
			.AddResource<MyResourceDictionaryClass>()
			.AddResource(packuri);

		MyApp app = builder.Build();
		app.Run();
	}
}
```
or
```csharp
class Entry
{
	[STAThread]
	public static void Main()
	{
		string packuri = "My ResourceDictionary uri to merge here";

		LoonaAppBuilder<MyApp>
			.Create()
			.WithIoc(MyIocInstance) // If you use Ioc.Default, can skip this line.
			.BootstrapWith<MyBootstrapper>()
			.AddResource<MyResourceDictionaryClass>()
			.AddResource(packuri)
			.Build()
			.Run();
	}
}
```