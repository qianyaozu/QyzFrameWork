using Qyz.FrameWork.Core;
using Qyz.FrameWork.Type;
using Qyz.UI.MVVM.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Qyz.UI.MVVM
{
    public class ViewModelBase : PropertyChangedBase
    {


        #region 执行命令
        private ICommand _actionCommand;
        public ICommand ActionCommand
        {
            get
            {
                if (_actionCommand == null)
                {
                    _actionCommand = new RelayCommand(
                         (para) => ActionExecute(para),
                        (para) => CanActionExecute(para)
                        );
                }
                return _actionCommand;
            }
            set { _actionCommand = value; }
        }

        /// <summary>
        /// 是否执行命令
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        protected bool CanActionExecute(object para)
        {
            return true;
        }
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        protected void ActionExecute(object para)
        {
            try
            {
                //根据方法名 反射调用按钮命令
                ReflectFunction.RunReflectMethod(this, "Execute" + para.ToString(), false);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.GetBaseException().Message);
            }
        }
        #endregion


        #region 按钮是否可用
        private bool _AllowAdd = false;
        private bool _AllowEdit = false;
        private bool _AllowDelete = false;
        private bool _AllowSave = false;
        private bool _AllowQuery = false;
        private bool _AllowReflesh = false;
        private bool _AllowView = false;
        private bool _AllowAudit = false;
        private bool _AllowUnAudit = false;
        private bool _AllowPrint = false;
        private bool _AllowExport = false;
        private bool _AllowCustomA = false;
        private bool _AllowCustomB = false;
        private bool _AllowCustomC = false;
        private bool _AllowCustomD = false;
        private bool _AllowCustomE = false;
        private bool _AllowCustomF = false;
        private bool _AllowCustomG = false;
        /// <summary>
        /// 允许自定义G
        /// </summary>
        public bool AllowCustomG
        {
            get { return _AllowCustomG; }
            set { _AllowCustomG = value; RaisePropertyChanged("AllowCustomG"); }
        }
        /// <summary>
        /// 允许自定义F
        /// </summary>
        public bool AllowCustomF
        {
            get { return _AllowCustomF; }
            set { _AllowCustomF = value; RaisePropertyChanged("AllowCustomF"); }
        }
        /// <summary>
        /// 允许自定义E
        /// </summary>
        public bool AllowCustomE
        {
            get { return _AllowCustomE; }
            set { _AllowCustomE = value; RaisePropertyChanged("AllowCustomE"); }
        }
        /// <summary>
        /// 允许自定义D
        /// </summary>
        public bool AllowCustomD
        {
            get { return _AllowCustomD; }
            set { _AllowCustomD = value; RaisePropertyChanged("AllowCustomD"); }
        }
        /// <summary>
        /// 允许自定义C
        /// </summary>
        public bool AllowCustomC
        {
            get { return _AllowCustomC; }
            set { _AllowCustomC = value; RaisePropertyChanged("AllowCustomC"); }
        }
        /// <summary>
        /// 允许自定义B
        /// </summary>
        public bool AllowCustomB
        {
            get { return _AllowCustomB; }
            set { _AllowCustomB = value; RaisePropertyChanged("AllowCustomB"); }
        }
        /// <summary>
        /// 允许自定义A
        /// </summary>
        public bool AllowCustomA
        {
            get { return _AllowCustomA; }
            set { _AllowCustomA = value; RaisePropertyChanged("AllowCustomA"); }
        }
        /// <summary>
        /// 允许导出
        /// </summary>
        public bool AllowExport
        {
            get { return _AllowExport; }
            set { _AllowExport = value; RaisePropertyChanged("AllowExport"); }
        }
        /// <summary>
        /// 允许打印
        /// </summary>
        public bool AllowPrint
        {
            get { return _AllowPrint; }
            set { _AllowPrint = value; RaisePropertyChanged("AllowPrint"); }
        }


        /// <summary>
        /// 允许弃审
        /// </summary>
        public bool AllowUnAudit
        {
            get { return _AllowUnAudit; }
            set { _AllowUnAudit = value; RaisePropertyChanged("AllowUnAudit"); }
        }
        /// <summary>
        /// 允许审核
        /// </summary>
        public bool AllowAudit
        {
            get { return _AllowAudit; }
            set { _AllowAudit = value; RaisePropertyChanged("AllowAudit"); }
        }

        /// <summary>
        /// 允许视图
        /// </summary>
        public bool AllowView
        {
            get { return _AllowView; }
            set { _AllowView = value; RaisePropertyChanged("AllowView"); }
        }

        /// <summary>
        /// 允许刷新
        /// </summary>
        public bool AllowReflesh
        {
            get { return _AllowReflesh; }
            set { _AllowReflesh = value; RaisePropertyChanged("AllowReflesh"); }
        }
        /// <summary>
        /// 允许查询
        /// </summary>
        public bool AllowQuery
        {
            get { return _AllowQuery; }
            set { _AllowQuery = value; RaisePropertyChanged("AllowQuery"); }
        }
        /// <summary>
        /// 允许保存
        /// </summary>
        public bool AllowSave
        {
            get { return _AllowSave; }
            set { _AllowSave = value; RaisePropertyChanged("AllowSave"); }
        }
        /// <summary>
        /// 允许删除
        /// </summary>
        public bool AllowDelete
        {
            get { return _AllowDelete; }
            set { _AllowDelete = value; RaisePropertyChanged("AllowDelete"); }
        }
        /// <summary>
        /// 允许新增
        /// </summary>
        public bool AllowAdd
        {
            get { return _AllowAdd; }
            set { _AllowAdd = value; RaisePropertyChanged("AllowAdd"); }
        }

        /// <summary>
        /// 允许编辑
        /// </summary>
        public bool AllowEdit
        {
            get { return _AllowEdit; }
            set { _AllowEdit = value; RaisePropertyChanged("AllowEdit"); }
        }


        #endregion

        #region 按钮名称
        private string _ContentAdd = "新增";
        private string _ContentEdit = "编辑";
        private string _ContentDelete = "删除";
        private string _ContentSave = "保存";
        private string _ContentQuery = "查询";
        private string _ContentReflesh = "刷新";
        private string _ContentView = "视图";
        private string _ContentAudit = "审核";
        private string _ContentUnAudit = "弃审";
        private string _ContentPrint = "打印";
        private string _ContentExport = "导出";
        private string _ContentCustomA = "";
        private string _ContentCustomB = "";
        private string _ContentCustomC = "";
        private string _ContentCustomD = "";
        private string _ContentCustomE = "";
        private string _ContentCustomF = "";
        private string _ContentCustomG = "";
        /// <summary>
        /// 自定义G按钮标题
        /// </summary>
        public string ContentCustomG
        {
            get { return _ContentCustomG; }
            set { _ContentCustomG = value; RaisePropertyChanged("ContentCustomG"); }
        }
        /// <summary>
        /// 自定义F按钮标题
        /// </summary>
        public string ContentCustomF
        {
            get { return _ContentCustomF; }
            set { _ContentCustomF = value; RaisePropertyChanged("ContentCustomF"); }
        }
        /// <summary>
        /// 自定义E按钮标题
        /// </summary>
        public string ContentCustomE
        {
            get { return _ContentCustomE; }
            set { _ContentCustomE = value; RaisePropertyChanged("ContentCustomE"); }
        }
        /// <summary>
        /// 自定义D按钮标题
        /// </summary>
        public string ContentCustomD
        {
            get { return _ContentCustomD; }
            set { _ContentCustomD = value; RaisePropertyChanged("ContentCustomD"); }
        }
        /// <summary>
        /// 自定义C按钮标题
        /// </summary>
        public string ContentCustomC
        {
            get { return _ContentCustomC; }
            set { _ContentCustomC = value; RaisePropertyChanged("ContentCustomC"); }
        }
        /// <summary>
        /// 自定义B按钮标题
        /// </summary>
        public string ContentCustomB
        {
            get { return _ContentCustomB; }
            set { _ContentCustomB = value; RaisePropertyChanged("ContentCustomB"); }
        }
        /// <summary>
        /// 自定义A按钮标题
        /// </summary>
        public string ContentCustomA
        {
            get { return _ContentCustomA; }
            set { _ContentCustomA = value; RaisePropertyChanged("ContentCustomA"); }
        }
        /// <summary>
        /// 导出按钮标题
        /// </summary>
        public string ContentExport
        {
            get { return _ContentExport; }
            set { _ContentExport = value; RaisePropertyChanged("ContentExport"); }
        }
        /// <summary>
        /// 打印按钮标题
        /// </summary>
        public string ContentPrint
        {
            get { return _ContentPrint; }
            set { _ContentPrint = value; RaisePropertyChanged("ContentPrint"); }
        }


        /// <summary>
        /// 弃审按钮标题
        /// </summary>
        public string ContentUnAudit
        {
            get { return _ContentUnAudit; }
            set { _ContentUnAudit = value; RaisePropertyChanged("ContentUnAudit"); }
        }
        /// <summary>
        /// 审核按钮标题
        /// </summary>
        public string ContentAudit
        {
            get { return _ContentAudit; }
            set { _ContentAudit = value; RaisePropertyChanged("ContentAudit"); }
        }

        /// <summary>
        /// 视图按钮标题
        /// </summary>
        public string ContentView
        {
            get { return _ContentView; }
            set { _ContentView = value; RaisePropertyChanged("ContentView"); }
        }

        /// <summary>
        /// 刷新按钮标题
        /// </summary>
        public string ContentReflesh
        {
            get { return _ContentReflesh; }
            set { _ContentReflesh = value; RaisePropertyChanged("ContentReflesh"); }
        }
        /// <summary>
        /// 查询按钮标题
        /// </summary>
        public string ContentQuery
        {
            get { return _ContentQuery; }
            set { _ContentQuery = value; RaisePropertyChanged("ContentQuery"); }
        }
        /// <summary>
        /// 保存按钮标题
        /// </summary>
        public string ContentSave
        {
            get { return _ContentSave; }
            set { _ContentSave = value; RaisePropertyChanged("ContentSave"); }
        }
        /// <summary>
        /// 删除按钮标题
        /// </summary>
        public string ContentDelete
        {
            get { return _ContentDelete; }
            set { _ContentDelete = value; RaisePropertyChanged("ContentDelete"); }
        }
        /// <summary>
        /// 新增按钮标题
        /// </summary>
        public string ContentAdd
        {
            get { return _ContentAdd; }
            set { _ContentAdd = value; RaisePropertyChanged("ContentAdd"); }
        }

        /// <summary>
        /// 编辑按钮标题
        /// </summary>
        public string ContentEdit
        {
            get { return _ContentEdit; }
            set { _ContentEdit = value; RaisePropertyChanged("ContentEdit"); }
        }
        #endregion

        #region 按钮命令
        /// <summary>
        /// 新增命令
        /// </summary>
        /// <returns></returns>
        public virtual bool ExecuteAdd()
        {
            return true; 
        }
        /// <summary>
        /// 编辑命令
        /// </summary>
        /// <returns></returns>
        public virtual bool ExecuteEdit()
        {
            return true;
        }
        /// <summary>
        /// 删除命令
        /// </summary>
        /// <returns></returns>
        public virtual bool ExecuteDelete()
        {
            return true;
        }
        /// <summary>
        /// 保存命令
        /// </summary>
        /// <returns></returns>
        public virtual bool ExecuteSave()
        {
            return true;
        }
        /// <summary>
        /// 查询命令
        /// </summary>
        /// <returns></returns>
        public virtual bool ExecuteQuery()
        {
            return true;
        }
        /// <summary>
        /// 刷新命令
        /// </summary>
        /// <returns></returns>
        public virtual bool ExecuteReflesh()
        {
            return true;
        }
        /// <summary>
        /// 视图命令
        /// </summary>
        /// <returns></returns>
        public virtual bool ExecuteView()
        {
            return true;
        }
        /// <summary>
        /// 审核命令
        /// </summary>
        /// <returns></returns>
        public virtual bool ExecuteAudit()
        {
            return true;
        }
        /// <summary>
        /// 弃审命令
        /// </summary>
        /// <returns></returns>
        public virtual bool ExecuteUnAudit()
        {
            return true;
        }
        /// <summary>
        /// 打印命令
        /// </summary>
        /// <returns></returns>
        public virtual bool ExecutePrint()
        {
            return true;
        }
        /// <summary>
        /// 导出命令
        /// </summary>
        /// <returns></returns>
        public virtual bool ExecuteExport()
        {
            return true;
        }
        /// <summary>
        /// 自定义A命令
        /// </summary>
        /// <returns></returns>
        public virtual bool ExecuteCustomA()
        {
            return true;
        }
        /// <summary>
        /// 自定义B命令
        /// </summary>
        /// <returns></returns>
        public virtual bool ExecuteCustomB()
        {
            return true;
        }
        /// <summary>
        /// 自定义C命令
        /// </summary>
        /// <returns></returns>
        public virtual bool ExecuteCustomC()
        {
            return true;
        }
        /// <summary>
        /// 自定义D命令
        /// </summary>
        /// <returns></returns>
        public virtual bool ExecuteCustomD()
        {
            return true;
        }
        /// <summary>
        /// 自定义E命令
        /// </summary>
        /// <returns></returns>
        public virtual bool ExecuteCustomE()
        {
            return true;
        }
        /// <summary>
        /// 自定义F命令
        /// </summary>
        /// <returns></returns>
        public virtual bool ExecuteCustomF()
        {
            return true;
        }
        /// <summary>
        /// 自定义G命令
        /// </summary>
        /// <returns></returns>
        public virtual bool ExecuteCustomG()
        {
            return true;
        }
        #endregion


    }
}
