using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            XElement element = new XElement("Order");
            element.SetAttributeValue("Open", "Opened");
            element.SetAttributeValue("InstrumentCode", "AUDUSD");
            element.SetAttributeValue("ExecutedPrice", "85.2*1000");
            element.SetAttributeValue("ExecutedPrice", DateTime.Now.ToShortDateString());
            Console.WriteLine(element.ToString());
            CreateBmpFromXml("my.bmp");
            Console.Read();
        }

        public static void CreateBMP(string fileName)
        {
            int width = 800;
            int height = 400;
            Bitmap bitmap = new Bitmap(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    bitmap.SetPixel(i, j, System.Drawing.Color.White);
                }
            }
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Bmp);
                Image image = Image.FromStream(ms);
                Bitmap bm = new Bitmap(image, image.Width, image.Height);
                Graphics g = Graphics.FromImage(bm);
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Near;
                Font font = new Font("Verdana", 12);

                string tag1 = "Opened:  ";
                string tag2 = "InstrumentCode:  ";
                string tag3 = "Time:  ";

                string content1 = "Closed";
                string content2 = "AUDUSD";
                string content3 = DateTime.Now.ToString();
                SolidBrush brush = new SolidBrush(System.Drawing.Color.Black);

                float left = 30;
                float top = 50;
                var tilefont = new Font("Verdana", 14, FontStyle.Bold);
                var sz = g.MeasureString("OrderExecuted", tilefont);
                g.DrawString("OrderExecuted", tilefont, brush, (width - sz.Width) / 2, 0);
                var size = g.MeasureString(tag1, font);
                g.DrawString(tag1, font, brush, left, top, sf);
                g.DrawString(content1, font, brush, left + size.Width, top, sf);
                top += 2 * size.Height;
                size = g.MeasureString(tag2, font);
                g.DrawString(tag2, font, brush, left, top, sf);
                g.DrawString(content2, font, brush, left + size.Width, top, sf);
                top += 2 * size.Height;
                size = g.MeasureString(tag3, font);

                g.DrawString(tag3, font, brush, left, top, sf);
                g.DrawString(content3, font, brush, left + size.Width, top, sf);

                bm.Save(fileName);
                bitmap.Dispose();
                bm.Dispose();
            }

        }

        public static void CreateBmpFromXml(string fileName)
        {
            int width = 800;
            int height = 400;
            Bitmap bitmap = new Bitmap(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    bitmap.SetPixel(i, j, System.Drawing.Color.White);
                }
            }
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Bmp);
                Image image = Image.FromStream(ms);
                Bitmap bm = new Bitmap(image, image.Width, image.Height);
                Graphics g = Graphics.FromImage(bm);
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Near;
                Font font = new Font("Verdana", 12);
                XElement element = XElement.Load("Format.xml");
                XElement contentXel = element.Descendants("Format").Single();
                string pattern = @"\n?\s?(\{\w+\})\n?\s?";
                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                MatchCollection col = regex.Matches(contentXel.Value);
                string result = string.Empty;
                foreach (Match match in col)
                {

                    Regex r = new Regex(match.Groups[1].Value);
                    if (match.Groups[1].Value == "{ExecutedTime}")
                    {
                        result = r.Replace(contentXel.Value, "ExecutedTime:  " + DateTime.Now.ToString());
                    }
                    else if (match.Groups[1].Value == "{code}")
                    {
                        result = r.Replace(result, "code:  AUDUSD");
                    }
                }

                string pattern2 = @"(\\n|\\r\\n)";
                Regex regex2 = new Regex(pattern2);
                List<string> list = new List<string>();
                int lastPosition = 0;
                foreach (Match match in regex2.Matches(result))
                {
                    list.Add(result.Substring(lastPosition, match.Index - lastPosition));
                    lastPosition = match.Index + 2;
                }
                list.Add(result.Substring(lastPosition));
                SolidBrush brush = new SolidBrush(System.Drawing.Color.Black);
                float left = 0;
                float top = 0;
                foreach (string str in list)
                {
                    var size = g.MeasureString(str, font);
                    g.DrawString(str, font, brush, left, top, sf);
                    top += size.Height * 2;
                }
                bm.Save(fileName);
                bitmap.Dispose();
                bm.Dispose();
            }
            Process.Start(fileName);
        }
    }
}
