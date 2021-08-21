using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MapRotationTracker.MVVM.Model
{
    public class ReplayManager
    {
        private static readonly Regex RReplayName = new(@"^[0-9]{8}_[0-9]{4}_.*?(\.wotreplay)$", RegexOptions.Compiled);

        private static readonly string TempDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Rota Tracker", "temp");

        private static readonly string TempFileName = "temp.json";

        public static List<Replay> LoadRoamingReplays()
        {
            _ = Directory.CreateDirectory(TempDirectory);
            string[] files = Directory.GetFiles(TempDirectory);

            string tempFile = files.FirstOrDefault(f => Path.GetFileName(f) == TempFileName);

            if (tempFile is null)
            {
                return new List<Replay>();
            }

            try
            {
                return JsonConvert.DeserializeObject<List<Replay>>(File.ReadAllText(tempFile));
            }
            catch (Exception e)
            {
                // log pls
                return new List<Replay>();
            }
        }

        public static void SaveRoamingReplays(List<Replay> currentReplays)
        {
            _ = Directory.CreateDirectory(TempDirectory);
            string json = JsonConvert.SerializeObject(currentReplays);
            string filePath = Path.Combine(TempDirectory, TempFileName);
            File.WriteAllText(filePath, json);
        }

        /// <summary>
        /// Requires Settings.ReplayPath validation
        /// </summary>
        /// <param name="loadedReplays"></param>
        /// <returns></returns>
        public static void UpdateReplayData(List<Replay> currentReplays)
        {
            ILookup<string, Replay> loadedReplays = currentReplays.ToLookup(c => c.FileName);
            List<string> missingReplays = new List<string>();

            IEnumerable<string> replayNames = Directory
                .GetFiles(Settings.ReplaysPath)
                .Select(r => Path.GetFileName(r))
                .Where(r => RReplayName.IsMatch(r));

            foreach (string replayName in replayNames)
            {
                if (!loadedReplays[replayName].Any())
                {
                    missingReplays.Add(Path.Combine(Settings.ReplaysPath, replayName));
                }
            }

            foreach (string missingReplayPath in missingReplays)
            {
                try
                {
                    byte[] fileBytes = File.ReadAllBytes(missingReplayPath);
                    int blockSize = BitConverter.ToInt32(fileBytes, 8);

                    byte[] js = new byte[blockSize];
                    Array.Copy(fileBytes, 12, js, 0, blockSize);
                    string result = Encoding.UTF8.GetString(js);

                    Replay info = JsonConvert.DeserializeObject<Replay>(result);
                    info.FileName = Path.GetFileName(missingReplayPath);
                    currentReplays.Add(info);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }
    }
}
