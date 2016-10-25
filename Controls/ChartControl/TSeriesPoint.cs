using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TM.Controls.ChartControl
{
    public class TSeriesPoint
    {
        /// <summary>
        /// 宿主
        /// </summary>
        public TSeriesPointCollection Owner { get; internal set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string ID;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 点的参数
        /// </summary>
        public string Argument;
        /// <summary>
        /// 点的值
        /// </summary>
        public float Value;
        /// <summary>
        /// 目标值
        /// </summary>
        public float TargetValue;
        /// <summary>
        /// 显示的颜色
        /// </summary>
        public Color Color;
        private TChartItem _item;
        /// <summary>
        /// 项目，报表中显示的项目
        /// </summary>
        public TChartItem Item
        {
            get { return _item; }
            set
            {
                _item = value;
                _item.Owner = this;
            }
        }
        private TChartItem _targetItem;
        /// <summary>
        /// 项目，报表中显示的目标项目
        /// </summary>
        public TChartItem TargetItem
        {
            get { return _targetItem; }
            set
            {
                _targetItem = value;
                _targetItem.Owner = this;
            }
        }
        private TTipRectangle _tipRectangle;
        /// <summary>
        /// 提示框
        /// </summary>
        public TTipRectangle TipRectangle
        {
            get { return _tipRectangle; }
            set
            {
                _tipRectangle = value;
                _tipRectangle.Owner = this;
            }
        }
        private TTipRectangle _targetTipRectangle;
        /// <summary>
        /// 目标提示提示框
        /// </summary>
        public TTipRectangle TargetTipRectangle
        {
            get { return _targetTipRectangle; }
            set
            {
                _targetTipRectangle = value;
                _targetTipRectangle.Owner = this;
            }
        }

        public TSeriesPoint()
        {

        }
        public TSeriesPoint(string _argument, float _value, float _targetValue =0F)
        {
            this.Argument = _argument;
            this.Value = _value;
            this.TargetValue = _targetValue;
        }

        public void SetColor(Color _color)
        {
            this.Color = _color;
        }
    }
}
