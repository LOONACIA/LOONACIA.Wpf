using System.Windows;
using System.Windows.Controls;

namespace LOONACIA.Wpf.Controls;
public class PlaceholderTextBox : TextBox
{
	public static readonly DependencyProperty PlaceholderProperty =
		DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(PlaceholderTextBox), new PropertyMetadata(string.Empty));

	static PlaceholderTextBox()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(PlaceholderTextBox), new FrameworkPropertyMetadata(typeof(PlaceholderTextBox)));
	}

	public string Placeholder
	{
		get { return (string)GetValue(PlaceholderProperty); }
		set { SetValue(PlaceholderProperty, value); }
	}
}
