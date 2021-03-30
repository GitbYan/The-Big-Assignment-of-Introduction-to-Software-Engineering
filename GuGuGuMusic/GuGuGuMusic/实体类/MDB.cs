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

        /// <summary>
        /// 执行sql语句返回所有user信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetUserInfo()
        {
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter("select * from userinfo", connection());
            DataSet userInfoDataSet = new DataSet();
            mySqlDataAdapter.Fill(userInfoDataSet, "userinfo");
            return userInfoDataSet;
        }

        /// <summary>
        /// 执行sql语句返回所有music信息
        /// </summary>
        /// <returns></returns>
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
        /// 修改数据库中对应的音乐表，返回操作涉及的项数，失败返回-1
        /// </summary>
        /// <param name="musicList">列表对象</param>
        /// <param name="music">音乐对象</param>
        /// <returns></returns>
        public int UpdateMusicList(MusicList musicList)
        {
            try
            {
                string filepath = "";
                List<string> lines = new List<string>();
                if (musicList.ListName.ToString() == "本地与下载")
                {
                    filepath = "../local/local.txt";
                    foreach(Music music in musicList.Musics)
                    {
                        lines.Add(music.FileURL);
                    }
                    File.WriteAllLines(filepath, lines.ToArray());
                    return 0;
                    
                }
                else if (musicList.ListName.ToString() == "播放列表")
                {
                    filepath = "../local/playing.txt";
                    foreach (Music music in musicList.Musics)
                    {
                        lines.Add(music.FileURL);
                    }
                    File.WriteAllLines(filepath, lines.ToArray());
                    return 0;
                }
                else
                {
                    int n = 0;
                    string sql1 = "DELETE FROM musiclistinfo WHERE user_id = '" + musicList.User_Id + "' and Listname = '" + musicList.ListName + "'";
                    n += ExcuteNonQuery(sql1);
                    foreach (Music music in musicList.Musics)
                    {
                        string sql2 = "INSERT INTO musiclistinfo VALUES('" + musicList.ListName + "','" + musicList.User_Id + "','" + music.FileURL.Replace("\\", "/") + "') ON DUPLICATE KEY UPDATE Fileurl = '" + music.FileURL.Replace("\\", "/") + "'";
                        n += ExcuteNonQuery(sql2);
                    }  
                    Console.WriteLine("数据库修改成功，修改数据条数" + n);
                    return n;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message+"数据库musiclist表操作失败");
                return -1;
            }
        }
        
        /// <summary>
        /// 修改数据库中歌单名,返回数据库操作所涉及的数据条数，失败返回-1
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
        /// 删除数据库中指定歌单
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
        /// 从数据库读取用户歌单信息
        /// </summary>
        /// <returns></returns>
        public MusicList[] GetMusicList(User user)
        {
            try
            {
                string sql = "SELECT Listname FROM user_musiclist_info WHERE user_id = '" + user.User_Id + "'";
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
        /// 从数据库读取任一歌单内容，返回歌单内所有音乐信息
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
        public MusicList GetLocalMusicList(MusicList musicList)
        {
            try
            {
                FileInfo myFile = new FileInfo("../local/local.txt");
                if(musicList.ListName.ToString()== "本地与下载")
                {
                    myFile = new FileInfo("../local/local.txt");
                }
                else
                {
                    myFile = new FileInfo("../local/playing.txt");
                }
                StreamReader sR = myFile.OpenText();
                string nextLine;
                while ((nextLine = sR.ReadLine()) != null)
                {
                    Music music = new Music().InitMusic(nextLine);
                    musicList.Add(music);
                }
                sR.Close();
                return musicList;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message+"读取本地歌单失败");
                return new MusicList();
            }
        }

        #endregion

    }
}
