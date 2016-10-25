using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace TM.Controls.ChartControl
{
    public partial class ChartControl : UserControl
    {
        #region 属性
        /// <summary>
        /// 私有系列集合
        /// </summary>
        private TSeriesCollection series;
        [Description("序列集合"),Category("报表属性")]
        public TSeriesCollection Series { get { return series; } }
        /// <summary>
        /// Y轴上的刻度数
        /// </summary>
        private int yPoints;
        [Description("Y轴上的刻度个数"), Category("报表属性")]
        public int YPoints
        {
            get { return yPoints; }
            set { yPoints = value; }
        }
        [Description("是否显示关联线，每个序列的最后一个点，和图例中所对应的矩形通过关联线连接"), Category("报表属性")]
        public bool IsShowAssociation;
        /// <summary>
        /// 数据源
        /// </summary>
        private DataTable dataSource;
        [Description("报表的数据源"), Category("报表属性")]
        public DataTable DataSource
        {
            get { return this.dataSource; }
            set
            {
                dataSource = value;
                if (value != null)
                {
                    this.series.Clear();
                    var query = value.AsEnumerable().GroupBy(g => g[SeriesFieldName]);
                    foreach (var item in query)
                    {
                        TSeries _series = new TSeries(item.Key.ToString().Replace("\r\n", string.Empty));
                        foreach (var sonItem in item)
                        {
                            TSeriesPoint seriesPoint = new TSeriesPoint(sonItem[SeriesArgumentDataMember].ToString(), sonItem[SeriesValueDataMember] is DBNull ? 0 : Convert.ToInt32(sonItem[SeriesValueDataMember]), sonItem[SeriesTargetValueDataMember] is DBNull ? 0 : Convert.ToInt32(sonItem[SeriesTargetValueDataMember]));
                            if (SeriesPointID != null) seriesPoint.ID = sonItem[SeriesPointID].ToString();
                            if (SeriesPointName != null) seriesPoint.Name = sonItem[SeriesPointName].ToString();
                            _series.Points.Add(seriesPoint);
                        }
                        this.series.Add(_series);
                    }
                }
            }
        }
        [Description("序列字段名"), Category("报表属性")]
        public string SeriesFieldName { get; set; }
        [Description("绑定的文字信息（名称）(坐标X轴)"), Category("报表属性")]
        public string SeriesArgumentDataMember { get; set; }
        [Description("绑定的值（数据）(坐标Y轴)"), Category("报表属性")]
        public string SeriesValueDataMember { get; set; }
        [Description("绑定的目标值（数据）(坐标Y轴)"), Category("报表属性")]
        public string SeriesTargetValueDataMember { get; set; }
        [Description("报表名称"), Category("报表属性")]
        public string ChartName { get; set; }
        [Description("是否显示报表名称"), Category("报表属性")]
        public bool IsShowChartName { get; set; }
        [Description("序列点的ID"), Category("报表属性")]
        public string SeriesPointID { get; set; }
    [Description("序列点的Name"), Category("报表属性")]
        public string SeriesPointName { get; set; }
        [Description("是否显示Y轴值的百分比"), Category("报表属性")]
        public bool IsShowYPercent { get; set; }
        #endregion 属性
        #region 参数
        /// <summary>
        /// 图片
        /// </summary>
        private Bitmap image;
        /// <summary>
        /// Graphics类对象
        /// </summary>
        private Graphics graphics;
        /// <summary>
        /// 基础画笔
        /// </summary>
        private Pen basePen;

        /// <summary>
        /// X、Y轴的交点
        /// </summary>
        private TPoint intersection { get {return new TPoint { X = this.Width *0.05F, Y = this.Height *0.9F }; } }
        /// <summary>
        /// X轴的长度
        /// </summary>
        private float XLength;
        /// <summary>
        /// Y轴的长度
        /// </summary>
        private float YLength;
        /// <summary>
        /// 基本字体
        /// </summary>
        private Font baseFont;
        /// <summary>
        /// X轴上的点
        /// </summary>
        private List<TPoint> XPoints;

        /// <summary>
        /// X轴上每个点的间隔
        /// </summary>
        private float XOneStep;

        /// <summary>
        /// Y轴上每个点的间隔
        /// </summary>
        private float YOneStep;
        /// <summary>
        /// Y轴上刻度值
        /// </summary>
        private float YOneStepValue;
        /// <summary>
        /// 每个项目的颜色，自动随机取，最多21种
        /// </summary>
        private Color[] itemColors;

        /// <summary>
        /// 项目集合
        /// </summary>
        private List<TChartItem> lst_chartItems;
        /// <summary>
        /// 鼠标的上一位置
        /// </summary>
        private Point oldMousePoint;

        /// <summary>
        /// 图例画笔
        /// </summary>
        private Pen legendPen;

        /// <summary>
        /// 图例的矩形
        /// </summary>
        private TRectangle legendRectangle;

        /// <summary>
        /// 报表标题的矩形
        /// </summary>
        private TRectangle chartNameRectangle;

        /// <summary>
        /// XY轴闭环的矩形
        /// </summary>
        private TRectangle XYRectangle;

        /// <summary>
        /// 待画的提示框集合
        /// </summary>
        private List<TTipRectangle> lst_TipRectangle;

        /// <summary>
        /// 矩形内部画笔
        /// </summary>
        private Pen rectInsidePen;
        #endregion 参数


        public ChartControl()
        {
            InitializeComponent();
            this.Init();

        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            try
            {
                this.InitValue();
                this.InitEvent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 数据初始化
        /// </summary>
        private void InitValue()
        {
            series = new TSeriesCollection();

            baseFont = new Font("宋体", 10, FontStyle.Regular);

            XPoints = new List<TPoint>();

            basePen = new Pen(Color.Black, 1);

            lst_chartItems = new List<TChartItem>();

            legendPen = new Pen(Color.FromArgb(215, 199, 165), 1);

            lst_TipRectangle = new List<TTipRectangle>();

            rectInsidePen = new Pen(new SolidBrush(Color.FromArgb(188, 188, 188)), 1.5F);

            IsShowAssociation = true;

            yPoints = 10;

            IsShowYPercent = true;

            IsShowChartName = true;

        }
        /// <summary>
        /// 初始化图片参数
        /// </summary>
        private void InitImg()
        {
            image = new Bitmap(this.Width==0?100:this.Width, this.Height==0?100:this.Height);
            graphics = Graphics.FromImage(image);
            itemColors = new Color[] { Color.FromArgb(223,167,59) , Color.FromArgb(127,187,42) , Color.FromArgb(217,97,50),Color.FromArgb(220,191,66) , Color.FromArgb(130,216,75) ,
Color.FromArgb(170,72,36),Color.FromArgb(13,151,12),Color.FromArgb(179,180,0),Color.FromArgb(64,177,127),Color.FromArgb(223,189,121),Color.FromArgb(124,210,187),Color.FromArgb(243,159,130),Color.FromArgb(220,164,55),Color.FromArgb(215,94,47),Color.FromArgb(220,191,66)
,Color.FromArgb(132,219,76),Color.FromArgb(167,70,34),Color.FromArgb(17,156,15),Color.FromArgb(177,178,0),Color.FromArgb(62,174,125),Color.LightSeaGreen};
        }

        /// <summary>
        /// 事件初始化
        /// </summary>
        private void InitEvent()
        {
            this.Disposed += TChartControl_Disposed;
            this.SizeChanged += GoldChartControl_SizeChanged;
            this.MyImg.MouseMove += MyImg_MouseMove;
            this.Load += GoldChartControl_Load;
            this.MyImg.MouseClick += MyImg_MouseClick;
        }

        private void MyImg_MouseClick(object sender, MouseEventArgs e)
        {
            //找到鼠标选中的项目并返回参数
            var findItem = lst_chartItems.Where(w => (e.X >= w.X) && (e.X <= w.X + w.Width) && (e.Y <= w.Y + w.Height) && (e.Y >= w.Y)).FirstOrDefault();
            if (findItem != null)
            {
                this.ObjectSelected(this, new TObjectSelectedEventArgs {  SeriesPoint= findItem.Owner });
            }
        }

        /// <summary>
        /// 加载报表的默认显示样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoldChartControl_Load(object sender, EventArgs e)
        {

            TSeries series1 = new TSeries("Series1");
            series1.Points.Add(new TSeriesPoint("A", 10));
            series1.Points.Add(new TSeriesPoint("B", 12));
            series1.Points.Add(new TSeriesPoint("C", 14));
            series1.Points.Add(new TSeriesPoint("D", 17));
            series.Add(series1);
            this.Run();
            series.Clear();
        }

        /// <summary>
        /// 实时获取鼠标所在的当前位置，实时显示柱形图的提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyImg_MouseMove(object sender, EventArgs e)
        {
            if (lst_chartItems.Count == 0) return;
            Point mousePoint = MyImg.PointToClient(Control.MousePosition);
            if (oldMousePoint.Equals(mousePoint)) return;
            var findItem = lst_chartItems.Where(w => (mousePoint.X >= w.X) && (mousePoint.X <= w.X + w.Width) && (mousePoint.Y <= w.Y + w.Height) && (mousePoint.Y >= w.Y)).FirstOrDefault();
            if (findItem != null)
            {
                toolTip.Show(findItem.ToolTipString, MyImg);
            }
            else
            {
                toolTip.Hide(MyImg);
            }
            oldMousePoint = mousePoint;
        }
        /// <summary>
        /// 报表大小发生变化时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoldChartControl_SizeChanged(object sender, EventArgs e)
        {
            if (legendRectangle !=null && (this.Width < legendRectangle.Width*1.5 || this.Height < legendRectangle.Y+ legendRectangle.Height))
            {
                this.InitImg();
                this.InitChartImg();
                //画：增加图表大小
                this.DrawErrorString();
                //显示报表图
                this.ShowChartImg();

            }
            else
            {
                this.Run();
            }
            
        }

        private void TChartControl_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
            this.DisposeSources();
            this.toolTip.Dispose();
        }

        /// <summary>
        /// 开始构建报表
        /// </summary>
        public void Run()
        {
            if (series.Count == 0) return;
            this.InitImg();
            this.InitChartImg();
            //画报表标题
            if (IsShowChartName) this.DrawChartName();
            #region 画X、Y轴上的刻度
            //画图例
            this.DrawLegend();
            //画原点
            this.DrawIntersection();
            //画X、Y轴
            this.DrawXY();
            //画X轴上的刻度
            TSeries first = series.First();
            var allXPoints = first.Points.Select(s => s.Argument).ToList();
            this.DrawXPoints(allXPoints);
            //画Y轴的刻度
            var maxValue = series.SelectMany(s => s.Points).Max(m => m.Value);
            var maxTargetValue = series.SelectMany(s => s.Points).Max(m => m.TargetValue);
            float maxYValue = maxTargetValue > maxValue ? maxTargetValue : maxValue;
            this.DrawYPoints(maxYValue);
            #endregion 画X、Y轴上的刻度
            //画项目柱状图
            var lst_points = series.SelectMany(s => s.Points).ToList();
            this.DrawXItems(lst_points);
            //画提示框
            this.DrawTipRectangle();
            //画关联线
            if(IsShowAssociation) this.DrawAssociation();
            //显示报表图
            this.ShowChartImg();

        }
        //画关联线
        private void DrawAssociation()
        {
            //遍历所有系列，取最后一个点，并和图例中对应的标识通过关联线连接
            foreach (var ser in series)
            {
                Pen seriesPen = new Pen(ser.SeriesColor);
                //序列对应的图例矩形
                var legendRectangle=ser.LegendRectangle;
                //序列的最后一个点
                var lastPoint=ser.Points.LastOrDefault();
                if (lastPoint.TargetTipRectangle == null)
                {
                    //竖线
                    graphics.DrawLine(seriesPen, lastPoint.TipRectangle.Rectangle.X + lastPoint.TipRectangle.Rectangle.Width / 2, lastPoint.TipRectangle.Rectangle.Y, lastPoint.TipRectangle.Rectangle.X + lastPoint.TipRectangle.Rectangle.Width / 2, legendRectangle.Y+ legendRectangle.Width/2);
                    //横线
                    graphics.DrawLine(seriesPen, legendRectangle.X, legendRectangle.Y + legendRectangle.Width / 2, lastPoint.TipRectangle.Rectangle.X + lastPoint.TipRectangle.Rectangle.Width / 2, legendRectangle.Y + legendRectangle.Width / 2);
                }
                else
                {
                    //竖线
                    graphics.DrawLine(seriesPen, lastPoint.TargetTipRectangle.Rectangle.X + lastPoint.TargetTipRectangle.Rectangle.Width / 2, lastPoint.TargetTipRectangle.Rectangle.Y, lastPoint.TargetTipRectangle.Rectangle.X + lastPoint.TargetTipRectangle.Rectangle.Width / 2, legendRectangle.Y + legendRectangle.Width / 2);
                    //横线
                    graphics.DrawLine(seriesPen, legendRectangle.X, legendRectangle.Y + legendRectangle.Width / 2, lastPoint.TargetTipRectangle.Rectangle.X + lastPoint.TargetTipRectangle.Rectangle.Width / 2, legendRectangle.Y + legendRectangle.Width / 2);
                }
            }
        }
        /// <summary>
        /// 画原点
        /// </summary>
        private void DrawIntersection()
        {
            graphics.DrawLine(basePen, intersection.X, intersection.Y, intersection.X - 3, intersection.Y);
            graphics.DrawString("0",baseFont, Brushes.Black, intersection.X-15,intersection.Y-6);
        }
        /// <summary>
        /// 画报表标题
        /// </summary>
        private void DrawChartName()
        {
            string chartName = string.IsNullOrWhiteSpace(this.ChartName) ? "报表" : this.ChartName;
            chartNameRectangle = new TRectangle(this.Width * 0.5F - chartName.Length * 30 / 2, 5, chartName.Length * 30,31);
            graphics.DrawString(chartName, new Font("Arial", 20, FontStyle.Regular), Brushes.Black, chartNameRectangle.X, chartNameRectangle.Y);
        }
        /// <summary>
        /// 初始化报表图
        /// </summary>
        private void InitChartImg()
        {
            graphics.Clear(Color.White);
            graphics.FillRectangle(Brushes.White, 0, 0, this.Width, this.Height);
            //画图片的边框线
            graphics.DrawRectangle(new Pen(Color.WhiteSmoke), 0, 0, image.Width - 1, image.Height - 1);
        }
        /// <summary>
        /// 画X、Y轴
        /// </summary>
        private void DrawXY()
        {
            Pen XYpen = new Pen(Color.FromArgb(125, 94, 32), 1);
            //画X轴
            graphics.DrawLine(XYpen, intersection.X, intersection.Y, this.Width - legendRectangle.Width-20, intersection.Y);
            XLength = this.Width - legendRectangle.Width - 20 - intersection.X;
            //画Y轴
            graphics.DrawLine(XYpen, intersection.X, intersection.Y, intersection.X, this.legendRectangle.Y);
            YLength = intersection.Y - this.legendRectangle.Y;

            //画边界线
            graphics.DrawLine(legendPen, intersection.X, intersection.Y - YLength, intersection.X+XLength, intersection.Y - YLength);
            graphics.DrawLine(legendPen, intersection.X+XLength, intersection.Y - YLength, intersection.X + XLength, intersection.Y);

            XYRectangle = new TRectangle(intersection.X+1, this.legendRectangle.Y+1,XLength-2,YLength-2);
            //填充背景色
            graphics.FillRectangle(new LinearGradientBrush(new PointF(0,0),new PointF(XYRectangle.X+ XYRectangle.Width, XYRectangle.Y+ XYRectangle.Height),Color.FromArgb(249,241,228), Color.FromArgb(249, 241, 228)), XYRectangle);
        }
        /// <summary>
        /// 画X轴上的点
        /// </summary>
        private void DrawXPoints(List<string> XPointsContent)
        {
            XPoints.Clear();
            int counts = XPointsContent.Count;
            XOneStep = (XLength - XLength *0.6F/(counts+1))/ counts;
            int steps = 1;
            foreach (var content in XPointsContent)
            {
                float x = intersection.X + XOneStep * steps;
                //画点
                graphics.DrawLine(basePen, x, intersection.Y, x, intersection.Y + 3);
                //画显示文本
                graphics.DrawString(content, baseFont, Brushes.Black, x - 5, intersection.Y + 5); //设置文字内容及输出位置
                XPoints.Add(new TPoint(x, intersection.Y, content));
                steps++;
            }
        }

        /// <summary>
        /// 画Y轴上的点
        /// </summary>
        private void DrawYPoints(float maxYValue)
        {
            YOneStepValue = maxYValue / yPoints;
            YOneStep = YLength*0.8F / yPoints;
            int steps = 1;
            for (int i = 0; i < yPoints; i++)
            {
                float y = intersection.Y - YOneStep * steps;
                //画点
                graphics.DrawLine(basePen, intersection.X, y, intersection.X-3, y);
                //画显示文本
                string YValue = (YOneStepValue * steps).ToString("0");
                graphics.DrawString(YValue, new Font("Times New Roman", 10, FontStyle.Regular), Brushes.Black, intersection.X -3- YValue.Length*8, y-6); //设置文字内容及输出位置
                string yPercent = string.Format("{0}%", Convert.ToInt32(YValue) * 100 / maxYValue);
                if (IsShowYPercent) graphics.DrawString(yPercent, new Font("Times New Roman", 10, FontStyle.Regular), Brushes.Black, intersection.X - 3 - yPercent.Length * 8-5, y +5); //设置文字内容及输出位置
                steps++;
            }
            
        }

        /// <summary>
        /// 画X轴上的项目【柱状图】
        /// </summary>
        private void DrawXItems(List<TSeriesPoint> lst_points)
        {
            lst_chartItems.Clear();
            lst_TipRectangle.Clear();
            foreach (var point in XPoints)
            {
                var findSeriesPoints = lst_points.Where(w=>w.Argument.Equals(point.name));
                if (findSeriesPoints.Any())
                {
                    float findCount = findSeriesPoints.Count();
                    float oneStep = XOneStep*0.9F / findCount;
                    int steps = 0;
                    foreach (var seriesPoint in findSeriesPoints)
                    {
                        //获取在报表上的值
                        float trueValue = seriesPoint.Value / YOneStepValue * YOneStep;
                        TChartItem item1 = new TChartItem(point.X - XOneStep * 0.8F / 2 + oneStep * steps, point.Y - trueValue, oneStep * 0.8F, trueValue-1);
                        StringBuilder toolTipString = new StringBuilder();
                        lst_chartItems.Add(item1);
                        seriesPoint.Item = item1;
                        //画当前产量
                        graphics.FillRectangle(new SolidBrush(seriesPoint.Owner.Owner.SeriesColor), item1.X, item1.Y, item1.Width, item1.Height);
                        //画内部阴影矩形
                        graphics.DrawRectangle(rectInsidePen, item1.X, item1.Y, item1.Width, item1.Height);
                        //画当前产量提示框
                        seriesPoint.TipRectangle = new TTipRectangle(item1.X + item1.Width / 2, item1.Y, seriesPoint.Owner.Owner.SeriesColor, seriesPoint.Value.ToString());
                        lst_TipRectangle.Add(seriesPoint.TipRectangle);
                        //设置提示信息
                        toolTipString.Append(string.Format("线别：{0}，当前完成量：{1}", seriesPoint.Owner.Owner.Name, seriesPoint.Value));
                        if (seriesPoint.TargetValue > 0F)
                        {
                            toolTipString.Append(string.Format("，目标量：{0},完成率{1}% ！！！", seriesPoint.TargetValue, seriesPoint.Value / seriesPoint.TargetValue * 100));
                            //获取在报表上的值
                            float trueTargetValue = seriesPoint.TargetValue / YOneStepValue * YOneStep;
                            TChartItem item2 = new TChartItem(point.X - XOneStep * 0.8F / 2 + oneStep * steps, point.Y - trueTargetValue, oneStep * 0.8F, trueTargetValue - 1);
                            //画目标产量
                            if (seriesPoint.TargetValue > seriesPoint.Value)
                            {
                                graphics.DrawRectangle(new Pen(seriesPoint.Owner.Owner.SeriesColor), item2.X, item2.Y, item2.Width, item2.Height);
                                lst_chartItems.Add(item2);
                                seriesPoint.TargetItem = item2;
                                //画目标产量提示框
                                seriesPoint.TargetTipRectangle = new TTipRectangle(item2.X + item2.Width / 2, item2.Y, seriesPoint.Owner.Owner.SeriesColor, seriesPoint.TargetValue.ToString());
                                lst_TipRectangle.Add(seriesPoint.TargetTipRectangle);
                                item2.SettoolTipString(toolTipString.ToString());
                            }
                            else
                            {
                                graphics.DrawLine(new Pen(Color.Red, 2), item2.X, item2.Y, item2.X + item2.Width, item2.Y);
                            }

                        }
                        item1.SettoolTipString(toolTipString.ToString());

                        steps++;
                    }
                }
            }
            
        }

        /// <summary>
        /// 画提示框
        /// </summary>
        private void DrawTipRectangle()
        {
            foreach (var tipRectangle in lst_TipRectangle)
            {
                tipRectangle.DrawTipRectangle(graphics);
            }
        }
        /// <summary>
        /// 画图例
        /// </summary>
        private void DrawLegend()
        {
            //序列名称最长的长度
            var seriesNameMaxLength = series.Select(s => s.Name).Max(m => m.Length);
            int seriesCount = series.Count;
            int steps = 0;
            int colorIndex = 0;
            float seriesWidth = 20;
            float seriesHeight = 14;
            //图例原点坐标
            legendRectangle = new TRectangle { X = this.Width - 10 -seriesWidth-2-12*seriesNameMaxLength, Y = chartNameRectangle.Y+ chartNameRectangle.Height+5 };
            //画具体的series
            foreach (var item in series)
            {
                if(item.SeriesColor==Color.Empty)
                {
                    Color itemColor = Color.Empty;
                    //项目颜色
                    if (colorIndex > itemColors.Length)
                    {
                        itemColor = itemColors[colorIndex - itemColors.Length];
                    }
                    {
                        itemColor = itemColors[colorIndex];
                    }
                    item.SeriesColor = itemColor;
                    colorIndex++;
                }

                TRectangle seriesRect = new TRectangle(legendRectangle.X + 5, legendRectangle.Y + seriesHeight * steps + 4 * (steps + 1), seriesWidth, seriesHeight);
                graphics.FillRectangle(new SolidBrush(item.SeriesColor), seriesRect);
                item.LegendRectangle = seriesRect;
                //画内部阴影矩形
                graphics.DrawRectangle(rectInsidePen, seriesRect.X, seriesRect.Y, seriesRect.Width, seriesRect.Height);
                graphics.DrawString(item.Name, new Font("宋体", 10, FontStyle.Regular), Brushes.Black, seriesRect.X+ seriesWidth, seriesRect.Y);
                steps++;
            }
            legendRectangle.Width = this.Width - 5 - legendRectangle.X;
            legendRectangle.Height= seriesHeight * seriesCount + 4* seriesCount+4;
            graphics.DrawRectangle(legendPen, legendRectangle);
        }
        /// <summary>
        /// 显示画好的报表图片
        /// </summary>
        private void ShowChartImg()
        {
            try
            {
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    MyImg.Image = Image.FromStream(ms);
                }
                this.DisposeSources();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        /// <summary>
        /// 画错误提示
        /// </summary>
        private void DrawErrorString()
        {
            if (this.Width > 0 && this.Height >0)
            {
                graphics.DrawString("增加图表大小", new Font("Arial", this.Width * 0.06F, FontStyle.Regular), Brushes.Black,
                    this.Width * 0.3F, this.Height * 0.5F);
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        private void DisposeSources()
        {
            this.graphics.Dispose();
            this.image.Dispose();
        }

        public delegate void ObjectSelectedHandle(object sender, TObjectSelectedEventArgs e);
        /// <summary>
        /// 对象选中事件
        /// </summary>
        public event ObjectSelectedHandle ObjectSelected;
        
    }
}
