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
    /// FrmMenuSetting.xaml 的交互逻辑
    /// </summary>
    public partial class FrmMenuSetting : MdiControl
    {
        MenuSettingViewModel viewModel = new MenuSettingViewModel();
        public FrmMenuSetting()
        {
            InitializeComponent(); 
        }
        public override void LoadingFrameWork()
        {
            RegisterButton(SystemButton.Add);
            RegisterButton(SystemButton.Delete);
            RegisterButton(SystemButton.Edit);
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
