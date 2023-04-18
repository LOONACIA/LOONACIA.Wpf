using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Windows;

namespace LOONACIA.Wpf.App;
public class LoonaAppBuilder<T> where T : Application, new()
{
	private readonly List<IBootstrapper> _bootstrappers = new();

	private readonly List<Func<ResourceDictionary>> _resourceFactory = new();

	private readonly IServiceCollection _services;

	private readonly T? _application;

	private Ioc? _ioc;

	public LoonaAppBuilder()
	{
		_services = new ServiceCollection();
	}

	public LoonaAppBuilder(T application) : this()
	{
		_application = application;
	}

	public static LoonaAppBuilder<T> Create() => new();

	public LoonaAppBuilder<T> WithIoc(Ioc ioc)
	{
		_ioc = ioc;
		return this;
	}

	public LoonaAppBuilder<T> BootstrapWith(IBootstrapper bootstrapper)
	{
		_bootstrappers.Add(bootstrapper);
		return this;
	}

	public LoonaAppBuilder<T> AddResource(ResourceDictionary resourceDictionary)
	{
		_resourceFactory.Add(() => resourceDictionary);
		return this;
	}

	public LoonaAppBuilder<T> AddResource<TResource>()
		where TResource : ResourceDictionary, new()
	{
		_resourceFactory.Add(() => new TResource());
		return this;
	}

	public LoonaAppBuilder<T> AddResource(Uri resourceUri)
	{
		_resourceFactory.Add(() => new ResourceDictionary()
		{
			Source = resourceUri
		});
		return this;
	}

	public LoonaAppBuilder<T> AddResource(string packUri)
	{
		return AddResource(new Uri(packUri, UriKind.RelativeOrAbsolute));
	}

	public LoonaAppBuilder<T> BootstrapWith<TBootstrapper>()
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

		T app = _application ?? new();

		foreach (var getResource in _resourceFactory)
		{
			app.Resources.MergedDictionaries.Add(getResource());
		}

		return app;
	}
}
