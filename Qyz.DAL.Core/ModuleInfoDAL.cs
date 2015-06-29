using Qyz.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qyz.DAL.Core
{
    public class ModuleInfoDAL
    {
        /// <summary>
        /// 根据菜单查询模块信息
        /// </summary>
        /// <param name="MenuID"></param>
        /// <returns></returns>
        public List<Sys_Modules> GetModuleInfoList(int MenuID)
        {
            using (var db = new QYZEntity())
            {
                var data = from mo in db.Sys_Modules
                           where mo.MenuID == MenuID
                           select mo;
                if (data != null)
                    return new List<Sys_Modules>(data);
                return new List<Sys_Modules>();
            }
        }

        /// <summary>
        /// 获取模块ID最大值
        /// </summary>
        /// <returns></returns>
        public int GetMaxModuleID()
        {
            using (var db = new QYZEntity())
            {
                int data = db.Sys_Modules.Max(p => p.ID);
                return data + 1;
            }
        }


        /// <summary>
        /// 删除模块信息
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public bool DeleteModuleInfo(Sys_Modules module)
        {
            using (var db = new QYZEntity())
            {
                Sys_Modules m = db.Sys_Modules.Where(p => p.ID == module.ID).ToList<Sys_Modules>().FirstOrDefault<Sys_Modules>();
                db.Sys_Modules.DeleteObject(m);
                db.SaveChanges();
                return true;
            }
        }


        /// <summary>
        /// 新增模块信息
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public bool InsertModuleInfo(Sys_Modules module)
        {
            using (var db = new QYZEntity())
            {
                db.Sys_Modules.AddObject(module);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 更新模块信息
        /// </summary>
        /// <param name="modules"></param>
        /// <returns></returns>
        public bool UpdateModuleInfo(Sys_Modules modules)
        {
            using (var db = new QYZEntity())
            {
                Sys_Modules module = db.Sys_Modules.Where(p => p.ID == modules.ID).ToList<Sys_Modules>().FirstOrDefault<Sys_Modules>();
                module.Name = modules.Name;
                module.MenuID = modules.MenuID;
                module.ImagePath = modules.ImagePath;
                module.Parameter = modules.Parameter;
                module.Remark = modules.Remark;
                module.StartUpClass = modules.StartUpClass;
                module.DllName = modules.DllName;
                db.SaveChanges();
                return true;
            }
        }
    }
}
