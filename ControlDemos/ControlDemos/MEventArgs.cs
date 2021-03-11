using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDemos
{
    public class MEventArgs:EventArgs
    {
        /// <summary>
        /// 事件数据
        /// </summary>
        /// <param name="value">值</param>
        public MEventArgs(object value)
        {
            Value = value;
        }
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }
    }
}
