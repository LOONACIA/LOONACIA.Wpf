using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace LOONACIA.Wpf.App;
public abstract class LoonaciaApp : Application
{
	public LoonaciaApp(IServiceProvider serviceProvider)
	{
		ServiceProvider = serviceProvider;
	}

	public IServiceProvider ServiceProvider { get; internal set; }

	public static T? GetService<T>() where T : class
	{
		if (Current is not LoonaciaApp app)
		{
			throw new InvalidOperationException($"Current Application does not inherit {typeof(LoonaciaApp)}");
		}

		return app.ServiceProvider.GetService<T>();
	}

	public static T GetRequiredService<T>() where T : class
	{
		if (GetService<T>() is not T service)
		{
			throw new ArgumentException($"{typeof(T)} is not registered.");
		}

		return service;
	}

	protected abstract Window CreateWindow();

	protected override void OnStartup(StartupEventArgs e)
	{
		base.OnStartup(e);

		var win = CreateWindow();
		if (win != null)
		{
			MainWindow = win;
			OnWindowCreated();
		}

		Launch();
	}

	protected virtual void Launch()
	{
		MainWindow?.Show();
		OnLaunched();
	}

	protected virtual void OnWindowCreated()
	{
	}

	protected virtual void OnLaunched()
	{
	}
}