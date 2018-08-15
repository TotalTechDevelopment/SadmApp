using System;
using System.Globalization;
using SADM.Enums;
using Xamarin.Forms;

namespace SADM.Converters
{
    public class NotificationTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var colorName = "PageBackgroundColor";
            if (value is NotificationType notificationType)
            {
                colorName = notificationType == NotificationType.Error ? "RowDarkColor" : "RowLightColor";
            } 
            return (Color)Application.Current.Resources[colorName];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
