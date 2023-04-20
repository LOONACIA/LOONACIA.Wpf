using LOONACIA.Wpf.Example.Input;
using LOONACIA.Wpf.Example.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace LOONACIA.Wpf.Example.ViewModels;
public class LoginViewModel : INotifyPropertyChanged
{
	private readonly ILoginService _loginService;

	private string? _id;

	private string? _password;

	private bool _loginSucceed;

	private SimpleRelayCommand? _loginCommand;

	public LoginViewModel(ILoginService loginService)
	{
		_loginService = loginService;
	}

	public string? Id
	{
		get => _id;
		set { _id = value; OnPropertyChanged(); }
	}

	public string? Password
	{
		get => _password;
		set { _password = value; OnPropertyChanged(); }
	}

	public bool LoginSucceed
	{
		get => _loginSucceed;
		set { _loginSucceed = value; OnPropertyChanged(); }
	}

	public ICommand LoginCommand => _loginCommand ??= new SimpleRelayCommand(Login, CanLogin);

	private bool CanLogin(object? parameter)
	{
		return !string.IsNullOrWhiteSpace(_id);
	}

	private void Login()
	{
		LoginSucceed = _loginService.TryLogin(Id, Password);
	}

	public event PropertyChangedEventHandler? PropertyChanged;

	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new(propertyName));
	}
}
