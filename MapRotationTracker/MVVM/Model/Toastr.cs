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

        /// <summary>
        /// Set to 0 if unlimited / waiting for update.
        /// </summary>
        public int Duration { get; set; }
    }
}
