using System;

namespace mef_extensions.CustomEvents
{
    public delegate void PluginsConfLoadedEventHandler(object sender, PluginsConfLoadedEventArgs args);

    public class PluginsConfLoadedEventArgs : EventArgs
    {
        private readonly PluginsConf _pluginsConf;
        public PluginsConf PluginsConf
        {
            get { return _pluginsConf; }
        }

        public PluginsConfLoadedEventArgs(PluginsConf pluginsConf)
        {
            _pluginsConf = pluginsConf;
        }
    }
}