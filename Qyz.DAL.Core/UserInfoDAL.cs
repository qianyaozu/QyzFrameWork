using Qyz.Model.Common;
using Qyz.Model.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qyz.DAL.Core
{
    public class UserInfoDAL
    {
        QYZ_CommonEntities db = new QYZ_CommonEntities();
        /// <summary>
        ///  获取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public Account GetUserInfo(string userName, string passWord)
        {
            var query = db.Account.Where(p => p.AccountName == userName && p.PassWord == passWord).FirstOrDefault();
            return query ?? (query as Account);

        }

        public bool InsertUser(Account account)
        {

            if (ExistsUser(account) != null)
            {
                db.Account.Add(account);
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool UpdateUser(Account account)
        {
            Account acc = ExistsUser(account);
            if (acc != null)
            {
                acc = account;
                db.SaveChanges();
                return true;
            }
            return false;

        }
        public Account ExistsUser(Account account)
        {
            return db.Account.First(p => p.AccountName == account.AccountName || p.ID == account.ID);
        }


        public List<Systems> GetSystemsByAccount(string userName)
        {


            var query = from p in db.Account
                        join p2 in db.Role_System on p.RoleID equals p2.RoleID
                        join p3 in db.Systems on p2.SystemID equals p3.ID
                        where p.AccountName == userName  
                        select p3;
            if(query!=null)
            {
                List<Systems> systemList = new List<Systems>(query);
                return systemList;
            }
            return null;
        }

        /// <summary>
        /// 根据用户名返回菜单列表
        /// </summary>
        /// <param name="accountName"></param>
        /// <returns></returns>
        public List<MenuModel> GetMenuModelByAccount(int userId)
        {
            //获取所有菜单和模块信息
            var query = from p1 in db.Systems
                        join p2 in db.System_Menu on p1.ID equals p2.SystemID
                        join p3 in db.Menu on p2.MenuID equals p3.ID
                        join p4 in db.Menu_Module on p3.ID equals p4.MenuID
                        join p5 in db.Module on p4.ModuleID equals p5.ID
                        select new MenuModel
                        {
                            UserID = userId,
                            SystemID = p1.ID,
                            SystemName = p1.Name,
                            SystemEnable = 0,
                            MenuID = p3.ID,
                            MenuName = p3.Name,
                            MenuEnable = 0,
                            MenuImage = p3.ImagePath,
                            ModuleID = p5.ID,
                            ModuleName = p5.Name,
                            ModuleEnable = 0,
                            DllName = p5.DllName,
                            StartUpClass = p5.StartUpClass,
                            Parameter = p5.Parameter,
                            ICO = p5.ICO
                        };
            List<MenuModel> list = new List<MenuModel>(query);
            int roleId = db.Account.FirstOrDefault(p => p.ID == userId).RoleID;
            //判断系统是否启用
            db.Role_System.Where(p => p.RoleID == roleId && p.IsEnable.Value).ToList().ForEach(mo =>
                {
                    list.FindAll(i => i.SystemID == mo.SystemID).ForEach(ii => ii.SystemEnable = 1);
                });
            //判断菜单是否启用
            db.Role_Menu.Where(p => p.RoleID == roleId && p.IsEnable.Value).ToList().ForEach(mo =>
                {
                    list.FindAll(i => i.MenuID == mo.MenuID).ForEach(ii => ii.MenuEnable = 1);
                });
            //判断模块是否启用
            db.Role_Module.Where(p => p.RoleID == roleId && p.IsEnable.Value).ToList().ForEach(mo =>
                {
                    list.FindAll(i => i.MenuID == mo.ModuleID).ForEach(ii => ii.ModuleEnable = 1);
                });
            return list;
                

        }
    }
}
