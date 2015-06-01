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
            LoadingFrameWork();


        }
        /// <summary>
        /// 根据用户权限加载界面按钮
        /// 重写则手动注册界面按钮
        /// </summary>
        public virtual void LoadingFrameWork()
        {
            //查找 本页面对应人员的权限，然后注册按钮
            RegisterButton(SystemButton.Add, true);
        }
      
        /// <summary>
        /// 手动注册按钮
        /// </summary>
        /// <param name="sysButton"></param>
        public void RegisterButton(SystemButton sysButton)
        {
            RegisterButton(sysButton, true);
        }
        /// <summary>
        /// 注册每个按钮
        /// </summary>
        /// <param name="buttonName"></param>
        /// <param name="sysButton"></param>
        /// <param name="visible"></param>
        private void RegisterButton(SystemButton sysButton, bool visible)
        {
            if (!visible)
                return;
            object obj = this.Template.FindName(sysButton.ToString() + "Button", this);
            if (obj != null)
            {
                Button bt = obj as Button; 
                bt.Visibility = System.Windows.Visibility.Visible; 
                bt.CommandParameter = sysButton;
                bt.SetBinding(Button.CommandProperty, new Binding("ActionCommand") { Source = this.DataContext });
                bt.SetBinding(Button.ContentProperty, new Binding("Content" + sysButton.ToString()) { Source = this.DataContext });
                bt.SetBinding(Button.IsEnabledProperty, new Binding("Allow" + sysButton.ToString()) { Source = this.DataContext });
            }
        }
    }
}
