using ControlzEx.Standard;
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
using System.Windows.Shapes;

namespace Project1
{
    /// <summary>
    /// Interaction logic for XemChiTiet.xaml
    /// </summary>
    public partial class XemChiTiet : UserControl, INotifyPropertyChanged
    {
        NhanKhauService nhanKhauService=new NhanKhauService();
        PhongService phongService = new PhongService();
        private List<NhanKhau> _list;
        private int _id;
       
        public DelegateCommand<NhanKhau> EditCommand { get; set; }
        public DelegateCommand<NhanKhau> DeleteCommand { get; set; }

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }
        private DateTime _ngayCoc;
        public DateTime NgayCoc
        {
            get { return _ngayCoc; }
            set
            {
                if (_ngayCoc != value)
                {
                    _ngayCoc = value;
                    OnPropertyChanged(nameof(NgayCoc));
                }
            }
        }
        public List<NhanKhau> ListNhanKhau {
            get { return _list; }
            set {
                if (_list != value)
                {
                    _list = value;
                    OnPropertyChanged(nameof(ListNhanKhau));
                }
            } 
        }
        public XemChiTiet()
        {

        }
        public XemChiTiet(int id)
        {
            InitializeComponent();
            Id = id;
            
            using (var dbContext = new MyDbContext())
            {
                ListNhanKhau = nhanKhauService.GetFrom(Id);
                NgayCoc=(dbContext.Phong.FirstOrDefault(x=>x.Id==Id)).NgayCoc;
            }
            DataContext = this;
            EditCommand = new DelegateCommand<NhanKhau>(EditNhanKhau);
            DeleteCommand = new DelegateCommand<NhanKhau>(DeleteNhanKhau);
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
            ListNhanKhau = nhanKhauService.GetFrom(Id);
        }
        
        private void DeleteNhanKhau(NhanKhau dto)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa người này?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result==MessageBoxResult.Yes)
            {
                if(ListNhanKhau.Count==1)
                {
                    MessageBoxResult result1 = MessageBox.Show("Nếu xóa người này thì phòng "+Id+" sẽ trống, bạn có muốn tiếp tục?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if(result1==MessageBoxResult.Yes)
                    {
                        
                        DependencyObject parent = VisualTreeHelper.GetParent(this);
                        while (parent != null && !(parent is Border))
                        {
                            parent = VisualTreeHelper.GetParent(parent);

                        }
                        if (parent is Border border)
                        {
                            nhanKhauService.Delete(dto.Id);
                            phongService.Decrease1(dto.Phong);
                            border.Child = new ListRoom();

                        }
                    }
                   
                }
                else {
                    nhanKhauService.Delete(dto.Id);
                    phongService.Decrease1(dto.Phong);
                    ListNhanKhau = nhanKhauService.GetFrom(Id);
                }
                
            }
        }    
        private void Back_ListRoom(object sender, RoutedEventArgs e)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(this);
            while (parent != null && !(parent is Border))
            {
                parent = VisualTreeHelper.GetParent(parent);

            }
            if (parent is Border border)
            {
                border.Child = new ListRoom();

            }
        }

        private void AddKhach(object sender, RoutedEventArgs e)
        {
            ThemKhach themKhach = new ThemKhach(Id);
            Application.Current.MainWindow.Opacity = 0.2;
            themKhach.ShowDialog();
            Application.Current.MainWindow.Opacity = 1;
            ListNhanKhau = nhanKhauService.GetFrom(Id);
        }
    }
}
