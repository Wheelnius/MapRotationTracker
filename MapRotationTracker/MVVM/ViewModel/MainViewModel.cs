﻿using MapRotationTracker.Controls;
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


        private Map[] _maps;
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

        private Map[] _searchResults;

        public Map[] SearchResults
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

            var maps = JsonConvert.DeserializeObject<Map[]>(Encoding.UTF8.GetString(Properties.Resources.Maps));
            _maps = maps.Select(m => new Map()
            {
                CodeName = m.CodeName,
                FileName = m.FileName,
                Id = m.Id,
                Name = m.Name,
                Path = @"/MapRotationTracker;component/Resources/" + m.FileName
            }).ToArray();

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

            SearchResultCommand = new RelayCommand(o =>
            {
                SearchText = "";

                switch (CurrentView)
                {
                    case MapListViewModel:
                    case MapInfoViewModel:
                        MapInfoVM.CurrentMap = (Map)o;
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
