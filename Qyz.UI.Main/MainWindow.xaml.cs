using Qyz.FrameWork.Core;
using Qyz.Model.Common; 
using Qyz.UI.Base;
using Qyz.UI.Main.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Qyz.UI.Main
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel viewModel = new MainWindowViewModel();
        
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
            this.MaxHeight = SystemParameters.WorkArea.Height;
            this.MaxWidth = SystemParameters.WorkArea.Width;
        } 
     

        #region 窗体事件
        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.ThumbSizeChange.Visibility = Visibility.Hidden;
            }
            else
            {
                this.ThumbSizeChange.Visibility = Visibility.Visible;
            }
        }

        private void ThumbSizeChange_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            Point position = Mouse.GetPosition(this);
            if (position.X > 10 && position.Y > 10)
            {
                this.Height = position.Y;
                this.Width = position.X;
            }
        }

        

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Maximized)
            {
                this.WindowState = System.Windows.WindowState.Normal;
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void btnLeft_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button btn = sender as Button;
            TransformGroup transform = new TransformGroup();
            RotateTransform rotate;
            if (this.LayoutRoot.ColumnDefinitions[1].Width == new GridLength(0))
                rotate = new RotateTransform(0, btn.ActualWidth / 2, btn.ActualHeight / 2);
            else
                rotate = new RotateTransform(180, btn.ActualWidth / 2, btn.ActualHeight / 2);
            transform.Children.Add(rotate);
            btn.RenderTransform = transform;
            this.LayoutRoot.ColumnDefinitions[1].Width = this.LayoutRoot.ColumnDefinitions[1].Width == new GridLength(0) ? new GridLength(180)
                : new GridLength(0);
        }

        private void TitleBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void closebtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            viewModel.CloseMdiControlCommand((sender as Grid).Tag.ToString());
        }
        #endregion

       

       

       

      
        
    }
}
