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

/*
 * 2
 */
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

        private bool m_aeroEnabled = true;                     // variables for box shadow
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
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
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
        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        private const int edgeX = 4;//4px的间距来实现缩放
        private const int edgeY = 4;
        #endregion

        public Main()
        {
            InitializeComponent();
            //设置双缓冲减少屏幕闪烁
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            //添加第三方控件AxWindowsMediaPlayer，实例化
            ((System.ComponentModel.ISupportInitialize)(this.AWMP)).BeginInit();
            this.Controls.Add(AWMP);
            ((System.ComponentModel.ISupportInitialize)(this.AWMP)).EndInit();

            //初始化音乐app(数据初始化
            myMusicApp = new MyMusicApp();

            //设置默认音乐列表为本地与下载
            ChoosedMLButton = Btn_Local;

            //初始化界面的音乐列表，显示本地与下载列表中的音乐文件
            Btn_PopMusic.MusicList = myMusicApp.PopMusicList;
            Btn_History.MusicList = myMusicApp.HistoryMusicList;
            Btn_Local.MusicList = myMusicApp.LocalMusicList;
            Btn_Liked.MusicList = myMusicApp.LikedMusicList;           
            ShowList(Btn_Local.MusicList.Musics);

            //初始化播放器
            InitAWMP(myMusicApp.PlayingMusicList, myMusicApp.PlayingMusicList.StartIndex);
            AWMP.settings.setMode("loop", true);//设置为循环播放
            AWMP.settings.volume = 100;

            base.Opacity = 0;
            Timer_Loading.Start();
        }

        /// <summary>
        /// 自定义歌单自动命名
        /// </summary>
        public Dictionary<string, int> AutoName = new Dictionary<string, int>();

        public enum OperateList
        {
            Create,
            Delete,
            Rename,
        }
        public OperateList Operation = OperateList.Create;

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
            try
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
            catch (Exception ce)
            {
                Console.WriteLine("2001:" + ce.Message);
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
            Btn_Shut.Location = new Point(Btn_Shut.Location.X, Panel_PlayList.Height - 68);
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
            try
            {
                this.Hide();
                SetVisibleCore(false);
            }
            catch (Exception ce)
            {
                Console.WriteLine("2002:" + ce.Message);
            }
        }

        /// <summary>
        /// 窗体最大化按钮Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_MaxSize_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ce)
            {
                Console.WriteLine("2003:" + ce.Message);
            }

        }

        /// <summary>
        /// 窗体最小化按钮Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_MinSize_Click(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Minimized;
            }
            catch (Exception ce)
            {
                Console.WriteLine("2004:" + ce.Message);
            }
            
        }

        /// <summary>
        /// 菜单按钮Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Menu_Click(object sender, EventArgs e)
        {
            try
            {
                Point p = new Point(0, 0);
                p = Btn_Menu.PointToScreen(p);
                CMS_Main.Show(p.X - CMS_Main.Width / 2, p.Y + Btn_Menu.Height);
            }
            catch (Exception ce)
            {
                Console.WriteLine("2005:" + ce.Message);
            }

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
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (this.Visible)
                    {
                        this.Hide();
                    }
                    else
                    {
                        this.BringToFront();
                        this.Show();
                    }
                }
            }
            catch (Exception ce)
            {
                Console.WriteLine("2006:" + ce.Message);
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
                if (musicList != null && musicList.Musics.Count() != 0)
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
                Console.WriteLine("2007:"+e.Message);
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
                Console.WriteLine("2008:"+ce.Message);
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
                Console.WriteLine("2009:"+ce.Message);
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
                Console.WriteLine("2010:"+ce.Message);
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
                Console.WriteLine("2011:"+ce.Message);
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
                Console.WriteLine("2012:"+ex.Message);
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
                Console.WriteLine("2013:"+ex.Message);
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
                Console.WriteLine("2014:"+ex.Message);
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
                Console.WriteLine("2058:"+ce.Message);
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
                Console.WriteLine("2015:"+ce.Message);
            }

        }

        /// <summary>
        /// 手动控制进度时关闭计时器控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mTrackBar_MouseDown(object sender, EventArgs e)
        {
            try
            {
                Timer_Music.Stop();
            }
            catch (Exception ce)
            {
                Console.WriteLine("2016:" + ce.Message);
            }
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
                Console.WriteLine("2017:"+ce.Message);
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
                Console.WriteLine("2018:"+ce.Message);
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
                Console.WriteLine("2019:"+ce.Message);
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
                Console.WriteLine("2020:"+ce.Message);
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
                Console.WriteLine("2021:"+ce.Message);
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
                Console.WriteLine("2022:"+ce.Message);
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
                if(Musics.Count() != 0)
                {
                    foreach (Music music in Musics)
                    {
                        MButton b = new MButton()
                        {
                            Size = new System.Drawing.Size(Panel_MusicList.Width, 30),
                            BackColor = Color.Transparent,
                            M_music = music,
                            Index = i,
                            ContextMenuStrip = CMS歌曲,
                        };
                        b.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
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
                Console.WriteLine("2023:"+e.Message);
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
                ResetChoosedMButton(mButton);
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
                Console.WriteLine("2024:"+ce.Message);
            }
        }
        #endregion


        #region 新建歌单相关事件

        /// <summary>
        /// 正在创建的列表
        /// </summary>
        private MLButton CreatingBtn = new MLButton();

        /// <summary>
        /// 创建歌单按钮Click事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_CteateList_Click(object sender, EventArgs e)
        {
            try
            {
                if (Btn_Spread.Text == "↑")
                {
                    Btn_Spread.Text = "↓";
                    Panel_CreatedList.Show();
                }
                Operation = OperateList.Create;
                if(myMusicApp.CanCreateMusicList()){
                    MLButton button = new MLButton()
                    {
                        ContextMenuStrip = CMS歌单,
                        Enabled = false,
                        Location = new Point(20, 0),
                    };
                    int newindex = MyMusicApp.ListLimits;
                    string autoname = "新建歌单" + newindex;
                    foreach(KeyValuePair<string,int> keyValuePair in AutoName)
                    {
                        int tmp = keyValuePair.Value;
                        if (tmp > 0 && tmp < newindex) 
                        {
                            newindex = tmp;
                            autoname = keyValuePair.Key;
                        }
                    }
                    button.MusicList = new MusicList(autoname, myMusicApp.user.User_Id);
                    button.Click += new EventHandler(CreatedListBtn_Click);
                    button.FlatAppearance.BorderSize = 0;
                    CreatingBtn = button;
                    foreach(MLButton mLButton in Panel_CreatedList.Controls)
                    {
                        mLButton.Location = new Point(mLButton.Location.X, mLButton.Location.Y + mLButton.Height);
                    }
                    Panel_CreatedList.Controls.Add(button);
                    TextBox textBox = new TextBox()
                    {
                        Text = autoname,
                        Location = new Point(20, 0),
                        Font = new System.Drawing.Font("微軟正黑體 Light", 11F),
                        Size = new Size(160, 30),
                        TextAlign = HorizontalAlignment.Center,
                    };
                    textBox.LostFocus += new EventHandler(CreatedListBtnNamed_LostFocus);
                    Panel_CreatedList.Controls.Add(textBox);
                    textBox.Show();
                    textBox.BringToFront();
                    textBox.Focus();
                    Timer_NamingList.Start();
                }

            }catch(Exception ce)
            {
                Console.WriteLine("2025:"+ce.Message);
            }

        }

        /// <summary>
        /// 新建歌单默认绑定事件，检测鼠标的悬停并改变按钮显示样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreatedList_OnMouseHover(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                button.ForeColor = System.Drawing.SystemColors.ControlText;
            }catch(Exception ce)
            {
                Console.WriteLine("2026:"+ce.Message);
            }
        }

        /// <summary>
        /// 鼠标停靠时改变字体颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreatedList_OnMouseLeave(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                button.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            }catch(Exception ce)
            {
                Console.WriteLine("2027:" + ce.Message);
            }
        }

        /// <summary>
        /// 当命名框失去焦点时，生成按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreatedListBtnNamed_LostFocus(object sender,EventArgs e)
        {
            try
            {
                TextBox textBox = (TextBox)sender;
                string oldListName = CreatingBtn.MusicList.ListName;
                switch (Operation)
                {
                    case OperateList.Create:
                        myMusicApp.CreateMusicList(CreatingBtn.MusicList);
                        if (AutoName.ContainsKey(CreatingBtn.MusicList.ListName))
                        {
                            AutoName[CreatingBtn.MusicList.ListName] = -AutoName[CreatingBtn.MusicList.ListName];
                        }
                        break;
                    case OperateList.Rename:
                        bool exist = false;
                        foreach(string listname in myMusicApp.CreatedMusicList.Keys)
                        {
                            if(listname == textBox.Text.ToString())
                            {
                                exist = true;
                            }
                        }
                        if (!exist)
                        {
                            myMusicApp.UpdateMusicList(CreatingBtn.MusicList, textBox.Text.ToString());
                            if (AutoName.ContainsKey(oldListName))
                            {
                                AutoName[oldListName] = -AutoName[oldListName];
                            }
                            if (AutoName.ContainsKey(CreatingBtn.MusicList.ListName))
                            {
                                AutoName[CreatingBtn.MusicList.ListName] = -AutoName[CreatingBtn.MusicList.ListName];
                            }
                        }
                        break;
                }
                CreatingBtn.Text = "            "+CreatingBtn.MusicList.ListName;
                CreatingBtn.Enabled = true;
                Panel_CreatedList.Controls.Remove(textBox);
                Timer_NamingList.Dispose();
                CreatingBtn.Show();
                ResetChoosedMLButton(ChoosedMLButton);
            }catch(Exception ce)
            {
                Console.WriteLine("2028:"+ce.Message);
            }
            
        }


        /// <summary>
        /// 歌单默认点击事件，点击显示歌单详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreatedListBtn_Click(object sender, EventArgs e)
        {
            try
            {
                ResetChoosedMLButton(sender);
                ShowList(ChoosedMLButton.MusicList.Musics);
            }catch(Exception ce)
            {
                Console.WriteLine("2029:" + ce.Message);
            }

        }

        /// <summary>
        /// 用于在创建歌单时，检测用户命名是否结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_NamingList_Tick(object sender, EventArgs e)
        {
            try
            {
                if (MouseButtons == MouseButtons.Left || MouseButtons == MouseButtons.Right)
                {
                    if (!InBox(new Size(160,30),new Point(20 +this.Left + Main_Panel.Left + Panel_MenuList.Left + Panel_CreatedList.Left + Panel_Nav.Left, CreatingBtn.Location.Y + this.Top + Main_Panel.Top + Panel_MenuList.Top + Panel_CreatedList.Top +  Panel_Nav.Top)))
                    {
                        this.Focus();
                    }
                }
            }
            catch (Exception ce)
            {
                Console.WriteLine("2030:" + ce.Message);
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
            try
            {
                int x = MousePosition.X;
                int y = MousePosition.Y;
                if (x < point.X || x > point.X+size.Width || y < point.Y || y > point.Y+size.Height) 
                {
                    return false;
                }
                return true;
            }
            catch (Exception ce)
            {
                Console.WriteLine("2031:" + ce.Message);
                return false;
            }

        }

        /// <summary>
        /// 展开自建歌单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Spread_Click(object sender, EventArgs e)
        {
            try
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
            }catch(Exception ce)
            {
                Console.WriteLine("2032:"+ce.Message);
            }
        }
        #endregion


        #region 默认歌单相关事件

        /// <summary>
        /// 用于定位左侧菜单栏 当前选择的音乐列表
        /// </summary>
        /// <param name="sender"></param>
        private void ResetChoosedMLButton(object sender)//定位当前显示列表
        {
            try
            {
                MLButton oldChoosedMLButton = (MLButton)ChoosedMLButton;
                oldChoosedMLButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));

                MLButton newChoosedMLButton = (MLButton)sender;
                newChoosedMLButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
                ChoosedMLButton = (MLButton)sender;
                if (ChoosedMLButton.MusicList.ListName != Btn_Local.MusicList.ListName)
                {
                    Btn_AddLocalMusic.Hide();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("2033:"+e.Message);
            }
        }
        /// <summary>
        /// 用于定位当前选择播放的音乐
        /// </summary>
        /// <param name="sender"></param>
        private void ResetChoosedMButton(object sender)//定位当前播放音乐
        {
            try
            {
                ChoosedMButton = (MButton)sender;
            }
            catch (Exception e)
            {
                Console.WriteLine("2034:"+e.Message);
            }
        }


        /// <summary>
        /// 单击左键显示本地与下载列表歌单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Local_Click(object sender, EventArgs e)
        {
            try
            {
                Btn_AddLocalMusic.BringToFront();
                Btn_AddLocalMusic.Show();
                ResetChoosedMLButton(sender);
                ShowList(myMusicApp.LocalMusicList.Musics);

            }
            catch (Exception ce)
            {
                Console.WriteLine("2035:" + ce.Message);
            }
        }

        /// <summary>
        /// 单击左键显示历史播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_History_Click(object sender, EventArgs e)
        {
            try
            {
                ResetChoosedMLButton(sender);
                ShowList(myMusicApp.HistoryMusicList.Musics);
            }
            catch (Exception ce)
            {
                Console.WriteLine("2036:" + ce.Message);
            }

        }

        /// <summary>
        /// 单击左键显示流行音乐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_PopMusic_Click(object sender, EventArgs e)
        {
            try
            {
                ResetChoosedMLButton(sender);
                ShowList(myMusicApp.PopMusicList.Musics);
            }
            catch (Exception ce)
            {
                Console.WriteLine("2037:" + ce.Message);
            }
 
        }

        /// <summary>
        ///  单击左键显示我喜欢音乐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Liked_Click(object sender, EventArgs e)
        {
            try
            {
                ResetChoosedMLButton(sender);
                ShowList(Btn_Liked.MusicList.Musics);
            }
            catch (Exception ce)
            {
                Console.WriteLine("2038:" + ce.Message);
            }

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
                Console.WriteLine("2039:"+ce.Message);
            }
        }

        /// <summary>
        /// 关闭播放列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Shut_Click(object sender, EventArgs e)
        {
            try
            {
                Panel_PlayList.Width = 0;
                Panel_PlayList.Hide();
            }
            catch (Exception ce)
            {
                Console.WriteLine("2040:" + ce.Message);
            }

        }

        /// <summary>
        /// 显示播放列表，实例化播放列表中的音乐按钮
        /// </summary>
        /// <param name="PlayingMusicList"></param>
        private void PlayListUpdate(MusicList PlayingMusicList)
        {
            try
            {
                Panel_PlayingMusicList.Controls.Clear();
                if (PlayingMusicList != null && PlayingMusicList.Musics.Count() != 0)
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
                        Panel_PlayingMusicList.Controls.Add(b);
                        b.Show();
                        i++;
                    }
                }
                Invalidate();
            }
            catch (Exception e)
            {
                Console.WriteLine("2041:"+e.Message);
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
                Console.WriteLine("2042:"+ce.Message);
            }
        }



        #endregion

        private void InitCreatedList()
        {
            try
            {
                for(int i = 1; i <= MyMusicApp.ListLimits; i++)
                {
                    AutoName.Add("新建歌单" + i , i );
                }
                int index = 0;
                foreach(KeyValuePair<string,int> keyValuePair in myMusicApp.CreatedMusicList) 
                {
                    if (MyMusicApp.DefaultList.Contains(keyValuePair.Key))
                    {
                        continue;
                    }
                    MLButton button = new MLButton()
                    {
                        ContextMenuStrip = CMS歌单,
                    };
                    button.Click += new EventHandler(CreatedListBtn_Click);
                    button.FlatAppearance.BorderSize = 0;
                    button.MusicList = myMusicApp.AllMusicLists[keyValuePair.Value];
                    button.Text = "            " + button.MusicList.ListName.ToString();
                    button.Location = new Point(20, (myMusicApp.GetCreatedMusicListNumber() - 1 - index) * button.Height);
                    index++;
                    Panel_CreatedList.Controls.Add(button);
                    button.Show();
                    AutoName[button.MusicList.ListName] = -AutoName[button.MusicList.ListName];
                }
            }
            catch(Exception ce)
            {
                Console.WriteLine("2043:"+ce.Message);
            }
        }

        private void InitDefaultList()
        {
            try
            {
                Btn_Liked.MusicList = myMusicApp.LikedMusicList;
                Btn_History.MusicList = myMusicApp.HistoryMusicList;
                Btn_Liked.Show();
                Btn_History.Show();
            }
            catch (Exception e)
            {
                Console.WriteLine("2058:"+e.Message);
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
                Console.WriteLine("2044:"+ce.Message);
            }

        }

        private void CMS默认歌单_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                MLButton mLButton = (MLButton)((ContextMenuStrip)sender).SourceControl;
                ResetChoosedMLButton(mLButton);
                ShowList(mLButton.MusicList.Musics);
            }
            catch (Exception ce)
            {
                Console.WriteLine("2045:"+ce.Message);
            }

        }

        private void CMS歌单_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                MLButton mLButton = (MLButton)((ContextMenuStrip)sender).SourceControl;
                string listname = mLButton.MusicList.ListName;
                if (MyMusicApp.DefaultList.Contains(listname))
                {
                    删除ToolStripMenuItem.Enabled = false;
                    重命名ToolStripMenuItem.Enabled = false;
                }
                else
                {
                    删除ToolStripMenuItem.Enabled = true;
                    重命名ToolStripMenuItem.Enabled = true;
                }
                if (mLButton.MusicList.Musics.Count() != 0)
                {
                    播放.Enabled = true;
                }
                else
                {
                    播放.Enabled = false;
                }
                CreatingBtn = mLButton;
                ResetChoosedMLButton(mLButton);
                ShowList(mLButton.MusicList.Musics);
            }catch(Exception ce)
            {
                Console.WriteLine("2046:"+ce.Message);
            }
        }

        private void 删除_Click(object sender, EventArgs e)
        {
            try
            {
                Operation = OperateList.Delete;
                MusicList ListToDelete = CreatingBtn.MusicList;
                myMusicApp.DeleteMusicList(CreatingBtn.MusicList);
                if (AutoName.Keys.Contains(ListToDelete.ListName))
                {
                    AutoName[ListToDelete.ListName] = -AutoName[ListToDelete.ListName];
                }
                Panel_CreatedList.Controls.Remove(CreatingBtn);
                MLButton mLButton = (MLButton)Panel_CreatedList.Controls[Panel_CreatedList.Controls.Count-1];
                ResetChoosedMLButton(mLButton);
                ShowList(mLButton.MusicList.Musics);
                ResetList();
            }
            catch(Exception ce)
            {
                Console.WriteLine("2047:"+ce.Message);
            }
        }

        private void ResetList()
        {
            try
            {
                int i = Panel_CreatedList.Controls.Count;
                foreach(Control c in Panel_CreatedList.Controls)
                {
                    c.Location = new Point(20, (i-1) * c.Height);
                    i--;
                }
            }catch(Exception e)
            {
                Console.WriteLine("2048:"+e.Message);
            }
        }

        private void 重命名_Click(object sender, EventArgs e)
        {
            try
            {
                Operation = OperateList.Rename;
                CreatingBtn.Hide();
                TextBox textBox = new TextBox()
                {
                    Text = CreatingBtn.MusicList.ListName.ToString(),
                    Location = new Point(CreatingBtn.Location.X, CreatingBtn.Location.Y),
                    Font = new System.Drawing.Font("微軟正黑體 Light", 11F),
                    Size = new Size(160, 30),
                    TextAlign = HorizontalAlignment.Center,
                };
                textBox.LostFocus += new EventHandler(CreatedListBtnNamed_LostFocus);
                Panel_CreatedList.Controls.Add(textBox);
                textBox.Show();
                textBox.BringToFront();
                textBox.Focus();
                Timer_NamingList.Start();
            }
            catch(Exception ce)
            {
                Console.WriteLine("2049:"+ce.Message);
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
                Console.WriteLine("2050:"+ce.Message);
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
                Console.WriteLine("2051:"+ce.Message);
            }
        }

        private void Timer_Volume_Tick(object sender, EventArgs e)
        {
            try
            {
                if (MouseButtons == MouseButtons.Left || MouseButtons == MouseButtons.Right)
                {
                    if (!InBox(new Size(65, 190), new Point(Panel_Volume.Location.X + this.Left, Panel_Volume.Location.Y + this.Top)))
                    {
                        mTrackBar_Volume.Enabled = false;
                        Panel_Volume.Visible = false;
                        Timer_Volume.Stop();
                    }
                }

            }
            catch (Exception ce)
            {
                Console.WriteLine("2052:"+ce.Message);
            }
        }

        private void Btn_User_Click(object sender, EventArgs e)
        {
            try
            {
                new Login(this).Show();
            }catch(Exception ce)
            {
                Console.WriteLine("2053:"+ce.Message);
            }
        }

        private void 播放ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                ResetChoosedMButton(ChoosedMButton);
                if (PlayingMusicListName != ChoosedMLButton.MusicList.ListName)
                {
                    myMusicApp.PlayingMusicList.Musics = ChoosedMLButton.MusicList.Musics;
                    myMusicApp.UpdateMusicList(myMusicApp.PlayingMusicList);
                    PlayingMusicListName = ChoosedMLButton.MusicList.ListName;
                }
                InitAWMP(myMusicApp.PlayingMusicList, ChoosedMButton.Index);
                AWMP.Ctlcontrols.play();
                Timer_Music.Start();
                Btn_Play.Text = "||";
            }
            catch (Exception ce)
            {
                Console.WriteLine("2054:"+ce.Message);
            }
        }

        private void CMS歌曲_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                MButton mButton = (MButton)((ContextMenuStrip)sender).SourceControl;
                ResetChoosedMButton(mButton);
                this.添加到ToolStripMenuItem.DropDownItems.Clear();
                ToolStripMenuItem toolStripMenuItem;
                //我喜欢
                if (Btn_Liked.Visible)
                {
                    toolStripMenuItem = new ToolStripMenuItem(Btn_Liked.MusicList.ListName.ToString());
                    toolStripMenuItem.Name = Btn_Liked.MusicList.ListName.ToString();
                    toolStripMenuItem.Click += new EventHandler(添加到歌单ToolStripMenuItem_Click);
                    this.添加到ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem });
                }
                //播放队列
                toolStripMenuItem = new ToolStripMenuItem(myMusicApp.PlayingMusicList.ListName);
                toolStripMenuItem.Name = myMusicApp.PlayingMusicList.ListName;
                toolStripMenuItem.Click += new EventHandler(添加到歌单ToolStripMenuItem_Click);
                this.添加到ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem });
                //自定义歌单
                foreach (MLButton mLButton in Panel_CreatedList.Controls)
                {
                    toolStripMenuItem = new ToolStripMenuItem(mLButton.MusicList.ListName.ToString());
                    toolStripMenuItem.Name = mLButton.MusicList.ListName.ToString();
                    toolStripMenuItem.Click += new EventHandler(添加到歌单ToolStripMenuItem_Click);
                    this.添加到ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem });
                }
            }catch(Exception ce)
            {
                Console.WriteLine("2055:"+ce.Message);
            }

        }

        /// <summary>
        /// 点击歌曲将歌曲添加到歌单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 添加到歌单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
                MButton mButton = ChoosedMButton;
                MusicList ListToUpdate = new MusicList();
                //向所选歌单中添加所选歌曲
                //自定义歌单
                foreach(MLButton mLButton in Panel_CreatedList.Controls)
                {
                    if(mLButton.MusicList.ListName.ToString() == toolStripMenuItem.Name.ToString())
                    {
                        mLButton.MusicList.Add(mButton.M_music);
                        ListToUpdate = mLButton.MusicList;
                    }
                }
                //默认歌单
                if(Btn_Liked.MusicList.ListName == toolStripMenuItem.Name)
                {
                    Btn_Liked.MusicList.Add(mButton.M_music);
                    ListToUpdate = Btn_Liked.MusicList;
                }
                //播放列表
                if(myMusicApp.PlayingMusicList.ListName == toolStripMenuItem.Name)
                {
                    myMusicApp.PlayingMusicList.Add(mButton.M_music);
                    myMusicApp.UpdateMusicList(myMusicApp.PlayingMusicList);
                }
                //更新musiclist和数据库
                myMusicApp.UpdateMusicList(ListToUpdate);
            }
            catch(Exception ce)
            {
                Console.WriteLine("2056:"+ce.Message);
            }
        }

        private void Timer_Loading_Tick(object sender, EventArgs e)
        {
            if (this.Opacity >= 1)
            {
                Timer_Loading.Stop();
            }
            else
            {
                base.Opacity += 0.2;
            }
        }

        public void Login(User user)
        {
            try
            {
                myMusicApp.InitLoginInfo(user);
                Panel_CreateList.Show();
                InitCreatedList();
                InitDefaultList();
            }
            catch (Exception e)
            {
                Console.WriteLine("2057:"+e.Message);
            }
        }

        private void 移除ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



    }
}
