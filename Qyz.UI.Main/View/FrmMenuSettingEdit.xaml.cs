using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Qyz.UI.Base;
using Qyz.Model.Common;
using Qyz.UI.MVVM.Command;
using Qyz.BLL.Core;
namespace Qyz.UI.Main.View
{
    /// <summary>
    /// FrmMenuSettingEdit.xaml 的交互逻辑
    /// </summary>
    public partial class FrmMenuSettingEdit : DialogWindow
    {
        public event Action<Sys_Menus> SaveEvent;
        public static DependencyProperty MyTitleProperty = DependencyProperty.Register("MyTitle", typeof(string), typeof(FrmMenuSettingEdit), new PropertyMetadata("新增菜单"));
        public static DependencyProperty MenuProperty = DependencyProperty.Register("MainMenu", typeof(Sys_Menus), typeof(FrmMenuSettingEdit), new PropertyMetadata(null));

        /// <summary>
        /// 当前菜单名称
        /// </summary>
        public string MyTitle
        {
            get { return GetValue(MyTitleProperty).ToString(); }
            set { SetValue(MyTitleProperty, value); }
        }
        /// <summary>
        /// 编辑的菜单
        /// </summary>
        public Sys_Menus MainMenu
        {
            get { return (Sys_Menus)GetValue(MenuProperty); }
            set { SetValue(MenuProperty, value); }
        }

        public FrmMenuSettingEdit(Sys_Menus menu)
        {
            InitializeComponent();
            this.DataContext = this;
            MainMenu = menu;
            if (menu.Name == null||menu.Name=="") 
                MyTitle = "新增菜单"; 
            else
                MyTitle = "编辑菜单"; 
        }


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
                    if (MyTitle == "新增菜单")
                        new MenuInfoBLL().AddMenuInfo(MainMenu);
                    else
                        new MenuInfoBLL().UpdateMenuInfo(MainMenu);

                    if (SaveEvent != null)
                        SaveEvent(MainMenu);
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
        /// 修改图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
                //ofd.Multiselect = false;
                ofd.InitialDirectory = "c:\\";
                ofd.Filter = "图片文件|*.jpg;*.png;*.gif";
                bool? b = ofd.ShowDialog();
                if (b.Value)
                { 
                    System.IO.FileInfo fi = new System.IO.FileInfo(ofd.FileName);
                    string FileName = fi.Name;
                    //如果不在同一个目录下，则复制到Image文件夹下
                    if (!fi.DirectoryName.Equals(Environment.CurrentDirectory + "/Image"))
                    {
                        
                        //如果该目录下存在此名称的图片，则判断hash值
                        if (System.IO.File.Exists(Environment.CurrentDirectory + "/Image/" + fi.Name))
                        {
                            //如果图片hash值相同,则制定Image文件夹下图片
                            if (ComputeMD5(ofd.FileName).Equals(ComputeMD5(Environment.CurrentDirectory + "/Image/" + fi.Name)))
                            {
                                MainMenu.ImagePath = fi.Name;
                                return;
                            }
                            else//否则，则拷贝到Image文件夹下，并重命名图片
                            {
                                FileName = fi.Name.Split('.')[0] + "(1)." + fi.Extension;
                                //fi.CopyTo(Environment.CurrentDirectory + "/Image/" + FileName, true);
                            }
                        }
                        fi.CopyTo(Environment.CurrentDirectory + "/Image/" + FileName, true);
                    }
                    MainMenu.ImagePath = FileName;
                }
            }
            catch (Exception ee)
            {
            }
        }
        private string ComputeMD5(string fileName)
        {
            String hashMD5 = String.Empty;
            //检查文件是否存在，如果文件存在则进行计算，否则返回空值
            if (System.IO.File.Exists(fileName))
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    //计算文件的MD5值
                    System.Security.Cryptography.MD5 calculator = System.Security.Cryptography.MD5.Create();
                    Byte[] buffer = calculator.ComputeHash(fs);
                    calculator.Clear();
                    //将字节数组转换成十六进制的字符串形式
                    StringBuilder stringBuilder = new StringBuilder();
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        stringBuilder.Append(buffer[i].ToString("x2"));
                    }
                    hashMD5 = stringBuilder.ToString();
                }//关闭文件流
            }//结束计算
            return hashMD5;
        }
        #endregion
        #region 私有方法
        /// <summary>
        /// 验证菜单数据
        /// </summary>
        /// <returns></returns>
        private bool ValiateData()
        {
            if (MainMenu.Name == "") return false;
            if (MainMenu.ImagePath == "") return false;
            return true;
        }
        #endregion

       
    }
}
