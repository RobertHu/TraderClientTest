using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Xml;

namespace DJServer
{
    class Program
    {
        static void Main(string[] args)
        {
            DJNewsServer.Default.Initialize(20887, 20888, 20889);
            DJNewsServer.Default.Start();
            System.Console.Read();
        }

      

        //private static int ConvertLittleEndian(byte[] array)
        //{
        //    int pos = 0;
        //    int result = 0;
        //    foreach (byte by in array)
        //    {
        //        result |= (int)(by << pos);
        //        pos += 8;
        //    }
        //    return result;
        //}

        //static void ParseXml(string input)
        //{
        //    XmlDocument doc = new XmlDocument();
        //    doc.LoadXml(input);
        //    XmlElement root = doc.DocumentElement;
        //    string djStory = GetElementContent(root, "/root/DJStory");
        //    string id = GetElementContent(root, "/root/uniqueid");
        //    string storyDate = GetElementContent(root, "/root/StoryDate");
        //    string storyTime = GetElementContent(root, "/root/StoryTime");
        //    string title = GetElementContent(root, "/root/title");
        //    string content = GetElementContent(root, "/root/fulltext");
        //}

        //static string GetElementContent(XmlElement root, string path)
        //{
        //    XmlNode node = root.SelectSingleNode(path);
        //    return node.InnerText;
        //}

        //static void Log(string content,bool newLine = false)
        //{
        //    using (FileStream fs = new FileStream("log.txt", FileMode.Append, FileAccess.Write))
        //    {
        //        StreamWriter sw = new StreamWriter(fs);
        //        if (newLine)
        //        {
        //            sw.WriteLine();
        //            sw.WriteLine(content);
        //            sw.WriteLine();
        //        }
        //        else
        //        {
        //            sw.Write(string.Format("{0} ",content));
        //        }
                
        //        sw.Flush();
        //        sw.Close();
        //    }
        //}

    }

   
  
}
