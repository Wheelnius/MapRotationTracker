using MapRotationTracker.Core;
using MapRotationTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapRotationTracker.MVVM.ViewModel
{
    internal class SettingsViewModel : ObservableObject
    {
        public RelayCommand ReplaysFolderCommand { get; set; }

        public string ReplaysPath
        {
            get => Settings.ReplaysPath;
            set
            {
                Settings.ReplaysPath = value;
                OnPropertyChanged();
            }
        }

        public bool StartWithWindows
        {
            get => Settings.StartWithWindows;
            set
            {
                Settings.StartWithWindows = value;
                OnPropertyChanged();
            }
        }

        public SettingsViewModel()
        {
            ReplaysFolderCommand = new RelayCommand(o =>
            {
                ReplaysPath = Settings.SelectReplaysFolder();
            });
        }
    }
}
