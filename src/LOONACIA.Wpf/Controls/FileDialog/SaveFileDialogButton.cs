using Microsoft.Win32;

namespace LOONACIA.Wpf.Controls;
public class SaveFileDialogButton : FileDialogButtonBase
{
	protected override FileDialogInfo CreateFileDialog()
	{
		return new(new SaveFileDialog(), dialog => dialog.FileName);
	}
}