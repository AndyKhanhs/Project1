using Prism.Commands;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project1
{
    /// <summary>
    /// Interaction logic for ThongKe.xaml
    /// </summary>
    public partial class DSNhanKhau : UserControl,INotifyPropertyChanged
    {
        NhanKhauService nhanKhauService = new NhanKhauService();
        PhongService phongService = new PhongService();

        private string _filterText;
        private int _sum;        
        private List<NhanKhau> _list;
       
        public string FilterText
        {
            get { return _filterText; }
            set
            {
                if (_filterText != value)
                {
                    _filterText = value;
                    OnPropertyChanged(nameof(FilterText));
                }
            }
        }
        public int SUm
        {
            get { return _sum; }
            set
            {
                if (_sum != value)
                {
                    _sum = value;
                    OnPropertyChanged(nameof(SUm));
                }
            }
        }
        public List<NhanKhau> ListNhanKhau
        {
            get { return _list; }
            set
            {
                if (_list != value)
                {
                    _list = value;
                    OnPropertyChanged(nameof(ListNhanKhau));
                }
            }
        }

        public DelegateCommand<NhanKhau> EditCommand { get; set; }
        public DelegateCommand<NhanKhau> DeleteCommand { get; set; }
        public DelegateCommand FilterCommand { get; set; }

        public DSNhanKhau()
        {
            InitializeComponent();

            using (var dbContext = new MyDbContext())
            {
                ListNhanKhau = nhanKhauService.GetAll();
                SUm = ListNhanKhau.Count();
            }
            
            DataContext = this;
            EditCommand = new DelegateCommand<NhanKhau>(EditNhanKhau);
            DeleteCommand = new DelegateCommand<NhanKhau>(DeleteNhanKhau);
            FilterCommand = new DelegateCommand(FilterItem);
        }
       
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void EditNhanKhau(NhanKhau dto)
        {
            DieuChinhNhanKhau dieuChinhNhanKhau = new DieuChinhNhanKhau(dto);
            Application.Current.MainWindow.Opacity = 0.2;
            dieuChinhNhanKhau.ShowDialog();
            Application.Current.MainWindow.Opacity = 1;
            ListNhanKhau = nhanKhauService.GetAll();
        }

        private void DeleteNhanKhau(NhanKhau dto)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa người này?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                using (var dbContext = new MyDbContext())
                {
                    var room = dbContext.Phong.FirstOrDefault(x => x.Id == dto.Phong);
                    if (room.Soluong == 1)
                    {

                        MessageBoxResult result1 = MessageBox.Show("Nếu xóa người này thì phòng " + dto.Phong + " sẽ trống, bạn có muốn tiếp tục?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result1 == MessageBoxResult.Yes)
                        {
                            nhanKhauService.Delete(dto.Id);
                            phongService.Decrease1(dto.Phong);
                            ListNhanKhau = nhanKhauService.GetAll();
                            SUm = ListNhanKhau.Count();
                        }

                    }
                    else
                    {
                        nhanKhauService.Delete(dto.Id);
                        phongService.Decrease1(dto.Phong);
                        ListNhanKhau = nhanKhauService.GetAll();
                        SUm = ListNhanKhau.Count();
                    }
                }
            }       
 
        }
        private void FilterItem()
        {
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                ListNhanKhau = nhanKhauService.GetAll();
            }
            else
          {
                using (var dbContext = new MyDbContext())
                {
                    ListNhanKhau=dbContext.NhanKhauDb.Where(x => x.Ten.ToLower().Contains(FilterText)).ToList();
                }
            }
        }
    }
}
