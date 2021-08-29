using MapRotationTracker.Extensions;
using MapRotationTracker.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapRotationTracker.MVVM.Model
{
    internal class DataProvider
    {
        public MapListViewModel MapListViewModel { get; }
        public MapInfoViewModel MapInfoViewModel { get; }

        public MapStatistic[] MapStatistics { get; set; }
        public int CurrentFilter { get; set; } = 0;

        public DataProvider(MapListViewModel mapListViewModel, MapInfoViewModel mapInfoViewModel)
        {
            MapInfoViewModel = mapInfoViewModel;
            MapListViewModel = mapListViewModel;
        }

        public void UpdateViewModelData(string filter)
        {
            switch (filter)
            {
                default:
                case "All":
                    MapListViewModel.MapStatistics = MapStatistics;
                    CurrentFilter = 0;
                    break;
                case "30":
                    MapListViewModel.MapStatistics = MapStatistics.FilterByAge(30, CultureInfo.GetCultureInfo("de-DE"));
                    CurrentFilter = 30;
                    break;
                case "7":
                    MapListViewModel.MapStatistics = MapStatistics.FilterByAge(7, CultureInfo.GetCultureInfo("de-DE"));
                    CurrentFilter = 7;
                    break;
                case "1":
                    MapListViewModel.MapStatistics = MapStatistics.FilterByAge(1, CultureInfo.GetCultureInfo("de-DE"));
                    CurrentFilter = 1;
                    break;
            }

            MapInfoViewModel.CurrentMap = GetMapStatistics(MapInfoViewModel.CurrentMap?.Map);
        }

        public MapStatistic GetMapStatistics(Map map)
        {
            if (map is null)
                return null;

            if (CurrentFilter is 0)
            {
                return MapStatistics
                    .Where(s => s.Map.Id == map.Id)
                    .FirstOrDefault() ?? new MapStatistic
                    {
                        Map = map
                    };
            }

            return MapStatistics.FilterByAge(CurrentFilter, CultureInfo.GetCultureInfo("de-DE"))
                .Where(s => s.Map.Id == map.Id)
                .FirstOrDefault() ?? new MapStatistic
                {
                    Map = map
                };

        }
    }
}
