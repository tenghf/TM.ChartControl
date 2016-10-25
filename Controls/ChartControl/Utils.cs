using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TM.Controls.ChartControl
{
    public static class Utils
    {
        /// <summary>
        /// 画矩形
        /// </summary>
        /// <param name="graphics">绘图图面</param>
        /// <param name="pen">画笔</param>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public static void DrawRectangle(this Graphics graphics, Pen pen, TRectangle rectangle)
        {
            graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        /// <summary>
        /// 画矩形
        /// </summary>
        /// <param name="graphics">绘图图面</param>
        /// <param name="pen">画笔</param>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public static void FillRectangle(this Graphics graphics, Brush brush, TRectangle rectangle)
        {
            graphics.FillRectangle(brush, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }
    }
}
