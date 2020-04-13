using FishySkypeParser.DataModels;
using FishySkypeParser.DataTypes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace FishySkypeParser.Converters
{
    public class BuildMessageConverter : IValueConverter
    {
        public static MainDataModel dataM { get; set; }

        //public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        //{
        //    FSPMessage msg = value as FSPMessage;

        //    if (msg == null)
        //        return null;

        //    if (parameter.ToString() == "name")
        //    {
        //        if (msg.from == dataM.Myself)
        //        {
        //            return "Me";
        //        }
        //        else
        //        {
        //            return !string.IsNullOrEmpty(msg.displayName) ? msg.displayName : msg.from ?? "";
        //        }
        //    }
        //    else if (parameter.ToString() == "time")
        //    {
        //        return msg.originalarrivaltime?.ToString("yyyy-MM-dd HH:mm:ss");
        //    }
        //    else if (parameter.ToString() == "content")
        //    {
        //        return msg.content;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            FSPMessage msg = value as FSPMessage;

            if (msg == null)
                return null;

            string textToDisplay = null;

            if (parameter.ToString() == "name")
            {
                if (msg.from == dataM.Myself)
                {
                    textToDisplay = "Me";
                }
                else
                {
                    textToDisplay = !string.IsNullOrEmpty(msg.displayName) ? msg.displayName : msg.from ?? "";
                }
            }
            else if (parameter.ToString() == "time")
            {
                textToDisplay = msg.originalarrivaltime?.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (parameter.ToString() == "content")
            {
                textToDisplay = msg.content;
            }
            else
            {
                textToDisplay = null;
            }

            if (textToDisplay != null)
            {
                var textBlock = new TextBlock();
                textBlock.TextWrapping = TextWrapping.Wrap;
                textBlock.Padding = new Thickness(2);

                if (string.IsNullOrWhiteSpace(dataM.SearchString))
                {
                    textBlock.Text = textToDisplay;
                }
                else
                {
                    var parts = SplitAndKeep(textToDisplay, dataM.SearchString);

                    // https://stackoverflow.com/questions/3728584/how-to-display-search-results-in-a-wpf-items-control-with-highlighted-query-terms

                    foreach (var part in parts)
                    {
                        if (0 == string.Compare(part, dataM.SearchString, true))
                        {
                            textBlock.Inlines.Add(new Run(part)
                            {
                                FontWeight = FontWeights.Bold,
                                Background = Brushes.Yellow
                            });

                        }
                        else
                        {
                            textBlock.Inlines.Add(new Run(part));
                        }
                    }
                }

                return textBlock;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<string> SplitAndKeep(string s, string delim)
        {
            int delimlen = delim.Length;
            string wk = s;
            List<string> list = new List<string>();
            while (true)
            {
                int index = wk.IndexOf(delim, 0, StringComparison.InvariantCultureIgnoreCase);
                if (index == -1)
                {
                    if (wk.Length > 0)
                    {
                        list.Add(wk);
                    }
                    break;
                }

                if (index > 0)
                {
                    list.Add(wk.Substring(0, index));
                }
                list.Add(delim);
                wk = wk.Substring(index + delimlen);
            }

            return list;
        }
    }
}