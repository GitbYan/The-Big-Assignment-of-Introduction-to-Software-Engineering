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
        public string Name { set; get; } = "歌曲";

        /// <summary>
        /// 歌手
        /// </summary>
        public string Singer { set; get; } = "歌手";

        /// <summary>
        /// 音乐文件地址url
        /// </summary>
        public string FileURL { set; get; } = "";

        /// <summary>
        /// 专辑
        /// </summary>
        public string Album { set; get; } = "专辑";

        /// <summary>
        /// 是否收藏
        /// </summary>
        public string Stared { get; set; } = "0";
        #endregion

        public override string ToString()
        {
            return Name + "-" + Singer;
        }

        public override bool Equals(object obj)
        {
            Music m = (Music)obj;
            return Name == m.Name && Singer == m.Singer;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
