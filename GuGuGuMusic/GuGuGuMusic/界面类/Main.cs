using AxWMPLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuGuGuMusic
{

    public partial class Main : Form
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

        ///实现窗体尺寸改变
        const int Guying_HTLEFT = 10;
        const int Guying_HTRIGHT = 11;
        const int Guying_HTTOP = 12;
        const int Guying_HTTOPLEFT = 13;
        const int Guying_HTTOPRIGHT = 14;
        const int Guying_HTBOTTOM = 15;
        const int Guying_HTBOTTOMLEFT = 0x10;
        const int Guying_HTBOTTOMRIGHT = 17;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0084:
                    base.WndProc(ref m);
                    Point vPoint = new Point((int)m.LParam & 0xFFFF,
                        (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)Guying_HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)Guying_HTBOTTOMLEFT;
                        else m.Result = (IntPtr)Guying_HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)Guying_HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)Guying_HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)Guying_HTRIGHT;
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)Guying_HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)Guying_HTBOTTOM;
                    break;
                case 0x0201:                //鼠标左键按下的消息 
                    m.Msg = 0x00A1;         //更改消息为非客户区按下鼠标 
                    m.LParam = IntPtr.Zero; //默认值 
                    m.WParam = new IntPtr(2);//鼠标放在标题栏内 
                    base.WndProc(ref m);
                    break;
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
            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)     // drag the form
                m.Result = (IntPtr)HTCAPTION;       

        }

        /// 实现窗体拖动
        
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        private const int edgeX = 4;//4px的间距来实现缩放
        private const int edgeY = 4;
        #endregion

        public Main()
        {
            m_aeroEnabled = false;
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);            
            this.Panel_Tool.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            this.Panel_Play.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            this.Panel_Icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            this.Main_Panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);

            ((System.ComponentModel.ISupportInitialize)(this.WMP)).BeginInit();//myClient为第三方控件名
            this.Controls.Add(WMP);
            ((System.ComponentModel.ISupportInitialize)(this.WMP)).EndInit();


            playListForm = new PlayListForm() { TopLevel = false,TopMost = true};
            this.Controls.Add(playListForm);
            musics = mDB.GetMusics();
            mGroup.musics = musics;
        }

        private MDB mDB = new MDB();//连接数据库
        private PlayListForm playListForm = new PlayListForm() { TopLevel = false };//展示当前播放列表   
        AxWindowsMediaPlayer WMP = new AxWindowsMediaPlayer() { Visible = false };//音乐播放控件
        private MGroup mGroup = new MGroup();//当前播放列表
        private List<Music> musics = new List<Music>();




        #region 事件
        ///实现部分控件随窗体缩放
        private void Main_Resize(object sender, EventArgs e)//resize事件 实现缩放
        {
            playListForm.Width = 0;
            playListForm.Hide();
            //大小
            Main_Panel.Width = this.Width - 2 * edgeX;
            Main_Panel.Height = this.Height - 2 * edgeY;
            Panel_Nav.Height = (int)(this.Height - 2 * edgeY);
            Panel_Tool.Width = (int)(this.Width - Panel_Nav.Width - 2 * edgeX);
            Panel_Play.Width = (int)(this.Width - Panel_Nav.Width - 2 * edgeX);
            mTrackBar.Width = (int)(this.Width - Panel_Nav.Width - 2 * edgeX);
            Panel_PlayStatus.Width = (int)(this.Width - Panel_Nav.Width - 2 * edgeX);
            Panel_Detail.Width = (int)(this.Width - Panel_Nav.Width - 2 * edgeX);
            Panel_Detail.Height = (int)(this.Height - Panel_Play.Height - 80 - 2 * edgeY);
            Panel_List.Height = (int)(Panel_Nav.Height - Panel_Icon.Height);
            //位置
            Panel_Play.Location = new Point(Panel_Play.Location.X, Panel_Detail.Location.Y + Panel_Detail.Height);
            Panel_Control.Location = new Point((Panel_PlayStatus.Width - Panel_Control.Width) / 2, 0);

        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if (this.Visible)
                {
                    this.Hide();
                }
                else
                {
                    this.Show();
                }
            }
        }
        
        private void 退出听听鸽ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
            notifyIcon.Dispose();
        }


        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Hide();
            SetVisibleCore(false);
        }

        private void Btn_MaxSize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                toolTip.SetToolTip(Btn_MaxSize, "还原");
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                toolTip.SetToolTip(Btn_MaxSize, "最大化");
            }
        }

        private void Btn_MinSize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Btn_Menu_Click(object sender, EventArgs e)
        {
            Point p = new Point(0, 0);
            p = Btn_Menu.PointToScreen(p);
            contextMenuStrip.Show(p.X - contextMenuStrip.Width / 2, p.Y + Btn_Menu.Height);
        }

        private void Btn_Playlist_Click(object sender, EventArgs e)
        {
            playListForm.MusicUpdate(mGroup);
            playListForm.BringToFront();
            playListForm.Show();
            playListForm.Width = 300;
            playListForm.Location = new Point(this.Width - 300, 0);
            /*
            for (int i = 0; i <= 150; i++)
            {
                playListForm.Width = i * 2;
                playListForm.Location = new Point(this.Width - i * 2, 0);
            }*/

        }

        private void Btn_Mode_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Volume_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                mTrackBar.M_Value = 0;
                Music music = InitMusic(openFileDialog.FileName);
                this.mGroup.Add(music);
                mDB.AddMusic(music);
            }
        }

        private Music InitMusic(string filename)
        {
            try
            {
                string[] str = filename.Split('\\');
                string fileurl = "";
                string name = "";
                string singer = "";
                foreach (string s in str)
                {
                    if (s != str.Last())
                    {
                        fileurl = fileurl + s + "\\";
                    }
                    if (s == str.Last())
                    {
                        fileurl += s;
                        string[] str_ = s.Split('-');
                        name = str_[1];
                        singer = str_[0];
                    }
                }
                Music music = new Music(name, singer, "", fileurl);                
                Console.WriteLine("成功解析音乐路径:"+name+" : "+singer+" : "+fileurl);
                return music;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message + "\n" + "解析音乐路径失败");
                return null;
            }
        }

        /// <summary>
        /// 是否在播放中
        /// </summary>
        private bool MediaStatus = false;

        private void Btn_Play_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MediaStatus)
                {
                    Btn_Play.Text = "||";
                    Console.WriteLine(musics[0].FileURL.ToString());
                    WMP.URL = "C:\\Users\\ASUS\\Music\\Laura Shigihara - Zombies On Your Lawn.mp3";
                    WMP.Ctlcontrols.play();

                    MediaStatus = true;
                }
                else
                {
                    Btn_Play.Text = "▶";
                    WMP.Ctlcontrols.stop();
                    MediaStatus = false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + "\n音乐播放失败");
            }
        }
        #endregion


    }

}
