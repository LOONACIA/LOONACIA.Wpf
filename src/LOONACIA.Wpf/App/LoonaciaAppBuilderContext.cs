using Microsoft.Extensions.Configuration;

namespace LOONACIA.Wpf.App;
public class LoonaciaAppBuilderContext
{
	public IConfiguration Configuration { get; internal set; } = default!;
}