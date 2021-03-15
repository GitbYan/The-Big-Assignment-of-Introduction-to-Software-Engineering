using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

/*
 * 
 * TABLE STRUCTURE:
 *      musicinfo()
 *      userinfo()
 *      musiclistinfo(Listname CHAR(255) PRI, user_id CHAR(255) PRI, Fileurl CHAR(255) PRI)
 *      
 */

namespace GuGuGuMusic
{
    public class MDB
    {
        public DataSet MusicInfoDataSet = new DataSet();

        [Obsolete]
        private static string _DBConnString = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"];
        /// <summary>
        /// 数据库连接设置
        /// </summary>
        public static string DBConnString
        {
            get { return _DBConnString; }
        }

        public MySqlConnection connection()
        {
            MySqlConnection conn = new MySqlConnection(DBConnString);
            try
            {
                conn.Open();
                Console.WriteLine("数据库连接成功");
                return conn;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("数据库连接失败");
                return null;
            }
        }

        /// <summary>
        /// Initialize a new instance of the MySqlCommand class with the text of the query and a MySqlConnection.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public MySqlCommand Command(string sql)
        {
            MySqlCommand command = new MySqlCommand(sql, connection());
            return command;
        }

        /// <summary>
        /// Executes a SQL statement against the connection and returns the number of rows affected.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExcuteNonQuery(string sql)
        {
            return Command(sql).ExecuteNonQuery();
        }

        /// <summary>
        /// Sends the MySqlCommand.CommandText to the MySqlConnetion and builds a MySqlDataReader.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public MySqlDataReader Read(string sql)
        {
            return Command(sql).ExecuteReader();
        }

        public DataSet GetUserInfo()
        {
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter("select * from userinfo", connection());
            DataSet userInfoDataSet = new DataSet();
            mySqlDataAdapter.Fill(userInfoDataSet, "userinfo");
            return userInfoDataSet;
        }

        public bool AddMusic(Music music)
        {
            try
            {
                string sql = "INSERT INTO musicinfo VALUES('" + music.Name + "','" + music.Singer + "','" + music.FileURL.Replace("\\", "/") + "','"+music.Album + "') ON DUPLICATE KEY UPDATE Fileurl = VALUES(Fileurl)";
                int n = ExcuteNonQuery(sql);
                Console.WriteLine("数据库添加成功，修改数据条数" + n);
                return true;

            }catch(Exception e)
            {
                Console.WriteLine(e.Message + "数据库添加失败");
                return false;
            }
        }

        public List<Music> GetMusics()
        {
            try
            {
                string sql = "SELECT * FROM musicinfo";
                MySqlDataReader rd = Read(sql);
                List<Music> Musics = new List<Music>();
                while (rd.Read())
                {
                    Music music = new Music(rd["Name"].ToString(), rd["Singer"].ToString(), rd["Album"].ToString(), rd["Fileurl"].ToString().Replace("/", "\\"));
                    Musics.Add(music);
                }
                Console.WriteLine("读取数据库成功");
                return Musics;

            } catch(Exception e)
            {
                Console.WriteLine(e.Message + "读取数据库失败");
                return null;
            }
        }

        #region 歌单部分
        /// <summary>
        /// 修改数据库对应音乐表，返回操作涉及的项数，失败返回-1
        /// </summary>
        /// <param name="musicList">列表对象</param>
        /// <param name="music">音乐对象</param>
        /// <returns></returns>
        public int UpdateMusicList(MusicList musicList,Music music, ListOperation listOperation)
        {
            try
            {
                int n = 0;
                switch (listOperation)
                {
                    case ListOperation.ADD:
                        string sql1 = "INSERT INTO musiclistinfo VALUES('" + musicList.ListName + "','" + musicList.User_Id + "','" + music.FileURL.Replace("\\", "/") + "') ON DUPLICATE KEY UPDATE Fileurl = '" + music.FileURL.Replace("\\", "/") + "'";
                        n = ExcuteNonQuery(sql1);
                        break;
                    case ListOperation.DELETE:
                        string sql2 = "DELETE FROM musiclistinfo WHERE user_id = '" + musicList.User_Id + "' and Listname = '" + musicList.ListName + "' and Fileurl = '" + music.FileURL + "'";
                        n = ExcuteNonQuery(sql2);
                        break;                        
                }
                Console.WriteLine("数据库修改成功，修改数据条数" + n);
                return n;
            } catch(Exception e)
            {
                Console.WriteLine(e.Message+"数据库musiclist表操作失败");
                return -1;
            }
        }
        
        /// <summary>
        /// 修改歌单名,返回数据库操作设计的数据条数，失败返回-1
        /// </summary>
        /// <param name="musicList"></param>
        /// <param name="ListName"></param>
        public int UpdateMusicList(MusicList musicList, string ListName)
        {
            try
            {
                musicList.ListName = ListName;
                string sql = "UPDATE musiclistinfo SET Listname = '"+ListName+ "' WHERE  user_id = '" + musicList.User_Id + "' and Listname = '" + musicList.ListName + "'";
                int n = ExcuteNonQuery(sql);
                Console.WriteLine(musicList.ListName + "重命名成功");
                return n;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + musicList.ListName + "重命名失败");
                return -1;
            }
        }

        /// <summary>
        /// 删除指定歌单
        /// </summary>
        /// <param name="musicList"></param>
        /// <returns></returns>
        public bool DeleteMusicList(MusicList musicList)
        {
            try
            {
                string sql = "DELETE FROM musiclistinfo WHERE Listname = '" + musicList.ListName + "'";
                int n = ExcuteNonQuery(sql);
                Console.WriteLine(musicList.ListName + "表删除成功，共删除" + n + "项数据");
                return true;
            }catch(Exception e)
            {
                Console.WriteLine(e.Message + "删除表失败");
                return false;
            }
        }

