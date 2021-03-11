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
            this.Main_Panel = new System.Windows.Forms.Panel();
            this.Detail_Panel = new System.Windows.Forms.Panel();
            this.Play_Panel = new System.Windows.Forms.Panel();
            this.PlayControl_Panel = new System.Windows.Forms.Panel();
            this.Btn_Playlist = new ControlDemos.MButton();
            this.mTrackBar = new ControlDemos.MTrackBar();
            this.Tool_Panel = new System.Windows.Forms.Panel();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.切换账号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出听听鸽ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_User = new System.Windows.Forms.Button();
            this.Btn_Menu = new ControlDemos.MButton();
            this.Btn_Line = new ControlDemos.MButton();
            this.Btn_MinSize = new ControlDemos.MButton();
            this.Btn_MaxSize = new ControlDemos.MButton();
            this.Btn_Close = new ControlDemos.MButton();
            this.Nav_Panel = new System.Windows.Forms.Panel();
            this.List_Panel = new System.Windows.Forms.Panel();
            this.Icon_Panel = new System.Windows.Forms.Panel();
            this.Icon_pictureBox = new System.Windows.Forms.PictureBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.Btn_Shut = new ControlDemos.MButton();
            this.Playlist_Panel = new System.Windows.Forms.Panel();
            this.shut_panel = new System.Windows.Forms.Panel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Main_Panel.SuspendLayout();
            this.Play_Panel.SuspendLayout();
            this.PlayControl_Panel.SuspendLayout();
            this.Tool_Panel.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.Nav_Panel.SuspendLayout();
            this.Icon_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Icon_pictureBox)).BeginInit();
            this.Playlist_Panel.SuspendLayout();
            this.shut_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Main_Panel
            // 
            this.Main_Panel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Main_Panel.Controls.Add(this.Detail_Panel);
            this.Main_Panel.Controls.Add(this.Play_Panel);
            this.Main_Panel.Controls.Add(this.Tool_Panel);
            this.Main_Panel.Controls.Add(this.Nav_Panel);
            this.Main_Panel.Location = new System.Drawing.Point(4, 4);
            this.Main_Panel.Margin = new System.Windows.Forms.Padding(0);
            this.Main_Panel.MinimumSize = new System.Drawing.Size(900, 640);
            this.Main_Panel.Name = "Main_Panel";
            this.Main_Panel.Size = new System.Drawing.Size(900, 640);
            this.Main_Panel.TabIndex = 0;
            // 
            // Detail_Panel
            // 
            this.Detail_Panel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Detail_Panel.Location = new System.Drawing.Point(200, 80);
            this.Detail_Panel.Margin = new System.Windows.Forms.Padding(0);
            this.Detail_Panel.MinimumSize = new System.Drawing.Size(700, 480);
            this.Detail_Panel.Name = "Detail_Panel";
            this.Detail_Panel.Size = new System.Drawing.Size(700, 480);
            this.Detail_Panel.TabIndex = 2;
            // 
            // Play_Panel
            // 
            this.Play_Panel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Play_Panel.Controls.Add(this.PlayControl_Panel);
            this.Play_Panel.Controls.Add(this.mTrackBar);
            this.Play_Panel.Location = new System.Drawing.Point(200, 560);
            this.Play_Panel.Margin = new System.Windows.Forms.Padding(0);
            this.Play_Panel.MinimumSize = new System.Drawing.Size(700, 80);
            this.Play_Panel.Name = "Play_Panel";
            this.Play_Panel.Size = new System.Drawing.Size(700, 80);
            this.Play_Panel.TabIndex = 1;
            // 
            // PlayControl_Panel
            // 
            this.PlayControl_Panel.Controls.Add(this.Btn_Playlist);
            this.PlayControl_Panel.Location = new System.Drawing.Point(0, 16);
            this.PlayControl_Panel.MinimumSize = new System.Drawing.Size(700, 64);
            this.PlayControl_Panel.Name = "PlayControl_Panel";
            this.PlayControl_Panel.Size = new System.Drawing.Size(700, 64);
            this.PlayControl_Panel.TabIndex = 1;
            // 
            // Btn_Playlist
            // 
            this.Btn_Playlist.BackColor = System.Drawing.Color.Transparent;
            this.Btn_Playlist.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_Playlist.FlatAppearance.BorderSize = 0;
            this.Btn_Playlist.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Btn_Playlist.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Btn_Playlist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Playlist.Location = new System.Drawing.Point(630, 0);
            this.Btn_Playlist.M_Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Btn_Playlist.M_HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Btn_Playlist.Name = "Btn_Playlist";
            this.Btn_Playlist.Size = new System.Drawing.Size(70, 64);
            this.Btn_Playlist.TabIndex = 1;
            this.Btn_Playlist.TabStop = false;
            this.Btn_Playlist.Tag = "";
            this.toolTip.SetToolTip(this.Btn_Playlist, "关闭");
            this.Btn_Playlist.UseVisualStyleBackColor = false;
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
            // Tool_Panel
            // 
            this.Tool_Panel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Tool_Panel.ContextMenuStrip = this.contextMenuStrip;
            this.Tool_Panel.Controls.Add(this.Btn_User);
            this.Tool_Panel.Controls.Add(this.Btn_Menu);
            this.Tool_Panel.Controls.Add(this.Btn_Line);
            this.Tool_Panel.Controls.Add(this.Btn_MinSize);
            this.Tool_Panel.Controls.Add(this.Btn_MaxSize);
            this.Tool_Panel.Controls.Add(this.Btn_Close);
            this.Tool_Panel.Location = new System.Drawing.Point(200, 10);
            this.Tool_Panel.Margin = new System.Windows.Forms.Padding(0);
            this.Tool_Panel.MinimumSize = new System.Drawing.Size(700, 60);
            this.Tool_Panel.Name = "Tool_Panel";
            this.Tool_Panel.Size = new System.Drawing.Size(700, 60);
            this.Tool_Panel.TabIndex = 0;
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
            this.Btn_User.BackColor = System.Drawing.Color.Transparent;
            this.Btn_User.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_User.FlatAppearance.BorderSize = 0;
            this.Btn_User.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Btn_User.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Btn_User.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_User.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_User.Location = new System.Drawing.Point(435, 0);
            this.Btn_User.Name = "Btn_User";
            this.Btn_User.Size = new System.Drawing.Size(135, 60);
            this.Btn_User.TabIndex = 5;
            this.Btn_User.TabStop = false;
            this.Btn_User.Text = "小鼠凉茶";
            this.Btn_User.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_User.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Btn_User.UseVisualStyleBackColor = false;
            // 
            // Btn_Menu
            // 
            this.Btn_Menu.BackColor = System.Drawing.Color.Transparent;
            this.Btn_Menu.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_Menu.FlatAppearance.BorderSize = 0;
            this.Btn_Menu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Btn_Menu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Btn_Menu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Menu.ForeColor = System.Drawing.Color.Transparent;
            this.Btn_Menu.Location = new System.Drawing.Point(570, 0);
            this.Btn_Menu.M_Color = System.Drawing.Color.Black;
            this.Btn_Menu.M_HoverColor = System.Drawing.Color.Black;
            this.Btn_Menu.Name = "Btn_Menu";
            this.Btn_Menu.Size = new System.Drawing.Size(20, 60);
            this.Btn_Menu.TabIndex = 4;
            this.Btn_Menu.TabStop = false;
            this.toolTip.SetToolTip(this.Btn_Menu, "主菜单");
            this.Btn_Menu.UseVisualStyleBackColor = false;
            this.Btn_Menu.Click += new System.EventHandler(this.Btn_Menu_Click);
            // 
            // Btn_Line
            // 
            this.Btn_Line.BackColor = System.Drawing.Color.Transparent;
            this.Btn_Line.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_Line.Enabled = false;
            this.Btn_Line.FlatAppearance.BorderSize = 0;
            this.Btn_Line.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Btn_Line.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Btn_Line.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Line.Location = new System.Drawing.Point(590, 0);
            this.Btn_Line.M_Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Btn_Line.M_HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Btn_Line.Name = "Btn_Line";
            this.Btn_Line.Size = new System.Drawing.Size(20, 60);
            this.Btn_Line.TabIndex = 3;
            this.Btn_Line.TabStop = false;
            this.Btn_Line.UseVisualStyleBackColor = false;
            // 
            // Btn_MinSize
            // 
            this.Btn_MinSize.BackColor = System.Drawing.Color.Transparent;
            this.Btn_MinSize.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_MinSize.FlatAppearance.BorderSize = 0;
            this.Btn_MinSize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Btn_MinSize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Btn_MinSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_MinSize.Location = new System.Drawing.Point(610, 0);
            this.Btn_MinSize.M_Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Btn_MinSize.M_HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Btn_MinSize.Name = "Btn_MinSize";
            this.Btn_MinSize.Size = new System.Drawing.Size(20, 60);
            this.Btn_MinSize.TabIndex = 2;
            this.Btn_MinSize.TabStop = false;
            this.toolTip.SetToolTip(this.Btn_MinSize, "最小化");
            this.Btn_MinSize.UseVisualStyleBackColor = false;
            this.Btn_MinSize.Click += new System.EventHandler(this.Btn_MinSize_Click);
            // 
            // Btn_MaxSize
            // 
            this.Btn_MaxSize.BackColor = System.Drawing.Color.Transparent;
            this.Btn_MaxSize.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_MaxSize.FlatAppearance.BorderSize = 0;
            this.Btn_MaxSize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Btn_MaxSize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Btn_MaxSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_MaxSize.Location = new System.Drawing.Point(630, 0);
            this.Btn_MaxSize.M_Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Btn_MaxSize.M_HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Btn_MaxSize.Name = "Btn_MaxSize";
            this.Btn_MaxSize.Size = new System.Drawing.Size(20, 60);
            this.Btn_MaxSize.TabIndex = 1;
            this.Btn_MaxSize.TabStop = false;
            this.toolTip.SetToolTip(this.Btn_MaxSize, "最大化");
            this.Btn_MaxSize.UseVisualStyleBackColor = false;
            this.Btn_MaxSize.Click += new System.EventHandler(this.Btn_MaxSize_Click);
            // 
            // Btn_Close
            // 
            this.Btn_Close.BackColor = System.Drawing.Color.Transparent;
            this.Btn_Close.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_Close.FlatAppearance.BorderSize = 0;
            this.Btn_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Btn_Close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Close.Location = new System.Drawing.Point(650, 0);
            this.Btn_Close.M_Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Btn_Close.M_HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Btn_Close.Name = "Btn_Close";
            this.Btn_Close.Size = new System.Drawing.Size(50, 60);
            this.Btn_Close.TabIndex = 0;
            this.Btn_Close.TabStop = false;
            this.Btn_Close.Tag = "";
            this.toolTip.SetToolTip(this.Btn_Close, "关闭");
            this.Btn_Close.UseVisualStyleBackColor = false;
            this.Btn_Close.Click += new System.EventHandler(this.Btn_Close_Click);
            // 
            // Nav_Panel
            // 
            this.Nav_Panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Nav_Panel.Controls.Add(this.List_Panel);
            this.Nav_Panel.Controls.Add(this.Icon_Panel);
            this.Nav_Panel.Location = new System.Drawing.Point(0, 0);
            this.Nav_Panel.Margin = new System.Windows.Forms.Padding(0);
            this.Nav_Panel.MinimumSize = new System.Drawing.Size(200, 640);
            this.Nav_Panel.Name = "Nav_Panel";
            this.Nav_Panel.Size = new System.Drawing.Size(200, 640);
            this.Nav_Panel.TabIndex = 0;
            // 
            // List_Panel
            // 
            this.List_Panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.List_Panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.List_Panel.Location = new System.Drawing.Point(0, 79);
            this.List_Panel.Margin = new System.Windows.Forms.Padding(0);
            this.List_Panel.Name = "List_Panel";
            this.List_Panel.Size = new System.Drawing.Size(200, 558);
            this.List_Panel.TabIndex = 1;
            // 
            // Icon_Panel
            // 
            this.Icon_Panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Icon_Panel.Controls.Add(this.Icon_pictureBox);
            this.Icon_Panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Icon_Panel.Location = new System.Drawing.Point(0, 0);
            this.Icon_Panel.Margin = new System.Windows.Forms.Padding(0);
            this.Icon_Panel.Name = "Icon_Panel";
            this.Icon_Panel.Size = new System.Drawing.Size(200, 79);
            this.Icon_Panel.TabIndex = 0;
            // 
            // Icon_pictureBox
            // 
            this.Icon_pictureBox.Location = new System.Drawing.Point(49, 10);
            this.Icon_pictureBox.Name = "Icon_pictureBox";
            this.Icon_pictureBox.Size = new System.Drawing.Size(100, 60);
            this.Icon_pictureBox.TabIndex = 0;
            this.Icon_pictureBox.TabStop = false;
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // Btn_Shut
            // 
            this.Btn_Shut.BackColor = System.Drawing.Color.Transparent;
            this.Btn_Shut.Dock = System.Windows.Forms.DockStyle.Right;
            this.Btn_Shut.FlatAppearance.BorderSize = 0;
            this.Btn_Shut.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Btn_Shut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Btn_Shut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Shut.Location = new System.Drawing.Point(228, 0);
            this.Btn_Shut.M_Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Btn_Shut.M_HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Btn_Shut.Name = "Btn_Shut";
            this.Btn_Shut.Size = new System.Drawing.Size(70, 56);
            this.Btn_Shut.TabIndex = 2;
            this.Btn_Shut.TabStop = false;
            this.Btn_Shut.Tag = "";
            this.toolTip.SetToolTip(this.Btn_Shut, "关闭");
            this.Btn_Shut.UseVisualStyleBackColor = false;
            this.Btn_Shut.Click += new System.EventHandler(this.Btn_Shut_Click);
            // 
            // Playlist_Panel
            // 
            this.Playlist_Panel.Controls.Add(this.shut_panel);
            this.Playlist_Panel.Dock = System.Windows.Forms.DockStyle.Right;
            this.Playlist_Panel.Location = new System.Drawing.Point(608, 0);
            this.Playlist_Panel.Name = "Playlist_Panel";
            this.Playlist_Panel.Size = new System.Drawing.Size(300, 648);
            this.Playlist_Panel.TabIndex = 1;
            this.Playlist_Panel.Visible = false;
            // 
            // shut_panel
            // 
            this.shut_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.shut_panel.Controls.Add(this.Btn_Shut);
            this.shut_panel.Location = new System.Drawing.Point(0, 590);
            this.shut_panel.Name = "shut_panel";
            this.shut_panel.Size = new System.Drawing.Size(300, 58);
            this.shut_panel.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(908, 648);
            this.Controls.Add(this.Main_Panel);
            this.Controls.Add(this.Playlist_Panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(908, 648);
            this.Name = "Main";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.Main_Panel.ResumeLayout(false);
            this.Play_Panel.ResumeLayout(false);
            this.PlayControl_Panel.ResumeLayout(false);
            this.Tool_Panel.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.Nav_Panel.ResumeLayout(false);
            this.Icon_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Icon_pictureBox)).EndInit();
            this.Playlist_Panel.ResumeLayout(false);
            this.shut_panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Main_Panel;
        private System.Windows.Forms.Panel Nav_Panel;
        private System.Windows.Forms.Panel Play_Panel;
        private System.Windows.Forms.Panel Tool_Panel;
        private System.Windows.Forms.Panel Detail_Panel;
        private System.Windows.Forms.Panel Icon_Panel;
        private System.Windows.Forms.Panel List_Panel;
        private ControlDemos.MTrackBar mTrackBar;
        private System.Windows.Forms.PictureBox Icon_pictureBox;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 切换账号ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出听听鸽ToolStripMenuItem;
        private System.Windows.Forms.Button Btn_User;
        private System.Windows.Forms.ToolTip toolTip;
        private ControlDemos.MButton Btn_Close;
        private ControlDemos.MButton Btn_MaxSize;
        private ControlDemos.MButton Btn_MinSize;
        private ControlDemos.MButton Btn_Line;
        private ControlDemos.MButton Btn_Menu;
        private System.Windows.Forms.Panel PlayControl_Panel;
        private ControlDemos.MButton Btn_Playlist;
        private System.Windows.Forms.Panel Playlist_Panel;
        private System.Windows.Forms.Panel shut_panel;
        private ControlDemos.MButton Btn_Shut;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

