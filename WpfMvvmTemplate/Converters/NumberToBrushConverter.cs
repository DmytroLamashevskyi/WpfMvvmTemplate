using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfMvvmTemplate.Converters
{
    public class NumberToBrushConverter : IValueConverter
    {
        public Brush LowBrush { get; set; } = Brushes.Green;
        public Brush MediumBrush { get; set; } = Brushes.Yellow;
        public Brush HighBrush { get; set; } = Brushes.Red;

        public double LowThreshold { get; set; } = 33.33;
        public double HighThreshold { get; set; } = 66.66;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is double doubleValue)
            {
                if(doubleValue <= LowThreshold)
                    return LowBrush;
                else if(doubleValue <= HighThreshold)
                    return MediumBrush;
                else
                    return HighBrush;
            }
            else if(value is int intValue)
            { 
                if(intValue <= LowThreshold)
                    return LowBrush;
                else if(intValue <= HighThreshold)
                    return MediumBrush;
                else
                    return HighBrush;
            }
            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
