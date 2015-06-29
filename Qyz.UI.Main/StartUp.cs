using Qyz.UI.Base;
using Qyz.UI.Main.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qyz.UI.Main
{
    public class StartUp:IStartUp
    {
        public System.Windows.Controls.UserControl MainControl(string para,string menuID)
        {
            if (para.Equals("1"))
                return new FrmSystemSetting();//系统设置
            if (para.Equals("2"))
                return new FrmMenuSetting();//菜单设置
            if (para.Equals("3"))
                return new FrmModuleSetting(menuID);//模块设置 
            return null;
        }
    }
}
