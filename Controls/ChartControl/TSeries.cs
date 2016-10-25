using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TM.Controls.ChartControl
{
    public class TSeries
    {
        /// <summary>
        /// 系列点的集合
        /// </summary>
        public TSeriesPointCollection Points { set; get; }
        /// <summary>
        /// 系列名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 系列颜色
        /// </summary>
        public Color SeriesColor;
        /// <summary>
        /// 图例中的矩形
        /// </summary>
        public TRectangle LegendRectangle;
        public TSeries(string name)
        {
            this.Name = name;
            Points = new TSeriesPointCollection();
            Points.Owner = this;
            SeriesColor = Color.Empty;
        }

    }
}
