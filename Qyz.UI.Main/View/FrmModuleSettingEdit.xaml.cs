using Qyz.BLL.Core;
using Qyz.FrameWork.Core;
using Qyz.Model.Common;
using Qyz.UI.Base;
using Qyz.UI.MVVM.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Qyz.UI.Main.View
{
    /// <summary>
    /// FrmModuleSettingEdit.xaml 的交互逻辑
    /// </summary>
    public partial class FrmModuleSettingEdit : DialogWindow
    {
        #region  构造函数
        public FrmModuleSettingEdit(Sys_Modules module)
        {
            InitializeComponent();
            this.DataContext = this;
            MainModule = module;
            if (module.Name == null || module.Name == "")
                MyTitle = "新增菜单";
            else
                MyTitle = "编辑菜单";
            
            new MenuInfoBLL().GetMenuInfo((int)GlobalVariable.systemType).ForEach(p => MenuList.Add(p));
            SelectedMenu = MenuList.FirstOrDefault(p => p.ID == module.MenuID);
            if(module.DllName!="")
            {
                
                ReflectFunction.GetClassName(module.DllName).ForEach(p => StartUpClassList.Add(p));
                StartUpClass = StartUpClassList.FirstOrDefault(p => p.FullName == module.StartUpClass);
            }
            
        }
        #endregion

        #region 绑定
        public event Action<Sys_Modules> SaveEvent;
        public static DependencyProperty MyTitleProperty = DependencyProperty.Register("MyTitle", typeof(string), typeof(FrmModuleSettingEdit), new PropertyMetadata("新增菜单"));
        public static DependencyProperty MainModuleProperty = DependencyProperty.Register("MainModule", typeof(Sys_Modules), typeof(FrmModuleSettingEdit), new PropertyMetadata(null));
        public static DependencyProperty MenuListProperty = DependencyProperty.Register("MenuList", typeof(ObservableCollection<Sys_Menus>), typeof(FrmModuleSettingEdit), new PropertyMetadata(null));
        public static DependencyProperty SelectedMenuProperty = DependencyProperty.Register("SelectedMenu", typeof(Sys_Menus), typeof(FrmModuleSettingEdit), new PropertyMetadata(null));
        public static DependencyProperty StartUpClassProperty = DependencyProperty.Register("StartUpClass", typeof(Type), typeof(FrmModuleSettingEdit), new PropertyMetadata(null));
        public static DependencyProperty StartUpClassListProperty = DependencyProperty.Register("StartUpClassList", typeof(ObservableCollection<Type>), typeof(FrmModuleSettingEdit), new PropertyMetadata(new ObservableCollection<Type>()));


        public Type StartUpClass
        {
            get { return GetValue(StartUpClassProperty) as Type; }
            set
            {
                SetValue(StartUpClassProperty, value);
                MainModule.StartUpClass = StartUpClass.FullName;
            }
        }
        public ObservableCollection<Type> StartUpClassList
        {
            get { return GetValue(StartUpClassListProperty) as ObservableCollection<Type>; }
            set { SetValue(StartUpClassListProperty, value); }
        }
        /// <summary>
        /// 当前模块名称
        /// </summary>
        public string MyTitle
        {
            get { return GetValue(MyTitleProperty).ToString(); }
            set { SetValue(MyTitleProperty, value); }
        } 

        /// <summary>
        /// 当前模块
        /// </summary>
        public Sys_Modules MainModule
        {
            get { return GetValue(MainModuleProperty) as Sys_Modules; }
            set { SetValue(MainModuleProperty, value); }
        } 

        /// <summary>
        /// 菜单集合
        /// </summary>
        public ObservableCollection<Sys_Menus> MenuList
        {
            get { return GetValue(MenuListProperty) as ObservableCollection<Sys_Menus>; }
            set { SetValue(MenuListProperty, value); }
        } 

        /// <summary>
        /// 选中的菜单
        /// </summary>
        public Sys_Menus SelectedMenu
        {
            get { return GetValue(SelectedMenuProperty) as Sys_Menus; }
            set { 
                SetValue(SelectedMenuProperty, value);
                MainModule.MenuID = SelectedMenu.ID;
            }
        }
        #endregion

        #region 命令
        /// <summary>
        /// 保存命令
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand((para) =>
                {
                    if (!ValiateData())
                    {
                        return;
                    } 
                    //保存到数据库中
                    if (MyTitle == "新增模块")
                        new ModuleInfoBLL().InsertModuleInfo(MainModule);
                    else
                        new ModuleInfoBLL().UpdateModuleInfo(MainModule);
                    if (SaveEvent != null)
                        SaveEvent(MainModule);
                    this.Close();

                });
            }
        }
        /// <summary>
        /// 取消命令
        /// </summary>
        public ICommand CancelCommand
        {
            get
            {
                return new RelayCommand((p) =>
                {
                    this.Close();
                });
            }
        }
        /// <summary>
        /// 选择程序集命令
        /// </summary>
        public ICommand LoadAssemblyCommand
        {
            get
            {
                return new RelayCommand((p) =>
                {
                    try
                    {
                        Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
                        ofd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        ofd.Filter = "dll,exe|*.dll;*.exe";
                        bool? b = ofd.ShowDialog();
                        if (b.Value)
                        {
                            System.IO.FileInfo fi = new System.IO.FileInfo(ofd.FileName);
                            string FileName = fi.Name;
                            //如果不在同一个目录下，则复制到Image文件夹下
                            if (!fi.DirectoryName.Equals(Environment.CurrentDirectory))
                            {
                                //如果该目录下存在此名称的图片，则判断hash值
                                if (!System.IO.File.Exists(Environment.CurrentDirectory + fi.Name))
                                {
                                    fi.CopyTo(Environment.CurrentDirectory + FileName, true);
                                }
                            }
                            MainModule.DllName = FileName;
                            StartUpClassList.Clear();
                            ReflectFunction.GetClassName(FileName).ForEach(pp => StartUpClassList.Add(pp)); 
                            StartUpClass = StartUpClassList.Count > 0 ? StartUpClassList[0] : null;
                        }
                    }
                    catch (Exception ee)
                    {
                    }
                });
            }
        }
        
        #endregion
        #region 私有方法
        /// <summary>
        /// 验证菜单数据
        /// </summary>
        /// <returns></returns>
        private bool ValiateData()
        {
            if (MainModule.Name == "") return false;
            return true;
        }
        #endregion
    }
}
