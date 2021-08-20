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
    public class ToastrNotification : Control
    {
        public static readonly DependencyProperty ToastrProperty = DependencyProperty.Register("Toastr", typeof(Toastr), typeof(ToastrNotification));

        public Toastr ResultCount
        {
            get => (Toastr)GetValue(ToastrProperty);
            set => SetValue(ToastrProperty, value);
        }

        public ToastrNotification()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToastrNotification), new FrameworkPropertyMetadata(typeof(ToastrNotification)));
        }
    }
}
