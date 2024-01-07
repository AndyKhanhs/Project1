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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Prism.Common;
using Prism.Mvvm;
using System.ComponentModel;
using Prism.Commands;

namespace Project1
{
    /// <summary>
    /// Interaction logic for Item_Phong.xaml
    /// </summary>
    public partial class Item_Phong : UserControl,INotifyPropertyChanged
    {
        

        private string _status;
       
       
        private static int _currentIndex=0; // Static to ensure it increments across all instances
        public int CurrentIndex
        {
            get { return _currentIndex; }
            set
            {
                if (_currentIndex != value)
                {
                    _currentIndex = value;
                    OnPropertyChanged(nameof(CurrentIndex));
                }
            }
        }

        public int CurrentRoomNumber
        {
            get
            {
                int[] roomNumbers = { 101, 102, 103, 104, 105, 201, 202, 203, 204, 205, 301, 302, 303, 304, 305 }; // Your array of room numbers
                if (CurrentIndex < roomNumbers.Length)
                {
                    int roomNumber = roomNumbers[CurrentIndex];
                     // Increment for the next instance
                    return roomNumber;
                }
                else
                {
                    // Handle the case when there are no more available room numbers
                    return 0;
                }
            }
        }
        public string Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }
        
        
        public Item_Phong()
        {
            InitializeComponent();
            Status = "0";
            DataContext = this;
            roomNumbertxt.Text = CurrentRoomNumber.ToString();
            using (var dbContext = new MyDbContext())
            {
                var room = dbContext.Phong.FirstOrDefault(x => x.Id == int.Parse(roomNumbertxt.Text));
                if (room != null)
                {
                    Status = "1";
                    Quantity.Text = room.Soluong.ToString();
                    BitmapImage newImageSource = new BitmapImage(new Uri("/Images/check-mark.png", UriKind.Relative));                                       
                    ImageStatus.Source = newImageSource;
                    using (var dbContext2 = new MyDbContext())
                    {
                        var person = dbContext.NhanKhauDb.FirstOrDefault(x => x.Phong == room.Id);

                        if (person != null)
                        {
                            Host.Text = person.Ten;
                        }
                    }
                }
            }
            
            CurrentIndex++;
            if (CurrentIndex == 15) CurrentIndex = 0;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ChiTietCommand(object sender, RoutedEventArgs e)
        {
            
            DependencyObject parent = VisualTreeHelper.GetParent(this);
            while(parent !=null && !(parent is Grid)){
                parent = VisualTreeHelper.GetParent(parent);

            }
            if (parent is Grid grid)
            {
                while (parent != null && !(parent is Border))
                {
                    parent = VisualTreeHelper.GetParent(parent);

                }
                if (parent is Border border)
                {
                    parent = VisualTreeHelper.GetParent(parent);
                    while (parent != null && !(parent is Border))
                    {
                        parent = VisualTreeHelper.GetParent(parent);

                    }
                    if (parent is Border border1)
                    {
                        border1.Child = new XemChiTiet(int.Parse(roomNumbertxt.Text));
                    }
                }

            }
        }
        
        private void TraPhongCommand(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn trả phòng này?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                PhongService phongService = new PhongService();
                phongService.Delete(int.Parse(roomNumbertxt.Text));
                NhanKhauService nhanKhauService = new NhanKhauService();
                nhanKhauService.DeleteFrom(int.Parse(roomNumbertxt.Text));
                DependencyObject parent = VisualTreeHelper.GetParent(this);
                while (parent != null && !(parent is Grid))
                {
                    parent = VisualTreeHelper.GetParent(parent);

                }
                if (parent is Grid grid)
                {
                    while (parent != null && !(parent is Border))
                    {
                        parent = VisualTreeHelper.GetParent(parent);

                    }
                    if (parent is Border border)
                    {
                        parent = VisualTreeHelper.GetParent(parent);
                        while (parent != null && !(parent is Border))
                        {
                            parent = VisualTreeHelper.GetParent(parent);

                        }
                        if (parent is Border border1)
                        {
                            border1.Child = new ListRoom();
                        }
                    }

                }
                //phongService.Delete(int.Parse(roomNumbertxt.Text));

            }
        }
        private void ThemKhach(object sender, RoutedEventArgs e)
        {
            ThemKhach2 themKhach = new ThemKhach2(int.Parse(roomNumbertxt.Text));
            Application.Current.MainWindow.Opacity = 0.2;
            themKhach.ShowDialog();
            Application.Current.MainWindow.Opacity = 1;
            DependencyObject parent = VisualTreeHelper.GetParent(this);
            while (parent != null && !(parent is Grid))
            {
                parent = VisualTreeHelper.GetParent(parent);

            }
            if (parent is Grid grid)
            {
                while (parent != null && !(parent is Border))
                {
                    parent = VisualTreeHelper.GetParent(parent);

                }
                if (parent is Border border)
                {
                    parent = VisualTreeHelper.GetParent(parent);
                    while (parent != null && !(parent is Border))
                    {
                        parent = VisualTreeHelper.GetParent(parent);

                    }
                    if (parent is Border border1)
                    {
                        border1.Child = new ListRoom();
                    }
                }

            }

        }

    }
}
