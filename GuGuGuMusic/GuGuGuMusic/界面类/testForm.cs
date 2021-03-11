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
    public partial class testForm : Form
    {
        public testForm()
        {
            InitializeComponent();
            timer1.Enabled = false;
            axWindowsMediaPlayer1.URL = "https://y.qq.com/n/yqq/song/0008Jzxl1kRtev.html";
        }



        double max, min, bal;  //为播放进度条提前声明变量

        private void mTrackBar1_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Enabled = false;     //停止检测播放进度
            if (axWindowsMediaPlayer1.URL != null)
            {
                axWindowsMediaPlayer1.Ctlcontrols.pause();  //暂停当前播放文件

                max = axWindowsMediaPlayer1.currentMedia.duration;      //获取文件的时间长度
                min = mTrackBar1.M_Value;//获取文件的当前播放位置
                bal = min / max;                                //计算百分比
                mTrackBar1.M_Value = (int)(bal * 100);     //设置滑块位置
                //将时长转化为XX：XX并显示在相应label上
                int intmax = (int)max;
                int intmin = (int)min;
                int intminM = intmin / 60;
                int intminS = intmin % 60;
                int intmaxM = intmax / 60;
                int intmaxS = intmax % 60;
                label2.Text = intminM.ToString() + ":" + intminS.ToString() + "/" + intmaxM.ToString() + ":" + intmaxS.ToString();
                //截取路径的文件名部分显示在“当前歌曲”的label上
                string nowsong = listBox1.SelectedItem.ToString();
                label1.Text = nowsong.Substring(nowsong.LastIndexOf("\\") + 1);
            }
        }

        private void mTrackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            double newValue = mTrackBar1.M_Value * 0.1 * 0.1 * max;
            if (axWindowsMediaPlayer1.URL!= null)
            {
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = newValue;          //为播放控件赋予新进度
                axWindowsMediaPlayer1.Ctlcontrols.play();                              //从当前进度开始播放
                timer1.Enabled = true;                                 //开始检测播放进度
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();         //开始播放
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            //若点击的歌曲列表有效，为播放控件赋予新路径
            if (listBox1.SelectedIndex != -1)
            {
                axWindowsMediaPlayer1.URL = listBox1.SelectedItem.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void mTrackBar1_MValueChanged(object sender, ControlDemos.MEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                max = axWindowsMediaPlayer1.currentMedia.duration;      //获取文件的时间长度
                min = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;//获取文件的当前播放位置
                bal = min / max;                                //计算百分比
                mTrackBar1.M_Value = (int)(bal * 100);     //设置滑块位置
                //将时长转化为XX：XX并显示在相应label上
                int intmax = (int)max;
                int intmin = (int)min;
                int intminM = intmin / 60;
                int intminS = intmin % 60;
                int intmaxM = intmax / 60;
                int intmaxS = intmax % 60;
                label2.Text = intminM.ToString() + ":" + intminS.ToString() + "/" + intmaxM.ToString() + ":" + intmaxS.ToString();
                //截取路径的文件名部分显示在“当前歌曲”的label上
                string nowsong = listBox1.SelectedItem.ToString();
                label1.Text = nowsong.Substring(nowsong.LastIndexOf("\\") + 1);
            }
            catch (Exception)
            {
                //切换歌曲瞬间可能导致max,min无值，添加try跳转报错但并不对报错进行处理
                Console.WriteLine("111");
            }
        }

        private void mButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();       //实例化一个通用对话框
            open.Filter = "音频文件(*.mp3)|*.mp3";            //选择文件格式
            if (open.ShowDialog() == DialogResult.OK)
            {
                //还原最大值最小值及进度条位置
                max = 0.0;
                min = 0.0;
                bal = 0.0;
                mTrackBar1.M_Value = 0;
                axWindowsMediaPlayer1.URL = open.FileName;            //添加到播放器组件
                listBox1.Items.Add(open.FileName);       //将音频文件路径添加到列表框内
                listBox1.SelectedIndex = listBox1.Items.Count - 1;    //列表框选中项为添加的歌曲路径
                timer1.Enabled = true;
            }
        }
     
    }
}
