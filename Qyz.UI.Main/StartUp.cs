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
        public System.Windows.Controls.UserControl MainControl(string uParameter)
        {
            if (uParameter.Equals("1"))
                return new FrmMenuSetting();
            if (uParameter.Equals("2"))
                return new FrmMenuSetting();

            return null;
        }
    }
}
