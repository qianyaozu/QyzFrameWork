using Qyz.Model.Common; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qyz.DAL.Core
{
    public class UserInfoDAL
    {
        QYZEntity db = new QYZEntity();

        #region 用户信息
        /// <summary>
        ///  获取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public Sys_Accounts GetAccountInfo(string userName, string passWord)
        { 
            var query = from ac in db.Sys_Accounts
                        where ac.AccountName == userName && ac.PassWord == passWord
                        select ac;
            if (query != null && query.ToList().Count > 0)
                return query.FirstOrDefault();
            return null;

        }
        public List<Sys_Accounts> GetAccountInfo()
        {
            var query = from ac in db.Sys_Accounts 
                        select ac;
            if (query != null && query.ToList().Count > 0)
                return query.ToList<Sys_Accounts>();
            return null;

        }
        /// <summary>
        /// 新增账户
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool InsertAccount(Sys_Accounts account)
        {
            if (account != null && account.UserName != "" && account.PassWord != "")
            {
                db.Sys_Accounts.AddObject(account);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        /// <summary>
        /// 删除账号信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool DeleteAccount(Sys_Accounts account)
        {
            db.Sys_Accounts.DeleteObject(account);
            db.SaveChanges();
            return true;
        }

        /// <summary>
        /// 获取账户最大ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxAccountID()
        {
            var query = db.Sys_Accounts.Max(p => p.ID);

            if (query != null)
                return Convert.ToInt32(query);
            return 1;
        }
        /// <summary>
        /// 更新账户
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool UpdateAccount(Sys_Accounts account)
        {
            Sys_Accounts acc = ExistsAccount(account);
            if (acc != null)
            {
                acc = account;
                db.SaveChanges();
                return true;
            }
            return false;

        }
        /// <summary>
        /// 是否存在该账户
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Sys_Accounts ExistsAccount(Sys_Accounts account)
        {
            var query = from ac in db.Sys_Accounts
                        where ac.AccountName == account.UserName
                        select ac;
            if (query != null && query.ToList().Count > 0)
                return query.ToList<Sys_Accounts>().FirstOrDefault();
            return null;
        }

        /// <summary>
        /// 根据用户名称获取系统权限
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<Sys_Systems> GetSystemsByAccount(string userName)
        {
            var query = from sys in db.Sys_Systems
                        join rs in db.Sys_Role_System on sys.ID equals rs.SystemID
                        join ac in db.Sys_Accounts on rs.RoleID equals ac.RoleID
                        where ac.AccountName == userName && rs.IsEnable.Value
                        select sys;
            return query == null ? null : new List<Sys_Systems>(query);
        }

        /// <summary>
        /// 根据用户名获得菜单列表（如果是管理员级别则显示系统设置）
        /// </summary>
        /// <param name="accountName"></param>
        /// <returns></returns>
        public List<Sys_Menus> GetMenuByAccount(Sys_Accounts account, int systemId)
        {

            if (account.RoleID < 2)//管理员
            {
                var query = (from menu in db.Sys_Menus
                             where menu.SystemID == systemId
                             select menu).Union(from p in db.Sys_Menus
                                                where p.SystemID == -1
                                                select p);
                return new List<Sys_Menus>(query);
            }
            else//用户
            {
                var query = (from menu in db.Sys_Menus
                             join rm in db.Sys_Role_Menu on menu.ID equals rm.MenuID
                             join ac in db.Sys_Accounts on rm.RoleID equals ac.RoleID
                             where ac.AccountName == account.AccountName && menu.SystemID == systemId && rm.IsEnable.Value
                             select menu);
                return new List<Sys_Menus>(query);
            }
        }
        /// <summary>
        /// 根据用户名和系统获取模块权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Sys_Modules> GetModuleButtonsByAccount(Sys_Accounts account, int systemId)
        {
            if (account.RoleID < 2)//管理员
            {
                var query = (from m in db.Sys_Modules
                             join menu in db.Sys_Menus on m.MenuID equals menu.ID
                             where menu.SystemID == systemId
                             select m).Union(from m in db.Sys_Modules
                                             join menu in db.Sys_Menus on m.MenuID equals menu.ID
                                             where menu.SystemID == -1
                                             select m);
                return new List<Sys_Modules>(query);
            }
            else//普通用户
            {
                var query = (from m in db.Sys_Modules
                             join menu in db.Sys_Menus on m.MenuID equals menu.ID
                             join rm in db.Sys_Role_Module on m.ID equals rm.ModuleID
                             join ac in db.Sys_Accounts on rm.RoleID equals ac.RoleID
                             where menu.SystemID == systemId && ac.AccountName == account.AccountName && rm.IsEnable.Value
                             select m);
                return new List<Sys_Modules>(query);
            }
        }
        #endregion

        #region 角色信息
        /// <summary>
        /// 查询角色信息
        /// </summary>
        /// <returns></returns>
        public List<Sys_Roles> GetRoleInfo()
        {
            var query = from ac in db.Sys_Roles 
                        select ac;
            if (query != null && query.ToList().Count > 0)
                return query.ToList<Sys_Roles>();
            return null;
        }
        /// <summary>
        /// 查询角色信息
        /// </summary>
        /// <returns></returns>
        public Sys_Roles GetRoleInfo(int roleID)
        {
            var query = from ac in db.Sys_Roles
                        where ac.ID == roleID
                        select ac;
            if (query != null && query.ToList().Count > 0)
                return query.ToList()[0];
            return null;
        }
        /// <summary>
        /// 获取角色最大ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxRoleID()
        {
            return db.Sys_Roles.Max(p => p.ID);
        }

        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool DeleteRoleInfo(Sys_Roles role)
        {
            Sys_Roles m = db.Sys_Roles.Where(p => p.ID == role.ID).ToList<Sys_Roles>().FirstOrDefault<Sys_Roles>();
            db.Sys_Roles.DeleteObject(m);
            db.SaveChanges();
            return true;
        }

        /// <summary>
        /// 新增角色信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool AddRoleInfo(Sys_Roles role)
        {
            if (role != null && role.Name != ""  )
            {
                db.Sys_Roles.AddObject(role);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool UpdateRoleInfo(Sys_Roles role)
        {
            Sys_Roles m = db.Sys_Roles.Where(p => p.ID == role.ID).ToList<Sys_Roles>().FirstOrDefault<Sys_Roles>();
            m.Name = role.Name;
            m.Remark = role.Remark;
            db.SaveChanges();
            return true;
        }
        #endregion
    }
}
