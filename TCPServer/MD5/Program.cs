using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace MD5dddd
{
    class Program
    {
        static void Main(string[] args)
        {

            string hash = GetHash();

            Console.WriteLine("Hash: {0}", hash);

            Console.ReadKey();

        }


        public static string GetHash()
        {
            string pathDest = "d:\\FaxAndEmailManager.exe";

            //File.Copy(pathSrc, pathDest, true);

            String md5Result;
            StringBuilder sb = new StringBuilder();
            MD5 md5Hasher = MD5.Create();

            using (FileStream fs = File.OpenRead(pathDest))
            {
                foreach (Byte b in md5Hasher.ComputeHash(fs))
                    sb.Append(b.ToString("x2").ToLower());
            }

            md5Result = sb.ToString();

           // File.Delete(pathDest);

            return md5Result;
        }

    }
}
