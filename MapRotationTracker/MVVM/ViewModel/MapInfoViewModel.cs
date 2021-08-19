using MapRotationTracker.Core;
using MapRotationTracker.MVVM.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapRotationTracker.MVVM.ViewModel
{
    class MapInfoViewModel : ObservableObject
    {
        private Map _currentMap;

        public Map CurrentMap
        {
            get { return _currentMap; }
            set { _currentMap = value;
                OnPropertyChanged();
            }
        }

        private TankStatistic[] _tanks;

        public TankStatistic[] Tanks
        {
            get { return _tanks; }
            set { _tanks = value;
                OnPropertyChanged();
            }
        }

        public MapInfoViewModel()
        {
            var tanks = JsonConvert.DeserializeObject<Tank[]>(Encoding.UTF8.GetString(Properties.Resources.Tanks));
            Random r = new Random();

            Tanks = tanks.Select(m => new TankStatistic()
            {
                TimesPlayed = r.Next(0,100),
                Winrate = (((double)r.Next(4000, 6000))/100).ToString() + '%',
                Tank = new Tank()
                {
                    Nation = m.Nation,
                    CodeName = m.CodeName,
                    FileName = m.FileName,
                    Name = m.Name,
                    Path = @"/MapRotationTracker;component/Resources/" + m.FileName
                }

            }).OrderByDescending(t => t.TimesPlayed).Take(10).ToArray();

        }
    }
}
