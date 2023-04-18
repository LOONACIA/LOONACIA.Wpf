using System;
using System.Windows.Markup;

namespace LOONACIA.Wpf.Extensions;
public class FractionToDouble : MarkupExtension
{
	public string? Denominator { get; set; }

	public string? Numerator { get; set; }

	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		if (double.TryParse(Denominator, out var denominator) && double.TryParse(Numerator, out var numerator))
		{
			return numerator / denominator;
		}

		throw new InvalidOperationException("Inputs is invalid");
	}
}
