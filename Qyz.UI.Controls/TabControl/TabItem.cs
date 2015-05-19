using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System;

namespace Qyz.UI.Controls.TabControl
{
    [TemplatePart(Name = "PART_CloseButton", Type = typeof(ButtonBase))]
    public class TabItem : System.Windows.Controls.TabItem
    {
        static TabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Qyz.UI.Controls.TabControl.TabItem), new FrameworkPropertyMetadata(typeof(Qyz.UI.Controls.TabControl.TabItem)));
        }

        /// <summary>
        /// Provides a place to display an Icon on the Header and on the DropDown Context Menu
        /// </summary>
        public object Icon
        {
            get { return (object)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(object), typeof(TabItem), new UIPropertyMetadata(null));

        /// <summary>
        /// 是否显示关闭按钮
        /// </summary>
        public bool AllowDelete
        {
            get { return (bool)GetValue(AllowDeleteProperty); }
            set { SetValue(AllowDeleteProperty, value); }
        }
        public static readonly DependencyProperty AllowDeleteProperty = DependencyProperty.Register("AllowDelete", typeof(bool), typeof(TabItem), new UIPropertyMetadata(true));

        private bool IsSelected = true;
        public event Action CloseEventHandler;
        /// <summary>
        /// OnApplyTemplate override
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // wire up the CloseButton's Click event if the button exists
            ButtonBase button = this.Template.FindName("PART_CloseButton", this) as ButtonBase;
            if (button != null)
            {
                button.Click += delegate
                {
                    // get the parent tabcontrol 
                    TabControl tc = Helper.FindParentControl<TabControl>(this);
                    if (tc == null) return;
                    if (CloseEventHandler != null)
                    {
                        CloseEventHandler();
                    }
                    this.Content = null;
                     
                    // remove this tabitem from the parent tabcontrol
                    tc.RemoveTabItem(this);
                   
                     
                };
            }
        } 
        
        /// <summary>
        ///     Used by the TabPanel for sizing
        /// </summary>
        internal Dimension Dimension { get; set; }

        /// <summary>
        /// OnMouseEnter, Create and Display a Tooltip
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(System.Windows.Input.MouseEventArgs e)
        {
            base.OnMouseEnter(e);

            this.ToolTip = Helper.CloneElement(Header);
            e.Handled = true;
        }

        protected override void OnSelected(RoutedEventArgs e)
        {
            IsSelected = true;
            base.OnSelected(e);
        }

        protected override void OnUnselected(RoutedEventArgs e)
        {
            IsSelected = false;
            base.OnUnselected(e);
        }
        /// <summary>
        /// OnMouseLeave, remove the tooltip
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(System.Windows.Input.MouseEventArgs e)
        {
            base.OnMouseLeave(e);

            this.ToolTip = null;
            e.Handled = true;
        }
    }
}
