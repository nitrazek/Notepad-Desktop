using System;
using System.Globalization;
using System.Windows.Data;

namespace NotepadDesktop.utils
{
    public class NotificationDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime notificationDate)
            {
                return "Data przypomnienia: " + notificationDate.ToString("dd.MM.yyyy HH:mm");
            }
            return "Data przypomnienia: -";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
