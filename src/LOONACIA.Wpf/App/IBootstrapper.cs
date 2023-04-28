using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LOONACIA.Wpf.App;
public interface IBootstrapper
{
	void ConfigureAppConfiguration(IConfigurationBuilder builder);

	void ConfigureServices(LoonaciaAppBuilderContext context, IServiceCollection services);
}