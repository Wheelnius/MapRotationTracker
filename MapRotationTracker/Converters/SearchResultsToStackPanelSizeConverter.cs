using MapRotationTracker.MVVM.Model;
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
                return 40;

            if (value is Map[] results)
            {
                return results.Length switch
                {
                    0 => 40,
                    1 => 75,
                    2 => 105,
                    3 => 135,
                    4 => 165,
                    5 => 195,
                    _ => 195
                };
            }

            return 40;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
