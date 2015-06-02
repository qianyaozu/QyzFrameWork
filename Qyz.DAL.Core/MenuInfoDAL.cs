using Qyz.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qyz.DAL.Core
{
    /// <summary>
    /// 菜单操作类
    /// </summary>
   public class MenuInfoDAL
    {
       
       /// <summary>
       /// 根据系统ID获得菜单集合
       /// </summary>
       /// <param name="systemId"></param>
       /// <returns></returns>
       public List<Sys_Menus> GetMenuInfo(int systemId)
       {
           using (var db = new QYZEntity())
           {
               var query = from p in db.Sys_Menus
                           where p.SystemID == systemId
                           select p;
               return query.ToList<Sys_Menus>();
           }
       }

       /// <summary>
       /// 新增菜单
       /// </summary>
       /// <param name="menu"></param>
       /// <returns></returns>
        public bool AddMenuInfo(Sys_Menus menu)
        {
            using (var db = new QYZEntity())
            {
                db.Sys_Menus.AddObject(menu); 
                db.SaveChanges();
                return true;
            }
        }

       /// <summary>
       /// 更新菜单
       /// </summary>
       /// <param name="menu"></param>
       /// <returns></returns>
        public bool UpdateMenuInfo(Sys_Menus menu)
        {
            using (var db = new QYZEntity())
            { 
                Sys_Menus m = db.Sys_Menus.Where(p => p.SystemID == menu.SystemID && p.ID == menu.ID).ToList<Sys_Menus>().FirstOrDefault<Sys_Menus>();
                m.Name=menu.Name;
                m.ImagePath = menu.ImagePath;
                m.Remark = menu.Remark; 
                db.SaveChanges();
                return true; 
            }
        }
       /// <summary>
       /// 删除菜单
       /// </summary>
       /// <param name="menu"></param>
       /// <returns></returns>
        public bool DeleteMenuInfo(Sys_Menus menu)
        {
            using (var db = new QYZEntity())
            {
                Sys_Menus m = db.Sys_Menus.Where(p => p.SystemID == menu.SystemID && p.ID == menu.ID).ToList<Sys_Menus>().FirstOrDefault<Sys_Menus>();

                db.Sys_Menus.DeleteObject(m);
                db.SaveChanges();
                return true;
            }
        }
    }
}
