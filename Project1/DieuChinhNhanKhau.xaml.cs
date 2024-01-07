using Project1.Models;
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
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Project1
{
    /// <summary>
    /// Interaction logic for DieuChinhNhanKhau.xaml
    /// </summary>
    public partial class DieuChinhNhanKhau : Window
    {
        NhanKhauService nhanKhauService = new NhanKhauService();
        public int Id {  get; set; }
        public string Ten { get; set; }
        public int Phong {  get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Sdt { get; set; }
        public string Cccd { get; set; }
        public string QueQuan { get; set; }
        public DateTime NgayO { get; set; }
        public DieuChinhNhanKhau()
        {

        }
        public DieuChinhNhanKhau(NhanKhau dto)
        {
            InitializeComponent();
            Id = dto.Id;
            NgaySinh = dto.NgaySinh;
            Ten = dto.Ten;
            Phong = dto.Phong;
            GioiTinh = dto.GioiTinh;
            if (GioiTinh=="Nam") Nam.IsChecked = true;
            else Nu.IsChecked=true;
            Cccd = dto.Cccd;
            Sdt = dto.Sdt;
            QueQuan = dto.QueQuan;
            NgayO = dto.NgayO;
            DataContext = this;
        }

        private void LuuThongTin(object sender, RoutedEventArgs e)
        {
            if (Ten == null || Cccd == null || Sdt == null || NgaySinh.CompareTo(new DateTime(1, 1, 1)) == 0 || NgayO.CompareTo(new DateTime(1, 1, 1)) == 0 || QueQuan == null)
            {
                MessageBox.Show("Bạn chưa nhập đủ", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else nhanKhauService.Update(new NhanKhau(Id, Ten, GioiTinh, Phong, NgaySinh, Sdt, Cccd, QueQuan, NgayO));
            this.Close();

        }

        private void Nam_Checked(object sender, RoutedEventArgs e)
        {
            GioiTinh = "Nam";
        }

        private void Nu_Checked(object sender, RoutedEventArgs e)
        {
            GioiTinh = "Nữ";
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
