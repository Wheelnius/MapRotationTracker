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
    public class SearchResultsToStackPanelSizeConverter : DependencyObject, IValueConverter
    {
        //public static DependencyProperty MaxElementsProperty = DependencyProperty.Register("MaxElements", typeof(int), typeof(SearchResultsToStackPanelSizeConverter), new PropertyMetadata(5));

        //public int MaxElements
        //{
        //    get => (int)GetValue(MaxElementsProperty);
        //    set => SetValue(MaxElementsProperty, value);
        //}

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //SetValue(MaxElementsProperty, parameter);

            if (value is null)
                return 40;

            if (value is Map[] results)
            {
                var maxHeight = GetMaxSize(results.Length);
                var limitedMaxHeight = GetMaxSize(5);


                return maxHeight <= limitedMaxHeight ? maxHeight : limitedMaxHeight;
            }

            return 40;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static int GetMaxSize(int count)
        {
            var maxLen = (count - 1) * 30 + 65;

            return count switch
            {
                0 => 40,
                1 => 65,
                2 => 95,
                3 => 125,
                4 => 155,
                5 => 185,
                _ => maxLen
            };
        }
    }
}
