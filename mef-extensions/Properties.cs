using System;
using System.Collections.Generic;
using System.IO;

namespace mef_extensions
{
    public class Properties
    {
        private readonly Dictionary<string, string> _propDic;
        private readonly string _filePath;

        public Properties(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath");
            }

            _filePath = filePath;
            _propDic = new Dictionary<string, string>();
            
            ReloadProperties();

            FileSystemWatcher watcher = new FileSystemWatcher
            {
                Path = Path.GetDirectoryName(_filePath),
                //Filter = _filePath
            };

            watcher.Changed += WatcherOnChanged;
            watcher.EnableRaisingEvents = true;
        }

        private void WatcherOnChanged(object sender, FileSystemEventArgs eventArgs)
        {
            if (eventArgs.ChangeType == WatcherChangeTypes.Changed)
            {
                ReloadProperties();
            }
        }

        public string GetProp(string key)
        {
            string result = null;
            if (_propDic.ContainsKey(key))
            {
                result = _propDic[key];
            }

            return result;
        }

        private void ReloadProperties()
        {
            string[] readAllLines = File.ReadAllLines(_filePath);
            foreach (string line in readAllLines)
            {
                string[] tokens = line.Split('=');
                _propDic[tokens[0].Trim()] = tokens[1].Trim();
            }
        }
    }
}