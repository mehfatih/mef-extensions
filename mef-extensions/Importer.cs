using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace mef_extensions
{
    public class Importer
    {
        public DirectoryCatalog DirCatalog { get; private set; }

        public void Init()
        {
            AggregateCatalog catalog = new AggregateCatalog();

            //Add all the parts found in all assemblies in
            //the same directory as the executing program
            string executingAsseblyLocation = Assembly.GetExecutingAssembly().Location;
            if (!string.IsNullOrEmpty(executingAsseblyLocation))
            {
                string directoryPath = Path.GetDirectoryName(executingAsseblyLocation);

                if (!string.IsNullOrEmpty(directoryPath))
                {
                    DirCatalog = new DirectoryCatalog(directoryPath);
                    catalog.Catalogs.Add(DirCatalog);
                }
            }

            //Create the CompositionContainer with the parts in the catalog.
            CompositionContainer container = new CompositionContainer(catalog);

            //Fill the imports of this object
            container.ComposeParts(this);
        }
    }
}
