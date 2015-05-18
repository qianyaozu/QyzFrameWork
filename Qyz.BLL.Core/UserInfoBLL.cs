using Qyz.DAL.Core;
using Qyz.Model.Common;
using Qyz.Model.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qyz.BLL.Core
{
    public class UserInfoBLL
    {
        UserInfoDAL dal = new UserInfoDAL();
        public Account GetUserInfo(string userName, string passWord)
        {
            return dal.GetUserInfo(userName, passWord);

        }

        public List<Systems> GetSystemsByAccount(string userName)
        {
            return dal.GetSystemsByAccount(userName);
        }
        public List<MenuModel> GetMenuModelByAccount(int userId)
        {
            return dal.GetMenuModelByAccount(userId);
        }


        public bool InsertUser(Account account)
        {
            return dal.InsertUser(account);
        }

        public bool UpdateUser(Account account)
        {
            return dal.UpdateUser(account);

        }
        public Account ExistsUser(Account account)
        {
            return dal.ExistsUser(account);
        }
    }
}
