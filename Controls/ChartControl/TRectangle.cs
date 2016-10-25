using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TM.Controls.ChartControl
{
    /// <summary>
    /// 矩形
    /// </summary>
    public class TRectangle
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
        public TRectangle()
        {

        }

        public TRectangle(float _X,float _Y,float _Width,float _Height)
        {
            this.X = _X;
            this.Y = _Y;
            this.Width = _Width;
            this.Height = _Height;
        }
    }
}
