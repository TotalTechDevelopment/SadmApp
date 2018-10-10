using System;
using System.Globalization;
using Xamarin.Forms;

namespace SADM.Converters
{
    public class DoubleToAmountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType = null, object parameter = null, CultureInfo culture = null)
        {
            if(System.Convert.ToString(value) is string valueString)
            {
                /*var textToAdd = string.Empty;
                if(string.IsNullOrWhiteSpace(valueString))
                {
                    valueString = "0";
                }
                var parts = valueString.Split('.');
                if(parts.Length == 1)
                {
                    textToAdd = ".00";
                }
                else
                {
                    var decimals = parts[1];
                    if(decimals.Length == 1)
                    {
                        textToAdd = $".{decimals}0";
                    }
                    else if(decimals.Length > 2)
                    {
                        textToAdd = $".{decimals.Substring(0, 2)}";
                    }
                }
                return $"$ {parts[0]}{textToAdd}";
                */

                if (string.IsNullOrWhiteSpace(valueString))
                    return "$ 0.00";

                return int.Parse(valueString).ToString("C");
            }
            return "$ 0.00";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
