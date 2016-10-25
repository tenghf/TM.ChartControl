using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TM.Controls.ChartControl
{
    /// <summary>
    /// 坐标点
    /// </summary>
    public class TPoint
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string name;
        /// <summary>
        /// X坐标
        /// </summary>
        public float X;
        /// <summary>
        /// Y坐标
        /// </summary>
        public float Y;

        public TPoint()
        {

        }
        /// <summary>
        /// 坐标点的X、Y坐标赋值
        /// </summary>
        /// <param name="_X">X坐标</param>
        /// <param name="_Y">Y坐标</param>
        public TPoint(float _X,float _Y,string _name)
        {
            this.X = _X;
            this.Y = _Y;
            this.name = _name;
        }
    }
}
