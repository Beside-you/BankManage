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

namespace BankManage.query
{
    /// <summary>
    /// TotalQuery.xaml 的交互逻辑
    /// </summary>
    public partial class TotalQuery : Page
    {
        BankEntities context = new BankEntities();
        public TotalQuery()
        {
            InitializeComponent();
            this.Unloaded += TotalQuery_Unloaded;//卸载页面时关闭实体数据模型实例
        }

        void TotalQuery_Unloaded(object sender, RoutedEventArgs e)
        {
            context.Dispose();
        }
        //查询当前账号的所有记录信息
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //查询对应账号的记录
            var query = from t in context.MoneyInfo
                        where t.accountNo == txtID.Text
                        select t;
            datagrid1.ItemsSource = query.ToList();
        }
    }
}
