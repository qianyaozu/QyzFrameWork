using Qyz.FrameWork.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qyz.DataObject.Core
{
    public class ModuleModel:PropertyChangedBase
    {
        #region 字段
        private int _id;
        private string _name;
        private int _systemID;
        private string _dllName;
        private string _fullClassName;
        private string _parameter;
        private string _ico;
        private string _icoSize;
        private string _remark;
        #endregion

        #region 属性
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; RaisePropertyChanged("Remark"); }
        }
        public string ICOSize
        {
            get { return _icoSize; }
            set { _icoSize = value; RaisePropertyChanged("ICOSize"); }
        }
        public string ICO
        {
            get { return _ico; }
            set { _ico = value; RaisePropertyChanged("ICO"); }
        }

        public string Parameter
        {
            get { return _parameter; }
            set { _parameter = value; RaisePropertyChanged("Parameter"); }
        }
        public string FullClassName
        {
            get { return _fullClassName; }
            set { _fullClassName = value; RaisePropertyChanged("FullClassName"); }
        }
        public string DllName
        {
            get { return _dllName; }
            set { _dllName = value; RaisePropertyChanged("DllName"); }
        }


        public int SystemID
        {
            get { return _systemID; }
            set { _systemID = value; RaisePropertyChanged("SystemID"); }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("Name"); }
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged("ID"); }
        }
        #endregion
    }

}
