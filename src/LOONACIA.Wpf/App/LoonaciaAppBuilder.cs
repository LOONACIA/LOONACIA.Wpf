using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace LOONACIA.Wpf.App;
public class LoonaciaAppBuilder<T> where T : LoonaciaApp
{
	private readonly List<Func<ResourceDictionary>> _resourceFactory = new();

	private readonly IServiceCollection _services;

	private IBootstrapper? _bootstrapper;

	private LoonaciaAppBuilderContext? _appBuilderContext;

	private IConfiguration? _appConfiguration;

	private IServiceProvider? _appServiceProvider;

	public LoonaciaAppBuilder()
	{
		_services = new ServiceCollection();
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
		Bootstrap();

		T app = _appServiceProvider.CreateInstanceForUnregisteredType<T>();

		foreach (var getResource in _resourceFactory)
		{
			app.Resources.MergedDictionaries.Add(getResource());
		}

		return app;
	}

	[MemberNotNull(nameof(_appConfiguration))]
	[MemberNotNull(nameof(_appServiceProvider))]
	private void Bootstrap()
	{
		InitializeAppConfiguration();
		InitializeBuilderContext();
		InitializeAppServiceProvider();
	}

	[MemberNotNull(nameof(_appBuilderContext))]
	private void InitializeBuilderContext()
	{
		_appBuilderContext = new()
		{
			Configuration = _appConfiguration!
		};
	}

	[MemberNotNull(nameof(_appConfiguration))]
	private void InitializeAppConfiguration()
	{
		ConfigurationBuilder builder = new();
		_bootstrapper?.ConfigureAppConfiguration(builder);
		_appConfiguration = builder.Build();
	}

	[MemberNotNull(nameof(_appServiceProvider))]
	private void InitializeAppServiceProvider()
	{
		_bootstrapper?.ConfigureServices(_appBuilderContext!, _services);

		_appServiceProvider = _services.BuildServiceProvider();
	}
}