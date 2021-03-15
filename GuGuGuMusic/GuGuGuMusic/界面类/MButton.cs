using System;
using System.Collections.Generic;
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

        }

        public MButton(Music music)
        {
            _music = music;
        }

        private Music _music = new Music();
        public Music M_music
        {
            get { return _music; }
            set
            {
                _music = value;
            }
        }

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
