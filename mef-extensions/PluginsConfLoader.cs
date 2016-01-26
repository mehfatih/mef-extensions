using System;
using System.IO;
using mef_extensions.CustomEvents;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace mef_extensions
{
    public class PluginsConfLoader
    {
        #region Events

        public event PluginsConfLoadedEventHandler Loaded;

        protected virtual void OnPluginsLoaded(PluginsConfLoadedEventArgs args)
        {
            var handler = Loaded;
            if (handler != null) handler(this, args);
        }

        #endregion

        private readonly string _pluginsYmlFile;

        public PluginsConfLoader() : this("plugins.yml")
        {
        }

        public PluginsConfLoader(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("PluginsConf yaml file not founded", _pluginsYmlFile);
            }

            _pluginsYmlFile = filePath;
        }

        public void Load()
        {
            LoadPlugins();

            WatchPluginsFile();
        }

        private void WatchPluginsFile()
        {
            string directoryPath = Path.GetDirectoryName(_pluginsYmlFile);
            string fileName = Path.GetFileName(_pluginsYmlFile);

            FileSystemWatcher watcher = new FileSystemWatcher
            {
                Path = directoryPath,
                //Filter = ".yml",
                IncludeSubdirectories = false,
            };

            watcher.Changed += (sender, args) =>
            {
                if (args.ChangeType == WatcherChangeTypes.Changed)
                {
                    LoadPlugins();
                }
                else
                {
                    throw new NotSupportedException(string.Format("{0} file change type not supported.",args.ChangeType));
                }
            };

            watcher.EnableRaisingEvents = true;
        }

        private void LoadPlugins()
        {
            string text = File.ReadAllText(_pluginsYmlFile);

            Deserializer deserializer = new Deserializer(namingConvention: new CamelCaseNamingConvention());

            PluginsConf pluginsConf = deserializer.Deserialize<PluginsConf>(new StringReader(text));

            OnPluginsLoaded(new PluginsConfLoadedEventArgs(pluginsConf));
        }

    }
}