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
    public class SearchResultsToItemVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return Visibility.Hidden;
            }

            if (parameter == null)
            {
                return Visibility.Hidden;
            }

            if (value is Map[] results)
            {
                int len = results.Length;
                return int.Parse(parameter.ToString()) <= len ? Visibility.Visible : Visibility.Hidden;
            }

            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
