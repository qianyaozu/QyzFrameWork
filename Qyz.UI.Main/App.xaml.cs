using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Qyz.UI.Main
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            #region 加载资源
            ResourceDictionary rd = new ResourceDictionary();
            rd.Source = new Uri("pack://application:,,,/Qyz.UI.Themes;component/defaultTheme/defaultImage.xaml", UriKind.RelativeOrAbsolute);
            ResourceDictionary rd1 = new ResourceDictionary();
            rd1.Source = new Uri("pack://application:,,,/Qyz.UI.Themes;component/defaultTheme/defaultControls.xaml", UriKind.RelativeOrAbsolute);
            ResourceDictionary rd2 = new ResourceDictionary();
            rd2.Source = new Uri("pack://application:,,,/Qyz.UI.Themes;component/defaultTheme/defaultWindow.xaml", UriKind.RelativeOrAbsolute);

            this.Resources.MergedDictionaries.Add(rd);
            this.Resources.MergedDictionaries.Add(rd1);
            this.Resources.MergedDictionaries.Add(rd2);
            #endregion

            MainWindow main = new MainWindow();
            
            FrmLogin login = new FrmLogin();
            if (!login.ShowDialog().Value)
            {
                Environment.Exit(0);
            }
            
            main.Show();
           
        }
    }
}
