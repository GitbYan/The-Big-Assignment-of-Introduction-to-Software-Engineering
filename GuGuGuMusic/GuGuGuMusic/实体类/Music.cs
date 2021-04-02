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
    public class Music
    {
        public Music()
        {

        }

        public Music(string FileURL)
        {
            this.Name = InitMusic(FileURL).Name;
            this.Singer = InitMusic(FileURL).Singer;
            this.Album = InitMusic(FileURL).Album;
            this.FileURL = InitMusic(FileURL).FileURL;
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
                        name = str_[1].Split('.')[0];
                        singer = str_[0];
                    }
                }
                Music music = new Music(name, singer, "", fileurl);
                return music;
            }
            catch (Exception e)
            {
                Console.WriteLine("6001:"+e.Message);
                return null;
            }
        }
    }
}
