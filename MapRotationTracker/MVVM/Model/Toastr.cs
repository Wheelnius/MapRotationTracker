using MapRotationTracker.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapRotationTracker.MVVM.Model
{
    public class Toastr
    {
        public string Message { get; set; }
        public bool Visible { get; set; }

        public static Toastr Show(string message)
        {
            return new Toastr() 
            { 
                Message = message,
                Visible = true
            };
        }

        public static Toastr Hide()
        {
            return new Toastr()
            {
                Visible = false
            };
        }
    }
}
