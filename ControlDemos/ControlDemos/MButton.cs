using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlDemos
{
    public class MButton: Button
    {
        public MButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            CreateControl();
        }

        private MouseStatus mouseStatus = MouseStatus.Leave;

        #region 属性
        private Color _Color = Color.FromArgb(128, 255, 128);
        /// <summary>
        /// 图画颜色
        /// </summary>
        [Category("DemoUI"), Description("图画颜色")]
        public Color M_Color
        {
            get { return _Color; }
            set
            {
                _Color = value;
                Invalidate();
            }           
        }

        private Color _HoverColor = Color.FromArgb(128, 255, 128);
        /// <summary>
        /// 鼠标停靠时图画颜色
        /// </summary>
        [Category("DemoUI"), Description("鼠标停靠时图画颜色")]
        public Color M_HoverColor
        {
            get { return _HoverColor; }
            set
            {
                _HoverColor = value;
                Invalidate();
            }
        }
        
        #endregion

        #region 重写
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            DrawShape(M_Color, new Point(Width, Height), pevent);
            if (mouseStatus == MouseStatus.Up)
            {
                DrawShape(M_Color, new Point(Width, Height), pevent);
            }
            else if (mouseStatus == MouseStatus.Down)
            {
                DrawShape(M_HoverColor, new Point(Width + 2, Height + 2), pevent);
            }
            else if (mouseStatus == MouseStatus.Enter)
            {
                DrawShape(M_HoverColor, new Point(Width, Height), pevent);
            }
            else
            {
                DrawShape(M_Color, new Point(Width, Height), pevent);
            }

        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            mouseStatus = MouseStatus.Enter;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            mouseStatus = MouseStatus.Leave;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            mouseStatus = MouseStatus.Down;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            mouseStatus = MouseStatus.Up;
            Invalidate();
        }

        protected override bool ShowFocusCues
        {
            get
            {
                return false;
            }
        }
        #endregion

        public virtual void DrawShape(Color color,Point p, PaintEventArgs pevent)
        {

        }
    }

}
