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
    /// NewAccount.xaml 的交互逻辑
    /// </summary>
    public partial class NewAccount : Page
    {
        public NewAccount()
        {
            InitializeComponent();
            //初始化combobox(开户类型下拉菜单)
            InitComboBox();
        }

        private void InitComboBox()
        {
            //获取开户类型
            string[] items = Enum.GetNames(typeof(MoneyAccountType));
            //将类型信息添加到下拉菜单
            foreach (var s in items)
            {
                comboBoxAccountType.Items.Add(s);
            }
            comboBoxAccountType.SelectedIndex = 0;
        }
        //开户
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            //初始化业务信息类为开户，开户业务类型为选中类型
            Custom custom = DataOperation.CreateCustom(comboBoxAccountType.SelectedItem.ToString());
            //设置账户基本信息
            custom.AccountInfo.accountNo = this.txtAccountNo.Text;//账户
            custom.AccountInfo.IdCard = this.txtIDCard.Text;//身份证号
            custom.AccountInfo.accountName = this.txtAccountName.Text;//账户名
            custom.AccountInfo.accountPass = this.txtPass.Password;//密码
            //创建操作记录基本信息
            custom.Create(this.txtAccountNo.Text, double.Parse(this.txtMoney.Text));
            //实例化操作查询页面，用以显示历史操作记录
            OperateRecord page = new OperateRecord();

            //页面跳转，实现原理不清楚
            NavigationService ns = NavigationService.GetNavigationService(this);  
            ns.Navigate(page);
        }

        //取消开户
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //清空填入的信息
            this.txtAccountName.Clear();
            this.txtIDCard.Clear();
            this.txtAccountName.Clear();
            this.txtPass.Clear();
            this.txtMoney.Clear();
            //跳转到历史操作记录页面
            OperateRecord page = new OperateRecord();
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(page);
        }
        //开户类型下拉菜单焦点改变，则设置对应账户类型的初始id
        private void comboBoxAccountType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //获取选中的类型
            string s = comboBoxAccountType.SelectedItem.ToString();

            using(BankEntities c = new BankEntities())
            {
                //查询账户信息表中同类型账户
                var q = from t in c.AccountInfo
                        where t.accountType == s
                        select t;
                //若存在符合要求的信息
                if (q.Count() > 0)
                {
                    //提取找到的账户信息，获取其中最大的账号id，并在此基础上加一作为默认开户账号
                    this.txtAccountNo.Text = string.Format("{0}", int.Parse(q.Max(x => x.accountNo)) + 1);
                }
                else
                {
                    txtAccountNo.Text = string.Format("{0}00001", comboBoxAccountType.SelectedIndex + 1);
                }
            }
        }
    }
}
