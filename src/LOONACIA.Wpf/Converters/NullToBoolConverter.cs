using System;
using System.Globalization;
using System.Windows.Data;

namespace LOONACIA.Wpf.Converters;
public class NullToBoolConverter : IValueConverter
{
	/// <summary>
	/// Gets or sets a converter is inverted. If true, return value is null. otherwise, return value is not null.
	/// </summary>
	public bool IsInverted { get; set; }

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return IsInverted ? value is null : value is not null;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
