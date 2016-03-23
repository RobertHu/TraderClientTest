using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Mail;
using System.Net;

namespace Smtp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
             MailMessage message = new MailMessage();
            using (SmtpClient smtpClient = new SmtpClient("smtp.163.com", 25))
            {
                smtpClient.Timeout =1000*60;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("omnicare_hhb", "Omni1234");

                message.From = new MailAddress("omnicare_hhb@163.com");
                message.To.Add("huhuabo111@126.com");
                message.Subject ="Hello world";
                message.IsBodyHtml = false;
                message.Body = "dfsssssssssssssssssssssssssssssssssssssssssssssssss";
                smtpClient.Send(message);
            }
        }
    }
}
