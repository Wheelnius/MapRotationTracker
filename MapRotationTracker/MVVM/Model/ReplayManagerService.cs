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

            MainViewModel.Toastr = Toastr.Show("Loading and caching replay files...", out Guid process);
            var savedReplays = ReplayManager.LoadRoamingReplays();
            ReplayManager.UpdateReplayData(savedReplays);
            ReplayManager.SaveRoamingReplays(savedReplays);
            Thread.Sleep(1000);
            MainViewModel.Toastr = Toastr.Hide(process);
        }
    }
}
