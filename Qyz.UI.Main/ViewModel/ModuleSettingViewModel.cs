using Qyz.UI.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qyz.UI.Main.ViewModel
{
    public class ModuleSettingViewModel : ViewModelBase
    {
        public ModuleSettingViewModel()
        {
            AllowAdd = true;
        }
        public override bool ExecuteAdd()
        {
            return base.ExecuteAdd();
        }

    }
}
