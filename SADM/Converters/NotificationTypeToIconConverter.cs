using System;
using System.Globalization;
using SADM.Enums;
using Xamarin.Forms;

namespace SADM.Converters
{
    public class NotificationTypeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var icon = "info.png";
            if(value is NotificationType notificationType)
            {
                if(notificationType == NotificationType.Success)
                {
                    icon = "ic_success.png";
                }
                else if (notificationType == NotificationType.Error)
                {
                    icon = "ic_error.png";
                }
                else
                {
                    icon = "info.png";
                }
            }
            return icon;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
