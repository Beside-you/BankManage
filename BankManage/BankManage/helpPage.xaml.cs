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

namespace BankManage
{
    /// <summary>
    /// helpPage.xaml 的交互逻辑
    /// </summary>
    public partial class helpPage : Page
    {
        public helpPage()
        {
            InitializeComponent();
            comList.SelectedIndex = 0;
            page1.Visibility = Visibility.Visible;
        }

        private void comList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            page1.Visibility = Visibility.Hidden;
            page2.Visibility = Visibility.Hidden;
            page3.Visibility = Visibility.Hidden;
            page4.Visibility = Visibility.Hidden;
            page5.Visibility = Visibility.Hidden;
            switch (comList.SelectedIndex)
            {
                case 0:
                    page1.Visibility = Visibility.Visible;
                    break;
                case 1:
                    page2.Visibility = Visibility.Visible;
                    break;
                case 2:
                    page3.Visibility = Visibility.Visible;
                    break;
                case 3:
                    page4.Visibility = Visibility.Visible;
                    break;
                case 4:
                    page5.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
