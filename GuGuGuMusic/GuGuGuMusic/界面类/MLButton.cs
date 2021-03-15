using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuGuGuMusic
{
    public class MLButton:Button
    {
        public MLButton()
        {

        }
        public MLButton(MusicList mGroup)
        {
            MusicList = mGroup;
        }
        public MusicList MusicList { get; set; } = new MusicList();
    }
}
