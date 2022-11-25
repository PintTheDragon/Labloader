using System.IO;
using UnityEngine.Device;

namespace Labloader.API.Features
{
    /// <summary>
    /// Contains data about different file paths.
    /// </summary>
    public static class Paths
    {
        /// <summary>
        /// The main data directory.
        /// </summary>
        public static string Main = Path.Combine(Application.dataPath, "../Labloader");

        /// <summary>
        /// The directory where plugin DLLs are stored.
        /// </summary>
        public static string Plugins = Path.Combine(Main, "Plugins");

        /// <summary>
        /// The directory where plugin config files are stored.
        /// </summary>
        public static string Config = Path.Combine(Main, "Config");
    }
}