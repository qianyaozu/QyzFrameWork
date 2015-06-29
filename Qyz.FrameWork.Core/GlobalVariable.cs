using Qyz.FrameWork.Core.Type;
using Qyz.Model.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Qyz.FrameWork.Core
{
    public class GlobalVariable
    {
        //用户信息
        public static Sys_Accounts myAccount = null;
        public static SystemType systemType = SystemType.Assets;
        //用户对应的菜单信息,模块信息,  配置界面中不可用，因为该信息不是所有配置信息，只是该用户权限下的信息
        public static List<Sys_Modules> modulelList = new List<Sys_Modules>();
        public static List<Sys_Menus> menuList = new List<Sys_Menus>();

        #region 模块方法
        /// <summary>
        /// 注册模块更新方法
        /// </summary>
        private static Func<List<Sys_Modules>> funcModule = null;
        /// <summary>
        /// 刷新该用户的全局模块信息
        /// </summary>
        /// <param name="_func"></param>
        public static void RefleshModuleInfo(Func<List<Sys_Modules>> _func)
        {
            funcModule = _func;
            RefleshModuleInfo();
        }
        /// <summary>
        /// 刷新该用户的全局模块信息
        /// </summary>
        /// <param name="_func"></param>
        public static void RefleshModuleInfo()
        {
            if (funcModule!=null)
                modulelList = funcModule.Invoke();
        }
        #endregion



        #region 菜单方法
        /// <summary>
        /// 注册菜单更新方法
        /// </summary>
        private static Func<List<Sys_Menus>> funcMenu = null;
        /// <summary>
        /// 刷新该用户的全局菜单信息
        /// </summary>
        /// <param name="_func"></param>
        public static void RefleshMenuInfo(Func<List<Sys_Menus>> _func)
        {
            funcMenu = _func;
            RefleshMenuInfo();
        }
        /// <summary>
        /// 刷新该用户的全局菜单信息
        /// </summary>
        /// <param name="_func"></param>
        public static void RefleshMenuInfo()
        { 
            if (funcMenu != null)
                menuList = funcMenu.Invoke();
        }
        #endregion
    }
}
