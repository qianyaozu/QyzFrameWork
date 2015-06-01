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
    /// FrmModuleSetting.xaml 的交互逻辑
    /// </summary>
    public partial class FrmModuleSetting : MdiControl
    {
        ModuleSettingViewModel viewModel = new ModuleSettingViewModel();
        public FrmModuleSetting()
        {
            InitializeComponent();
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
