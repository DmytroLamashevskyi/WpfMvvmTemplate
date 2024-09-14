using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfMvvmTemplate.Converters
{
    public class RangeToColorConverter : IValueConverter
    {
        public List<RangeColor> Ranges { get; set; }

        public RangeToColorConverter()
        {
            Ranges = new List<RangeColor>();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double numericValue = 0.0;
            if(value is double d)
                numericValue = d;
            else if(value is int i)
                numericValue = i;
            else
                return Brushes.Transparent;

            foreach(var range in Ranges)
            {
                if(numericValue >= range.Minimum && numericValue <= range.Maximum)
                    return new SolidColorBrush(range.Color);
            }

            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class RangeColor
    {
        public double Minimum { get; set; }
        public double Maximum { get; set; }
        public Color Color { get; set; }
    }
}
