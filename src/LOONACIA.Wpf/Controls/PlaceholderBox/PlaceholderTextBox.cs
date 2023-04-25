using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LOONACIA.Wpf.Controls;
public class PlaceholderTextBox : TextBox
{
	public static readonly DependencyProperty PlaceholderProperty =
		DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(PlaceholderTextBox), new PropertyMetadata(string.Empty));

	public static readonly DependencyProperty PlaceholderBrushProperty =
		DependencyProperty.Register(nameof(PlaceholderBrush), typeof(Brush), typeof(PlaceholderTextBox), new PropertyMetadata(null));

	public static readonly DependencyProperty CornerRadiusProperty =
		DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(PlaceholderTextBox), new PropertyMetadata(default));

	static PlaceholderTextBox()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(PlaceholderTextBox), new FrameworkPropertyMetadata(typeof(PlaceholderTextBox)));
	}

	public string Placeholder
	{
		get { return (string)GetValue(PlaceholderProperty); }
		set { SetValue(PlaceholderProperty, value); }
	}

	public Brush PlaceholderBrush
	{
		get { return (Brush)GetValue(PlaceholderBrushProperty); }
		set { SetValue(PlaceholderBrushProperty, value); }
	}

	public CornerRadius CornerRadius
	{
		get { return (CornerRadius)GetValue(CornerRadiusProperty); }
		set { SetValue(CornerRadiusProperty, value); }
	}
}
