using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace mef_extensions
{
    public class PluginsConf
    {
        [YamlAlias("plugins-dir")]
        public string PluginsDir { get; set; }

        [YamlAlias("plugins")]
        public List<string> PluginsList { get; set; }
    }
}