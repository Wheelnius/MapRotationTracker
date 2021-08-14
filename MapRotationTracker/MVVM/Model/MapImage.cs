using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapRotationTracker.MVVM.Model
{
    public class MapImage
    {
        public string ImagePath { get; set; }
        public string MapName { get; set; }
        public Map Map { get; set; }
    }
}
