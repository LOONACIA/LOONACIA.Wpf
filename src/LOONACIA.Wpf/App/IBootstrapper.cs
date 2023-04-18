using Microsoft.Extensions.DependencyInjection;

namespace LOONACIA.Wpf.App;
public interface IBootstrapper
{
    void ConfigureServices(IServiceCollection services);
}
