using Qyz.FrameWork.Core;
using Qyz.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Qyz.UI.Main
{
    public class MenuNameConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                Sys_Menus menu = GlobalVariable.menuList.First(p => p.ID == System.Convert.ToInt32(value));
                if (menu != null)
                    return menu.Name;
            }
            return "";
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
