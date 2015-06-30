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
    public class AccountSettingViewModel : ViewModelBase
    {
         #region 构造函数
        public AccountSettingViewModel()
        {
            InitButtonState();
            AccountList = new ObservableCollection<Sys_Accounts>(new UserInfoBLL().GetAccountInfo());
        }
        #endregion

        #region 绑定的属性
        private ObservableCollection<Sys_Accounts> _AccountList = new ObservableCollection<Sys_Accounts>();
        /// <summary>
        /// 系统列表
        /// </summary>
        public ObservableCollection<Sys_Accounts> AccountList
        {
            get { return _AccountList; }
            set { _AccountList = value; }
        }

        private Sys_Accounts _SelectedAccount;
        /// <summary>
        /// 选中的系统
        /// </summary>
        public Sys_Accounts SelectedAccount
        {
            get { return _SelectedAccount; }
            set { _SelectedAccount = value; RaisePropertyChanged("SelectedAccount"); }
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
            Sys_Accounts account = new Sys_Accounts();
            account.ID = new UserInfoBLL().GetMaxAccountID() + 1;
            FrmAccountSettingEdit edit = new FrmAccountSettingEdit(account);
            edit.SaveEvent += (r) =>
            {
                AccountList.Add(r);
            };
            edit.ShowDialog();
        }
        /// <summary>
        /// 修改命令
        /// </summary>
        /// <returns></returns>
        public override void ExecuteEdit()
        {
            if (SelectedAccount != null)
            {
                Sys_Accounts account = new Sys_Accounts();
                account.ID = SelectedAccount.ID;
                account.AccountName = SelectedAccount.AccountName;
                account.PassWord = SelectedAccount.PassWord;
                account.UserName = SelectedAccount.UserName;
                account.Address = SelectedAccount.Address;
                account.Email = SelectedAccount.Email;
                account.Phone = SelectedAccount.Phone;
                account.Discribtion = SelectedAccount.Discribtion;
                account.Remark = SelectedAccount.Remark;
                account.RoleID = SelectedAccount.RoleID;
                FrmAccountSettingEdit edit = new FrmAccountSettingEdit(account);
                edit.SaveEvent += (s) =>
                {
                    AccountList[AccountList.IndexOf(SelectedAccount)] = s;
                    SelectedAccount = s;
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
            if (SelectedAccount != null)
            {
                if(SelectedAccount.AccountName=="00")
                {
                    System.Windows.MessageBox.Show("超级管理员账户不能删除");
                    return;
                }
                UserInfoBLL bll = new UserInfoBLL();
                if (bll.DeleteAccount(SelectedAccount))
                {
                    AccountList.Remove(SelectedAccount);
                    SelectedAccount = null;
                }
            }
        }
        #endregion
    }
}
