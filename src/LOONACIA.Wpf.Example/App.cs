using LOONACIA.Wpf.App;
using System;
using System.Windows;

namespace LOONACIA.Wpf.Example;
internal class App : LoonaciaApp
{
	public App(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override Window CreateWindow() => new MainWindow();
}
