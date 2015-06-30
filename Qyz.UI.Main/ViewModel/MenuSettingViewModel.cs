using Qyz.BLL.Core;
using Qyz.FrameWork.Core;
using Qyz.Model.Common;
using Qyz.UI.Controls;
using Qyz.UI.Main.View;
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
        #region 构造函数
        public MenuSettingViewModel()
        {
            InitButtonState();
            new MenuInfoBLL().GetMenuInfo((int)GlobalVariable.systemType).ForEach(p => MenuList.Add(p));
        }
        #endregion

        #region 绑定的属性
        private ObservableCollection<Sys_Menus> _MenuList = new ObservableCollection<Sys_Menus>();
        /// <summary>
        /// 菜单列表
        /// </summary>
        public ObservableCollection<Sys_Menus> MenuList
        {
            get { return _MenuList; }
            set { _MenuList = value; }
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
        /// <summary>
        /// 新增命令
        /// </summary>
        /// <returns></returns>
        public override void ExecuteAdd()
        {
            Sys_Menus menu = new Sys_Menus();
            menu.ID = MenuList.Max(p => p.ID) + 1;
            menu.SystemID = (int)GlobalVariable.systemType;
            FrmMenuSettingEdit edit = new FrmMenuSettingEdit(menu);
            edit.SaveEvent += (m) =>
            {
                MenuList.Add(m);
                GlobalVariable.RefleshMenuInfo();
            };
            edit.ShowDialog(); 
        }
        /// <summary>
        /// 修改命令
        /// </summary>
        /// <returns></returns>
        public override void ExecuteEdit()
        {
            if (SelectedMenu != null)
            {
                Sys_Menus menu = new Sys_Menus();
                menu.ID = SelectedMenu.ID;
                menu.Name = SelectedMenu.Name;
                menu.ImagePath = SelectedMenu.ImagePath;
                menu.SystemID = SelectedMenu.SystemID;
                menu.Remark = SelectedMenu.Remark;

                FrmMenuSettingEdit edit = new FrmMenuSettingEdit(menu);
                edit.SaveEvent += (m) =>
                {
                    MenuList[MenuList.IndexOf(SelectedMenu)] = m;
                    SelectedMenu = m;
                    GlobalVariable.RefleshMenuInfo();
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
            if (SelectedMenu != null)
            {
                if (SelectedMenu.SystemID == -1)
                {
                    MessageBoxEx.Show("系统设置菜单不可删除", "提示", MessageBoxButtonType.OK);
                    return;
                }
                MenuInfoBLL bll = new MenuInfoBLL();
                if (bll.DeleteMenuInfo(SelectedMenu))
                {
                    MenuList.Remove(SelectedMenu);
                    SelectedMenu = null;
                    GlobalVariable.RefleshMenuInfo();
                } 
            } 
        }
        #endregion

    }
}
