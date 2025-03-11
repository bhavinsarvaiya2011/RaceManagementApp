using System;
using System.Globalization;
using Microsoft.UI.Xaml.Data;

namespace RaceManagementApp.UI.Helpers
{
    public class DateFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTimeOffset dateTime)
            {
                return dateTime.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else if (value is DateTimeOffset?)
            {
                var nullableDto = (DateTimeOffset?)value;
                return nullableDto?.Date.ToString("dd/MM/yyyy") ?? string.Empty;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (DateTimeOffset.TryParseExact(value as string, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTimeOffset dateTime))
            {
                return dateTime;
            }
            return value;
        }
    }
}
