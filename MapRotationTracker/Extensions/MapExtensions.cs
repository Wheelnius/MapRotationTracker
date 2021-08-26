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
            Map map = Cache.MapByName[replays.First().MapName].FirstOrDefault();
            if (map is null)
                return null;

            return new MapStatistic()
            {
                Map = map,
                TimesPlayed = replays.Count(),
                Replays = replays.ToArray(),
                TankStatistics = replays.ToArray().ToTankStatistics()
            };
        }

        public static Tank[] ToTanks(this MapStatistic mapStatistic)
        {
            if (mapStatistic is null)
                return null;

            return mapStatistic.Replays
                .SelectMany(r => Cache.TankByName[r.PlayerVehicle])
                .ToArray();
        }

        public static TankStatistic[] ToTankStatistics(this Replay[] replays)
        {
            return replays
                .GroupBy(r => r.PlayerVehicle)
                .Select(g => new TankStatistic
                {
                    Tank = Cache.TankByName[g.Key].FirstOrDefault(),
                    TimesPlayed = g.Count()
                })
                .OrderByDescending(t => t.TimesPlayed)
                .ToArray();
        }
    }
}
