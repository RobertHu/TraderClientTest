using System.IO;
using System;

namespace RoslynCodeSearcher
{
    /// <summary>
    /// Simple logger class that appends messages to a textfile Logfile.txt in the directory of the executable.
    /// </summary>
    public static class Logger
    {
        private static string logfile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logfile.txt");
        private static readonly object _syncObject = new object();

        public static void Log(string message)
        {
            lock (_syncObject)
            {
                File.AppendAllText(logfile, message + Environment.NewLine);
            }
        }

        public static void DeleteLog()
        {
            if (File.Exists(logfile))
            {
                File.Delete(logfile);
            }
        }
    }
}