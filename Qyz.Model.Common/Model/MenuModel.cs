using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qyz.Model.Common.Model
{
    /// <summary>
    /// 菜单详情信息
    /// </summary>
   public class MenuModel
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public int SystemID { get; set; }
        public string SystemName { get; set; }

        public int SystemEnable { get; set; }
        public int MenuID { get; set; }
        public string MenuName { get; set; }

        public int MenuEnable { get; set; }
        public string MenuImage { get; set; }
        public int ModuleID { get; set; }
        public string ModuleName { get; set; }

        public int ModuleEnable { get; set; }
        public string DllName { get; set; }
        public string StartUpClass { get; set; }
        public string Parameter { get; set; }
        public string ICO { get; set; }
        
    }

}
