using Qyz.DAL.Core;
using Qyz.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qyz.BLL.Core
{
    public class MenuInfoBLL
    {
         
        /// <summary>
        /// 根据系统ID获得菜单集合
        /// </summary>
        /// <param name="systemId"></param>
        /// <returns></returns>
        public List<Sys_Menus> GetMenuInfo(int systemId)
        {
           return new MenuInfoDAL().GetMenuInfo(systemId);
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public bool AddMenuInfo(Sys_Menus menu)
        {
            return new MenuInfoDAL().AddMenuInfo(menu);
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public bool UpdateMenuInfo(Sys_Menus menu)
        {
            return new MenuInfoDAL().UpdateMenuInfo(menu);
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public bool DeleteMenuInfo(Sys_Menus menu)
        {
            return new MenuInfoDAL().DeleteMenuInfo(menu);
        }
    }
}
