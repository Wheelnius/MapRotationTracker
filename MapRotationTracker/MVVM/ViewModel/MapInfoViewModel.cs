using MapRotationTracker.Core;
using MapRotationTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapRotationTracker.MVVM.ViewModel
{
    class MapInfoViewModel : ObservableObject
    {
        private MapImage _currentMapImage;

        public MapImage CurrentMapImage
        {
            get { return _currentMapImage; }
            set { _currentMapImage = value;
                OnPropertyChanged();
            }
        }

        public MapInfoViewModel()
        {

        }
    }
}
