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
            //添加程序在屏幕上的拖动事件
            this.Panel_Tool.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            this.Panel_Play.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            this.Panel_Icon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            this.Main_Panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_MouseDown);
            //myClient为第三方控件名
            ((System.ComponentModel.ISupportInitialize)(this.WMP)).BeginInit();
            this.Controls.Add(WMP);
            ((System.ComponentModel.ISupportInitialize)(this.WMP)).EndInit();
            //
            this.Btn_Local.BackColor = System.Drawing.SystemColors.ButtonShadow;
            ChoosedListButton = Btn_Local;
            //

            //读取各项列表信息
            PlayingMusicGroup.musics = mDB.GetMusics();
            LocalMusicGroup.musics = mDB.GetMusics();
            //
            ReadyMusic = PlayingMusicGroup.musics.First();
            //显示默认列表
            ShowList(LocalMusicGroup.musics);
        }
        private MDB mDB = new MDB();//连接数据库
        AxWindowsMediaPlayer WMP = new AxWindowsMediaPlayer() { Visible = false };//音乐播放控件


        /// <summary>
        /// 当前播放列表
        /// </summary>
        private MGroup PlayingMusicGroup = new MGroup();
        /// <summary>
        /// 本地和下载列表
        /// </summary>
        private MGroup LocalMusicGroup = new MGroup();
        /// <summary>
        /// 历史播放列表
        /// </summary>
        private MGroup HistoryMusicGroup = new MGroup();
        /// <summary>
        /// 流行音乐列表
        /// </summary>
        private MGroup PopMusicGroup = new MGroup();
        /// <summary>
        /// 创建的列表 
        /// </summary>
        private MGroup[] CreatedListMusicGroup = new MGroup[100];
        /// <summary>
        /// 正在播放的音乐
        /// </summary>
        private Music ReadyMusic = new Music();
        /// <summary>
        /// 当前选择播放的音乐 对应button
        /// </summary>
        private MButton ChoosedMButton = new MButton();
        /// <summary>
        /// 当前选中的音乐播放列表 对应button
        /// </summary>
        private object ChoosedListButton = new object();
        /// <summary>
        /// 是否在播放中
        /// </summary>
        private bool MediaStatus = false;


        #region 事件
        //resize事件 实现缩放
        private void Main_Resize(object sender, EventArgs e)
        {
            Panel_PlayList.Width = 0;
            Panel_PlayList.Hide();
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
            Panel_MenuList.Height = (int)(Panel_Nav.Height - Panel_Icon.Height);
            Panel_PlayList.Height = this.Height;
            
            //位置
            Panel_Play.Location = new Point(Panel_Play.Location.X, Panel_Detail.Location.Y + Panel_Detail.Height);
            Panel_Control.Location = new Point((Panel_PlayStatus.Width - Panel_Control.Width) / 2, 0);
            Panel_PlayList.Location = new Point(this.Width - Panel_PlayList.Width, 0);

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
            try
            {
                PlayListUpdate(PlayingMusicGroup);
                Panel_PlayList.BringToFront();
                Panel_PlayList.Show();
                Panel_PlayList.Width = 300;
                Panel_PlayList.Location = new Point(this.Width - 300, 0);
                
                /*
                for (int i = 0; i <= 150; i++)
                {
                    Panel_PlayList.Width = i * 2;
                    Panel_PlayList.Location = new Point(this.Width - i * 2, 0);
                }*/
            }
            catch (Exception ce)
            {
                Console.WriteLine(ce.Message);
            }

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
                this.PlayingMusicGroup.Add(music);
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

        private void Btn_Play_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MediaStatus)
                {
                    Btn_Play.Text = "||";
                    Console.WriteLine(ReadyMusic.Name);
                    WMP.URL = ReadyMusic.FileURL.ToString();
                    WMP.currentMedia.name = ReadyMusic.Name + " - " + ReadyMusic.Singer;
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

        private void PlayListUpdate(MGroup PlayingMusicGroup)
        {
            try
            {
                Panel_PlayingMusciGroup.Controls.Clear();
                if (PlayingMusicGroup != null && PlayingMusicGroup.musics != null)
                {
                    int i = 0;
                    foreach (Music music in PlayingMusicGroup.musics)
                    {
                        MButton b = new MButton()
                        {
                            FlatStyle = FlatStyle.Flat,
                            Size = new System.Drawing.Size(300, 64),
                            TabStop = false,
                            BackColor = Color.White,
                            M_music = music
                        };
                        b.DoubleClick += new EventHandler(PlayChoosedMusic);
                        b.Location = new Point(0, 64 * i);
                        b.Text = music.Name;
                        b.FlatAppearance.BorderSize = 0;
                        Panel_PlayingMusciGroup.Controls.Add(b);
                        b.Show();
                        i++;
                    }
                }
                Invalidate();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void ResetChoosedListButton(object sender)//定位当前列表
        {
            Button newChoosedListButton = (Button)sender;
            newChoosedListButton.BackColor = System.Drawing.SystemColors.ButtonShadow;
            Button oldChoosedListButton = (Button)ChoosedListButton;
            oldChoosedListButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            ChoosedListButton = sender;
        }

        private void ShowList(List<Music> musics)
        {
            try
            {
                panel1.Controls.Clear();
                int i = 0;
                if(musics != null)
                {
                    foreach (Music music in musics)
                    {
                        MButton b = new MButton()
                        {
                            FlatStyle = FlatStyle.Flat,
                            Size = new System.Drawing.Size(panel1.Width, 64),
                            TabStop = false,
                            BackColor = Color.Transparent,
                            M_music = music
                        
                        };
                        b.DoubleClick += new EventHandler(PlayChoosedMusic);
                        b.Location = new Point(0, 64 * i);
                        b.Text = music.Name;
                        b.FlatAppearance.BorderSize = 0;
                        panel1.Controls.Add(b);
                        b.Show();
                        i++;
                    }
                }

            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void PlayChoosedMusic(object sender, EventArgs e)
        {
            Btn_Play.Text = "||";
            Console.WriteLine("测试");
            MButton MB = (MButton)sender;

            MB.BackColor = System.Drawing.SystemColors.ButtonShadow;
            ChoosedMButton.BackColor = Color.Transparent;
            ChoosedMButton = MB;

            ReadyMusic = MB.M_music;
            MediaStatus = false;
            Console.WriteLine(ReadyMusic.Name);
            WMP.URL = ReadyMusic.FileURL.ToString();
            WMP.currentMedia.name = ReadyMusic.Name + " - " + ReadyMusic.Singer;
            WMP.Ctlcontrols.play();
            MediaStatus = true;
        }


        private void CreatedList_OnMouseHover(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.ForeColor = System.Drawing.SystemColors.ControlText;
        }


        private void CreatedList_OnMouseLeave(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.ForeColor = System.Drawing.SystemColors.ButtonShadow;
        }

        private void Btn_Spread_Click(object sender, EventArgs e)
        {
            if (Btn_Spread.Text == "↑")
            {
                Btn_Spread.Text = "↓";
                Panel_CreatedList.Show();
            }
            else
            {
                Btn_Spread.Text = "↑";
                Panel_CreatedList.Hide();
            }
        }

        /// <summary>
        /// 正在创建的列表
        /// </summary>
        private MLButton CreatingBtn = new MLButton();
        private string MLB_Name = "";

        private void Btn_CteateList_Click(object sender, EventArgs e)
        {
            try
            {
                MLButton button = new MLButton()
                {
                    FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                    Font = new System.Drawing.Font("微軟正黑體 Light", 10F),
                    Margin = new System.Windows.Forms.Padding(0),
                    Size = new Size(160, 30),
                    Name = "            ",
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                };
                button.Click += new EventHandler(CreatedListBtn_Click);
                button.FlatAppearance.BorderSize = 0;
                int number = 0;
                foreach (Control control in Panel_CreatedList.Controls)
                {
                    number += 1;
                    control.Location = new Point(20, control.Location.Y + button.Height);
                }
                button.Location = new Point(20, 0);
                button.Enabled = false;
                CreatingBtn = button;
                Panel_CreatedList.Controls.Add(button);
                button.Show();
                TextBox textBox = new TextBox()
                {
                    Text = "新建歌单" + number,
                    Location = new Point(20, 0)
                };
                MLB_Name = textBox.Text.ToString();
                textBox.LostFocus += new EventHandler(CreatedListBtnNamed_LostFocus);
                Panel_CreatedList.Controls.Add(textBox);
                textBox.Show();
                textBox.BringToFront();
                textBox.Focus();
                Timer_CreatingList.Start();
            }catch(Exception ce)
            {
                Console.WriteLine(ce.Message);
            }

        }

        private void CreatedListBtnNamed_LostFocus(object sender,EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            MLB_Name = textBox.Text.ToString();
            CreatingBtn.Text = CreatingBtn.Name.ToString() + MLB_Name;
            CreatingBtn.Enabled = true;
            Panel_CreatedList.Controls.Remove(textBox);
            textBox.Dispose();
            Timer_CreatingList.Dispose();
        }

        private void CreatedListBtn_Click(object sender, EventArgs e)
        {
            ResetChoosedListButton(sender);
            MLButton button = (MLButton)sender;
            ShowList(button.M_mGroup.musics);
        }

        private void Timer_CreatingList_Tick(object sender, EventArgs e)
        {
            if (MouseButtons == MouseButtons.Left || MouseButtons == MouseButtons.Right)
            {
                Button button = (Button)ChoosedListButton;
                button.Focus();
            }
        }

        private void Btn_Local_Click(object sender, EventArgs e)
        {
            ResetChoosedListButton(sender);
            ShowList(LocalMusicGroup.musics);

        }

        private void Btn_History_Click(object sender, EventArgs e)
        {
            ResetChoosedListButton(sender);
            ShowList(HistoryMusicGroup.musics);
        }

        private void Btn_PopMusic_Click(object sender, EventArgs e)
        {
            ResetChoosedListButton(sender);
            ShowList(PopMusicGroup.musics);
        }

        private void Btn_Shut_Click(object sender, EventArgs e)
        {
            Panel_PlayList.Width = 0;
            Panel_PlayList.Hide();
        }

        private void Btn_Next_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Previus_Click(object sender, EventArgs e)
        {

        }
    }

}
