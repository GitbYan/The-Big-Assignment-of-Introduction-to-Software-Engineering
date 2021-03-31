using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Drawing.Drawing2D;

namespace ControlDemos
{
    public class MSerachBox : TextBox
    {
        public MSerachBox()
        {
            Controls.Add(HintLabel);
            HintLabel.Text = HintText;
            CreateControl();
        }
        private readonly Label HintLabel = new Label() { Location = new Point(1, 1) };

        #region 属性
        /// <summary>
        /// 文本框提示
        /// </summary>
        [Category("DemoUI"),Description("文本框提示")]
        public string HintText { get; set; } = "文本框提示";

        /// <summary>
        /// 提示字体颜色
        /// </summary>
        [Category("DemoUI"),Description("提示字体颜色")]
        public Color M_HintColor { get; set; } = Color.Gray;

        /// <summary>
        /// 判断是否有输入
        /// </summary>
        private bool HasInput { get; set; } = false;

        /// <summary>
        /// 输入字体颜色
        /// </summary>
        [Category("DemoUI"),Description("输入字体颜色")]
        public Color M_ForeColor { get; set; } = Color.Black;

        /// <summary>
        /// 是否圆角<para>默认：是</para>
        /// </summary>
        [Category("DemoUI"), Description("是否圆角")]
        public bool M_IsRound { get; set; } = true;
        
        /// <summary>
        /// 背景颜色
        /// </summary>
        [Category("DemoUI"),Description("背景颜色")]
        public Color M_BackColor { get; set; } = Color.White;
        #endregion

        #region 重写
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (Text == HintText)
            {
                Select(0, 0);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (Text == HintText)
            {
                Select(0, 0);
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            if (Text == HintText)
            {
                Select(0, 0);
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (HasInput)
            {
                if(Text == "")
                {
                    Text = HintText;
                    HasInput = false;
                }
            }
            else if (Text == HintText)
            {
                Text = "";
                HasInput = true;
            }
        }
        #endregion

    }
}
