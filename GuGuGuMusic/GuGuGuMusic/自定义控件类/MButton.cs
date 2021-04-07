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
    public class MButton:Button
    {
        public enum Type
        {
            Detail,
            Simple,
            Default,//Detail的特殊情况，用于标题排序
        }

        public enum SortWay
        {
            SortByNameAscend,
            SortByNameDescend,
            SortBySingerAscend,
            SortBySingerDescend,
            SortByAlbumAscend,
            SortByAlbumDescend,
            Default,
        }

        public MButton()
        {
            Init();
            CreateControl();
        }

        public MButton(Music music)
        {
            M_music = music;
            Init();
        }

        public void Init()
        {
            //self
            Location = new Point(0,0);
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
            FlatAppearance.BorderColor = BackColor;
            FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ActiveCaption;
            FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveCaption;
        }

        /// <summary>
        /// 按钮布局
        /// </summary>
        [Category("DemoUI"),Description("按钮布局")]
        public Type type { get; set; } = Type.Detail;

        public SortWay sortWay = SortWay.Default;
        private SortWay TmpSortWay = SortWay.Default;

        private bool SortFlag = false;

        private PointF mousePoint = Point.Empty;

        public Color DefaultColor { get; set; } = System.Drawing.SystemColors.ActiveCaption;

        public int MNameLimit
        {
            get
            {
                switch (type)
                {
                    case Type.Detail:
                        return Width * 9 / 20;
                    case Type.Simple:
                        return Width * 7 / 10;
                    default:
                        return Width * 9 / 20;
                }
            }
        }

        public int MSingerLimit
        {
            get
            {
                switch (type)
                {
                    case Type.Detail:
                        return Width / 4;
                    case Type.Simple:
                        return Width * 7 / 10;
                    default:
                        return Width / 4;
                }
            }
        }

        public int MAlbumLimit
        {
            get
            {
                return Width / 4;
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
                    singer =_music.Singer;
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



        public new event EventHandler DoubleClick;
        DateTime clickTime;
        bool isClicked = false;
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (type!=Type.Default)
            {
                if (isClicked)
                {
                    TimeSpan span = DateTime.Now - clickTime;
                    if (span.Milliseconds < SystemInformation.DoubleClickTime)
                    {
                        DoubleClick(this, e);
                        isClicked = false;
                    }
                    else
                    {
                        isClicked = true;
                        clickTime = DateTime.Now;
                    }
                }
                else
                {
                    isClicked = true;
                    clickTime = DateTime.Now;
                }
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (type == Type.Default)
            {
                mousePoint = e.Location;
                SortFlag = true;
                if (mousePoint.X < 30 + MNameLimit)
                {
                    switch (sortWay)
                    {
                        case SortWay.SortByNameAscend:
                            TmpSortWay = SortWay.SortByNameDescend;
                            break;
                        case SortWay.SortByNameDescend:
                            TmpSortWay = SortWay.Default;
                            break;
                        default:
                            TmpSortWay = SortWay.SortByNameAscend;
                            break;
                    }
                }
                else if (mousePoint.X  < 30 + MNameLimit + MSingerLimit)
                {
                    switch (sortWay)
                    {
                        case SortWay.SortBySingerAscend:
                            TmpSortWay = SortWay.SortBySingerDescend;
                            break;
                        case SortWay.SortBySingerDescend:
                            TmpSortWay = SortWay.Default;
                            break;
                        default:
                            TmpSortWay = SortWay.SortBySingerAscend;
                            break;
                    }
                }
                else
                {
                    switch (sortWay)
                    {
                        case SortWay.SortByAlbumAscend:
                            TmpSortWay = SortWay.SortByAlbumDescend;
                            break;
                        case SortWay.SortByAlbumDescend:
                            TmpSortWay = SortWay.Default;
                            break;
                        default:
                            TmpSortWay = SortWay.SortByAlbumAscend;
                            break;
                    }
                }
            }

        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (type == Type.Default)
            {
                bool MouseCase1 = mousePoint.X < 30 + MNameLimit;
                bool ECase1 = (e.Location.X < 30 + MNameLimit) && (e.Location.X > 0);
                bool MouseCase2 = (mousePoint.X > 30 + MNameLimit) && (mousePoint.X < 30 + MNameLimit + MSingerLimit);
                bool ECase2 = (e.Location.X > 30 + MNameLimit) && (e.Location.X < 30 + MNameLimit + MSingerLimit);
                bool MouseCase3 = (mousePoint.X > 30 + MNameLimit + MSingerLimit);
                bool ECase3 = (e.Location.X > 30 + MNameLimit + MSingerLimit) && (e.Location.X < Width);
                if (MouseCase1 && !ECase1)
                {
                    SortFlag = false;
                }
                else if (MouseCase2 && !ECase2)
                {
                    SortFlag = false;
                }
                else if(MouseCase3 && !ECase3)
                {
                    SortFlag = false;
                }
                else
                {
                    sortWay = TmpSortWay;
                    SortFlag = true;
                }
                Invalidate();
            }

        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            Text = "";
            base.OnPaint(pevent);
            SolidBrush brush = new SolidBrush(ForeColor);
            string mname = MName;
            string msinger= MSinger;
            string malbum = MAlbum;
            switch (type)
            {
                case Type.Detail:
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
                    while (pevent.Graphics.MeasureString(malbum, Font).Width > MAlbumLimit)
                    {
                        int length = malbum.Trim('.').Length - 1;
                        malbum = malbum.Trim('.').Substring(0, length) + "...";
                    }
                    pevent.Graphics.DrawString(mname, Font, brush,30,(Height-(int)(Font.GetHeight()))/2);
                    pevent.Graphics.DrawString(msinger, Font, brush,30+ MNameLimit, (Height-(int)(Font.GetHeight()))/2);
                    pevent.Graphics.DrawString(malbum, Font, brush,30+ MNameLimit+ MSingerLimit, (Height-(int)(Font.GetHeight()))/2);
                    break;
                case Type.Simple:
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
                    pevent.Graphics.DrawString(msinger, Font, brush, 10, Height/ 2);
                    break;
                case Type.Default:
                    brush = new SolidBrush(DefaultColor);
                    if (SortFlag)
                    {
                        switch (sortWay)
                        {
                            case SortWay.SortByNameAscend:
                                mname = MName;
                                msinger = MSinger;
                                malbum = MAlbum;
                                mname += "↑";
                                break;
                            case SortWay.SortByNameDescend:
                                mname = MName;
                                msinger = MSinger;
                                malbum = MAlbum;
                                mname += "↓";
                                break;
                            case SortWay.SortBySingerAscend:
                                mname = MName;
                                msinger = MSinger;
                                malbum = MAlbum;
                                msinger += "↑";
                                break;
                            case SortWay.SortBySingerDescend:
                                mname = MName;
                                msinger = MSinger;
                                malbum = MAlbum;
                                msinger += "↓";
                                break;
                            case SortWay.SortByAlbumAscend:
                                mname = MName;
                                msinger = MSinger;
                                malbum = MAlbum;
                                malbum += "↑";
                                break;
                            case SortWay.SortByAlbumDescend:
                                mname = MName;
                                msinger = MSinger;
                                malbum = MAlbum;
                                malbum += "↓";
                                break;
                            case SortWay.Default:
                                mname = MName;
                                msinger = MSinger;
                                malbum = MAlbum;
                                mname += "↕";
                                break;
                        }
                    }
                    pevent.Graphics.DrawString(mname, Font, brush, 30, (Height - (int)(Font.GetHeight())) / 2);
                    pevent.Graphics.DrawString(msinger, Font, brush, 30 + MNameLimit, (Height - (int)(Font.GetHeight())) / 2);
                    pevent.Graphics.DrawString(malbum, Font, brush, 30 + MNameLimit + MSingerLimit, (Height - (int)(Font.GetHeight())) / 2);
                    break;
            }
        }

    }


}
