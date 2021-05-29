using AxWMPLib;
using ControlDemos;
using Mono.Web;
using Shell32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
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
        
        private const int WM_NCHITTEST = 0x84;          // variables for dragging the form
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

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
            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)// drag the form
                m.Result = (IntPtr)HTCAPTION;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        /// 实现窗体拖动

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        private void Form_MouseDown(object sender, MouseEventArgs e)//拖动事件
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

            //设置默认音乐列表为本地与下载
            ChoosedMLButton = Btn_Local;

            //初始化界面的音乐列表，显示本地与下载列表中的音乐文件
            Btn_PopMusic.MusicList = myMusicApp.PopMusicList;
            Btn_History.MusicList = myMusicApp.HistoryMusicList;
            Btn_Local.MusicList = myMusicApp.LocalMusicList;
            Btn_Liked.MusicList = myMusicApp.LikedMusicList;
            Btn_SearchResult.MusicList = SearchMusicList;
            ShowList(Btn_Local.MusicList);

            //初始化播放器
            InitAWMP(myMusicApp.PlayingMusicList, myMusicApp.PlayingMusicList.StartIndex);
            AWMP.settings.setMode("loop", true);//设置为循环播放
            AWMP.settings.volume = 100;
            base.Opacity = 0;
            Timer_Loading.Start();

            //
            double duration = 0;
            if (AWMP.currentMedia != null)
            {
                IWMPMedia media= AWMP.currentMedia;
                Double.TryParse(media.getItemInfo("Duration"),out duration);
            }
            string max_minute = (int)(duration / 60) >= 10 ? ((int)(duration / 60)).ToString() : "0" + ((int)(duration / 60)).ToString();
            string max_second = (int)(duration % 60) >= 10 ? ((int)(duration % 60)).ToString() : "0" + ((int)(duration % 60)).ToString();
            Btn_Process.Text = "00:00 / " + max_minute + ":" + max_second;
            MTBar_Music.M_Value = 0;

            //Console.WriteLine(AWMP.currentMedia.sourceURL);
            //Console.WriteLine(ChoosedMButton.M_music.FileURL);
        }

        private void Form_MouseClick(object sender, MouseEventArgs e)
        {
            MouseClicked = true;
        }

        /// <summary>
        /// 鼠标是否点击
        /// </summary>
        public bool MouseClicked = false;
        
        /// <summary>
        /// 自定义歌单自动命名
        /// </summary>
        public Dictionary<string, int> AutoName = new Dictionary<string, int>();

        public Dictionary<string, Music> MediaDic = new Dictionary<string, Music>();

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
        public MyMusicApp myMusicApp = new MyMusicApp();

        /// <summary>
        /// 保存搜索结果
        /// </summary>
        public MusicList SearchMusicList = new MusicList("搜索结果");

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
                    string filename = openFileDialog.FileName;
                    ShellClass sh = new ShellClass();
                    Folder dir = sh.NameSpace(Path.GetDirectoryName(filename));
                    FolderItem item = dir.ParseName(Path.GetFileName(filename));
                    string name = dir.GetDetailsOf(item, 21);
                    string[] s = name.Split('.');
                    name = s[0];
                    for(int i = 0; i < name.Length; i++)
                    {
                        if(name[i]==' ')
                        {
                            continue;
                        }
                        name = name.Substring(i);
                        break;
                    }
                    string singer = dir.GetDetailsOf(item, 13);
                    string fileurl = filename;
                    string album = dir.GetDetailsOf(item, 14);
                    Music music = new Music(name, singer, album, fileurl);

                    myMusicApp.LocalMusicList.Add(music);
                    Btn_Local.MusicList = myMusicApp.LocalMusicList;
                    myMusicApp.UpdateMusicList(myMusicApp.LocalMusicList);
                    ShowList(myMusicApp.LocalMusicList);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
            int deltaX = this.Width - 2 * edgeX - Main_Panel.Width;
            int deltaY = this.Height - 2 * edgeY - Main_Panel.Height;
            //大小
            Main_Panel.Width = this.Width - 2 * edgeX;
            Main_Panel.Height = this.Height - 2 * edgeY;
            Panel_Nav.Height = (int)(this.Height - 2 * edgeY);
            Panel_Tool.Width = (int)(this.Width - Panel_Nav.Width - 2 * edgeX);
            Panel_Play.Width = (int)(this.Width - Panel_Nav.Width - 2 * edgeX);
            MTBar_Music.Width = (int)(this.Width - Panel_Nav.Width - 2 * edgeX);
            Panel_PlayStatus.Width = (int)(this.Width - Panel_Nav.Width - 2 * edgeX);
            Panel_Detail.Width = (int)(this.Width - Panel_Nav.Width - 2 * edgeX);
            Panel_Detail.Height = (int)(this.Height - Panel_Play.Height - Panel_Icon.Height - 2 * edgeY);
            Panel_MusicListScroll.Height = Panel_Detail.Width - 80;
            Panel_MusicListScroll.Width = Panel_Detail.Width - 60;
            Panel_MusicList.Height = Panel_Detail.Width - 80;
            Panel_MusicList.Width = Panel_Detail.Width - 60;
            Panel_MenuList.Height = (int)(Panel_Nav.Height - Panel_Icon.Height);
            TxtBox_SearchBox.Width = TxtBox_SearchBox.Width + deltaX / 3;
            Btn_SearchBorder.Width = Btn_SearchBorder.Width + deltaX / 3;
            MSBar_MenuList.Height = MSBar_MenuList.Height + deltaY;
            //MSBar_MusicList.Height = MSBar_MusicList.Height + deltaY;
            MSBar_PlayingMusicList.Height = MSBar_PlayingMusicList.Height + deltaY;
            //位置
            Panel_Play.Location = new Point(Panel_Play.Location.X, Panel_Detail.Location.Y + Panel_Detail.Height);
            Panel_Control.Location = new Point((Panel_PlayStatus.Width - Panel_Control.Width) / 2, 0);
            Panel_Mode.Location = new Point(deltaX/2 + Panel_Mode.Location.X, Panel_Mode.Location.Y+deltaY);
            Panel_Volume.Location = new Point(deltaX /2 + Panel_Volume.Location.X, Panel_Volume.Location.Y+deltaY);
            Btn_Shut.Location = new Point(Btn_Shut.Location.X, Panel_PlayList.Height - Btn_Shut.Height);
            Btn_Search.Location = new Point(Btn_Search.Location.X + deltaX / 3, Btn_Search.Location.Y);
            MSBar_MusicList.Location = new Point(Panel_Detail.Width- MSBar_MusicList.Width, MSBar_MusicList.Location.Y);
            //刷新
            MButtonType.Width = Panel_MusicListScroll.Width;
            foreach (Control control in Panel_MusicList.Controls)
            {
                control.Width = Panel_MusicListScroll.Width;
            }
            if ((Panel_MusicList.Controls.Count * MButtonType.Height) > Panel_MusicListScroll.Height)
            {
                MSBar_MusicList.Enabled = true;
                MSBar_MusicList.M_Value = 0;
                MSBar_MusicList.M_SliderLength = MSBar_MusicList.Height * Panel_MusicListScroll.Height / (Panel_MusicList.Controls.Count * MButtonType.Height);
                MSBar_MusicList.Show();
            }

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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
                    //ResetChoosedMButton(Panel_MusicList.Controls[index]);
                    IWMPPlaylist playList = AWMP.playlistCollection.newPlaylist("MyPlayList");
                    IWMPMedia media = AWMP.newMedia(musicList.Musics[index].FileURL);
                    playList.appendItem(media);
                    if (!MediaDic.ContainsKey(media.sourceURL))
                    {
                        MediaDic.Add(media.sourceURL, musicList.Musics[index]);
                    }
                    Btn_CurrentMusic.M_music = musicList.Musics[index];
                    for (int i = index+1; i < musicList.Musics.Count; i++)
                    {
                        string url = musicList.Musics[i].FileURL.ToString();
                        media = AWMP.newMedia(url);
                        if (!MediaDic.ContainsKey(media.sourceURL))
                        {
                            MediaDic.Add(media.sourceURL, musicList.Musics[i]);
                        }
                        playList.appendItem(media);
                    }
                    for (int i = 0; i < index; i++)
                    {
                        string url = musicList.Musics[i].FileURL.ToString();
                        media = AWMP.newMedia(url);
                        if (!MediaDic.ContainsKey(media.sourceURL))
                        {
                            MediaDic.Add(media.sourceURL, musicList.Musics[i]);
                        }
                        playList.appendItem(media);
                    }
                    AWMP.currentPlaylist = playList;
                    AWMP.Ctlcontrols.stop();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
                    Panel_Volume.Show();
                    MTBar_Volume.Enabled = true;
                    MTBar_Volume.Focus();
                    Timer_Volume.Start();
                }
                else
                {
                    Panel_Volume.Hide();
                    MTBar_Volume.Enabled = false;
                    Timer_Volume.Stop();
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 音量滑块获取鼠标焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MTBar_Volume_MouseEnter(object sender,EventArgs e)
        {
            try
            {
                MTBar_Volume.Focus();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
                    Timer_Music.Stop();
                    if(AWMP.currentPlaylist != null)
                    {
                        AWMP.Ctlcontrols.pause();
                    }
                }
                Btn_Play.Text = Btn_Play.Text == "||" ? "▶" : "||";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
                ChoosedMButton.BackColor = ChoosedMButton.FlatAppearance.BorderColor;
                foreach (MButton mButton in Panel_MusicList.Controls)
                {
                    if (mButton.M_music.Equals(MediaDic[AWMP.currentMedia.sourceURL]))
                    {
                        ChoosedMButton = mButton;
                        ChoosedMButton.BackColor = ChoosedMButton.FlatAppearance.MouseDownBackColor;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
                ChoosedMButton.BackColor = ChoosedMButton.FlatAppearance.BorderColor;
                foreach (MButton mButton in Panel_MusicList.Controls)
                {
                    if (mButton.M_music.Equals(MediaDic[AWMP.currentMedia.sourceURL]))
                    {
                        ChoosedMButton = mButton;
                        ChoosedMButton.BackColor = ChoosedMButton.FlatAppearance.MouseDownBackColor;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private Image GetMusicImage(string Fileurl)
        {
            HttpWebRequest webRequest = WebRequest.CreateHttp("http://android-artworks.25pp.com/fs08/2016/05/05/10/110_ee7a7120ccad83088a2a7beb18f010cf_con.png");
            webRequest.Method = "GET";
            Stream stream = webRequest.GetResponse().GetResponseStream();
            Image image = Image.FromStream(stream);
            Btn_MusicPic.BackgroundImage = image;
            return image;
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
                if (AWMP.currentMedia != null)
                {
                    if (!Btn_CurrentMusic.M_music.Equals(MediaDic[AWMP.currentMedia.sourceURL])){
                        Btn_CurrentMusic.M_music = MediaDic[AWMP.currentMedia.sourceURL];

                        //Btn_MusicPic.BackgroundImage = GetMusicImage("http://android-artworks.25pp.com/fs08/2016/05/05/10/110_ee7a7120ccad83088a2a7beb18f010cf_con.png");
                    }
                    foreach (MButton mButton in Panel_MusicList.Controls)
                    {
                        if(mButton.M_music.Equals(MediaDic[AWMP.currentMedia.sourceURL]))
                        {
                            mButton.BackColor = mButton.FlatAppearance.MouseDownBackColor;
                        }
                    }
                }
                double currentPosition = AWMP.Ctlcontrols.currentPosition;
                double duration = AWMP.currentMedia.duration;
                double value;
                if(duration <= 0)
                {
                    value = 0;
                }
                else
                {
                    value = MTBar_Music.M_Maximum * currentPosition / duration;
                }
                if (value < 0) { MTBar_Music.M_Value = 0; }
                else { MTBar_Music.M_Value = value; }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void MTBar_Music_MValueChanged(object sender, MEventArgs e)
        {
            try
            {
                if (AWMP.currentMedia != null)
                {
                    double duration = AWMP.currentMedia.duration;
                    double currentPosition = (MTBar_Music.M_Value / MTBar_Music.M_Maximum) * duration;
                    //用数字实时显示播放进度
                    string max_minute = (int)(duration / 60) >= 10 ? ((int)(duration / 60)).ToString() : "0" + ((int)(duration / 60)).ToString();
                    string max_second = (int)(duration % 60) >= 10 ? ((int)(duration % 60)).ToString() : "0" + ((int)(duration % 60)).ToString();
                    string cur_minute = (int)(currentPosition / 60) >= 10 ? ((int)(currentPosition / 60)).ToString() : "0" + ((int)(currentPosition / 60)).ToString();
                    string cur_second = (int)(currentPosition % 60) >= 10 ? ((int)(currentPosition % 60)).ToString() : "0" + ((int)(currentPosition % 60)).ToString();
                    if (cur_minute.Length > 2) { cur_minute = "00"; }
                    if (cur_second.Length > 2) { cur_second = "00"; }
                    Btn_Process.Text = cur_minute + ":" + cur_second + " / " + max_minute + ":" + max_second;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

        }

        /// <summary>
        /// 手动控制进度时关闭计时器控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MTBar_Music_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Timer_Music.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 松开后从指定进度开始播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MTBar_Music_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                double newValue = MTBar_Music.M_Value / MTBar_Music.M_Maximum * AWMP.currentMedia.duration;
                //播放中
                if (AWMP.currentPlaylist!=null)
                {
                    AWMP.Ctlcontrols.currentPosition = newValue;
                    AWMP.Ctlcontrols.play();
                    Timer_Music.Start();
                    Btn_Play.Text = "||";
                }
                //其他情况下
                else
                {
                    AWMP.Ctlcontrols.currentPosition = newValue;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
                count = myMusicApp.PlayingMusicList.Count();
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 实时调节音量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MTBar_Volume_ValueChanged(object sender, MEventArgs e)
        {
            try
            {
                int volume = (int)(100 * MTBar_Volume.M_Value / MTBar_Volume.M_Maximum);
                Lbl_Volume.Text = volume.ToString() + "%";
                AWMP.settings.volume = volume;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        #endregion


        #region 实例化音乐列表
        /// <summary>
        /// 在主页显示选择的音乐列表，批量实例化音乐按钮
        /// </summary>
        /// <param name="Musics"></param>
        private void ShowList(MusicList musicList)
        {
            try
            {
                Lbl_ShowList.Text = musicList.ListName;
                List<Music> Musics = musicList.Musics;
                Panel_MusicList.Controls.Clear();
                Panel_MusicList.Height = 0;
                Panel_MusicList.Location = new Point(0, 0);
                int i = 0;
                if(Musics.Count() != 0)
                {
                    foreach (Music music in Musics)
                    {
                        MButton b = new MButton()
                        {
                            Size = new System.Drawing.Size(Panel_MusicList.Width, 30),
                            BackColor = Panel_MusicList.BackColor,
                            M_music = music,
                            Index = i,
                            ContextMenuStrip = CMS歌曲,
                            Font = ButtonType.Font
                        };
                        b.DoubleClick += new EventHandler(PlayChoosedMusic);
                        b.Location = new Point(0, b.Height * i);
                        b.FlatAppearance.BorderSize = 0;
                        b.FlatAppearance.BorderColor = b.BackColor;
                        Panel_MusicList.Controls.Add(b);
                        b.Show();
                        i++;
                    }
                }
                foreach (MButton m in Panel_MusicList.Controls)
                {
                    if (AWMP.currentMedia != null) 
                    {
                        if (m.M_music.Equals(MediaDic[AWMP.currentMedia.sourceURL]))
                        {
                            m.BackColor = m.FlatAppearance.MouseDownBackColor;
                            ChoosedMButton = m;
                        }
                    }
                }
                if ((Panel_MusicList.Height) > Panel_MusicListScroll.Height)
                {
                    MSBar_MusicList.Enabled = true;
                    MSBar_MusicList.M_SliderLength = MSBar_MusicList.Height * Panel_MusicListScroll.Height / Panel_MusicList.Height;
                    MSBar_MusicList.M_Value = 0;
                    MSBar_MusicList.Show();
                }
                else
                {
                    MSBar_MusicList.Hide();
                    MSBar_MusicList.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
                ChoosedMButton.BackColor = ChoosedMButton.FlatAppearance.BorderColor;
                MButton mButton = (MButton)sender;
                ResetChoosedMButton(mButton);
                mButton.BackColor = mButton.FlatAppearance.MouseDownBackColor;
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
                        Font = Btn_Local.Font
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
                    button.Location = new Point(20, 0);
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

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
                ShowList(ChoosedMLButton.MusicList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
                    if (!InRectangle(new Size(160,30),new Point(CreatingBtn.Location.X+ Panel_CreatedList.Left +Panel_MenuList.Left+Panel_MenuListScroll.Left+ Panel_Nav.Left + Main_Panel.Left + this.Left, CreatingBtn.Location.Y + Panel_CreatedList.Location.Y + Panel_MenuList.Location.Y + Panel_MenuListScroll.Location.Y + Panel_Nav.Location.Y + Main_Panel.Location.Y + this.Top)))
                    {
                        this.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

        }

        /// <summary>
        /// 检测鼠标是否在指定区域
        /// </summary>
        /// <param name="size">区域大小</param>
        /// <param name="point">区域坐标相对于屏幕</param>
        /// <returns></returns>
        private bool InRectangle(Size size,Point point)
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
             
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
                    Panel_MenuList.Location = new Point(0, 0);
                    if ((Panel_CreatedList.Location.Y+Panel_CreatedList.Height) > Panel_MenuListScroll.Height)
                    {
                        MSBar_MenuList.Enabled = true;
                        MSBar_MenuList.M_Value = 0;
                        MSBar_MenuList.M_SliderLength = MSBar_MenuList.Height * Panel_MenuListScroll.Height / (Panel_CreatedList.Location.Y + Panel_CreatedList.Height);
                        MSBar_MenuList.Show();
                    }
                }
                else
                {
                    Panel_MenuList.Location = new Point(0, 0);
                    Btn_Spread.Text = "↑";
                    Panel_CreatedList.Hide();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
                ChoosedMButton.BackColor = Color.Transparent;
                ChoosedMButton = (MButton)sender;
                ChoosedMButton.BackColor = ChoosedMButton.FlatAppearance.MouseDownBackColor;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
                ShowList(myMusicApp.LocalMusicList);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
                ShowList(myMusicApp.HistoryMusicList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
                ShowList(myMusicApp.PopMusicList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
                ShowList(Btn_Liked.MusicList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
                Lbl_Number.Text = "  " + myMusicApp.PlayingMusicList.Count() + "首歌曲";
                PlayListUpdate(myMusicApp.PlayingMusicList);
                Panel_PlayList.Width = 300;
                Panel_PlayList.Location = new Point(this.Width - 300, 0);
                Panel_PlayList.BringToFront();
                Panel_PlayList.Show();
                if (Panel_PlayingMusicListScroll.Height < Panel_PlayingMusicList.Height)
                {
                    MSBar_PlayingMusicList.Enabled = true;
                    MSBar_PlayingMusicList.M_SliderLength = MSBar_PlayingMusicList.Height * Panel_PlayingMusicListScroll.Height / Panel_PlayingMusicList.Height;
                    Slider_PlayingMusicList.Height = MSBar_PlayingMusicList.M_SliderLength;
                    MSBar_PlayingMusicList.M_Value = 0;
                    Slider_PlayingMusicList.Show();
                }
                else
                {
                    MSBar_PlayingMusicList.Enabled = false;
                    Slider_PlayingMusicList.Hide();
                }
                Timer_ClosePlayList.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
                MSBar_PlayingMusicList.Enabled = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
                Panel_PlayingMusicList.Height = 0;
                Panel_PlayingMusicList.Location = new Point(0, 0);
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
                            Font = ButtonType.Font,
                            type = MButton.Type.Simple
                        };
                        b.DoubleClick += new EventHandler(PlayChoosedMusic2);
                        b.Location = new Point(0, 64 * i);
                        b.FlatAppearance.BorderSize = 0;
                        Panel_PlayingMusicList.Controls.Add(b);
                        b.Show();
                        i++;
                    }
                    foreach (MButton m in Panel_PlayingMusicList.Controls)
                    {
                        if (AWMP.currentMedia != null)
                        {
                            if (m.M_music.Equals(MediaDic[AWMP.currentMedia.sourceURL]))
                            {
                                m.BackColor = m.FlatAppearance.MouseDownBackColor;
                            }
                        }
                    }
                }
                Invalidate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
                ChoosedMButton.BackColor = ChoosedMButton.FlatAppearance.BorderColor;
                foreach (MButton m in Panel_MusicList.Controls)
                {
                    if (m.M_music.Equals(MediaDic[AWMP.currentMedia.sourceURL]))
                    {
                        ChoosedMButton = m;
                        ChoosedMButton.BackColor = ChoosedMButton.FlatAppearance.MouseDownBackColor;
                    }
                }
                foreach (MButton m in Panel_PlayingMusicList.Controls)
                {
                    m.BackColor = m.FlatAppearance.BorderColor;
                }
                mButton.BackColor = mButton.FlatAppearance.MouseDownBackColor;
                AWMP.Ctlcontrols.play();
                Timer_Music.Start();
                Btn_Play.Text = "||";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
                    button.Font = Btn_Local.Font;
                    button.Text = "            " + button.MusicList.ListName.ToString();
                    button.Location = new Point(20, (myMusicApp.GetCreatedMusicListNumber() - 1 - index) * button.Height);
                    index++;
                    Panel_CreatedList.Controls.Add(button);
                    button.Show();
                    if (AutoName.ContainsKey(button.MusicList.ListName))
                    {
                        AutoName[button.MusicList.ListName] = -AutoName[button.MusicList.ListName];
                    }
                }
                if ((Panel_CreatedList.Location.Y + Panel_CreatedList.Height) > Panel_MenuListScroll.Height)
                {
                    MSBar_MenuList.Enabled = true;
                    MSBar_MenuList.M_Value = 0;
                    MSBar_MenuList.M_SliderLength = MSBar_MenuList.Height * Panel_MenuListScroll.Height / (Panel_CreatedList.Location.Y + Panel_CreatedList.Height);
                    MSBar_MenuList.Show();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void CMS默认歌单_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                MLButton mLButton = (MLButton)((ContextMenuStrip)sender).SourceControl;
                ResetChoosedMLButton(mLButton);
                ShowList(mLButton.MusicList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
                ShowList(mLButton.MusicList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
                ShowList(mLButton.MusicList);
                ResetList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void Timer_ClosePlayList_Tick(object sender, EventArgs e)
        {
            try
            {
                if (MouseClicked || MouseButtons == MouseButtons.Left || MouseButtons == MouseButtons.Right)
                {
                    MouseClicked = false;
                    if (!InRectangle(new Size(Panel_PlayList.Width,Panel_PlayList.Height), new Point(this.Width - 300 + this.Left, this.Top)))
                    {
                        Panel_PlayList.Width = 0;
                        Panel_PlayList.Hide();
                        MSBar_PlayingMusicList.Enabled = false;
                        Timer_ClosePlayList.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void Timer_PlayingMode_Tick(object sender, EventArgs e)
        {
            try
            {
                if (MouseClicked || MouseButtons == MouseButtons.Left || MouseButtons == MouseButtons.Right)
                {
                    MouseClicked = false;
                    if (!InRectangle(new Size(Panel_Mode.Width, Panel_Mode.Height), new Point(Panel_Mode.Location.X + this.Left, Panel_Mode.Location.Y + this.Top)))
                    {
                        Panel_Mode.Visible = false;
                        Timer_PlayingMode.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void Timer_Volume_Tick(object sender, EventArgs e)
        {
            try
            {
                if (MouseClicked || MouseButtons == MouseButtons.Left || MouseButtons == MouseButtons.Right)
                {
                    MouseClicked = false;
                    if (!InRectangle(new Size(Btn_Volume.Width, Btn_Volume.Height),new Point(Btn_Volume.Location.X+Panel_Control.Location.X+Panel_PlayStatus.Location.X+Panel_Play.Location.X+Main_Panel.Location.X+this.Left, Btn_Volume.Location.Y + Panel_Control.Location.Y + Panel_PlayStatus.Location.Y + Panel_Play.Location.Y + Main_Panel.Location.Y + this.Top))
                        &&!InRectangle(new Size(Panel_Volume.Width, Panel_Volume.Height), new Point(Panel_Volume.Location.X + this.Left, Panel_Volume.Location.Y + this.Top)))
                    {
                        MTBar_Volume.Enabled = false;
                        Panel_Volume.Visible = false;
                        Timer_Volume.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void Btn_Login_Click(object sender, EventArgs e)
        {
            try
            {
                new Login(this).ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void CMS歌曲_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                MButton mButton = (MButton)((ContextMenuStrip)sender).SourceControl;
                ResetChoosedMButton(mButton);
                this.添加到ToolStripMenuItem.DropDownItems.Clear();
                if (ChoosedMLButton.MusicList.ListName == "本地与下载")
                {
                    this.下载ToolStripMenuItem.Visible = false;
                }
                else
                {
                    this.下载ToolStripMenuItem.Visible = true;
                }
                ToolStripMenuItem toolStripMenuItem;
                //我喜欢
                if (Btn_Liked.Visible)
                {
                    toolStripMenuItem = new ToolStripMenuItem(Btn_Liked.MusicList.ListName.ToString())
                    {
                        Name = Btn_Liked.MusicList.ListName.ToString()
                    };
                    toolStripMenuItem.Click += new EventHandler(添加到歌单ToolStripMenuItem_Click);
                    this.添加到ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem });
                }
                //播放队列
                toolStripMenuItem = new ToolStripMenuItem(myMusicApp.PlayingMusicList.ListName)
                {
                    Name = myMusicApp.PlayingMusicList.ListName
                };
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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
                        myMusicApp.UpdateMusicList(ListToUpdate);
                    }
                }
                //默认歌单
                if(Btn_Liked.MusicList.ListName == toolStripMenuItem.Name)
                {
                    Btn_Liked.MusicList.Add(mButton.M_music);
                    ListToUpdate = Btn_Liked.MusicList;
                    myMusicApp.UpdateMusicList(ListToUpdate);
                }
                //播放列表
                if(myMusicApp.PlayingMusicList.ListName == toolStripMenuItem.Name)
                {
                    myMusicApp.PlayingMusicList.Add(mButton.M_music);
                    Console.WriteLine(myMusicApp.PlayingMusicList.ListName);
                    myMusicApp.UpdateMusicList(myMusicApp.PlayingMusicList);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
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

        private User user = new User();
        public void Login(User user)
        {
            try
            {
                this.user = user;
                Timer_Login.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void 移除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
                MButton mButton = ChoosedMButton;
                MusicList ListToUpdate = new MusicList();
                //对应歌单中删除所选歌曲
                ChoosedMLButton.MusicList.Remove(mButton.M_music);
                ShowList(ChoosedMLButton.MusicList);
                ListToUpdate = ChoosedMLButton.MusicList;
                //播放列表
                if (PlayingMusicListName == ChoosedMLButton.MusicList.ListName)
                {
                    myMusicApp.PlayingMusicList.Remove(mButton.M_music);
                    myMusicApp.UpdateMusicList(myMusicApp.PlayingMusicList);
                }
                //更新musiclist和数据库
                myMusicApp.UpdateMusicList(ListToUpdate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void 切换账号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                myMusicApp.ResetLogoutInfo();
                Btn_Liked.MusicList = new MusicList(Btn_Liked.MusicList.ListName);
                Btn_History.MusicList = new MusicList(Btn_History.MusicList.ListName);
                Btn_History.Hide();
                Btn_Liked.Hide();
                AutoName = new Dictionary<string, int>();
                Panel_CreatedList.Controls.Clear();
                Panel_CreateList.Hide();
                Btn_Login.Text = "登陆";
                new Login(this).ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void CMS_Main_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                if (myMusicApp.IsLogin)
                {
                    CMS_Main.Items.Remove(登陆ToolStripMenuItem);
                    CMS_Main.Items.Insert(0, 切换账号ToolStripMenuItem);
                }
                else
                {
                    CMS_Main.Items.Remove(切换账号ToolStripMenuItem);
                    CMS_Main.Items.Insert(0, 登陆ToolStripMenuItem);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void 登陆ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                new Login(this).ShowDialog();
            }
            catch(Exception ce)
            {
                Console.WriteLine("2060:" + ce.Message);
            }
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                Search();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void Btn_Search_MouseDown(object sender,MouseEventArgs e)
        {
            try
            {
                TxtBox_SearchBox.Focus();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void Search()
        {
            try
            {
                if(TxtBox_SearchBox.Text.ToString().Trim(' ') != "")
                {
                    if (myMusicApp.NetLinked())
                    {
                        Btn_SearchResult.MusicList.Clear();
                        string searchtext = TxtBox_SearchBox.Text.ToString();
                        Regex regex = new Regex(searchtext, RegexOptions.IgnoreCase);
                        foreach (Music music in myMusicApp.MusicInfo)
                        {
                            Match match = regex.Match(music.Name + "," + music.Singer + "," + music.Album);
                            if (match.Success || searchtext.Contains(music.Name) || searchtext.Contains(music.Singer) || searchtext.Contains(music.Album))
                            {
                                Btn_SearchResult.MusicList.Add(music);
                            }
                        }
                        ResetChoosedMLButton(Btn_SearchResult);
                        ShowList(Btn_SearchResult.MusicList);
                    }
                    Btn_Search.Focus();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void TxtBox_SearchBox_TextChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }


        private void TxtBox_GotFocus(object sender, EventArgs e)
        {
            try
            {
                if(TxtBox_SearchBox.Text.ToString() == "🔍搜索音乐")
                {
                    TxtBox_SearchBox.Text = "";
                }
                Btn_Search.Show();
                Timer_Searching.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void TxtBox_LostFocus(object sender, EventArgs e)
        {
            try
            {
                Timer_Searching.Stop();
                if (TxtBox_SearchBox.Text.ToString().Trim(' ') == "")
                {
                    TxtBox_SearchBox.Text = "🔍搜索音乐";
                    Btn_Search.Hide();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void TxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox mTextBox = (TextBox)sender;
                if (mTextBox.Text.ToString() != "" && e.KeyChar == 13)
                {
                    Search();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void Timer_Searching_Tick(object sender, EventArgs e)
        {
            try
            {
                if (MouseClicked || MouseButtons == MouseButtons.Left || MouseButtons == MouseButtons.Right)
                {
                    MouseClicked = false;
                    if (!InRectangle(new Size(Btn_SearchBorder.Width, Btn_SearchBorder.Height), new Point(Btn_SearchBorder.Location.X + Panel_Tool.Left + this.Left + Main_Panel.Left, Btn_SearchBorder.Location.Y + this.Top + Main_Panel.Top + Panel_Tool.Top)))
                    {
                        ChoosedMLButton.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }


        private void MSBar_MenuList_MValueChanged(object sender, MEventArgs e)
        {
            try
            {
                double value = MSBar_MenuList.M_Value;
                double max = MSBar_MenuList.M_Maximum;
                int height = Panel_MenuList.Height - MSBar_MenuList.Height;
                int delta = (int)(height * value / max);
                Panel_MenuList.Location = new Point(0, -delta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void MSBar_MusicList_MValueChanged(object sender, MEventArgs e)
        {
            try
            {
                double value = MSBar_MusicList.M_Value;
                double max = MSBar_MusicList.M_Maximum;
                //int height = Panel_MusicList.Height - Panel_MusicListScroll.Height;
                int height = Panel_MusicList.Height-(Panel_Detail.Height - MButtonType.Height - MButtonType.Location.Y);
                int delta = (int)(height * value / max);
                Panel_MusicList.Location = new Point(0, -delta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void MSBar_PlayingMusicList_MValueChanged(object sender, MEventArgs e)
        {
            try
            {
                double value = MSBar_PlayingMusicList.M_Value;
                double max = MSBar_PlayingMusicList.M_Maximum;
                int top = MSBar_PlayingMusicList.Location.Y+(int)(value * (MSBar_PlayingMusicList.Height-Slider_PlayingMusicList.Height) / max);
                Slider_PlayingMusicList.Location = new Point(Slider_PlayingMusicList.Location.X, top);
                int delta = (Panel_PlayingMusicList.Controls.Count*ButtonType.Height - MSBar_PlayingMusicList.Height) * (top-MSBar_PlayingMusicList.Location.Y) / (MSBar_PlayingMusicList.Height - Slider_PlayingMusicList.Height);
                Panel_PlayingMusicList.Location = new Point(0, -delta);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void Slider_PlayingMusicList_MouseDown(object sender, EventArgs e)
        {
            try
            {
                Timer_ClosePlayList.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void Slider_PlayingMusicList_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    double max = MSBar_PlayingMusicList.M_Maximum;
                    int center = MousePosition.Y - this.Top - Panel_PlayList.Top;
                    if(center< MSBar_PlayingMusicList.Location.Y + Slider_PlayingMusicList.Height / 2)
                    {
                        center = MSBar_PlayingMusicList.Location.Y + Slider_PlayingMusicList.Height / 2;
                    }
                    if(center> MSBar_PlayingMusicList.Location.Y + MSBar_PlayingMusicList.Height - Slider_PlayingMusicList.Height / 2)
                    {
                        center = MSBar_PlayingMusicList.Location.Y + MSBar_PlayingMusicList.Height - Slider_PlayingMusicList.Height / 2;
                    }
                    int top = center - Slider_PlayingMusicList.Height / 2;
                    Slider_PlayingMusicList.Location = new Point(Slider_PlayingMusicList.Location.X, top);
                    MSBar_PlayingMusicList.M_Value = (top - MSBar_PlayingMusicList.Location.Y) * max / (MSBar_PlayingMusicList.Height - MSBar_PlayingMusicList.M_SliderLength);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void Slider_PlayingMusicList_MouseUp(object sender, EventArgs e)
        {
            try
            {
                Timer_ClosePlayList.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void Form_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                //
                if (InRectangle(new Size(this.Width, 80), new Point(this.Left, this.Top)))
                {
                    return;
                }
                if (MTBar_Volume.Enabled)
                {
                    MTBar_Volume.M_Value += e.Delta * MTBar_Volume.M_Maximum / 1200;
                }
                //音乐列表滑块
                if (MSBar_MusicList.Enabled && ((!MSBar_PlayingMusicList.Enabled&&InRectangle(new Size(Panel_Detail.Width,Panel_Detail.Height),new Point(Panel_Detail.Location.X+Main_Panel.Location.X+this.Left,Panel_Detail.Location.Y+Main_Panel.Location.Y+this.Top))) || (MSBar_PlayingMusicList.Enabled&&InRectangle(new Size(Panel_Detail.Width-300,Panel_Detail.Height),new Point(Panel_Detail.Location.X+Main_Panel.Location.X+this.Left,Panel_Detail.Location.Y+Main_Panel.Location.Y+this.Top)))))
                {
                    double max = MSBar_MusicList.M_Maximum - (MSBar_MusicList.M_SliderLength * MSBar_MusicList.M_Maximum / MSBar_MusicList.Height);
                    int totalHeight = Panel_MusicList.Height - Panel_MusicListScroll.Height;
                    double k = MButtonType.Height * max * 5 / (360 * totalHeight);
                    MSBar_MusicList.M_Value -= e.Delta * k;
                }
                //
                if (MSBar_MenuList.Enabled && InRectangle(new Size(200,this.Height-80),new Point(this.Left,this.Top+80)))
                {
                    MSBar_MenuList.M_Value -= e.Delta * MSBar_MenuList.M_Maximum / 1200;
                }
                //播放列表滑块
                if (MSBar_PlayingMusicList.Enabled && InRectangle(new Size(Panel_PlayingMusicListScroll.Width, Panel_PlayingMusicListScroll.Height), new Point(this.Left + Panel_PlayList.Location.X+Panel_PlayingMusicListScroll.Location.X, this.Top+Panel_PlayList.Location.Y+ Panel_PlayingMusicListScroll.Location.Y)))
                {
                    //滑块可变动值
                    double max = MSBar_PlayingMusicList.M_Maximum - (MSBar_PlayingMusicList.M_SliderLength * MSBar_PlayingMusicList.M_Maximum / MSBar_PlayingMusicList.Height);
                    //内容可滑动高度
                    int totalHeight = Panel_PlayingMusicList.Controls.Count * ButtonType.Height - MSBar_PlayingMusicList.Height;
                    //系数比  三比五:鼠标滚动三次，移动距离等于五个音乐宽度
                    double k = ButtonType.Height * max * 5 / (360 * totalHeight);
                    //
                    MSBar_PlayingMusicList.M_Value -= e.Delta * k;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void Timer_MSBar_MenuList_Tick(object sender, EventArgs e)
        {
            try
            {
                if (MouseClicked ||MouseButtons == MouseButtons.Left || MouseButtons == MouseButtons.Right)
                {
                    MouseClicked = false;
                    if (!InRectangle(new Size(Panel_Volume.Width, Panel_Volume.Height), new Point(Panel_Volume.Location.X + this.Left, Panel_Volume.Location.Y + this.Top)))
                    {
                        MSBar_MenuList.Enabled = false;
                        Timer_MSBar_MenuList.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void Btn_PlayAll_Click(object sender, EventArgs e)
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
                ResetChoosedMButton(Panel_MusicList.Controls[myMusicApp.PlayingMusicList.StartIndex]);
                AWMP.Ctlcontrols.play();
                Timer_Music.Start();
                Btn_Play.Text = "||";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void Timer_Login_Tick(object sender, EventArgs e)
        {
            try
            {
                myMusicApp.InitLoginInfo(user);
                Panel_CreateList.Show();
                InitDefaultList();
                InitCreatedList();
                
                //
                Btn_Login.Enabled = false;
                Btn_Login.Text = "欢迎您: " + user.User_Id;
                //
                Timer_Login.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void 下载ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string url = ChoosedMButton.M_music.FileURL;
                string filename = ChoosedMButton.M_music.Name + " - " + ChoosedMButton.M_music.Singer + ".mp3";
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                using (Stream stream = response.GetResponseStream())
                {
                    //获取文件后缀
                    filename = "../local/" + filename;
                    using (FileStream fileStream = new FileStream(filename, FileMode.CreateNew))
                    {
                        byte[] bArr = new byte[1024];
                        int size = stream.Read(bArr, 0, (int)bArr.Length);
                        while (size > 0)
                        {
                            fileStream.Write(bArr, 0, size);
                            size = stream.Read(bArr, 0, (int)bArr.Length);
                        }
                        fileStream.Close();
                    }
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void MButtonType_Click(object sender, EventArgs e)
        {
            try
            {
                switch (MButtonType.sortWay)
                {
                    case MButton.SortWay.SortByAlbumAscend:
                        break;
                    case MButton.SortWay.SortByAlbumDescend:
                        break;
                    case MButton.SortWay.SortByNameAscend:
                        break;
                    case MButton.SortWay.SortByNameDescend:
                        break;
                    case MButton.SortWay.SortBySingerAscend:
                        break;
                    case MButton.SortWay.SortBySingerDescend:
                        break;
                    case MButton.SortWay.Default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        private void Btn_CurrentMusic_Click(object sender, EventArgs e)
        {

        }

    }
}
