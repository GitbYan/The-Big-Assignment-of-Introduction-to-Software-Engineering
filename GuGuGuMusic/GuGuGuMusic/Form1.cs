using AxWMPLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace GuGuGuMusic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer)).BeginInit();
            this.Controls.Add(axWindowsMediaPlayer);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer)).EndInit();

            string URL1 = "http://isure.stream.qqmusic.qq.com/C4000042Cw752gLfrn.m4a?guid=8706049567&vkey=680B733D1C9EA923DD74D93B37B013D5227132582C97546B02693688473AA9C57EF003C6636A761A8BBB58FAA68DA9DCB08ACAC14EF975B1&uin=6129&fromtag=66";
            string URL2 = "http://ws.stream.qqmusic.qq.com/C400004dQefs1nDE3n.m4a?guid=8706049567&vkey=CFA42BF36294A6FB15D94B929312841D0E252253844DD444B02C23E9971EAB1B6BB7674EA3B2144A40E9E6D155E37139D74A7C00A4529FFD&uin=6129&fromtag=66";
            axWindowsMediaPlayer.mediaCollection.add(URL1); 
            axWindowsMediaPlayer.mediaCollection.add(URL2);

            IWMPPlaylist playList = axWindowsMediaPlayer.playlistCollection.newPlaylist("MyPlayList");
            IWMPMedia media;
            media = axWindowsMediaPlayer.newMedia(URL1);
            playList.appendItem(media);
            media = axWindowsMediaPlayer.newMedia(URL2);
            playList.appendItem(media);

            axWindowsMediaPlayer.currentPlaylist = playList;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
