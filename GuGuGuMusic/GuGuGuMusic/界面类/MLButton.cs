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
        public MLButton(MusicList musicList)
        {
            MusicList = musicList;
        }

        /// <summary>
        /// 保存按钮对应的音乐列表信息
        /// </summary>
        public MusicList MusicList { get; set; } = new MusicList();

        public int index = 0;
    }
   
}
