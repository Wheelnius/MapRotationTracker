using MapRotationTracker.Controls;
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
        public RelayCommand SearchResultCommand { get; set; }

        public MapListViewModel MapListVM;
        public HomeViewModel HomeVM;
        public MapInfoViewModel MapInfoVM;
        public SettingsViewModel SettingsVM;

        private ReplayManagerService _replayManagerService;
        private Map[] _maps;
        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterMaps(value);
            }
        }

        private Map[] _searchResults;

        public Map[] SearchResults
        {
            get => _searchResults;
            set
            {
                _searchResults = value;
                OnPropertyChanged();
            }
        }

        private Toastr _toastr;

        public Toastr Toastr
        {
            get => _toastr;
            set
            {
                _toastr = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {

            HomeVM = new HomeViewModel();
            MapListVM = new MapListViewModel(this);
            MapInfoVM = new MapInfoViewModel();
            SettingsVM = new SettingsViewModel();


            Toastr = new Toastr(this);

            //_replayManagerService = new ReplayManagerService(this);
            _replayManagerService = new ReplayManagerService(Toastr, this);
            _replayManagerService.Start();

            _maps = Cache.Maps;

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

            SearchResultCommand = new RelayCommand(async o =>
            {
                SearchText = "";
                if ((MapListVM.MapStatistics is null || !MapListVM.MapStatistics.Any()) && _replayManagerService.IsFinished)
                {
                    await Task.Run(() => _replayManagerService.Callback(null));
                }

                var mapStats = MapListVM.MapStatistics.Where(s => s.Map.Id == ((Map)o).Id).FirstOrDefault();
                if (mapStats is null)
                {
                    mapStats = new MapStatistic
                    {
                        Map = (Map)o
                    };
                }

                switch (CurrentView)
                {
                    case MapListViewModel:
                    case MapInfoViewModel:
                        MapInfoVM.CurrentMap = mapStats;
                        CurrentView = MapInfoVM;
                        break;
                    case HomeViewModel:
                        break;
                    default: break;
                }
            });
        }

        private void FilterMaps(string filter)
        {
            if (CurrentView is MapListViewModel or MapInfoViewModel)
            {
                if (string.IsNullOrEmpty(filter))
                {
                    SearchResults = Array.Empty<Map>();
                    return;
                }

                SearchResults = _maps.Where(m => m.Name.ToLower().Contains(filter.ToLower())).ToArray();
            }
        }
    }
}
