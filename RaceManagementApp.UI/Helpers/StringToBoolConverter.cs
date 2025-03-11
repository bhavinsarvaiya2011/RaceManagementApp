using Microsoft.UI.Xaml.Data;
using System;

namespace RaceManagementApp.UI.Helpers
{
    public class StringToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value != null && value.ToString() == parameter.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? parameter.ToString() : null;
        }
    }
}
