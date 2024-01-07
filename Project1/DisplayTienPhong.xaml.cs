using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions;
using OfficeOpenXml.Style;
using Prism.Commands;
using Project1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for DisplayTienPhong.xaml
    /// </summary>
    public partial class DisplayTienPhong : UserControl,INotifyPropertyChanged
    {
        private int _summ;
        private int _sumDaNop;
        private int _sumConThieu;
        private List<TienPhongFull> _list;
        public int Mon {  get; set; }
        public int Ye { get; set; }
        public int Summ
        {
            get { return _summ; }
            set
            {
                if (_summ != value)
                {
                    _summ = value;
                    OnPropertyChanged(nameof(Summ));
                }
            }
        }
        public int SumDaNop
        {
            get { return _sumDaNop; }
            set
            {
                if (_sumDaNop != value)
                {
                    _sumDaNop = value;
                    OnPropertyChanged(nameof(SumDaNop));
                }
            }
        }

            public int SumConThieu
        {
            get { return _sumConThieu; }
            set
            {
                if (_sumConThieu != value)
                {
                    _sumConThieu = value;
                    OnPropertyChanged(nameof(SumConThieu));
                }
            }
        }
        public List<TienPhongFull> ListTienPhong
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

        public DelegateCommand<TienPhongFull> DoubleClickCommand { get; set; }

        public DisplayTienPhong(int mon,int ye )
        {           
            
            InitializeComponent();
            Mon = mon;
            Ye = ye;
            DataContext = this;
            ListTienPhong = new List<TienPhongFull>();
            using (var moneyContext=new MoneyContext(Mon, Ye))
            {
                List<TienPhong> lis = moneyContext.GetListTienPhong();
                foreach(TienPhong t in lis)
                {
                    ListTienPhong.Add(new TienPhongFull(t.Id,t.NuocCu,t.NuocMoi,t.DienCu,t.DienMoi,t.TNo,t.DaNop));
                    
                }
            }
            foreach(TienPhongFull t in ListTienPhong)
            {
                Summ += t.Tong;
                SumDaNop += t.DaNop;
                SumConThieu += t.ConThieu;
            }
            DoubleClickCommand = new DelegateCommand<TienPhongFull>(SelectItemTienPhong);
        }
       
            
        private void SelectItemTienPhong(TienPhongFull t)
        {

            using (var moneyContext = new MoneyContext(Mon, Ye))
            {
                
                UpdateTienPhong updateTienPhong = new UpdateTienPhong(Mon, Ye, t);
                Application.Current.MainWindow.Opacity = 0.2;
                updateTienPhong.ShowDialog();
                
                List<TienPhong> lis = moneyContext.GetListTienPhong();
                List<TienPhongFull> List1 = new List<TienPhongFull>();
               foreach (TienPhong tt in lis)
                {
                    List1.Add(new TienPhongFull(tt.Id, tt.NuocCu, tt.NuocMoi, tt.DienCu, tt.DienMoi, tt.TNo, tt.DaNop));
                }
                ListTienPhong = List1;
                SumConThieu = 0; Summ = 0;SumDaNop = 0;
                foreach (TienPhongFull tt in ListTienPhong)
                {
                    Summ += tt.Tong;
                    SumDaNop += tt.DaNop;
                    SumConThieu += tt.ConThieu;
                }
                
                Application.Current.MainWindow.Opacity = 1;
                
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
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void ExportToExcel( string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    MessageBoxResult result = MessageBox.Show("File already exists. Do you want to continue exporting?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }

                    int counter = 1;
                    string originalFileName = System.IO.Path.GetFileNameWithoutExtension(filePath);
                    string fileExtension = System.IO.Path.GetExtension(filePath);
                    string directory = System.IO.Path.GetDirectoryName(filePath);
                    string newFileName = originalFileName;

                    // Modify the file name by appending a counter until a unique name is found
                    while (File.Exists(System.IO.Path.Combine(directory, newFileName + fileExtension)))
                    {
                        counter++;
                        newFileName = $"{originalFileName} ({counter})";
                    }

                    filePath = System.IO.Path.Combine(directory, newFileName + fileExtension);
                }
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (ExcelPackage package = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Data");

                    // Set column widths based on your DataGrid layout
                    for (int i = 0; i < myDataGrid.Columns.Count; i++)
                    {
                        worksheet.Column(i + 1).Width = myDataGrid.Columns[i].ActualWidth; // Adjust the divisor as needed
                    }

                    worksheet.Cells[1, 1].Value = "Phòng";
                    worksheet.Cells[1, 1, 2, 1].Merge = true;
                    worksheet.Cells[1, 1, 2, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    worksheet.Cells[1, 2].Value = "Giá";
                    worksheet.Cells[1, 2, 2, 2].Merge = true;
                    worksheet.Cells[1, 2, 2, 2].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    worksheet.Cells[1, 3].Value = "Điện";
                    worksheet.Cells[1, 3, 1, 6].Merge = true;
                    worksheet.Cells[1, 3, 1, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    worksheet.Cells[2, 3].Value = "Số cũ";
                    worksheet.Cells[2, 3].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    worksheet.Cells[2, 4].Value = "Số mới";
                    worksheet.Cells[2, 4].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    worksheet.Cells[2, 5].Value = "Tiêu thụ";
                    worksheet.Cells[2, 5].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    worksheet.Cells[2, 6].Value = "Thành tiền";
                    worksheet.Cells[2, 6].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    worksheet.Cells[1, 7].Value = "Nước";
                    worksheet.Cells[1, 7, 1, 10].Merge = true;
                    worksheet.Cells[1, 7, 1, 10].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    worksheet.Cells[2, 7].Value = "Số cũ";
                    worksheet.Cells[2, 7].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    worksheet.Cells[2, 8].Value = "Số mới";
                    worksheet.Cells[2, 8].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    worksheet.Cells[2, 9].Value = "Tiêu thụ";
                    worksheet.Cells[2, 9].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    worksheet.Cells[2, 10].Value = "Thành tiền";
                    worksheet.Cells[2, 10].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    worksheet.Cells[1, 12].Value = "Vệ sinh";
                    worksheet.Cells[1, 12, 2, 12].Merge = true;
                    worksheet.Cells[1, 12, 2, 12].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    worksheet.Cells[1, 11].Value = "Mạng";
                    worksheet.Cells[1, 11, 2, 11].Merge = true;
                    worksheet.Cells[1, 11, 2, 11].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    worksheet.Cells[1, 13].Value = "Tổng tiên";
                    worksheet.Cells[1, 13, 2, 13].Merge = true;
                    worksheet.Cells[1, 13, 2, 13].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    worksheet.Cells[1, 14].Value = "Nợ tháng trước";
                    worksheet.Cells[1, 14, 2, 14].Merge = true;
                    worksheet.Cells[1, 14, 2, 14].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    worksheet.Cells[1, 15].Value = "Đã nộp";
                    worksheet.Cells[1, 15, 2, 15].Merge = true;
                    worksheet.Cells[1, 15, 2, 15].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    worksheet.Cells[1, 16].Value = "Còn thiếu";
                    worksheet.Cells[1, 16, 2, 16].Merge = true;
                    worksheet.Cells[1, 16, 2, 16].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Medium);
                    // Write the data to Excel
                    
                    for (int row = 0; row < myDataGrid.Items.Count; row++)
                    {
                        for (int col = 0; col < myDataGrid.Columns.Count; col++)
                        {
                            int rowIndex = row + 3;
                            TienPhongFull rowData = (TienPhongFull)myDataGrid.Items[row];
                            string cellValue=null;
                            switch (col)
                            {
                                case 0:
                                    cellValue = rowData.Id.ToString();break;
                                case 1:
                                    cellValue = rowData.Gia.ToString(); break;
                                case 2:
                                    cellValue = rowData.DienCu.ToString(); break;
                                case 3:
                                    cellValue = rowData.DienMoi.ToString(); break;
                                case 4:
                                    cellValue = rowData.TieuThuDien.ToString(); break;
                                case 5:
                                    cellValue = rowData.ThanhTienDien.ToString(); break;
                                case 6:
                                    cellValue = rowData.NuocCu.ToString(); break;
                                case 7:
                                    cellValue = rowData.NuocMoi.ToString(); break;
                                case 8:
                                    cellValue = rowData.TieuThuNuoc.ToString(); break;
                                case 9:
                                    cellValue = rowData.ThanhTienNuoc.ToString(); break;
                                case 10:
                                    cellValue = rowData.Mang.ToString(); break;
                                case 11:
                                    cellValue = rowData.VeSinh.ToString(); break;
                                case 12:
                                    cellValue = rowData.TNo.ToString(); break;
                                case 13:
                                    cellValue = rowData.Tong.ToString(); break;
                                case 14:
                                    cellValue = rowData.DaNop.ToString(); break;
                                case 15:
                                    cellValue = rowData.ConThieu.ToString(); break;

                            }
                            worksheet.Cells[rowIndex, col + 1].Value = cellValue;
                            worksheet.Cells[rowIndex, col + 1].Style.Border.Top.Style=ExcelBorderStyle.Thin;
                            worksheet.Cells[rowIndex, col + 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            worksheet.Cells[rowIndex, col + 1].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                            worksheet.Cells[rowIndex, col + 1].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                        }
                    }

                    worksheet.Cells.AutoFitColumns();

                    package.SaveAs(new FileInfo(filePath));

                    MessageBox.Show("Data exported successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel("C:\\Users\\Admin\\OneDrive - Hanoi University of Science and Technology\\Documents\\Tiền phòng nhà 10\\Tiền_phòng_tháng_" + Mon + "_" + Ye + ".xlsx");
        }
    }
    public class TienPhongFull:INotifyPropertyChanged
    {
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
        public int Id { get; set; }
        public int Gia {  get; set; }
        public int DienCu
        {
            get { return _dienCu; }
            set
            {
                if (_dienCu != value)
                {
                    _dienCu = value;
                    OnPropertyChanged(nameof(DienCu));
                    UpdateTieuThuDien();
                }
            }
        }

        public int DienMoi
        {
            get { return _dienMoi; }
            set
            {
                if (_dienMoi != value)
                {
                    _dienMoi = value;
                    OnPropertyChanged(nameof(DienMoi));
                    UpdateTieuThuDien();
                }
            }
        }

        public int TieuThuDien
        {
            get { return _tieuThuDien; }
            set
            {
                if (_tieuThuDien != value)
                {
                    _tieuThuDien = value;
                    OnPropertyChanged(nameof(TieuThuDien));
                }
            }
        }        
        public int ThanhTienDien {
            get { return _thanhTienDien; }
            set
            {
                if (_thanhTienDien != value)
                {
                    _thanhTienDien = value;
                    OnPropertyChanged(nameof(ThanhTienDien));
                }
            }
        }
        private void UpdateTieuThuDien()
        {
            TieuThuDien = DienMoi - DienCu;
            ThanhTienDien = TieuThuDien * 4000;
            Tong = Gia + ThanhTienDien + ThanhTienNuoc + TNo + VeSinh + Mang;
            ConThieu = Tong - DaNop;
        }
        public int NuocCu
        {
            get { return _nuocCu; }
            set
            {
                if (_nuocCu != value)
                {
                    _nuocCu = value;
                    OnPropertyChanged(nameof(NuocCu));
                    UpdateTieuThuNuoc();
                }
            }
        }

        public int NuocMoi
        {
            get { return _nuocMoi; }
            set
            {
                if (_nuocMoi != value)
                {
                    _nuocMoi = value;
                    OnPropertyChanged(nameof(NuocMoi));
                    UpdateTieuThuNuoc();
                }
            }
        }

        public int TieuThuNuoc
        {
            get { return _tieuThuNuoc; }
            set
            {
                if (_tieuThuNuoc != value)
                {
                    _tieuThuNuoc = value;
                    OnPropertyChanged(nameof(TieuThuNuoc));
                }
            }
        }
        public int ThanhTienNuoc
        {
            get { return _thanhTienNuoc; }
            set
            {
                if (_thanhTienNuoc != value)
                {
                    _thanhTienNuoc = value;
                    OnPropertyChanged(nameof(ThanhTienNuoc));
                }
            }
        }
        private void UpdateTieuThuNuoc()
        {
            TieuThuNuoc = NuocMoi - NuocCu;
            ThanhTienNuoc = TieuThuNuoc * 26000;
            Tong = Gia + ThanhTienDien + ThanhTienNuoc + TNo + VeSinh + Mang;
            ConThieu = Tong - DaNop;
        }
        public int Tong
        {
            get { return _tong; }
            set
            {
                if (_tong != value)
                {
                    _tong = value;
                    OnPropertyChanged(nameof(Tong));
                    
                }
            }
        }

        public int DaNop
        {
            get { return _daNop; }
            set
            {
                if (_daNop != value)
                {
                    _daNop = value;
                    OnPropertyChanged(nameof(DaNop));
                }
            }
        }
        public int ConThieu
        {
            get { return _conThieu; }
            set
            {
                if (_conThieu != value)
                {
                    _conThieu = value;
                    OnPropertyChanged(nameof(ConThieu));
                }
            }
        }
        public int VeSinh {  get; set; }
        public int Mang {  get; set; }
        public int TNo { get; set; }
        
        public TienPhongFull(int id,int nuocCu,int nuocMoi,int dienCu, int dienMoi, int tNo,int daNop)
        {
            Id = id;
            Gia = 2500000;
            NuocCu =nuocCu;
            NuocMoi =nuocMoi;
            TieuThuNuoc = NuocMoi - NuocCu;
            ThanhTienNuoc = TieuThuNuoc * 26000;
            DienCu = dienCu;
            DienMoi=dienMoi;
           
            ThanhTienDien = TieuThuDien * 4000;
            VeSinh = 20000;
            Mang = 100000;
            TNo = tNo;
            Tong = ThanhTienDien + ThanhTienNuoc + Gia + VeSinh + Mang + TNo;
            DaNop = daNop;
            ConThieu = Tong - DaNop;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }

}
