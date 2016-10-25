using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TM.Controls.ChartControl
{
    /// <summary>
    /// Chart项
    /// </summary>
    public class TChartItem
    {
        /// <summary>
        /// X坐标
        /// </summary>
        public float X;
        /// <summary>
        /// Y坐标
        /// </summary>
        public float Y;
        /// <summary>
        /// 宽度
        /// </summary>
        public float Width;
        /// <summary>
        /// 高度
        /// </summary>
        public float Height;
        /// <summary>
        /// 提示信息
        /// </summary>
        public string ToolTipString;

        /// <summary>
        /// 宿主
        /// </summary>
        public TSeriesPoint Owner;

        public TChartItem(float _x, float _y, float _width, float _height)
        {
            this.X = _x;
            this.Y=_y;
            this.Width = _width;
            this.Height = _height;
        }
        /// <summary>
        /// 设置提示信息
        /// </summary>
        /// <param name="str"></param>
        public void SettoolTipString(string str)
        {
            this.ToolTipString = str;
        }

    }
}
