using MapRotationTracker.Core;
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

        private string _replaysPath;

        public string ReplaysPath
        {
            get { return _replaysPath; }
            set
            {
                _replaysPath = value;
                OnPropertyChanged();
            }
        }

        public SettingsViewModel()
        {
            ReplaysFolderCommand = new RelayCommand(o =>
            {
                SelectReplaysFolder();
            });
        }

        private void SelectReplaysFolder()
        {
            FolderBrowserDialog folderBrowserDialog = new();
            _ = folderBrowserDialog.ShowDialog();

            string replaysPath = folderBrowserDialog.SelectedPath;

            if (replaysPath is null or "")
            {
                Properties.Settings.Default.ReplaysFolder = "";
                Properties.Settings.Default.Save();
            }
            else
            {
                ReplaysPath = replaysPath;
                Properties.Settings.Default.ReplaysFolder = replaysPath;
                Properties.Settings.Default.Save();
            }
        }
    }
}
