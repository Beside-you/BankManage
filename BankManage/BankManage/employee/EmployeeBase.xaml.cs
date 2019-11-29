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
            try
            {
                context.SaveChanges();
                MessageBox.Show("保存成功");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "保存失败");
            }
        }
    }
}
