using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TM.Controls.ChartControl
{
    /// <summary>
    /// 提示框
    /// </summary>
    public class TTipRectangle
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
        /// 颜色
        /// </summary>
        public Color Color;
        /// <summary>
        /// 提示框中的值
        /// </summary>
        public string Value;
        /// <summary>
        /// 宽度
        /// </summary>
        public float Width;
        /// <summary>
        /// 宽度
        /// </summary>
        public float Height=15;
        /// <summary>
        /// 宿主
        /// </summary>
        public TSeriesPoint Owner { get; internal set; }

        /// <summary>
        /// 矩形
        /// </summary>
        public TRectangle Rectangle;

        /// <summary>
        /// 提示框初始化
        /// </summary>
        /// <param name="_x">X坐标</param>
        /// <param name="_y">Y坐标</param>
        /// <param name="_color">颜色</param>
        /// <param name="_value">提示框中的值</param>
        public TTipRectangle(float _X, float _Y, Color _COLOR, string _VALUE)
        {
            this.X = _X;
            this.Y = _Y;
            this.Color = _COLOR;
            this.Value = _VALUE;
        }

        /// <summary>
        /// 画提示框
        /// </summary>
        public void DrawTipRectangle(Graphics graphics)
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            Pen tipPen = new Pen(this.Color, 1);
            graphics.DrawLine(tipPen, X, Y, X, Y - 6);
            this.Width = Value.Length * 8;
            this.Rectangle = new TRectangle(X - this.Width / 2, Y - 6 - this.Height, this.Width, this.Height);
            graphics.DrawRectangle(tipPen, this.Rectangle.X, this.Rectangle.Y, this.Rectangle.Width, this.Rectangle.Height);
            //画白色背景
            graphics.FillRectangle(new SolidBrush(Color.White), this.Rectangle.X+1, this.Rectangle.Y+1, this.Rectangle.Width-2, this.Rectangle.Height-2);
            graphics.DrawString(this.Value, new Font("宋体", 9, FontStyle.Regular), new SolidBrush(this.Color), new RectangleF(this.Rectangle.X, this.Rectangle.Y, this.Rectangle.Width, this.Rectangle.Height), stringFormat);
        }
    }
}
