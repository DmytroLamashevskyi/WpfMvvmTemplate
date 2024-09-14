using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfMvvmTemplate.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public string DateFormat { get; set; } = "dd MMM yyyy";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is DateTime dateTime)
            {
                string format = parameter as string ?? DateFormat;
                return dateTime.ToString(format, culture);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(DateTime.TryParseExact(value as string, DateFormat, culture, DateTimeStyles.None, out DateTime dateTime))
                return dateTime;
            return Binding.DoNothing;
        }
    }
}
