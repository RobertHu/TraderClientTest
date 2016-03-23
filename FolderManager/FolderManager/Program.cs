using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
namespace FolderManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Explore(args[0]);
        }
        private static void Explore(string dir)
        {
            try
            {
                Process.Start("explorer.exe", dir);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
