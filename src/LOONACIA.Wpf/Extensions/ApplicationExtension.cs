using LOONACIA.Wpf.App;
using System.Windows;

namespace LOONACIA.Wpf;
public static class ApplicationExtension
{
	public static LoonaAppBuilder<T> CreateBuilder<T>(this T app) where T : Application, new()
	{
		return new LoonaAppBuilder<T>(app);
	}
}
