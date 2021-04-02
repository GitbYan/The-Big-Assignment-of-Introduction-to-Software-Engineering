using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

/*
 * 4
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
                return conn;
            }
            catch(Exception e)
            {
                Console.WriteLine("4000:"+e.Message);
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
            try
            {
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter("select * from userinfo", connection());
                DataSet userInfoDataSet = new DataSet();
                mySqlDataAdapter.Fill(userInfoDataSet, "userinfo");
                return userInfoDataSet;      
            }catch(Exception e)
            {
                Console.WriteLine("4001" + e.Message);
                return null;
            }
        }

        /// <summary>
        /// 获取用户密码(加密字符串)
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetUserPassword(string userid)
        {
            try
            {
                string sql = "select * from userinfo where userid = '" + userid + "'";
                string password = "";
                MySqlDataReader rd = Read(sql);
                rd.Read();
                password = rd["password"].ToString();
                rd.Close();
                return password;
            }catch(Exception e)
            {
                Console.WriteLine("4002:"+e.Message);
                return "";
            }

        }

        /// <summary>
        /// 查询用户是否存在
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool ExistUser(string userid)
        {
            try
            {
                bool exist = false;
                string sql = "select * from userinfo where userid = '" + userid + "'";
                MySqlDataReader rd = Read(sql);
                while (rd.Read())
                {
                    exist = true;
                }
                rd.Close();
                return exist;
            }
            catch (Exception e)
            {
                Console.WriteLine("4003:"+e.Message);
                return true;
            }

        }

        /// <summary>
        /// 在数据库中添加用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int CreateUser(string userid,string password)
        {
            try
            {
                int n = 0;
                //userinfo表
                string sql1 = "INSERT INTO userinfo VALUES('" + userid + "','" + password+ "','none')";
                n += ExcuteNonQuery(sql1);
                //user_musiclist_info表
                string sql2 = "INSERT INTO user_musiclist_info VALUES('" + userid + "','我喜欢')";
                n += ExcuteNonQuery(sql2);
                string sql3 = "INSERT INTO user_musiclist_info VALUES('" + userid + "','播放历史')";
                n += ExcuteNonQuery(sql3);
                return n;
            }
            catch (Exception e)
            {
                Console.WriteLine("4004:"+e.Message);
                return -1;
            }
        }

        #region 歌曲部分
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
                rd.Close();
                return Musics;

            }
            catch (Exception e)
            {
                Console.WriteLine("4005:"+e.Message);
                return null;
            }
        }

        public List<Music> GetMusics(MusicList musicList)
        {
            try
            {
                string sql = "SELECT * FROM musiclistinfo where ListName='"+musicList.ListName+"' and user_id ='"+musicList.User_Id+"'";
                MySqlDataReader rd = Read(sql);
                List<Music> Musics = new List<Music>();
                while (rd.Read())
                {
                    string filename = rd["Fileurl"].ToString().Replace("/", "\\");
                    Music music = new Music(filename);
                    Musics.Add(music);
                }
                rd.Close();
                return Musics;

            }
            catch (Exception e)
            {
                Console.WriteLine("4005:" + e.Message);
                return null;
            }
        }

        /// <summary>
        /// 在数据库中添加新建歌单
        /// </summary>
        /// <param name="musicList"></param>
        /// <returns></returns>
        public int CreateMusicList(MusicList musicList)
        {
            try
            {
                int n = 0;
                string sql1 = "INSERT INTO user_musiclist_info VALUES('" + musicList.User_Id + "','" + musicList.ListName + "')";
                n += ExcuteNonQuery(sql1);
                foreach (Music music in musicList.Musics)
                {
                    if (music.FileURL != "")
                    {
                        string sql2 = "INSERT INTO musiclistinfo VALUES('" + musicList.ListName + "','" + musicList.User_Id + "','" + music.FileURL.Replace("\\", "/") + "') ON DUPLICATE KEY UPDATE Fileurl = '" + music.FileURL.Replace("\\", "/") + "'";
                        n += ExcuteNonQuery(sql2);
                    }
                }
                return n;
            }
            catch(Exception e)
            {
                Console.WriteLine("4006:"+e.Message);
                return -1;
            }
        }
        
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
                    return n;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("4007:"+e.Message);
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
                string sql1 = "UPDATE musiclistinfo SET Listname = '"+ListName+ "' WHERE  user_id = '" + musicList.User_Id + "' and Listname = '" + musicList.ListName + "'";
                int n = ExcuteNonQuery(sql1);
                string sql2 = "UPDATE user_musiclist_info SET Listname = '" + ListName + "' WHERE  user_id = '" + musicList.User_Id + "' and Listname = '" + musicList.ListName + "'";
                n += ExcuteNonQuery(sql2);
                return n;
            }
            catch (Exception e)
            {
                Console.WriteLine("4008:"+e.Message);
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
                string sql1 = "DELETE FROM musiclistinfo WHERE Listname = '" + musicList.ListName + "' and user_id = '" + musicList.User_Id + "'";
                int n = ExcuteNonQuery(sql1);
                string sql2 = "DELETE FROM user_musiclist_info WHERE Listname = '" + musicList.ListName + "' and user_id = '"+musicList.User_Id+"'";
                n += ExcuteNonQuery(sql2);
                return true;
            }catch(Exception e)
            {
                Console.WriteLine("4009:"+e.Message);
                return false;
            }
        }

        /// <summary>
        /// 从数据库读取用户歌单信息
        /// </summary>
        /// <returns></returns>
        public List<MusicList> GetAllMusicList(User user)
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
                List<MusicList> musicLists = new List<MusicList>();
                for(int i = 0;i< listNames.Count; i++) 
                {
                    MusicList musicList = new MusicList(listNames[i], user.User_Id);
                    musicList.Musics = GetMusics(musicList);
                    musicLists.Add(musicList);
                }
                return musicLists;
            }
            catch (Exception e)
            {
                Console.WriteLine("4010:"+e.Message);
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
            try
            {
                string sql = "SELECT * FROM musiclistinfo WHERE user_id = '" + musicList.User_Id + "' and Listname = '" + musicList.ListName + "'";
                MySqlDataReader rd = Read(sql);
                while (rd.Read())
                {
                    Music music = new Music(rd["Fileurl"].ToString().Replace("/", "\\"));
                    musicList.Add(music);
                }
                rd.Close();
                return musicList;
            }
            catch (Exception e)
            {
                Console.WriteLine("4011:"+e.Message);
                return null;
            }
        }
        
        /// <summary>
        /// 从本地读取歌单
        /// </summary>
        /// <returns></returns>
        public MusicList GetLocalMusicList(MusicList musicList)
        {
            try
            {
                FileInfo myFile;
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
                    Music music = new Music().InitMusic(nextLine.ToString());
                    musicList.Add(music);
                }
                sR.Close();
                return musicList;
            }
            catch(Exception e)
            {
                Console.WriteLine("4012:"+e.Message);
                return new MusicList();
            }
        }

        #endregion

    }
}
