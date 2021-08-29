using MapRotationTracker.Extensions;
using MapRotationTracker.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace MapRotationTracker.MVVM.Model
{
    public class ReplayManagerService
    {
        private static Timer _timer;
        private readonly MainViewModel _mainViewModel;
        private readonly ToastrFactory _toastrFactory;

        public ReplayManager ReplayManager { get; }
        public bool IsFinished { get; private set; } = true;

        internal ReplayManagerService(ToastrFactory toastrFactory, MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ReplayManager = new ReplayManager();
            _toastrFactory = toastrFactory;
        }

        public void Start()
        {
            _timer = new Timer(Callback, null, 5000, 10000);
        }

        public static void Stop()
        {
            _timer.Dispose();
        }

        public void Callback(object state)
        {
            if (!IsFinished)
            {
                Debug.WriteLine("Job not finished. Waiting...");
                return;
            }

            IsFinished = false;
            _toastrFactory.Show("Loading and caching replay files...", 2000, out Guid process);

            List<Replay> replays = ScanReplays(process);
            MapStatistic[] mapStats = replays
                .GroupBy(r => r.MapName)
                .Select(g => g.ToStatistic())
                .OrderByDescending(s => s.TimesPlayed)
                .ToArray();
            _mainViewModel.MapListVM.MapStatistics = mapStats;
            IsFinished = true;
        }

        private List<Replay> ScanReplays(Guid toastrProcessGuid)
        {
            List<Replay> savedReplays = ReplayManager.LoadRoamingReplays();

            if (ReplayManager.UpdateReplayData(savedReplays))
            {
                _ = ReplayManager.SaveRoamingReplays(savedReplays);
            }
            else
            {
                _toastrFactory.Update(toastrProcessGuid, "Only loaded replays from cache. Check Settings if the path is correct.", 3000);
            }

            return savedReplays;
        }
    }
}
