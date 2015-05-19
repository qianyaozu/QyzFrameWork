using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows; 

namespace Qyz.UI.Controls
{
    public class MessageBoxEx
    {
        // Methods
        public static bool? Show(string text)
        {
            return ShowWindow(text, "", MessageBoxButtonType.OK);
        }

        public static bool? Show(string text, string caption)
        {
            return ShowWindow(text, caption, MessageBoxButtonType.OK);
        }
        public static bool? Show(string text, string caption, MessageBoxButtonType buttons)
        {
            return ShowWindow(text, caption, buttons);
        }
        private static bool? ShowWindow(string text, string caption, MessageBoxButtonType buttons)
        {
            MessageWindow window = new MessageWindow(text, caption, buttons);
            return window.DrawWindows();
        }
    }


    public enum MessageBoxButtonType
    {
        OK,
        OKCancel
    }
}
