using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 7
 */
namespace GuGuGuMusic
{
    [Serializable]
    public class MusicList : IList
    {
        public MusicList()
        {

        }

        public MusicList(User user)
        {

        }

        public MusicList(string ListName)
        {
            this.ListName = ListName;
        }

        public MusicList(string ListName, string User_Id)
        {
            this.ListName = ListName;
            this.User_Id = User_Id;
        }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string User_Id { get; set; } = "";

        /// <summary>
        /// 歌单名称
        /// </summary>
        public string ListName { get; set; } = "";

        /// <summary>
        /// 开始播放位置
        /// </summary>
        public int StartIndex { get; set; } = 0;

        public bool IsReadOnly => throw new NotImplementedException();

        public bool IsFixedSize => throw new NotImplementedException();

        public int Count()
        {
            try
            {
                int count = 0;
                foreach (Music music in Musics)
                {
                    if (music.Name != "")
                    {
                        count++;
                    }
                }
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
                return 0;
            }
        }

        public object SyncRoot => throw new NotImplementedException();

        public bool IsSynchronized => throw new NotImplementedException();

        public object this[int index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        /// <summary>
        /// 歌单内容
        /// </summary>
        public List<Music> Musics { get; set; } = new List<Music>();

        int ICollection.Count => throw new NotImplementedException();

        /// <summary>
        /// 向列表中添加音乐,已有返回0，成功返回1，失败返回-1
        /// </summary>
        /// <param name="value">音乐对象</param>
        /// <returns></returns>
        public int Add(object value)
        {
            Music music = (Music)value;
            try
            {
                if (Contains(music))
                {
                    return 0;
                }
                else
                {
                    Musics.Add(music);
                    return 1;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
                return -1;
            }
        }

        /// <summary>
        /// 判断列表中是否已存在该音乐
        /// </summary>
        /// <param name="value">音乐对象</param>
        /// <returns></returns>
        public bool Contains(object value)
        {
            try
            {
                Music music = (Music)value;
                bool IsExist = false;
                foreach (Music m in Musics)
                {
                    if (m.FileURL == music.FileURL)
                    {
                        IsExist = true;
                    }
                    else
                    {
                        continue;
                    }
                }
                return IsExist;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
                return true;
            }
        }

        public void Clear()
        {
            try
            {
                this.Musics.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 返回音乐对象在列表中的序号,未找到返回-1
        /// </summary>
        /// <param name="value">音乐对象</param>
        /// <returns></returns>
        public int IndexOf(object value)
        {
            try
            {
                Music music = (Music)value;
                int i = 0;
                foreach (Music m in Musics)
                {
                    if (m.FileURL == music.FileURL)
                    {
                        return i;
                    }
                }
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
                 return -1;
            }
        }

        public void Insert(int index, object value)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 删除音乐列表中指定音乐
        /// </summary>
        /// <param name="value">音乐对象</param>
        public void Remove(object value)
        {
            try
            {
                Music music = (Music)value;
                Musics.Remove(music);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        public void RemoveAt(int index)
        {
            try
            {
                Musics.RemoveAt(index);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        public void CopyTo(Array array, int index)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 返回循环访问Musics的枚举数
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            try
            {
                return Musics.GetEnumerator();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
 
            return null;
            }
        }
    }

}
