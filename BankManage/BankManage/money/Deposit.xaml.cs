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
    /// Deposit.xaml 的交互逻辑
    /// </summary>
    public partial class Deposit : Page
    {
        
        public Deposit()
        {
            InitializeComponent();
        }

        //存款
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            
            //根据操作账号id实例化对应的操作信息记录类
            Custom custom = DataOperation.GetCustom(this.txtAccount.Text);

            if(custom.AccountInfo.accountType.Equals(MoneyAccountType.定期存款.ToString()))
            {
                MessageBox.Show("定期账户不可二次存款！");
                return;
            }
            if(custom.AccountInfo.accountType.Equals(MoneyAccountType.零存整取.ToString()))
            {
                if(double.Parse(txtmount.Text) < 5)
                {
                    MessageBox.Show("零存整取五元起存！");
                    return;
                }
            }
            if(custom.AccountInfo.accountType.Equals(MoneyAccountType.活期存款.ToString()))
            {
                if(double.Parse(txtmount.Text) < 100)
                {
                    MessageBox.Show("活期存款一百元起存！");
                    return;
                }
            }
            //若未找到对应账户信息
            if (custom == null)
            {
                MessageBox.Show("帐号不存在！");

                return;
            }
            //设置存取款操作的操作账号
            custom.MoneyInfo.accountNo = txtAccount.Text;
            //存款
            custom.Diposit("存款", double.Parse(this.txtmount.Text));
            //导航到历史操作记录页面
            OperateRecord page = new OperateRecord();
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(page);
        }
        //取消存款
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //导航到历史操作记录页面
            OperateRecord page = new OperateRecord();
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(page);
        }
    }
}
