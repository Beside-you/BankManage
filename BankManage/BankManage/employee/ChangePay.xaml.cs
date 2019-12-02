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

namespace BankManage.employee
{
    /// <summary>
    /// ChangePay.xaml 的交互逻辑
    /// </summary>
    public partial class ChangePay : Page
    {
        BankEntities context = new BankEntities();
        public ChangePay()
        {
            InitializeComponent();
            this.Unloaded += ChangePay_Unloaded;

            var q = from t in context.EmployeeInfo
                    select t;
            dataGrid1.ItemsSource = q.ToList();
        }

        public void ChangePay_Unloaded(object sender, RoutedEventArgs e)
        {
            context.Dispose();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("确定修改信息？", "提示", MessageBoxButton.OKCancel);
            if (!(result == MessageBoxResult.OK))
            {
                return;
            }
            try
            {
                context.SaveChanges();
                MessageBox.Show("成功保存");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "保存失败");

            }
        }
    }
}
