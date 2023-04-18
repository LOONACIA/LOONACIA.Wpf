using Microsoft.Win32;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace LOONACIA.Wpf.Behaviors;
public class SaveFileDialogBehavior : Behavior<ButtonBase>
{
	public static readonly DependencyProperty SaveCommandProperty =
		DependencyProperty.Register(nameof(SaveCommand), typeof(ICommand), typeof(SaveFileDialogBehavior), new PropertyMetadata(null));

	public static readonly DependencyProperty FilterProperty =
		DependencyProperty.Register(nameof(Filter), typeof(string), typeof(SaveFileDialogBehavior), new PropertyMetadata(string.Empty));

	public static readonly DependencyProperty InitialDirectoryProperty =
		DependencyProperty.Register(nameof(InitialDirectory), typeof(string), typeof(SaveFileDialogBehavior), new PropertyMetadata(string.Empty));

	public ICommand SaveCommand
	{
		get { return (ICommand)GetValue(SaveCommandProperty); }
		set { SetValue(SaveCommandProperty, value); }
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

		SaveFileDialog sfd = new()
		{
			Filter = filter
		};

		if (!string.IsNullOrWhiteSpace(InitialDirectory))
		{
			sfd.InitialDirectory = InitialDirectory;
		}

		if (sfd.ShowDialog() is not true)
		{
			return;
		}

		SaveCommand?.Execute(sfd.FileName);
	}
}