using Qyz.FrameWork.Core.Type;
using Qyz.Model.Common;
using Qyz.Model.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qyz.FrameWork.Core
{
    public class GlobalVariable
    {
        //用户信息
        public static Account myAccount = null;
        public static SystemType systemType;
        //用户对应的菜单信息->每个功能的按钮信息
        public static List<MenuModel> menuModelList = new List<MenuModel>();
    }
}
