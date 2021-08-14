using MapRotationTracker.Core;
using MapRotationTracker.MVVM.Model;
using Newtonsoft.Json;
using System.Collections;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;

namespace MapRotationTracker.MVVM.ViewModel
{
    internal class MapListViewModel : ObservableObject
    {
        public RelayCommand MapInfoCommand { get; set; }

        private Map[] _maps;

        public Map[] Maps
        {
            get { return _maps; }
            set
            {
                _maps = value;
                OnPropertyChanged();
            }
        }

        public MapListViewModel(MainViewModel mainView)
        {
            MapInfoCommand = new RelayCommand(o =>
            {
                mainView.MapInfoVM.CurrentMap = (Map)o;
                mainView.CurrentView = mainView.MapInfoVM;          
            });

            var maps = JsonConvert.DeserializeObject<Map[]>(Encoding.UTF8.GetString(Properties.Resources.Maps));

            this.Maps = maps.Select(m => new Map()
            {
                CodeName = m.CodeName,
                FileName = m.FileName,
                Id = m.Id,
                Name = m.Name,
                Path = @"/MapRotationTracker;component/Resources/" + m.FileName
            }).ToArray();
        }
    }
}
