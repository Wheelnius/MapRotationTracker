using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapRotationTracker.MVVM.Model
{
    public class Statistic
    {
        public int TimesPlayed { get; set; }
    }

    public class TankStatistic : Statistic
    {
        public Tank Tank { get; set; }
    }

    public class MapStatistic : Statistic
    {
        public Map Map { get; set; }
        public Replay[] Replays { get; set; }
        public TankStatistic[] TankStatistics { get; set; }
    }
}
