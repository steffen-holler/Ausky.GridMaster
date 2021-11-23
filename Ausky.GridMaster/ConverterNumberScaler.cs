using System;
using System.Windows.Data;
using System.Globalization;

namespace Ausky.GridMaster
{
    internal class ConverterNumberScaler: IValueConverter
    {
        public double Scaler { get; set; } = 1;
        public double Offset { get; set; } = 0;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !double.TryParse(value.ToString(), out double result))
                return null;
            return result * Scaler + Offset; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !double.TryParse(value.ToString(), out double result))
                return null;
            return (result - Offset) / Scaler;
        }
    }
}
