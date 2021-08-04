using MapRotationTracker.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static MapRotationTracker.Controls.MapButton;

namespace MapRotationTracker.Converters
{
    public class ButtonSizeToRectWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return 80;

            if (value is ButtonSize size)
            {
                return size switch
                {
                    ButtonSize.Small => 50,
                    ButtonSize.Medium => 100,
                    ButtonSize.Large => 150,
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
