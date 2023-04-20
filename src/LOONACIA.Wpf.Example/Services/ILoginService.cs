namespace LOONACIA.Wpf.Example.Services;
public interface ILoginService
{
	bool TryLogin(string? username, string? password);
}
