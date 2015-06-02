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
        /// <summary>
        /// 根据账号和密码进行登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public Sys_Accounts GetUserInfo(string userName, string passWord)
        {
            return dal.GetUserInfo(userName, passWord);

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

        public bool InsertUser(Sys_Accounts account)
        {
            return dal.InsertUser(account);
        }

        public bool UpdateUser(Sys_Accounts account)
        {
            return dal.UpdateUser(account);

        }
        public Sys_Accounts ExistsUser(Sys_Accounts account)
        {
            return dal.ExistsUser(account);
        }
    }
}
