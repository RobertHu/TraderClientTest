namespace TrainTicketLogin
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLoginStop = new System.Windows.Forms.Button();
            this.btnChangeLoginImage = new System.Windows.Forms.Button();
            this.btnOpenPage = new System.Windows.Forms.Button();
            this.labellogin = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLoginStop);
            this.groupBox1.Controls.Add(this.btnChangeLoginImage);
            this.groupBox1.Controls.Add(this.btnOpenPage);
            this.groupBox1.Controls.Add(this.labellogin);
            this.groupBox1.Controls.Add(this.btnLogin);
            this.groupBox1.Controls.Add(this.txtCode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(1, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(692, 228);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "第一步: 登录";
            // 
            // btnLoginStop
            // 
            this.btnLoginStop.Location = new System.Drawing.Point(16, 151);
            this.btnLoginStop.Name = "btnLoginStop";
            this.btnLoginStop.Size = new System.Drawing.Size(75, 25);
            this.btnLoginStop.TabIndex = 10;
            this.btnLoginStop.Text = "停止登陆";
            this.btnLoginStop.UseVisualStyleBackColor = true;
            this.btnLoginStop.Click += new System.EventHandler(this.btnLoginStop_Click);
            // 
            // btnChangeLoginImage
            // 
            this.btnChangeLoginImage.Location = new System.Drawing.Point(215, 62);
            this.btnChangeLoginImage.Name = "btnChangeLoginImage";
            this.btnChangeLoginImage.Size = new System.Drawing.Size(83, 23);
            this.btnChangeLoginImage.TabIndex = 9;
            this.btnChangeLoginImage.Text = "获取验证码";
            this.btnChangeLoginImage.UseVisualStyleBackColor = true;
            this.btnChangeLoginImage.Click += new System.EventHandler(this.btnChangeLoginImage_Click);
            // 
            // btnOpenPage
            // 
            this.btnOpenPage.ForeColor = System.Drawing.Color.Red;
            this.btnOpenPage.Location = new System.Drawing.Point(111, 106);
            this.btnOpenPage.Name = "btnOpenPage";
            this.btnOpenPage.Size = new System.Drawing.Size(106, 25);
            this.btnOpenPage.TabIndex = 5;
            this.btnOpenPage.Text = "用IE打开12306";
            this.btnOpenPage.UseVisualStyleBackColor = true;
            this.btnOpenPage.Click += new System.EventHandler(this.btnOpenPage_Click);
            // 
            // labellogin
            // 
            this.labellogin.AutoEllipsis = true;
            this.labellogin.AutoSize = true;
            this.labellogin.Location = new System.Drawing.Point(16, 197);
            this.labellogin.Name = "labellogin";
            this.labellogin.Size = new System.Drawing.Size(55, 13);
            this.labellogin.TabIndex = 7;
            this.labellogin.Text = "运行结果";
            // 
            // btnLogin
            // 
            this.btnLogin.ForeColor = System.Drawing.Color.Red;
            this.btnLogin.Location = new System.Drawing.Point(19, 106);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 25);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "登陆";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(70, 63);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(55, 20);
            this.txtCode.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "验证码:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(254, 26);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(130, 20);
            this.txtPassword.TabIndex = 2;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(66, 26);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(130, 20);
            this.txtUserName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(213, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "密码:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "用户名:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(138, 64);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(60, 22);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 262);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnChangeLoginImage;
        private System.Windows.Forms.Button btnOpenPage;
        private System.Windows.Forms.Label labellogin;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnLoginStop;
    }
}

