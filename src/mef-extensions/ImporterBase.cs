using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;

namespace mef_extensions
{
    public abstract class ImporterBase
    {
        public CompositionContainer Container { get; private set; }

        public void Init(string pluginsYmlFilePath)
        {
            Init(new PluginsConfLoader(pluginsYmlFilePath));
        }

        public void Init()
        {
            Init(new PluginsConfLoader());
        }

        private void Init(PluginsConfLoader pluginsConfLoader)
        {
            pluginsConfLoader.Loaded += (sender, args) =>
            {
                if (Container != null)
                {
                    Container.Dispose();
                }

                PluginsConf pluginsConf = args.PluginsConf;

                AggregateCatalog catalog = new AggregateCatalog();

                foreach (string plugins in pluginsConf.PluginsList)
                {
                    string path = Path.Combine(pluginsConf.PluginsDir, plugins);
                    Assembly assembly = Assembly.LoadFile(path);
                    catalog.Catalogs.Add(new AssemblyCatalog(assembly));
                }

                //Create the CompositionContainer with the parts in the catalog.
                Container = new CompositionContainer(catalog);

                //Fill the imports of this object
                Container.ComposeParts(GetComposedParts());
            };

            pluginsConfLoader.Load();
        }

        public virtual object[] GetComposedParts()
        {
            return new object[]{this};
        }
    }
}
