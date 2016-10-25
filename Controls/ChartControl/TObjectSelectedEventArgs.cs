using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TM.Controls.ChartControl
{
    /// <summary>
    /// 选中对象参数
    /// </summary>
    public class TObjectSelectedEventArgs : EventArgs

    {
        /// <summary>
        /// 选中序列点
        /// </summary>
        public TSeriesPoint SeriesPoint;
        /// <summary>
        /// 选中对象参数构建
        /// </summary>
        public TObjectSelectedEventArgs()
        {

        }
        /// <summary>
        /// 选中对象参数构建
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="name">名称</param>
        public TObjectSelectedEventArgs(TSeriesPoint seriesPoint)
        {
            this.SeriesPoint = seriesPoint;
        }
    }
}
