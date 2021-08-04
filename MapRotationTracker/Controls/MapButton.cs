using MapRotationTracker.Converters;
using MapRotationTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MapRotationTracker.Controls
{
    public class MapButton : Button
    {
        public enum ButtonSize
        {
            Small,
            Medium,
            Large
        }

        public static DependencyProperty MapProperty = DependencyProperty.Register("Map", typeof(MapImage), typeof(MapButton), new PropertyMetadata(GetDefaultMap()));
        public static DependencyProperty SizeProperty = DependencyProperty.Register("Size", typeof(ButtonSize), typeof(MapButton), new PropertyMetadata(ButtonSize.Small));
        public static DependencyProperty IsNameVisibleProperty = DependencyProperty.Register("IsNameVisible", typeof(bool), typeof(MapButton), new PropertyMetadata(false));

        public MapImage Map
        {
            get => (MapImage)GetValue(MapProperty);
            set => SetValue(MapProperty, value);
        }

        public ButtonSize Size
        {
            get => (ButtonSize)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        public bool IsNameVisible
        {
            get => (bool)GetValue(IsNameVisibleProperty);
            set => SetValue(IsNameVisibleProperty, value);
        }

        static MapButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MapButton), new FrameworkPropertyMetadata(typeof(MapButton)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        public static MapImage GetDefaultMap()
        {
            return new MapImage()
            {
                ImagePath = @"/MapRotationTracker;component/Images/default_map.png",
                MapName = "None"
            };
        }

    }
}
