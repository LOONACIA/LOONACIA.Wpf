using LOONACIA.Wpf.App;
using LOONACIA.Wpf.Example.Bootstrap;
using LOONACIA.Wpf.Example.Resources;
using System;

namespace LOONACIA.Wpf.Example;
internal class Entry
{
	[STAThread]
	internal static void Main()
	{
		LoonaciaAppBuilder<App>.Create()
			.BootstrapWith<Bootstrapper>()
			.AddResource<AppResource>()
			.Build()
			.Run();
	}
}
