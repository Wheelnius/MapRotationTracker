using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapRotationTracker.MVVM.Model
{
    public class Replay
    {
        public string FileName { get; set; }
        public string PlayerVehicle { get; set; }
        public string ClientVersionFromXml { get; set; }
        public string ClientVersionFromExe { get; set; }
        public string RegionCode { get; set; }
        public int PlayerID { get; set; }
        public string ServerName { get; set; }
        public string MapDisplayName { get; set; }
        public string DateTime { get; set; }
        public string MapName { get; set; }
        public string GameplayID { get; set; }
        public int BattleType { get; set; }
        public bool HasMods { get; set; }
        public string PlayerName { get; set; }
    }
}
