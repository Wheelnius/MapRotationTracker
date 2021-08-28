using MapRotationTracker.Core;
using MapRotationTracker.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MapRotationTracker.MVVM.Model
{
    public class Toastr
    {
        public string Message { get; set; }
        public bool Visible { get; set; }

        private static IDictionary<Guid, string> _processes = new Dictionary<Guid, string>();
        private MainViewModel MainViewModel { get; }
        private Timer _timer;

        internal Toastr(MainViewModel mainViewModel)
        {
            this.MainViewModel = mainViewModel;
        }

        public Toastr() { }

        public static Toastr Show(string message, out Guid processGuid)
        {
            processGuid = Guid.NewGuid();
            _processes.Add(processGuid, message);

            return new Toastr() 
            { 
                Message = message,
                Visible = true
            };
        }

        public void Show(string message, int delay, out Guid processGuid)
        {
            Debug.WriteLine("Started..");
            processGuid = Guid.NewGuid();
            _processes.Add(processGuid, message);

            MainViewModel.Toastr = new Toastr()
            {
                Message = message,
                Visible = true
            };

            
            _timer = new Timer(Hide, processGuid, delay, 10000000);
        }

        private void Hide(object state)
        {
            Debug.WriteLine("Finished..");
            _timer.Dispose();
            var guid = (Guid)state;

            _processes.Remove(guid);

            if (_processes.Any())
            {
                MainViewModel.Toastr = new Toastr()
                {
                    Message = _processes.First().Value,
                    Visible = true
                };
            }

            MainViewModel.Toastr = new Toastr()
            {
                Visible = false
            };
        }

        public void Update(Guid existingProcessGuid, string message, int delay)
        {
            if (_processes.ContainsKey(existingProcessGuid))
            {
                _processes[existingProcessGuid] = message;
                _timer.Dispose();

                _timer = new Timer(Hide, existingProcessGuid, 1, delay);

                MainViewModel.Toastr = new Toastr()
                {
                    Message = message,
                    Visible = true
                };
            }
        }

        /// <summary>
        /// Tries to hide toastr if there are no running processes. Otherwise returns next running process.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Toastr Hide(Guid item)
        {
            _processes.Remove(item);

            if (_processes.Any())
            {
                return new Toastr()
                {
                    Message = _processes.First().Value,
                    Visible = true
                };
            }

            return new Toastr()
            {
                Visible = false
            };
        }

        /// <summary>
        /// Removes all processes and hides toastr.
        /// </summary>
        /// <returns></returns>
        public static Toastr Hide()
        {
            _processes.Clear();

            return new Toastr()
            {
                Visible = false
            };
        }

        /// <summary>
        /// Tries to update existing toastr, creates a new one if it doesn't exist
        /// </summary>
        /// <param name="existingProcessGuid"></param>
        /// <param name="message"></param>
        /// <param name="processGuid"></param>
        /// <returns></returns>
        public static Toastr Update(Guid existingProcessGuid, string message, out Guid processGuid)
        {
            if (_processes.ContainsKey(existingProcessGuid))
            {
                processGuid = existingProcessGuid;
                _processes[existingProcessGuid] = message;

                return new Toastr()
                {
                    Message = message,
                    Visible = true
                };
            }
            else
            {
                return Toastr.Show(message, out processGuid);
            }
        }
    }
}
