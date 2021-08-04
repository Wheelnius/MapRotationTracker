using MapRotationTracker.Core;
using MapRotationTracker.MVVM.Model;
using Newtonsoft.Json;
using System.IO;

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
          
            this.MapImages = JsonConvert.DeserializeObject<MapImage[]>(File.ReadAllText(@"C:\Users\luxoi\source\repos\MapRotationTracker\MapRotationTracker\Images\images.json"));

            //for (int i = 0; i < 50; i++)
            //{
            //    MapImages[i] = new MapImage()
            //    {
            //        ImagePath = @"/MapRotationTracker;component/Images/default_map.png",
            //        MapName = "None"
            //    };
            //}

            //MapImages[0] = new MapImage()
            //{
            //    ImagePath = @"/MapRotationTracker;component/Images/95.png",
            //    MapName = "Ghost Town",
            //    Map = Enums.Map.Ghost_Town
            //};

            //MapImages[1] = new MapImage()
            //{
            //    ImagePath = @"/MapRotationTracker;component/Images/23.png",
            //    MapName = "Westfield",
            //    Map = Enums.Map.Westfield
            //};

            //MapImages[2] = new MapImage()
            //{
            //    ImagePath = @"/MapRotationTracker;component/Images/1.png",
            //    MapName = "Karelia",
            //    Map = Enums.Map.Karelia
            //};

            //MapImages[3] = new MapImage()
            //{
            //    ImagePath = @"/MapRotationTracker;component/Images/29.png",
            //    MapName = "El Halluf",
            //    Map = Enums.Map.El_Halluf
            //};

        }
    }
}
