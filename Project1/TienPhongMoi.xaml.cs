using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for TienPhongMoi.xaml
    /// </summary>
    public partial class TienPhongMoi : UserControl,INotifyPropertyChanged
    {
        private int dienCu;
        private int nuocCu;
        private int dienMoi;
        private int nuocMoi;
        private int tNo;
        
        private List<TienPhong> _list;
        public int Mon { get; set; }
        public int Ye { get; set; }
        public int Mon1 {  get; set; }
        public int Ye1 { get; set; }
        public int DienMoi
        {
            get { return dienMoi; }
            set
            {
                if (dienMoi != value)
                {
                    dienMoi = value;
                    OnPropertyChanged(nameof(DienMoi));
                }
            }
        }
        public int NuocMoi
        {
            get { return nuocMoi; }
            set
            {
                if (nuocMoi != value)
                {
                    nuocMoi = value;
                    OnPropertyChanged(nameof(NuocMoi));
                }
            }
        }
        public List<TienPhong> ListTienPhong
        {
            get { return _list; }
            set
            {
                if (_list != value)
                {
                    _list = value;
                    OnPropertyChanged(nameof(ListTienPhong));
                }
            }
        }
        public TienPhongMoi()
        {

        }
        public TienPhongMoi(int mon,int ye)
        {
            InitializeComponent();
            DataContext = this;
            Mon = mon;
            Ye=ye;
            ListTienPhong = new List<TienPhong>();
            if(mon==1)
            {
                Mon1 = 12;Ye1 = ye - 1;
            }
            else
            {
                Mon1 = mon - 1; Ye1 = ye;
            }
            using (var dbContext=new MyDbContext())
            {
                List<PhongDto> listPhong = dbContext.Phong.ToList();
                
                foreach(PhongDto phong in listPhong)
                {
                    using (var moneyContect=new MoneyContext(Mon1,Ye1))
                    {
                        
                        var Phi_Phong = moneyContect.GetTienPhong(phong.Id);
                        if (Phi_Phong != null)
                        {
                            nuocCu = Phi_Phong.NuocMoi;
                            dienCu = Phi_Phong.DienMoi;
                            tNo = Phi_Phong.Tong - Phi_Phong.DaNop;
                            ListTienPhong.Add(new TienPhong(phong.Id, nuocCu, NuocMoi, dienCu, DienMoi, tNo));
                        }
                        else
                        {
                            nuocCu = phong.NuocBD;
                            dienCu = phong.DienBD;
                            tNo = 0;
                            ListTienPhong.Add(new TienPhong(phong.Id, nuocCu, NuocMoi, dienCu, DienMoi, tNo));
                        }
                    }
                    
                }
            }
            

        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LuuTienPhong(object sender, RoutedEventArgs e)
        {
            using(var moneyContext1=new MoneyContext(Mon,Ye))
            {
                using (var dbContext = new MyDbContext())
                {
                    dbContext.ListTGNop.Add(new TGNop(Mon, Ye));
                    dbContext.SaveChanges();
                    
                    moneyContext1.CreateCustomTable();
                    var _list = dbContext.ListTGNop.ToList();
                    
                     foreach (TienPhong tienPhong in ListTienPhong)
                    {
                        tienPhong.Tong = 2500000 + (tienPhong.NuocMoi - tienPhong.NuocCu) * 26000 + (tienPhong.DienMoi - tienPhong.DienCu) * 4000 + 20000 + 100000 + tienPhong.TNo;
                        moneyContext1.AddData(tienPhong);
                        //moneyContext1.Entry(tienPhong).State = EntityState.Added;
                        //moneyContext1.SaveChanges();
                    }
                    
                   
                    moneyContext1.SaveChanges();
                   
                    DependencyObject parent = VisualTreeHelper.GetParent(this);
                    while (parent != null && !(parent is Border))
                    {
                        parent = VisualTreeHelper.GetParent(parent);

                    }
                    if (parent is Border border)
                    {

                        border.Child = new TienPhong1();

                    }
                }
                   
            }
           
        }
        private void Back_ListMoney(object sender, RoutedEventArgs e)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(this);
            while (parent != null && !(parent is System.Windows.Controls.Border))
            {
                parent = VisualTreeHelper.GetParent(parent);

            }
            if (parent is System.Windows.Controls.Border border)
            {
                border.Child = new TienPhong1();

            }
        }
    }
}
