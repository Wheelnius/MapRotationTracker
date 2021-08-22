using MapRotationTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapRotationTracker.Extensions
{
    public static class MapExtensions
    {
        public static MapStatistic ToStatistic(this IGrouping<string, Replay> replays)
        {
            var map = Cache.MapByName[replays.First().MapName].FirstOrDefault();
            if (map is null)
                return null;

            return new MapStatistic()
            {
                Map = map,
                TimesPlayed = replays.Count()
            };
        }
    }
}
