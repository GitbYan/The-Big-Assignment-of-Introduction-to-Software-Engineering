using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

/*
 * 5
 */
namespace GuGuGuMusic
{
    public class MyMusicApp
    {
        public MyMusicApp()
        {
            //初始化本地存储目录
            WriteLocalTxt("local.txt", "../local/");
            WriteLocalTxt("playing.txt", "../local/");
            //读取本地目录信息
            LocalMusicList = mDB.GetLocalMusicList(LocalMusicList);
            PlayingMusicList = mDB.GetLocalMusicList(PlayingMusicList);
            //默认登陆时，读取用户信息(目前是无用代码)
            if (IsLogin)
            {
                InitLoginInfo(user);
            }
            if (Connected = NetLinked())
            {
                MusicInfo = mDB.GetMusics();
            }
        }

        public bool NetLinked()
        {
            try
            {
                WebRequest myrequest = WebRequest.Create("http://www.baidu.com");
                WebResponse webResponse = myrequest.GetResponse();
                webResponse.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("5009:" + e.Message);
                return false;
            }
        }

        public void InitOnlineMusics()
        {
            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine("5010:" + e.Message);
            }
        }

        public void InitLoginInfo(User user)
        {
            try
            {
                IsLogin = true;
                this.user = user;
                //defaultlist
                HistoryMusicList.User_Id = user.User_Id;
                HistoryMusicList = mDB.GetMusicList(HistoryMusicList);
                LikedMusicList.User_Id = user.User_Id;
                LikedMusicList = mDB.GetMusicList(LikedMusicList);
                //createdlist
                AllMusicLists = mDB.GetAllMusicList(user);
                for (int i = 0; i < AllMusicLists.Count(); i++)
                {
                    if (DefaultList.Contains(AllMusicLists[i].ListName))
                    {
                        continue;
                    }
                    CreatedMusicList.Add(mDB.GetMusicList(AllMusicLists[i]).ListName, i);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("5001:" + e.Message);
            }
        }

        public void ResetLogoutInfo()
        {
            try
            {
                IsLogin = false;
                this.user = new User();
                HistoryMusicList = new MusicList("播放历史");
                LikedMusicList = new MusicList("我喜欢");
                AllMusicLists = new List<MusicList>();
                CreatedMusicList = new Dictionary<string, int>();

            }
            catch (Exception e)
            {
                Console.WriteLine("5009:" + e.Message);
            }
        }

        /// <summary>
        /// 创建本地存储目录
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="path"></param>
        public void WriteLocalTxt(string fileName, string path)
        {
            // 如果文件不存在，创建文件； 如果存在，覆盖文件 
            try
            {
                if (!Directory.Exists(path))
                {
                    //如果不存在就创建file文件夹
                    Directory.CreateDirectory(path);
                }
                if (!File.Exists(path + fileName))
                {
                    FileInfo myFile = new FileInfo(path + fileName);
                    StreamWriter streamWriter = myFile.CreateText();
                    streamWriter.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("5002:" + e.Message);
            }
        }
        /// <summary>
        /// 数据库连接
        /// </summary>
        private MDB mDB = new MDB();
        /// <summary>
        /// 自定义歌单数量上限
        /// </summary>
        public static readonly int ListLimits = 20;
        #region 属性
        /// <summary>
        /// 是否登录
        /// </summary>
        public bool IsLogin { get; set; } = false;
        /// <summary>
        /// 是否联网
        /// </summary>
        public bool Connected { get; set; } = false;
        /// <summary>
        /// 当前登陆用户
        /// </summary>
        public User user { get; set; } = new User();
        public static readonly List<string> DefaultList = new List<string>
        {
            "播放列表",
            "本地与下载",
            "播放历史",
            "流行音乐",
            "我喜欢"
        };
        /// <summary>
        /// 当前播放列表 -- 绑定本地
        /// </summary>
        public MusicList PlayingMusicList { get; set; } = new MusicList("播放列表");
        /// <summary>
        /// 本地和下载列表 -- 绑定本地
        /// </summary>
        public MusicList LocalMusicList { get; set; } = new MusicList("本地与下载");
        /// <summary>
        /// 历史播放列表 -- 绑定用户
        /// </summary>
        public MusicList HistoryMusicList { get; set; } = new MusicList("播放历史");
        /// <summary>
        /// 流行音乐列表
        /// </summary>
        public MusicList PopMusicList { get; set; } = new MusicList("流行音乐");
        /// <summary>
        /// 我喜欢列表 -- 绑定用户
        /// </summary>
        public MusicList LikedMusicList { get; set; } = new MusicList("我喜欢");
        /// <summary>
        /// 创建的列表 储存musiclist名以及对应allmusiclist中的index
        /// </summary>
        public Dictionary<string, int> CreatedMusicList { get; set; } = new Dictionary<string, int>();
        /// <summary>
        /// 创建的列表
        /// </summary>
        public List<MusicList> AllMusicLists { get; set; } = new List<MusicList>();
        /// <summary>
        /// 正在播放的音乐
        /// </summary>
        public Music ReadyMusic { get; set; } = new Music();
        /// <summary>
        /// 播放列表中上一首音乐
        /// </summary>
        public Music LastMusic { get; set; } = new Music();
        /// <summary>
        /// 播放列表中下一首音乐
        /// </summary>
        public Music NextMusic { get; set; } = new Music();
        /// <summary>
        /// 歌曲库
        /// </summary>
        public List<Music> MusicInfo { get; set; } = new List<Music>();
        #endregion

        #region 歌单部分
        /// <summary>
        /// 判断是否能继续创建自定义歌单
        /// </summary>
        /// <returns></returns>
        public bool CanCreateMusicList()
        {
            try
            {
                return GetCreatedMusicListNumber() < ListLimits;
            }
            catch (Exception e)
            {
                Console.WriteLine("5003:" + e.Message);
                return false;
            }
        }
        /// <summary>
        /// 查询自定义歌单数量
        /// </summary>
        /// <returns></returns>
        public int GetCreatedMusicListNumber()
        {
            try
            {
                return CreatedMusicList.Count();
            }
            catch (Exception e)
            {
                Console.WriteLine("5004:" + e.Message);
                return -1;
            }
        }
        /// <summary>
        /// 创建自定义歌单
        /// </summary>
        /// <param name="musicList"></param>
        /// <returns></returns>
        public bool CreateMusicList(MusicList musicList)
        {
            try
            {
                int n = 0;
                if (GetCreatedMusicListNumber() < ListLimits)
                {
                    n = mDB.CreateMusicList(musicList);
                    int i = AllMusicLists.Count();
                    AllMusicLists.Add(musicList);
                    CreatedMusicList.Add(musicList.ListName, i);
                }
                return n >= 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("5005:" + e.Message);
                return false;
            }
        }
        /// <summary>
        /// 更新对应歌单（MusicList）的相关信息
        /// </summary>
        /// <param name="musicList">歌单</param>
        public bool UpdateMusicList(MusicList musicList)
        {
            try
            {
                if (musicList != null)
                {
                    int n = 0;
                    n = mDB.UpdateMusicList(musicList);
                    return (n >= 0);
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("5006:" + e.Message);
                return false;
            }
        }
        /// <summary>
        /// 修改数据库中歌单名
        /// </summary>
        /// <param name="musicList"></param>
        /// <param name="ListName"></param>
        public bool UpdateMusicList(MusicList musicList, string ListName)
        {
            try
            {
                if (musicList != null)
                {
                    int index = CreatedMusicList[musicList.ListName];
                    CreatedMusicList.Remove(musicList.ListName);
                    int n = mDB.UpdateMusicList(musicList, ListName);
                    musicList.ListName = ListName;
                    AllMusicLists[index].ListName = ListName;
                    CreatedMusicList.Add(musicList.ListName, index);
                    return (n >= 0);
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("5007:" + e.Message);
                return false;
            }
        }
        /// <summary>
        /// 删除数据库中歌单
        /// </summary>
        /// <param name="musicList"></param>
        /// <returns></returns>
        public bool DeleteMusicList(MusicList musicList)
        {
            try
            {
                bool n = mDB.DeleteMusicList(musicList);
                CreatedMusicList.Remove(musicList.ListName);
                return n;
            }
            catch (Exception e)
            {
                Console.WriteLine("5008:" + e.Message);
                return false;
            }
        }
        #endregion


    }





}
