using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace BankManage.employee
{
    /// <summary>
    /// addWindow.xaml 的交互逻辑
    /// </summary>
    public partial class addWindow : Window
    {
        
        BankEntities context;
        private string photoPath = "";
        private EmployeeInfo employee;
        public addWindow()
        {
            InitializeComponent();
            context = new BankEntities();

        }

        private void uploadButton_Click(object sender, RoutedEventArgs e)
        {
            photoBox.Source = null;
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == true)
            {
                photoPath = file.FileName;
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(photoPath, UriKind.RelativeOrAbsolute);
                img.EndInit();
                this.photoBox.Source = img;
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            employee = new EmployeeInfo();

            try
            {
                employee.EmployeeName = nameBox.Text;
                employee.EmployeeNo = numberBox.Text;
                employee.idCard = idBox.Text;
                employee.workDate = dateBox.SelectedDate;
                if (manButton.IsChecked == true)
                {
                    employee.sex = "男";
                }
                else
                {
                    employee.sex = "女";
                }
                employee.telphone = phoneBox.Text;

                if (photoPath != "")
                {
                    Stream stream = File.OpenRead(photoPath);
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, (int)stream.Length);
                    employee.photo = buffer;
                }

                context.EmployeeInfo.Add(employee);
                context.SaveChanges();
                MessageBox.Show("保存成功");
                this.Close();
            }
            catch
            {
                MessageBox.Show("保存失败！");
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
