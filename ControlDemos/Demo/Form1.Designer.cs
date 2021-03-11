namespace Demo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.mTrackBar1 = new ControlDemos.MTrackBar();
            this.mTrackBar2 = new ControlDemos.MTrackBar();
            this.mTrackBar3 = new ControlDemos.MTrackBar();
            this.mTrackBar4 = new ControlDemos.MTrackBar();
            this.mTrackBar5 = new ControlDemos.MTrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(21, 265);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 3;
            this.trackBar1.Value = 5;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // mTrackBar1
            // 
            this.mTrackBar1.Location = new System.Drawing.Point(288, 237);
            this.mTrackBar1.M_BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.mTrackBar1.M_BarSize = 2;
            this.mTrackBar1.M_CircleRadius = 6;
            this.mTrackBar1.M_IsRound = true;
            this.mTrackBar1.M_Maximum = 100;
            this.mTrackBar1.M_Minimum = 0;
            this.mTrackBar1.M_Orientation = ControlDemos.Orientation.Horizontal_LR;
            this.mTrackBar1.M_SliderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.mTrackBar1.M_Value = 0;
            this.mTrackBar1.Name = "mTrackBar1";
            this.mTrackBar1.Size = new System.Drawing.Size(75, 13);
            this.mTrackBar1.TabIndex = 6;
            this.mTrackBar1.Text = "mTrackBar1";
            // 
            // mTrackBar2
            // 
            this.mTrackBar2.Location = new System.Drawing.Point(278, 114);
            this.mTrackBar2.M_BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.mTrackBar2.M_BarSize = 2;
            this.mTrackBar2.M_CircleRadius = 8;
            this.mTrackBar2.M_IsRound = true;
            this.mTrackBar2.M_Maximum = 100;
            this.mTrackBar2.M_Minimum = 0;
            this.mTrackBar2.M_Orientation = ControlDemos.Orientation.Horizontal_LR;
            this.mTrackBar2.M_SliderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.mTrackBar2.M_Value = 0;
            this.mTrackBar2.Name = "mTrackBar2";
            this.mTrackBar2.Size = new System.Drawing.Size(500, 17);
            this.mTrackBar2.TabIndex = 5;
            this.mTrackBar2.Text = "mTrackBar2";
            // 
            // mTrackBar3
            // 
            this.mTrackBar3.Location = new System.Drawing.Point(441, 237);
            this.mTrackBar3.M_BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.mTrackBar3.M_BarSize = 2;
            this.mTrackBar3.M_CircleRadius = 8;
            this.mTrackBar3.M_IsRound = true;
            this.mTrackBar3.M_Maximum = 100;
            this.mTrackBar3.M_Minimum = 0;
            this.mTrackBar3.M_Orientation = ControlDemos.Orientation.Horizontal_RL;
            this.mTrackBar3.M_SliderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.mTrackBar3.M_Value = 0;
            this.mTrackBar3.Name = "mTrackBar3";
            this.mTrackBar3.Size = new System.Drawing.Size(75, 17);
            this.mTrackBar3.TabIndex = 7;
            this.mTrackBar3.Text = "mTrackBar3";
            // 
            // mTrackBar4
            // 
            this.mTrackBar4.Location = new System.Drawing.Point(326, 340);
            this.mTrackBar4.M_BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.mTrackBar4.M_BarSize = 2;
            this.mTrackBar4.M_CircleRadius = 8;
            this.mTrackBar4.M_IsRound = true;
            this.mTrackBar4.M_Maximum = 100;
            this.mTrackBar4.M_Minimum = 0;
            this.mTrackBar4.M_Orientation = ControlDemos.Orientation.Vertical_UD;
            this.mTrackBar4.M_SliderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.mTrackBar4.M_Value = 0;
            this.mTrackBar4.Name = "mTrackBar4";
            this.mTrackBar4.Size = new System.Drawing.Size(17, 75);
            this.mTrackBar4.TabIndex = 8;
            this.mTrackBar4.Text = "mTrackBar4";
            // 
            // mTrackBar5
            // 
            this.mTrackBar5.Location = new System.Drawing.Point(450, 340);
            this.mTrackBar5.M_BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.mTrackBar5.M_BarSize = 2;
            this.mTrackBar5.M_CircleRadius = 8;
            this.mTrackBar5.M_IsRound = true;
            this.mTrackBar5.M_Maximum = 100;
            this.mTrackBar5.M_Minimum = 0;
            this.mTrackBar5.M_Orientation = ControlDemos.Orientation.Vertical_DU;
            this.mTrackBar5.M_SliderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.mTrackBar5.M_Value = 0;
            this.mTrackBar5.Name = "mTrackBar5";
            this.mTrackBar5.Size = new System.Drawing.Size(17, 75);
            this.mTrackBar5.TabIndex = 9;
            this.mTrackBar5.Text = "mTrackBar5";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 689);
            this.Controls.Add(this.mTrackBar5);
            this.Controls.Add(this.mTrackBar4);
            this.Controls.Add(this.mTrackBar3);
            this.Controls.Add(this.mTrackBar1);
            this.Controls.Add(this.mTrackBar2);
            this.Controls.Add(this.trackBar1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TrackBar trackBar1;
        private ControlDemos.MTrackBar mTrackBar2;
        private ControlDemos.MTrackBar mTrackBar1;
        private ControlDemos.MTrackBar mTrackBar3;
        private ControlDemos.MTrackBar mTrackBar4;
        private ControlDemos.MTrackBar mTrackBar5;
    }
}

