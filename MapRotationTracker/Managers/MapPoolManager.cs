using MapRotationTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapRotationTracker.Managers
{
    public static class MapPoolManager
    {
        public static MapImage[] _mapImages;

        public static MapImage[] MapImages
        {
            get {
                if (_mapImages == null)
                {

                }
                return _mapImages;
            }
        }

        
    }
}
