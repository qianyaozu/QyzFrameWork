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
    public class SystemSettingViewModel : ViewModelBase
    {
         #region 构造函数
        public SystemSettingViewModel()
        { 
            InitButtonState();
            SystemList = new ObservableCollection<Sys_Systems>(new SystemInfoBLL().GetSystemInfo());
        }
        #endregion

        #region 绑定的属性
        private ObservableCollection<Sys_Systems> _SystemList;
        /// <summary>
        /// 系统列表
        /// </summary>
        public ObservableCollection<Sys_Systems> SystemList
        {
            get { return _SystemList; }
            set { _SystemList = value; }
        }

        private Sys_Systems _SelectedSystem;
        /// <summary>
        /// 选中的系统
        /// </summary>
        public Sys_Systems SelectedSystem
        {
            get { return _SelectedSystem; }
            set { _SelectedSystem = value; RaisePropertyChanged("SelectedSystem"); }
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
        public override bool ExecuteAdd()
        {
            Sys_Systems system = new Sys_Systems();
            system.ID = SystemList.Max(p => p.ID) + 1; 
            FrmSystemSettingEdit edit = new FrmSystemSettingEdit(system);
            edit.SaveEvent += (sys) =>
            {
                SystemList.Add(sys);
            };
            edit.ShowDialog(); 
            return true;
        }
        /// <summary>
        /// 修改命令
        /// </summary>
        /// <returns></returns>
        public override bool ExecuteEdit()
        {
            if (SelectedSystem != null)
            {
                Sys_Systems sys = new Sys_Systems();
                sys.ID = SelectedSystem.ID;
                sys.Name = SelectedSystem.Name; 
                sys.Remark = SelectedSystem.Remark;


                FrmSystemSettingEdit edit = new FrmSystemSettingEdit(sys);
                edit.SaveEvent += (s) =>
                {
                    SystemList[SystemList.IndexOf(SelectedSystem)] = s;
                    SelectedSystem = s;
                };
                edit.ShowDialog();
            }
            return true;
        }
        /// <summary>
        /// 删除命令
        /// </summary>
        /// <returns></returns>
        public override bool ExecuteDelete()
        {
            if (SelectedSystem != null)
            { 
                SystemInfoBLL bll = new SystemInfoBLL();
                if (bll.DeleteSystemInfo(SelectedSystem))
                {
                    SystemList.Remove(SelectedSystem);
                    SelectedSystem = null;
                } 
            }
            return true;
        }
        #endregion
    }
}
