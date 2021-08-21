using MapRotationTracker.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapRotationTracker.MVVM.Model
{
    public class Toastr
    {
        public string Message { get; set; }
        public bool Visible { get; set; }

        private static IDictionary<Guid, string> _processes = new Dictionary<Guid, string>();

        public static Toastr Show(string message, out Guid processGuid)
        {
            processGuid = new Guid();
            _processes.Add(processGuid, message);

            return new Toastr() 
            { 
                Message = message,
                Visible = true
            };
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
    }
}
