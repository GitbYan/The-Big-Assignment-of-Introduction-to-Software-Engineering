using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuGuGuMusic
{
    public class MButton:Button
    {
        public MButton()
        {
            FlatStyle = FlatStyle.Flat;
            TabStop = false;
            TextAlign = ContentAlignment.MiddleLeft;
            Cursor = System.Windows.Forms.Cursors.Hand;
        }

        public MButton(Music music)
        {
            _music = music;
            FlatStyle = FlatStyle.Flat;
            TabStop = false;
            TextAlign = ContentAlignment.MiddleLeft;
            Cursor = System.Windows.Forms.Cursors.Hand;
        }

        private Music _music = new Music();

        /// <summary>
        /// 保存按钮对应的音乐信息
        /// </summary>
        public Music M_music
        {
            get { return _music; }
            set
            {
                _music = value;
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


}
