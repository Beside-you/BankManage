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

namespace BankManage.rateManage
{
    /// <summary>
    /// ratePage.xaml 的交互逻辑
    /// </summary>
    public partial class ratePage : Page
    {
        BankEntities context = new BankEntities();

        public ratePage()
        {
            InitializeComponent();
            this.Unloaded += ratePage_Unloaded;
            //查询利率设置信息
            var q = from t in context.RateInfo
                    select t;
            dataGrid1.ItemsSource = q.ToList();
        }
        void ratePage_Unloaded(object sender, RoutedEventArgs e)
        {
            context.Dispose();
        }
        //保存
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("确定修改利率？", "提示", MessageBoxButton.OKCancel);
            if (!(result == MessageBoxResult.OK))
            {
                return;
            }
            try
            {
                context.SaveChanges();
                MessageBox.Show("保存成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "保存失败");
            }

        }
    }
}
