using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RaceManagementApp.UI.Helpers
{
    public static class SearchHelper
    {

        public static List<T> GetFilteredData<T>(IEnumerable<T> data, DateTime? fromDate, DateTime? toDate, IEnumerable<string> propertyNames = null) where T : class
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            var filteredData = data.AsQueryable();

            if (fromDate.HasValue)
            {
                filteredData = filteredData.Where(item =>
                    GetPropertyValue<DateTime?>(item, "ord_enq_date") >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                filteredData = filteredData.Where(item =>
                    GetPropertyValue<DateTime?>(item, "ord_enq_date") <= toDate.Value);
            }

            if (propertyNames != null && propertyNames.Any())
            {
                filteredData = filteredData.Where(item =>
                    propertyNames.All(p =>
                        !string.IsNullOrEmpty(GetPropertyValue<string>(item, p))));
            }

            return filteredData.ToList();
        }

        public static TProp GetPropertyValue<TProp>(object obj, string propertyName)
        {
            var property = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (property != null)
            {
                var value = property.GetValue(obj);
                if (value is TProp castedValue)
                {
                    return castedValue;
                }
                if (value is DateTimeOffset dateTimeOffsetValue && typeof(TProp) == typeof(DateTime?))
                {
                    return (TProp)(object)(dateTimeOffsetValue.DateTime);
                }
            }
            return default;
        }



        public static Dictionary<string, string> GetPropertiesForSearch<T>(T obj, IEnumerable<string> propertyNames = null) where T : class
        {

            if (obj == null) throw new ArgumentNullException(nameof(obj));

            // Get all public instance properties of the object
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Filter properties based on specified names
            if (propertyNames != null && propertyNames.Any())
            {
                properties = properties.Where(p => propertyNames.Contains(p.Name)).ToArray();
            }

            // Convert filtered properties to a dictionary with property names as keys and string representations of values
            return properties.ToDictionary(
                prop => prop.Name, // Property name as the key
                prop => prop.GetValue(obj)?.ToString() ?? string.Empty // Property value as the string value
            );
        }

        public static IEnumerable<T> FilterDataBySearch<T>(
            IEnumerable<T> dataList,
            string searchText,
            IEnumerable<string> columns = null)
            where T : class
        {
            if (dataList == null) throw new ArgumentNullException(nameof(dataList));
            if (string.IsNullOrWhiteSpace(searchText)) return dataList; // No search text, return all data

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            
            if (columns != null && columns.Any())
            {
                properties = properties.Where(p => columns.Contains(p.Name)).ToArray();
            }

            return dataList.Where(item =>
            {
                foreach (var property in properties)
                {
                    var value = property.GetValue(item)?.ToString() ?? string.Empty;
                    if (value.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        return true;
                    }
                }
                return false;
            });
        }


        public static IEnumerable<T> FilterDataByDate<T>(
            IEnumerable<T> dataList,
            DateTimeOffset? fromDate = null,
            DateTimeOffset? toDate = null,
            IEnumerable<string> columns = null)
            where T : class
        {
            if (dataList == null) throw new ArgumentNullException(nameof(dataList));

            if (!fromDate.HasValue && !toDate.HasValue)
                return dataList;

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            if (columns != null && columns.Any())
            {
                properties = properties.Where(p => columns.Contains(p.Name)).ToArray();
            }

            return dataList.Where(item =>
            {

                foreach (var property in properties)
                {
                    if (property.PropertyType == typeof(DateTimeOffset?))
                    {
                        var dateValue = (DateTimeOffset?)property.GetValue(item);

                        if (fromDate.HasValue && dateValue <= fromDate.Value)
                        {
                            return false;
                        }

                        if (toDate.HasValue && dateValue >= toDate.Value)
                        {
                            return false;
                        }

                        return true;
                    }
                }

                return true;
            });
        }


        public static IEnumerable<T> FilterDataByBoolean<T>(
            IEnumerable<T> dataList,
            bool? boolValue,
            IEnumerable<string> columns = null)
            where T : class
        {
            if (dataList == null) throw new ArgumentNullException(nameof(dataList));

            if (!boolValue.HasValue)
            {
                return dataList;
            }

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            if (columns != null && columns.Any())
            {
                properties = properties.Where(p => columns.Contains(p.Name)).ToArray();
            }

            return dataList.Where(item =>
            {
                foreach (var property in properties)
                {
                    if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
                    {
                        var value = property.GetValue(item);

                        if (boolValue == true)
                        {
                            if (value is bool boolValueTyped && boolValueTyped == true)
                            {
                                return true;
                            }
                        }
                        else if (boolValue == false)
                        {
                            if (value == null || (value is bool boolValueTyped && boolValueTyped == false))
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            });
        }
    }
}