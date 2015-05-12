using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qyz.FrameWork.Type
{
    public class SysTypeAttribute : Attribute
    {
        private string _name;
        public SysTypeAttribute(string name)
        {
            _name = name;
        }
    }
}
