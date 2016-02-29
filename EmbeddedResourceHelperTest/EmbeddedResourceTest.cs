using System;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.IO;
using System.Reflection;
using EmbeddedResourceHelper;

namespace EmbeddedResourceHelperTest
{
    [TestClass]
    public class EmbeddedResourceTest
    {
        private Assembly GetLocalAssembly()
        {
            return Assembly.GetExecutingAssembly();

        }

        private Assembly getRemoteAssembly()
        {
            return Assembly.Load("EmbeddedResourceHelper");
        }

        [TestMethod]
        public void GetAsStreamFromCallingAssemblyTest()
        {
            var fileNames = new List<string> { Constants.fileName1NoExtension, Constants.fileName1WithExtension, Constants.fileName1WithExtension.ToLower() };

            foreach (var file in fileNames)
            {
                using (var stream = EmbeddedResource.GetAsStreamFromCallingAssembly(file))
                using (var reader = new StreamReader(stream))
                {
                    var result = reader.ReadToEnd();
                    Assert.IsFalse(string.IsNullOrWhiteSpace(result), string.Format("The resource '{0}' did not contain any information and should have.", file));
                }
            }
        }

        [TestMethod]
        public void GetAsStreamTest()
        {
            //We will test that the file name search works
            //by using various formats of the name to search with
            var fileNames = new List<string> { Constants.fileName2NoExtension, Constants.fileName2WithExtension, Constants.fileName2WithExtension.ToLower() };
            foreach (var file in fileNames)
            {
                using (var stream = EmbeddedResource.GetAsStream(getRemoteAssembly(), file))
                using (var reader = new StreamReader(stream))
                {
                    var result = reader.ReadToEnd();
                    Assert.IsFalse(string.IsNullOrWhiteSpace(result), string.Format("The resource '{0}' did not contain any information and should have.", file));
                }
            }
        }

        [TestMethod]
        public void SaveToDisk_CallingAssembly()
        {
            var fileNames = new List<string>
            {
                Constants.fileName1WithExtension, Constants.fileName1WithExtension.ToLower(),
                Constants.fileName3WithExtension
            };
            RunSaveFileTest(fileNames, GetLocalAssembly());
        }

        [TestMethod]
        public void SaveToDisk_SpecifiedAssembly()
        {
            var fileNames = new List<string> { Constants.fileName2WithExtension, Constants.fileName2WithExtension.ToLower() };
            RunSaveFileTest(fileNames, getRemoteAssembly());
        }

        private void RunSaveFileTest( List<string> fileNames, Assembly assembly)
        {
            var applicationFolder = AppDomain.CurrentDomain.BaseDirectory;
            
            Console.WriteLine("Files will be output to '{0}'", applicationFolder);
            foreach (var file in fileNames)
            {
                var fullFile = EmbeddedResource.SaveToDisk(assembly, file, applicationFolder);
                Assert.IsTrue(File.Exists(fullFile), "The file was not saved to disk {0}", fullFile);
                File.Delete(fullFile);
                Thread.Sleep(2000);
                Assert.IsFalse(File.Exists(fullFile), "The file was not deleted from disk {0}", fullFile);
            }
            
        }

    }
}
