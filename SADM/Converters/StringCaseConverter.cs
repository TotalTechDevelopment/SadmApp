using System;
using System.Globalization;
using Xamarin.Forms;

namespace SADM.Converters
{
    public class StringCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = System.Convert.ToString(value) ?? string.Empty;
            var param = System.Convert.ToString(parameter) ?? "u";
            return param.ToUpper() == "U" ? text.ToUpper() : text.ToLower();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
