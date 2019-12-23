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
    /// EmployeeBase.xaml 的交互逻辑
    /// </summary>
    public partial class EmployeeBase : Page
    {
        BankEntities context = new BankEntities();
        public EmployeeBase()
        {
            InitializeComponent();
            this.Unloaded += EmployeeBase_Unloaded;

            var q = from t in context.EmployeeInfo
                    select t;
            dataGrid.ItemsSource = q.ToList();
        }

        public void EmployeeBase_Unloaded(object sender, RoutedEventArgs e)
        {
            context.Dispose();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            //更新员工信息
            var item = dataGrid.SelectedItem as EmployeeInfo;
            if(item == null)
            {
                MessageBox.Show("请选择要更新的信息");
                return;
            }
            string employeeNo = item.EmployeeNo;
            InfoWindow updateWindow = new InfoWindow(employeeNo);
            updateWindow.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var item = dataGrid.SelectedItem as EmployeeInfo;
            
            if(item == null)
            {
                MessageBox.Show("请选择要删除的信息！");
                return;
            }

            MessageBoxResult result = MessageBox.Show("确定删除选中信息？", "删除确认", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                var q = from t in context.EmployeeInfo
                        where t.EmployeeNo == item.EmployeeNo
                        select t;
                if(q != null)
                {
                    try
                    {
                        context.EmployeeInfo.Remove(q.FirstOrDefault());
                        int i = context.SaveChanges();
                        MessageBox.Show("删除" + i + "条记录");
                    }
                    catch
                    {
                        MessageBox.Show("删除失败");
                    }
                }
            }
        }
    }
}