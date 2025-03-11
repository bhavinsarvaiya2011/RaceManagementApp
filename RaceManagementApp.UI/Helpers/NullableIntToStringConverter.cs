using Microsoft.UI.Xaml.Data;
using System;

namespace RaceManagementApp.UI.Helpers
{
    public class NullableIntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value is int intValue ? intValue.ToString("0.00") : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (int.TryParse(value as string, out var result))
            {
                return result;
            }
            return null; 
        }
    }
}
