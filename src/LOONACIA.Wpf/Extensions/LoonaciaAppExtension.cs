using LOONACIA.Wpf.App;

namespace LOONACIA.Wpf.Extensions;
public static class LoonaciaAppExtension
{
	public static LoonaciaAppBuilder<T> CreateBuilder<T>(this T app) where T : LoonaciaApp, new()
	{
		return new LoonaciaAppBuilder<T>(app);
	}
}