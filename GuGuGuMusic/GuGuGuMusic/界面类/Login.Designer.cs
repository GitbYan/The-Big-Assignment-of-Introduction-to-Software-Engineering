namespace GuGuGuMusic
{
    partial class Login
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
            this.Btn_Login = new System.Windows.Forms.Button();
            this.Btn_Close = new System.Windows.Forms.Button();
            this.PicIcon = new System.Windows.Forms.PictureBox();
            this.Btn_Register = new System.Windows.Forms.Button();
            this.Btn_Forgot = new System.Windows.Forms.Button();
            this.Lbl_Tip1 = new System.Windows.Forms.Label();
            this.Lbl_Tip2 = new System.Windows.Forms.Label();
            this.TxtBox_Password = new System.Windows.Forms.TextBox();
            this.TxtBox_Account = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.PicIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // Btn_Login
            // 
            this.Btn_Login.BackColor = System.Drawing.SystemColors.Highlight;
            this.Btn_Login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Login.FlatAppearance.BorderSize = 0;
            this.Btn_Login.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Highlight;
            this.Btn_Login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Login.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Btn_Login.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Btn_Login.Location = new System.Drawing.Point(100, 220);
            this.Btn_Login.Name = "Btn_Login";
            this.Btn_Login.Size = new System.Drawing.Size(280, 40);
            this.Btn_Login.TabIndex = 2;
            this.Btn_Login.Text = "授权并登录";
            this.Btn_Login.UseVisualStyleBackColor = false;
            this.Btn_Login.Click += new System.EventHandler(this.Btn_Login_Click);
            // 
            // Btn_Close
            // 
            this.Btn_Close.FlatAppearance.BorderSize = 0;
            this.Btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Close.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.Btn_Close.Location = new System.Drawing.Point(430, 0);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(50, 50);
            this.Btn_Close.TabIndex = 3;
            this.Btn_Close.TabStop = false;
            this.Btn_Close.Text = "×";
            this.Btn_Close.UseVisualStyleBackColor = true;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // PicIcon
            // 
            this.PicIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PicIcon.Location = new System.Drawing.Point(0, 0);
            this.PicIcon.Margin = new System.Windows.Forms.Padding(2);
            this.PicIcon.Name = "PicIcon";
            this.PicIcon.Size = new System.Drawing.Size(60, 50);
            this.PicIcon.TabIndex = 26;
            this.PicIcon.TabStop = false;
            // 
            // Btn_Register
            // 
            this.Btn_Register.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Register.FlatAppearance.BorderSize = 0;
            this.Btn_Register.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Btn_Register.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Btn_Register.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Register.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_Register.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Btn_Register.Location = new System.Drawing.Point(310, 325);
            this.Btn_Register.Name = "Btn_Register";
            this.Btn_Register.Size = new System.Drawing.Size(75, 23);
            this.Btn_Register.TabIndex = 27;
            this.Btn_Register.TabStop = false;
            this.Btn_Register.Text = "注册账号";
            this.Btn_Register.UseVisualStyleBackColor = true;
            this.Btn_Register.Click += new System.EventHandler(this.Btn_Register_Click);
            this.Btn_Register.MouseLeave += new System.EventHandler(this.Btn_OnMouseLeave);
            this.Btn_Register.MouseHover += new System.EventHandler(this.Btn_OnMouseHover);
            // 
            // Btn_Forgot
            // 
            this.Btn_Forgot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Forgot.FlatAppearance.BorderSize = 0;
            this.Btn_Forgot.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Btn_Forgot.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Btn_Forgot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Forgot.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_Forgot.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Btn_Forgot.Location = new System.Drawing.Point(391, 325);
            this.Btn_Forgot.Name = "Btn_Forgot";
            this.Btn_Forgot.Size = new System.Drawing.Size(75, 23);
            this.Btn_Forgot.TabIndex = 28;
            this.Btn_Forgot.TabStop = false;
            this.Btn_Forgot.Text = "忘记密码?";
            this.Btn_Forgot.UseVisualStyleBackColor = true;
            this.Btn_Forgot.Click += new System.EventHandler(this.Btn_Forgot_Click);
            this.Btn_Forgot.MouseLeave += new System.EventHandler(this.Btn_OnMouseLeave);
            this.Btn_Forgot.MouseHover += new System.EventHandler(this.Btn_OnMouseHover);
            // 
            // Lbl_Tip1
            // 
            this.Lbl_Tip1.BackColor = System.Drawing.SystemColors.Window;
            this.Lbl_Tip1.Enabled = false;
            this.Lbl_Tip1.Font = new System.Drawing.Font("微软雅黑 Light", 16F);
            this.Lbl_Tip1.ForeColor = System.Drawing.SystemColors.Menu;
            this.Lbl_Tip1.Location = new System.Drawing.Point(108, 123);
            this.Lbl_Tip1.Name = "Lbl_Tip1";
            this.Lbl_Tip1.Size = new System.Drawing.Size(62, 31);
            this.Lbl_Tip1.TabIndex = 29;
            this.Lbl_Tip1.Text = "账号";
            // 
            // Lbl_Tip2
            // 
            this.Lbl_Tip2.BackColor = System.Drawing.SystemColors.Window;
            this.Lbl_Tip2.Enabled = false;
            this.Lbl_Tip2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Lbl_Tip2.Font = new System.Drawing.Font("微软雅黑 Light", 16F);
            this.Lbl_Tip2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.Lbl_Tip2.Location = new System.Drawing.Point(108, 173);
            this.Lbl_Tip2.Name = "Lbl_Tip2";
            this.Lbl_Tip2.Size = new System.Drawing.Size(62, 31);
            this.Lbl_Tip2.TabIndex = 30;
            this.Lbl_Tip2.Text = "密码";
            // 
            // TxtBox_Password
            // 
            this.TxtBox_Password.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.TxtBox_Password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBox_Password.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtBox_Password.Font = new System.Drawing.Font("微软雅黑 Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtBox_Password.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.TxtBox_Password.Location = new System.Drawing.Point(100, 170);
            this.TxtBox_Password.MaxLength = 12;
            this.TxtBox_Password.Name = "TxtBox_Password";
            this.TxtBox_Password.PasswordChar = '●';
            this.TxtBox_Password.Size = new System.Drawing.Size(280, 39);
            this.TxtBox_Password.TabIndex = 1;
            this.TxtBox_Password.UseSystemPasswordChar = true;
            this.TxtBox_Password.TextChanged += new System.EventHandler(this.TxtBox_Password_TextChanged);
            this.TxtBox_Password.GotFocus += new System.EventHandler(this.TxtBox_GotFocus);
            this.TxtBox_Password.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBox_KeyPress);
            this.TxtBox_Password.LostFocus += new System.EventHandler(this.TxtBox_LostFocus);
            // 
            // TxtBox_Account
            // 
            this.TxtBox_Account.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.TxtBox_Account.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBox_Account.Font = new System.Drawing.Font("微软雅黑 Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtBox_Account.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.TxtBox_Account.Location = new System.Drawing.Point(100, 120);
            this.TxtBox_Account.MaxLength = 12;
            this.TxtBox_Account.Name = "TxtBox_Account";
            this.TxtBox_Account.Size = new System.Drawing.Size(280, 39);
            this.TxtBox_Account.TabIndex = 0;
            this.TxtBox_Account.TextChanged += new System.EventHandler(this.TxtBox_Account_TextChanged);
            this.TxtBox_Account.GotFocus += new System.EventHandler(this.TxtBox_GotFocus);
            this.TxtBox_Account.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBox_KeyPress);
            this.TxtBox_Account.LostFocus += new System.EventHandler(this.TxtBox_LostFocus);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(480, 360);
            this.Controls.Add(this.Lbl_Tip2);
            this.Controls.Add(this.Lbl_Tip1);
            this.Controls.Add(this.Btn_Forgot);
            this.Controls.Add(this.Btn_Register);
            this.Controls.Add(this.PicIcon);
            this.Controls.Add(this.Btn_Close);
            this.Controls.Add(this.Btn_Login);
            this.Controls.Add(this.TxtBox_Password);
            this.Controls.Add(this.TxtBox_Account);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            ((System.ComponentModel.ISupportInitialize)(this.PicIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtBox_Account;
        private System.Windows.Forms.TextBox TxtBox_Password;
        private System.Windows.Forms.Button Btn_Login;
        private System.Windows.Forms.Button Btn_Close;
        private System.Windows.Forms.PictureBox PicIcon;
        private System.Windows.Forms.Button Btn_Register;
        private System.Windows.Forms.Button Btn_Forgot;
        private System.Windows.Forms.Label Lbl_Tip2;
        private System.Windows.Forms.Label Lbl_Tip1;
    }
}