using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Labloader
{
    public static class Loader
    {
        /// <summary>
        /// A list of required dependencies for Labloader to function.
        /// These depenedencies are embedded in the Labloader DLL, and follow
        /// the format Labloader.NAME
        ///
        /// If these DLLs do not exist in the dependencies folder, then they will
        /// be created automatically.
        /// </summary>
        private static readonly string[] RequiredDependencies = new[]
        {
            "0Harmony.dll"
        };

        private static readonly string DependenciesPath = Path.Combine(Application.dataPath, "..", "Labloader", "Plugins", "dependencies");
        
        public static void Main()
        {
            // This just bootstraps Labloader.
            // It does so by loading dependencies,
            // then loading the internally packaged
            // Lablaoder.Core assembly and executing
            // its Labloader.Init method.

            try
            {
                LoadDependencies();
                LoadLabloader();
            }
            catch (Exception e)
            {
                Error("An error occured while bootstrapping Labloader:");
                Error(e);
            }
        }

        private static void LoadLabloader()
        {
            // Just like how required dependencies work, we'll get the Labloader.Core.dll
            // stored internally. However, we'll load this as an Assembly directly (without saving),
            // and will call the Labloader.Init method.

            byte[] coreDLL;
            using(var tempStream = new MemoryStream())
            {
                var resourceStream = typeof(Loader).Assembly.GetManifestResourceStream("Labloader.Labloader.Core.dll");
                if (resourceStream == null)
                {
                    throw new Exception("Could not find Labloader core assembly!");
                }
                
                resourceStream.CopyTo(tempStream);
                coreDLL = tempStream.ToArray();
            }

            var coreAssembly = Assembly.Load(coreDLL);
            var coreInit = coreAssembly.GetType("Labloader.Core.Labloader")?.GetMethod("Init");
            if (coreInit == null)
            {
                throw new Exception("Could not find Labloader Init method!");
            }

            coreInit.Invoke(null, Array.Empty<object>());
        }

        private static void LoadDependencies()
        {
            if (!Directory.Exists(DependenciesPath))
            {
                Directory.CreateDirectory(DependenciesPath);
            }
            
            var tempDeps = Directory.GetFiles(DependenciesPath);

            // We need to ensure our required DLLs exist.
            foreach (var requiredDep in RequiredDependencies.Where(dep => !tempDeps.Contains(Path.Combine(DependenciesPath, dep))))
            {
                // It doesn't exist, so make it.
                using (var fileStream = File.Create(Path.Combine(DependenciesPath, requiredDep)))
                {
                    var data = typeof(Loader).Assembly.GetManifestResourceStream("Labloader." + requiredDep);
                    if(data == null)
                        throw new Exception("Expected to find \"Labloader." + requiredDep + "\", but it does not exist!");
                    
                    data.CopyTo(fileStream);
                }
            }
            
            var deps = Directory.GetFiles(DependenciesPath);
            Info("Loading " + deps.Length + " dependencies.");
            
            for (var i = 0; i < deps.Length; i++)
            {
                LoadDependency(i, deps.Length, deps[i]);
            }
        }

        private static void LoadDependency(int index, int count, string path)
        {
            if (!path.EndsWith(".dll")) return;

            var fileName = Path.GetFileName(path);
            Info(string.Concat(new object[] { 
                "Dependency ",
                fileName,
                " loaded. (",
                (index + 1),
                "/",
                count,
                ")"
            }));
            Assembly.Load(File.ReadAllBytes(path));
        }
        
        public static void Info(object message) => Log("INFO", message.ToString(), ConsoleColor.Gray);
        public static void Error(object message) => Log("ERROR", message.ToString(), ConsoleColor.Red);

        private static void Log(string level, string message, ConsoleColor color)
        {
            RConServer.SendLog(level, string.Concat(new string[]
            {
                "[Labloader] ",
                message
            }), color);
        }
    }
}