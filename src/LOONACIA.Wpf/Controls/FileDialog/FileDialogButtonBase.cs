using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace LOONACIA.Wpf.Controls;
public abstract class FileDialogButtonBase : Button
{
	public static readonly DependencyProperty FilterProperty =
		DependencyProperty.Register(nameof(Filter), typeof(string), typeof(FileDialogButtonBase), new PropertyMetadata(string.Empty));

	public static readonly DependencyProperty InitialDirectoryProperty =
		DependencyProperty.Register(nameof(InitialDirectory), typeof(string), typeof(FileDialogButtonBase), new PropertyMetadata(string.Empty));

	public string Filter
	{
		get { return (string)GetValue(FilterProperty); }
		set { SetValue(FilterProperty, value); }
	}

	public string InitialDirectory
	{
		get { return (string)GetValue(InitialDirectoryProperty); }
		set { SetValue(InitialDirectoryProperty, value); }
	}

	protected override void OnClick()
	{
		ShowDialog();
	}

	protected virtual void ShowDialog()
	{
		FileDialogInfo info = CreateFileDialog();
		FileDialog dialog = info.Dialog;
		dialog.Filter = Filter;
		dialog.InitialDirectory = InitialDirectory;

		if (dialog.ShowDialog() is not true)
		{
			return;
		}

		object? parameter = info.GetParameters(dialog);
		if (Command?.CanExecute(parameter) is true)
		{
			Command.Execute(parameter);
		}
	}

	protected abstract FileDialogInfo CreateFileDialog();
}
