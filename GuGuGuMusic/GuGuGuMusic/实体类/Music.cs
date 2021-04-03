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

        #region
        private string _Name = "我的音乐";
        /// <summary>
        /// 音乐名
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
            }
        }

        private string _Singer = "我";
        /// <summary>
        /// 歌手
        /// </summary>
        public string Singer
        {
            get { return _Singer; }
            set
            {
                _Singer = value;
            }
        }

        private string _FileURL = "";
        /// <summary>
        /// 音乐文件地址url
        /// </summary>
        public string FileURL
        {
            get { return _FileURL; }
            set
            {
                _FileURL = value;
            }
        }

        private string _Album = "";
        /// <summary>
        /// 专辑
        /// </summary>
        public string Album
        {
            get { return _Album; }
            set
            {
                _Album = value;
            }
        }
        #endregion

        private string _Duration = "";
        /// <summary>
        /// 时常
        /// </summary>
        public string Duration
        {
            get { return _Duration; }
            set
            {
                _Duration = value;
            }
        }

        /// <summary>
        /// 解析音乐文件保存信息,初始化音乐信息
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public Music ReadLocalMusic(string filename)
        {
            try
            {
                Music music = new Music();
                string[] str = filename.Split('\\');
                string fileurl = "";
                foreach (string s in str)
                {
                    if (s == str.First())
                    {
                        fileurl = s;
                    }
                    else if (s != str.Last())
                    {
                        fileurl = fileurl + "\\" + s;
                    }
                    else if (s == str.Last())
                    {
                        string[] str_ = s.Split('-');
                        music.Singer = str_[0];
                        music.Album = str_[1];
                        music.Name = str_[2];
                    }
                }
                music.FileURL = fileurl;
                return music;
            }
            catch (Exception e)
            {
                Console.WriteLine("6001:" + e.Message);
                return null;
            }
        }

        public string WriteLocalMusic()
        {
            try
            {
                string localstring = "";
                localstring += FileURL + "\\";
                localstring += Singer + "-" + Album + "-" + Name;
                return localstring;
            }
            catch (Exception e)
            {
                Console.WriteLine("6002:" + e.Message);
                return null;
            }
        }

    }
}
