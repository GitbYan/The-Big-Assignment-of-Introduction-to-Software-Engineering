using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuGuGuMusic
{
    public class MButtonV2 : Button
    {
        public MButtonV2()
        {
            CreateControl();
            Init();
        }

        public MButtonV2(Music music)
        {
            M_music = music;
            Init();
        }

        public void Init()
        {
            //self
            Location = new Point(0, 0);
            Size = new Size(640, 30);
            ForeColor = System.Drawing.SystemColors.ControlText;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            TabStop = false;
            TextAlign = ContentAlignment.MiddleLeft;
            Text = "";
            Cursor = System.Windows.Forms.Cursors.Hand;
            Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            Margin = new System.Windows.Forms.Padding(0);
            Padding = new System.Windows.Forms.Padding(0);
            FlatAppearance.MouseDownBackColor = BackColor;
            FlatAppearance.MouseOverBackColor = BackColor;
        }
        private PointF mousePoint = Point.Empty;

        public Color DefaultColor { get; set; } = System.Drawing.SystemColors.ActiveCaption;

        public int MNameLimit
        {
            get
            {
                return Width * 7 / 10;
            }
        }

        public int MSingerLimit
        {
            get
            {
                return Width * 7 / 10;
            }
        }

        public string MName
        {
            get
            {
                string name = "歌曲";
                if (_music != null)
                {
                    name = _music.Name;
                }
                return name;
            }
        }

        public string MSinger
        {
            get
            {
                string singer = "歌手";
                if (_music != null)
                {
                    singer = _music.Singer;
                }
                return singer;
            }
        }

        public string MAlbum
        {
            get
            {
                string album = "专辑";
                if (_music != null)
                {
                    album = _music.Album;
                }
                return album;
            }
        }


        private Music _music = new Music();
        /// <summary>
        /// 保存按钮对应的音乐信息
        /// </summary>
        public Music M_music
        {
            get
            {
                return _music;
            }
            set
            {
                _music = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 保存音乐在歌单的位置
        /// </summary>
        public int Index { get; set; }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            mousePoint = e.Location;
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            Text = "";
            base.OnPaint(pevent);
            SolidBrush brush = new SolidBrush(ForeColor);
            string mname = MName;
            string msinger = MSinger;
            while (pevent.Graphics.MeasureString(mname, Font).Width > MNameLimit)
            {
                int length = mname.Trim('.').Length - 1;
                mname = mname.Trim('.').Substring(0, length) + "...";
            }
            while (pevent.Graphics.MeasureString(msinger, Font).Width > MSingerLimit)
            {
                int length = msinger.Trim('.').Length - 1;
                msinger = msinger.Trim('.').Substring(0, length) + "...";
            }
            pevent.Graphics.DrawString(mname, Font, brush, 10, (Height - (int)(2 * Font.GetHeight())) / 2);
            brush = new SolidBrush(DefaultColor);
            pevent.Graphics.DrawString(msinger, Font, brush, 10, Height / 2);
                  
        }

    }


}
