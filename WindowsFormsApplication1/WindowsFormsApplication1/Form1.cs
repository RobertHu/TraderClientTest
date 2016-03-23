using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                FAXCOMLib.FaxServer faxServer = new FAXCOMLib.FaxServerClass();
                faxServer.Connect(Environment.MachineName);

    

                FAXCOMLib.FaxDoc faxDoc = (FAXCOMLib.FaxDoc)faxServer.CreateDocument("Robert.txt");

                faxDoc.RecipientName = "Omnicare";
                faxDoc.FaxNumber = "86182993";

                faxDoc.DisplayName = "Test";
              
                int Response = faxDoc.Send();



                faxServer.Disconnect();

            }
            catch (Exception Ex) { MessageBox.Show(Ex.Message); }
        }

        private void AddPage(string fileName,PdfDocument pdfDoc)
        {
            using (FileStream stream1 = new FileStream(Application.StartupPath + @"\"+fileName, FileMode.Open))
            {
                StreamReader sr = new StreamReader(stream1);
                string content = sr.ReadToEnd();
                PdfPage page = pdfDoc.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Verdana", 20, XFontStyle.Regular);
                gfx.DrawString(content, font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormats.Center);
            }
        }
    }
}
