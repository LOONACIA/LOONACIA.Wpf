using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Windows;

namespace LOONACIA.Wpf.App;
public class LunaAppBuilder<T> where T : Application, new()
{
	private readonly List<IBootstrapper> _bootstrappers = new();

	private readonly List<ResourceDictionary> _resources = new();

	private readonly IServiceCollection _services;

	private readonly T? _application;

	private Ioc? _ioc;

	public LunaAppBuilder()
	{
		_services = new ServiceCollection();
	}

	public LunaAppBuilder(T application) : this()
	{
		_application = application;
	}

	public static LunaAppBuilder<T> Create() => new();

	public LunaAppBuilder<T> WithIoc(Ioc ioc)
	{
		_ioc = ioc;
		return this;
	}

	public LunaAppBuilder<T> BootstrapWith(IBootstrapper bootstrapper)
	{
		_bootstrappers.Add(bootstrapper);
		return this;
	}

	public LunaAppBuilder<T> AddResource(ResourceDictionary resourceDictionary)
	{
		_resources.Add(resourceDictionary);
		return this;
	}

	public LunaAppBuilder<T> AddResource<TResource>()
		where TResource : ResourceDictionary, new()
	{
		return AddResource(new TResource());
	}

	public LunaAppBuilder<T> AddResource(Uri resourceUri)
	{
		return AddResource(new ResourceDictionary()
		{
			Source = resourceUri
		});
	}

	public LunaAppBuilder<T> AddResource(string packUri)
	{
		return AddResource(new Uri(packUri, UriKind.RelativeOrAbsolute));
	}

	public LunaAppBuilder<T> BootstrapWith<TBootstrapper>()
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
		foreach (var resource in _resources)
		{
			app.Resources.MergedDictionaries.Add(resource);
		}

		return app;
	}
}
