using System;
using System.Globalization;
using System.Windows.Data;

namespace LOONACIA.Wpf.Converters;
public class IsGreaterThanConverter : IValueConverter
{
	public bool NoValidation { get; set; }

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (!double.TryParse(value.ToString(), out double input))
		{
			return NoValidation ? false : throw new ArgumentException($"{nameof(value)}(value: {value}) is not a number");
		}

		if (!double.TryParse(parameter.ToString(), out double param))
		{
			return NoValidation ? false : throw new ArgumentException($"{nameof(parameter)}(value: {parameter}) is not a number");
		}

		return input > param;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return value;
	}
}