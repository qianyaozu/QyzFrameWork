using Qyz.FrameWork.Core;
using Qyz.Model.Common;
using Qyz.Model.Common.Model;
using Qyz.UI.Base;
using Qyz.UI.Main.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
        MainWindowViewModel VM = new MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = VM;
        }

        #region  属性改变事件
        public event PropertyChangedEventHandler PropertyChanged; 
        protected void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region 字段
        private ObservableCollection<MenuModel> _menuList = new ObservableCollection<MenuModel>();
        private ObservableCollection<Module> _secMenuList = new ObservableCollection<Module>();
        private string _userName;
        #endregion

        #region CLR属性
        /// <summary>
        /// 登录人员的名字
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; RaisePropertyChanged("UserName"); }
        }
        /// <summary>
        /// 二级菜单
        /// </summary>
        public ObservableCollection<Module> SecMenuList
        {
            get { return _secMenuList; }
            set { _secMenuList = value; }
        }


        private Qyz.Model.Common.Module _SelectSecMenu;
        /// <summary>
        /// 选中的二级菜单
        /// </summary>
        public Qyz.Model.Common.Module SelectSecMenu
        {
            get { return _SelectSecMenu; }
            set { _SelectSecMenu = value; RaisePropertyChanged("SelectSecMenu"); }
        }
        /// <summary>
        /// 一级菜单
        /// </summary>
        public ObservableCollection<MenuModel> MenuList
        {
            get { return _menuList; }
            set { _menuList = value; }
        }
        #endregion

        #region 窗体事件
        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void ThumbSizeChange_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {

        }

        private void MenuCheck_Checked(object sender, RoutedEventArgs e)
        {

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
        #endregion

      
        private void SelectSecondMenuEvent()
        {
            if(SelectSecMenu!=null)
            {
                object start = ReflectMethod.CreateInstance(SelectSecMenu.DllName, SelectSecMenu.StartUpClass, true);
                if (start != null)
                {
                    MethodInfo method = ((Type)start).GetMethods(BindingFlags.Static | BindingFlags.Public).Where(p => p.Name == "MainControl").FirstOrDefault();
                    if(method!=null)
                    {
                        object[] obs = SelectSecMenu.Parameter.Split(',');
                        MdiControl mdi = method.Invoke(null, obs) as MdiControl;
                        mdi.Tag = SelectSecMenu.Name;

                        TabItem tbi = new TabItem();
                        tbi.Style = (Style)this.FindResource("TabItemStyle1");
                        tbi.Header = SelectSecMenu.Name;
                        MainControl.Items.Add(tbi);
                        return;
                    } 
                }
                MessageBox.Show("加载失败");
            }
        }
    }
}
