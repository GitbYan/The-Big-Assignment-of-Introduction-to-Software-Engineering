using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuGuGuMusic
{
    public partial class Login : Form
    {
        #region 无边框
        /// 实现窗体边框阴影
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
         );
        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        private bool m_aeroEnabled;                     // variables for box shadow
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        public struct MARGINS                           // struct for box shadow
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        private const int WM_NCHITTEST = 0x84;          // variables for dragging the form
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();

                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;

                return cp;
            }
        }

        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:// box shadow 窗体边框阴影
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 1,
                            rightWidth = 1,
                            topHeight = 1
                        };
                        DwmExtendFrameIntoClientArea(this.Handle, ref margins);
                    }
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        /// 实现窗体拖动

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        private const int edgeX = 4;//4px的间距来实现缩放
        private const int edgeY = 4;
        #endregion

        public Login()
        {
            InitializeComponent();
            this.MouseDown += new MouseEventHandler(Form_MouseDown);
        }

        private User user = new User();

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_OnMouseHover(object sender,EventArgs e)
        {
            try
            {
                Button b = (Button)sender;
                b.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
            catch(Exception ce)
            {
                Console.WriteLine(ce.Message);
            }
        }

        private void Btn_OnMouseLeave(object sender, EventArgs e)
        {
            try
            {
                Button b = (Button)sender;
                b.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            }
            catch (Exception ce)
            {
                Console.WriteLine(ce.Message);
            }
        }

        private void Btn_Register_Click(object sender, EventArgs e)
        {
            try
            {

            }catch(Exception ce)
            {
                Console.WriteLine(ce.Message);
            }
        }

        private void Btn_Forgot_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ce)
            {
                Console.WriteLine(ce.Message);
            }
        }

        private void Btn_Login_Click(object sender, EventArgs e)
        {
            try
            {
                TryLogin();
            }
            catch (Exception ce)
            {
                Console.WriteLine(ce.Message);
            }
        }

        private void TxtBox_GotFocus(object sender,EventArgs e)
        {
            try
            {
                TextBox mTextBox = (TextBox)sender;
                switch (mTextBox.Name)
                {
                    case "TxtBox_Account":
                        if (Lbl_Tip1.Visible)
                        {
                            Lbl_Tip1.ForeColor = System.Drawing.SystemColors.Menu;
                        }
                        break;
                    case "TxtBox_Password":
                        if (Lbl_Tip2.Visible)
                        {
                            Lbl_Tip2.ForeColor = System.Drawing.SystemColors.Menu;
                        }
                        break;
                }
            }catch(Exception ce)
            {
                Console.WriteLine(ce.Message);
            }
        }

        private void TxtBox_LostFocus(object sender, EventArgs e)
        {
            try
            {
                TextBox mTextBox = (TextBox)sender;
                switch (mTextBox.Name)
                {
                    case "TxtBox_Account":
                        if (Lbl_Tip1.Visible)
                        {
                            Lbl_Tip1.ForeColor = System.Drawing.SystemColors.GrayText;
                        }
                        break;
                    case "TxtBox_Password":
                        if (Lbl_Tip2.Visible)
                        {
                            Lbl_Tip2.ForeColor = System.Drawing.SystemColors.GrayText;
                        }
                        break;
                }
            }
            catch (Exception ce)
            {
                Console.WriteLine(ce.Message);
            }
        }

        private void TxtBox_Account_TextChanged(object sender, EventArgs e)
        {
            if (TxtBox_Account.Text == "")
            {
                Lbl_Tip1.Show();
            }
            else
            {
                Lbl_Tip1.Hide();
            }
        }


        private void TxtBox_Password_TextChanged(object sender, EventArgs e)
        {
            if (TxtBox_Password.Text == "")
            {
                Lbl_Tip2.Show();
            }
            else
            {
                Lbl_Tip2.Hide();
            }
        }

        private void TxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox mTextBox = (TextBox)sender;
                if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122) || e.KeyChar == 8 || e.KeyChar == 13))
                {
                    e.Handled = true;
                }
                if (mTextBox.Name == "TxtBox_Password" && e.KeyChar == 13)
                {
                    TryLogin();
                }
            }catch(Exception ce)
            {
                Console.WriteLine(ce.Message);
            }
        }

        private void TryLogin()
        {
            try
            {
                string userid = TxtBox_Account.Text.ToString();
                string password = TxtBox_Password.Text.ToString();
                ///
                var sha256 = new SHA256Managed();
                var Asc = new ASCIIEncoding();
                var tmpByte = Asc.GetBytes(password);
                var EncryptBytes = sha256.ComputeHash(tmpByte);
                password = EncryptBytes.ToString();
                sha256.Clear();
                ///
                if (ValidateAccount(userid, password))
                {
                    this.Close();
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private bool ValidateAccount(string Account,string Password)
        {
            try
            {
                MDB mDB = new MDB();
                if(Password == mDB.GetUserPassword(Account))
                {
                    user.User_Id = Account;
                    return true;
                }
                return false;
            }catch(Exception ce)
            {
                Console.WriteLine("登陆失败" + ce.Message);
                return false;
            }
        }
    }
}
