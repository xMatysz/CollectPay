using System.Globalization;

namespace CollectionPay.Maui.Converters;

public class SharedToColorConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return value is true
			? Colors.Magenta
			: Colors.IndianRed;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotSupportedException();
	}
}