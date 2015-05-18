using Qyz.FrameWork.Type;
using Qyz.UI.MVVM.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Qyz.UI.MVVM
{
    public class ViewModelBase : PropertyChangedBase
    {
        #region 是否可用

        #endregion
        #region 具体命令
        #endregion
        #region 执行命令 
        private ICommand _actionCommand;
        public ICommand ActionCommand
        {
            get
            {
                if (_actionCommand != null)
                {
                    _actionCommand = new RelayCommand(
                        (para) => CanActionExecute(para),
                        (para) => ActionExecute(para) 
                        );
                }

                return _actionCommand;
            }
            set { _actionCommand = value; }
        }


        protected bool CanActionExecute(object para)
        {
            return true;
        }
        protected bool ActionExecute(object para)
        {
            return true;
        }
        #endregion
    }
}
