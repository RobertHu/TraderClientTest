using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Windows.Media.Imaging;
using BitMiracle.LibTiff.Classic;
using System.Diagnostics;
using System.Drawing.Imaging;


namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //string fileName = "hu.bmp";
            //Bitmap bitmap = new Bitmap(800, 800);
            //for (int i = 0; i < 800; i++)
            //{
            //    for (int j = 0; j < 800; j++)
            //    {
            //        bitmap.SetPixel(i, j, Color.White);
            //    }
            //}
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    bitmap.Save(ms, ImageFormat.Bmp);
            //    Image image = Image.FromStream(ms);
            //    Bitmap bm = new Bitmap(image, image.Width, image.Height);
            //    Graphics g = Graphics.FromImage(bm);
            //    g.DrawString("dddddd", new Font("Verdana", 8), new SolidBrush(Color.Black), new PointF(0, 0));
            //    bm.Save(fileName);
            //}
            //Process.Start(fileName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //FaxEmailService.FaxEmailService service = new FaxEmailService.FaxEmailService();
            //service.NotifyOrderExecuted(new Guid("319904DA-5DB6-4023-87DC-FFF0A6A8E86D"))

        }
    }
}
