using Qyz.FrameWork.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qyz.DataObject.Core
{
    public class SystemModule:PropertyChangedBase
    {
        #region 字段
        private int _systemID;
        private string _systemName; 
        private int _menuID;
        private string _menuName;
        private int _moduleID;
        private string _moduleName;
        private int _enableSystem;
        private int _enableMenu;
        private int _enableModule;
        #endregion

        #region 属性
        /// <summary>
        /// 是否启用该模块
        /// </summary>
        public int EnableModule
        {
            get { return _enableModule; }
            set { _enableModule = value; RaisePropertyChanged("EnableModule"); }
        }
        /// <summary>
        /// 是否启用该菜单
        /// </summary>
        public int EnableMenu
        {
            get { return _enableMenu; }
            set { _enableMenu = value; RaisePropertyChanged("EnableMenu"); }
        }

        /// <summary>
        /// 是否启用该系统
        /// </summary>
        public int EnableSystem
        {
            get { return _enableSystem; }
            set { _enableSystem = value; RaisePropertyChanged("EnableSystem"); }
        }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName
        {
            get { return _moduleName; }
            set { _moduleName = value; RaisePropertyChanged("ModuleName"); }
        }
        /// <summary>
        /// 模块编码
        /// </summary>
        public int ModuleID
        {
            get { return _moduleID; }
            set { _moduleID = value; RaisePropertyChanged("ModuleID"); }
        }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName
        {
            get { return _menuName; }
            set { _menuName = value; RaisePropertyChanged("MenuName"); }
        }
        /// <summary>
        /// 菜单编码
        /// </summary>
        public int MenuID
        {
            get { return _menuID; }
            set { _menuID = value; RaisePropertyChanged("MenuID"); }
        }
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName
        {
            get { return _systemName; }
            set { _systemName = value; RaisePropertyChanged("SystemName"); }
        }
        /// <summary>
        /// 系统编码
        /// </summary>
        public int SystemID
        {
            get { return _systemID; }
            set { _systemID = value; RaisePropertyChanged("SystemID"); }
        }
        #endregion
    }
}
