using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;

namespace WebApplication1
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MailMessage message = new MailMessage();
            using (SmtpClient smtpClient = new SmtpClient("smtp.163.com",21))
            {


                smtpClient.UseDefaultCredentials = false; 
                smtpClient.Credentials = new NetworkCredential("omnicare_hhb", "Omni1234");
                
                smtpClient.EnableSsl = true;
                message.From = new MailAddress("omnicare_hhb@163.com");
                message.To.Add("huhuabo111@126.com");
                message.Subject = "RobertHu";
                message.IsBodyHtml = true;
                
                message.Body = "dfsssssssssssssssssssssssssssssssssssssssssssssssss";
                smtpClient.Send(message);
            }
        }
    }
}
