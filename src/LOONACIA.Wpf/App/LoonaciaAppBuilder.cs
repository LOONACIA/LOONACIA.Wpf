using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Windows;

namespace LOONACIA.Wpf.App;
public class LoonaciaAppBuilder<T> where T : LoonaciaApp
{
	private readonly List<Func<ResourceDictionary>> _resourceFactory = new();

	private readonly IServiceCollection _services;

	private readonly T? _application;

	private IBootstrapper? _bootstrapper;

	public LoonaciaAppBuilder()
	{
		_services = new ServiceCollection();
	}

	public LoonaciaAppBuilder(T application) : this()
	{
		_application = application;
	}

	public static LoonaciaAppBuilder<T> Create() => new();

	public LoonaciaAppBuilder<T> BootstrapWith(IBootstrapper bootstrapper)
	{
		if (_bootstrapper != null && ReferenceEquals(_bootstrapper, bootstrapper))
		{
			throw new InvalidOperationException("AppBuilder is already wired with another bootstrapper.");
		}

		_bootstrapper = bootstrapper;
		return this;
	}

	public LoonaciaAppBuilder<T> AddResource(ResourceDictionary resourceDictionary)
	{
		_resourceFactory.Add(() => resourceDictionary);
		return this;
	}

	public LoonaciaAppBuilder<T> AddResource<TResource>()
		where TResource : ResourceDictionary, new()
	{
		_resourceFactory.Add(() => new TResource());
		return this;
	}

	public LoonaciaAppBuilder<T> AddResource(Uri resourceUri)
	{
		_resourceFactory.Add(() => new ResourceDictionary()
		{
			Source = resourceUri
		});
		return this;
	}

	public LoonaciaAppBuilder<T> AddResource(string packUri)
	{
		return AddResource(new Uri(packUri, UriKind.RelativeOrAbsolute));
	}

	public LoonaciaAppBuilder<T> BootstrapWith<TBootstrapper>()
		where TBootstrapper : IBootstrapper, new()
	{
		return BootstrapWith(new TBootstrapper());
	}

	public T Build()
	{
		IServiceProvider serviceProvider = Bootstrap();

		T app = _application ?? serviceProvider.CreateInstanceForUnregisteredType<T>();

		foreach (var getResource in _resourceFactory)
		{
			app.Resources.MergedDictionaries.Add(getResource());
		}

		return app;
	}

	protected virtual IServiceProvider Bootstrap()
	{
		_bootstrapper?.ConfigureServices(_services);

		return _services.BuildServiceProvider();
	}
}