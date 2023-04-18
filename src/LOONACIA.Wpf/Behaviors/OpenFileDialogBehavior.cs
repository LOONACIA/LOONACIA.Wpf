using Microsoft.Win32;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace LOONACIA.Wpf.Behaviors;
public class OpenFileDialogBehavior : Behavior<ButtonBase>
{
	public static readonly DependencyProperty OpenCommandProperty =
		DependencyProperty.Register(nameof(OpenCommand), typeof(ICommand), typeof(OpenFileDialogBehavior), new PropertyMetadata(null));

	public static readonly DependencyProperty MultiSelectProperty =
		DependencyProperty.Register(nameof(MultiSelect), typeof(bool), typeof(OpenFileDialogBehavior), new PropertyMetadata(false));

	public static readonly DependencyProperty FilterProperty =
		DependencyProperty.Register(nameof(Filter), typeof(string), typeof(OpenFileDialogBehavior), new PropertyMetadata(string.Empty));

	public static readonly DependencyProperty InitialDirectoryProperty =
		DependencyProperty.Register(nameof(InitialDirectory), typeof(string), typeof(OpenFileDialogBehavior), new PropertyMetadata(string.Empty));

	public ICommand OpenCommand
	{
		get { return (ICommand)GetValue(OpenCommandProperty); }
		set { SetValue(OpenCommandProperty, value); }
	}

	public bool MultiSelect
	{
		get { return (bool)GetValue(MultiSelectProperty); }
		set { SetValue(MultiSelectProperty, value); }
	}

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

	protected override void OnAttached()
	{
		base.OnAttached();
		AssociatedObject.Click += OnClick;
	}

	protected override void OnDetaching()
	{
		AssociatedObject.Click -= OnClick;
		base.OnDetaching();
	}

	private void OnClick(object sender, RoutedEventArgs e)
	{
		string filter = Filter;
		if (string.IsNullOrWhiteSpace(filter))
		{
			filter = "All Files|*.*";
		}

		OpenFileDialog ofd = new()
		{
			Multiselect = MultiSelect,
			Filter = filter,
		};

		if (!string.IsNullOrWhiteSpace(InitialDirectory))
		{
			ofd.InitialDirectory = InitialDirectory;
		}

		if (ofd.ShowDialog() is not true)
		{
			return;
		}

		if (MultiSelect)
		{
			OpenCommand?.Execute(ofd.FileNames);
		}
		else
		{
			OpenCommand?.Execute(ofd.FileName);
		}
	}
}
