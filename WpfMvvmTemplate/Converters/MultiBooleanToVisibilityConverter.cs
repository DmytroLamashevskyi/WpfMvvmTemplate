using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace WpfMvvmTemplate.Converters
{
    public class MultiBooleanToVisibilityConverter : IMultiValueConverter
    {
        public bool Invert { get; set; }
        public bool Collapse { get; set; } = true;
        public bool UseAndOperator { get; set; } = true;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = UseAndOperator
                ? values.All(v => v is bool b && b)
                : values.Any(v => v is bool b && b);

            if(Invert)
                result = !result;

            return result ? Visibility.Visible : (Collapse ? Visibility.Collapsed : Visibility.Hidden);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
