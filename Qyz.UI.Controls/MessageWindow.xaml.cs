using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes; 
using System.IO;
using System.Xml; 

namespace Qyz.UI.Controls
{
    public partial class MessageWindow : Window
    {

        public MessageWindow(string text, string caption, MessageBoxButtonType buttons)
        {

            this.InitializeComponent();

            this.LabTitle.Content = (((caption != null) && (caption != string.Empty)) && (caption != "")) ? caption : "提示";
            this.LabTips.Text = text;
            if (buttons == MessageBoxButtonType.OK)
            {
                vbutton2.Visibility = Visibility.Collapsed;
            }
        }
        internal bool? DrawWindows()
        {
             base.ShowDialog();
             return this.DialogResult;
        }

        private void buttonclose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void vbutton1_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void vbutton2_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

       

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.DragMove();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Topmost = true;
        }
    }
}
