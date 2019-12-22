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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BankManage.other
{
    /// <summary>
    /// ChangeEmployee.xaml 的交互逻辑
    /// </summary>
    public partial class ChangeEmployee : Page
    {
        BankEntities context = new BankEntities();
        public ChangeEmployee()
        {
            InitializeComponent();
        }
        //更改密码
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("确定修改密码？", "提示", MessageBoxButton.OKCancel);
            if (!(result == MessageBoxResult.OK))
            {
                return;
            }
            //获取对应的信息记录
            var query = from t in context.AccountInfo
                        where t.accountNo == this.txtAccount.Text
                        select t;
            //若存在记录
            if (query.Count() > 0)
            {
                //取第一条记录信息
                var q = query.First();
                //修改密码
                q.accountPass = this.txtNewPass.Password;
                try
                {
                    context.SaveChanges();
                    MessageBox.Show("更改密码成功！");
                }
                catch
                {
                    MessageBox.Show("更改密码失败！");
                }
            }
        }
        //取消更改
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.txtNewPass.Clear();
            this.txtPassConf.Clear();
        }
    }
}
