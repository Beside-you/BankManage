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

namespace BankManage
{
    /// <summary>
    /// Main.xaml 的交互逻辑
    /// </summary>
    public partial class Main : Window
    {
        //TODO:信息验证
        //TODO:帮助模块，操作帮助
        public Main()
        {
            InitializeComponent();
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.SourceInitialized += MainWindow_SourceInitialized;//绑定初始化事件
        }
        //根据点击的按钮显示对应页面
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Button item = e.Source as Button;
            if (item != null)
            {
                frame1.Source = new Uri(item.Tag.ToString(), UriKind.Relative);
            }
        }

        //窗口初始化事件
        void MainWindow_SourceInitialized(object sender, System.EventArgs e)
        {
            //默认显示当前页面，（frame框架）
            this.frame1.Source = new Uri("money/OperateRecord.xaml", UriKind.Relative);
            //优先启动启动登陆窗体，阻塞模式
            LoginForm login = new LoginForm();
            login.ShowDialog();
        }
    }
}
