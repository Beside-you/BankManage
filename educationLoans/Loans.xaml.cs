using System;
using System.Windows;
using System.Windows.Controls;

namespace BankManage.NewFolder1
{
    /// <summary>
    /// Loans.xaml 的交互逻辑
    /// </summary>
    public partial class Loans : Page
    {
        private int i = 100;
        public Loans()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int num = Convert.ToInt32(moneyText.Text);
            if (num == 8000)
            {
                num = 8000;
            }
            else
            {
                num += i;
            }
            moneyText.Text = num.ToString();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            int num = Convert.ToInt32(moneyText.Text);
            if (num == 1000)
            {
                num = 1000;
            }
            else
            {
                num -= i;
            }
            moneyText.Text = num.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result =  MessageBox.Show("是否确定贷款？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) { }
            else
            {
                using (var context = new loansEntities())
                {
                    Table table = new Table()
                    {
                        名字 = nameText.Text.ToString(),
                        贷款金额 = Convert.ToInt32(moneyText.Text),
                        贷款年份 = Convert.ToInt32(yearText.Text),
                        贷款月份 = Convert.ToInt32(monthText.Text),
                        贷款日期 = Convert.ToInt32(dayText.Text),
                        身份证号 = IDText.Text.ToString()
                    };
                    try
                    {
                        context.Table.Add(table);
                        context.SaveChanges();
                        MessageBox.Show("贷款成功！");
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("贷款失败！请重新操作！");
                    }
                       
                   
                }
            }
        }

    }
}
