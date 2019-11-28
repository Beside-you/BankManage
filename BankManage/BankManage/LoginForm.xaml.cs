using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BankManage;
using BankManage.common;

namespace BankManage
{
    /// <summary>
    /// LoginForm.xaml 的交互逻辑
    /// </summary>
    public partial class LoginForm : Window
    {
        public string UserName { get; set; }
        private BankEntities dbEntity = new BankEntities();//实体数据模型

        public LoginForm()
        {
            InitializeComponent();
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            UserName = string.Empty;
        }
        //单击登录时进行身份验证
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //从LoginInfo表中获取对应的记录
            var query = from t in dbEntity.LoginInfo
                        where t.Bno == this.combox.Text && t.Password == this.pass.Password
                        select t;

            //若存在记录
            if (query.Count() > 0)
            {
                //取获取到的记录的第一条信息
                var q = query.First();
                //执行数据操作，获取姓名
                UserName = DataOperation.GetOperateName(q.Bno);
                this.Close();//关闭窗口
            }
            else//若不存在记录，显示信息，清除密码，设置光标焦点为密码输入框
            {
                MessageBox.Show("登录失败！");
                this.pass.Clear();
                this.pass.Focus();
            }

        }

        //关闭窗体
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //窗体关闭时进行关闭操作
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //若用户姓名信息为空，则终止程序
            if (string.IsNullOrEmpty(this.UserName) == true)
            {
                App.Current.Shutdown();
            }
        }

        //将账户表中的账号信息显示到此处
        private void Window_SourceInitialized_1(object sender, EventArgs e)
        {
            //查询登录表中的记录
            var query = from t in dbEntity.LoginInfo
                        select t.Bno;
            //将获取的Bno标号显示到combox
            this.combox.ItemsSource = query.ToList();
        }

    }
}
