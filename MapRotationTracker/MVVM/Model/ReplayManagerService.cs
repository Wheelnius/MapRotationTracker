using MapRotationTracker.MVVM.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MapRotationTracker.MVVM.Model
{
    public class ReplayManagerService
    {
        private static readonly Regex RReplayName = new Regex(@"\.wotreplay$", RegexOptions.Compiled);

        private MapListViewModel _mapListViewModel;
        private MapInfoViewModel _mapInfoViewModel;

        private static Replay[] Replays { get; set; }

        private bool _isRunning;

        internal ReplayManagerService(MapListViewModel mapListViewModel, MapInfoViewModel mapInfoViewModel)
        {
            _mapListViewModel = mapListViewModel;
            _mapInfoViewModel = mapInfoViewModel;
        }

        public void Start()
        {
            _isRunning = true;

            try
            {
                _ = Task.Run(() =>
                {
                    while (_isRunning)
                    {
                        ScanReplays();
                        Thread.Sleep(Settings.ReplayScanInterval);
                    }
                });

            }
            catch (Exception e)
            {
                _isRunning = false;
                Debug.WriteLine(e);
            }

        }

        public void Stop()
        {
            _isRunning = false;
        }

        private static void ScanReplays()
        {
            Debug.WriteLine("Scanning....");

            List<Replay> replays = new();

            var replayNames = Directory.GetFiles(Settings.ReplaysPath);
            replayNames = replayNames.Where(r => RReplayName.IsMatch(r)).ToArray();

            foreach (var name in replayNames)
            {
                var fileBytes = File.ReadAllBytes(name);
                var blockSize = BitConverter.ToInt32(fileBytes, 8);
                byte[] js = new byte[blockSize];
                Array.Copy(fileBytes, 12, js, 0, blockSize);
                var jsonString = BitConverter.ToString(fileBytes, 12, blockSize);
                string result = Encoding.UTF8.GetString(js);
                var info = JsonConvert.DeserializeObject<Replay>(result);
                info.FileName = name;
                replays.Add(info);
            }

            string filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "userinfo.txt");

            int? a = null;

            var folder = Environment.SpecialFolder.ApplicationData;


        }
    }
}
