using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfMvvmTemplate.Converters
{
    public class ValueToGradientBrushConverter : IValueConverter
    {
        public Color StartColor { get; set; } = Colors.Green;
        public Color EndColor { get; set; } = Colors.Red;
        public double Minimum { get; set; } = 0.0;
        public double Maximum { get; set; } = 100.0;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double numericValue = 0.0;
            if(value is double d)
                numericValue = d;
            else if(value is int i)
                numericValue = i;
            else
                return new SolidColorBrush(StartColor);

            double range = Maximum - Minimum;
            double percent = (numericValue - Minimum) / range;
            percent = Math.Min(Math.Max(percent, 0.0), 1.0); // Clamp between 0 and 1

            Color color = InterpolateColor(StartColor, EndColor, percent);
            return new SolidColorBrush(color);
        }

        private Color InterpolateColor(Color start, Color end, double fraction)
        {
            byte a = (byte)(start.A + (end.A - start.A) * fraction);
            byte r = (byte)(start.R + (end.R - start.R) * fraction);
            byte g = (byte)(start.G + (end.G - start.G) * fraction);
            byte b = (byte)(start.B + (end.B - start.B) * fraction);
            return Color.FromArgb(a, r, g, b);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
