using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;

namespace RaceManagementApp.UI.Helpers
{
	public class NullToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			bool invert = parameter as string == "Invert";
			bool isNull = string.IsNullOrEmpty(value as string);

			return (isNull ^ invert) ? Visibility.Visible : Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
