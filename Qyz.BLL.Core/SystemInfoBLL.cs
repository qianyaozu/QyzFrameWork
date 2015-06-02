using Qyz.DAL.Core;
using Qyz.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qyz.BLL.Core
{
    public class SystemInfoBLL
    {
        
        /// <summary>
        /// 获得系统集合
        /// </summary>
        /// <param name="systemId"></param>
        /// <returns></returns>
        public List<Sys_Systems> GetSystemInfo()
        {
            return new SystemInfoDAL().GetSystemInfo();
        }

        /// <summary>
        /// 新增系统
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public bool AddSystemInfo(Sys_Systems system)
        {
            return new SystemInfoDAL().AddSystemInfo(system);
        }

        /// <summary>
        /// 更新系统信息
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public bool UpdateSystemInfo(Sys_Systems system)
        {
            return new SystemInfoDAL().UpdateSystemInfo(system);
        }
        /// <summary>
        /// 删除系统
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public bool DeleteSystemInfo(Sys_Systems system)
        {
            return new SystemInfoDAL().UpdateSystemInfo(system);
        }
    }
}
