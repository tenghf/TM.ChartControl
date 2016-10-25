namespace TM.Controls.ChartControl
{
    partial class ChartControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MyImg = new System.Windows.Forms.PictureBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.MyImg)).BeginInit();
            this.SuspendLayout();
            // 
            // MyImg
            // 
            this.MyImg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MyImg.Location = new System.Drawing.Point(0, 0);
            this.MyImg.Name = "MyImg";
            this.MyImg.Size = new System.Drawing.Size(793, 321);
            this.MyImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.MyImg.TabIndex = 0;
            this.MyImg.TabStop = false;
            // 
            // GoldChartControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MyImg);
            this.Name = "GoldChartControl";
            this.Size = new System.Drawing.Size(793, 321);
            ((System.ComponentModel.ISupportInitialize)(this.MyImg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox MyImg;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
