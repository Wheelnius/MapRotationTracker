using MapRotationTracker.Extensions;
using MapRotationTracker.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MapRotationTracker.MVVM.Model
{
    public class ReplayManagerService
    {
        private Timer _timer;
        private readonly FilterViewModel _filterViewModel;
        private readonly ToastrFactory _toastrFactory;
        private readonly DataProvider _dataProvider;

        public ReplayManager ReplayManager { get; }
        public bool IsFinished { get; private set; } = true;

        internal ReplayManagerService(ToastrFactory toastrFactory, DataProvider dataProvider, FilterViewModel filterViewModel)
        {
            _dataProvider = dataProvider;
            ReplayManager = new ReplayManager();
            _toastrFactory = toastrFactory;
            _filterViewModel = filterViewModel;
        }

        public void Start()
        {
            _timer = new Timer(Callback, null, 5000, 10000);
        }

        public void Stop()
        {
            _timer.Dispose();
        }

        public async Task ForceExecAsync()
        {
            if (IsFinished)
            {
                await Task.Run(() => Callback(null));
            }
        }

        public void ForceExec()
        {
            if (IsFinished)
            {
                _ = Task.Run(() => Callback(null));
            }
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

            _dataProvider.MapStatistics = mapStats;
            TriggerDataUpdate();

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

        public void TriggerDataUpdate()
        {
            if (_dataProvider.MapStatistics is null)
            {
                ForceExec();
                return;
            }

            _dataProvider.UpdateViewModelData(_filterViewModel.LocalSetting);
        }
    }
}
