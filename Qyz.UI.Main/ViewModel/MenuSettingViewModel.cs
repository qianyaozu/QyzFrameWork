using Qyz.FrameWork.Core;
using Qyz.Model.Common;
using Qyz.UI.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Qyz.UI.Main.ViewModel
{
    public class MenuSettingViewModel : ViewModelBase
    {

        public MenuSettingViewModel()
        {
            GlobalVariable.menuList.ForEach(p => MenuList.Add(p));
            InitButtonState();
        }
        #region 绑定的属性
        private ObservableCollection<Sys_Menus> _menuList = new ObservableCollection<Sys_Menus>();
        /// <summary>
        /// 菜单列表
        /// </summary>
        public ObservableCollection<Sys_Menus> MenuList
        {
            get { return _menuList; }
            set { _menuList = value; }
        }

        private Sys_Menus _SelectedMenu;
        /// <summary>
        /// 选中的菜单
        /// </summary>
        public Sys_Menus SelectedMenu
        {
            get { return _SelectedMenu; }
            set { _SelectedMenu = value; RaisePropertyChanged("SelectedMenu"); }
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
        public override bool ExecuteAdd()
        {
            return base.ExecuteAdd();
        }

        public override bool ExecuteEdit()
        {
            if (SelectedMenu != null)
            {

            }
            return true;
        }
        public override bool ExecuteDelete()
        {
            if (SelectedMenu != null)
            {

            }
            return true;
        }
        #endregion

    }
}
