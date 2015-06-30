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
    /// FrmAccountSettingEdit.xaml 的交互逻辑
    /// </summary>
    public partial class FrmAccountSettingEdit : DialogWindow
    {
        public event Action<Sys_Accounts> SaveEvent;
        public static DependencyProperty MyTitleProperty = DependencyProperty.Register("MyTitle", typeof(string), typeof(FrmAccountSettingEdit), new PropertyMetadata("新增账户"));
        public static DependencyProperty MainAccountProperty = DependencyProperty.Register("MainAccount", typeof(Sys_Accounts), typeof(FrmAccountSettingEdit), new PropertyMetadata(null));
        public static DependencyProperty RoleListProperty = DependencyProperty.Register("RoleList", typeof(List<Sys_Roles>), typeof(FrmAccountSettingEdit), new PropertyMetadata(null));
        public static DependencyProperty SelectedRoleProperty = DependencyProperty.Register("SelectedRole", typeof(Sys_Roles), typeof(FrmAccountSettingEdit), new PropertyMetadata(null));
       
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
        public Sys_Accounts MainAccount
        {
            get { return (Sys_Accounts)GetValue(MainAccountProperty); }
            set { SetValue(MainAccountProperty, value); }
        }
        /// <summary>
        /// 角色集合
        /// </summary>
        public List<Sys_Roles> RoleList
        {
            get { return GetValue(RoleListProperty) as List<Sys_Roles>; }
            set { SetValue(RoleListProperty, value); }
        }

        /// <summary>
        /// 选中的角色
        /// </summary>
        public Sys_Roles SelectedRole
        {
            get { return GetValue(SelectedRoleProperty) as Sys_Roles; }
            set
            {
                SetValue(SelectedRoleProperty, value); 
            }
        }
        public FrmAccountSettingEdit(Sys_Accounts account)
        {
            InitializeComponent();
            this.DataContext = this;
            MainAccount = account;
            MyTitle = account.AccountName == null ? "新增账户" : "编辑账户";
            RoleList = new UserInfoBLL().GetRoleInfo(); 
            SelectedRole = account.AccountName != null ? RoleList.Find(p => p.ID == account.RoleID) : RoleList[0];
           
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
                    if (MyTitle == "新增账户")
                        new UserInfoBLL().InsertAccount(MainAccount);
                    else
                        new UserInfoBLL().UpdateAccount(MainAccount);

                    if (SaveEvent != null)
                        SaveEvent(MainAccount);
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
            if (MainAccount.AccountName == "") return false;
            return true;
        }
        #endregion

    }
}
