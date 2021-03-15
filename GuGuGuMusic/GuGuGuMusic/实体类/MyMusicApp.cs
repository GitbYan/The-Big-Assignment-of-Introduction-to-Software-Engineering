using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            LocalMusicList = mDB.GetLocalMusicList();
            PlayingMusicList = mDB.GetPlayingMusicList();
            //默认登陆时，读取用户信息
            if (IsLogin)
            {
                HistoryMusicList = mDB.GetMusicList(new MusicList("历史播放列表", user.User_Id));
                CreatedMusicList = mDB.GetMusicList(user);
                for (int i = 0; i < CreatedMusicList.Count(); i++) 
                {
                    CreatedMusicList[i] = mDB.GetMusicList(CreatedMusicList[i]);
                }
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
                if(!File.Exists(path + fileName))
                {
                    FileInfo myFile = new FileInfo(path + fileName);
                    StreamWriter streamWriter = myFile.CreateText();
                    streamWriter.Close();
                    Console.WriteLine("本地存储目录创建成功");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "本地txt创建失败");
            }
        }

        private MDB mDB = new MDB();//连接数据库

        #region 属性
        /// <summary>
        /// 是否登录
        /// </summary>
        public bool IsLogin { get; set; } = false;
        /// <summary>
        /// 当前登陆用户
        /// </summary>
        public User user { get; set; } = new User();
        /// <summary>
        /// 当前播放列表
        /// </summary>
        public MusicList PlayingMusicList { get; set; } = new MusicList("播放列表");
        /// <summary>
        /// 本地和下载列表
        /// </summary>
        public MusicList LocalMusicList { get; set; } = new MusicList("本地与下载");
        /// <summary>
        /// 历史播放列表
        /// </summary>
        public MusicList HistoryMusicList { get; set; } = new MusicList("历史播放列表");
        /// <summary>
        /// 流行音乐列表
        /// </summary>
        public MusicList PopMusicList { get; set; } = new MusicList("流行音乐");
        /// <summary>
        /// 创建的列表 
        /// </summary>
        public MusicList[] CreatedMusicList { get; set; } = new MusicList[100];
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

        #endregion

        #region 歌单部分
        /// <summary>
        /// 创建自定义歌单
        /// </summary>
        /// <returns></returns>
        public bool CreateMusicList()
        {
            return CreatedMusicList.Count() < 100;
        }
        /// <summary>
        /// 对数据库中对应的歌单歌曲进行添加或删除操作,同时更新对应歌单（MusicList）的音乐（Musics）信息
        /// </summary>
        /// <param name="musicList">歌单</param>
        /// <param name="music">歌曲</param>
        /// <param name="listOperation">操作</param>
        public bool UpdateMusicList(MusicList musicList, Music music, ListOperation listOperation)
        {
            try
            {
                if(musicList.ListName.ToString() == "本地与下载")
                {
                    mDB.UpdateLocalMusicList(LocalMusicList, music, listOperation);
                    LocalMusicList.Add(music);
                    Console.WriteLine(musicList.ListName + "添加音乐成功");
                    return true;
                }
                else if (musicList.ListName.ToString() == "播放列表")
                {
                    mDB.UpdatePlayingMusicList(PlayingMusicList, music, listOperation);

                    PlayingMusicList.Add(music);
                    Console.WriteLine(musicList.ListName + "添加音乐成功");
                    return true;
                }
                else
                {
                    int n = 0;
                    switch (listOperation)
                    {
                        case ListOperation.ADD:
                            n = mDB.UpdateMusicList(musicList, music, listOperation);
                            musicList.Add(music);
                            Console.WriteLine(musicList.ListName + "添加音乐成功");
                            break;
                        case ListOperation.DELETE:
                            n = mDB.UpdateMusicList(musicList, music, listOperation);
                            musicList.Remove(music);
                            Console.WriteLine(musicList.ListName + "删除音乐成功");
                            break;
                    }
                    return (n >= 0);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message + musicList.ListName + "音乐增删操作失败");
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
                int n = mDB.UpdateMusicList(musicList, ListName);
                musicList.ListName = ListName;
                Console.WriteLine(musicList.ListName + "重命名成功");
                return (n >= 0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + musicList.ListName + "重命名失败");
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
                Console.WriteLine(musicList.ListName + "歌单删除成功");
                return n;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + musicList.ListName + "歌单删除失败");
                return false;
            }
        }
        #endregion

        #region 播放部分
        /// <summary>
        /// 播放音乐
        /// </summary>
        /// <param name="music"></param>
        public void PlayMusic(Music music)
        {
            try
            {
                ReadyMusic = music;
                UpdateMusicList(PlayingMusicList, ReadyMusic, ListOperation.ADD);
                int index = PlayingMusicList.IndexOf(ReadyMusic);
                int n = PlayingMusicList.Count - 1;
                if (index > 0 && index < n) //ReadyMusic既不是第一个元素也不是最后一个元素
                {
                    LastMusic = PlayingMusicList.Musics[index - 1];
                    NextMusic = PlayingMusicList.Musics[index + 1];
                }
                else if (index == 0 && index < n) //ReadyMusic是第一个元素但不是最后一个元素
                {
                    LastMusic = PlayingMusicList.Musics.Last();
                    NextMusic = PlayingMusicList.Musics[index + 1];
                }
                else if (index == n && index > 0) //ReadyMusic是最后一个元素但不是第一个元素
                {
                    LastMusic = PlayingMusicList.Musics[index - 1];
                    NextMusic = PlayingMusicList.Musics.First();
                }
                else //ReadyMusic既是第一个元素也是最后一个元素
                {
                    NextMusic = LastMusic = ReadyMusic;
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message + "播放失败");
            }
        }
        /// <summary>
        /// 播放下一首
        /// </summary>
        /// <param name="music"></param>
        public void PlayNextMusic()
        {
            PlayMusic(NextMusic);
        }
        /// <summary>
        /// 播放上一首
        /// </summary>
        public void PlayLastMusic()
        {
            PlayMusic(LastMusic);
        }
        #endregion

    }
    

    /// <summary>
    /// 歌单操作种类
    /// </summary>
    public enum ListOperation
    {
        ADD,//添加音乐
        DELETE,//删除音乐
    }



}
