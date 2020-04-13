using System;
using System.Windows.Data;

namespace FishySkypeParser.Converters
{
    public class ReduceWidthByConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            try
            {
                return System.Convert.ToDouble(value) - System.Convert.ToDouble(parameter);
            }
            catch
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}