using FishySkypeParser.DataModels;
using System;
using System.Windows.Data;

namespace FishySkypeParser.Converters
{
    public class MessageFromToGridColumnConverter : IValueConverter
    {
        public static MainDataModel dataM { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value.ToString() == dataM.Myself) ? 2 : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}