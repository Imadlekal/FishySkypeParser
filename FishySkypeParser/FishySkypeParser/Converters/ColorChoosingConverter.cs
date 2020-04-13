using FishySkypeParser.DataModels;
using FishySkypeParser.DataTypes;
using System;
using System.Windows.Data;
using System.Windows.Media;
using System.Linq;

namespace FishySkypeParser.Converters
{
    public class ColorChoosingConverter : IValueConverter
    {
        private readonly SolidColorBrush[] topColors = new SolidColorBrush[]
        {
            (SolidColorBrush)(new BrushConverter().ConvertFrom("#b4b4b4")),
            (SolidColorBrush)(new BrushConverter().ConvertFrom("#4f9d9d")),
            (SolidColorBrush)(new BrushConverter().ConvertFrom("#bad99b")),
        };

        private readonly SolidColorBrush[] bottomColors = new SolidColorBrush[]
        {
            (SolidColorBrush)(new BrushConverter().ConvertFrom("#c0c0c0")),
            (SolidColorBrush)(new BrushConverter().ConvertFrom("#87c2c2")),
            (SolidColorBrush)(new BrushConverter().ConvertFrom("#d8e9c7")),
        };

        private readonly SolidColorBrush topColorMy = (SolidColorBrush)(new BrushConverter().ConvertFrom("#a8a8d5"));
        private readonly SolidColorBrush bottomColorMy = (SolidColorBrush)(new BrushConverter().ConvertFrom("#c9c9e4"));


        public static MainDataModel dataM { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            FSPMessage msg = value as FSPMessage;

            if (msg == null)
                return null;

            if (msg.from == dataM.Myself)
            {
                if (parameter?.ToString() == "top")
                    return topColorMy;
                else if (parameter?.ToString() == "bottom")
                    return bottomColorMy;
                else
                    return null;
            }
            else
            {
                int index = dataM.MessagesToDisplay.Select(m => m.from)
                    .Distinct()
                    .Where(f => f != dataM.Myself)
                    .TakeWhile(x => x != msg.from).Count() % topColors.Count();

                if (parameter?.ToString() == "top")
                    return topColors[index];
                else if (parameter?.ToString() == "bottom")
                    return bottomColors[index];
                else
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}