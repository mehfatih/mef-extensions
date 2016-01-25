using System;
using System.Collections.Generic;
using System.IO;
using mef_extensions.CustomEvents;

namespace mef_extensions
{
    public class Properties
    {
        #region Events

        public event PropertiesChangedEventHandler PropertiesChanged;
        protected virtual void OnPropertiesChanged(PropertiesChangedEventArgs args)
        {
            var handler = PropertiesChanged;
            if (handler != null) handler(this, args);
        }

        #endregion

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

            string directoryPath = Path.GetDirectoryName(_filePath);
            string fileName = Path.GetFileName(_filePath);

            FileSystemWatcher watcher = new FileSystemWatcher
            {
                Path = directoryPath,
                Filter = fileName,
                IncludeSubdirectories = false,
            };

            watcher.Changed += WatcherOnChanged;
            watcher.EnableRaisingEvents = true;
        }

        private void WatcherOnChanged(object sender, FileSystemEventArgs eventArgs)
        {
            if (eventArgs.ChangeType == WatcherChangeTypes.Changed)
            {
                ReloadProperties();
                OnPropertiesChanged(new PropertiesChangedEventArgs());
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