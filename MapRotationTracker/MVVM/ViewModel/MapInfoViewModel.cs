using MapRotationTracker.Core;
using MapRotationTracker.Extensions;
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
        private MapStatistic _currentMap;

        public MapStatistic CurrentMap
        {
            get => _currentMap;
            set { _currentMap = value;
                OnPropertyChanged();
            }
        }

        public MapInfoViewModel()
        {

        }
    }
}
