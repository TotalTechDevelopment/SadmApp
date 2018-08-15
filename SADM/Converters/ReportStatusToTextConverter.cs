using System;
using System.Globalization;
using SADM.Enums;
using SADM.Models;
using Xamarin.Forms;

namespace SADM.Converters
{
    public class ReportStatusToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Report report)
            {
                switch(report.Status)
                {
                    case ReportStatus.Pending:
                        return targetType == typeof(ImageSource) ? "blueclock.png" : $"Tu solicitud con el número de reporte {report.Id} se esta procesando...";
                    case ReportStatus.Attended:
                        return targetType == typeof(ImageSource) ? "bluesuccess.png" : $"Tu solicitud con el número de reporte {report.Id} ha sido atendida.";
                    default:
                        throw new NotImplementedException();
                }
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}