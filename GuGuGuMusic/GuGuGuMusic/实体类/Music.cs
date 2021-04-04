using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * 6
 */
namespace GuGuGuMusic
{
    [Serializable]
    public class Music
    {
        public Music()
        {

        }

        public Music(string Name, string Singer, string Album, string FileURL)
        {
            this.Name = Name;
            this.Singer = Singer;
            this.Album = Album;
            this.FileURL = FileURL;
        }

        #region 属性
        /// <summary>
        /// 音乐名
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 歌手
        /// </summary>
        public string Singer { set; get; }

        /// <summary>
        /// 音乐文件地址url
        /// </summary>
        public string FileURL { set; get; }

        /// <summary>
        /// 专辑
        /// </summary>
        public string Album { set; get; }
        #endregion

    }
}
