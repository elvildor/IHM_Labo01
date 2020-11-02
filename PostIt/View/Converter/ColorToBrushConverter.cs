using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using MediaColor = System.Windows.Media.Color;
using DrawingColor = System.Drawing.Color;

namespace PostIt.View.Converter
{
    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush solidColorBrush = new SolidColorBrush(MediaColor.FromArgb(0, 0, 0, 0));
            if (value is DrawingColor color)
            {
                solidColorBrush = new SolidColorBrush(MediaColor.FromArgb(color.A, color.R, color.G, color.B));
            }
            return solidColorBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DrawingColor color = DrawingColor.FromArgb(0,0,0,0);
            if (value is SolidColorBrush solidColorBrush)
            {
                color = DrawingColor.FromArgb(solidColorBrush.Color.A, solidColorBrush.Color.R, solidColorBrush.Color.G, solidColorBrush.Color.B);
            }
            return color;

        }
    }
}
