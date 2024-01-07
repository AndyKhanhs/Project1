using ControlzEx.Theming;
using MahApps.Metro.Controls;
using Microsoft.EntityFrameworkCore;
using Project1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Project1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            MainBorder.Child = new ListRoom();
            
        }

        private void Open_TrangChu(object sender, MouseButtonEventArgs e)
        {
            MainBorder.Child = new ListRoom();
            TrangChuBorder.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#2FADFF");
            ThongKeBorder.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#7ACBFF");
            TienPhongBorder.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#7ACBFF");
        }
        private void Open_TienPhong(object sender, MouseButtonEventArgs e)
        {
            MainBorder.Child = new TienPhong1();
            TrangChuBorder.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#7ACBFF");
            ThongKeBorder.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#7ACBFF");
            TienPhongBorder.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#2FADFF");
        }
        private void Open_ThongKe(object sender, MouseButtonEventArgs e)
        {
            MainBorder.Child=new DSNhanKhau();
            TrangChuBorder.Background= (SolidColorBrush)new BrushConverter().ConvertFrom("#7ACBFF");
            ThongKeBorder.Background=(SolidColorBrush)new BrushConverter().ConvertFrom("#2FADFF");
            TienPhongBorder.Background= (SolidColorBrush)new BrushConverter().ConvertFrom("#7ACBFF");
        }
    }
}
