using LOONACIA.Wpf.App;
using LOONACIA.Wpf.Example.Services;
using LOONACIA.Wpf.Example.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LOONACIA.Wpf.Example.Bootstrap;
internal class Bootstrapper : IBootstrapper
{
	public void ConfigureAppConfiguration(IConfigurationBuilder builder)
	{
	}

	public void ConfigureServices(LoonaciaAppBuilderContext context, IServiceCollection services)
	{
		services.AddSingleton<ILoginService, LoginService>();

		services.AddTransient<LoginViewModel>();
	}
}