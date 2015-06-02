using Qyz.FrameWork.Core.Type;
using Qyz.Model.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Qyz.FrameWork.Core
{
    public class GlobalVariable
    {
        //用户信息
        public static Sys_Accounts myAccount = null;
        public static SystemType systemType;
        //用户对应的菜单信息,模块信息
        public static List<Sys_Modules> modulelList = new List<Sys_Modules>();
        public static ObservableCollection<Sys_Menus> menuList = new ObservableCollection<Sys_Menus>();
    }
}
