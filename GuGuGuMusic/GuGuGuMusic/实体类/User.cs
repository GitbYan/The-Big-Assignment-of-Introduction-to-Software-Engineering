using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuGuGuMusic
{
    public class User
    {
        public User()
        {

        }

        public User(string userid)
        {
            User_Id = userid;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string User_Id { get; set; } = "";

        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; } = "";

        private string _ImageURL = "";

        /// <summary>
        /// 用户头像图片地址
        /// </summary>
        public string ImageURL
        {
            get { return _ImageURL; }
            set
            {
                _ImageURL = value;
            }
        }



    }
}
