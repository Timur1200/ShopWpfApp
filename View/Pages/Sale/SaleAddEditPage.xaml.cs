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

namespace ShopWpfApp.View.Pages.Sale
{
    /// <summary>
    /// Логика взаимодействия для SaleAddEditPage.xaml
    /// </summary>
    public partial class SaleAddEditPage : Page
    {
        public SaleAddEditPage(Check c)
        {
            InitializeComponent();
            if (c == null)
            {
                _check = new Check();
                _check.Date = DateTime.Now;
                _check.Sum = 0;
                ShopEntities.GetContext().Check.Add(_check);
                ShopEntities.GetContext().SaveChanges();
            }
            else
            {
                _check = c;
            }
           
            TBlockId.Text = $"Чек №{_check.Id}";
        }
        private Check _check;
        private List<Material> _materialsList;
        private SaleList _saleList;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _materialsList = ShopEntities.GetContext().Material.Where(q => !q.IsDeleted).ToList();
            TovarCBox.ItemsSource = _materialsList;
            var listSale = ShopEntities.GetContext().SaleList.Where(q => q.CheckId==_check.Id).ToList();
            TovarLBox.ItemsSource = listSale;
            _saleList = new SaleList();
            _saleList.CheckId = _check.Id;
            DataContext= _saleList;

            int sum = 0;
            foreach(var item in listSale)
            {
                sum = sum + (item.Material.Price * item.Amount);
            }
            _check.Sum = sum; 
            TBlockSum.Text = $"Сумма: {sum} руб";
        }

        private void ReloadClick(object sender, RoutedEventArgs e)
        {
            Page_Loaded(null, null);
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            Material mat = TovarCBox.SelectedItem as Material;
            if (mat.Amount<_saleList.Amount)
            {
                MessageBox.Show("Недостаточно товаров на складе");
                return;
            }
            var item = mat;
            item.Amount = item.Amount - _saleList.Amount;
            
            ShopEntities.GetContext().SaleList.Add(_saleList);
            ShopEntities.GetContext().SaveChanges();
            Page_Loaded(null, null);
        }

        private void DelClick(object sender, RoutedEventArgs e)
        {
            if (TovarLBox.SelectedItem == null)
            {
                MessageBox.Show("Нужно выбрать товар!");
                return;
            }
            var item = TovarLBox.SelectedItem as SaleList;
            var mater = item.Material;
            mater.Amount = item.Amount + item.Amount;
            ShopEntities.GetContext().SaleList.Remove(item);
            ShopEntities.GetContext().SaveChanges() ;
            Page_Loaded(null, null);
        }

        private void TovarCBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TovarCBox.Text == "")
            {

                TovarCBox.ItemsSource = _materialsList;
                TovarCBox.SelectedItem = null;
                return;
            }
            TovarCBox.IsDropDownOpen = true;
            string Text = TovarCBox.Text.ToLower();


            TovarCBox.ItemsSource = _materialsList.Where(q => q.FullName.ToLower().Contains(Text)).ToList();
        }

        private void SelectedClick(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
