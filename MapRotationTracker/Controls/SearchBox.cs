using MapRotationTracker.Core;
using MapRotationTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MapRotationTracker.Controls
{
    public class SearchBox : TextBox
    {
        public static readonly DependencyProperty ButtonCommandProperty = DependencyProperty.Register("ButtonCommand", typeof(ICommand), typeof(SearchBox));
        public static readonly DependencyProperty SearchResultsProperty = DependencyProperty.Register("SearchResults", typeof(Map[]), typeof(SearchBox), new PropertyMetadata(Array.Empty<Map>()));
        public static readonly DependencyProperty ResultCountProperty = DependencyProperty.Register("ResultCount", typeof(int), typeof(SearchBox), new PropertyMetadata(5));

        public int ResultCount
        {
            get => (int)GetValue(ResultCountProperty);
            set => SetValue(ResultCountProperty, value);
        }

        public Map[] SearchResults
        {
            get => (Map[])GetValue(SearchResultsProperty);
            set => SetValue(SearchResultsProperty, value);
        }

        public ICommand ButtonCommand
        {
            get => (ICommand)GetValue(ButtonCommandProperty);
            set => SetValue(ButtonCommandProperty, value);
        }
        public SearchBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchBox), new FrameworkPropertyMetadata(typeof(SearchBox)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        public static Map GetDefaultMap()
        {
            return Cache.MapById[0].FirstOrDefault();
        }
    }
}
