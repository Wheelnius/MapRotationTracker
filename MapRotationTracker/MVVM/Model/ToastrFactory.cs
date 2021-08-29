using MapRotationTracker.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace MapRotationTracker.MVVM.Model
{
    public class ToastrFactory
    {
        private IDictionary<Guid, Notification> _notifications = new Dictionary<Guid, Notification>();
        private MainViewModel MainViewModel { get; }
        private const int _maxTimerDelay = 10000000;

        internal ToastrFactory(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
        }

        /// <summary>
        /// Shows a toastr message. Delay 0 for unlimited.
        /// Returns created notification guid.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="delay"></param>
        /// <param name="notificationGuid"></param>
        public void Show(string message, int delay, out Guid notificationGuid)
        {
            notificationGuid = Guid.NewGuid();

            _notifications.Add(notificationGuid, new Notification
            {
                Message = message,
                Timer = delay is 0 ? null : new Timer(Callback, notificationGuid, delay, _maxTimerDelay + delay)
            });

            MainViewModel.Toastr = new Toastr()
            {
                Message = message,
                Visible = true
            };
        }

        private void Callback(object state)
        {
            try
            {
                Guid guid = (Guid)state;
                Hide(guid);
            }
            catch (InvalidCastException e)
            {
                Debug.WriteLine(e);
                Hide();
            }
        }

        /// <summary>
        /// Deletes a toastr notification by guid.
        /// </summary>
        /// <param name="guid"></param>
        public void Hide(Guid guid)
        {
            if (_notifications.ContainsKey(guid))
            {
                _notifications[guid].Stop();
                _ = _notifications.Remove(guid);
            }

            if (_notifications.Any())
            {
                MainViewModel.Toastr = new Toastr()
                {
                    Message = _notifications.First().Value.Message,
                    Visible = true
                };
                return;
            }

            MainViewModel.Toastr = Toastr.GetHiddenToastr();
        }

        /// <summary>
        /// Updates a toastr notification message and show time. Delay 0 for unlimited.
        /// </summary>
        /// <param name="existingNotificationGuid"></param>
        /// <param name="message"></param>
        /// <param name="delay"></param>
        public void Update(Guid existingNotificationGuid, string message, int delay)
        {
            if (_notifications.ContainsKey(existingNotificationGuid))
            {
                Notification notification = _notifications[existingNotificationGuid];
                notification.Message = message;
                notification.Stop();

                if (delay is not 0)
                {
                    notification.Timer = new Timer(Callback, existingNotificationGuid, delay, _maxTimerDelay + delay);
                }

                MainViewModel.Toastr = new Toastr()
                {
                    Message = message,
                    Visible = true
                };
            }
        }

        /// <summary>
        /// Removes all notifications and hides toastr.
        /// </summary>
        /// <returns></returns>
        public void Hide()
        {
            foreach (var notification in _notifications)
            {
                notification.Value.Stop();
            }
            _notifications = new Dictionary<Guid, Notification>();
            MainViewModel.Toastr = Toastr.GetHiddenToastr();
        }

        private class Notification
        {
            public string Message { get; set; }
            public Timer Timer { get; set; }

            public void Stop()
            {
                if (Timer is not null)
                {
                    Timer.Dispose();
                }
            }
        }
    }
}
