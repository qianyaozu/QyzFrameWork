using Qyz.FrameWork.Type;
using Qyz.UI.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace Qyz.UI.Base
{
    public class MdiControl : UserControl
    {
        /// <summary>
        /// 指定该界面的ViewModel
        /// </summary>
        protected virtual ViewModelBase VM { get; set; }
        public MdiControl()
        {
            object ob = this.FindResource("mdiControlTemplate");
            this.Template = ob as ControlTemplate;
            if (this.Template == null) return;

            this.Loaded += MdiControl_Loaded;
        }

        void MdiControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DataContext = this.VM;
            //查找 本页面对应人员的权限，然后注册按钮
        }


        private void RegisterButton(string buttonName,SystemButton sysButton,bool visible)
        {
            object obj = this.Template.FindName(buttonName, this);
            if (obj != null)
            {
                Button bt = obj as Button;
                if (!visible)
                {
                    bt.Visibility = System.Windows.Visibility.Collapsed;
                    return;
                } 
                bt.CommandParameter = sysButton;
                bt.SetBinding(Button.CommandProperty, new Binding("ActionCommand") { Source = this.DataContext });
                bt.SetBinding(Button.ContentProperty, new Binding("Content" + sysButton.ToString()) { Source = this.DataContext });
                bt.SetBinding(Button.IsEnabledProperty, new Binding("Allow" + sysButton.ToString()) { Source = this.DataContext });
            }

        }
    }
}
