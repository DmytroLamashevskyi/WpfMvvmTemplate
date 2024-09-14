using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfMvvmTemplate.Converters
{
    public class NumberToColorConverter : IValueConverter
    {
        public Color StartColor { get; set; } = Colors.Blue;
        public Color EndColor { get; set; } = Colors.Red;
        public double Minimum { get; set; } = 0;
        public double Maximum { get; set; } = 100;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double numericValue = 0;
            if(value is double d)
                numericValue = d;
            else if(value is int i)
                numericValue = i;
            else
                return new SolidColorBrush(StartColor);

            double range = Maximum - Minimum;
            double fraction = (numericValue - Minimum) / range;
            fraction = Math.Min(Math.Max(fraction, 0.0), 1.0); // Clamp between 0 and 1

            Color color = InterpolateColor(StartColor, EndColor, fraction);
            return new SolidColorBrush(color);
        }

        private Color InterpolateColor(Color startColor, Color endColor, double fraction)
        {
            byte a = (byte)(startColor.A + (endColor.A - startColor.A) * fraction);
            byte r = (byte)(startColor.R + (endColor.R - startColor.R) * fraction);
            byte g = (byte)(startColor.G + (endColor.G - startColor.G) * fraction);
            byte b = (byte)(startColor.B + (endColor.B - startColor.B) * fraction);
            return Color.FromArgb(a, r, g, b);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
