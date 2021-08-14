using MapRotationTracker.Core;
using MapRotationTracker.MVVM.Model;
using Newtonsoft.Json;
using System.Collections;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;

namespace MapRotationTracker.MVVM.ViewModel
{
    internal class MapListViewModel : ObservableObject
    {
        public RelayCommand MapInfoCommand { get; set; }

        private MapImage[] _mapImages;

        public MapImage[] MapImages
        {
            get { return _mapImages; }
            set
            {
                _mapImages = value;
                OnPropertyChanged();
            }
        }

        public MapListViewModel(MainViewModel mainView)
        {
            MapInfoCommand = new RelayCommand(o =>
            {
                mainView.MapInfoVM.CurrentMapImage = (MapImage)o;
                mainView.CurrentView = mainView.MapInfoVM;          
            });

            var maps = JsonConvert.DeserializeObject<Map[]>(Encoding.UTF8.GetString(Properties.Resources.Maps));
            var tanks = JsonConvert.DeserializeObject<Tank[]>(Encoding.UTF8.GetString(Properties.Resources.Tanks));

            this.MapImages = maps.Select(m => new MapImage()
            {

                ImagePath = @"/MapRotationTracker;component/Resources/" + m.IconPath,
                Map = m,
                MapName = m.Name
            }).ToArray();
        }
    }
}
