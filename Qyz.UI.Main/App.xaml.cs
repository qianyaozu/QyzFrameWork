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
            ResourceDictionary rd = new ResourceDictionary();
            rd.Source = new Uri("pack://application:,,,/Qyz.UI.Themes;component/defaultTheme/defaultImage.xaml", UriKind.RelativeOrAbsolute);
            ResourceDictionary rd1 = new ResourceDictionary();
            rd1.Source = new Uri("pack://application:,,,/Qyz.UI.Themes;component/defaultTheme/defaultControls.xaml", UriKind.RelativeOrAbsolute);

            this.Resources.MergedDictionaries.Add(rd);
            this.Resources.MergedDictionaries.Add(rd1);

            FrmLogin login = new FrmLogin();
            login.Show();
        }
    }
}
