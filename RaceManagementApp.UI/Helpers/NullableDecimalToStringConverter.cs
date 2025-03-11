using Microsoft.UI.Xaml.Data;
using System;

namespace RaceManagementApp.UI.Helpers
{
    public class NullableDecimalToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value is decimal decimalValue ? decimalValue.ToString("0.00") : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (decimal.TryParse(value as string, out var result))
            {
                return result;
            }
            return null;
        }
    }
}
