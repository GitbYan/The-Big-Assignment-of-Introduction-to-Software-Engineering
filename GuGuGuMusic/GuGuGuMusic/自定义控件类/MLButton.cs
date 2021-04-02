using System;
using System.Collections.Generic;
using System.Drawing;
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
            FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            Font = new System.Drawing.Font("微軟正黑體 Light", 10F);
            Margin = new System.Windows.Forms.Padding(0);
            Size = new Size(160, 30);
            TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            Cursor = System.Windows.Forms.Cursors.Hand;
        }
        public MLButton(MusicList musicList)
        {
            MusicList = musicList;
            FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            Font = new System.Drawing.Font("微軟正黑體 Light", 10F);
            Margin = new System.Windows.Forms.Padding(0);
            Size = new Size(160, 30);
            TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            Cursor = System.Windows.Forms.Cursors.Hand;
        }

        /// <summary>
        /// 保存按钮对应的音乐列表信息
        /// </summary>
        public MusicList MusicList { get; set; } = new MusicList();

    }
   
}
