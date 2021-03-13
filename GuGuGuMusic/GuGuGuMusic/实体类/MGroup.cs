using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuGuGuMusic
{
    public enum KindOfGroup
    {
        MGroup,
        LocalMusic,
        PlayingMusic,
        LikedMusic
    }

    public class MGroup
    {
        public MGroup()
        {

        }

        public MGroup(string user_id)
        {

        }

        private string user_id = "";
        /// <summary>
        /// 用户账号
        /// </summary>
        public string User_Id
        {
            get { return user_id; }
            set
            {
                user_id = value;
            }
        }

        public KindOfGroup kindOfGroup = KindOfGroup.MGroup;

        public List<Music> musics = new List<Music>();

        public void Add(Music music)
        {
            try
            {
                Console.WriteLine("00000");

                this.musics.Add(music);
                Console.WriteLine("音乐添加成功");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message + "音乐添加失败");
            }

        }
    }


    //本地和下载
    public class LocalMusic : MGroup
    {
        public LocalMusic()
        {
            kindOfGroup = KindOfGroup.LocalMusic;
        }
    }

    //播放列表
    public class PlayingMusic : MGroup
    {
        public PlayingMusic()
        {
            kindOfGroup = KindOfGroup.PlayingMusic;
        }
    }

    //我喜欢
    public class LikedMusic : MGroup
    {
        public LikedMusic()
        {
            kindOfGroup = KindOfGroup.LikedMusic;
        }
    }
    
}
