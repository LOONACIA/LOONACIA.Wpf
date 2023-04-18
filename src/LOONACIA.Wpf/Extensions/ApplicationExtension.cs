using LOONACIA.Wpf.App;
using System.Windows;

namespace LOONACIA.Wpf;
public static class ApplicationExtension
{
	public static LunaAppBuilder<T> CreateBuilder<T>(this T app) where T : Application, new()
	{
		return new LunaAppBuilder<T>(app);
	}
}
