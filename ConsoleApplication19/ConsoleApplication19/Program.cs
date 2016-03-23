using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApplication19
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir = @"D:\Work\experiment";
            long expected = 72906800;
            DirectoryInfo di = new DirectoryInfo(dir);
            DelDirectoriesIfExceedSize(di, expected);
            string dir2 = @"D:\Work\experiment\hu\";
            if (!Directory.Exists(dir2))
            {
                Directory.CreateDirectory(dir2);
            }
            string file = Path.Combine(dir2, "my.txt");
            using (var fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            using (var sw = new StreamWriter(fs))
            {
                foreach (var line in GetLines())
                {
                    sw.WriteLine(line);
                }
            }
            
            Console.ReadKey();
        }

        static IEnumerable<string> GetLines()
        {
            yield return "line1";
            yield return "line2";
        }

         public static void DelSubDirectories(DirectoryInfo parent)
        {
            foreach (DirectoryInfo di in parent.GetDirectories())
            {
                di.Delete(true);
            }
        }

        public static void DelDirectoriesIfExceedSize(DirectoryInfo di, long size)
        {
            long currentSize = GetDirectorySize(di);
            //_Log.InfoFormat("DelDirectoriesIfExceedSize  CurrentSize:{0}", currentSize);
            if (currentSize >= size)
            {
               DelSubDirectories(di);
            }
        }

        public static long GetDirectorySize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += GetDirectorySize(di);
            }
            return size;
        }
        
    }
}
