using MapRotationTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MapRotationTracker.Converters
{
    public class SearchResultsToClipRectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is null)
                return new Rect(0d, 0d, 200d, 40d);

            if (value is Map[] results)
            {
                var maxHeight = GetMaxSize(results.Length);
                var limitedMaxHeight = GetMaxSize(5);
                
                return new Rect(0d, 0d, 200d, maxHeight <= limitedMaxHeight ? maxHeight : limitedMaxHeight);
            }

            return new Rect(0d, 0d, 200d, 40d);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static int GetMaxSize(int count)
        {
            var maxLen = (count - 1) * 30 + 60;

            return count switch
            {
                0 => 40,
                1 => 64,
                2 => 94,
                3 => 124,
                4 => 154,
                5 => 184,
                _ => maxLen
            };
        }
    }
}
