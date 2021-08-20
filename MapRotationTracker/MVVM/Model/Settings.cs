using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapRotationTracker.MVVM.Model
{
    internal static class Settings
    {
        internal static string ReplaysPath { get; set; }
        internal static bool StartWithWindows { get; set; }
        internal static int ReplayScanInterval { get; set; } = 20 * 1000;

        static Settings()
        {
            ReplaysPath = Properties.Settings.Default.ReplaysFolder;
        }

        public static string SelectReplaysFolder()
        {
            FolderBrowserDialog folderBrowserDialog = new();
            _ = folderBrowserDialog.ShowDialog();

            string replaysPath = folderBrowserDialog.SelectedPath;

            Properties.Settings.Default.ReplaysFolder = replaysPath;
            Properties.Settings.Default.Save();

            return replaysPath;
        }
    }
}
