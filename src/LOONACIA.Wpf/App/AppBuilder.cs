using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;

namespace LOONACIA.Wpf.App;
public class AppBuilder<T> where T : Application, new()
{
	private readonly List<IBootstrapper> _bootstrappers = new();

	private readonly IServiceCollection _services;

	private readonly T? _application;

	private Ioc? _ioc;

	public AppBuilder()
	{
		_services = new ServiceCollection();
	}

	public AppBuilder(T application) : this()
	{
		_application = application;
	}

	public static AppBuilder<T> Create() => new();

	public AppBuilder<T> WithIoc(Ioc ioc)
	{
		_ioc = ioc;
		return this;
	}

	public AppBuilder<T> BootstrapWith(IBootstrapper bootstrapper)
	{
		_bootstrappers.Add(bootstrapper);
		return this;
	}

	public AppBuilder<T> BootstrapWith<TBootstrapper>()
		where TBootstrapper : IBootstrapper, new()
	{
		return BootstrapWith(new TBootstrapper());
	}

	public T Build()
	{
		_ioc ??= Ioc.Default;

		foreach (var bootstrap in _bootstrappers)
		{
			bootstrap.ConfigureServices(_services);
		}

		_ioc.ConfigureServices(_services.BuildServiceProvider());

		return _application ?? new();
	}
}
