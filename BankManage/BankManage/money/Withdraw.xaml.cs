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
using BankManage.common;

namespace BankManage.money
{
    /// <summary>
    /// Withdraw.xaml 的交互逻辑
    /// </summary>
    public partial class Withdraw : Page
    {
        public Withdraw()
        {
            InitializeComponent();
        }
        //取款
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("确定取款？", "提示", MessageBoxButton.OKCancel);
            if (!(result == MessageBoxResult.OK))
            {
                return;
            }

            //根据对应id创建业务记录
            Custom custom = DataOperation.GetCustom(this.txtAccount.Text);

            //验证密码
            if (custom.AccountInfo.accountPass != this.txtPassword.Password)
            {
                MessageBox.Show("密码不正确");
                return;
            }
            //执行取款操作
            if (!this.txtmount.Text.Equals("全部金额"))
            {
                custom.Withdraw(double.Parse(this.txtmount.Text));
            }
            else
            {
                custom.Withdraw(0); 
            }

            //导航到历史操作记录页面
            OperateRecord page = new OperateRecord();
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(page);
        }
        //取消取款
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //导航到历史操作记录页面
            OperateRecord page = new OperateRecord();
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(page);
        }

        /// <summary>
        /// 设置定期存款和零存整取的应取金额
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAccount_LostFocus(object sender, RoutedEventArgs e)
        {
            
            using (BankEntities context = new BankEntities())
            {
                var q = from t in context.AccountInfo
                        where t.accountNo == txtAccount.Text
                        select t;
                try
                {
                    if (q.Single().accountType == MoneyAccountType.定期存款.ToString() || q.Single().accountType == MoneyAccountType.零存整取.ToString())
                    {
                        //Custom custom = DataOperation.GetCustom(q.Single().accountNo);
                        //txtmount.Text = custom.AccountBalance.ToString();
                        txtmount.Text = "全部金额";
                        txtmount.IsReadOnly = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("信息错误" + ex.Message);
                }
            }
        }
    }
}
