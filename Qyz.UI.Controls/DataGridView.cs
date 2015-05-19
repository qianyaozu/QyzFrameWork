using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Collections;
using System.Data;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Qyz.UI.Controls
{ 
    public class DataGridView : DataGrid
    { 
        public static readonly DependencyProperty DataSourseProperty = DependencyProperty.RegisterAttached(
           "DataSourse", typeof(DataView), typeof(DataGridView), new PropertyMetadata(null));

         
        public DataGridView()
        {
            this.ColumnWidth = DataGridLength.Auto;
            this.SizeChanged += new SizeChangedEventHandler(AutoFillDataGrid_SizeChanged);
            //绑定DataGrid
            this.SetBinding(DataGridView.ItemsSourceProperty, new Binding("DataSourse") { Source = this });
            
        }
        void AutoFillDataGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.Columns.Count * 40 < this.ActualWidth)
            {
                AutoFill = true;
            }
            else
            {
                AutoFill = false;
            }
            if (AutoFill)
            {
                this.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                LoadWidth();
            }
            else
            {
                this.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            }
        }
        private double MeasureTextWidth(string text)
        {
            FormattedText formattedText = new FormattedText(
            text,
            System.Globalization.CultureInfo.InvariantCulture,
            FlowDirection.LeftToRight,
            new Typeface("Microsoft YaHei"),
            14,
            Brushes.Black
            );
            return formattedText.WidthIncludingTrailingWhitespace;
        }

        void LoadWidth()
        {
            #region[原有逻辑]
            //int weight = 0;
            //double fix = 0; 
            //Queue<DataGridColumn> queue = new Queue<DataGridColumn>();
            //foreach (DataGridColumn column in this.Columns)
            //{ 
            //    if (column.Visibility == Visibility.Visible)
            //    {
            //        if (column is DataGridTemplateColumn)
            //        {
            //            fix += column.ActualWidth;
            //        }
            //        else if (column.Header != null && column.Header.ToString() != "IsSelect")
            //        { 
            //            weight += 1;
            //            queue.Enqueue(column);
            //        }
            //    }
            //}
            //double width = this.ActualWidth - this.paddingRight - fix - this.Padding.Left - this.Padding.Right;
            //double actualWidth = 0;

            //while (queue.Count > 0)
            //{
            //    DataGridColumn column = queue.Dequeue();
            //    actualWidth = width / weight;
            //    if (actualWidth < column.MinWidth)
            //    {
            //        actualWidth = column.MinWidth;
            //    }
            //    if (actualWidth > column.MaxWidth)
            //    {
            //        actualWidth = column.MaxWidth;
            //    }
            //    column.Width = new DataGridLength(actualWidth);
            //}
            #endregion
            #region [权重] 
            double actualWidth = 0;
            string weight = "";
            List<DataGridColumn> queue = new List<DataGridColumn>();
            double fix =0;
            foreach (DataGridColumn column in this.Columns)
            {
                if (column.Visibility == Visibility.Visible)
                {
                    if (column is DataGridTemplateColumn)
                    {
                        fix += column.ActualWidth; 
                    }
                    else if (column.Header != null && column.Header.ToString() != "IsSelect")
                    {
                        double max = this.MinColumnWidth;
                        DataView dv=this.DataSourse as DataView;
                        for (int i = 0; i < dv.Count; i++)
                        {
                            double t = MeasureTextWidth(dv[i][column.Header.ToString()].ToString());
                            if (t > max)
                            {
                                max = t;
                            }
                        }
                        actualWidth += max;
                        weight += "," + max.ToString();
                        queue.Add(column);
                    }
                }
            } 
            double width = this.ActualWidth - this.paddingRight - fix - this.Padding.Left - this.Padding.Right;
            double cell = width / actualWidth;
            if (queue.Count > 0)
            {
                string[] cells = weight.Substring(1).Split(',');
                for (int i = 0; i < queue.Count; i++)
                {
                    queue[i].Width = cell * Convert.ToDouble(cells[i]);
                }
            }
            #endregion
        }

        private double paddingRight = 0;
        public double PaddingRight
        {
            get
            {
                return this.paddingRight;
            }
            set
            {
                this.paddingRight = value;
            }
        }
       
        public DataView DataSourse
        {
            get
            {
                return this.GetValue(DataSourseProperty) as DataView;
            }
            set
            {
                SetValue(DataSourseProperty, value);

                foreach (DataGridColumn column in this.Columns)
                {
                    if (!(column is DataGridTemplateColumn))
                    {
                        column.IsReadOnly = true;
                    }
                    if (column.Header != null)
                    {
                        if (_unvisibilitycolumns != null && _unvisibilitycolumns.Contains(column.Header.ToString()))
                        {
                            column.Visibility = Visibility.Collapsed;
                        }
                        if (_timestringformatcolumns != null && _timestringformatcolumns.Contains(column.Header.ToString()))
                        {
                            column.ClipboardContentBinding.StringFormat = "yyyy-MM-dd HH:mm:ss";
                        }
                        if (column.Header.ToString() == "IsSelect")
                        {
                            column.Visibility = Visibility.Collapsed; 
                        }
                    }
                }

                if (this.Columns.Count * 40 < this.ActualWidth)
                {
                    AutoFill = true;
                }
                else
                {
                    AutoFill = false;
                }

                if (AutoFill)
                {
                    this.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                    LoadWidth();
                }
                else
                {
                    this.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                }
            }
        } 
        private string[] _unvisibilitycolumns = null;
        /// <summary>
        /// 设置隐藏的列集合
        /// </summary>
        public string[] UnVisibilityColumns
        {
            get { return _unvisibilitycolumns; }
            set { _unvisibilitycolumns = value; }
        }


        private string[] _timestringformatcolumns = null;
        /// <summary>
        /// 设置内容为时间的列
        /// </summary>
        public string[] TimeStringFormatColumns
        {
            get { return _timestringformatcolumns; }
            set { _timestringformatcolumns = value; }
        }

        private bool _autofill = true;
        /// <summary>
        /// 设置表格是否自适应填充
        /// </summary>
        public bool AutoFill
        {
            get { return _autofill; }
            set { _autofill = value; }
        }
        private bool _showcheckbox = false;
        /// <summary>
        /// 是否显示CheckBox
        /// </summary>
        public bool ShowCheckBox
        {
            get { return _showcheckbox; }
            set { _showcheckbox = value; LoadWidth(); }
        }
    }

}
