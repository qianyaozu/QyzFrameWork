using Qyz.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qyz.DAL.Core
{
    public class SystemInfoDAL
    {
        /// <summary>
        /// 获得系统集合
        /// </summary>
        /// <param name="systemId"></param>
        /// <returns></returns>
        public List<Sys_Systems> GetSystemInfo()
        {
            using (var db = new QYZEntity())
            {
                var query = from p in db.Sys_Systems
                            select p;
                return query.ToList<Sys_Systems>();
            }
        }

        /// <summary>
        /// 新增系统
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public bool AddSystemInfo(Sys_Systems system)
        {
            using (var db = new QYZEntity())
            {
                db.Sys_Systems.AddObject(system);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 更新系统信息
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public bool UpdateSystemInfo(Sys_Systems system)
        {
            using (var db = new QYZEntity())
            {
                Sys_Systems m = db.Sys_Systems.Where(p => p.ID == system.ID).ToList<Sys_Systems>().FirstOrDefault<Sys_Systems>();
                m.Name = system.Name;
                m.Remark = system.Remark;
                db.SaveChanges();
                return true;
            }
        }
        /// <summary>
        /// 删除系统
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public bool DeleteSystemInfo(Sys_Systems system)
        {
            using (var db = new QYZEntity())
            {
                Sys_Systems m = db.Sys_Systems.Where(p => p.ID == system.ID).ToList<Sys_Systems>().FirstOrDefault<Sys_Systems>();

                db.Sys_Systems.DeleteObject(m);
                db.SaveChanges();
                return true;
            }
        }
    }
}
