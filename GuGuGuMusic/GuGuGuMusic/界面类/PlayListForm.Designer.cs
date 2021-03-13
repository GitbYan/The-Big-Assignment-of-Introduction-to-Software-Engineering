using System.Windows.Forms;

namespace GuGuGuMusic
{
    partial class PlayListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Lbl_Number = new System.Windows.Forms.Label();
            this.Panel_MGroup = new System.Windows.Forms.Panel();
            this.Btn_Shut = new System.Windows.Forms.Button();
            this.Lbl_PlayList = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Lbl_Number
            // 
            this.Lbl_Number.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lbl_Number.ForeColor = System.Drawing.Color.DarkGray;
            this.Lbl_Number.Location = new System.Drawing.Point(0, 45);
            this.Lbl_Number.Name = "Lbl_Number";
            this.Lbl_Number.Size = new System.Drawing.Size(300, 23);
            this.Lbl_Number.TabIndex = 1;
            this.Lbl_Number.Text = " 0首歌曲";
            // 
            // Panel_MGroup
            // 
            this.Panel_MGroup.AutoScroll = true;
            this.Panel_MGroup.Location = new System.Drawing.Point(0, 68);
            this.Panel_MGroup.MinimumSize = new System.Drawing.Size(300, 512);
            this.Panel_MGroup.Name = "Panel_MGroup";
            this.Panel_MGroup.Size = new System.Drawing.Size(300, 512);
            this.Panel_MGroup.TabIndex = 3;
            // 
            // Btn_Shut
            // 
            this.Btn_Shut.FlatAppearance.BorderSize = 0;
            this.Btn_Shut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Shut.Font = new System.Drawing.Font("宋体", 20F);
            this.Btn_Shut.Location = new System.Drawing.Point(225, 580);
            this.Btn_Shut.Name = "Btn_Shut";
            this.Btn_Shut.Size = new System.Drawing.Size(75, 64);
            this.Btn_Shut.TabIndex = 4;
            this.Btn_Shut.Text = "◲";
            this.Btn_Shut.UseVisualStyleBackColor = true;
            this.Btn_Shut.Click += new System.EventHandler(this.Btn_Shut_Click);
            // 
            // Lbl_PlayList
            // 
            this.Lbl_PlayList.Enabled = false;
            this.Lbl_PlayList.FlatAppearance.BorderSize = 0;
            this.Lbl_PlayList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Lbl_PlayList.Font = new System.Drawing.Font("微軟正黑體 Light", 20F);
            this.Lbl_PlayList.Location = new System.Drawing.Point(0, 0);
            this.Lbl_PlayList.Name = "Lbl_PlayList";
            this.Lbl_PlayList.Size = new System.Drawing.Size(300, 45);
            this.Lbl_PlayList.TabIndex = 5;
            this.Lbl_PlayList.Text = "播放列表";
            this.Lbl_PlayList.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.Lbl_PlayList.UseVisualStyleBackColor = true;
            // 
            // PlayListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(300, 648);
            this.Controls.Add(this.Lbl_PlayList);
            this.Controls.Add(this.Btn_Shut);
            this.Controls.Add(this.Panel_MGroup);
            this.Controls.Add(this.Lbl_Number);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PlayListForm";
            this.Text = "PlayListForm";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label Lbl_Number;
        private System.Windows.Forms.Panel Panel_MGroup;
        private System.Windows.Forms.Button Btn_Shut;
        private Button Lbl_PlayList;
    }
}