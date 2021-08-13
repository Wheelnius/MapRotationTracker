using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MapRotationTracker.Converters
{
    public class SearchResultsToStackPanelSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return 80;

            if (value is string[] results)
            {
                return results.Length switch
                {
                    1 => 70,
                    2 => 100,
                    3 => 130,
                    4 => 160,
                    5 => 190,
                    _ => 80,
                };
            }

            return 80;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
