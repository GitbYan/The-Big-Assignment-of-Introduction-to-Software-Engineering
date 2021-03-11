using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDemos
{

    /// <summary>
    /// 滑块方向
    /// </summary>
    public enum Orientation
    {
        /// <summary>
        /// 水平方向（从左到右）
        /// </summary>
        Horizontal_LR,
        /// <summary>
        /// 水平方向（从右到左）
        /// </summary>
        Horizontal_RL,
        /// <summary>
        /// 水平方向（从上到下）
        /// </summary>
        Vertical_UD,
        /// <summary>
        /// 水平方向（从下到上）
        /// </summary>
        Vertical_DU
    }

    public enum MouseStatus
    {
        /// <summary>
        /// 鼠标进入
        /// </summary>
        Enter,
        /// <summary>
        /// 鼠标离开
        /// </summary>
        Leave,
        /// <summary>
        /// 鼠标按下
        /// </summary>
        Down,
        /// <summary>
        /// 鼠标按下释放
        /// </summary>
        Up
    }
}
