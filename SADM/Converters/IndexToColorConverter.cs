using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace SADM.Converters
{
    public class IndexToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) return (Color)Application.Current.Resources["PageBackgroundColor"];
            var colorName = ((ListView)parameter).ItemsSource.Cast<object>().ToList().IndexOf(value) % 2 == default(int) 
                                        ? "RowLightColor" : "RowDarkColor";
            return (Color)Application.Current.Resources[colorName];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
