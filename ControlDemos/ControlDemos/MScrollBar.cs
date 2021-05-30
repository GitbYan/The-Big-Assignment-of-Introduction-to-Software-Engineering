using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlDemos
{
    [DefaultEvent("MValueChanged")]
    public class MScrollBar:Control
    {
        public MScrollBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            CreateControl();
        }

        #region 属性

        private Color _BarColor = Color.FromArgb(217, 217, 217);
        /// <summary>
        /// 背景条颜色
        /// </summary>
        [Category("DemoUI"), Description("背景条颜色")]
        public Color M_BarColor
        {
            get { return _BarColor; }
            set
            {
                _BarColor = value;
                Invalidate();
            }
        }

        private Color _SliderColor = Color.FromArgb(224, 224, 224);
        /// <summary>
        /// 滑块颜色
        /// </summary>
        [Category("DemoUI"), Description("滑块颜色")]
        public Color M_SliderColor
        {
            get { return _SliderColor; }
            set
            {
                _SliderColor = value;
                Invalidate();
            }
        }

        private Color _SliderHoverColor = Color.FromArgb(200, 200, 200);
        /// <summary>
        /// 滑块颜色
        /// </summary>
        [Category("DemoUI"), Description("鼠标停留时滑块颜色")]
        public Color M_SliderHoverColor
        {
            get
            {
                return _SliderHoverColor;
            }
            set
            {
                _SliderHoverColor = value;
                Invalidate();
            }
        }

        private Color _PresentColor = Color.FromArgb(224, 224, 224);//绿
        /// <summary>
        /// 滑块颜色
        /// </summary>
        [Category("DemoUI"), Description("鼠标停留时滑块颜色")]
        public Color M_PresentColor
        {
            get
            {
                return _PresentColor;
            }
            set
            {
                _PresentColor = value;
                Invalidate();
            }
        }


        private bool _IsRound = true;
        /// <summary>
        /// 是否圆角<para>默认：是</para>
        /// </summary>
        [Category("DemoUI"), Description("是否圆角")]
        public bool M_IsRound
        {
            get { return _IsRound; }
            set
            {
                _IsRound = value;
                Invalidate();
            }
        }

        private double _Maximum = 100;
        /// <summary>
        /// 滑块范围<para>范围：大于等于0</para>
        /// </summary>
        [Category("DemoUI"), Description("滑块范围")]
        public double M_Maximum
        {
            get { return _Maximum; }
            set
            {
                _Maximum = value;
                if (_Maximum <= 0) _Maximum = 0 + 1;
                Invalidate();
            }
        }

        private double _Value = 0;
        /// <summary>
        /// 滑块当前标识值
        /// </summary>
        [Category("DemoUI"), Description("滑块当前标识值")]
        public double M_Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                if (_Value < 0) _Value = 0;
                if (_Value > _Maximum) _Value = _Maximum;
                Invalidate();
                MValueChanged?.Invoke(this, new MEventArgs(_Value));
            }
        }

        private Orientation _Orientation = Orientation.Horizontal_LR;
        /// <summary>
        /// 滑块方向
        /// </summary>
        [Category("DemoUI"), Description("滑块方向")]
        public Orientation M_Orientation
        {
            get { return _Orientation; }
            set
            {
                Orientation old = _Orientation;
                _Orientation = value;
                if ((old == Orientation.Horizontal_LR || old == Orientation.Horizontal_RL) && (_Orientation == Orientation.Vertical_DU || _Orientation == Orientation.Vertical_UD))
                {
                    Size = new Size(Size.Height, Size.Width);
                }
                if ((_Orientation == Orientation.Horizontal_LR || _Orientation == Orientation.Horizontal_RL) && (old == Orientation.Vertical_DU || old == Orientation.Vertical_UD))
                {
                    Size = new Size(Size.Height, Size.Width);
                }
                Invalidate();
            }
        }

        private int _BarSize = 10;
        /// <summary>
        /// 滑条高度（水平）/宽度（垂直）
        /// </summary>
        [Category("DemoUI"), Description("滑条高度（水平）/宽度（垂直）")]
        public int M_BarSize
        {
            get { return _BarSize; }
            set
            {
                _BarSize = value;
                if (_BarSize < 1) _BarSize = 1;
                if (_Orientation == Orientation.Horizontal_LR || _Orientation == Orientation.Horizontal_RL)
                {
                    Size = new Size(Width, _BarSize);
                }
                else
                {
                    Size = new Size(_BarSize, Height);
                }
                MValueChanged?.Invoke(this, new MEventArgs(_Value));
            }
        }

        private int _SliderLength = 10;
        /// <summary>
        /// 滑条长度
        /// </summary>
        [Category("DemoUI"), Description("滑条长度")]
        public int M_SliderLength
        {
            get { return _SliderLength; }
            set
            {
                _SliderLength = value;
                Invalidate();
            }
        }
        #endregion

        #region 
        /// <summary>
        /// 委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void MValueChangedEventHandler(object sender, MEventArgs e);
        /// <summary>
        /// 值发生改变时引发的事件
        /// </summary>
        public event MValueChangedEventHandler MValueChanged;
        #endregion

        private MouseStatus mouseStatus = MouseStatus.Leave;
        private PointF mousePoint = Point.Empty;

        private void MPointToValue()
        {
            float fCapHalfWidth = 0;
            float fCapWidth = 0;
            if (_IsRound)
            {
                fCapWidth = _SliderLength;
                fCapHalfWidth = _SliderLength / 2.0f;
            }

            if (_Orientation == Orientation.Horizontal_LR || _Orientation == Orientation.Horizontal_RL)
            {
                float fRatio = Convert.ToSingle(mousePoint.X - fCapHalfWidth) / (Width - fCapWidth);
                _Value = Convert.ToInt32(fRatio * _Maximum);
            }
            else if (_Orientation == Orientation.Vertical_UD|| _Orientation == Orientation.Vertical_DU)
            {
                float fRatio = Convert.ToSingle(mousePoint.Y - fCapHalfWidth) / (Height - fCapWidth);
                _Value = Convert.ToInt32(fRatio * _Maximum);
            }
            if (_Value < 0) _Value = 0;
            if (_Value > _Maximum) _Value = _Maximum;
            MValueChanged?.Invoke(this, new MEventArgs(_Value));
        }

        private void MValueToPoint()
        {
            float fCapHalfWidth = 0;
            float fCapWidth = 0;
            if (_IsRound)
            {
                fCapWidth = _SliderLength;
                fCapHalfWidth = _SliderLength / 2.0f;
            }

            float fRatio = Convert.ToSingle((float)(_Value / _Maximum));
            if (_Orientation == Orientation.Horizontal_LR || _Orientation == Orientation.Horizontal_RL)
            {
                float fPointValue = fRatio * (Width - fCapWidth) + fCapHalfWidth;
                mousePoint = new PointF(fPointValue, _BarSize / 2.0f);
            }
            else if (_Orientation == Orientation.Vertical_UD || _Orientation == Orientation.Vertical_DU)
            {
                float fPointValue = fRatio * (Height - fCapWidth) + fCapHalfWidth;
                mousePoint = new PointF(_BarSize / 2.0f, fPointValue);
            }
        }

        #region 重写

        /// <summary>
        /// 设置控件的工作边界
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="specified"></param>
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            int MHeight = _BarSize;
            if (_Orientation == Orientation.Horizontal_LR || _Orientation == Orientation.Horizontal_RL)
            {
                base.SetBoundsCore(x, y, width, MHeight, specified);
            }
            else
            {
                base.SetBoundsCore(x, y, MHeight, height, specified);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            MValueToPoint();
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            Pen penBarFore;
            if (mouseStatus == MouseStatus.Enter || mouseStatus == MouseStatus.Down)
            {
                penBarFore = new Pen(_SliderHoverColor, _BarSize);
            }
            else
            {
                penBarFore = new Pen(_SliderColor, _BarSize);
            }
            Pen penBarBack = new Pen(_BarColor, _BarSize);

            float fCapHalfWidth = 0;
            float fCapWidth = 0;
            if (_IsRound)
            {
                fCapWidth = _BarSize;
                fCapHalfWidth = _BarSize / 2.0f;
                penBarBack.StartCap = LineCap.Round;
                penBarBack.EndCap = LineCap.Round;
                penBarFore.StartCap = LineCap.Round;
                penBarFore.EndCap = LineCap.Round;
            }

            float fPointValue = 0;
            if (_Orientation == Orientation.Horizontal_LR || _Orientation == Orientation.Horizontal_RL)
            {
                e.Graphics.DrawLine(penBarBack, fCapHalfWidth, Height / 2f, Width - fCapHalfWidth, Height / 2f);
                fPointValue = mousePoint.X;
                if (fPointValue - _SliderLength / 2 < 0) fPointValue = _SliderLength / 2;
                if (fPointValue + _SliderLength / 2 > Width) fPointValue = Width  - _SliderLength / 2;
            }
            else
            {
                e.Graphics.DrawLine(penBarBack, Width / 2f, fCapHalfWidth, Width / 2f, Height - fCapHalfWidth);
                fPointValue = mousePoint.Y;
                if (fPointValue - _SliderLength / 2 < 0) fPointValue = _SliderLength / 2;
                if (fPointValue + _SliderLength / 2 > Height) fPointValue = Height - _SliderLength / 2;
            }

            if (_Orientation == Orientation.Horizontal_LR|| _Orientation == Orientation.Horizontal_RL)
            {
                e.Graphics.DrawLine(penBarFore, fPointValue - _SliderLength / 2, Height / 2f, fPointValue + _SliderLength / 2, Height / 2f);
            }
            else if (_Orientation == Orientation.Vertical_UD|| _Orientation == Orientation.Vertical_DU)
            {
                e.Graphics.DrawLine(penBarFore, Width / 2f, fPointValue - _SliderLength / 2, Width / 2f, fPointValue + _SliderLength / 2);
            }

        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            mouseStatus = MouseStatus.Down;
            mousePoint = e.Location;
            MPointToValue();
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (mouseStatus == MouseStatus.Down)
            {
                mousePoint = e.Location;
                MPointToValue();
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            mouseStatus = MouseStatus.Up;
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

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            Invalidate();
        }
        #endregion
    }
}
