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
        public static Map[] _maps;

        public static Map[] Maps
        {
            get {
                if (_maps == null)
                {

                }
                return _maps;
            }
        }

        
    }
}
