using Qyz.BLL.Core;
using Qyz.Model.Common;
using Qyz.UI.Main.View;
using Qyz.UI.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Qyz.UI.Main.ViewModel
{
    public class RoleSettingViewModel : ViewModelBase
    {
        #region 构造函数
        public RoleSettingViewModel()
        {
            InitButtonState();
            RoleList = new ObservableCollection<Sys_Roles>(new UserInfoBLL().GetRoleInfo());
        }
        #endregion

        #region 绑定的属性
        private ObservableCollection<Sys_Roles> _RoleList = new ObservableCollection<Sys_Roles>();
        /// <summary>
        /// 系统列表
        /// </summary>
        public ObservableCollection<Sys_Roles> RoleList
        {
            get { return _RoleList; }
            set { _RoleList = value; }
        }

        private Sys_Roles _SelectedRole;
        /// <summary>
        /// 选中的系统
        /// </summary>
        public Sys_Roles SelectedRole
        {
            get { return _SelectedRole; }
            set { _SelectedRole = value; RaisePropertyChanged("SelectedRole"); }
        }
        #endregion


        #region 初始化界面
        /// <summary>
        /// 初始化按钮状态
        /// </summary>
        private void InitButtonState()
        {
            AllowDelete = true;
            AllowEdit = true;
            AllowAdd = true;
        }
        #endregion

        #region 命令
        /// <summary>
        /// 新增命令
        /// </summary>
        /// <returns></returns>
        public override void ExecuteAdd()
        {
            Sys_Roles role = new Sys_Roles();
            role.ID = new UserInfoBLL().GetMaxRoleID() + 1;
            FrmRoleSettingEdit edit = new FrmRoleSettingEdit(role);
            edit.SaveEvent += (r) =>
            {
                RoleList.Add(r);
            };
            edit.ShowDialog();
        }
        /// <summary>
        /// 修改命令
        /// </summary>
        /// <returns></returns>
        public override void ExecuteEdit()
        {
            if (SelectedRole != null)
            {
                Sys_Roles role = new Sys_Roles();
                role.ID = SelectedRole.ID;
                role.Name = SelectedRole.Name;
                role.Remark = SelectedRole.Remark;


                FrmRoleSettingEdit edit = new FrmRoleSettingEdit(role);
                edit.SaveEvent += (s) =>
                {
                    RoleList[RoleList.IndexOf(SelectedRole)] = s;
                    SelectedRole = s;
                };
                edit.ShowDialog();
            }
        }
        /// <summary>
        /// 删除命令
        /// </summary>
        /// <returns></returns>
        public override void ExecuteDelete()
        {
            if (SelectedRole != null)
            {
                if (SelectedRole.Name == "管理员" && SelectedRole.Name == "超级管理员")
                {
                    System.Windows.MessageBox.Show("管理员信息不得删除");
                    return;
                }

                UserInfoBLL bll = new UserInfoBLL();
                if (bll.DeleteRoleInfo(SelectedRole))
                {
                    RoleList.Remove(SelectedRole);
                    SelectedRole = null;
                }
            }
        }
        #endregion
    }
}
