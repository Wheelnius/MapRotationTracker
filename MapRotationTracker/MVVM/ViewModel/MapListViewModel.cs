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

        private MapStatistic[] _mapStatistics;

        public MapStatistic[] MapStatistics
        {
            get { return _mapStatistics; }
            set
            {
                _mapStatistics = value;
                OnPropertyChanged();
            }
        }

        public MapListViewModel(MainViewModel mainView)
        {
            MapInfoCommand = new RelayCommand(o =>
            {
                mainView.MapInfoVM.CurrentMap = (MapStatistic)o;
                mainView.CurrentView = mainView.MapInfoVM;          
            });
        }
    }
}
