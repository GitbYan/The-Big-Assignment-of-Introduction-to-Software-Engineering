namespace GuGuGuMusic
{
    partial class Register
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
            this.Lbl_Tip2 = new System.Windows.Forms.Label();
            this.Lbl_Tip1 = new System.Windows.Forms.Label();
            this.Btn_Login = new System.Windows.Forms.Button();
            this.TxtBox_Password = new System.Windows.Forms.TextBox();
            this.TxtBox_Account = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtBox_ConfirmPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Lbl_Tip3 = new System.Windows.Forms.Label();
            this.Btn_Back = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Lbl_Tip2
            // 
            this.Lbl_Tip2.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Tip2.Enabled = false;
            this.Lbl_Tip2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Lbl_Tip2.Font = new System.Drawing.Font("微软雅黑 Light", 16F);
            this.Lbl_Tip2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.Lbl_Tip2.Location = new System.Drawing.Point(104, 160);
            this.Lbl_Tip2.Name = "Lbl_Tip2";
            this.Lbl_Tip2.Size = new System.Drawing.Size(62, 31);
            this.Lbl_Tip2.TabIndex = 35;
            this.Lbl_Tip2.Text = "密码";
            // 
            // Lbl_Tip1
            // 
            this.Lbl_Tip1.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Tip1.Enabled = false;
            this.Lbl_Tip1.Font = new System.Drawing.Font("微软雅黑 Light", 16F);
            this.Lbl_Tip1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.Lbl_Tip1.Location = new System.Drawing.Point(104, 110);
            this.Lbl_Tip1.Name = "Lbl_Tip1";
            this.Lbl_Tip1.Size = new System.Drawing.Size(62, 31);
            this.Lbl_Tip1.TabIndex = 34;
            this.Lbl_Tip1.Text = "账号";
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
            this.Btn_Login.Location = new System.Drawing.Point(100, 260);
            this.Btn_Login.Name = "Btn_Login";
            this.Btn_Login.Size = new System.Drawing.Size(280, 40);
            this.Btn_Login.TabIndex = 3;
            this.Btn_Login.Text = "注册并登录";
            this.Btn_Login.UseVisualStyleBackColor = false;
            this.Btn_Login.Click += new System.EventHandler(this.Btn_Login_Click);
            // 
            // TxtBox_Password
            // 
            this.TxtBox_Password.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.TxtBox_Password.BackColor = System.Drawing.Color.White;
            this.TxtBox_Password.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtBox_Password.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtBox_Password.Font = new System.Drawing.Font("微软雅黑 Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtBox_Password.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.TxtBox_Password.Location = new System.Drawing.Point(100, 160);
            this.TxtBox_Password.MaxLength = 12;
            this.TxtBox_Password.Name = "TxtBox_Password";
            this.TxtBox_Password.PasswordChar = '●';
            this.TxtBox_Password.Size = new System.Drawing.Size(280, 32);
            this.TxtBox_Password.TabIndex = 1;
            this.TxtBox_Password.UseSystemPasswordChar = true;
            this.TxtBox_Password.TextChanged += new System.EventHandler(this.TxtBox_Password_TextChanged);
            this.TxtBox_Password.GotFocus += new System.EventHandler(this.TxtBox_GotFocus);
            this.TxtBox_Password.LostFocus += new System.EventHandler(this.TxtBox_LostFocus);
            // 
            // TxtBox_Account
            // 
            this.TxtBox_Account.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.TxtBox_Account.BackColor = System.Drawing.Color.White;
            this.TxtBox_Account.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtBox_Account.Font = new System.Drawing.Font("微软雅黑 Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtBox_Account.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.TxtBox_Account.Location = new System.Drawing.Point(100, 110);
            this.TxtBox_Account.MaxLength = 12;
            this.TxtBox_Account.Name = "TxtBox_Account";
            this.TxtBox_Account.Size = new System.Drawing.Size(280, 32);
            this.TxtBox_Account.TabIndex = 0;
            this.TxtBox_Account.TextChanged += new System.EventHandler(this.TxtBox_Account_TextChanged);
            this.TxtBox_Account.GotFocus += new System.EventHandler(this.TxtBox_GotFocus);
            this.TxtBox_Account.LostFocus += new System.EventHandler(this.TxtBox_LostFocus);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(93, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(294, 20);
            this.label1.TabIndex = 36;
            this.label1.Text = "———————————————————";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label2.Location = new System.Drawing.Point(93, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(294, 20);
            this.label2.TabIndex = 37;
            this.label2.Text = "———————————————————";
            // 
            // TxtBox_ConfirmPassword
            // 
            this.TxtBox_ConfirmPassword.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.TxtBox_ConfirmPassword.BackColor = System.Drawing.Color.White;
            this.TxtBox_ConfirmPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtBox_ConfirmPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtBox_ConfirmPassword.Font = new System.Drawing.Font("微软雅黑 Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtBox_ConfirmPassword.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.TxtBox_ConfirmPassword.Location = new System.Drawing.Point(100, 210);
            this.TxtBox_ConfirmPassword.MaxLength = 12;
            this.TxtBox_ConfirmPassword.Name = "TxtBox_ConfirmPassword";
            this.TxtBox_ConfirmPassword.PasswordChar = '●';
            this.TxtBox_ConfirmPassword.Size = new System.Drawing.Size(280, 32);
            this.TxtBox_ConfirmPassword.TabIndex = 2;
            this.TxtBox_ConfirmPassword.UseSystemPasswordChar = true;
            this.TxtBox_ConfirmPassword.TextChanged += new System.EventHandler(this.TxtBox_ConfirmPassword_TextChanged);
            this.TxtBox_ConfirmPassword.GotFocus += new System.EventHandler(this.TxtBox_GotFocus);
            this.TxtBox_ConfirmPassword.LostFocus += new System.EventHandler(this.TxtBox_LostFocus);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label3.Location = new System.Drawing.Point(93, 235);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(294, 20);
            this.label3.TabIndex = 39;
            this.label3.Text = "———————————————————";
            // 
            // Lbl_Tip3
            // 
            this.Lbl_Tip3.BackColor = System.Drawing.Color.Transparent;
            this.Lbl_Tip3.Enabled = false;
            this.Lbl_Tip3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Lbl_Tip3.Font = new System.Drawing.Font("微软雅黑 Light", 16F);
            this.Lbl_Tip3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.Lbl_Tip3.Location = new System.Drawing.Point(104, 210);
            this.Lbl_Tip3.Name = "Lbl_Tip3";
            this.Lbl_Tip3.Size = new System.Drawing.Size(121, 31);
            this.Lbl_Tip3.TabIndex = 40;
            this.Lbl_Tip3.Text = "确认密码";
            // 
            // Btn_Back
            // 
            this.Btn_Back.FlatAppearance.BorderSize = 0;
            this.Btn_Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Back.Font = new System.Drawing.Font("微软雅黑 Light", 20F);
            this.Btn_Back.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.Btn_Back.Location = new System.Drawing.Point(420, 0);
            this.Btn_Back.Name = "Btn_Back";
            this.Btn_Back.Size = new System.Drawing.Size(60, 60);
            this.Btn_Back.TabIndex = 41;
            this.Btn_Back.TabStop = false;
            this.Btn_Back.Text = "←";
            this.Btn_Back.UseVisualStyleBackColor = true;
            this.Btn_Back.Click += new System.EventHandler(this.Btn_Back_Click);
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(480, 360);
            this.Controls.Add(this.Btn_Back);
            this.Controls.Add(this.Lbl_Tip3);
            this.Controls.Add(this.TxtBox_ConfirmPassword);
            this.Controls.Add(this.Lbl_Tip2);
            this.Controls.Add(this.Lbl_Tip1);
            this.Controls.Add(this.Btn_Login);
            this.Controls.Add(this.TxtBox_Password);
            this.Controls.Add(this.TxtBox_Account);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Register";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lbl_Tip2;
        private System.Windows.Forms.Label Lbl_Tip1;
        private System.Windows.Forms.Button Btn_Login;
        private System.Windows.Forms.TextBox TxtBox_Password;
        private System.Windows.Forms.TextBox TxtBox_Account;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtBox_ConfirmPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Lbl_Tip3;
        private System.Windows.Forms.Button Btn_Back;
    }
}