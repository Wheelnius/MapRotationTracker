using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapRotationTracker.MVVM.Model
{
    public static class Cache
    {
        private const string _defaultImagePath = @"/MapRotationTracker;component/Resources/";
        private static Map[] _maps;
        private static Tank[] _tanks;

        private static ILookup<int, Map> _mapById;
        private static ILookup<string, Map> _mapByName;

        private static ILookup<string, Tank> _tankByName;

        public static bool Loaded { get; set; }
        public static string DefaultImagePath => _defaultImagePath;

        public static Map[] Maps
        {
            get
            {
                if (!Loaded) LoadCache();
                return _maps;
            }
            set => _maps = value;
        }

        public static ILookup<int, Map> MapById
        {
            get
            {
                if (!Loaded) LoadCache();
                return _mapById;
            }
            set => _mapById = value;
        }

        public static ILookup<string, Map> MapByName
        {
            get
            {
                if (!Loaded) LoadCache();
                return _mapByName;
            }
            set => _mapByName = value;
        }

        public static ILookup<string, Tank> TankByName
        {
            get
            {
                if (!Loaded) LoadCache();
                return _tankByName;
            }
            set => _tankByName = value;
        }

        public static void LoadCache()
        {
            _maps = JsonConvert.DeserializeObject<Map[]>(Encoding.UTF8.GetString(Properties.Resources.Maps));
            _tanks = JsonConvert.DeserializeObject<Tank[]>(Encoding.UTF8.GetString(Properties.Resources.Tanks));

            foreach (var map in _maps)
            {
                map.Path = DefaultImagePath + map.FileName;
            }

            foreach (var tank in _tanks)
            {
                tank.Path = DefaultImagePath + tank.FileName;
            }

            MapById = _maps.ToLookup(m => m.Id);
            MapByName = _maps.ToLookup(m => m.CodeName);

            //TODO should probably just update the json later
            TankByName = _tanks.ToLookup(t => t.FileName.Replace(".png", ""));

            Loaded = true;
        }

    }
}
