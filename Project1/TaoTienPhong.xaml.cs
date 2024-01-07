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

namespace Project1
{
    /// <summary>
    /// Interaction logic for TaoTienPhong.xaml
    /// </summary>
    public partial class TaoTienPhong : Window
    {
        public int Mon {  get; set; }
        public int Ye { get; set; }
        public TaoTienPhong()
        {
            InitializeComponent();
            for (int i = -1; i <= 4; i++)
            {
                AddComboBoxItem(DateTime.Now.Year+i);
            }
            Thang.SelectedItem = DateTime.Now.Month;
            Nam.SelectedItem = DateTime.Now.Year;
        }
        private void AddComboBoxItem(int value)
        {
            // Create a new ComboBoxItem
            ComboBoxItem newItem = new ComboBoxItem();

            // Set the content (displayed text) of the ComboBoxItem
            newItem.Content = value;

            // Add the ComboBoxItem to the ComboBox's Items collection
            Nam.Items.Add(newItem);
        }
        private void CancelTao(object sender,RoutedEventArgs e)
        {
            this.Close();
        }
        private void TaoTP(object sender, RoutedEventArgs e)
        {
            this.Close();
            Border mainBorder = (Border)Application.Current.MainWindow.FindName("MainBorder");

            if (mainBorder != null)
            {
                string Monstr = ((ComboBoxItem)Thang.SelectedItem).Content.ToString();
                string Yestr = ((ComboBoxItem)Nam.SelectedItem).Content.ToString();

                if (int.TryParse(Monstr, out int result1) && int.TryParse(Yestr, out int result2))
                {
                    mainBorder.Child = new TienPhongMoi(result1, result2);
                }
            }
        }
    }
}
