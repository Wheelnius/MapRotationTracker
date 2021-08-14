using MapRotationTracker.Core;
using MapRotationTracker.MVVM.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapRotationTracker.MVVM.ViewModel
{
    class MapInfoViewModel : ObservableObject
    {
        private Map _currentMap;

        public Map CurrentMap
        {
            get { return _currentMap; }
            set { _currentMap = value;
                OnPropertyChanged();
            }
        }

        private Tank[] _tanks;

        public Tank[] Tanks
        {
            get { return _tanks; }
            set { _tanks = value;
                OnPropertyChanged();
            }
        }

        public MapInfoViewModel()
        {
            var tanks = JsonConvert.DeserializeObject<Tank[]>(Encoding.UTF8.GetString(Properties.Resources.Tanks));
            Tanks = tanks;
        }
    }
}
