using Qyz.FrameWork.Type;
using Qyz.UI.Base;
using Qyz.UI.Main.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Qyz.UI.Main.View
{
    /// <summary>
    /// FrmRoleSetting.xaml 的交互逻辑
    /// </summary>
    public partial class FrmRoleSetting : MdiControl
    {
        RoleSettingViewModel viewModel = new RoleSettingViewModel();
        public FrmRoleSetting()
        {
            InitializeComponent();
        }

        public override void LoadingFrameWork()
        {
            RegisterButton(SystemButton.Add);
            RegisterButton(SystemButton.Edit);
            RegisterButton(SystemButton.Delete);
        }
        protected override MVVM.ViewModelBase VM
        {
            get
            {
                return viewModel;
            } 
        }
    }
}
