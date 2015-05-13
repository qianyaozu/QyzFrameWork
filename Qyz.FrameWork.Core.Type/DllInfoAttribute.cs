using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qyz.FrameWork.Type
{
    public class DllInfoAttribute : Attribute
    {
        /// <summary>
        /// dll名称
        /// </summary>
        public string DllName { get; set; }

        /// <summary>
        /// 类全名
        /// </summary>
        public string FullClassName { get; set; }
    }

}
