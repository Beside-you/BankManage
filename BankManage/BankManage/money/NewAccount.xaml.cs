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
            MessageBoxResult result = MessageBox.Show("确定开通账户吗？", "提示", MessageBoxButton.OKCancel);
            if (!(result == MessageBoxResult.OK))
            {
                return;
            }
           
            //如果不是零存整取，则一百元起存
            if (!comboBoxAccountType.SelectedItem.ToString().Equals(MoneyAccountType.零存整取.ToString()))
            {
                if(double.Parse(txtMoney.Text) < 100)
                {
                    MessageBox.Show("100元起存！");
                    return;
                }
            }
            else//零存整取五元起存
            {
                if(double.Parse(txtMoney.Text) < 5)
                {
                    MessageBox.Show("五元起存");
                    return;
                }
            }

            //初始化业务信息类为开户，开户业务类型为选中类型,开户利率类别为选中类别
            Custom custom = DataOperation.CreateCustom(comboBoxAccountType.SelectedItem.ToString(),RateTypeCom.SelectedItem.ToString());
            //设置账户基本信息
            custom.AccountInfo.accountNo = this.txtAccountNo.Text;//账户
            custom.AccountInfo.IdCard = this.txtIDCard.Text;//身份证号
            custom.AccountInfo.accountName = this.txtAccountName.Text;//账户名
            custom.AccountInfo.accountPass = this.txtPass.Password;//密码
            //创建操作记录基本信息
            custom.Create( this.txtAccountNo.Text, double.Parse( this.txtMoney.Text));
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
            rateTypeText.Visibility = Visibility.Hidden;
            RateTypeCom.Visibility = Visibility.Hidden;

            RateTypeCom.Items.Clear();
            //显示期限选择栏
            //设置Combox中的枚举值然后实现切换隐藏
            if (s.Equals(MoneyAccountType.定期存款.ToString()))
            {
                rateTypeText.Visibility = Visibility.Visible;
                RateTypeCom.Visibility = Visibility.Visible;

                
                RateTypeCom.Items.Add(RateType.定期1年.ToString());
                RateTypeCom.Items.Add(RateType.定期3年.ToString());
                RateTypeCom.Items.Add(RateType.定期5年.ToString());
                RateTypeCom.SelectedIndex = 0;
                //string[] rateType = {""}
            }
            else if(s.Equals(MoneyAccountType.零存整取.ToString()))
            {
                rateTypeText.Visibility = Visibility.Visible;
                RateTypeCom.Visibility = Visibility.Visible;
                RateTypeCom.Items.Add(RateType.零存整取1年.ToString());
                RateTypeCom.Items.Add(RateType.零存整取3年.ToString());
                RateTypeCom.Items.Add(RateType.零存整取5年.ToString());
                RateTypeCom.SelectedIndex = 0;
            }
            else if(s.Equals(MoneyAccountType.活期存款.ToString()))
            {
                RateTypeCom.Items.Add( RateType.活期.ToString());
                RateTypeCom.SelectedIndex =  0;
            }


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
