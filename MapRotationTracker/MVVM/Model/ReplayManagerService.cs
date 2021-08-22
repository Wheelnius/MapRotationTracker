using MapRotationTracker.Extensions;
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
        private static Timer _timer;
        private bool _isFinished = true;

        private readonly MainViewModel _mainViewModel;
        private MainViewModel MainViewModel { get => _mainViewModel; }

        private readonly ReplayManager _replayManager;
        public ReplayManager ReplayManager { get => _replayManager; }

        private static Replay[] Replays { get; set; }

        internal ReplayManagerService(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _replayManager = new ReplayManager();
        }

        public void Start()
        {
            _timer = new Timer(Callback, null, 5000, 10000);
        }

        public void Stop()
        {
            _timer.Dispose();
        }

        private void Callback(object state)
        {
            if (!_isFinished)
            {
                Debug.WriteLine("Job not finished. Waiting...");
                return;
            }

            _isFinished = false;
            MainViewModel.Toastr = Toastr.Show("Loading and caching replay files...", out Guid process);

            var replays = ScanReplays(process);
            var mapStats = replays
                .GroupBy(r => r.MapName)
                .Select(g => g.ToStatistic())
                .OrderByDescending(s => s.TimesPlayed)
                .ToArray();
            MainViewModel.MapListVM.MapStatistics = mapStats;

            MainViewModel.Toastr = Toastr.Hide(process);
            _isFinished = true;
        }

        private List<Replay> ScanReplays(Guid toastrProcessGuid)
        {
            List<Replay> savedReplays = ReplayManager.LoadRoamingReplays();

            if (ReplayManager.UpdateReplayData(savedReplays))
            {
                _ = ReplayManager.SaveRoamingReplays(savedReplays);
                Thread.Sleep(1000);
            }
            else
            {
                MainViewModel.Toastr = Toastr.Update(toastrProcessGuid, "Failed to update replays. Check Settings if the path is correct.", out _);
                Thread.Sleep(3000);
            }

            return savedReplays;
        }
    }
}
