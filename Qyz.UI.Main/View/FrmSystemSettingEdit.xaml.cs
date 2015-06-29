using Qyz.BLL.Core;
using Qyz.Model.Common;
using Qyz.UI.Base;
using Qyz.UI.MVVM.Command;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Qyz.UI.Main.View
{
    /// <summary>
    /// FrmSystemSettingEdit.xaml 的交互逻辑
    /// </summary>
    public partial class FrmSystemSettingEdit : DialogWindow
    {

        public event Action<Sys_Systems> SaveEvent;
        public static DependencyProperty MyTitleProperty = DependencyProperty.Register("MyTitle", typeof(string), typeof(FrmSystemSettingEdit), new PropertyMetadata("新增系统"));
        public static DependencyProperty SystemProperty = DependencyProperty.Register("MainSystem", typeof(Sys_Systems), typeof(FrmSystemSettingEdit), new PropertyMetadata(null));

        /// <summary>
        /// 当前系统名称
        /// </summary>
        public string MyTitle
        {
            get { return GetValue(MyTitleProperty).ToString(); }
            set { SetValue(MyTitleProperty, value); }
        }
        /// <summary>
        /// 编辑的系统
        /// </summary>
        public Sys_Systems MainSystem
        {
            get { return (Sys_Systems)GetValue(SystemProperty); }
            set { SetValue(SystemProperty, value); }
        }

        public FrmSystemSettingEdit(Sys_Systems sys)
        {
            InitializeComponent();
            this.DataContext = this;
            MainSystem = sys;
            if (sys.Name == null || sys.Name == "")
                MyTitle = "新增系统";
            else
                MyTitle = "编辑系统";
        }


        #region 命令
        /// <summary>
        /// 保存命令
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand((para) =>
                {
                    if (!ValiateData())
                    {
                        return;
                    }
                    //保存到数据库中
                    if (MyTitle == "新增菜单")
                        new SystemInfoBLL().AddSystemInfo(MainSystem);
                    else
                        new SystemInfoBLL().UpdateSystemInfo(MainSystem);

                    if (SaveEvent != null)
                        SaveEvent(MainSystem);
                    this.Close();

                });
            }
        }
        /// <summary>
        /// 取消命令
        /// </summary>
        public ICommand CancelCommand
        {
            get
            {
                return new RelayCommand((p) =>
                    {
                        this.Close();
                    });
            }
        }


        #endregion
        #region 私有方法
        /// <summary>
        /// 验证菜单数据
        /// </summary>
        /// <returns></returns>
        private bool ValiateData()
        {
            if (MainSystem.Name == "") return false;
            return true;
        }
        #endregion


    }
}
