using Qyz.DAL.Core;
using Qyz.Model.Common; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qyz.BLL.Core
{
    public class UserInfoBLL
    {
        UserInfoDAL dal = new UserInfoDAL();
        #region 用户信息
        /// <summary>
        /// 根据账号和密码进行登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public Sys_Accounts GetAccountInfo(string userName, string passWord)
        {
            return dal.GetAccountInfo(userName, passWord); 
        }
         /// <summary>
        /// 根据账号和密码进行登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public List<Sys_Accounts> GetAccountInfo()
        {
            return dal.GetAccountInfo();
        }
        /// <summary>
        /// 根据账号获取系统权限
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<Sys_Systems> GetSystemsByAccount(string userName)
        {
            return dal.GetSystemsByAccount(userName);
        }
        /// <summary>
        /// 根据账号获取菜单信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<Sys_Menus> GetMenuByAccount(Sys_Accounts account, int systemId)
        {
            return dal.GetMenuByAccount(account, systemId);
        }
        /// <summary>
        /// 根据账号和系统类别获取模块及权限
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="systemId"></param>
        /// <returns></returns>
        public List<Sys_Modules> GetModuleButtonsByAccount(Sys_Accounts account, int systemId)
        {
            return dal.GetModuleButtonsByAccount(account, systemId);
        }

        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool InsertAccount(Sys_Accounts account)
        {
            return dal.InsertAccount(account);
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool UpdateAccount(Sys_Accounts account)
        {
            return dal.UpdateAccount(account);

        }
        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Sys_Accounts ExistsAccount(Sys_Accounts account)
        {
            return dal.ExistsAccount(account);
        }

         /// <summary>
        /// 删除账号信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool DeleteAccount(Sys_Accounts account)
        {
            return dal.DeleteAccount(account);
        }

        /// <summary>
        /// 获取账户最大ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxAccountID()
        {
            return dal.GetMaxAccountID();
        }
        #endregion 



        #region 角色信息
        /// <summary>
        /// 查询角色信息
        /// </summary>
        /// <returns></returns>
        public List<Sys_Roles> GetRoleInfo()
        {
            return dal.GetRoleInfo();
        }

        public Sys_Roles GetRoleInfo(int roleID)
        {
            return dal.GetRoleInfo(roleID);
        }
         /// <summary>
        /// 获取角色最大ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxRoleID()
        {
            return dal.GetMaxRoleID();
        }
        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool DeleteRoleInfo(Sys_Roles role)
        {
            return dal.DeleteRoleInfo(role);
        } /// <summary>
        /// 新增角色信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool AddRoleInfo(Sys_Roles role)
        {
            return dal.AddRoleInfo(role);
        }
        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool UpdateRoleInfo(Sys_Roles role)
        {
            return dal.UpdateRoleInfo(role);
        }
        #endregion
    }
}
