using ShopWpfApp.Model;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace ShopWpfApp.View.Pages.Tovar
{
    /// <summary>
    /// Логика взаимодействия для TovarMainPage.xaml
    /// </summary>
    public partial class TovarMainPage : Page
    {
        public TovarMainPage()
        {
            InitializeComponent();
            if (!MainWindow.IsAdmin) adminPanel.Visibility = Visibility.Collapsed;
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _list = ShopEntities.GetContext().Material.Where(q => !q.IsDeleted).ToList();
            DGrid.ItemsSource = _list;
        }
        List<Material> _list;
        private void AddClick(object sender, RoutedEventArgs e)
        {
            Nav.frame.Navigate(new TovarAddEditPage(null));
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem == null)
            {
                MessageBox.Show("Нужно выделить запись!");
                return;
            }
            Nav.frame.Navigate(new TovarAddEditPage(DGrid.SelectedItem as Material));
        }

        private void DelClick(object sender, RoutedEventArgs e)
        {
            if (DGrid.SelectedItem == null)
            {
                MessageBox.Show("Нужно выделить запись!");
                return;
            }
            var item = DGrid.SelectedItem as Material;
            item.IsDeleted = true;
            ShopEntities.GetContext().SaveChanges();
            Page_Loaded(null, null);
        }

        private void ExcelClick(object sender, RoutedEventArgs e)
        {
            var items = ShopEntities.GetContext().Material.Where(q => !q.IsDeleted).ToList();
            int StartRowIndex = 1;
            var application = new Excel.Application();
            application.SheetsInNewWorkbook = 1;//колво страниц
            Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);

            Excel.Worksheet worksheet = application.Worksheets.Item[1];
            worksheet.Name = "Товары";
            worksheet.Cells[1][StartRowIndex] = "Код";
            worksheet.Cells[2][StartRowIndex] = "Наименование";
            worksheet.Cells[3][StartRowIndex] = "Количество";
            worksheet.Cells[4][StartRowIndex] = "Цена";
            StartRowIndex++;

            Excel.Range headerRange = worksheet.Range[worksheet.Cells[1][StartRowIndex], worksheet.Cells[4][StartRowIndex]];
            headerRange.Merge();
            headerRange.Value = "Товары";
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            StartRowIndex++;
            foreach (var i in items)
            {
                worksheet.Cells[1][StartRowIndex] = i.Id;
                worksheet.Cells[2][StartRowIndex] = i.Name;
                worksheet.Cells[3][StartRowIndex] = i.Amount;
                worksheet.Cells[4][StartRowIndex] = i.Price;
                StartRowIndex++;
            }
            worksheet.Columns.AutoFit();
            application.Visible= true;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTextBox.Text.Length == 0)
            {
                DGrid.ItemsSource = _list;
                return;
            }
            string text = SearchTextBox.Text.ToLower();
            DGrid.ItemsSource = _list.Where(q=>q.Name.ToLower().Contains(text)).ToList();
        }
    }
}
