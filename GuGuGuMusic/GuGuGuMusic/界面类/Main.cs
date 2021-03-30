using AxWMPLib;
using ControlDemos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

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
            //设置双缓冲减少屏幕闪烁
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            
            //添加第三方控件AxWindowsMediaPlayer，实例化
            ((System.ComponentModel.ISupportInitialize)(this.AWMP)).BeginInit();
            this.Controls.Add(AWMP);
            ((System.ComponentModel.ISupportInitialize)(this.AWMP)).EndInit();

            
            //设置默认音乐列表为本地与下载
            ChoosedMLButton = Btn_Local;

            //初始化音乐app(数据初始化
            myMusicApp = new MyMusicApp();

            //初始化界面的音乐列表，显示本地与下载列表中的音乐文件
            Btn_PopMusic.MusicList = myMusicApp.PopMusicList;
            Btn_History.MusicList = myMusicApp.HistoryMusicList;

            Btn_Local.MusicList = myMusicApp.LocalMusicList;
            Btn_Liked.MusicList = myMusicApp.LikedMusicList;           
            ShowList(myMusicApp.LocalMusicList.Musics);

            AllMusicList.Add(Btn_Liked.MusicList.ListName);

            //初始化自定义歌单
            InitCreatedList();

            //初始化播放器
            InitAWMP(myMusicApp.PlayingMusicList, myMusicApp.PlayingMusicList.StartIndex);
            AWMP.settings.setMode("loop", true);//设置为循环播放
            AWMP.settings.volume = 100;
        }

        /// <summary>
        /// 记录所有歌单
        /// </summary>
        public List<string> AllMusicList = new List<string>();

        /// <summary>
        /// 音乐app，所有对数据的操作、事务逻辑都由其实现
        /// </summary>
        public MyMusicApp myMusicApp;

        /// <summary>
        /// 独立的音乐播放控件，软件核心
        /// </summary>
        AxWindowsMediaPlayer AWMP = new AxWindowsMediaPlayer() { Visible = false };

        /// <summary>
        /// 当前选择播放的音乐 对应button
        /// </summary>
        private MButton ChoosedMButton = new MButton();

        /// <summary>
        /// 当前选中的音乐播放列表 对应button
        /// </summary>
        private MLButton ChoosedMLButton = new MLButton();

        /// <summary>
        /// 当前播放列表对应歌单名
        /// </summary>
        private string PlayingMusicListName = "播放列表";

        /// <summary>
        /// 添加本地音乐按钮Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_AddLocalMusic_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                mTrackBar_Music.M_Value = 0;
                Music music = new Music().InitMusic(openFileDialog.FileName);
                myMusicApp.LocalMusicList.Add(music);
                Btn_Local.MusicList = myMusicApp.LocalMusicList;

                myMusicApp.UpdateMusicList(myMusicApp.LocalMusicList);
                ShowList(myMusicApp.LocalMusicList.Musics);
            }
        }

        #region 窗体相关事件
        /// <summary>
        /// resize事件 实现窗体的缩放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            mTrackBar_Music.Width = (int)(this.Width - Panel_Nav.Width - 2 * edgeX);
            Panel_PlayStatus.Width = (int)(this.Width - Panel_Nav.Width - 2 * edgeX);
            Panel_Detail.Width = (int)(this.Width - Panel_Nav.Width - 2 * edgeX);
            Panel_Detail.Height = (int)(this.Height - Panel_Play.Height - 80 - 2 * edgeY);
            Panel_MenuList.Height = (int)(Panel_Nav.Height - Panel_Icon.Height);
            Panel_PlayList.Height = this.Height;
            Panel_MusicList.Height = Panel_Detail.Width - 80;
            Panel_MusicList.Width = Panel_Detail.Width - 60;
            
            //位置
            Panel_Play.Location = new Point(Panel_Play.Location.X, Panel_Detail.Location.Y + Panel_Detail.Height);
            Panel_Control.Location = new Point((Panel_PlayStatus.Width - Panel_Control.Width) / 2, 0);
            Panel_PlayList.Location = new Point(this.Width - Panel_PlayList.Width, 0);
            Panel_Mode.Location = new Point((this.Width - 908)/2 + 418, Panel_Mode.Location.Y);
            Panel_Volume.Location = new Point((this.Width - 908)/2 + 610, Panel_Volume.Location.Y);

            //刷新
            ShowList(ChoosedMLButton.MusicList.Musics);
        }

        /// <summary>
        /// 窗体关闭按钮Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Hide();
            SetVisibleCore(false);
        }

        /// <summary>
        /// 窗体最大化按钮Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 窗体最小化按钮Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_MinSize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// 菜单按钮Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Menu_Click(object sender, EventArgs e)
        {
            Point p = new Point(0, 0);
            p = Btn_Menu.PointToScreen(p);
            CMS_Main.Show(p.X - CMS_Main.Width / 2, p.Y + Btn_Menu.Height);
        }

        #endregion

       
        #region 通知栏图标相关事件
        
        /// <summary>
        /// 通知栏图标左键单机事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        #endregion


        #region 音乐播放相关

        /// <summary>
        /// 初始化AxWindowsMediaPlayer的播放列表
        /// </summary>
        /// <param name="musicList"></param>
        /// <param name="index"></param>
        private void InitAWMP(MusicList musicList, int index)
        {
            try
            {
                if (musicList != null && musicList.Musics != null)
                {
                    IWMPPlaylist playList = AWMP.playlistCollection.newPlaylist("MyPlayList");
                    IWMPMedia media = AWMP.newMedia(musicList.Musics[index].FileURL);
                    playList.appendItem(media);
                    for(int i = 0; i < musicList.Musics.Count; i++) 
                    {
                        if (i == index)
                        {
                            continue;
                        }
                        string url = musicList.Musics[i].FileURL.ToString();
                        media = AWMP.newMedia(url);
                        playList.appendItem(media);
                    }
                    AWMP.currentPlaylist = playList;
                    AWMP.Ctlcontrols.stop();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message+"初始化AWMP播放列表失败");
            }            
        }

        /// <summary>
        /// 播放状态设置事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Mode_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Panel_Mode.Visible)
                {
                    Timer_PlayingMode.Start();
                }
                else
                {
                    Timer_PlayingMode.Stop();
                }
                Panel_Mode.Visible = Panel_Mode.Visible == true ? false : true;                
            }catch(Exception ce)
            {
                Console.WriteLine(ce.Message);
            }
        }

        /// <summary>
        /// 音量调节事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Volume_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Panel_Volume.Visible)
                {
                    Timer_Volume.Start();
                }
                else
                {
                    Timer_Volume.Stop();
                }
                mTrackBar_Volume.Enabled = mTrackBar_Volume.Enabled ? false : true;
                if (mTrackBar_Volume.Enabled) { mTrackBar_Volume.Focus(); }
                Panel_Volume.Visible = Panel_Volume.Visible == true ? false : true;
            }
            catch(Exception ce)
            {
                Console.WriteLine(ce.Message);
            }
        }

        /// <summary>
        /// 音量滑块获取鼠标焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mTrackBar_Volume_MouseEnter(object sender,EventArgs e)
        {
            try
            {
                mTrackBar_Volume.Focus();
            }
            catch(Exception ce)
            {
                Console.WriteLine(ce.Message);
            }
        }
        /// <summary>
        /// 鼠标滚轮控制调节音量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mTrackBar_Volume_MouseWheel(object sender,MouseEventArgs e)
        {
            try
            {
                if (mTrackBar_Volume.Enabled)
                {
                    mTrackBar_Volume.M_Value += e.Delta / 120;
                }
            }catch(Exception ce)
            {
                Console.WriteLine(ce.Message);
            }
        }

        /// <summary>
        /// 播放按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Play_Click(object sender, EventArgs e)
        {
            try
            {
                if (Btn_Play.Text == "▶")
                {
                    if (AWMP.currentPlaylist!=null) 
                    {
                        AWMP.Ctlcontrols.play();
                        Timer_Music.Start();
                    }
                }
                else
                {
                    if(AWMP.currentPlaylist != null)
                    {
                        AWMP.Ctlcontrols.pause();
                    }
                }
                Btn_Play.Text = Btn_Play.Text == "||" ? "▶" : "||";
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + "\n音乐播放失败");
            }
        }

        /// <summary>
        /// 点击播放下一首
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Next_Click(object sender, EventArgs e)
        {
            try
            {
                AWMP.Ctlcontrols.stop();
                if (AWMP.currentPlaylist != null)
                {
                    AWMP.Ctlcontrols.next();
                    Btn_Play.Text ="||";
                    AWMP.Ctlcontrols.play();
                    Timer_Music.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n音乐播放失败");
            }
        }

        /// <summary>
        /// 点击播放上一首
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Last_Click(object sender, EventArgs e)
        {
            try
            {
                AWMP.Ctlcontrols.stop();
                if (AWMP.currentPlaylist != null)
                {
                    AWMP.Ctlcontrols.previous();
                    Btn_Play.Text = "||";
                    AWMP.Ctlcontrols.play();
                    Timer_Music.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n音乐播放失败");
            }
        }

        /// <summary>
        /// 利用计时器让进度条跟随音乐播放进度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Music_Tick(object sender, EventArgs e)
        {
            try
            {
                double currentPosition = AWMP.Ctlcontrols.currentPosition;
                double duration = AWMP.currentMedia.duration;
                double value;
                if(duration <= 0)
                {
                    value = 0;
                }
                else
                {
                    value = mTrackBar_Music.M_Maximum * currentPosition / duration;
                }                
                if (value < 0) { mTrackBar_Music.M_Value = 0; }
                else { mTrackBar_Music.M_Value = value; }
            }
            catch (Exception ce)
            {
                Console.WriteLine(ce.Message);
            }
        }

        private void mTrackBar_Music_MValueChanged(object sender, MEventArgs e)
        {
            try
            {
                double duration = AWMP.currentMedia.duration;
                double currentPosition = (mTrackBar_Music.M_Value / mTrackBar_Music.M_Maximum) * duration;
                //用数字实时显示播放进度
                string max_minute = (int)(duration / 60) >= 10 ? ((int)(duration / 60)).ToString() : "0" + ((int)(duration / 60)).ToString();
                string max_second = (int)(duration % 60) >= 10 ? ((int)(duration % 60)).ToString() : "0" + ((int)(duration % 60)).ToString();
                string cur_minute = (int)(currentPosition / 60) >= 10 ? ((int)(currentPosition / 60)).ToString() : "0" + ((int)(currentPosition / 60)).ToString();
                string cur_second = (int)(currentPosition % 60) >= 10 ? ((int)(currentPosition % 60)).ToString() : "0" + ((int)(currentPosition % 60)).ToString();
                if (cur_minute.Length > 2) { cur_minute = "00"; }
                if (cur_second.Length > 2) { cur_second = "00"; }
                Btn_Process.Text = cur_minute + ":" + cur_second + " / " + max_minute + ":" + max_second;

            }
            catch (Exception ce)
            {
                Console.WriteLine(ce.Message);
            }

        }

        /// <summary>
        /// 手动控制进度时关闭计时器控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mTrackBar_MouseDown(object sender, EventArgs e)
        {
            Timer_Music.Stop();
        }

        /// <summary>
        /// 松开后从指定进度开始播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mTrackBar_MouseUp(object sender, EventArgs e)
        {
            try
            {
                double newValue = mTrackBar_Music.M_Value / mTrackBar_Music.M_Maximum * AWMP.currentMedia.duration;
                //播放中
                if (AWMP.playState == WMPPlayState.wmppsPlaying)
                {
                    AWMP.Ctlcontrols.currentPosition = newValue;//为播放控件赋予新进度
                    AWMP.Ctlcontrols.play();
                    Timer_Music.Start();
                }
                //其他情况下
                else
                {
                    AWMP.Ctlcontrols.currentPosition = newValue;
                }
            }
            catch(Exception ce)
            {
                Console.WriteLine(ce.Message);
            }
        }

        /// <summary>
        /// 单曲循环
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_SingleLoop_Click(object sender, EventArgs e)
        {
            try
            {
                Panel_Mode.Hide();
                //清空播放列表其他音乐
                int count = AWMP.currentPlaylist.count;
                int index = 0;
                for(int i =0;i< count; i++)
                {
                    if (AWMP.currentMedia.sourceURL == AWMP.currentPlaylist.Item[i].sourceURL)
                    {
                        index = i;
                    }
                }
                AWMP.currentPlaylist.moveItem(index, 0);
                for(int i = 0; i < count-1; i++)
                {
                    AWMP.currentPlaylist.removeItem(AWMP.currentPlaylist.Item[AWMP.currentPlaylist.count - 1]);
                }

                Btn_Mode.Text = Btn_SingleLoop.Text;
            }
            catch (Exception ce)
            {
                Console.WriteLine(ce.Message + "单曲循环失败");
            }
        }

        /// <summary>
        /// 默认设置列表循环
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Loop_Click(object sender, EventArgs e)
        {
            try
            {
                Panel_Mode.Hide();
                //清空播放列表其他音乐
                int count = AWMP.currentPlaylist.count;
                int index = 0;
                for (int i = 0; i < count; i++)
                {
                    if (AWMP.currentMedia.sourceURL == AWMP.currentPlaylist.Item[i].sourceURL)
                    {
                        index = i;
                    }
                }
                AWMP.currentPlaylist.moveItem(index, 0);
                for (int i = 0; i < count - 1; i++)
                {
                    AWMP.currentPlaylist.removeItem(AWMP.currentPlaylist.Item[AWMP.currentPlaylist.count - 1]);
                }
                //加载新播放列表
                bool locate = false;
                foreach(Music music in myMusicApp.PlayingMusicList)
                {
                    if (music.FileURL.ToString() == AWMP.currentMedia.sourceURL.ToString())
                    {
                        locate = true;
                        continue;
                    }
                    IWMPMedia media = AWMP.newMedia(music.FileURL);
                    if (locate)
                    {
                        AWMP.currentPlaylist.appendItem(media);
                    }
                    else
                    {
                        AWMP.currentPlaylist.insertItem(AWMP.currentPlaylist.count - 1, media);
                    }
                }

                Btn_Mode.Text = Btn_Loop.Text;
            }
            catch (Exception ce)
            {
                Console.WriteLine(ce.Message + "列表循环失败");
            }
        }

        /// <summary>
        /// 列表循环
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Sequential_Click(object sender, EventArgs e)
        {
            try
            {
                Panel_Mode.Hide();
                //清空播放列表其他音乐
                int count = AWMP.currentPlaylist.count;
                int index = 0;
                for (int i = 0; i < count; i++)
                {
                    if (AWMP.currentMedia.sourceURL == AWMP.currentPlaylist.Item[i].sourceURL)
                    {
                        index = i;
                    }
                }
                AWMP.currentPlaylist.moveItem(index, 0);
                for (int i = 0; i < count - 1; i++)
                {
                    AWMP.currentPlaylist.removeItem(AWMP.currentPlaylist.Item[AWMP.currentPlaylist.count - 1]);
                }
                //加载新播放列表
                bool locate = false;
                foreach (Music music in myMusicApp.PlayingMusicList)
                {
                    if (music.FileURL.ToString() == AWMP.currentMedia.sourceURL.ToString())
                    {
                        locate = true;
                        continue;
                    }
                    IWMPMedia media = AWMP.newMedia(music.FileURL);
                    if (locate)
                    {
                        AWMP.currentPlaylist.appendItem(media);
                    }
                    else
                    {
                        AWMP.currentPlaylist.insertItem(AWMP.currentPlaylist.count - 1, media);
                    }
                }

                Btn_Mode.Text = Btn_Sequential.Text;
            }
            catch (Exception ce)
            {
                Console.WriteLine(ce.Message + "顺序播放失败");
            }
        }

        /// <summary>
        /// 随机播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Random_Click(object sender, EventArgs e)
        {
            try
            {
                Panel_Mode.Hide();
                //清空播放列表其他音乐
                int count = AWMP.currentPlaylist.count;
                int index = 0;
                for (int i = 0; i < count; i++)
                {
                    if (AWMP.currentMedia.sourceURL == AWMP.currentPlaylist.Item[i].sourceURL)
                    {
                        index = i;
                    }
                }
                AWMP.currentPlaylist.moveItem(index, 0);
                for (int i = 0; i < count - 1; i++)
                {
                    AWMP.currentPlaylist.removeItem(AWMP.currentPlaylist.Item[AWMP.currentPlaylist.count - 1]);
                }
                //加载新播放列表
                count = myMusicApp.PlayingMusicList.Count;
                Random rd = new Random();
                foreach (Music music in myMusicApp.PlayingMusicList)
                {
                    if (music.FileURL.ToString() == AWMP.currentMedia.sourceURL.ToString())
                    {
                        continue;
                    }
                    IWMPMedia media = AWMP.newMedia(music.FileURL);
                    AWMP.currentPlaylist.insertItem(rd.Next(0, AWMP.currentPlaylist.count), media);
                }
                Btn_Mode.Text = Btn_Random.Text;
            }
            catch (Exception ce)
            {
                Console.WriteLine(ce.Message + "随机播放失败");
            }
        }

        /// <summary>
        /// 实时调节音量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mTrackBar_Volume_ValueChanged(object sender, MEventArgs e)
        {
            try
            {
                int volume = (int)(100 * mTrackBar_Volume.M_Value / mTrackBar_Volume.M_Maximum);
                Lbl_Volume.Text = volume.ToString() + "%";
                AWMP.settings.volume = volume;
            }
            catch (Exception ce)
            {
                Console.WriteLine(ce.Message);
            }
        }

        #endregion


        #region 实例化音乐列表
        /// <summary>
        /// 在主页显示选择的音乐列表，批量实例化音乐按钮
        /// </summary>
        /// <param name="Musics"></param>
        private void ShowList(List<Music> Musics)
        {
            try
            {
                Panel_MusicList.Controls.Clear();
                int i = 0;
                if(Musics != null)
                {
                    foreach (Music music in Musics)
                    {
                        MButton b = new MButton()
                        {
                            FlatStyle = FlatStyle.Flat,
                            Size = new System.Drawing.Size(Panel_MusicList.Width, 30),
                            TabStop = false,
                            BackColor = Color.Transparent,
                            M_music = music,
                            Index = i,
                        };
                        b.DoubleClick += new EventHandler(PlayChoosedMusic);
                        b.Location = new Point(0, b.Height * i);
                        b.Text = music.Name;
                        b.FlatAppearance.BorderSize = 0;
                        Panel_MusicList.Controls.Add(b);
                        b.Show();
                        i++;
                    }
                }

            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// 按钮默认绑定事件，双击音乐列表的一项音乐时播放该音乐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayChoosedMusic(object sender, EventArgs e)
        {
            try
            {
                MButton mButton = (MButton)sender;
                if (PlayingMusicListName != ChoosedMLButton.MusicList.ListName)
                {
                    myMusicApp.PlayingMusicList.Musics = ChoosedMLButton.MusicList.Musics;
                    myMusicApp.UpdateMusicList(myMusicApp.PlayingMusicList);
                    PlayingMusicListName = ChoosedMLButton.MusicList.ListName;
                }
                InitAWMP(myMusicApp.PlayingMusicList, mButton.Index);
                AWMP.Ctlcontrols.play();
                Timer_Music.Start();
                Btn_Play.Text = "||";
            }
            catch(Exception ce)
            {
                Console.WriteLine(ce.Message+"音乐播放失败");
            }
        }
        #endregion


        #region 新建歌单相关事件

        /// <summary>
        /// 正在创建的列表
        /// </summary>
        private MLButton CreatingBtn = new MLButton();
        /// <summary>
        /// 正在创建的列表名
        /// </summary>
        private string MLB_Name = "";

        /// <summary>
        /// 创建歌单按钮Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
                    ContextMenuStrip = CMS自定义歌单,
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
                    Location = new Point(20, 0),
                    Font = new System.Drawing.Font("微軟正黑體 Light", 11F),
                    Size = Size = new Size(160, 30),
                    TextAlign = HorizontalAlignment.Center,
                };
                
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

        /// <summary>
        /// 新建歌单默认绑定事件，检测鼠标的悬停并改变按钮显示样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreatedList_OnMouseHover(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.ForeColor = System.Drawing.SystemColors.ControlText;
        }

        /// <summary>
        /// 鼠标停靠时改变字体颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreatedList_OnMouseLeave(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.ForeColor = System.Drawing.SystemColors.ButtonShadow;
        }

        /// <summary>
        /// 当命名框失去焦点时，生成按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreatedListBtnNamed_LostFocus(object sender,EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            MLB_Name = "            "+textBox.Text.ToString();
            CreatingBtn.Text = MLB_Name;
            CreatingBtn.Enabled = true;
            Panel_CreatedList.Controls.Remove(textBox);
            textBox.Dispose();
            Timer_CreatingList.Dispose();
        }


        /// <summary>
        /// 歌单默认点击事件，点击显示歌单详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreatedListBtn_Click(object sender, EventArgs e)
        {
            ResetChoosedMLButton(sender);
            MLButton button = (MLButton)sender;
            ShowList(button.MusicList.Musics);
        }

        /// <summary>
        /// 用于在创建歌单时，检测用户命名是否结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_CreatingList_Tick(object sender, EventArgs e)
        {
            if (MouseButtons == MouseButtons.Left || MouseButtons == MouseButtons.Right)
            {
                if (!InBox(new Size(160,30),new Point(20 +this.Left + Main_Panel.Left + Panel_MenuList.Left + Panel_CreatedList.Left + Panel_Nav.Left, 0 + this.Top + Main_Panel.Top + Panel_MenuList.Top + Panel_CreatedList.Top + Panel_Nav.Top)))
                {
                    Button button = (Button)ChoosedMLButton;
                    button.Focus();
                }
            }
        }

        /// <summary>
        /// 检测鼠标是否在指定区域
        /// </summary>
        /// <param name="size">区域大小</param>
        /// <param name="point">区域坐标相对于屏幕</param>
        /// <returns></returns>
        private bool InBox(Size size,Point point)
        {
            int x = MousePosition.X;
            int y = MousePosition.Y;
            if (x < point.X || x > point.X+size.Width || y < point.Y || y > point.Y+size.Height) 
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 展开自建歌单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        #endregion


        #region 默认歌单相关事件

        /// <summary>
        /// 用于定位左侧菜单栏 当前选择的音乐列表
        /// </summary>
        /// <param name="sender"></param>
        private void ResetChoosedMLButton(object sender)//定位当前列表
        {
            Button newChoosedMLButton = (Button)sender;
            newChoosedMLButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            Button oldChoosedMLButton = (Button)ChoosedMLButton;
            oldChoosedMLButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            ChoosedMLButton = (MLButton)sender;
        }


        /// <summary>
        /// 单击左键显示本地与下载列表歌单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Local_Click(object sender, EventArgs e)
        {
            ResetChoosedMLButton(sender);
            ShowList(myMusicApp.LocalMusicList.Musics);
        }

        /// <summary>
        /// 单击左键显示历史播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_History_Click(object sender, EventArgs e)
        {
            ResetChoosedMLButton(sender);
            ShowList(myMusicApp.HistoryMusicList.Musics);
        }

        /// <summary>
        /// 单击左键显示流行音乐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_PopMusic_Click(object sender, EventArgs e)
        {
            ResetChoosedMLButton(sender);
            ShowList(myMusicApp.PopMusicList.Musics);
        }

        /// <summary>
        ///  单击左键显示我喜欢音乐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Liked_Click(object sender, EventArgs e)
        {
            ResetChoosedMLButton(sender);
            ShowList(myMusicApp.LikedMusicList.Musics);
        }

        #endregion


        #region 当前播放列表的呈现和收起
        /// <summary>
        /// 右下角展开播放列表按钮Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Playlist_Click(object sender, EventArgs e)
        {
            try
            {
                Lbl_Number.Text = "  " + myMusicApp.PlayingMusicList.Count + "首歌曲";
                PlayListUpdate(myMusicApp.PlayingMusicList);
                Panel_PlayList.BringToFront();
                Panel_PlayList.Show();
                Panel_PlayList.Width = 300;
                Panel_PlayList.Location = new Point(this.Width - 300, 0);
                Timer_ClosePlayList.Start();
            }
            catch (Exception ce)
            {
                Console.WriteLine(ce.Message);
            }
        }

        /// <summary>
        /// 关闭播放列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Shut_Click(object sender, EventArgs e)
        {
            Panel_PlayList.Width = 0;
            Panel_PlayList.Hide();
        }

        /// <summary>
        /// 显示播放列表，实例化播放列表中的音乐按钮
        /// </summary>
        /// <param name="PlayingMusicList"></param>
        private void PlayListUpdate(MusicList PlayingMusicList)
        {
            try
            {
                Panel_PlayingMusicLIst.Controls.Clear();
                if (PlayingMusicList != null && PlayingMusicList.Musics != null)
                {
                    int i = 0;
                    foreach (Music music in PlayingMusicList.Musics)
                    {
                        MButton b = new MButton()
                        {
                            FlatStyle = FlatStyle.Flat,
                            Size = new System.Drawing.Size(300, 64),
                            TabStop = false,
                            BackColor = Color.White,
                            M_music = music,
                            Index = i,
                        };
                        b.DoubleClick += new EventHandler(PlayChoosedMusic2);
                        b.Location = new Point(0, 64 * i);
                        b.Text = music.Name;
                        b.FlatAppearance.BorderSize = 0;
                        Panel_PlayingMusicLIst.Controls.Add(b);
                        b.Show();
                        i++;
                    }
                }
                Invalidate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// 点击播放列表中的音乐播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayChoosedMusic2(object sender, EventArgs e)
        {
            try
            {
                MButton mButton = (MButton)sender;
                InitAWMP(myMusicApp.PlayingMusicList, mButton.Index);
                AWMP.Ctlcontrols.play();
                Timer_Music.Start();
                Btn_Play.Text = "||";
            }
            catch (Exception ce)
            {
                Console.WriteLine(ce.Message + "音乐播放失败");
            }
        }



        #endregion

        private void InitCreatedList()
        {
            try
            {

            }catch(Exception ce)
            {
                Console.WriteLine(ce.Message + ":初始化自定义歌单失败");
            }
        }

        private void 播放ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (PlayingMusicListName != ChoosedMLButton.MusicList.ListName)
                {
                    myMusicApp.PlayingMusicList.Musics = ChoosedMLButton.MusicList.Musics;
                    myMusicApp.UpdateMusicList(myMusicApp.PlayingMusicList);
                    PlayingMusicListName = ChoosedMLButton.MusicList.ListName;
                }
                InitAWMP(myMusicApp.PlayingMusicList, myMusicApp.PlayingMusicList.StartIndex);
                AWMP.Ctlcontrols.play();
                Timer_Music.Start();
                Btn_Play.Text = "||";
            }catch(Exception ce)
            {
                Console.WriteLine(ce.Message);
            }

        }

        private void CMS默认歌单_Opening(object sender, CancelEventArgs e)
        {
            MLButton mLButton = (MLButton)((ContextMenuStrip)sender).SourceControl;
            ResetChoosedMLButton(mLButton);
            ShowList(mLButton.MusicList.Musics);
        }

        private void 删除_Click(object sender, EventArgs e)
        {
            try
            {

            }catch(Exception ce)
            {
                Console.WriteLine(ce.Message + ":删除歌单失败");
            }
        }

        private void 重命名_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch(Exception ce)
            {
                Console.WriteLine(ce.Message + ":重命名歌单失败");
            }
        }

        private void Timer_ClosePlayList_Tick(object sender, EventArgs e)
        {
            try
            {
                if (MouseButtons == MouseButtons.Left || MouseButtons == MouseButtons.Right)
                {
                    if (!InBox(new Size(300, 648), new Point(this.Width - 300 + this.Left, 0 + this.Top)))
                    {
                        Panel_PlayList.Width = 0;
                        Panel_PlayList.Hide();
                        Timer_ClosePlayList.Stop();
                    }
                }
            }
            catch (Exception ce)
            {
                Console.WriteLine(ce.Message+":关闭播放列表失败");
            }
        }

        private void Timer_PlayingMode_Tick(object sender, EventArgs e)
        {
            try
            {
                if (MouseButtons == MouseButtons.Left || MouseButtons == MouseButtons.Right)
                {
                    if (!InBox(new Size(93, 145), new Point(Panel_Mode.Location.X + this.Left, Panel_Mode.Location.Y + this.Top)))
                    {
                        Panel_Mode.Visible = false;
                        Timer_PlayingMode.Stop();
                    }
                }
                
            }
            catch (Exception ce)
            {
                Console.WriteLine(ce.Message);
            }
        }

        private void Timer_Volume_Tick(object sender, EventArgs e)
        {
            try
            {
                if (MouseButtons == MouseButtons.Left || MouseButtons == MouseButtons.Right)
                {
                    if (!InBox(new Size(65, 190), new Point(Panel_Volume.Location.X + this.Left, Panel_Mode.Location.Y + this.Top)))
                    {
                        mTrackBar_Volume.Enabled = false;
                        Panel_Volume.Visible = false;
                        Timer_Volume.Stop();
                    }
                }

            }
            catch (Exception ce)
            {
                Console.WriteLine(ce.Message);
            }
        }
    }

}
