using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GuGuGuMusic
{
    public class MDB
    {
        public DataSet MusicInfoDataSet = new DataSet();

        [Obsolete]
        private static string _DBConnString = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"];
        /// <summary>
        /// 数据库 TODO
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

        public DataSet GetMusicInfo()
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
                ExcuteNonQuery(sql);
                Console.WriteLine("数据库添加成功");
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
                List<Music> musics = new List<Music>();
                while (rd.Read())
                {
                    Music music = new Music(rd["Name"].ToString(), rd["Singer"].ToString(), rd["Album"].ToString(), rd["Fileurl"].ToString().Replace("/", "\\"));
                    musics.Add(music);
                }
                Console.WriteLine("读取数据库成功");
                return musics;

            } catch(Exception e)
            {
                Console.WriteLine(e.Message + "读取数据库失败");
                return null;
            }
        }
    }
}
