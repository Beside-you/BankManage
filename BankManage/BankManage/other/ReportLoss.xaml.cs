using BankManage.common;
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
    /// ReportLoss.xaml 的交互逻辑
    /// </summary>
    public partial class ReportLoss : Page
    {
        //挂失
        public ReportLoss()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 挂失账号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result =  MessageBox.Show("确认挂失？","挂失",MessageBoxButton.OKCancel);
            if( !(result == MessageBoxResult.OK))
            {
                return;
            }


            using(BankEntities context = new BankEntities())
            {
                //验证账号密码身份证号
                var q = from t in context.AccountInfo
                        where t.accountNo == txtAccountNo.Text
                        select t;
                if(q.Count() > 0)
                {
                    try
                    {
                        
                       if( !(q.Single().accountPass == txtPass.Text && q.Single().IdCard == txtId.Text))
                        {
                            MessageBox.Show("信息有误！");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("信息错误" + ex.Message);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("账号不存在");
                    return;
                }
                //修改账号信息
                //q.Single().accountNo = txtNewId.Text;

                AccountInfo account = new AccountInfo()
                {
                    accountNo = txtNewId.Text,
                    IdCard = txtId.Text,
                    accountName = q.Single().accountName,
                    accountPass = txtNewPass.Text,
                    accountType = q.Single().accountType
                };
                context.AccountInfo.Remove(q.Single());
                context.AccountInfo.Add(account);
                
                //修改操作记录信息
                var q1 = from t in context.MoneyInfo
                         where t.accountNo == txtAccountNo.Text
                         select t;
                foreach(var item in q1)
                {
                    item.accountNo = txtNewId.Text;
                }
                context.SaveChanges();
            }
            
        }
    }
}
