using System.Windows;
using Microsoft.Win32;

namespace LOONACIA.Wpf.Controls;
public class OpenFileDialogButton : FileDialogButtonBase
{
	public static readonly DependencyProperty MultiselectProperty =
		DependencyProperty.Register(nameof(Multiselect), typeof(bool), typeof(OpenFileDialogButton), new PropertyMetadata(false));

	public bool Multiselect
	{
		get { return (bool)GetValue(MultiselectProperty); }
		set { SetValue(MultiselectProperty, value); }
	}

	protected override FileDialogInfo CreateFileDialog()
	{
		bool multiselect = Multiselect;
		OpenFileDialog ofd = new()
		{
			Multiselect = multiselect
		};

		return new(ofd, dialog => multiselect ? dialog.FileNames : dialog.FileName);
	}
}