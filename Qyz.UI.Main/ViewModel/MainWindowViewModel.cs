using Qyz.FrameWork.Core;
using Qyz.FrameWork.Type;
using Qyz.Model.Common;
using Qyz.Model.Common.Model;
using Qyz.UI.Base;
using Qyz.UI.Main.Model;
using Qyz.UI.MVVM.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Qyz.UI.Main.ViewModel
{
    public class MainWindowViewModel : PropertyChangedBase
    {
        #region 字段 
        private ObservableCollection<Sys_Modules> _moduleList = new ObservableCollection<Sys_Modules>(); //模块集合
      
        private static ObservableCollection<TabItemInfo> _tabItemsList = new ObservableCollection<TabItemInfo>();//标签集合
        private Sys_Modules _SelectSecMenu;//选中二级目录
        private Sys_Menus _SelectMenu;//选中目录
        private int _SelectMdiIndex;//选中标签页索引
      
        private string _userName;//登录的用户名
        #endregion

        #region CLR属性
        /// <summary>
        /// tabItem选中的页签索引
        /// </summary>
        public int SelectMdiIndex
        {
            get { return _SelectMdiIndex; }
            set { _SelectMdiIndex = value; RaisePropertyChanged("SelectMdiIndex"); }
        }
        /// <summary>
        /// tabitem页签集合
        /// </summary>
        public static ObservableCollection<TabItemInfo> TabItemsList
        {
            get { return MainWindowViewModel._tabItemsList; }
            set { MainWindowViewModel._tabItemsList = value; }
        }
        /// <summary>
        /// 登录人员的名字
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; RaisePropertyChanged("UserName"); }
        }
        
       
        /// <summary>
        /// 选中的二级菜单
        /// </summary>
        public Sys_Modules SelectSecMenu
        {
            get { return _SelectSecMenu; }
            set { _SelectSecMenu = value; RaisePropertyChanged("SelectSecMenu"); }
        }
        /// <summary>
        /// 选中的一级菜单
        /// </summary>
        public Sys_Menus SelectMenu
        {
            get { return _SelectMenu; }
            set
            {
                _SelectMenu = value;
                //修改二级菜单目录
                ModuleList.Clear();
                GlobalVariable.modulelList.FindAll(p => p.MenuID == _SelectMenu.ID).ForEach(p => ModuleList.Add(p));
                RaisePropertyChanged("SelectMenu");
            }
        }
        /// <summary>
        /// 菜单集合
        /// </summary>
        public ObservableCollection<Sys_Menus> MenuList
        {
            get { return GlobalVariable.menuList; }
            set { GlobalVariable.menuList = value; }
        }
        /// <summary>
        /// 模块集合
        /// </summary>
        public ObservableCollection<Sys_Modules> ModuleList
        {
            get { return _moduleList; }
            set { _moduleList = value; }
        }
        #endregion


        #region 构造函数
        public MainWindowViewModel()
        {
            if (GlobalVariable.myAccount == null)
                return;
            UserName = GlobalVariable.myAccount.UserName; 
            if (MenuList.Count > 0) SelectMenu = MenuList.First(); 
           
        }
        #endregion
        #region 命令
        /// <summary>
        /// 选中二级菜单反射打开对应的界面
        /// </summary>
        public ICommand OpenModuleCommand 
        {
            get
            {
                return new RelayCommand((e) =>
                    {
                        Sys_Modules mo = e as Sys_Modules;
                        //判断该页面是否已经打开
                        for (int index = 0; index < TabItemsList.Count; index++)
                        {
                            if (TabItemsList[index].Name == mo.Name)
                            { 
                                SelectMdiIndex = index;
                                return;
                            }
                        }

                        object[] obs = mo.Parameter.Split(',');
                        object ob = ReflectFunction.RunReflectMethod(mo.DllName, mo.StartUpClass, "MainControl", false, obs);
                        if (ob == null)
                            return;

                        TabItemInfo tii = new TabItemInfo();
                        tii.Id = mo.ID;
                        tii.Name = mo.Name;
                        tii.ContentControl = ob as MdiControl; 
                        TabItemsList.Add(tii);
                        SelectMdiIndex = TabItemsList.IndexOf(tii);
                    });
            }
        }
        #endregion

        #region  公共方法
        public void CloseMdiControlCommand(string tabName)
        {
            for (int index = 0; index < TabItemsList.Count; index++)
            {
                if (TabItemsList[index].Name == tabName)
                {
                    if (index > 0)
                    {
                        SelectMdiIndex = index - 1;
                    }
                    else if (index < TabItemsList.Count - 1)
                    {
                        SelectMdiIndex = index + 1;
                    }
                    TabItemsList.RemoveAt(index);
                    break;
                }
            }
        }
        #endregion

        #region 私有方法
        #endregion

        public System.Windows.Visibility LoginVisibility { get; set; }
        public System.Windows.Visibility LoginOutVisibility { get; set; }
        

    }
}
