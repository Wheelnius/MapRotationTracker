using MapRotationTracker.Core;
using MapRotationTracker.MVVM.Model;

namespace MapRotationTracker.MVVM.ViewModel
{
    class FilterViewModel : ObservableObject
    {
        public RelayCommand AllTimeCommand { get; set; }
        public RelayCommand Days30Command { get; set; }
        public RelayCommand Days7Command { get; set; }
        public RelayCommand Days1Command { get; set; }
        public DataProvider DataProvider { get; set; }

        private string _localSetting;

        public string LocalSetting
        {
            get => _localSetting;
            set
            {
                _localSetting = value;
                OnPropertyChanged();
                DataProvider.UpdateViewModelData(value);
            }
        }

        public FilterViewModel(DataProvider dataProvider)
        {
            this.DataProvider = dataProvider;

            AllTimeCommand = new RelayCommand(o =>
            {
                LocalSetting = "All";
            });

            Days30Command = new RelayCommand(o =>
            {
                LocalSetting = "30";
            });

            Days7Command = new RelayCommand(o =>
            {
                LocalSetting = "7";
            });

            Days1Command = new RelayCommand(o =>
            {
                LocalSetting = "1";
            });
        }
    }
}