        /// <summary>
        /// 从数据库读取全部歌单索引
        /// </summary>
        /// <returns></returns>
        public MusicList[] GetMusicList(User user)
        {
            try
            {
                string sql = "SELECT Listname FROM musiclistinfo WHERE user_id = '" + user.User_Id + "'";
                MySqlDataReader rd = Read(sql);
                List<string> listNames = new List<string>();
                while (rd.Read())
                {
                    listNames.Add(rd["Listname"].ToString());                   
                }
                rd.Close();
                MusicList[] musicLists = new MusicList[listNames.Count];
                foreach (string s in listNames)
                {
                    MusicList musicList = new MusicList(s, user.User_Id);
                }
                Console.WriteLine("读取数据库成功");
                return musicLists;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "读取数据库失败");
                return null;
            }
        }

        /// <summary>
        /// 从数据库读取歌单内容
        /// </summary>
        /// <param name="musicList"></param>
        /// <returns></returns>
        public MusicList GetMusicList(MusicList musicList)
        {
            string sql = "SELECT Fileurl FROM musiclistinfo WHERE user_id = '" + musicList.User_Id + "' and Listname = '" + musicList.ListName + "'";
            MySqlDataReader rd = Read(sql);
            while (rd.Read())
            {
                Music music = new Music(rd["Fileurl"].ToString().Replace("/", "\\"));
                musicList.Add(music);
            }
            return musicList;
        }
        
        /// <summary>
        /// 从本地读取歌单
        /// </summary>
        /// <returns></returns>
        public MusicList GetLocalMusicList()
        {
            try
            {
                MusicList LocalMusicList = new MusicList("本地与下载");
                FileInfo myFile = new FileInfo("../local/local.txt");
                StreamReader sR = myFile.OpenText();
                string nextLine;
                while ((nextLine = sR.ReadLine()) != null)
                {
                    Music music = new Music().InitMusic(nextLine);
                    LocalMusicList.Add(music);
                }
                sR.Close();
                return LocalMusicList;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message+"读取本地与下载记录失败");
                return new MusicList();
            }
        }

        /// <summary>
        /// 更新本地歌单信息
        /// </summary>
        /// <param name="musicList"></param>
        public void UpdateLocalMusicList(MusicList musicList,Music music, ListOperation listOperation)
        {
            try
            {
                string filepath = "../local/local.txt";
                int index = 0;
                switch (listOperation)
                {
                    case ListOperation.ADD:
                        if ((index = ExistLocalMusic(music,filepath)) < 0)
                        {
                            StreamWriter sW = new StreamWriter(filepath, true, Encoding.GetEncoding("utf-8"));
                            sW.WriteLine(music.FileURL);
                            sW.Close();
                        }
                        break;
                    case ListOperation.DELETE:
                        if ((index = ExistLocalMusic(music,filepath)) >= 0)
                        {
                            List<string> lines = new List<string>(File.ReadAllLines(filepath));
                            lines.RemoveAt(index);
                            File.WriteAllLines(filepath, lines.ToArray());
                        }
                        break;
                    
                }
                Console.WriteLine("修改本地记录成功");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "修改本地与下载记录失败");
            }
        }

        /// <summary>
        /// 检索本地歌单，存在返回所在行数，不存在或者失败返回-1
        /// </summary>
        /// <param name="music"></param>
        /// <returns></returns>
        public int ExistLocalMusic(Music music, string filepath)
        {
            try
            {
                bool IsExist = false;
                FileInfo myFile = new FileInfo(filepath);
                StreamReader sR = myFile.OpenText();
                string nextLine;
                int i = 0;
                while ((nextLine = sR.ReadLine()) != null)
                {
                    if (nextLine.ToString() == music.FileURL.ToString())
                    {
                        IsExist = true;
                        break;
                    }
                    i++;
                }
                sR.Close();
                return IsExist ? i : -1;
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return -1;
            }
        }

        /// <summary>
        /// 从本地读取歌单
        /// </summary>
        /// <returns></returns>
        public MusicList GetPlayingMusicList()
        {
            try
            {
                MusicList PlayingMusicList = new MusicList("播放列表");
                FileInfo myFile = new FileInfo("../local/playing.txt");
                StreamReader sR = myFile.OpenText();
                string nextLine;
                while ((nextLine = sR.ReadLine()) != null)
                {
                    Music music = new Music().InitMusic(nextLine);
                    PlayingMusicList.Add(music);
                }
                sR.Close();
                return PlayingMusicList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "读取本地播放列表失败");
                return new MusicList();
            }
        }

        public void UpdatePlayingMusicList(MusicList musicList, Music music, ListOperation listOperation)
        {
            try
            {
                string filepath = "../local/playing.txt";
                int index = 0;
                switch (listOperation)
                {
                    case ListOperation.ADD:
                        if ((index = ExistLocalMusic(music, filepath)) < 0)
                        {
                            StreamWriter sW = new StreamWriter(filepath, true, Encoding.GetEncoding("utf-8"));
                            sW.WriteLine(music.FileURL);
                            sW.Close();
                            Console.WriteLine("txt添加");
                        }
                        break;
                    case ListOperation.DELETE:
                        if ((index = ExistLocalMusic(music, filepath)) >= 0)
                        {
                            List<string> lines = new List<string>(File.ReadAllLines(filepath));
                            lines.RemoveAt(index);
                            File.WriteAllLines(filepath, lines.ToArray());
                            Console.WriteLine("txt删除");
                        }
                        break;

                }
                Console.WriteLine("修改本地记录成功");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "修改本地与下载记录失败");
            }
        }

        #endregion

    }
}
