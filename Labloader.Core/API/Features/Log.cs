using System;
using System.Reflection;

namespace Labloader.Core.API.Features
{
    public class Log
    {
	    /// <summary>
        /// Shorthand for info log. Includes assembly name for tag.
        /// </summary>
        /// <param name="message">The message/object to be logged.</param>
        public static void Info(object message) => Logger.Log("INFO", Assembly.GetCallingAssembly().GetName().Name, message?.ToString(), ConsoleColor.Gray);
        
        /// <summary>
        /// Shorthand for warn log. Includes assembly name for tag.
        /// </summary>
        /// <param name="message">The message/object to be logged.</param>
        public static void Warn(object message) => Logger.Log("WARN", Assembly.GetCallingAssembly().GetName().Name, message?.ToString(), ConsoleColor.Yellow);
        
        /// <summary>
        /// Shorthand for error log. Includes assembly name for tag.
        /// </summary>
        /// <param name="message">The message/object to be logged.</param>
        public static void Error(object message) => Logger.Log("ERROR", Assembly.GetCallingAssembly().GetName().Name, message?.ToString(), ConsoleColor.Red);
        
        /// <summary>
        /// Shorthand for debug log. Includes assembly name for tag.
        /// </summary>
        /// <param name="message">The message/object to be logged.</param>
        public static void Debug(object message) => Logger.Log("DEBUG", Assembly.GetCallingAssembly().GetName().Name, message?.ToString(), ConsoleColor.Gray);
    }

    internal static class Logger
    {
        internal static void Log(string level, string tag, string message, ConsoleColor color)
		{
			RConServer.SendLog(level, string.Concat(new string[]
			{
				"[",
				tag,
				"] ",
				message
			}), color);
		}
    }
}