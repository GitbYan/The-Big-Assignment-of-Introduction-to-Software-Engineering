using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuGuGuMusic
{
    public partial class PlayListForm : Form
    {
        public PlayListForm()
        {
            InitializeComponent();
        }

        private void Btn_Shut_Click(object sender, EventArgs e)
        {
            this.Width = 0;
            this.Hide();
            //this.WindowState = FormWindowState.Minimized;
        }

        public void MusicUpdate(MGroup mGroup)
        {
            Panel_MGroup.Controls.Clear();
            if (mGroup != null && mGroup.musics!=null)
            {
                int i = 0;
                foreach (Music music in mGroup.musics)
                {
                    Button Btn_Shut = new Button() { FlatStyle = FlatStyle.Flat, Size = new System.Drawing.Size(300, 64), TabStop = false, BackColor = Color.White };
                    Btn_Shut.Location = new Point(0, 64 * i);
                    Btn_Shut.Text = music.Name;
                    Btn_Shut.FlatAppearance.BorderSize = 0;
                    Panel_MGroup.Controls.Add(Btn_Shut);
                    Btn_Shut.Show();
                    i++;
                }
            }
            Invalidate();
        }
    }
}
