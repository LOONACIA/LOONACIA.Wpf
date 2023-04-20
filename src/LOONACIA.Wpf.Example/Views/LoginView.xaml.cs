using LOONACIA.Wpf.App;
using LOONACIA.Wpf.Example.ViewModels;
using System.Windows.Controls;

namespace LOONACIA.Wpf.Example.Views;
public partial class LoginView : UserControl
{
	public LoginView()
	{
		DataContext = LoonaciaApp.GetService<LoginViewModel>();
		InitializeComponent();
	}
}
