using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qyz.UI.Base
{
    public interface IStartUp
    {
        void Run(string FormCaption, string uParameter);

        public virtual System.Windows.Controls.UserControl MainControl;
       
    }
}
