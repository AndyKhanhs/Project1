using Prism.Commands;
using Project1.Models;
using System;
using System.Collections;
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
    /// Interaction logic for TienPhong1.xaml
    /// </summary>
    public partial class TienPhong1 : UserControl, INotifyPropertyChanged
    {
        private string _filterText;
        private List<TGNop> _tg;
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
        public List<TGNop> Tg
        {
            get { return _tg; }
            set {
                if (_tg != value)
                {
                    _tg = value;
                    OnPropertyChanged(nameof(Tg));
                }
            }
        }

        public DelegateCommand FilterCommand { get; set; }
        public DelegateCommand<TGNop> TgCommand { get; set; }
      
        public TienPhong1()
        {

            InitializeComponent();
            using (var dbContext=new MyDbContext())
            {
                Tg = dbContext.ListTGNop.ToList();
            }
            DataContext = this;
            TgCommand = new DelegateCommand<TGNop>(XemChiTietTienPhong);
            FilterCommand = new DelegateCommand(FilterItem);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddTienPhong(object sender, RoutedEventArgs e)
        {
            TaoTienPhong taoTienPhong=new TaoTienPhong();
            Application.Current.MainWindow.Opacity = 0.2;
            taoTienPhong.ShowDialog();
            Application.Current.MainWindow.Opacity = 1;
        }

        private void XemChiTietTienPhong(TGNop t)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(this);
            while (parent != null && !(parent is Border))
            {
                parent = VisualTreeHelper.GetParent(parent);

            }
            if (parent is Border border)
            {
                DisplayTienPhong displayTienPhong = new DisplayTienPhong(t.Mon, t.Ye);
                border.Child=displayTienPhong;

            }

        }
        private void FilterItem()
        {
            
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                using (var dbContext = new MyDbContext())
                {
                    Tg=dbContext.ListTGNop.ToList();
                }
            }
            else
            {
                using (var dbContext = new MyDbContext())
                {
                    Tg = dbContext.ListTGNop
                  .Where(x => FilterText.Contains(x.Mon.ToString()) || FilterText.Contains(x.Ye.ToString()) || x.Mon.ToString().Contains(FilterText) || x.Ye.ToString().Contains(FilterText))
                  .ToList();
                }
            }
        }
    }
}
