using Qyz.FrameWork.Type;
using Qyz.UI.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qyz.UI.Main.Model
{
    /// <summary>
    /// 页签信息实体类
    /// </summary>
    public class TabItemInfo : PropertyChangedBase
    {
        private int _id;
        private string _name;
        private string _discribtion;
        
        private MdiControl _contentControl;

        public MdiControl ContentControl
        {
            get { return _contentControl; }
            set { _contentControl = value; RaisePropertiesChanged("ContentControl"); }
        }

        

        public string Discribtion
        {
            get { return _discribtion; }
            set { _discribtion = value; RaisePropertiesChanged("Discribtion"); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertiesChanged("Name"); }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; RaisePropertiesChanged("Id"); }
        }
    }
}
