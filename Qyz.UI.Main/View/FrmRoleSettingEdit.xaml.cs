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
using System.Windows.Shapes;

namespace Qyz.UI.Main.View
{
    /// <summary>
    /// FrmRoleSettingEdit.xaml 的交互逻辑
    /// </summary>
    public partial class FrmRoleSettingEdit : DialogWindow
    {
         public event Action<Sys_Roles> SaveEvent;
        public static DependencyProperty MyTitleProperty = DependencyProperty.Register("MyTitle", typeof(string), typeof(FrmRoleSettingEdit), new PropertyMetadata("新增角色"));
        public static DependencyProperty RoleProperty = DependencyProperty.Register("MainRole", typeof(Sys_Roles), typeof(FrmRoleSettingEdit), new PropertyMetadata(null));

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
        public Sys_Roles MainRole
        {
            get { return (Sys_Roles)GetValue(RoleProperty); }
            set { SetValue(RoleProperty, value); }
        }


        public FrmRoleSettingEdit(Sys_Roles sys)
        {
            InitializeComponent();
            this.DataContext = this;
            MainRole = sys; 
            MyTitle = sys.Name == null ? "新增角色" : "编辑角色";
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
                        new UserInfoBLL().AddRoleInfo(MainRole);
                    else
                    {
                        if (MainRole.ID == 1 || MainRole.ID == 2)
                        {
                            MessageBox.Show("管理员账号名称不能修改");
                            return;
                        }
                        new UserInfoBLL().UpdateRoleInfo(MainRole);
                    }

                    if (SaveEvent != null)
                        SaveEvent(MainRole);
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
            if (MainRole.Name == "") return false;
            return true;
        }
        #endregion
    }
}
