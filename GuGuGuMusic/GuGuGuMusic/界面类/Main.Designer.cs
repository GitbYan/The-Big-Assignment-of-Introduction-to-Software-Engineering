namespace GuGuGuMusic
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.Main_Panel = new System.Windows.Forms.Panel();
            this.Panel_Detail = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.Panel_Play = new System.Windows.Forms.Panel();
            this.Panel_PlayStatus = new System.Windows.Forms.Panel();
            this.Panel_Control = new System.Windows.Forms.Panel();
            this.Btn_Volume = new System.Windows.Forms.Button();
            this.Btn_Mode = new System.Windows.Forms.Button();
            this.Btn_Next = new System.Windows.Forms.Button();
            this.Btn_Previus = new System.Windows.Forms.Button();
            this.Btn_Play = new System.Windows.Forms.Button();
            this.Btn_MusicName = new System.Windows.Forms.Button();
            this.Btn_MusicPic = new System.Windows.Forms.Button();
            this.Btn_Rate = new System.Windows.Forms.Button();
            this.Btn_Playlist = new System.Windows.Forms.Button();
            this.mTrackBar = new ControlDemos.MTrackBar();
            this.Panel_Tool = new System.Windows.Forms.Panel();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.切换账号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出听听鸽ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_User = new System.Windows.Forms.Button();
            this.Btn_Menu = new System.Windows.Forms.Button();
            this.Btn_Line = new System.Windows.Forms.Button();
            this.Btn_MinSize = new System.Windows.Forms.Button();
            this.Btn_MaxSize = new System.Windows.Forms.Button();
            this.Btn_Close = new System.Windows.Forms.Button();
            this.Panel_Nav = new System.Windows.Forms.Panel();
            this.Panel_List = new System.Windows.Forms.Panel();
            this.Panel_Icon = new System.Windows.Forms.Panel();
            this.Icon_pictureBox = new System.Windows.Forms.PictureBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.Main_Panel.SuspendLayout();
            this.Panel_Detail.SuspendLayout();
            this.Panel_Play.SuspendLayout();
            this.Panel_PlayStatus.SuspendLayout();
            this.Panel_Control.SuspendLayout();
            this.Panel_Tool.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.Panel_Nav.SuspendLayout();
            this.Panel_Icon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Icon_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // Main_Panel
            // 
            this.Main_Panel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Main_Panel.Controls.Add(this.Panel_Detail);
            this.Main_Panel.Controls.Add(this.Panel_Play);
            this.Main_Panel.Controls.Add(this.Panel_Tool);
            this.Main_Panel.Controls.Add(this.Panel_Nav);
            this.Main_Panel.Location = new System.Drawing.Point(4, 4);
            this.Main_Panel.Margin = new System.Windows.Forms.Padding(0);
            this.Main_Panel.MinimumSize = new System.Drawing.Size(900, 640);
            this.Main_Panel.Name = "Main_Panel";
            this.Main_Panel.Size = new System.Drawing.Size(900, 640);
            this.Main_Panel.TabIndex = 0;
            // 
            // Panel_Detail
            // 
            this.Panel_Detail.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Panel_Detail.Controls.Add(this.button1);
            this.Panel_Detail.Location = new System.Drawing.Point(200, 80);
            this.Panel_Detail.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Detail.MinimumSize = new System.Drawing.Size(700, 480);
            this.Panel_Detail.Name = "Panel_Detail";
            this.Panel_Detail.Size = new System.Drawing.Size(700, 480);
            this.Panel_Detail.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "添加音乐";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Panel_Play
            // 
            this.Panel_Play.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Panel_Play.Controls.Add(this.Panel_PlayStatus);
            this.Panel_Play.Controls.Add(this.mTrackBar);
            this.Panel_Play.Location = new System.Drawing.Point(200, 560);
            this.Panel_Play.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Play.MinimumSize = new System.Drawing.Size(700, 80);
            this.Panel_Play.Name = "Panel_Play";
            this.Panel_Play.Size = new System.Drawing.Size(700, 80);
            this.Panel_Play.TabIndex = 1;
            // 
            // Panel_PlayStatus
            // 
            this.Panel_PlayStatus.Controls.Add(this.Panel_Control);
            this.Panel_PlayStatus.Controls.Add(this.Btn_MusicName);
            this.Panel_PlayStatus.Controls.Add(this.Btn_MusicPic);
            this.Panel_PlayStatus.Controls.Add(this.Btn_Rate);
            this.Panel_PlayStatus.Controls.Add(this.Btn_Playlist);
            this.Panel_PlayStatus.Location = new System.Drawing.Point(0, 16);
            this.Panel_PlayStatus.MinimumSize = new System.Drawing.Size(700, 64);
            this.Panel_PlayStatus.Name = "Panel_PlayStatus";
            this.Panel_PlayStatus.Size = new System.Drawing.Size(700, 64);
            this.Panel_PlayStatus.TabIndex = 1;
            // 
            // Panel_Control
            // 
            this.Panel_Control.Controls.Add(this.Btn_Volume);
            this.Panel_Control.Controls.Add(this.Btn_Mode);
            this.Panel_Control.Controls.Add(this.Btn_Next);
            this.Panel_Control.Controls.Add(this.Btn_Previus);
            this.Panel_Control.Controls.Add(this.Btn_Play);
            this.Panel_Control.Location = new System.Drawing.Point(240, 0);
            this.Panel_Control.Name = "Panel_Control";
            this.Panel_Control.Size = new System.Drawing.Size(220, 64);
            this.Panel_Control.TabIndex = 10;
            // 
            // Btn_Volume
            // 
            this.Btn_Volume.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Btn_Volume.FlatAppearance.BorderSize = 0;
            this.Btn_Volume.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Volume.Location = new System.Drawing.Point(180, 0);
            this.Btn_Volume.Name = "Btn_Volume";
            this.Btn_Volume.Size = new System.Drawing.Size(40, 64);
            this.Btn_Volume.TabIndex = 5;
            this.Btn_Volume.TabStop = false;
            this.Btn_Volume.Text = "🔈";
            this.Btn_Volume.UseVisualStyleBackColor = false;
            this.Btn_Volume.Click += new System.EventHandler(this.Btn_Volume_Click);
            // 
            // Btn_Mode
            // 
            this.Btn_Mode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Btn_Mode.FlatAppearance.BorderSize = 0;
            this.Btn_Mode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Mode.Location = new System.Drawing.Point(0, 0);
            this.Btn_Mode.Name = "Btn_Mode";
            this.Btn_Mode.Size = new System.Drawing.Size(40, 64);
            this.Btn_Mode.TabIndex = 9;
            this.Btn_Mode.TabStop = false;
            this.Btn_Mode.Text = "🔁";
            this.Btn_Mode.UseVisualStyleBackColor = false;
            this.Btn_Mode.Click += new System.EventHandler(this.Btn_Mode_Click);
            // 
            // Btn_Next
            // 
            this.Btn_Next.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Btn_Next.FlatAppearance.BorderSize = 0;
            this.Btn_Next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Next.Location = new System.Drawing.Point(140, 0);
            this.Btn_Next.Name = "Btn_Next";
            this.Btn_Next.Size = new System.Drawing.Size(40, 64);
            this.Btn_Next.TabIndex = 6;
            this.Btn_Next.TabStop = false;
            this.Btn_Next.Text = "▶|";
            this.Btn_Next.UseVisualStyleBackColor = false;
            // 
            // Btn_Previus
            // 
            this.Btn_Previus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Btn_Previus.FlatAppearance.BorderSize = 0;
            this.Btn_Previus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Previus.Location = new System.Drawing.Point(40, 0);
            this.Btn_Previus.Name = "Btn_Previus";
            this.Btn_Previus.Size = new System.Drawing.Size(40, 64);
            this.Btn_Previus.TabIndex = 8;
            this.Btn_Previus.TabStop = false;
            this.Btn_Previus.Text = "|◀";
            this.Btn_Previus.UseVisualStyleBackColor = false;
            // 
            // Btn_Play
            // 
            this.Btn_Play.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Btn_Play.FlatAppearance.BorderSize = 0;
            this.Btn_Play.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.Btn_Play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Play.Location = new System.Drawing.Point(80, 0);
            this.Btn_Play.Name = "Btn_Play";
            this.Btn_Play.Size = new System.Drawing.Size(60, 64);
            this.Btn_Play.TabIndex = 7;
            this.Btn_Play.TabStop = false;
            this.Btn_Play.Tag = "0";
            this.Btn_Play.Text = "▶";
            this.toolTip.SetToolTip(this.Btn_Play, "播放");
            this.Btn_Play.UseVisualStyleBackColor = false;
            this.Btn_Play.Click += new System.EventHandler(this.Btn_Play_Click);
            // 
            // Btn_MusicName
            // 
            this.Btn_MusicName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Btn_MusicName.Dock = System.Windows.Forms.DockStyle.Left;
            this.Btn_MusicName.FlatAppearance.BorderSize = 0;
            this.Btn_MusicName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_MusicName.Location = new System.Drawing.Point(75, 0);
            this.Btn_MusicName.Name = "Btn_MusicName";
            this.Btn_MusicName.Size = new System.Drawing.Size(63, 64);
            this.Btn_MusicName.TabIndex = 4;
            this.Btn_MusicName.TabStop = false;
            this.Btn_MusicName.UseVisualStyleBackColor = false;
            // 
            // Btn_MusicPic
            // 
            this.Btn_MusicPic.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Btn_MusicPic.Dock = System.Windows.Forms.DockStyle.Left;
            this.Btn_MusicPic.FlatAppearance.BorderSize = 0;
            this.Btn_MusicPic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_MusicPic.Location = new System.Drawing.Point(0, 0);
            this.Btn_MusicPic.Name = "Btn_MusicPic";
            this.Btn_MusicPic.Size = new System.Drawing.Size(75, 64);
            this.Btn_MusicPic.TabIndex = 3;
            this.Btn_MusicPic.TabStop = false;
            this.Btn_MusicPic.UseVisualStyleBackColor = false;
            // 
            // Btn_Rate
            // 
            this.Btn_Rate.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Btn_Rate.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_Rate.Enabled = false;
            this.Btn_Rate.FlatAppearance.BorderSize = 0;
            this.Btn_Rate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Btn_Rate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Btn_Rate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Rate.Location = new System.Drawing.Point(504, 0);
            this.Btn_Rate.Name = "Btn_Rate";
            this.Btn_Rate.Size = new System.Drawing.Size(121, 64);
            this.Btn_Rate.TabIndex = 2;
            this.Btn_Rate.TabStop = false;
            this.Btn_Rate.UseVisualStyleBackColor = false;
            // 
            // Btn_Playlist
            // 
            this.Btn_Playlist.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_Playlist.FlatAppearance.BorderSize = 0;
            this.Btn_Playlist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Playlist.Font = new System.Drawing.Font("宋体", 20F);
            this.Btn_Playlist.Location = new System.Drawing.Point(625, 0);
            this.Btn_Playlist.Name = "Btn_Playlist";
            this.Btn_Playlist.Size = new System.Drawing.Size(75, 64);
            this.Btn_Playlist.TabIndex = 0;
            this.Btn_Playlist.TabStop = false;
            this.Btn_Playlist.Text = "◱";
            this.Btn_Playlist.UseVisualStyleBackColor = true;
            this.Btn_Playlist.Click += new System.EventHandler(this.Btn_Playlist_Click);
            // 
            // mTrackBar
            // 
            this.mTrackBar.Location = new System.Drawing.Point(0, 2);
            this.mTrackBar.M_BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.mTrackBar.M_BarSize = 3;
            this.mTrackBar.M_CircleRadius = 6;
            this.mTrackBar.M_IsRound = true;
            this.mTrackBar.M_Maximum = 100;
            this.mTrackBar.M_Minimum = 0;
            this.mTrackBar.M_Orientation = ControlDemos.Orientation.Horizontal_LR;
            this.mTrackBar.M_SliderColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(204)))), ((int)(((byte)(108)))));
            this.mTrackBar.M_Value = 50;
            this.mTrackBar.Margin = new System.Windows.Forms.Padding(0);
            this.mTrackBar.MinimumSize = new System.Drawing.Size(700, 3);
            this.mTrackBar.Name = "mTrackBar";
            this.mTrackBar.Size = new System.Drawing.Size(700, 13);
            this.mTrackBar.TabIndex = 0;
            this.mTrackBar.Text = "mTrackBar1";
            // 
            // Panel_Tool
            // 
            this.Panel_Tool.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Panel_Tool.ContextMenuStrip = this.contextMenuStrip;
            this.Panel_Tool.Controls.Add(this.Btn_User);
            this.Panel_Tool.Controls.Add(this.Btn_Menu);
            this.Panel_Tool.Controls.Add(this.Btn_Line);
            this.Panel_Tool.Controls.Add(this.Btn_MinSize);
            this.Panel_Tool.Controls.Add(this.Btn_MaxSize);
            this.Panel_Tool.Controls.Add(this.Btn_Close);
            this.Panel_Tool.Location = new System.Drawing.Point(200, 10);
            this.Panel_Tool.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Tool.MinimumSize = new System.Drawing.Size(700, 60);
            this.Panel_Tool.Name = "Panel_Tool";
            this.Panel_Tool.Size = new System.Drawing.Size(700, 60);
            this.Panel_Tool.TabIndex = 0;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.切换账号ToolStripMenuItem,
            this.退出听听鸽ToolStripMenuItem});
            this.contextMenuStrip.Name = "Icon_contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(137, 48);
            // 
            // 切换账号ToolStripMenuItem
            // 
            this.切换账号ToolStripMenuItem.Name = "切换账号ToolStripMenuItem";
            this.切换账号ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.切换账号ToolStripMenuItem.Text = "切换账号";
            // 
            // 退出听听鸽ToolStripMenuItem
            // 
            this.退出听听鸽ToolStripMenuItem.Name = "退出听听鸽ToolStripMenuItem";
            this.退出听听鸽ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.退出听听鸽ToolStripMenuItem.Text = "退出听听鸽";
            this.退出听听鸽ToolStripMenuItem.Click += new System.EventHandler(this.退出听听鸽ToolStripMenuItem_Click);
            // 
            // Btn_User
            // 
            this.Btn_User.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_User.FlatAppearance.BorderSize = 0;
            this.Btn_User.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_User.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_User.Location = new System.Drawing.Point(435, 0);
            this.Btn_User.Name = "Btn_User";
            this.Btn_User.Size = new System.Drawing.Size(135, 60);
            this.Btn_User.TabIndex = 5;
            this.Btn_User.TabStop = false;
            this.Btn_User.Text = "登陆";
            this.Btn_User.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_User.UseVisualStyleBackColor = true;
            // 
            // Btn_Menu
            // 
            this.Btn_Menu.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_Menu.FlatAppearance.BorderSize = 0;
            this.Btn_Menu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Menu.Location = new System.Drawing.Point(570, 0);
            this.Btn_Menu.Name = "Btn_Menu";
            this.Btn_Menu.Size = new System.Drawing.Size(20, 60);
            this.Btn_Menu.TabIndex = 4;
            this.Btn_Menu.TabStop = false;
            this.Btn_Menu.Text = "≡";
            this.toolTip.SetToolTip(this.Btn_Menu, "主菜单");
            this.Btn_Menu.UseVisualStyleBackColor = true;
            this.Btn_Menu.Click += new System.EventHandler(this.Btn_Menu_Click);
            // 
            // Btn_Line
            // 
            this.Btn_Line.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_Line.Enabled = false;
            this.Btn_Line.FlatAppearance.BorderSize = 0;
            this.Btn_Line.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Line.Location = new System.Drawing.Point(590, 0);
            this.Btn_Line.Name = "Btn_Line";
            this.Btn_Line.Size = new System.Drawing.Size(20, 60);
            this.Btn_Line.TabIndex = 1;
            this.Btn_Line.TabStop = false;
            this.Btn_Line.Text = "|\r\n|\r\n";
            this.Btn_Line.UseVisualStyleBackColor = true;
            // 
            // Btn_MinSize
            // 
            this.Btn_MinSize.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_MinSize.FlatAppearance.BorderSize = 0;
            this.Btn_MinSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_MinSize.Location = new System.Drawing.Point(610, 0);
            this.Btn_MinSize.Name = "Btn_MinSize";
            this.Btn_MinSize.Size = new System.Drawing.Size(20, 60);
            this.Btn_MinSize.TabIndex = 3;
            this.Btn_MinSize.TabStop = false;
            this.Btn_MinSize.Text = "-";
            this.toolTip.SetToolTip(this.Btn_MinSize, "最小化");
            this.Btn_MinSize.UseVisualStyleBackColor = true;
            this.Btn_MinSize.Click += new System.EventHandler(this.Btn_MinSize_Click);
            // 
            // Btn_MaxSize
            // 
            this.Btn_MaxSize.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_MaxSize.FlatAppearance.BorderSize = 0;
            this.Btn_MaxSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_MaxSize.Location = new System.Drawing.Point(630, 0);
            this.Btn_MaxSize.Name = "Btn_MaxSize";
            this.Btn_MaxSize.Size = new System.Drawing.Size(20, 60);
            this.Btn_MaxSize.TabIndex = 2;
            this.Btn_MaxSize.TabStop = false;
            this.Btn_MaxSize.Text = "□";
            this.toolTip.SetToolTip(this.Btn_MaxSize, "最大化");
            this.Btn_MaxSize.UseVisualStyleBackColor = true;
            this.Btn_MaxSize.Click += new System.EventHandler(this.Btn_MaxSize_Click);
            // 
            // Btn_Close
            // 
            this.Btn_Close.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_Close.FlatAppearance.BorderSize = 0;
            this.Btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Close.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.Btn_Close.Location = new System.Drawing.Point(650, 0);
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(50, 60);
            this.Btn_Close.TabIndex = 0;
            this.Btn_Close.TabStop = false;
            this.Btn_Close.Text = "×";
            this.Btn_Close.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip.SetToolTip(this.Btn_Close, "关闭");
            this.Btn_Close.UseVisualStyleBackColor = true;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // Panel_Nav
            // 
            this.Panel_Nav.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Panel_Nav.Controls.Add(this.Panel_List);
            this.Panel_Nav.Controls.Add(this.Panel_Icon);
            this.Panel_Nav.Location = new System.Drawing.Point(0, 0);
            this.Panel_Nav.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Nav.MinimumSize = new System.Drawing.Size(200, 640);
            this.Panel_Nav.Name = "Panel_Nav";
            this.Panel_Nav.Size = new System.Drawing.Size(200, 640);
            this.Panel_Nav.TabIndex = 0;
            // 
            // Panel_List
            // 
            this.Panel_List.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Panel_List.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_List.Location = new System.Drawing.Point(0, 79);
            this.Panel_List.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_List.Name = "Panel_List";
            this.Panel_List.Size = new System.Drawing.Size(200, 558);
            this.Panel_List.TabIndex = 1;
            // 
            // Panel_Icon
            // 
            this.Panel_Icon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Panel_Icon.Controls.Add(this.Icon_pictureBox);
            this.Panel_Icon.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Icon.Location = new System.Drawing.Point(0, 0);
            this.Panel_Icon.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Icon.Name = "Panel_Icon";
            this.Panel_Icon.Size = new System.Drawing.Size(200, 79);
            this.Panel_Icon.TabIndex = 0;
            // 
            // Icon_pictureBox
            // 
            this.Icon_pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Icon_pictureBox.Location = new System.Drawing.Point(49, 10);
            this.Icon_pictureBox.Name = "Icon_pictureBox";
            this.Icon_pictureBox.Size = new System.Drawing.Size(100, 60);
            this.Icon_pictureBox.TabIndex = 0;
            this.Icon_pictureBox.TabStop = false;
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(908, 648);
            this.Controls.Add(this.Main_Panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(908, 648);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.Main_Panel.ResumeLayout(false);
            this.Panel_Detail.ResumeLayout(false);
            this.Panel_Play.ResumeLayout(false);
            this.Panel_PlayStatus.ResumeLayout(false);
            this.Panel_Control.ResumeLayout(false);
            this.Panel_Tool.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.Panel_Nav.ResumeLayout(false);
            this.Panel_Icon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Icon_pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Main_Panel;
        private System.Windows.Forms.Panel Panel_Nav;
        private System.Windows.Forms.Panel Panel_Play;
        private System.Windows.Forms.Panel Panel_Tool;
        private System.Windows.Forms.Panel Panel_Detail;
        private System.Windows.Forms.Panel Panel_Icon;
        private System.Windows.Forms.Panel Panel_List;
        private ControlDemos.MTrackBar mTrackBar;
        private System.Windows.Forms.PictureBox Icon_pictureBox;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 切换账号ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出听听鸽ToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Panel Panel_PlayStatus;
        private System.Windows.Forms.Button Btn_Mode;
        private System.Windows.Forms.Button Btn_Previus;
        private System.Windows.Forms.Button Btn_Play;
        private System.Windows.Forms.Button Btn_Next;
        private System.Windows.Forms.Button Btn_Volume;
        private System.Windows.Forms.Button Btn_MusicName;
        private System.Windows.Forms.Button Btn_MusicPic;
        private System.Windows.Forms.Button Btn_Rate;
        private System.Windows.Forms.Button Btn_User;
        private System.Windows.Forms.Button Btn_Menu;
        private System.Windows.Forms.Button Btn_Line;
        private System.Windows.Forms.Button Btn_MinSize;
        private System.Windows.Forms.Button Btn_MaxSize;
        private System.Windows.Forms.Button Btn_Close;
        private System.Windows.Forms.Button Btn_Playlist;
        private System.Windows.Forms.Panel Panel_Control;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}

