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
        private Button _button;
        private Image _image;
        private TextBlock _text;

        public static DependencyProperty MapProperty = DependencyProperty.Register("Map", typeof(MapImage), typeof(MapButton), new PropertyMetadata(GetDefaultMap()));

        public MapImage Map
        {
            get => (MapImage)GetValue(MapProperty);
            set => SetValue(MapProperty, value);
        }

        static MapButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MapButton), new FrameworkPropertyMetadata(typeof(MapButton)));
        }

        public override void OnApplyTemplate()
        {
            //_button = Template.FindName("MainButton", this) as Button;
            //_image = Template.FindName("MapIcon", this) as Image;
            //_text = Template.FindName("MapName", this) as TextBlock;

            //Binding mapImageBinding = new()
            //{
            //    Path = new PropertyPath(nameof(Map)),
            //    Source = this,
            //    Converter = new MapImageToSourceConverter()
            //};

            //Binding textBlockBinding = new()
            //{
            //    Path = new PropertyPath(nameof(Map)),
            //    Source = this,
            //    Converter = new MapImageToNameConverter()
            //};

            //_ = _text.SetBinding(TextBlock.TextProperty, textBlockBinding);
            //_ = _image.SetBinding(Image.SourceProperty, mapImageBinding);

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
