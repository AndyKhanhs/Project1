using Project1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Project1
{
    /// <summary>
    /// Interaction logic for UpdateTienPhong.xaml
    /// </summary>
    public partial  class UpdateTienPhong : Window,INotifyPropertyChanged
    {

        public int Mon {  get; set; }
        public int Ye { get; set; }
        private int _dienCu;
        private int _dienMoi;
        private int _tieuThuDien;
        private int _thanhTienDien;
        private int _nuocCu;
        private int _nuocMoi;
        private int _tieuThuNuoc;
        private int _thanhTienNuoc;
        private int _tong;
        private int _daNop;
        private int _conThieu;
        private int _tNo;
        public int Id { get; set; }
        public int gia { get; set; }
        public int dienCu
        {
            get { return _dienCu; }
            set
            {
                if (_dienCu != value)
                {
                    _dienCu = value;
                    OnPropertyChanged(nameof(dienCu));
                    UpdateTieuThuDien();
                }
            }
        }

        public int dienMoi
        {
            get { return _dienMoi; }
            set
            {
                if (_dienMoi != value)
                {
                    _dienMoi = value;
                    OnPropertyChanged(nameof(dienMoi));
                    UpdateTieuThuDien();
                }
            }
        }

        public int tieuThuDien
        {
            get { return _tieuThuDien; }
            set
            {
                if (_tieuThuDien != value)
                {
                    _tieuThuDien = value;
                    OnPropertyChanged(nameof(tieuThuDien));
                }
            }
        }
        public int thanhTienDien
        {
            get { return _thanhTienDien; }
            set
            {
                if (_thanhTienDien != value)
                {
                    _thanhTienDien = value;
                    OnPropertyChanged(nameof(thanhTienDien));
                }
            }
        }
        
        public int nuocCu
        {
            get { return _nuocCu; }
            set
            {
                if (_nuocCu != value)
                {
                    _nuocCu = value;
                    OnPropertyChanged(nameof(nuocCu));
                    UpdateTieuThuNuoc();
                }
            }
        }

        public int nuocMoi
        {
            get { return _nuocMoi; }
            set
            {
                if (_nuocMoi != value)
                {
                    _nuocMoi = value;
                    OnPropertyChanged(nameof(nuocMoi));
                    UpdateTieuThuNuoc();
                }
            }
        }

        public int tieuThuNuoc
        {
            get { return _tieuThuNuoc; }
            set
            {
                if (_tieuThuNuoc != value)
                {
                    _tieuThuNuoc = value;
                    OnPropertyChanged(nameof(tieuThuNuoc));
                }
            }
        }
        public int thanhTienNuoc
        {
            get { return _thanhTienNuoc; }
            set
            {
                if (_thanhTienNuoc != value)
                {
                    _thanhTienNuoc = value;
                    OnPropertyChanged(nameof(thanhTienNuoc));
                }
            }
        }
        
        public int tong
        {
            get { return _tong; }
            set
            {
                if (_tong != value)
                {
                    _tong = value;
                    OnPropertyChanged(nameof(tong));

                }
            }
        }

        public int daNop
        {
            get { return _daNop; }
            set
            {
                if (_daNop != value)
                {
                    _daNop = value;
                    OnPropertyChanged(nameof(daNop));
                }
            }
        }
        public int conThieu
        {
            get { return _conThieu; }
            set
            {
                if (_conThieu != value)
                {
                    _conThieu = value;
                    OnPropertyChanged(nameof(conThieu));
                }
            }
        }
        public int veSinh { get; set; }
        public int mang { get; set; }
        public int tNo {
            get { return _tNo; }
            set
            {
                if (_tNo != value)
                {
                    _tNo = value;
                    OnPropertyChanged(nameof(tNo));

                }
            }
        }
        
        public UpdateTienPhong(int mon,int ye,TienPhongFull t)
        {
            InitializeComponent();
            DataContext = this;
            Checkk = true;
            Mon = mon;
            Ye = ye;
            Id = t.Id;
            gia = t.Gia;
            dienCu = t.DienCu;
            dienMoi = t.DienMoi;
            tieuThuDien = t.TieuThuDien;
            thanhTienDien = t.ThanhTienDien;
            nuocCu = t.NuocCu;
            nuocMoi = t.NuocMoi;
            tieuThuNuoc = t.TieuThuNuoc;
            thanhTienNuoc = t.ThanhTienNuoc;
            tNo = t.TNo;
            tong = t.Tong;
            veSinh = t.VeSinh;
            mang = t.Mang;
            daNop = t.DaNop;
            conThieu = t.ConThieu;
        }

        private void XoaTP(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa tiền phòng tháng " + Mon + " năm " + Ye + " của phòng " + IdPhong.Text + " ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result==MessageBoxResult.Yes)
            {
                using (var moneyContext=new MoneyContext(Mon, Ye))
                {
                    moneyContext.XoaTienPhong(int.Parse(IdPhong.Text));
                    this.Close();
                }
            }
        }
        public bool Checkk {  get; set; }
        private void ChinhSuaTP(object sender, RoutedEventArgs e)
        {
            if (Checkk)
            {
                Tiltle.Text = "Chỉnh sửa tiền phòng";
                DienCu.IsReadOnly = false;
                DienMoi.IsReadOnly = false;
                NuocCu.IsReadOnly = false;
                NuocMoi.IsReadOnly = false;
                TNo.IsReadOnly = false;
                btnLuu.Visibility = Visibility.Hidden;
                btnXoa.Visibility = Visibility.Hidden;
                btnChinh.Content = "Hoàn tất chỉnh sửa";
                btnChinh.Width = 300;
                Checkk = !Checkk;
            }
            else
            {
                Tiltle.Text = "Chi tiết tiền phòng";
                DienCu.IsReadOnly = true;
                DienMoi.IsReadOnly = true;
                NuocCu.IsReadOnly = true;
                NuocMoi.IsReadOnly = true;
                TNo.IsReadOnly = true;
                btnLuu.Visibility = Visibility.Visible;
                btnXoa.Visibility = Visibility.Visible;
                btnChinh.Content = "Chỉnh sửa";
                btnChinh.Width = 200;
                Checkk = !Checkk;
            }
            
        }

        private void LuuTP(object sender, RoutedEventArgs e)
        {
            using(var moneyContext=new MoneyContext(Mon,Ye)) {
                moneyContext.UpdateTienPhong(new TienPhongFull(Id,nuocCu,nuocMoi,dienCu,dienMoi,tNo,daNop));
            }
            this.Close();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void DienCu_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTieuThuDien();
        }

        private void DienMoi_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTieuThuDien();
        }

        private void NuocCu_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTieuThuNuoc();
        }

        private void NuocMoi_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTieuThuNuoc();
        }

        private void TNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            tong = gia + thanhTienDien + thanhTienNuoc + tNo + veSinh + mang;
            conThieu = tong - daNop;
        }

        private void DaNop_TextChanged(object sender, TextChangedEventArgs e)
        {
            conThieu = tong - daNop;
        }
        
        private void UpdateTieuThuNuoc()
        {
            tieuThuNuoc = nuocMoi - nuocCu;
            thanhTienNuoc = tieuThuNuoc * 26000;
            tong = gia + thanhTienDien + thanhTienNuoc + tNo + veSinh + mang;
            conThieu = tong - daNop;
        }
        private void UpdateTieuThuDien()
        {
            tieuThuDien = dienMoi - dienCu;
            thanhTienDien = tieuThuDien * 4000;
            tong = gia + thanhTienDien + thanhTienNuoc + tNo + veSinh + mang;
            conThieu = tong - daNop;
        }
        public int preDaNop { get; set; }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (daNop != tong) preDaNop = daNop;
            if (NopDu.IsChecked == true)
            {
                daNop = tong;

            }
            if(NopDu.IsChecked==false) daNop = preDaNop;
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
