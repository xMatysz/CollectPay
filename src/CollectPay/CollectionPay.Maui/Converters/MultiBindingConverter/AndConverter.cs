using System.Globalization;

namespace CollectionPay.Maui.Converters.MultiBindingConverter;

public class AndConverter : IMultiValueConverter
{
	public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
	{
		return values.Length > 0 && values.All(val => val is not null && (bool)val);
	}

	public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
	{
		return null;
	}
}