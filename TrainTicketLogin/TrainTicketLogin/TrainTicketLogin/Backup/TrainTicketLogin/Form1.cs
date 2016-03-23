using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using TrainTicketLogin.Helper;
using System.Net;
using System.Diagnostics;

namespace TrainTicketLogin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {

        }




        Thread threadLogin;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Verify login field
            if (this.txtUserName.Text.Length < 2)
            {
                MessageBox.Show("请输入用户名!");
                return;
            }
            if (this.txtPassword.Text.Length < 2)
            {
                MessageBox.Show("请输入密码!");
                return;
            }
            if (this.txtCode.Text.Length < 4)
            {
                MessageBox.Show("请输入验证码");
                return;
            }

            // work
            threadLogin = new Thread(new ThreadStart(Login));
            threadLogin.Name = "LoginThread";
            threadLogin.Start();
        }

        private void GetLoginCode()
        {
            SetLoginResult("获取验证码中");
            string url = "https://dynamic.12306.cn/otsweb/passCodeAction.do?rand=sjrand";

            Stream stream = null;
            for (int i = 0; i < 10000; i++)
            {
                stream = HttpHelper.GetResponseImage(url);
                if (stream != null)
                {
                    break;
                }
                else
                {
                    SetLoginResult("获取验证码失败，正在进行第" + i.ToString() + "次重试");
                }
            }

            Image image = Image.FromStream(stream);
            this.pictureBox1.Image = image;
            SetLoginResult("验证码获取成功");
        }

        private void Login()
        {
            SetLoginResult("正在登陆中");

            string userName = this.txtUserName.Text.Trim();
            string password = this.txtPassword.Text.Trim();
            string code = this.txtCode.Text.Trim();

            string loginUrl = "https://dynamic.12306.cn/otsweb/loginAction.do?method=login";

            for (int i = 0; i < 10000; i++)
            {
                try
                {
                    // Get random number
                    string randomUrl = "https://dynamic.12306.cn/otsweb/loginAction.do?method=loginAysnSuggest";
                    string afterRandom = HttpHelper.GetResponse(randomUrl, "POST", string.Empty);
                    string[] randoms = afterRandom.Split('"');
                    string random = randoms[3];

                    string data = "loginRand=" + random + "&loginUser.user_name=" + userName + "&nameErrorFocus=&user.password=" + password + "&passwordErrorFocus=&randCode=" + code + "&randErrorFocus=";

      
                    string afterLogin = HttpHelper.GetResponse(loginUrl, "POST", data);

                    if (afterLogin.Contains("密码输入错误"))
                    {
                        SetLoginResult("密码输入错误，请重新输入密码");
                        break;
                    }
                    else if (afterLogin.Contains("欢迎您"))
                    {
                        SetLoginResult("登陆成功");

                        string pattern = "class=\"text_16\">(.*?)</h1>";
                        string welcomeText = HttpHelper.GetRegexString(pattern, afterLogin);
                        SetLoginResult("登陆成功 " + welcomeText);

                        break;
                    }
                    else if (afterLogin.Contains("请输入正确的验证码"))
                    {
                        SetLoginResult("登陆失败，请输入正确的验证码");

                        break;
                    }
                    else if (afterLogin.Contains("您的用户已经被锁定"))
                    {
                        SetLoginResult("您的用户已经被锁定,请稍候再试");

                        break;
                    }
                    else if (afterLogin.Contains("当前访问用户过多，请稍后重试"))
                    {
                        SetLoginResult("当前访问用户过多，请稍后重试，进行第" + i.ToString() + "次重试");
                    }
                    else
                    {
                        SetLoginResult("登录失败，进行第" + i.ToString() + "次重试");
                    }
                }
                catch { }
            }
        }

        private delegate void WriteLabelDelegate(object entry);

        private void WriteLoginResult(object text)
        {
            this.labellogin.Text = text.ToString();
        }

        private void SetLoginResult(string text)
        {
            this.labellogin.Invoke(new WriteLabelDelegate(WriteLoginResult), text);
        }



        private void GetLoginCodeThread()
        {
            Thread threadGetLoginCode = new Thread(new ThreadStart(GetLoginCode));
            threadGetLoginCode.Name = "GetLoginCodeThread";
            threadGetLoginCode.Start();
        }

        private void btnOpenPage_Click(object sender, EventArgs e)
        {
            foreach (Cookie cookie in HttpHelper.CookieContainers.GetCookies(new Uri("https://dynamic.12306.cn/otsweb/loginAction.do?method=login")))
            {
                API.InternetSetCookie("https://" + cookie.Domain.ToString(), cookie.Name.ToString(), cookie.Value.ToString() + ";expires=Sun,22-Feb-2099 00:00:00 GMT");
            }
            Process.Start("IExplore.exe", "https://dynamic.12306.cn/otsweb/");
        }

        private void btnChangeLoginImage_Click(object sender, EventArgs e)
        {
            GetLoginCodeThread();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetLoginCodeThread();
        }

        private void btnLoginStop_Click(object sender, EventArgs e)
        {
            try
            {
                threadLogin.Abort();
            }
            catch
            {
                

            }
        }
    }
}
