using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LOONACIA.Wpf.Converters;
internal class NullToVisibilityConverter : IValueConverter
{
	/// <summary>
	/// Gets or sets a converter is inverted. If true, return <see cref="Visibility.Visible"/> when value is null. otherwise, return <see cref="Visibility.Visible"/> when value is not null.
	/// </summary>
	public bool IsInverted { get; set; }

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (IsInverted)
		{
			return value is null ? Visibility.Visible : Visibility.Collapsed;
		}
		else
		{
			return value is not null ? Visibility.Visible : Visibility.Collapsed;
		}
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return IsInverted ? value is Visibility.Visible : value is not Visibility.Visible;
	}
}
