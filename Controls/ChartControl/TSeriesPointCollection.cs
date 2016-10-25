using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TM.Controls.ChartControl
{
    /// <summary>
    /// TSeriesPoint集合
    /// </summary>
    public class TSeriesPointCollection : ObservableCollection<TSeriesPoint>
    {
        /// <summary>
        /// 宿主
        /// </summary>
        public TSeries Owner { get; internal set; }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            foreach (TSeriesPoint item in e.NewItems)
            {
                item.Owner = this;
            }
        }

    }
}
