using MapRotationTracker.Core;
using MapRotationTracker.MVVM.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapRotationTracker.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand MapListViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }

        public MapListViewModel MapListVM;
        public HomeViewModel HomeVM;
        public MapInfoViewModel MapInfoVM;
        public SettingsViewModel SettingsVM;


        private MapImage[] _mapImages;
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterMaps(value);
            }
        }

        private string[] _searchResults;

        public string[] SearchResults
        {
            get { return _searchResults; }
            set
            {
                _searchResults = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            MapListVM = new MapListViewModel(this);
            MapInfoVM = new MapInfoViewModel();
            SettingsVM = new SettingsViewModel();

            _searchResults = new string[] { "Map A", "Map B", "Map C", "Map D"};

            _mapImages = JsonConvert.DeserializeObject<MapImage[]>(File.ReadAllText(@"C:\Users\luxoi\source\repos\MapRotationTracker\MapRotationTracker\Images\images.json"));
            CurrentView = MapListVM;

            SettingsViewCommand = new RelayCommand(o =>
            {
                CurrentView = SettingsVM;
            });

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            MapListViewCommand = new RelayCommand(o =>
            {
                CurrentView = MapListVM;
            });
        }

        private void FilterMaps(string filter)
        {
            return;
            if (CurrentView is MapListViewModel)
            {
                var vm = CurrentView as MapListViewModel;

                if (string.IsNullOrEmpty(filter))
                {
                    vm.MapImages = _mapImages;
                    return;
                }

                vm.MapImages = _mapImages.Where(m => m.MapName.ToLower().Contains(filter.ToLower())).ToArray();

            }
        }
    }
}
