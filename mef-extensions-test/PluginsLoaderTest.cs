using System.IO;
using System.Text;
using System.Threading;
using mef_extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mef_extensions_test
{
    [TestClass]
    public class PluginsLoaderTest
    {
        private string _filePath;

        [TestInitialize]
        public void Initialize()
        {
            StringBuilder fileContent = new StringBuilder();
            fileContent.AppendLine("plugins-dir : .");
            fileContent.AppendLine("plugins:");
            fileContent.AppendLine(" - a.dll");
            fileContent.AppendLine(" - b.dll");
            
            _filePath = Path.GetTempFileName();
            File.WriteAllText(_filePath, fileContent.ToString());
        }

        [TestMethod]
        public void LoadPlugins_With_Valid_Data()
        {
            PluginsLoader pluginsLoader = new PluginsLoader(_filePath);

            pluginsLoader.PluginsConfLoaded += (sender, args) =>
            {
                Assert.AreEqual(args.PluginsConf.PluginsDir, ".");
                Assert.IsNotNull(args.PluginsConf.PluginsList);
                Assert.AreEqual(args.PluginsConf.PluginsList.Count, 2);
                Assert.AreEqual(args.PluginsConf.PluginsList[0], "a.dll");
                Assert.AreEqual(args.PluginsConf.PluginsList[1], "b.dll");
            };

            pluginsLoader.Init();

        }

        [TestCleanup]
        public void CleanUp()
        {
            if (_filePath != null && File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }
    }
}