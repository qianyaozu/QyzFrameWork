using Qyz.DAL.Core;
using Qyz.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qyz.BLL.Core
{
    public class ModuleInfoBLL
    {
        ModuleInfoDAL dal = new ModuleInfoDAL();
        /// <summary>
        /// 获取模块ID最大值
        /// </summary>
        /// <returns></returns>
        public int GetMaxModuleID()
        {
            return dal.GetMaxModuleID();
        }

         /// <summary>
        /// 删除模块信息
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public bool DeleteModuleInfo(Sys_Modules module)
        {
            return dal.DeleteModuleInfo(module);
        }

        /// <summary>
        /// 新增模块信息
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public bool InsertModuleInfo(Sys_Modules module)
        {
            return dal.InsertModuleInfo(module);
        }

        /// <summary>
        /// 更新模块信息
        /// </summary>
        /// <param name="modules"></param>
        /// <returns></returns>
        public bool UpdateModuleInfo(Sys_Modules modules)
        {
            return dal.UpdateModuleInfo(modules);
        }


        /// <summary>
        /// 根据菜单获取模块信息
        /// </summary>
        /// <param name="MenuID"></param>
        /// <returns></returns>
        public List<Sys_Modules> GetModuleInfoList(int MenuID)
        {
            return dal.GetModuleInfoList(MenuID);
        }
    }
}
