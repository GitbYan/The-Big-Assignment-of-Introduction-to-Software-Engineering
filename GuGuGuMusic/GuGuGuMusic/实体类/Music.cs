using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuGuGuMusic
{
    public class Music
    {
        public Music()
        {

        }

        public Music(string FileURL)
        {
            Name = InitMusic(FileURL).Name;
            Singer = InitMusic(FileURL).Singer;
            Album = InitMusic(FileURL).Album;
            FileURL = InitMusic(FileURL).FileURL;
        }

        public Music(string Name,string Singer,string Album,string FileURL)
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
        /// 解析音乐文件路径名并返回音乐对象
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public Music InitMusic(string filename)
        {
            try
            {
                string[] str = filename.Split('\\');
                string fileurl = "";
                string name = "";
                string singer = "";
                foreach (string s in str)
                {
                    if (s != str.Last())
                    {
                        fileurl = fileurl + s + "\\";
                    }
                    if (s == str.Last())
                    {
                        fileurl += s;
                        string[] str_ = s.Split('-');
                        name = str_[1];
                        singer = str_[0];
                    }
                }
                Music music = new Music(name, singer, "", fileurl);
                Console.WriteLine("成功解析音乐路径:" + name + " : " + singer + " : " + fileurl);
                return music;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n" + "解析音乐路径失败");
                return null;
            }
        }
    }
}
