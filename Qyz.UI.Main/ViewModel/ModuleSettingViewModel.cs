using Qyz.BLL.Core;
using Qyz.FrameWork.Core;
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
    public class ModuleSettingViewModel : ViewModelBase
    {
        string MenuID;
        #region 构造函数
        public ModuleSettingViewModel(string menuID)
        {
            AllowDelete = true;
            AllowEdit = true;
            AllowAdd = true;
            MenuID = menuID;
            ModuleList = new ObservableCollection<Sys_Modules>(new ModuleInfoBLL().GetModuleInfoList(Convert.ToInt32(menuID)));
        }
        #endregion

        #region 绑定的属性
        private ObservableCollection<Sys_Modules> _moduleList;
        /// <summary>
        /// 模块列表
        /// </summary>
        public ObservableCollection<Sys_Modules> ModuleList
        {
            get { return _moduleList; }
            set { _moduleList = value; }
        }

        private Sys_Modules _SelectedModule;
        /// <summary>
        /// 选中的模块
        /// </summary>
        public Sys_Modules SelectedModule
        {
            get { return _SelectedModule; }
            set { _SelectedModule = value; RaisePropertyChanged("SelectedModule"); }
        }
        #endregion

        #region  命令
        public override bool ExecuteAdd()
        {
            Sys_Modules module = new Sys_Modules();
            module.ID = new ModuleInfoBLL().GetMaxModuleID();
            module.MenuID = Convert.ToInt32(MenuID);
            FrmModuleSettingEdit edit = new FrmModuleSettingEdit(module);
            edit.SaveEvent += (m) =>
            {
                ModuleList.Add(m);
            };
            edit.ShowDialog();

            return true;
        }
        public override bool ExecuteDelete()
        { 
            if (SelectedModule != null)
            { 
                ModuleInfoBLL bll = new ModuleInfoBLL(); 
                if (bll.DeleteModuleInfo(SelectedModule))
                {
                    ModuleList.Remove(SelectedModule);
                    SelectedModule = null;
                }
            }
            return true;
        }
        public override bool ExecuteEdit()
        {
            if (SelectedModule != null)
            {
                Sys_Modules module = new Sys_Modules();
                module.ID = SelectedModule.ID;
                module.Name = SelectedModule.Name;
                module.MenuID = SelectedModule.MenuID;
                module.ImagePath = SelectedModule.ImagePath;
                module.Parameter = SelectedModule.Parameter;
                module.Remark = SelectedModule.Remark;
                module.StartUpClass = SelectedModule.StartUpClass;
                module.DllName = SelectedModule.DllName;

                FrmModuleSettingEdit edit = new FrmModuleSettingEdit(module);
                edit.SaveEvent += (m) =>
                {
                    ModuleList[ModuleList.IndexOf(SelectedModule)] = m;
                    SelectedModule = m;
                };
                edit.ShowDialog();
            }
            return true;
        }
        #endregion




    }
}
