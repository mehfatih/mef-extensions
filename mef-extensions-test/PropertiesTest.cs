using System;
using System.IO;
using System.Text;
using mef_extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mef_extensions_test
{
    [TestClass]
    public class PropertiesTest
    {
        private string _filePath;

        [TestInitialize]
        public void Initialize()
        {
            StringBuilder fileContent = new StringBuilder();
            fileContent.AppendLine("prop1=a");
            fileContent.AppendLine("prop2=b");
            _filePath = Path.GetTempFileName();
            File.WriteAllText(_filePath,fileContent.ToString());
        }

        [TestMethod]
        public void GetProp_Test()
        {
            Properties properties = new Properties(_filePath);
            string prop1 = properties.GetProp("prop1");
            string prop2 = properties.GetProp("prop2");

            Assert.AreEqual(prop1,"a");
            Assert.AreEqual(prop2,"b");
        }

        [TestMethod]
        public void AddProp_Test()
        {
            Properties properties = new Properties(_filePath);

            StringBuilder fileContent = new StringBuilder();
            fileContent.AppendLine("prop3=c");
            File.AppendAllText(_filePath,fileContent.ToString());

            string prop3 = properties.GetProp("prop3");

            Assert.AreEqual(prop3,"c");
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
